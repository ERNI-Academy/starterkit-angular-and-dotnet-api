import { UserLoginComponent, UserLoginComponentDialogDataInfo } from './../user-login/user-login.component';
import { UserService } from './../../services/user/user.service';
import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { Role, User } from '../../services/user/user.model';
import { marker } from '@biesbjerg/ngx-translate-extract-marker';
import { createDialogComponent } from '@app/shared/component/dialog/dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { filter, map, take, tap } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { GlobalPendingChangesService } from '@app/core/services/pending-changes/global-pending-changes.service';
import { ConfirmationService } from '@app/shared/component/dialog/confirmation/confirmation.service';


const isOperator = (user: User) => user.role === Role.Operator;
const isNotOperator = (user: User) => !isOperator(user);

@Component({
  selector: 'app-user-selector',
  templateUrl: './user-selector.component.html',
  styleUrls: ['./user-selector.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class UserSelectorComponent implements OnInit {
  userName$ = this.userService.currentUser$.pipe(map((x) => x.name));

  constructor(
    private userService: UserService,
    private matDialog: MatDialog,
    private globalPendingChanges: GlobalPendingChangesService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit(): void {}

  async clickMainItem() {
    this.userService.currentUser$.pipe(take(1)).subscribe((user) => {
      this.checkPendingChanges(() => {
        this.openLoginDialog();
      });
    });
  }

  private openLoginDialog() {
    // open login screen (default action)
    createDialogComponent({
      matDialog: this.matDialog,
      dialogData: {
        innerComponentType: UserLoginComponent,
        title: marker('layout.login.title'),
        buttonPrimaryText: marker('base.action.ok'),
      },
    });
  }

  private checkPendingChanges(confirmAction: () => void) {
    if (this.globalPendingChanges.hasPendingChanges()) {
      this.confirmationService.openConfirmation({
        message: marker('base.messages.pending-changes-confirmation'),
        onConfirm: () => {
          confirmAction();
          this.globalPendingChanges.clearAllPendingChanges();
        },
        onCancel: () => {
          // nothing to do
        },
      });
    } else {
      confirmAction();
    }
  }

  async logout() {
    this.checkPendingChanges(() => {
      this.userService.logout();
    });
  }

  changePassword() {
    createDialogComponent({
      matDialog: this.matDialog,
      dialogData: {
        innerComponentType: UserLoginComponent,
        title: marker('layout.login.title'),
        info: {
          changePasswordMode: true,
        } as UserLoginComponentDialogDataInfo,
      },
    });
  }
}
