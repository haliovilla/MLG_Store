import { Injectable } from '@angular/core';
import { Observable, ReplaySubject, map } from 'rxjs';
import { CreateStoreDTO, StoreDTO } from '../models/Store';
import { HttpClient } from '@angular/common/http';
import { ErrorService } from './error.service';
import { environment } from '../../environment';
import { Result } from '../models/result';

@Injectable({
  providedIn: 'root'
})
export class StoreService {

  apiUrl: string = `${environment.apiUrlBase}/Store`;;

  private _Stores: ReplaySubject<Array<StoreDTO>>;
  private list: Array<StoreDTO> = [];

  constructor(private http: HttpClient,
    private errorService: ErrorService) {
    this._Stores = new ReplaySubject<Array<StoreDTO>>(1);
    this.getAll();
  }

  set Stores(value: Array<StoreDTO>) {
    this.list = value;
    this._Stores.next(this.list);
    console.log(`Loaded ${value.length} store items`);
  }

  get Stores$(): Observable<Array<StoreDTO>> {
    return this._Stores.asObservable();
  }

  private httpCall(): Observable<Result<Array<StoreDTO>>> {
    return this.http
      .get<Result<Array<StoreDTO>>>(this.apiUrl);
  }

  private getAll() {
    this.httpCall().subscribe({
      next: (response) => {
        this.Stores = response.data;
      }, error: (error) => {
        this.Stores = [];
        this.errorService.logError(error);
      }
    })
  }

  insert(store: CreateStoreDTO): Observable<StoreDTO> {
    return this.http.post<Result<StoreDTO>>(this.apiUrl, store)
      .pipe(map((response: Result<StoreDTO>) => {
        if (response.success) {
          this.add(response.data);
          return response.data;
        }
        this.errorService.logError(response);
        return null as any;
      }));
  }

  update(id: number, store: CreateStoreDTO): Observable<StoreDTO> {
    const uri = `${this.apiUrl}/${id}`;
    return this.http.put<Result<StoreDTO>>(uri, store)
      .pipe(map((response: Result<StoreDTO>) => {
        if (response.success) {
          this.edit(response.data);
          return response.data;
        }
        this.errorService.logError(response);
        return null as any;
      }));
  }

  delete(id: number): Observable<boolean> {
    const uri = `${this.apiUrl}/${id}`;
    return this.http.delete<Result<boolean>>(uri)
      .pipe(map((response: Result<boolean>) => {
        if (response.success) {
          this.remove(id);
          return true;
        }
        this.errorService.logError(response);
        return false;
      }));
  }

  private add(item: StoreDTO) {
    this.list.unshift(item);
    this._Stores.next(this.list);
  }

  private edit(item: StoreDTO) {
    const idx = this.list.findIndex(x => x.id === item.id);
    this.list[idx] = item;
    this._Stores.next(this.list);
  }

  private remove(id: number) {
    const idx = this.list.findIndex(x => x.id === id);
    this.list.splice(idx, 1);
    this._Stores.next(this.list);
  }



}
