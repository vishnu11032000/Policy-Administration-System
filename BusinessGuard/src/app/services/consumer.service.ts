import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Consumer } from 'app/models/consumer.model';
import { Property } from 'app/models/property.model';

import { Observable } from 'rxjs/internal/Observable';

const headers = new HttpHeaders().set('Content-Type', 'application/json');
const requestOptions: Object = {
  headers: headers,
  responseType: 'text',
};

@Injectable({
  providedIn: 'root',
})
export class ConsumerService {
  constructor(private http: HttpClient) {}
  // createConsumerBusiness
  public addConsumerBusiness(consumer: any): any {
    console.log(consumer);
    return this.http.post<any>(
     'https://localhost:7150/api/Consumer/createConsumerBusiness' ,
      consumer,
      requestOptions
    );
  }

  // createBusinessProperty
  public addBusinessProperty(property: Property): any {
    return this.http.post<Property>(
      'https://localhost:7150/api/Consumer/createBusinessProperty',
      property,
      requestOptions
    );
  }

  // updateConsumerBusiness
  public updateConsumerBusiness(consumer: Consumer): any {
    return this.http.put<Consumer>(
      'https://localhost:7150/api/Consumer/updateConsumerBusiness',
      consumer,
      requestOptions
    );
  }

  // updateBusinessProperty
  public updateBusinessProperty(property: Property): any {
    return this.http.put<Consumer>(
      'https://localhost:7150/api/Consumer/updateBusinessProperty',
      property,
      requestOptions
    );
  }

  // viewConsumerBusiness
  public getConsumerBusiness(id: number): Observable<any> {
    return this.http.get(`https://localhost:7150/api/Consumer/viewConsumerBusiness?consumerId=${id}`, { responseType: 'json' });
  }
  

  // viewConsumerProperty
  public getConsumerProperty(consumerId: number, propertyId: number): any {
    return this.http.get(`https://localhost:7150/api/Consumer/viewConsumerProperty?consumerId=${consumerId}&propertyId=${propertyId}`, { responseType: 'json' });
  }
  
}
