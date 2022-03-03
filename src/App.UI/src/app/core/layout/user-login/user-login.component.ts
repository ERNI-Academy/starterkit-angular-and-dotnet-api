import { SnackbarService, SnackBarType } from '@app/shared/services/snackbar.service';
import { TranslateService } from '@ngx-translate/core';
import { UserService } from './../../services/user/user.service';
import {
  DialogComponent,
  DialogData,
} from './../../../shared/component/dialog/dialog.component';
import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
} from '@angular/forms';
import { AbstractControlOrT } from '@app/shared/utils/types';
import { marker } from '@biesbjerg/ngx-translate-extract-marker';
import { FormHelper } from '@app/shared/utils/form.helper';
import { HttpErrorResponse } from '@angular/common/http';
import { ValidationResult } from '@app/shared/component/input/input/input.component';

interface FormGroupModel<T = unknown> {
  userName: AbstractControlOrT<T, string>;
  password: AbstractControlOrT<T, string>;
  newPassword?: AbstractControlOrT<T, string>;
  confirmPassword?: AbstractControlOrT<T, string>;
}

export interface UserLoginComponentDialogDataInfo {
  changePasswordMode?: boolean;
}

type UserLoginComponentDialogData = DialogData<
  typeof UserLoginComponent,
  UserLoginComponentDialogDataInfo
>;

function generalFromValidator(changePasswordMode: boolean, translate: TranslateService): ValidatorFn {
  return (group: FormGroup): ValidationErrors => {
    const changePassValidator = matchingPasswordValidator(group);
    if (changePassValidator) { return changePassValidator; }

    // continue with the rest of validators
  };

  function matchingPasswordValidator(group: FormGroup): ValidationErrors {
    if (!changePasswordMode) { return; }

    const controls: FormGroupModel<AbstractControl> = group.controls as any;
    const pwdControl = controls.newPassword;
    const confirmPwdControl = controls.confirmPassword;

    // in case fields has not been added yet
    if (!pwdControl || !confirmPwdControl) { return; }

    if (confirmPwdControl.errors && !confirmPwdControl.errors.mismatch) {
      // return if another validator has already found an error on the matchingControl
      return;
    }

    // set error on matchingControl if validation fails
    if (pwdControl.value !== confirmPwdControl.value) {
      confirmPwdControl.setErrors({
        mismatch: translate.instant(marker('layout.login.errors.mismatch')),
      });
    } else {
      confirmPwdControl.setErrors(null);
    }

  }
}

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.scss'],
})
export class UserLoginComponent implements OnInit {
  formGroup: FormGroup;
  changePasswordMode: boolean;

  get formControls(): FormGroupModel<FormControl>  {
    return this.formGroup.controls as any;
  }

  private formHelper: FormHelper;

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private dialog: DialogComponent,
    private snackbarService: SnackbarService,
    private translateService: TranslateService,
    @Inject(MAT_DIALOG_DATA) public data: UserLoginComponentDialogData // to pass data to the dialog
  ) {}

  getValidationMessage(fieldName: keyof FormGroupModel): ValidationResult {
    return this.formHelper?.getValidationMessageFormControl(fieldName);
  }

  ngOnInit(): void {
    if (this.data.info?.changePasswordMode) {
      this.changePasswordMode = true;
    }

    this.formGroup = this.formBuilder.group(
      {
        userName: new FormControl(''),
        password: new FormControl(''),
      } as FormGroupModel<FormControl>,
      { validator: generalFromValidator(this.changePasswordMode, this.translateService) }
    );

    if (this.changePasswordMode) {
      this.formGroup.addControl('newPassword', new FormControl(''));
      this.formGroup.addControl('confirmPassword', new FormControl(''));
    }

    this.formHelper = new FormHelper({
      formGroup: this.formGroup,
      // translateService: this.translateService,
    });

    this.dialog.closeDialog$.subscribe(async (response) => {
      if (response) {
        // console.log('values', this.formGroup.valid, [this.formGroup.value]);
        await this.formHelper.genericSubmitHandle({
          dialog: this.dialog,
          handleSubmit: () => this.onSubmit(),
          handleError: error => this.handleError(error),
        });
      }
    });
  }

  async onSubmit() {
    // this.submitted = true;
    if (this.changePasswordMode) {
      await this.onSubmitChangePassword();
    } else {
      await this.onSubmitLogin();
    }
  }

  async onSubmitLogin() {
    const data: FormGroupModel = this.formGroup.value;
    await this.userService.login(data.userName, data.password);
  }

  async onSubmitChangePassword() {
    const data: FormGroupModel = this.formGroup.value;
    await this.userService.changePassword(data.password, data.newPassword);
    this.snackbarService.openSnackBarTranslated(
      marker('layout.login.changePasswordSuccess'),
      SnackBarType.success,
    );
  }

  private handleError(error: HttpErrorResponse) {
    const apiMessage = error.error.message || error.error.Message;

    if (error.status === 400 && !apiMessage) {
      // in the 'login' error the message is here: error.error.Message

      // happens in the 'changePassword'
      // Example: {error: {errors: {Password: ["The field Password must be a string or array type with a minimum length of '6'."]}}}
      const passwordError = error.error.errors?.Password?.[0];
      const generalErrorText = this.translateService.instant('base.messages.general-error');

      const apiMessage = passwordError || 'unkwown api error...';
      const message = `${generalErrorText}: ${apiMessage}`;

      this.formGroup.setErrors({
        generalError: message,
      });
    } else {
      this.formHelper.setGeneralError(error);
    }
  }

  getGeneralError() {
    return this.formHelper?.getGeneralError();
  }
}
