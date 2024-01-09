import { Injectable } from '@angular/core';
import { User } from 'src/app/interfaces/user';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { AuthenticatedResponse } from 'src/app/interfaces/authenticated-response';
import { Router } from '@angular/router';
import { Login } from 'src/app/interfaces/login';
import { Observable } from 'rxjs';
import { UserCreate } from 'src/app/interfaces/userCreate';
import { enviroment } from 'src/enviroment/enviroment';
import { Message } from 'src/app/interfaces/message';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private usersUrl = enviroment.API_USER_BASE_URL+'/api/User';  // URL to web api
  private loginUrl = enviroment.API_USER_BASE_URL+'/api/Authenticate/login';
  private chatUrl = enviroment.API_CHAT_BASE_URL + '/api/Message';

  public isLoggedIn: boolean = false;
  
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient,
    private router: Router
  ) { }

  // Requests
  login(credentials: Login): Observable<AuthenticatedResponse> {
    return this.http.post<AuthenticatedResponse>(this.loginUrl, credentials, this.httpOptions);
  }
 

  
  // Utilities
  saveToken(token: string) {
    this.saveToLocalStorage("jwt", token);
    const savedUser = this.getUserFromToken();
    console.log(savedUser);
    if(savedUser.isAdmin) {
      console.log("admin");
      this.router.navigate(["/admin"]);
    }
    else{
      console.log("user");
      this.router.navigate(["/home"]);
    }
  }

  getToken() {
    return sessionStorage.getItem("jwt");
  }

  getUsers(): Observable<any[]> {
    const token = this.getToken(); // Replace with your method to get the JWT token
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.get<any[]>(this.usersUrl, { headers });
  }

  postUser(user: UserCreate): Observable<User> {
    const token = this.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.post<User>(this.usersUrl, user, { headers });
  }

  registerUser(name: string): Observable<any> {
    return this.http.post<string>(this.chatUrl+'?name='+name,name);
  }


  deleteUser(userId: number): Observable<any> {
    const token = this.getToken();
    const deleteUserUrl = `${this.usersUrl}/${userId}`;
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.delete<any>(deleteUserUrl, { headers });
  }

  updateUser(updatedUserData: any): Observable<any> {
    const token = this.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.put<any>(this.usersUrl, updatedUserData, { headers });
  }

  saveToLocalStorage(tag: string, content: any) {
    sessionStorage.setItem(tag, content);
  }

  getUserFromToken(): User {
    const token = sessionStorage.getItem("jwt");
    if (token) {
      const tokenParts = token.split('.');
      if (tokenParts.length === 3) {
        const payload = JSON.parse(atob(tokenParts[1]));
        
        const isAdmin = payload.IsAdmin.toLowerCase() === 'true';
        
        return { id: payload.Id, name: payload.Name, isAdmin: isAdmin };
      }
    }
  
    return { id: 0, name: '', isAdmin: false };
  }
  

  isUserAuthenticated = (): boolean => {
    return (sessionStorage.getItem("jwt") != null);
  }

  isAdmin = (): boolean => {
    const user = this.getUserFromToken();
    return user.isAdmin;
  }
}
