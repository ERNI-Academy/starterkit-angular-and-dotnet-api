import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { APPRoutes } from '@app/core/constants/constants';
import { OverviewComponent } from './pages/overview/overview.component';
import { StatusComponent } from './pages/status/status.component';

const routes: Routes = [
  { path: '', redirectTo: APPRoutes.ROBOT.OVERVIEW, pathMatch: 'full' },
  { path: 'status', component: StatusComponent },
  { path: APPRoutes.ROBOT.OVERVIEW, component: OverviewComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class RobotRoutingModule {}
