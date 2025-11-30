import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/services/api.service';
import { API_URLS } from 'src/app/config/apiUrlConfig';
import { LocationService } from 'src/app/services/location.service';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit {

  departments: any[] = [];
  popupVisible = false;
  popupMode: 'create' | 'edit' = 'create';
  formData: any = {};
  country :any[] = [];

  constructor(private api: ApiService, private locationService: LocationService) {}

  ngOnInit() {
    this.loadDepartments();  
//      this.locationService.getCountries().subscribe((countries) => {
//   this.country = countries;
// });

    // this.locationService.getStates(2).subscribe(states => console.log(states));
    // this.locationService.getCities(5).subscribe(cities => console.log(cities));
  }

  loadDepartments() {
    this.api.get(API_URLS.department.getAll)
      .subscribe((res: any) => {
        this.departments = res;
      });
  }

  openCreate() {
    this.popupMode = 'create';
    this.formData = {};
    this.popupVisible = true;
  }

  openEdit(data: any) {
    this.popupMode = 'edit';
    this.formData = { ...data };
    this.popupVisible = true;
  }

  onEditClick = (e: any) => {
  const rowData = e.row.data;
  this.openEdit(rowData);
};

onDeleteClick = (e: any) => {
  const rowData = e.row.data;
  this.delete(rowData);
};

  save() {
    if (this.popupMode === 'create') {
      this.api.post(API_URLS.department.create, this.formData)
        .subscribe(() => {
          this.popupVisible = false;
          this.loadDepartments();
        });

    } else {
      this.api.put(API_URLS.department.update(this.formData.departmentId), this.formData)
        .subscribe(() => {
          this.popupVisible = false;
          this.loadDepartments();
        });
    }
  }

  delete(data:any) {
    if (confirm("Are you sure?")) {
      this.api.delete(API_URLS.department.delete(data.departmentId))
        .subscribe(() => {
          this.loadDepartments();
        });
    }
  }


}
