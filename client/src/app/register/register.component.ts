import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {


  // The @Output decorator is used to emit events from this component to the parent component (HomeComponent).
  // The 'cancelRegister' event is an EventEmitter that allows the parent component to subscribe and handle this event.
  @Output() cancelRegister = new EventEmitter();

  model: any = {}

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }


  // The 'register' method initiates the user registration process by calling the 'register' method of the accountService.
  // After subscribing to the registration request, it handles the response using two callback functions: 'next' and 'error'.

  register() {
    // Call the 'register' method of the accountService to initiate the user registration process.
    // The 'this.model' contains the user registration data (e.g., username, password).

    this.accountService.register(this.model).subscribe({
      // Handle the response after a successful registration (next callback).
      // In this case, call the 'cancel' method to perform an action when the registration is successful.

      next: () => {
        this.cancel();
      },

      // Handle any errors that occur during the registration process (error callback).
      // In this case, log the error to the console.

      error: error => {
        console.log(error);
      }
    });
  }

  // The 'cancel' method is called when the user wants to cancel the registration process.
  // It emits an event using the 'cancelRegister' EventEmitter to notify the parent component.

  cancel() {
    // Emit the 'cancelRegister' event with the value 'false' to notify the parent component.
    // The parent component (HomeComponent) can subscribe to this event and handle it accordingly.

    this.cancelRegister.emit(false);
  }


}
