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
import { FeatureMatchingState } from './featureMatchingState';


/**
 * The process status response.
 */
export interface ProcessStatusResponse { 
    /**
     * Gets or sets the number of cycles.
     */
    nbrCycles?: number;
    /**
     * Gets or sets the current Step.
     */
    step?: number;
    /**
     * Gets or sets the translation error in mm.
     */
    tError?: number;
    /**
     * Gets or sets the rotation error in degrees.
     */
    rError?: number;
    matching?: FeatureMatchingState;
}

