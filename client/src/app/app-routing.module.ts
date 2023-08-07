import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';

// Define an array of route configurations for navigation in the Angular application.
const routes: Routes = [
  // Route for the root path (default route) maps to the HomeComponent.
  { path: '', component: HomeComponent },

  // Route for the "members" path maps to the MemberListComponent.
  { path: 'members', component: MemberListComponent },

  // Route for the "members/:id" path maps to the MemberDetailComponent.
  { path: 'members/:id', component: MemberDetailComponent },

  // Route for the "lists" path maps to the ListsComponent.
  { path: 'lists', component: ListsComponent },

  // Route for the "messages" path maps to the MessagesComponent.
  { path: 'messages', component: MessagesComponent },

  // Route for any other unspecified path maps to the HomeComponent.
  // This acts as a catch-all route for unknown paths.
  { path: '**', component: HomeComponent }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
