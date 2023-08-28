import { OnInit } from '@angular/core';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode = false;
  users: any;

  constructor() { }

  ngOnInit(): void {
    //this.getUsers();
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }


  /* getUsers() {
    this.http.get('https://localhost:7267/api/users').subscribe({
      next: response => this.users = response,
      error: error => console.log(error),
      complete: () => console.log('Request has completed'),
    });
  } */

  // The 'cancelRegisterMode' function is called when the child component emits the 'cancelRegister' event.
  // The function takes a boolean 'event' parameter, which represents the event data sent from the child component.
  cancelRegisterMode(event: boolean) {
    
    // Update the 'registerMode' property of the parent component (HomeComponent) based on the 'event'.
    // The 'registerMode' property is typically used to control the visibility of the registration form.
    this.registerMode = event;
  }


}
