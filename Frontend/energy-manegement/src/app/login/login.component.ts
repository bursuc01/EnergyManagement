import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserService } from '../services/user-service/user.service';
import { Login } from '../interfaces/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  invalidLogin: boolean = false;
  loginTry: boolean = false;
  credentials: Login = {name:'', password:''};

  constructor (
    private userService: UserService
  ) {}

  async onSubmit(form: NgForm) {
    this.loginTry = true;
    if (form.valid) {
      this.userService.login(this.credentials).subscribe( 
        (response: any) => {
          const token = response.token;
          this.userService.saveToken(token);
          this.userService.isLoggedIn = !this.invalidLogin;
        },
        error => {
          if (error.status === 401) {
            this.invalidLogin = true;
          }
        });
    }

  }
  
  
}
