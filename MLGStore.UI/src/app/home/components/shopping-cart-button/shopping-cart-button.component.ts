import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { ShoppingCartService } from '../../../services/shopping-cart.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-shopping-cart-button',
  templateUrl: './shopping-cart-button.component.html',
  styleUrls: ['./shopping-cart-button.component.scss']
})
export class ShoppingCartButtonComponent {

  cartItems: number = 0;
  isLoggedin!: boolean;

  constructor(private authService: AuthService,
    private shoppingCartService: ShoppingCartService,
  private router:Router) {
    authService.getToken()?.subscribe(token => {
      this.isLoggedin = token != undefined && token !== '';
    });

    shoppingCartService.CartItems$
      .subscribe(items => {
        this.cartItems = items.length;
      });
  }

  goToShoppingCart() {
    if (this.isLoggedin) {
      this.router.navigate(['home/shopping-cart'])
    } else {
      this.router.navigate(['home/login'])
    }
  }

}
