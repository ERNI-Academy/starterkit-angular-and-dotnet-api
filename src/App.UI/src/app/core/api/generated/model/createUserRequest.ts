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
import { Role } from './role';


/**
 * The create user request.
 */
export interface CreateUserRequest { 
    /**
     * Gets or sets the username.
     */
    username: string;
    /**
     * Gets or sets the first name.
     */
    firstName: string;
    /**
     * Gets or sets the last name.
     */
    lastName: string;
    role: Role;
    /**
     * Gets or sets the password.
     */
    password: string;
}

