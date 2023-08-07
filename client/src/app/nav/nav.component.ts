import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {

  model: any = {};



  constructor(public accountService: AccountService, private router: Router) { }

  ngOnInit(): void {

  }


  login() {

    // Perform user login by calling the 'login' method of the accountService.
    // The login details are passed in the 'model' object.

    this.accountService.login(this.model).subscribe({
      // Handle the response after a successful login (next callback).
      // In this case, navigate to the "/members" route using the Angular Router.

      next: response => {
        // Navigate to the "/members" route using the Angular Router.
        // This is typically done after successful authentication.

        this.router.navigateByUrl("/members");
      },

      // Handle any errors that occur during the login process (error callback).
      // In this case, log the error to the console.

      error: error => {
        console.log(error);
      }
    });

  }

  // The 'logout' function handles the user logout process.

  logout() {
    // Call the 'logout' method of the accountService to perform user logout.
    this.accountService.logout();

    // Navigate to the root path ("/") after logout.
    this.router.navigateByUrl("/");
  }


}
