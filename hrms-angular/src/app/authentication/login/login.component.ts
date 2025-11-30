import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { API_URLS } from 'src/app/config/apiUrlConfig';
import { ApiService } from 'src/app/services/api.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  model: any = {};
  toastMessage: string = '';
  toastType: 'success' | 'error' = 'success';
  showToast: boolean = false;
  invalidCredentials = false;  

  constructor(private api: ApiService, private router: Router) {}

  onSubmit() {
    // Use the centralized URL from API_URLS
    this.api.post<{ token: string }>(API_URLS.auth.login, this.model)
      .subscribe({
        next: (res) => {
          // Save token in localStorage
          localStorage.setItem('token', res.token);
          
          this.invalidCredentials = false;
          this.showToastMessage('Login successful!', 'success');

          // Redirect after toast
          setTimeout(() => {
            this.router.navigate(['/dashboard']);
          }, 1500);
        },
        error: (err) => {
          this.invalidCredentials = true;
          const msg = err?.error?.message || 'Something went wrong';
          this.showToastMessage(msg, 'error');
        }
      });
  }

  showToastMessage(message: string, type: 'success' | 'error') {
    this.toastMessage = message;
    this.toastType = type;
    this.showToast = true;

    setTimeout(() => {
      this.showToast = false;
    }, 5000); // hide after 5 seconds
  }

  onForgot() {
    console.log('Forgot password clicked');
  }

  onCreateAccount() {
    this.router.navigate(['/register']); // redirect to register page
  }
}
