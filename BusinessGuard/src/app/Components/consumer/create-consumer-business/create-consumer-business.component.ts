import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { ConsumerService } from 'app/services/consumer.service';

@Component({
  selector: 'app-create-consumer-business',
  templateUrl: './create-consumer-business.component.html',
  styleUrls: ['./create-consumer-business.component.css'],
})
export class CreateConsumerBusinessComponent implements OnInit {
  public consumerForm!: FormGroup;
  public hasError: boolean = false;
  public errorMsg: string = '';
  public response: any;

  constructor(
    private _consumerService: ConsumerService,
    private _fb: FormBuilder,
    private _router: Router
  ) {
    this.myForm();
  }

  myForm() {
    this.consumerForm = this._fb.group({
      firstName: [
        '',
        Validators.compose([
          Validators.required,
          Validators.pattern('^[A-Z+a-z ]{3,40}$'),
        ]),
      ],
      lastName: [
        '',
        Validators.compose([
          Validators.required,
          Validators.pattern('^[A-Z+a-z ]{3,40}$'),
        ]),
      ],
      email: [
        '',
        Validators.compose([
          Validators.required,
          Validators.email,
          Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,30}$'),
        ]),
      ],
      pan: [
        '',
        Validators.required,
      ],
      dob: [
        '',
        Validators.compose([
          Validators.required
          
        ]),
      ],
      businessName: ['', Validators.required],
      businessType: ['', Validators.required],
      capitalInvested: ['', Validators.required],
      validity: ['', Validators.required],
      agentId: [
        '',
        Validators.compose([
          Validators.required,
          Validators.pattern('[0-9]{1,4}'),
        ]),
      ],
      agentName: [
        '',
        Validators.compose([
          Validators.required,
          Validators.pattern('^[A-Z+a-z ]{3,40}$'),
        ]),
      ],
      businessTurnover: [
        '',
        Validators.compose([
          Validators.required,
        
        ]),
      ],
      businessAge: [
        '',
        Validators.compose([
          Validators.required,
          Validators.pattern('[0-9]{1,3}'),
        ]),
      ],
      totalEmployees: [
        '',
        Validators.compose([
          Validators.required,
          Validators.pattern('[0-9]{1,4}'),
        ]),
      ],
    });
  }

  ngOnInit(): void {}

  onSubmit(consumerForm: any): void {
    if (this.consumerForm.valid) {
      const {
        firstName,
        lastName,
        email,
        pan,
        dob,
        businessName,
        businessType,
        capitalInvested,
        validity,
        agentId,
        agentName,
        businessTurnover,
        businessAge,
        totalEmployees
      } = consumerForm;

      
      const formattedDob = this.formatDate(dob);

      const consumerData = {
        firstName,
        lastName,
        email,
        pan,
        dob,  
        businessName,
        businessType,
        capitalInvested,
        validity: validity === 'true',  
        agentId,
        agentName,
        businessTurnover,
        businessAge,
        totalEmployees
      };

      this._consumerService.addConsumerBusiness(consumerData).subscribe(
        (data: any) => {
          console.log(data);
          alert("Consumer Bussiness Created Successfully")
          this._router.navigate(["/home"])
          if (data === 'Created') {
            this.response = 'Successfully created Consumer Business';
          }
        },
        (error: HttpErrorResponse) => {
          this.hasError = true;
          this.errorMsg = error.error?.error ?? error.message;
          console.error(error);
        }
      );
    } else {
      this.errorMsg = 'Please fill out all required fields correctly';
      this.hasError = true;
    }
  }

  formatDate(dob: string): string {
    const [day, month, year] = dob.split('/');
    return `${year}-${month}-${day}`; 
  }

  onCancel(): void {
    this._router.navigate(['/']);
  }
}
