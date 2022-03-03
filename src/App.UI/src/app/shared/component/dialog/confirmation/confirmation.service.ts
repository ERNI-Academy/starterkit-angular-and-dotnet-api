import { ConfirmationComponent, ConfirmationComponentDialogDataInfo } from './confirmation.component';
import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { createDialogComponent } from '../dialog.component';
import { marker } from '@biesbjerg/ngx-translate-extract-marker';

interface ConfirmationOptions {
  message: string;
  onConfirm: () => void;
  onCancel?: () => void;
}

@Injectable({
  providedIn: 'root'
})
export class ConfirmationService {

  constructor(
    private matDialog: MatDialog,
  ) { }

  openConfirmation(options: ConfirmationOptions) {
    createDialogComponent({
      matDialog: this.matDialog,
      dialogData: {
        innerComponentType: ConfirmationComponent,
        title: marker('base.confirmation.title'),
        buttonPrimaryText: marker('base.confirmation.confirm-action'),
        info: {
          message: options.message,
          onConfirm: options.onConfirm,
          onCancel: options.onCancel,
        } as ConfirmationComponentDialogDataInfo,
      },
    });
  }
}
