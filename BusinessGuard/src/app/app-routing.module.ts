import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateConsumerBusinessComponent } from './Components/consumer/create-consumer-business/create-consumer-business.component';
import { CreateConsumerPropertyComponent } from './Components/consumer/create-consumer-property/create-consumer-property.component';
import { UpdateConsumerBusinessComponent } from './Components/consumer/update-consumer-business/update-consumer-business.component';
import { UpdateConsumerPropertyComponent } from './Components/consumer/update-consumer-property/update-consumer-property.component';
import { ViewConsumerBusinessComponent } from './Components/consumer/view-consumer-business/view-consumer-business.component';
import { ViewConsumerPropertyComponent } from './Components/consumer/view-consumer-property/view-consumer-property.component';
import { HomeComponent } from './Components/home/home.component';
import { LoginComponent } from './Components/login/login.component';
import { CreatePolicyComponent } from './Components/policy/create-policy/create-policy.component';
import { IssuePolicyComponent } from './Components/policy/issue-policy/issue-policy.component';
import { ViewPolicyComponent } from './Components/policy/view-policy/view-policy.component';
import { ViewQuotesComponent } from './Components/policy/view-quotes/view-quotes.component';
import { AboutComponent } from './Components/about/about.component';
import { ContactComponent } from './Components/contact/contact.component';
import { MainComponent } from './Components/main/main.component';
import { GuardGuard } from './guard.guard';



const routes: Routes = [
    { path: '', redirectTo: 'main', pathMatch: 'full'},
    { path: 'main', component: MainComponent },
    { path: 'login', component: LoginComponent },
    { path: 'home', canActivate : [GuardGuard], component: HomeComponent },
    { path: 'createConsumerProperty',component: CreateConsumerPropertyComponent,},
    { path: 'createConsumerBusiness',component: CreateConsumerBusinessComponent,},
    { path: 'createPolicy', component: CreatePolicyComponent },
    { path: 'updateConsumerProperty/:consumerId/:propertyId',component: UpdateConsumerPropertyComponent,},
    { path: 'updateConsumerBusiness/:consumerId',component: UpdateConsumerBusinessComponent,},
    { path: 'issuePolicy', component: IssuePolicyComponent },
    { path: 'viewConsumerBusiness/:consumerId',component: ViewConsumerBusinessComponent,},
    { path: 'viewConsumerProperty/:consumerId/:propertyId',component: ViewConsumerPropertyComponent,},
    { path: 'viewPolicy/:consumerId/:policyId',component: ViewPolicyComponent,},
    { path: 'viewQuotes', component: ViewQuotesComponent },
    { path: 'about', component: AboutComponent },
    { path: 'contact', component: ContactComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}


