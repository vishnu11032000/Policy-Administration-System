import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './Components/home/home.component';
import { ConsumerService } from './services/consumer.service';
import { PolicyService } from './services/policy.service';
import { ViewConsumerBusinessComponent } from './Components/consumer/view-consumer-business/view-consumer-business.component';
import { ViewConsumerPropertyComponent } from './Components/consumer/view-consumer-property/view-consumer-property.component';
import { CreatePolicyComponent } from './Components/policy/create-policy/create-policy.component';
import { IssuePolicyComponent } from './Components/policy/issue-policy/issue-policy.component';
import { ViewPolicyComponent } from './Components/policy/view-policy/view-policy.component';
import { ViewQuotesComponent } from './Components/policy/view-quotes/view-quotes.component';
import { CreateConsumerBusinessComponent } from './Components/consumer/create-consumer-business/create-consumer-business.component';
import { CreateConsumerPropertyComponent } from './Components/consumer/create-consumer-property/create-consumer-property.component';
import { UpdateConsumerPropertyComponent } from './Components/consumer/update-consumer-property/update-consumer-property.component';
import { UpdateConsumerBusinessComponent } from './Components/consumer/update-consumer-business/update-consumer-business.component';
import { LoginComponent } from './Components/login/login.component';




import { CardComponent } from './Cards/card/card.component';
import { AboutComponent } from './Components/about/about.component';
import { ContactComponent } from './Components/contact/contact.component';
import { MainComponent } from './Components/main/main.component';

@NgModule({
  declarations: [
    AppComponent,
    CreatePolicyComponent,
    ViewPolicyComponent,
    IssuePolicyComponent,
    HomeComponent,
    ViewConsumerPropertyComponent,
    ViewConsumerBusinessComponent,
    ViewQuotesComponent,
    CreateConsumerBusinessComponent,
    CreateConsumerPropertyComponent,
    UpdateConsumerPropertyComponent,
    UpdateConsumerBusinessComponent,
    LoginComponent,
    CardComponent,
    AboutComponent,
    ContactComponent,
    MainComponent,
  ],
  imports: [
    BrowserModule,
    RouterModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    ConsumerService,
    PolicyService,
    
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
