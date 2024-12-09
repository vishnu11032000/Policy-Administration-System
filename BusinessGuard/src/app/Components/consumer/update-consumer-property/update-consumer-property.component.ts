import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Consumer } from 'app/models/consumer.model';
import { ConsumerService } from 'app/services/consumer.service';

@Component({
  selector: 'app-update-consumer-property',
  templateUrl: './update-consumer-property.component.html',
  styleUrls: ['./update-consumer-property.component.css'],
})
export class UpdateConsumerPropertyComponent implements OnInit {
  public hasError: boolean = false;
  public errorMsg: string = '';
  public response: any;
  state: any = { consumerId: null, propertyId: null };

  propertyForm = new FormGroup({
    businessId: new FormControl(),
    consumerId: new FormControl(),
    buildingSqFt: new FormControl(),
    buildingType: new FormControl(),
    buildingStoreys: new FormControl(),
    buildingAge: new FormControl(),
    costOfTheAsset: new FormControl(), 
    salvageValue: new FormControl(),
    usefulLifeOfAsset: new FormControl(), 
  });

  constructor(
    private _consumerService: ConsumerService,
    private _router: Router,
    private _Activatedroute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.state.consumerId = this._Activatedroute.snapshot.paramMap.get('consumerId');
    this.state.propertyId = this._Activatedroute.snapshot.paramMap.get('propertyId');
    console.log(this.state);

    this.getConsumerProperty();
  }

  public getConsumerProperty(): any {
    return this._consumerService
      .getConsumerProperty(this.state.consumerId, this.state.propertyId)
      .subscribe(
        (consumerBusiness: any) => {
          console.log("consumerBusiness", consumerBusiness);
          this.setFormData(consumerBusiness);
        },
        (error: any) => {
          this.hasError = true;
          this.errorMsg = error.error || error.error.error;
          console.error(error);
        }
      );
  }

  public setFormData(data: any) {
    this.propertyForm.setValue({
      businessId: data.businessId,
      consumerId: data.consumerId,
      buildingSqFt: data.buildingSqFt,
      buildingType: data.buildingType,
      buildingStoreys: data.buildingStoreys,
      buildingAge: data.buildingAge,
      costOfTheAsset: data.costOfTheAsset,
      salvageValue: data.salvageValue,
      usefulLifeOfAsset: data.usefulLifeOfAsset, 
    });
  }

  onSubmit(consumerForm: Consumer): void {
    console.log(consumerForm);

    this._consumerService
      .updateBusinessProperty(consumerForm)
      .subscribe(
        (data: any) => {
          this.response = 'Successfully updated Business Property';
        },
        (error: any) => {
          this.hasError = true;
          this.errorMsg = error.error || error.error.error;
          console.error(error);
        }
      );
  }

  onCancel(): void {
    this._router.navigate(['/']);
  }
}
