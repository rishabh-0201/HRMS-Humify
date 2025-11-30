import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent {

   constructor(private auth: AuthService, private router: Router) {}

    logout() {
    localStorage.removeItem("token");
     this.router.navigate(['/login']);
  }

}
