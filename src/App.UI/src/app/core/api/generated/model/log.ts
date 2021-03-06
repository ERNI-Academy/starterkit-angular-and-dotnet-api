/**
 * HMI.API
 * This document describe the endpoints to use in the UI to connect with the entire CVS system.
 *
 * The version of the OpenAPI document: v1
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */
import { LogType } from './logType';


/**
 * The log.
 */
export interface Log { 
    /**
     * Gets or sets the id.
     */
    id?: number;
    /**
     * Gets or sets the time.
     */
    date?: string;
    type?: LogType;
    /**
     * Gets or sets a value indicating whether is acknowledged.
     */
    isAcknowledged?: boolean;
    /**
     * Gets or sets the code.
     */
    code?: string | null;
    /**
     * Gets or sets the module.
     */
    module?: string | null;
    /**
     * Gets or sets the title.
     */
    title?: string | null;
    /**
     * Gets or sets the description.
     */
    description?: string | null;
    /**
     * Gets or sets the actions.
     */
    actions?: string | null;
}

