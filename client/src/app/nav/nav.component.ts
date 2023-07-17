import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {

  model : any = {};
  loggedIn : boolean = false;


  constructor(private accountService : AccountService) { }

  ngOnInit(): void {
    this.getCurrentUser();
  }

  getCurrentUser(){
    this.accountService.currentUser$.subscribe({
      next : user => this.loggedIn = !!user,
      error : error => console.log(error)
    })
  }

  login() {
    
    // Perform user login by calling the login method of the accountService.
    // The login details are passed in the 'model' object.
    this.accountService.login(this.model).subscribe({
      next: response => {
        // Handle the response from the server after successful login.
        // In this case, the response is logged to the console and the 'loggedIn' flag is set to true.
        console.log(response);
        this.loggedIn = true;
      },

      error: error => {
        // Handle any errors that occur during the login process.
        // In this case, the error is logged to the console.
        console.log(error);
      }
    });
  }

  logout() {

    this.accountService.logout();
    this.loggedIn = false;
  }
  
}
