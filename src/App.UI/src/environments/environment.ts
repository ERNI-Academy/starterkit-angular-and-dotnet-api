import {environment as environmentOverride} from './environment.override';

/**
 * This file allows to override environment variables by "file replacement" or using "environment variables" at runtime.
 * The importance of those variables from less to more importance are:
 *   1. baseEnvironment variable
 *   2. File replacement through 'environment.override.ts' file
 *   3. Environment variables in 'env.js' file
 */

/// Default values. Used normally for development.
const baseEnvironment = {
  production: false,

  apiUrl: '/api',
  debug: false,
  mockUI: false,
};

/**
 * We override base variables with the content of 'environment.override.ts'
 */
let environmentVars = {
  ...baseEnvironment,
  ...environmentOverride,
};

/**
 * If '_window._env' (env.js file) is defined it will take precedence over the rest of values.
 * That file uses variables defined in the "runtime environment", like docker-compose variables.
 * These variables come at runtime by loading the file 'env.js' from 'index.html' file
 */

// tslint:disable-next-line: variable-name
const _window = window as any;
if (_window._env) {
  environmentVars = {
    ...environmentVars,
    ..._window._env,
  };
}

export const environment = environmentVars;


/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
