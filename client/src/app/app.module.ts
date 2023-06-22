import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
// The HttpClientModule is imported from the '@angular/common/http' module.
// This module provides the necessary dependencies for making HTTP requests in Angular applications.
// The HttpClientModule should be imported in the root or feature module of the application to enable HTTP functionality.
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    // The HttpClientModule is added to the 'imports' array of an Angular module.
    // By including HttpClientModule in the 'imports' array, the application gains access to the HttpClient service
    // and other HTTP-related functionalities provided by Angular.
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    BsDropdownModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
