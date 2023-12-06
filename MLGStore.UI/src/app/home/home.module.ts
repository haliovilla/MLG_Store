import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { LoginComponent } from './login/login.component';
import { MenuComponent } from './menu/menu.component';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';
import { SharedModulesModule } from '../shared-modules.module';
import { LoadingComponent } from '../common/components/loading/loading.component';
import { ArticlesComponent } from './articles/articles.component';
import { ShoppingCartButtonComponent } from './components/shopping-cart-button/shopping-cart-button.component';


@NgModule({
  declarations: [
    HomeComponent,
    LoginComponent,
    MenuComponent,
    ShoppingCartComponent,
    ArticlesComponent,
    ShoppingCartButtonComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    SharedModulesModule,
    LoadingComponent
  ]
})
export class HomeModule { }
