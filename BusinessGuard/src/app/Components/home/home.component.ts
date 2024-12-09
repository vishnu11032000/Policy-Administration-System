import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'app/services/auth.service';

import { ConsumerService } from 'app/services/consumer.service';
import { PolicyService } from 'app/services/policy.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  public quoteResponse: any;

  public consumerBusinessForm!: FormGroup;
  public consumerPropertyForm!: FormGroup;
  public policyForm!: FormGroup;
  public quotesForm!: FormGroup;

  consumerError: any;
  propertyError: any;
  quotesError: any;
  policyError: any;

  hasError: boolean = false;
  errorMsg: any;

  constructor(
    private _consumerService: ConsumerService,
    private _policyService: PolicyService,
    private _router: Router,
    private authService :AuthService
  ) {}

  ngOnInit(): void {
    this.consumerBusinessForm = new FormGroup({
      consumerId: new FormControl(),
    });

    this.consumerPropertyForm = new FormGroup({
      consumerId2: new FormControl(),
      propertyId: new FormControl(),
    });

    this.quotesForm = new FormGroup({
      businessValue: new FormControl(),
      propertyValue: new FormControl(),
      propertyType: new FormControl(),
    });

    this.policyForm = new FormGroup({
      consumerId3: new FormControl(),
      policyId: new FormControl(),
    });
  }

 
  public onClickAddConsumerBusiness(): void {
    this._router.navigate(['/createConsumerBusiness']);
  }
  public onClickViewConsumerBusiness(consumerBusinessForm: any): void {
    if (consumerBusinessForm.consumerId == null) {
      this.consumerError = 'Consumer Id is required';
      return;
    }
    
    this._router.navigateByUrl(
      '/viewConsumerBusiness/' + consumerBusinessForm.consumerId
    );
  }
  public onClickUpdateConsumerBusiness(consumerBusinessForm: any): void {
    if (consumerBusinessForm.consumerId == null) {
      this.consumerError = 'Consumer Id is required';
      return;
    }
    this._router.navigate([
      '/updateConsumerBusiness/' + consumerBusinessForm.consumerId,
    ]);
  }

  
  public onClickViewConsumerProperty(consumerPropertyForm: any): void {
    if (
      consumerPropertyForm.consumerId2 == null &&
      consumerPropertyForm.propertyId == null
    ) {
      this.propertyError = 'Consumer Id and Property Id is required';
      return;
    }
    if (consumerPropertyForm.consumerId2 == null) {
      this.propertyError = 'Consumer Id is required';
      return;
    }
    if (consumerPropertyForm.propertyId == null) {
      this.propertyError = 'Property Id is required';
      return;
    }
  
    this._router.navigateByUrl(
      '/viewConsumerProperty/' +
        consumerPropertyForm.consumerId2 +
        '/' +
        consumerPropertyForm.propertyId
    );
  }
  public onClickAddConsumerProperty(): void {
    this._router.navigate(['/createConsumerProperty']);
  }
  public onClickUpdateConsumerProperty(consumerPropertyForm: any): void {
    if (
      consumerPropertyForm.consumerId2 == null &&
      consumerPropertyForm.propertyId == null
    ) {
      this.propertyError = 'Consumer Id and Property Id is required';
      return;
    }
    if (consumerPropertyForm.consumerId2 == null) {
      this.propertyError = 'Consumer Id is required';
      return;
    }
    if (consumerPropertyForm.propertyId == null) {
      this.propertyError = 'Property Id is required';
      return;
    }
    this._router.navigate([
      '/updateConsumerProperty/' +
        consumerPropertyForm.consumerId2 +
        '/' +
        consumerPropertyForm.propertyId,
    ]);
  }

  // quotes function
  public onClickViewQuotes(quotesForm: any): void {
    this.hasError = false;
    console.log(quotesForm);
    const{businessValue,propertyValue,propertyType} =quotesForm
   
   this.authService.getQuotesForPolicy(businessValue, propertyValue , propertyType).subscribe(res =>{
    console.log("successfull",res)
    this.quoteResponse=res;
  
  })
  }
  


  public onClickViewPolicy(policyForm: any): void {
  
    if (policyForm.consumerId3 == null || policyForm.policyId == null) {
      this.policyError = 'All fields are required';
      return;
    }
    this._router.navigateByUrl(
      '/viewPolicy/' + policyForm.consumerId3 + '/' + policyForm.policyId
    );
  }
  public onClickAddPolicy(): void {
    this._router.navigate(['/createPolicy']);
  }
  public onClickIssuePolicy(): void {
    this._router.navigate(['/issuePolicy']);
  }
}
