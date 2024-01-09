import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { UserService } from '../user-service/user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthguardService  implements CanActivate{
  
  constructor(
    private userService: UserService, 
    private router: Router) { }

  canActivate(): boolean {
    if (this.userService.isAdmin()) {
      return true; 
    } else {
      this.router.navigate(['/home']); 
      return false; 
    }
  }
}
