import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AdminComponent } from './admin/admin.component';
import { RegisterUserComponent } from './register-user/register-user.component';
import { RegisterDeviceComponent } from './register-device/register-device.component';
import { LinkComponent } from './link/link.component';
import { AuthguardService } from './services/auth-service/authguard.service';
import { ChatComponent } from './chat/chat.component';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'admin', component: AdminComponent, canActivate: [AuthguardService]},
  { path: 'register-user', component: RegisterUserComponent, canActivate: [AuthguardService]},
  { path: 'register-device', component: RegisterDeviceComponent, canActivate: [AuthguardService]},
  { path: 'link', component: LinkComponent, canActivate: [AuthguardService]},
  { path: 'chat', component: ChatComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
