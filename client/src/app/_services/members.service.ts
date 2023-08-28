import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

// MemberService class handles HTTP requests to retrieve member data from the API.

@Injectable({
  providedIn: 'root'
})
export class MemberService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  // Get a list of members from the API.
  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'users');
  }

  // Get a specific member by username from the API.
  getMember(username: string) {
    return this.http.get<Member>(this.baseUrl + 'user/' + username);
  }

}

/**
 * 
 * 
 * 
 * Deleted Code 
  // Helper method to create HTTP request headers with authorization token.
  private getHttpOptions() {
    const userString = localStorage.getItem('user');

    // Check if a user is authenticated and has a token.
    if (!userString) return;

    // Parse the user object from local storage.
    const user = JSON.parse(userString);

    // Return HTTP request headers with the authorization token.
    return {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + user.token
      })
    };
  }
 */