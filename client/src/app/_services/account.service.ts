import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {


  baseUrl = 'https://localhost:7267/api/';

  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any) {

    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }

  // Send a registration request to the server with the provided 'model' data.
  // The 'model' parameter contains user registration details such as username, password, etc.

  register(model: any) {
    // Make a POST request to the server endpoint for user registration.
    // The 'User' type is expected to represent the user information received from the server.

    return this.http.post<User>(this.baseUrl + 'account/register', model)
      .pipe(
        // Use the 'map' operator to process the response from the server.
        map(user => {
          // If the 'user' object is truthy (not null or undefined),
          // store it in the local storage as a JSON string under the "user" key.
          // Also, update the 'currentUserSource' BehaviorSubject with the new user.

          if (user) {
            localStorage.setItem("user", JSON.stringify(user));
            this.currentUserSource.next(user);
          }
        })
      );
  }


  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

}
