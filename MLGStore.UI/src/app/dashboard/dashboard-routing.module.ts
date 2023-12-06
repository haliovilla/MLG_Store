import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './dashboard.component';
import { ArticlesComponent } from './articles/articles.component';
import { StoresComponent } from './stores/stores.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    children: [
      { path: '', redirectTo: 'stores', pathMatch: 'full' },
      { path: 'stores', component: StoresComponent },
      { path: 'articles', component: ArticlesComponent },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
