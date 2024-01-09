import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from './services/user-service/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  constructor(
    private router: Router,
    private userService: UserService
  ) {}

  isUserAuthenticated = (): boolean => {
    return this.userService.isUserAuthenticated();
  }

  ngOnInit(): void {
    if(!this.isUserAuthenticated() == true) {
      this.router.navigate(['/login']);
    }
  }
  
  // Add a method to handle logout
  logout(): void {
    sessionStorage.removeItem("jwt");
    // Clear the localStorage or perform any other necessary cleanup
    sessionStorage.removeItem("login");
  }
}
