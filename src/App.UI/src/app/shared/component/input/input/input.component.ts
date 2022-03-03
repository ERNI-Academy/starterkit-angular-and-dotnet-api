import {
  AfterViewInit,
  Component,
  DoCheck,
  EventEmitter,
  forwardRef,
  Input,
  OnChanges,
  OnDestroy,
  OnInit,
  Optional,
  Output,
  Self,
  SimpleChanges,
  ViewChild,
  ViewChildren,
} from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  FormBuilder,
  FormControl,
  FormGroup,
  NgControl,
  NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  ValidationErrors,
  Validator,
  Validators,
} from '@angular/forms';
import { FormHelper } from '@app/shared/utils/form.helper';
import { AbstractControlOrT } from '@app/shared/utils/types';
import { Subscription } from 'rxjs';

export interface ValidationResult {
  valid?: boolean;
  text?: string;
}

interface FormGroupModel<T = unknown> {
  customInput: AbstractControlOrT<T, string>;
}

export type InputDataType =
  | 'color'
  | 'date'
  | 'email'
  | 'month'
  | 'number'
  | 'password'
  | 'search'
  | 'tel'
  | 'text'
  | 'textarea'
  | 'time'
  | 'url'
  | 'week';

/**
 * There is no native way to be notified from the parent control when it's marked as 'touched' or 'pristine' using
 * externally a method like 'MarkAsTouched'. (which is necessary if we want to mark all inputs as touched programatically
 * when 'onSubmit')
 * As a workaround we need to Inject 'NgControl' and control it's state in 'ngDoCheck'.
 * This also implies we cannot use 'NG_VALUE_ACCESSOR', 'NG_VALIDATORS' providers.
 *
 * 'ngDoCheck' may present performance issues as its called many times so we opt to hack the 'MarkAsTouched, MarkAsPristine,...'
 * methods.
 *
 * More info:
 * https://github.com/angular/angular/issues/10887
 * https://stackoverflow.com/questions/44730711/how-do-i-know-when-custom-form-control-is-marked-as-pristine-in-angular
 * https://stackoverflow.com/questions/39809084/injecting-ngcontrol-in-custom-validator-directive-causes-cyclic-dependency
 *
 */
@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.scss'],
  providers: [
    // {
    //   provide: NG_VALUE_ACCESSOR,
    //   useExisting: forwardRef(() => InputComponent),
    //   multi: true,
    // },
    // {
    //   provide: NG_VALIDATORS,
    //   useExisting: forwardRef(() => InputComponent),
    //   multi: true,
    // },
  ],
})
export class InputComponent implements OnInit, OnDestroy, OnChanges, AfterViewInit, ControlValueAccessor, Validator {
  @Input() label: string;
  // tslint:disable-next-line: max-line-length
  @Input() dataType: InputDataType;
  @Input() required: boolean;
  @Input() discreet: boolean;
  @Input() hideValidationContainer: boolean;
  @Input() decimals = 0;
  /** Important: the step value is the abb-input is strongly related with the native html "step" value.
   * This means that if we don't set it and number has decimal, it won't be valid.
   * Also if the step value is '0.005' the steps with the keyboard will work accordingly BUT a value like
   * '0.001' would be invalid because only the right steps are allowed 'x.xx5 - x.xx0'.
   */
  @Input() step: number; // example: 0.001
  @Input() min: number;
  @Input() max: number;
  @Input() disabled: boolean;

  @Output() valueChanged = new EventEmitter();

  calculatedStep: number;
  calculatedPlaceHolder = '';


  formGroup: FormGroup;

  get formControls(): FormGroupModel<FormControl>  {
    return this.formGroup.controls as any;
  }

  errorValidationResult: ValidationResult;

  private _value: any;
  private subscriptions: Subscription[] = [];
  private formHelper: FormHelper;

  // private control: AbstractControl;

