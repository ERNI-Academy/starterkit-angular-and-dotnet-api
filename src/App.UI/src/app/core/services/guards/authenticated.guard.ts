import { tap, filter, map } from 'rxjs/operators';
import { Role } from '../user/user.model';
import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  CanActivateChild,
} from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '@app/core/services/user/user.service';

export interface MinimumRole {
  minRole: Role;
}

@Injectable({
  providedIn: 'root',
})
export class AuthenticatedGuard implements CanActivate, CanActivateChild {
  constructor(
    private userService: UserService,
  ) {}

  private firstTimeEntrance = true;

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot,
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    // wait for the user to be authenticated (and have a token)
    // otherwise future queries to API will fail
    return this.userService.currentUser$.pipe(
      tap(x => this.firstTimeEntrance && console.log('wait for user to be loggedid')),
      filter(user => !!user),
      tap(x => {
        if (this.firstTimeEntrance) {
          console.log('user its been loggen in');
          this.firstTimeEntrance = false;
        }
      }),
      map(x => true),
    );
  }

  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot,
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this.canActivate(childRoute, state);
  }
}
