import { AbstractControl } from '@angular/forms';
import { Routes } from '@angular/router';
import { MinimumRole } from '@app/core/services/guards/authenticated.guard';

/** Strongly typed strings as propery names */
export function nameof<T>(key: keyof T, instance?: T): keyof T {
  return key;
}

/** Type which can be used to type a FromControlGroup */
export type AbstractControlOrT<T1, T2> = T1 extends AbstractControl ? T1 : T2;

/** Export only the public properties of a type */
export type Public<T> = { [P in keyof T]: T[P] };

export type ABBDropdownSelectedElement<T> = {
  value: T;
  label: string;
  isNew?: boolean;
}[];

export type KeyObject<TObjectKeys, TValue = any> = {[P in keyof TObjectKeys]: TValue};

/** Useful to have the data of the Route properly typed */
export type RoutesExtendedWithTypedData = Routes & {
  children?: {
    data?: MinimumRole;
  }[]
}[];

export interface KeyValue {
  id: string;
  name: string;
}