  constructor(
    private formBuilder: FormBuilder,
    @Self() private ngControl: NgControl,
  ) {
    this.formGroup = this.formBuilder.group({
      customInput: new FormControl(''),
    } as FormGroupModel<FormControl>);

    // ngControl.control is undefined here (wait for ngOnInit)
    ngControl.valueAccessor = this;

    this.subscriptions.push(
      this.formGroup.valueChanges.subscribe((value: FormGroupModel) => {
        this.onChange(value.customInput);
        this.onTouched();
      }),
    );
  }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes.disabled) {
      this.setDisabledState(changes.disabled.currentValue);
    }
  }

  updateErrorValidationResult() {
    this.errorValidationResult = this.getValidationMessage();
  }


  private getValidationMessage(): ValidationResult {
    return this.formHelper?.getValidationMessage();
  }

  ngOnInit(): void {
    this.formHelper = new FormHelper({
      formControl: this.ngControl.control as FormControl,
    });
    /** Important: emitEvent = false. We don't want events to be emitted in NgOnInit or the input will be marked as "dirty" just to set
     * this property.
     */
    this.setDisabledState(this.disabled, false);
    
    this.subscriptions.push(
      // This will be triggered everytime a new "setError" is called in the parent "formControl"
      this.ngControl.control.statusChanges.subscribe(status => {
        this.updateErrorValidationResult();
      }),
    );

    this.addExternalValidators(this.ngControl.control, this.formControls.customInput);

    this.hackTopStatesPropagation();

    this.calculateStep();
    this.calculatePlaceHolder();

    this.formGroup.markAsPristine();
    this.ngControl.control.markAsPristine();
  }

  private addExternalValidators(externalFormControl: AbstractControl, innerFormControl: AbstractControl) {
    if (externalFormControl.validator) {
      innerFormControl.setValidators([
        externalFormControl.validator,
      ]);
    }
  }

  private hackTopStatesPropagation() {
    /** Propagate 'MarkAsPristine' parent action to customControl */
    const originalMarkAsPristine = this.ngControl.control.markAsPristine;
    this.ngControl.control.markAsPristine = (opts) => {
      // console.log('markAsPristine', opts);
      // this does not really seem to have any effect on the inner abb input element when it has already been touched
      this.formControls.customInput.markAsPristine(opts);

      originalMarkAsPristine.call(this.ngControl.control, opts);
    };

    /** Propagate 'MarkAsTouched' parent action to customControl */
    const originalMarkAsTouched = this.ngControl.control.markAsTouched;
    this.ngControl.control.markAsTouched = (opts) => {
      // console.log('markAsTouched', opts);
      this.formControls.customInput.markAsTouched(opts);

      originalMarkAsTouched.call(this.ngControl.control, opts);
    };
  }

  private calculateStep() {

    if (this.step !== undefined) {
      this.calculatedStep = this.step;
      return;
    }

    // calculate step based on number of decimals
    this.calculatedStep = this.decimals ?
      1 / Math.pow(10, this.decimals) :
      0;
  }

  private calculatePlaceHolder() {

    if (this.dataType === 'number' && this.calculatedStep) {
      this.calculatedPlaceHolder = this.calculatedStep.toString();
    }

  }

  ngAfterViewInit(): void {

    // without timeout "AfterViewCheckChanged..." exception is thrown

  }

  ngOnDestroy() {
    this.subscriptions.forEach(s => s.unsubscribe());
  }

  onChange: (val: any) => void = (val: any) => {
    this.valueChanged.emit(val);
  }
  onTouched = () => {};

  /** ControlValueAccessor INTERFACE */

  /** The value is set from outside */
  writeValue(obj: any): void {
    let value = obj;

    /** This 'hack' is needed as the internal abb-input 'number' component shows a 'red invalid' input when passed a
     * 0 as a value, and the input is 'required'. We work-around it converting it to a '0' string.
     */
    if (this.dataType === 'number' && obj === 0) {
      value = (obj as number)?.toString();
    }

    // propagate value custom control
    this.formControls.customInput.setValue(value);
    this._value = value;
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
  setDisabledState?(isDisabled: boolean, emitEvent: boolean = true): void {
    this.disabled = isDisabled;
    isDisabled ?
      this.formControls.customInput.disable({emitEvent}) :
      this.formControls.customInput.enable({emitEvent});
  }

  /** Validator INTERFACE */
  validate(control: AbstractControl): ValidationErrors {
    // return this.formGroup.valid ? null : { passwords: { valid: false } };
    return this.formGroup.valid ? null : this.formGroup.errors;
  }
}
