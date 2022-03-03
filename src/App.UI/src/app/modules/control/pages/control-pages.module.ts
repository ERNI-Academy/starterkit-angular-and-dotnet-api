import { SharedModule } from 'src/app/shared/shared.module';
import { ControlComponentsModule } from '../components/control-components.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StatusComponent } from './status/status.component';
import { Tab2Component } from './tab2/tab2.component';
import { MainComponent } from './main/main.component';

@NgModule({
    imports: [
        CommonModule,
        ControlComponentsModule,
        SharedModule,
    ],
    declarations: [
        StatusComponent,
        MainComponent,
        Tab2Component,
    ]
})
export class ControlPagesModule {}
