import { ReactiveFormsModule } from '@angular/forms';
import { BlockUIModule } from 'ng-block-ui';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { MaterialModule } from './material/material.module';
import { InputModule } from './component/input/input.module';
import { TabsModule } from './component/tabs/tabs.module';
import { SharedComponentsModule } from './component/shared-components.module';
import { SharedDirectivesModule } from './directives/directives.module';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MaterialModule,
    TranslateModule,
    InputModule,
    SharedDirectivesModule,
    BlockUIModule,
  ],
  exports: [
    ReactiveFormsModule,
    TranslateModule,
    MaterialModule,
    InputModule,
    TabsModule,
    SharedComponentsModule,
    SharedDirectivesModule,
    BlockUIModule,
  ],
})
export class SharedModule { }
