import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {PersonContainerComponent} from './ui/person/pages/person-container/person-container.component';

const routes: Routes = [
  {path: '', redirectTo: 'person', pathMatch: 'full'},
  {path: 'person', component: PersonContainerComponent},
  {path: '**', component: PersonContainerComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
