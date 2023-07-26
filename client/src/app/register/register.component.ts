import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  // The @Input decorator is used to receive data from the parent component (HomeComponent) into this component.
  // The 'usersFromHomeComponent' property will be populated with data passed from the HomeComponent.
  @Input() usersFromHomeComponent: any;

  // The @Output decorator is used to emit events from this component to the parent component (HomeComponent).
  // The 'cancelRegister' event is an EventEmitter that allows the parent component to subscribe and handle this event.
  @Output() cancelRegister = new EventEmitter();

  model : any = {}  
  
  constructor() { }

  ngOnInit(): void {
  }


  register() {
    console.log(this.model);
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

}
