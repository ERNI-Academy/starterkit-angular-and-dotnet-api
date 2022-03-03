import { environment } from './../../../../environments/environment';
import { ModuleWithProviders } from '@angular/core';
import { ApiModule } from '../generated/api.module';
import { Configuration } from '../generated/configuration';


export function ApiConfigurationService(): Configuration {
  return new Configuration({
    basePath: environment.apiUrl,
    // Pass Authentication cookie: needed by the API to pass the Authentication cookie in every call.
    // https://stackoverflow.com/a/27407440/4812475
    // withCredentials: true,
  });
}

/** We use our own factory method 'ApiConfigurationService' which uses DI to provide
 * the 'MainModuleConfiguration' parameter. Otherwise the generated ApiModule.forRoot() method does not allow DI in the
 * factory and we cannot see other way to provide the 'MainModuleConfiguration' parameter.
 */
export function ApiModuleForRoot(): ModuleWithProviders<ApiModule> {
  return {
    ngModule: ApiModule,
    providers: [
      // { provide: Configuration, useFactory: ApiConfigurationService, deps: [MainModuleConfiguration] },
      { provide: Configuration, useFactory: ApiConfigurationService },

      // Extended API Rest services
      // { provide: AuthenticationRestControllerService, useClass: AuthenticationRestControllerExtendedService },
    ],
  };
}