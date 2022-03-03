import { SharedModule } from './../../../shared/shared.module';
import { RobotComponentsModule } from './../components/robot-components.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StatusComponent } from './status/status.component';
import { OverviewComponent } from './overview/overview.component';

@NgModule({
    // eslint-disable-next-line prettier/prettier
    imports: [
        CommonModule,
        RobotComponentsModule,
        SharedModule,
    ],
    declarations: [
        StatusComponent,
        OverviewComponent,
    ]
})
export class RobotPagesModule {}
