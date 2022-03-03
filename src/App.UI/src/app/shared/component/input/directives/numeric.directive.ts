import { Directive, ElementRef, HostListener, Input } from '@angular/core';

@Directive({
  selector: '[appNumeric]'
})
export class NumericDirective {
  @Input() decimals = 0;
  @Input() negative = 0;
  // tslint:disable-next-line: no-input-rename
  @Input('appNumericTargetNativeElement') targetNativeElement: HTMLInputElement;
  @Input('appNumericEnabled') enabled = true;

  get targetElement(): HTMLInputElement {
    return this.targetNativeElement ?? this.el.nativeElement;
  }

  private checkAllowNegative(value: string) {
    if (this.decimals <= 0) {
      return String(value).match(new RegExp(/^-?\d+$/));
    } else {
      const regExpString =
        '^-?\\s*((\\d+(\\.\\d{0,' +
        this.decimals +
        '})?)|((\\d*(\\.\\d{1,' +
        this.decimals +
        '}))))\\s*$';
      return String(value).match(new RegExp(regExpString));
    }
  }

  private check(value: string) {
    if (this.decimals <= 0) {
      return String(value).match(new RegExp(/^\d+$/));
    } else {
      const regExpString =
        '^\\s*((\\d+(\\.\\d{0,' +
        this.decimals +
        '})?)|((\\d*(\\.\\d{1,' +
        this.decimals +
        '}))))\\s*$';
      return String(value).match(new RegExp(regExpString));
    }
  }

  private run(oldValue: string) {
    setTimeout(() => {
      const currentValue: string = this.targetElement.value;
      const allowNegative = this.negative > 0 ? true : false;

      if (allowNegative) {
        if (
          !['', '-'].includes(currentValue) &&
          !this.checkAllowNegative(currentValue)
        ) {
          this.targetElement.value = oldValue;
        }
      } else {
        if (currentValue !== '' && !this.check(currentValue)) {
          this.targetElement.value = oldValue;
        }
      }
    });
  }

  constructor(private el: ElementRef) {}

  @HostListener('keydown', ['$event'])
  onKeyDown(event: KeyboardEvent) {
    if (!this.enabled) { return; }
    this.run(this.targetElement.value);
  }

  @HostListener('paste', ['$event'])
  onPaste(event: ClipboardEvent) {
    if (!this.enabled) { return; }
    this.run(this.targetElement.value);
  }
}
