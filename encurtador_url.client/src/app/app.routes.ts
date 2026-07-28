import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Pages/home/home.component';
import { RedirectComponent } from './Pages/redirect/redirect.component';

export const routes: Routes = [
  {path: '', component: HomeComponent, title: 'Encurtador de Links', pathMatch: 'full'},
  {path: 'red/:code', title: 'Redirecionando', component: RedirectComponent},
  {path: '**', redirectTo: '', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
