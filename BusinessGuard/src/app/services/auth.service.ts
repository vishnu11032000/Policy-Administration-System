import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import {  HttpParams } from '@angular/common/http';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Access-Control-Allow-Origin': '*',
  }),
};
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl = 'https://localhost:7150/Quotes';

  constructor(private http: HttpClient) {}

  login( email: string, password: string): Observable<any> {
    console.log("success", email , password)
    return this.http.post(
      'https://localhost:7150/api/Auth/login' ,
      {
        email,
        password,
      }
    );
  }
// quotes.ts
  getQuotesForPolicy(businessValue: number, propertyValue: number, propertyType: string): Observable<any> {
    const params = new HttpParams()
      .set('businessValue', businessValue.toString())
      .set('propertyValue', propertyValue.toString())
      .set('propertyType', propertyType);
      const token = localStorage.getItem("token");
      console.log("token",token)
      const headers = new HttpHeaders()
      .set('Authorization', `Bearer ${token}`);  

    return this.http.get(`${this.baseUrl}/getQuotesForPolicy`, { headers, params });
    
  }


  
  
}
