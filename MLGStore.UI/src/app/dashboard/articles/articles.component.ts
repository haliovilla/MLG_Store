import { Component } from '@angular/core';
import { ArticleDTO } from '../../models/article';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ArticleService } from '../../services/article.service';
import { StoreDTO } from '../../models/Store';
import { StoreService } from '../../services/store.service';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.scss']
})
export class ArticlesComponent {

  articles: Array<ArticleDTO> = [];
  stores: Array<StoreDTO> = [];

  articleForm!: FormGroup;
  showForm: boolean = false;
  isEdition: boolean = false;

  article!: ArticleDTO;

  constructor(private articleService: ArticleService,
    private storeService:StoreService,
    private fb: FormBuilder) {
    articleService.Articles$
      .subscribe(res => this.articles = res);
    storeService.Stores$
      .subscribe(res => this.stores = res);
  }

  initForm() {
    this.articleForm = this.fb.group({
      storeId: ['', [Validators.required]],
      code: ['', [Validators.required]],
      description: ['', [Validators.required]],
      price: ['', [Validators.required]],
      stock: ['', [Validators.required]],
      image: ['', [Validators.required]],
    });
  }

  setFormValue(article: ArticleDTO) {
    this.articleForm = this.fb.group({
      storeId: [article.store.id, [Validators.required]],
      code: [article.code, [Validators.required]],
      description: [article.description, [Validators.required]],
      price: [article.price, [Validators.required]],
      stock: [article.stock, [Validators.required]],
      image: [article.image, [Validators.required]],
    });
  }

  setFormVisibility = () => this.showForm = !this.showForm;

  addNew() {
    this.isEdition = false;
    this.setFormVisibility();
    this.setFormValue(this.article);
  }

  isValidForm = (): boolean => this.articleForm.valid;

  save() {
    if (this.isValidForm()) {
      this.articleService.insert(this.articleForm.value)
        .subscribe(response => {
          if (Response) {
            this.setFormVisibility();
          }
        });
    }
  }

  edit(article: ArticleDTO) {
    this.article = article;
    this.isEdition = true;
    this.setFormVisibility();
    this.initForm();
    this.articleForm.patchValue(article);
  }

  update() {
    if (this.isValidForm()) {
      this.articleService.update(this.article.id, this.articleForm.value)
        .subscribe(response => {
          if (Response) {
            this.setFormVisibility();
          }
        });
    }
  }

  delete(article: ArticleDTO) {
    if (confirm(`¿Desea eliminar el artículo ${article.description}?`)) {
      this.articleService.delete(article.id)
        .subscribe(response => {
          if (Response) {
            alert("Registro eliminado");
          }
        });
    }
  }

}
