import { StoreDTO } from "./Store";

export interface ArticleDTO {
  id: number;
  code: string;
  description: string;
  price: number;
  image: string;
  stock: number;
  store: StoreDTO;
}

export interface CreateArticleDTO {
  code: string;
  description: string;
  price: number;
  stock: number;
  storeId: number;
}

export interface ShoppingCartItemDTO extends ArticleDTO {
  date: Date;
}

export interface CreateArticleWithImageUrlDTO extends CreateArticleDTO{
  image: string;
}

export interface CreateArticleWithImageFileDTO extends CreateArticleDTO{
  image: File;
}
