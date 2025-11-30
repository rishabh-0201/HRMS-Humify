import { Component } from '@angular/core';
import { API_URLS } from 'src/app/config/apiUrlConfig';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-designation',
  templateUrl: './designation.component.html',
  styleUrls: ['./designation.component.css']
})
export class DesignationComponent {

  designations: any[] = [];
  popupVisible = false;
  popupMode: 'create' | 'edit' = 'create';
  formData: any = {};

  constructor(private api: ApiService) { }

  ngOnInit() {
    this.loadDesignations();
  }

  loadDesignations() {
    this.api.get(API_URLS.designation.getAll)
      .subscribe((res: any) => {
        this.designations = res;
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
      this.api.post(API_URLS.designation.create, this.formData)
        .subscribe(() => {
          this.popupVisible = false;
          this.loadDesignations();
        });

    } else {
      this.api.put(API_URLS.designation.update(this.formData.designationId), this.formData)
        .subscribe(() => {
          this.popupVisible = false;
          this.loadDesignations();
        });
    }
  }

  delete(data: any) {
    if (confirm("Are you sure?")) {
      this.api.delete(API_URLS.designation.delete(data.designationId))
        .subscribe(() => {
          this.loadDesignations();
        });
    }
  }

}
