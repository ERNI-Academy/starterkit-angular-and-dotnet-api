import { AuthenticateResponse } from './../../api/generated/model/authenticateResponse';
import { Role as APIRole } from './../../api/generated/model/role';
import { tap, map } from 'rxjs/operators';
import { AuthService } from '@app/core/api/generated/api/auth.service';
import { UsersService as APIUserService } from '@app/core/api/generated/api/users.service';
import { BehaviorSubject, ReplaySubject, Subject, Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { Role, User } from './user.model';
import { APPRoutes, OPERATOR_PASSWORD, OPERATOR_USER_NAME } from '@app/core/constants/constants';
import { Configuration as APIConfiguration } from '@app/core/api/generated/configuration';
import { Router } from '@angular/router';
import { SnackbarService, SnackBarType } from '@app/shared/services/snackbar.service';

export interface PermissionOptions {
  minimumRole: Role;
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private _currentUser: User;
  public get currentUser() {
    return this._currentUser;
  }
  private _currentUser$ = new ReplaySubject<User>(1);
  public get currentUser$() {
    return this._currentUser$.asObservable();
  }

  constructor(
    private authService: AuthService,
    private apiUserService: APIUserService,
    private apiConfiguration: APIConfiguration,
    private router: Router,
    private snackbarService: SnackbarService,
    ) {
    this.subscribeToUserChanges();
    this.checkUserIsLoggedInOrAutoLogin();
    this.keepAlive();
  }

  /** With this method we want to recalculate permissions or throw the user to a different route in case it does not have permissions.
   * The easiest solution is to move the user to a "safe route" like the 'overview' which is visible for everyone.
   */
  private resetRoute() {
    // this.router.navigateByUrl('/', {skipLocationChange: true}).then(() =>
    //   this.router.navigate([location.href])
    // );

    this.router.navigate([APPRoutes.ROBOT.BASE]);

    // this.router.navigateByUrl(location.href, {skipLocationChange: true});
  }

  private subscribeToUserChanges() {
    this.currentUser$.subscribe(user => {
      /** recalculate permissions
       * we don't want to do it the first time (when there is no user) which would prevent an F5 to go to the same route
       */
      if (this._currentUser) {
        this.resetRoute();
      }

      // user changed
      this._currentUser = user;

      this.refreshToken(user.token);
    });
  }

  private keepAlive() {
    const everyMinutes = 10;
    setInterval(async () => {
      console.log('keepAlive!!!');
      this.authService.authRefreshTokenPost().toPromise().then(user => {
        this.refreshToken(user.jwtToken);
        /** We don't 'notify' a change of user by the 'observable' as that would have many consecuences... redirections, etc
         * but we update the current token in the user (although it should not be used from the user)
         */
        this._currentUser.token = user.jwtToken;
      });
    }, everyMinutes * 60 * 1000);
  }

  private refreshToken(token: string) {
    // update apiKey (with the token) for next API calls
    this.apiConfiguration.apiKeys = this.apiConfiguration.apiKeys || {};
    this.apiConfiguration.apiKeys.Authorization = token;
  }

  private checkUserIsLoggedInOrAutoLogin() {
    this.authService.authRefreshTokenPost().toPromise().then(user => {
      this._currentUser$.next(this.mapToUser(user));
    },
    async error => {
      // auto-login with operator
      console.log('user not logged in');
      const user = await this.loginAsOperator();
      console.log('auto log in', user);
    });
  }

  private async loginAsOperator() {
    try {
      return await this.login(OPERATOR_USER_NAME, OPERATOR_PASSWORD);
    } catch (error) {
      console.error('Problem with authentication service');
      this.snackbarService.openSnackBar(
        'Problem with authentication service',
        SnackBarType.error,
      );
    }
  }

  private convertRole(apiRole: APIRole): Role {
    switch (apiRole) {
      case APIRole.Operator:
        return Role.Operator;
      case APIRole.Admin:
        return Role.Admin;
      case APIRole.Maintenance:
        return Role.Maintenance;
      case APIRole.Commissioning:
        return Role.Commission;
      default:
        return Role.Operator;
    }
  }

  private mapToUser(userResponse: AuthenticateResponse): User {
    return {
      id: userResponse.id.toString(),
      userName: userResponse.username,
      name: userResponse.firstName,
      role: this.convertRole(userResponse.role),
      token: userResponse.jwtToken,
    };
  }

  login(userName: string, password: string): Promise<User> {

    return this.authService.authAuthenticatePost({
      authenticateRequest: {
        username: userName,
        password,
      }
    }).pipe(
      map(user => this.mapToUser(user)),
      tap(user => {
        this._currentUser$.next(user);
      }),
    ).toPromise();

  }

  async logout(): Promise<void> {
    return this.authService.authRevokeTokenPost({
      // when we send NO token the current cookie token is revoked and no more "refresh-tokens" can be executed
      revokeTokenRequest: {}
    }).pipe(
      tap(async () => {
        await this.loginAsOperator();
      }),
    ).toPromise();
  }

  async changePassword(currentPassword: string, newPassword: string): Promise<void> {
    // Implementation pending
    return this.apiUserService.usersResetPasswordPost({
      resetPasswordRequest: {
        oldPassword: currentPassword,
        newPassword,
      }
    }).toPromise();
  }

  hasPermission(options: PermissionOptions) {
    return this.currentUser.role >= options.minimumRole;
  }

  hasPermissionObservable(options: PermissionOptions) {
    return this.currentUser$.pipe(
      map(user => user.role >= options.minimumRole)
    );
  }
}
