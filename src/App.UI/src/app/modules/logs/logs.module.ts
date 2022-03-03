import { environment } from './../../../environments/environment';
import { NgModule, Provider } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LogsRoutingModule } from './logs-routing.module';
import { LogsPagesModule } from './pages/logs-pages.module';
import { LogsService } from './services/logs.service';
import { LogsServiceMock } from './services/logs.service.mock';
import { inject } from '@angular/core';

// export function LogServiceFactory() {
//   return environment.mockUI ? inject(LogsServiceMock) : inject(LogsService);
// }

export function LogServiceProvider(): Provider {
  return {provide: LogsService, useClass: environment.mockUI ? LogsServiceMock : LogsService };
}


@NgModule({
    imports: [
        CommonModule,
        LogsRoutingModule,
        LogsPagesModule,
    ],
    providers: [
    // If we would put a provider here the LogService would be duplicated as it's also used in Layout component for the notifications
    // {provide: LogsService, useClass: environment.mockUI ? LogsServiceMock : LogsService }
    ]
})
export class LogsModule {}
