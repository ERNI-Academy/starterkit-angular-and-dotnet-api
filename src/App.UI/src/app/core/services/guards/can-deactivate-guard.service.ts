import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot } from '@angular/router';
import { ConfirmationService } from '@app/shared/component/dialog/confirmation/confirmation.service';
import { marker } from '@biesbjerg/ngx-translate-extract-marker';
import { Observable } from 'rxjs';

export interface IDeactivateComponent {
  hasPendingChanges: () => boolean;
}

// @Injectable({
//   providedIn: 'root'
// })
// export class DeactivateGuard implements CanDeactivate<IDeactivateComponent> {
//   component: Record<string, any>;
//   route: ActivatedRouteSnapshot;

//   constructor() {}

//   canDeactivate(
//     component: IDeactivateComponent,
//     route: ActivatedRouteSnapshot,
//     state: RouterStateSnapshot,
//     nextState: RouterStateSnapshot,
//   ): Observable<boolean> | Promise<boolean> | boolean {
//     return component.canExitWithPendingChanges();
//   }
// }

@Injectable({
  providedIn: 'root'
})
export class DeactivateGuard implements CanDeactivate<IDeactivateComponent> {
  component: Record<string, any>;
  route: ActivatedRouteSnapshot;

  constructor(
    private confirmationService: ConfirmationService,
  ) {}

  canDeactivate(
    component: IDeactivateComponent,
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot,
    nextState: RouterStateSnapshot,
  ): Observable<boolean> | Promise<boolean> | boolean {
    // if (!component) { return; }

    const hasPendingChanges = component.hasPendingChanges() as boolean;
    return new Promise<boolean>((resolve, reject) => {
      if (hasPendingChanges) {
        this.confirmationService.openConfirmation({
          message: marker('base.messages.pending-changes-confirmation'),
          onConfirm: () => resolve(true),
          onCancel: () => resolve(false)
        });
      } else {
        resolve(true);
      }
    });
  }
}
