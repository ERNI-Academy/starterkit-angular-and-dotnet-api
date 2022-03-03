import { Component, Inject, OnInit, OnDestroy } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { DialogComponent, DialogData } from '../dialog.component';

export interface ConfirmationComponentDialogDataInfo {
  message: string;
  onConfirm: () => void;
  onCancel?: () => void;
}

type ConfirmationComponentDialogData = DialogData<
  typeof ConfirmationComponent,
  ConfirmationComponentDialogDataInfo
>;

@Component({
  selector: 'app-confirmation',
  templateUrl: './confirmation.component.html',
  styleUrls: ['./confirmation.component.scss']
})
export class ConfirmationComponent implements OnInit, OnDestroy {
  private subscriptions: Subscription[] = [];

  message: string;

  constructor(
    private dialog: DialogComponent,
    @Inject(MAT_DIALOG_DATA) public data: ConfirmationComponentDialogData,
  ) { }

  ngOnInit(): void {
    this.message = this.data.info.message;

    this.subscriptions.push(
      this.dialog.closeDialog$.subscribe(response => {
        if (response) {
          this.data.info.onConfirm();
          this.dialog.closeDialog();
        } else {
          this.data.info.onCancel?.();
        }
      }),
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(x => x.unsubscribe());
  }

}
