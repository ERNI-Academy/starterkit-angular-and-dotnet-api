import { PermissionOptions, UserService } from './user.service';
import { BehaviorSubject, from, Observable, ReplaySubject, Subject } from 'rxjs';
import { Injectable } from '@angular/core';
import { Role, User } from './user.model';
import { Public } from '@app/shared/utils/types';

const MockUsers: User[] = [
  {id: '1', name: 'oper', userName: 'Operator', role: Role.Operator, token: ''},
  {id: '2', name: 'admin', userName: 'Administrator', role: Role.Admin, token: ''},
  {id: '3', name: 'com', userName: 'Comission', role: Role.Commission, token: ''},
  {id: '4', name: 'main', userName: 'Maintenance', role: Role.Maintenance, token: ''},
];

@Injectable({
  providedIn: 'root'
})
export class MockUserService implements Public<UserService> {
  private _currentUser = new ReplaySubject<User>(1);
  public get currentUser$() {
    return this._currentUser.asObservable();
  }

  constructor() {
    // simulate user logged in
    this._currentUser.next(MockUsers.find(user => user.role === Role.Operator));

    this._currentUser.subscribe(user => this._currentUserValue = user);
  }
  private _currentUserValue: User;
  public get currentUser(): User {
    return this._currentUserValue;
  }
  hasPermission(options: PermissionOptions): boolean {
    return true;
  }
  hasPermissionObservable(options: PermissionOptions): Observable<boolean> {
    return from([true]);
  }

  async login(userName: string, password: string) {
    /** Simulate login */
    const user = MockUsers.find(x => x.name === userName);
    if (!user) {
      throw new Error(`Invalid user or password`);
    }
    this._currentUser.next(user);
    return user;
  }

  async logout() {
    const user = MockUsers.find(x => x.role === Role.Operator);
    this._currentUser.next(user);
  }

  async changePassword(currentPassword: string, newPassword: string) {
    
  }
}
