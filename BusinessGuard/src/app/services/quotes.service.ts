import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root',
})
export class QuotesService {
  
  constructor(private http: HttpClient) {}
  
  public getQuotes(
    businessValue: number,
    propertyValue: number,
    propertyType: string,
     
  ) {
    const token = localStorage.getItem('token')
    
    const headers = new HttpHeaders({
      
      Authorization: `Bearer ${token}`, 
    });

    
    return this.http.get<any>(
        propertyType,
      { headers } 
    );
  }
}
