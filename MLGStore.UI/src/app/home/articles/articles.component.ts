import { Component } from '@angular/core';
import { ArticleDTO } from '../../models/article';
import { ArticleService } from '../../services/article.service';
import { AuthService } from '../../services/auth.service';
import { ShoppingCartService } from '../../services/shopping-cart.service';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.scss']
})
export class ArticlesComponent {

  articles: Array<ArticleDTO> = [];
  disabledAddButtons: boolean;

  constructor(private articleService: ArticleService,
    private authService: AuthService,
  private shoppingCartService:ShoppingCartService) {
    const token: string | undefined = authService.sessionTokenValue();
    this.disabledAddButtons = token == undefined;

    articleService.Articles$
      .subscribe(res => this.articles = res);
  }

  addToCart(article: ArticleDTO) {
    this.shoppingCartService.addToCart(article)
      .subscribe((response: boolean) => {
if (response) {
  alert(`Se agregó ${article.description} al carrito`);
        }
      });
  }

}
