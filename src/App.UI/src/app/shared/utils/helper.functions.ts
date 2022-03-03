import { NgBlockUI } from 'ng-block-ui';
import { ABBDropdownSelectedElement, KeyValue } from './types';

export function settimeoutPromise<T = void>(action: () => T, miliseconds: number) {
  return new Promise<T>((resolve, reject) => {
    setTimeout(() => {
      try {
        resolve(action());
      } catch (error) {
        reject(error);
      }
    }, miliseconds);
  });
}

export async function blockUI(blockUIInstance: NgBlockUI, action: () => Promise<any>) {
  try {
    blockUIInstance.start();
    await action();
  } catch (error) {
    throw error;
  } finally {
    blockUIInstance.stop();
  }
}

export function generateDecimalNumbers(decimals: number) {
  const precision = Math.pow(10, decimals); // 100 = 2 decimals
  const randomnum = Math.floor(Math.random() * (10 * precision - precision) + precision) / precision;
  return randomnum;
}


export const toDataURL = url => fetch(url)
  .then(response => response.blob())
  .then(blob => new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.onloadend = () => resolve(reader.result);
    reader.onerror = reject;
    reader.readAsDataURL(blob);
  }));


/** Example */
// toDataURL('https://www.gravatar.com/avatar/d50c83cc0c6523b4d3f6085295c953e0')
//   .then(dataUrl => {
//     console.log('RESULT:', dataUrl)
//   })


export function getDropdownSelectedValue(value: string, options: KeyValue[]): ABBDropdownSelectedElement<string> {
  if (value) {
    const label = options?.find(x => x.id === value).name;
    return [
      {label, value}
    ];
  }
}

// tslint:disable
export function uuidv4() {
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
    var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
    return v.toString(16);
  });
}