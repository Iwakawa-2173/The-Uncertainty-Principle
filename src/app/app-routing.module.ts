import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './auth/auth.component';
import { EventComponent } from './event/event.component';
import { EndingAComponent } from './endings/ending-a/ending-a.component';
import { EndingBComponent } from './endings/ending-b/ending-b.component';

const routes: Routes = [
  { path: '', component: AuthComponent }, // Страница аутентификации
  { path: 'event', component: EventComponent }, // Страница событий
  { path: 'ending-a', component: EndingAComponent }, // Концовка A
  { path: 'ending-b', component: EndingBComponent }, // Концовка B
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
