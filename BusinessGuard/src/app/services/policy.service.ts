import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Policy } from 'app/models/policy.model';


@Injectable({
  providedIn: 'root',
})
export class PolicyService {
  constructor(private http: HttpClient) {}

  public addPolicy(consumer: any): any {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`, 
    });

    return this.http.post<any>(
      'https://localhost:7150/api/Policy/createPolicy',
      consumer,
      { headers } 
    );
  }
  // issuePolicy;
  public issuePolicy(policy: any): any {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`, 
    });
    return this.http.post<any>('https://localhost:7150/api/Policy/issuePolicy', policy, { headers });
  }
  
  

  // viewPolicy
  public getPolicy(policyId: string, consumerId: number) {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`, 
    });
    const url = `https://localhost:7150/api/Policy/viewPolicy?consumerId=${consumerId}&policyId=${policyId}`;

    
    return this.http.get<any>(url , {headers});
  }
  

  // getQuotes;
  public getQuotes(
    businessValue: Number,
    propertyValue: Number,
    propertyType: string
  ): any {
    return this.http.get<any>(
     
        propertyType
    );
  }
}
