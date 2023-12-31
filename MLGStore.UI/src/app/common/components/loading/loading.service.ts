import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {

  private _loading = false;
  loadingStatus = new Subject<boolean>();

  get loading(): boolean {
    return this._loading;
  }

  set loading(value: boolean) {
    this._loading = value;
    this.loadingStatus.next(value);
  }

  show() {
    this.loading = true;
  }

  hide() {
    this.loading = false;
  }


}
