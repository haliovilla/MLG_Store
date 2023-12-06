import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import { ArticlesComponent } from './articles/articles.component';
import { MenuComponent } from './menu/menu.component';
import { SharedModulesModule } from '../shared-modules.module';
import { StoresComponent } from './stores/stores.component';


@NgModule({
  declarations: [
    DashboardComponent,
    ArticlesComponent,
    MenuComponent,
    StoresComponent
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    SharedModulesModule
  ]
})
export class DashboardModule { }
