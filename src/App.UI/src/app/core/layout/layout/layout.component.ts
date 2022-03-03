import { UserService } from '@app/core/services/user/user.service';
import { Component, OnInit } from '@angular/core';
import { APPRoutes } from '@app/core/constants/constants';
import { LogsService } from '@app/modules/logs/services/logs.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {
  routes = APPRoutes;

  get userName() {
    return this.userService.currentUser?.name;
  }

  constructor(
    private userService: UserService,

    /** Logs service needs to be here to be initialized when Layout gets initialized.
     * Logs service at the same time initializes GeneralEventsService which calls SocketService, which will subscribe to the notifications. Without it
     * notifications by the Socket will not work.
     * LayoutComponent -> LogsService -> GeneralEventsService -> SocketService
     * This chain makes the notifications appear as a Snackbar in the top of the screen.
     * 
     * TODO: this should be better in a "Angular Initialize Hook" instead of here
     */
    private logsService: LogsService,
  ) { }

  ngOnInit(): void {
  }

}
