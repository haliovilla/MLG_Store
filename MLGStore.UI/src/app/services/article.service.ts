import { Injectable } from '@angular/core';
import { environment } from '../../environment';
import { HttpClient } from '@angular/common/http';
import { Observable, ReplaySubject, map } from 'rxjs';
import { ArticleDTO, CreateArticleDTO, CreateArticleWithImageUrlDTO } from '../models/article';
import { Result } from '../models/result';
import { ErrorService } from './error.service';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {

  apiUrl: string = `${environment.apiUrlBase}/Article`;;

  private _articles: ReplaySubject<Array<ArticleDTO>>;
  private list: Array<ArticleDTO> = [];

  constructor(private http: HttpClient,
    private errorService: ErrorService) {
    this._articles = new ReplaySubject<Array<ArticleDTO>>(1);
    this.getAll();
  }

  set Articles(value: Array<ArticleDTO>) {
    this.list = value;
    this._articles.next(this.list);
    console.log(`Loaded ${value.length} article items`);
  }

  get Articles$(): Observable<Array<ArticleDTO>> {
    return this._articles.asObservable();
  }

  private httpCall(): Observable<Result<Array<ArticleDTO>>> {
    return this.http
      .get<Result<Array<ArticleDTO>>>(this.apiUrl);
  }

  private getAll() {
    this.httpCall().subscribe({
      next: (response) => {
        this.Articles = response.data;
      }, error: (error) => {
        this.Articles = [];
        this.errorService.logError(error);
      }
    })
  }

  insert(article: CreateArticleWithImageUrlDTO): Observable<ArticleDTO> {
    const uri = `${this.apiUrl}/CreateArticleWithImageUrl`;
    return this.http.post<Result<ArticleDTO>>(uri, article)
      .pipe(map((response: Result<ArticleDTO>) => {
        if (response.success) {
          this.add(response.data);
          return response.data;
        }
        this.errorService.logError(response);
        return null as any;
      }));
  }

  update(id: number, article: CreateArticleWithImageUrlDTO): Observable<ArticleDTO> {
    const uri = `${this.apiUrl}/UpdateArticleWithImageUrl/${id}`;
    return this.http.put<Result<ArticleDTO>>(uri, article)
      .pipe(map((response: Result<ArticleDTO>) => {
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

  private add(item: ArticleDTO) {
    this.list.unshift(item);
    this._articles.next(this.list);
  }

  private edit(item: ArticleDTO) {
    const idx = this.list.findIndex(x => x.id === item.id);
    this.list[idx] = item;
    this._articles.next(this.list);
  }

  private remove(id: number) {
    const idx = this.list.findIndex(x => x.id === id);
    this.list.splice(idx, 1);
    this._articles.next(this.list);
  }



}
