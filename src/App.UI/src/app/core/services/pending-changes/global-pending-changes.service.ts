import { Injectable } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

export interface PendingChange {
  id: string;
}

@Injectable({
  providedIn: 'root'
})
export class GlobalPendingChangesService<TElement extends PendingChange = PendingChange> {
  private _pendingChanges: TElement[] = [];
  get pendingChanges() { return this._pendingChanges; }

  constructor(
    router: Router
  ) {
    router.events.subscribe((val) => {
      /** This is a way to be aware of the user clicking confirm on the 'DeactivateGuard' */
      if (val instanceof NavigationEnd) {
        this.clearAllPendingChanges();
      }
  });
  }

  notifyPendingChange(pendingChange: TElement) {
    const existingPendingChange = this._pendingChanges.find(x => x.id === pendingChange.id);
    if (!existingPendingChange) {
      this._pendingChanges.push(pendingChange);
    }
  }

  clearPendingChange(pendingChange: TElement) {
    // remove item
    this._pendingChanges = this._pendingChanges.filter(x => x.id !== pendingChange.id);
  }

  hasPendingChanges(pendingChange?: TElement): boolean {
    const pendingChanges = pendingChange ?
      this._pendingChanges.filter(change => change.id === pendingChange.id) :
      this._pendingChanges;

    return pendingChanges.length > 0;
  }

  clearAllPendingChanges(): void {
    this._pendingChanges = [];
  }
}
