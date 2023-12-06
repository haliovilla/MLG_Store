import { Injectable } from '@angular/core';
import { Observable, ReplaySubject, map } from 'rxjs';
import { ArticleDTO, ShoppingCartItemDTO } from '../models/article';
import { HttpClient } from '@angular/common/http';
import { ErrorService } from './error.service';
import { Result } from '../models/result';
import { environment } from '../../environment';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartService {

  apiUrl: string = `${environment.apiUrlBase}/Article`;;

  private _cartItems: ReplaySubject<Array<ShoppingCartItemDTO>>;
  private list: Array<ShoppingCartItemDTO> = [];

  constructor(private http: HttpClient,
    private errorService: ErrorService) {
    this._cartItems = new ReplaySubject<Array<ShoppingCartItemDTO>>(1);
    this.getAll();
  }

  set CartItems(value: Array<ShoppingCartItemDTO>) {
    this.list = value;
    this._cartItems.next(this.list);
    console.log(`Loaded ${value.length} cart items`);
  }

  get CartItems$(): Observable<Array<ShoppingCartItemDTO>> {
    return this._cartItems.asObservable();
  }

  addToCart(article: ArticleDTO): Observable<boolean> {
    const uri = `${this.apiUrl}/AddShoppingCartItem`;
    return this.http.post<Result<ShoppingCartItemDTO>>(uri, article.id)
      .pipe(map((response: Result<ShoppingCartItemDTO>) => {
      if (response.success) {
        this.add(response.data);
        return true;
      }
      this.errorService.logError(response);
      return false;
    }));
  }

  private httpCall(): Observable<Result<Array<ShoppingCartItemDTO>>> {
    const uri = `${this.apiUrl}/GetShoppingCartItems`;
    return this.http
      .get<Result<Array<ShoppingCartItemDTO>>>(uri);
  }

  private getAll() {
    this.httpCall().subscribe({
      next: (response) => {
        this.CartItems = response.data;
      }, error: (error) => {
        this.CartItems = [];
        this.errorService.logError(error);
      }
    })
  }

  private add(item: ShoppingCartItemDTO) {
    this.list.push(item);
    this._cartItems.next(this.list);
  }

  private remove(id: number) {
    const idx = this.list.findIndex(x => x.id === id);
    this.list.splice(idx, 1);
    this._cartItems.next(this.list);
  }
}
