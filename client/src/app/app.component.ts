import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'Dating app';
  users : any;
  
  constructor(private http: HttpClient, private accountService: AccountService){

  }
  
  // The ngOnInit() method is a lifecycle hook in Angular that is invoked when the component is initialized.
  // This method is typically used to perform initialization tasks, such as fetching data from a server,
  // setting up subscriptions, or initializing component properties.
  // In this case, an error is intentionally thrown to indicate that the ngOnInit() method is not implemented.
  // This serves as a placeholder or reminder for the developer to implement the necessary logic within ngOnInit().
  // TODO: Implement the necessary logic within the ngOnInit() method
  ngOnInit(): void {
    
    this.getUsers();
    this.setCurrentUser();
  }

  getUsers(){
    this.http.get('https://localhost:7267/api/users').subscribe({
      next : response => this.users = response,
      error : error => console.log(error),
      complete : () =>  console.log('Request has completed'),
    });
  }

  setCurrentUser(){
    const userString = localStorage.getItem('user');

    if(!userString) return;

    const user : User =  JSON.parse(userString);
    this.accountService.setCurrentUser(user); 
  }
}
