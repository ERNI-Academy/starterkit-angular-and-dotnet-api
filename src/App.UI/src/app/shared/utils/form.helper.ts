import { Subscription } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { FormControl, FormGroup, ValidationErrors } from '@angular/forms';
import { DialogComponent } from '../component/dialog/dialog.component';
import { ValidationResult } from '../component/input/input/input.component';

interface FormHelperOptions {
  formGroup?: FormGroup;
  formControl?: FormControl;
  handlePendingChanges?: (isDirty: boolean) => void;
}

interface SubmitHandleOptions {
  dialog?: DialogComponent;
  handleSubmit: () => Promise<any>;
  handleError: (error: HttpErrorResponse) => void;
}


/**
 * Example of custom validation for a field.
 *
 * @usageNotes
 * Remember to add these properties to the field to see properly the errors:
 * suppressNgControlErrors, externalValidationResult, showValidationIconWhenInvalid
 *
 * ```typescript
 * // field definition inside FormGroup
 * let fieldDefinition = {
 *      userName: new FormControl('', (control) => {
 *         return (
 *           control.value === 'AAA' && {
 *               forcedValidation: 'forced message!',
 *             }
 *           );
 *        }),
 * }
 *
 */


export class FormHelper {
  private subscriptions: Subscription[] = [];
  private hasPendingChanges = false;

  constructor(
    private options: FormHelperOptions,
  ) {
    if (options.handlePendingChanges) {
      this.subscriptions.push(
        options.formGroup?.statusChanges.subscribe(status => {
          const dirty = options.formGroup.dirty;
          // only notify when there is a change (for performance)
          if (this.hasPendingChanges !== dirty) {
            this.hasPendingChanges = dirty;
            options.handlePendingChanges(this.hasPendingChanges);
          }
        })
      );
    }
  }

  destroy() {
    this.subscriptions.forEach(x => x.unsubscribe());
  }

  getValidationMessageFormControl(fieldName: string): ValidationResult {
    const errors = this.options.formGroup?.controls[fieldName]?.errors;
    return this.getValidationMessageWithValidationErrors(errors);
  }

  getValidationMessage(): ValidationResult {
    const errors = this.options.formControl?.errors;
    return this.getValidationMessageWithValidationErrors(errors);
  }

  private getValidationMessageWithValidationErrors(errors: ValidationErrors): ValidationResult {
    if (errors) {
      const errorProperties = Object.getOwnPropertyNames(errors);
      const firstErrorProp = errorProperties[0];

      // we don't want to show the required message
      if (firstErrorProp === 'required') { return; }

      return {
        valid: false,
        // text: this.options.translateService.instant(errors[firstErrorProp]),
        text: errors[firstErrorProp],
      };
    }
  }

  setGeneralError(error: HttpErrorResponse) {
    const message = error.error?.message || error.error?.Message;
    this.options.formGroup.setErrors({
      generalError: message || 'General error...',
    });
  }

  getGeneralError(): string {
    return this.options.formGroup?.errors?.generalError;
  }

  validateAllFormFields(formGroup?: FormGroup) {
    const formGroupTarget = formGroup || this.options.formGroup;
    Object.keys(formGroupTarget.controls).forEach(field => {
      const control = formGroupTarget.get(field);
      if (control instanceof FormControl) {
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof FormGroup) {
        this.validateAllFormFields(control);
      }
    });
  }

  async genericSubmitHandle(options: SubmitHandleOptions) {
    // console.log('values', this.formGroup.valid, [this.formGroup.value]);
    try {
      if (this.options.formGroup.valid) {
        await options.handleSubmit();

        // handle pending changes
        this.options.handlePendingChanges?.(false);
        this.options.formGroup.markAsPristine();

        // on success submit close Dialog
        options.dialog?.closeDialog();
      } else {
        this.validateAllFormFields();
      }
    } catch (error) {
      const typedError: HttpErrorResponse = error;
      // handle api error
      console.error(error);
      options.handleError(error);
    }
  }
}
