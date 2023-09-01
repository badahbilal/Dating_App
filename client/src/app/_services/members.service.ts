import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { map, of } from 'rxjs';

// MemberService class handles HTTP requests to retrieve member data from the API.

@Injectable({
  providedIn: 'root'
})
export class MemberService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];

  constructor(private http: HttpClient) { }

  // Get a list of members from the API.
  getMembers() {
    if (this.members.length > 0) return of(this.members);
    return this.http.get<Member[]>(this.baseUrl + 'users').pipe(
      map(members => {
        this.members = members;
        return members;
      })
    )
  }

  // Get a specific member by username from the API.
  getMember(username: string) {
    const member = this.members.find(x => x.userName === username);
    if (member !== undefined) return of(member);
    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }

  // Update a member's data through the API.
  updateMember(member: Member) {
    // Make an HTTP PUT request to the API to update the member's data.
    return this.http.put(this.baseUrl + 'users', member).pipe(
      // Use the 'map' operator to update the local members array after a successful update.
      map(() => {
        // Find the index of the member being updated in the local 'members' array.
        const index = this.members.findIndex(m => m.id === member.id);

        // Update the local member object with the new data from the 'member' parameter.
        // This ensures that the local data is kept in sync with the updated data.
        this.members[index] = { ...this.members[index], ...member };
      })
    );
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