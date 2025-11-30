import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { API_URLS } from 'src/app/config/apiUrlConfig';
import { ApiService } from 'src/app/services/api.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  model: any = {};

  constructor(private api: ApiService, private router: Router) {}

  submit() {
    // Use centralized API URL
    this.api.post(API_URLS.auth.register, this.model)
      .subscribe({
        next: () => {
          alert("Company Registered! Now login");
          this.router.navigate(['/login']);
        },
        error: (err) => {
          const msg = err?.error?.message || 'Registration failed';
          alert(msg);
        }
      });
  }
}
