// import { Component, OnInit } from '@angular/core';
// import { TokenStorageService } from './services/token-storage.service';
// import { AuthService } from './services/auth.service';

// @Component({
//   selector: 'app-root',
//   templateUrl: './app.component.html',
//   styleUrls: ['./app.component.css'],
// })
// export class AppComponent implements OnInit {
//   private roles: string[] = [];
//   isLoggedIn = true;
 
//   username?: string;
//   constructor(private tokenStorageService: TokenStorageService , private authservice : AuthService) {}
//   ngOnInit(): void {
//     const token =localStorage.getItem("token")
//     if(token == null)
//     {
//       this.isLoggedIn = false;
//     }else
//     {
//     this.isLoggedIn =true;
//     }
//     this.isLoggedIn = !!this.tokenStorageService.getToken();
//     if (this.isLoggedIn) {
//       const user = this.tokenStorageService.getUser();
//       this.roles = user.roles;
    
//       this.username = user.username;
//     }
//   }
//   logout(): void {
//     this.tokenStorageService.signOut();
//     window.location.reload();
//   }
// }

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  isLoggedIn = true;

  constructor(private router: Router) {}

  ngOnInit(): void {
    
    const token = localStorage.getItem('token');
    this.isLoggedIn = token !== null;
    // window.location.reload()
  }

  logout(): void {
    
    localStorage.removeItem('token');
    this.isLoggedIn = false;  
    
    this.router.navigate(['/login']);
  }
}
