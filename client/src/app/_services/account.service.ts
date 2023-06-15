import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AccountService {


  baseUrl = 'https://localhost:7267/api/';
  
  constructor(private http : HttpClient) { }

}
