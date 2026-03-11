import { Routes } from '@angular/router';
import { HomeComponent } from './presentation/pages/home/home.component';
import { It01Component } from './presentation/pages/it01/it01.component';
import { LoginComponent } from './presentation/pages/it02/login/login.component';
import { RegisterComponent } from './presentation/pages/it02/register/register.component';
import { WelcomeComponent } from './presentation/pages/it02/welcome/welcome.component';
import { authGuard } from './core/guards/auth.guard';
import { It03Component } from './presentation/pages/it03/it03.component';
import { It04Component } from './presentation/pages/it04/it04.component';
import { It05Component } from './presentation/pages/it05/it05.component';
import { TicketComponent } from './presentation/pages/it05/ticket/ticket.component';
import { ResetComponent } from './presentation/pages/it05/reset/reset.component';
import { It06Component } from './presentation/pages/it06/it06.component';
import { It07Component } from './presentation/pages/it07/it07.component';
import { It08Component } from './presentation/pages/it08/it08.component';
import { It09Component } from './presentation/pages/it09/it09.component';
import { It09AddComponent } from './presentation/pages/it09/add/it09-add.component';
import { It10Component } from './presentation/pages/it10/it10.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'it01', component: It01Component },
  { path: 'it03', component: It03Component },
  { path: 'it04', component: It04Component },
  { path: 'it06', component: It06Component },
  { path: 'it07', component: It07Component },
  { path: 'it08', component: It08Component },
  {
    path: 'it09',
    children: [
      { path: '', component: It09Component },
      { path: 'add', component: It09AddComponent },
    ],
  },
  { path: 'it10', component: It10Component },
  {
    path: 'it05',
    children: [
      { path: '', component: It05Component },
      { path: 'ticket', component: TicketComponent },
      { path: 'reset', component: ResetComponent },
    ],
  },
  {
    path: 'it02',
    children: [
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'welcome', component: WelcomeComponent, canActivate: [authGuard] },
    ],
  },
];
