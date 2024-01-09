import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../services/user-service/user.service';
import { UserCreate } from '../interfaces/userCreate';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.scss']
})
export class RegisterUserComponent {
  credentials: UserCreate = {name:'', password:'', isAdmin: false};

  constructor(
    private route: Router,
    private userService: UserService
  ) {}

  postUser(): void {
    this.userService.postUser(this.credentials).subscribe(
      () => {
        this.back();
      }
    );
  }

  back(): void {
    this.route.navigate(["/admin"]);
  }
}
