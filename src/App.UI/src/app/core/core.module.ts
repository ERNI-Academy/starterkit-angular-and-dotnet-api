import { SocketService } from './api/sockets/socket.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiModuleForRoot } from './api/extended/api.module';
import { LayoutModule } from './layout/layout.module';
import { HttpClient } from '@angular/common/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { UserService } from './services/user/user.service';
import { MockSocketService } from './api/sockets/mock-socket.service';
import { Public } from '@app/shared/utils/types';
import { BlockUIModule } from 'ng-block-ui';
import { environment } from 'src/environments/environment';
import { MockUserService } from './services/user/user.service.mock';
import { LogServiceProvider } from '@app/modules/logs/logs.module';

// AoT requires an exported function for factories
export function HttpLoaderFactory(http: HttpClient): TranslateHttpLoader {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

export function SocketFactory(): Public<SocketService> {
  // use variables to decide if using mock or not
  return new MockSocketService();
}

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    LayoutModule,
    ApiModuleForRoot(),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      },
      defaultLanguage: 'en',
    }),
    BlockUIModule.forRoot(),
  ],
  providers: [
    {provide: SocketService, useClass: environment.mockUI ? MockSocketService : SocketService},
    {provide: UserService, useClass: environment.mockUI ? MockUserService : UserService },
    LogServiceProvider(),
  ]
})
export class CoreModule { }
