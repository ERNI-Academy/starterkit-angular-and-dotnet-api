import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

import { SnackbarComponent } from '../component/snackbar/snackbar.component';
import { TranslateService } from '@ngx-translate/core';
import { marker } from '@biesbjerg/ngx-translate-extract-marker';

export enum SnackBarType {
  error,
  info,
  success,
}

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {
  durationInSeconds = 3;

  // prettier-ignore
  constructor(
    private translateService: TranslateService,
    private snackBar: MatSnackBar,
  ) {}

  openSnackBarTranslated(resourceId: string, type: SnackBarType) {
    const message = this.translateService.instant(resourceId);
    this.openSnackBar(message, type);
  }

  openSnackBar(data: any, type: SnackBarType) {
    let panelClass: string;
    const customSnackBar = 'custom-snackbar';

    switch (type) {
      case SnackBarType.success:
        panelClass = 'snackbar-success';
        break;
      case SnackBarType.error:
        panelClass = 'snackbar-error';
        break;
    }

    this.snackBar.openFromComponent(SnackbarComponent, {
      duration: this.durationInSeconds * 1000,
      verticalPosition: 'top',
      panelClass: [customSnackBar, panelClass],
      data,
    });
  }

  defaultSuccess() {
    this.openSnackBarTranslated(
      marker('base.messages.executed-successful'),
      SnackBarType.success,
    );
  }
}
