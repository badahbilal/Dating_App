import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { authGuard } from './_guards/auth.guard';
import { TestErrorComponent } from './errors/test-error/test-error.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';

// Define an array of route configurations for navigation in the Angular application.
const routes: Routes = [
  // Route for the root path (default route) maps to the HomeComponent.
  { path: '', component: HomeComponent },
  // Define a route configuration for the main route ('') that contains child routes.
  // The 'runGuardsAndResolvers' property is set to 'always' to ensure that route guards and resolvers
  // run on each navigation to this route.

  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard], // Apply the 'authGuard' route guard to this route.
    children: [
      // Route for the "members" path maps to the MemberListComponent.
      // The 'authGuard' route guard is also applied to this child route.
      { path: 'members', component: MemberListComponent, canActivate: [authGuard] },

      // Route for the "members/:username" path maps to the MemberDetailComponent.
      // No specific route guard is applied to this child route.
      { path: 'members/:username', component: MemberDetailComponent },

      // Route for the "lists" path maps to the ListsComponent.
      // No specific route guard is applied to this child route.
      { path: 'lists', component: ListsComponent },

      // Route for the "messages" path maps to the MessagesComponent.
      // No specific route guard is applied to this child route.
      { path: 'messages', component: MessagesComponent },
    ]
  },

  { path: "errors", component: TestErrorComponent },
  { path: "not-found", component: NotFoundComponent },
  { path: "server-error", component: ServerErrorComponent },

  // Route for any other unspecified path maps to the HomeComponent.
  // This acts as a catch-all route for unknown paths.
  { path: '**', component: NotFoundComponent, pathMatch :  "full" }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
