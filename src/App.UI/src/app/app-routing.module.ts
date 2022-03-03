import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { APPRoutes } from '@app/core/constants/constants';
import { AuthenticatedGuard } from './core/services/guards/authenticated.guard';
import { LayoutComponent } from './core/layout/layout/layout.component';

const routes: Routes = [

  {
    path: '',
    component: LayoutComponent,
    canActivate: [AuthenticatedGuard],
    children: [
      {
        path: APPRoutes.ROBOT.BASE,
        loadChildren: () => import('./modules/robot/robot.module').then(m => m.RobotModule)
      },
      {
        path: 'control',
        loadChildren: () => import('./modules/control/control.module').then(m => m.ControlModule)
      },
      {
        path: APPRoutes.LOGS.BASE,
        loadChildren: () => import('./modules/logs/logs.module').then(m => m.LogsModule)
      },
    ]
  },
  { path: '**', pathMatch: 'full', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
