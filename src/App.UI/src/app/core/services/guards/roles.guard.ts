import { Role } from './../user/user.model';
import { tap } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  Router,
  CanActivateChild,
} from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '@app/core/services/user/user.service';
import { APPRoutes } from '@app/core/constants/constants';

export interface MinimumRole {
  minRole: Role;
}

@Injectable({
  providedIn: 'root',
})
export class RolesGuard implements CanActivate, CanActivateChild {
  constructor(
    private userService: UserService,
    private router: Router,
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot,
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const minimumRole = (route.data as MinimumRole).minRole ?? Role.Operator;

    return this.userService.hasPermissionObservable({
      minimumRole,
    }).pipe(
      tap(hasAccess => {
        if (!hasAccess) {
          this.router.navigate([APPRoutes.ROBOT.BASE]);
        }
      })
    );
    // const hasAccess = this.userService.hasPermission({
    //   minimumRole,
    // });
    // if (!hasAccess) {
    //   this.router.navigate([APPRoutes.ROBOT.BASE]);
    // }
    // return hasAccess;
  }

  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot,
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.canActivate(childRoute, state);
  }
}
