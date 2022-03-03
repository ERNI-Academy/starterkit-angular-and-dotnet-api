import { Tab2Component } from './pages/tab2/tab2.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { APPRoutes } from '@app/core/constants/constants';
import { MainComponent } from './pages/main/main.component';
import { StatusComponent } from './pages/status/status.component';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    children: [
      { path: '', redirectTo: APPRoutes.CONTROL.STATUS, pathMatch: 'full' },
      {
        path: APPRoutes.CONTROL.STATUS,
        component: StatusComponent,
      },
      {
        path: APPRoutes.CONTROL.TAB2,
        component: Tab2Component,
      },
    ],
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ControlRoutingModule {}
