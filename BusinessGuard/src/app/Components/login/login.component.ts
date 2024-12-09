import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { Component, OnInit } from '@angular/core';
import {  Router } from '@angular/router';
import { AuthService } from 'app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  form: any = {
    
    email: null,
    password: null,
  };
  isLoggedIn = false;
  isLoginFailed = false;
  errorMessage = '';
  responseText = '';
  roles: string[] = [];
  constructor(
    private authService: AuthService,
    private route : Router,
   
  ) {}
  ngOnInit(): void {

  }
  onSubmit(): void {
    const { email, password } = this.form;
    this.authService.login(email,password).subscribe(res=>{
      localStorage.setItem("token", res.token);
      console.log(res.token,"login success")
      this.route.navigate(["/home"])
      
    })
   
    
  }
  reloadPage(): void {
    window.location.reload();
  }
}
