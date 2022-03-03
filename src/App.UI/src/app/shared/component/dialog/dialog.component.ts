import { marker } from '@biesbjerg/ngx-translate-extract-marker';

import {
  Component,
  Inject,
  ViewChild,
  ComponentFactoryResolver,
  ViewContainerRef,
  ComponentRef,
  OnInit,
  OnDestroy,
  Type,
  ViewEncapsulation,
} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { Observable, Subject, Subscription } from 'rxjs';

export interface DialogData<TInnerComponentType extends Type<unknown>, TExtraInfo = unknown> {
  innerComponentType: TInnerComponentType;
  title?: string;
  titleArgs?: any;
  buttonSecondaryText?: string;
  buttonPrimaryText?: string;
  info?: TExtraInfo;
}

export interface CreateComponentOptions<TInnerComponentType extends Type<unknown>, TExtraInfo = unknown> {
  matDialog: MatDialog;
  dialogConfig?: Partial<MatDialogConfig>;
  dialogData: DialogData<TInnerComponentType, TExtraInfo>;
  onOpened?: (componentInstance: InstanceType<TInnerComponentType>) => void;
}

const defaultDialogConfig: Partial<MatDialogConfig> = {
  disableClose: true, // closes when click backdrop or 'ESC'
  autoFocus: false,
  width: '600px',
  panelClass: 'app-dialog-container',
  restoreFocus: false,
};

const defaultDialogData: Partial<DialogData<any>> = {
  buttonSecondaryText: marker('base.action.cancel'),
  buttonPrimaryText: marker('base.action.save'),
};

export function createDialogComponent<TInnerComponentType extends Type<unknown>, TExtraInfo = unknown>(
  options: CreateComponentOptions<TInnerComponentType, TExtraInfo>,
) {
  const dialogConfig = new MatDialogConfig();
  const dialogConfigOptions = { ...defaultDialogConfig, ...options.dialogConfig };
  const { disableClose, autoFocus, width, panelClass } = dialogConfigOptions;
  dialogConfig.disableClose = disableClose;
  dialogConfig.autoFocus = autoFocus;
  dialogConfig.width = width;
  dialogConfig.panelClass = panelClass;
  dialogConfig.data = options.dialogData;

  const matDialogRef: MatDialogRef<DialogComponent> = options.matDialog.open(DialogComponent, dialogConfig);
  const afterOpenSub: Subscription = matDialogRef.afterOpened().subscribe(() => {
    afterOpenSub.unsubscribe();

    if (options.onOpened) {
      const innerComponentInstance: InstanceType<TInnerComponentType> =
        matDialogRef.componentInstance.componentRef.instance;

      options.onOpened(innerComponentInstance);
    }
  });
}

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class DialogComponent implements OnInit, OnDestroy {
  @ViewChild('dialogContent', { read: ViewContainerRef, static: true }) vcRef: ViewContainerRef;
  componentRef: ComponentRef<any>;
  closeDialog$: Observable<boolean>;
  private closeDialogEvent: Subject<boolean> = new Subject();
  private _disabledButtons = false;
  get disabledButtons() {
    return this._disabledButtons;
  }

  constructor(
    public dialogRef: MatDialogRef<DialogComponent>,
    private resolver: ComponentFactoryResolver,
    @Inject(MAT_DIALOG_DATA) public data: DialogData<any>,
  ) {
    this.closeDialog$ = this.closeDialogEvent.asObservable();

    this.data = { ...defaultDialogData, ...data };
  }

  ngOnInit() {
    const factory = this.resolver.resolveComponentFactory(this.data.innerComponentType);
    this.componentRef = this.vcRef.createComponent(factory);
  }

  okButton() {
    this.closeDialogEvent.next(true);
  }

  closeDialog(): void {
    this.closeDialogEvent.next(false);
    this.dialogRef.close();
  }

  ngOnDestroy() {
    if (this.componentRef) {
      this.componentRef.destroy();
      this.closeDialogEvent.complete();
    }
  }

  disableButtons() {
    this._disabledButtons = true;
  }

  enableButtons() {
    this._disabledButtons = false;
  }
}
