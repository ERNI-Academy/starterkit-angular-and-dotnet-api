import { ScrollableOverflowWrapperModule } from './scrollable-overflow-wrapper/scrollable-overflow-wrapper.module';
import { SharedDirectivesModule } from './../directives/directives.module';
import { MaterialModule } from './../material/material.module';
import { CommonModule } from '@angular/common';
import { MenuModule } from './menu/menu.module';

import { NgModule } from '@angular/core';
import { OperationStatusComponent } from './operation-status/operation-status.component';
import { DialogComponent } from './dialog/dialog.component';
import { AlertStatusIconComponent } from './alert-status-icon/alert-status-icon.component';
import { TranslateModule } from '@ngx-translate/core';
import { InputModule } from './input/input.module';
import { UploadFileComponent } from './upload-file/upload-file.component';
import { ConfirmationComponent } from './dialog/confirmation/confirmation.component';
import { VideoModule } from './video/video.module';
import { SnackbarComponent } from './snackbar/snackbar.component';

@NgModule({
  imports: [
    CommonModule,
    TranslateModule,
    MaterialModule,
    InputModule,
    SharedDirectivesModule,
    ScrollableOverflowWrapperModule,
  ],
  declarations: [
    DialogComponent,
    AlertStatusIconComponent,
    OperationStatusComponent,
    UploadFileComponent,
    ConfirmationComponent,
    SnackbarComponent,
  ],
  exports: [
    MenuModule,
    OperationStatusComponent,
    InputModule,
    UploadFileComponent,
    VideoModule,
  ],
})
export class SharedComponentsModule {}
