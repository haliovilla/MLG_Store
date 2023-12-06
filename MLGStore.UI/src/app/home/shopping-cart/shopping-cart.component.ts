import { Component } from '@angular/core';
import { ShoppingCartItemDTO } from '../../models/article';
import { ShoppingCartService } from '../../services/shopping-cart.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.scss']
})
export class ShoppingCartComponent {

  cartItems: Array<ShoppingCartItemDTO> = [];

  constructor(private shoppingCartService: ShoppingCartService) {
    shoppingCartService.CartItems$
      .subscribe(items => {
this.cartItems = items;
      });
  }

  calculateTotal(): number {
    if (this.cartItems && this.cartItems.length > 0) {
return this.cartItems
        .map(item => item.price)
        .reduce((a, b) => a + b);
    } else {
      return 0;
    }
  }

}
