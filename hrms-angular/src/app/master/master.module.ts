import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MasterRoutingModule } from './master-routing.module';
// DevExtreme Modules
import { DxDataGridModule } from 'devextreme-angular';
import { DxPopupModule } from 'devextreme-angular';
import { DxButtonModule } from 'devextreme-angular';
import { DxFormModule } from 'devextreme-angular';
import { DxTextBoxModule } from 'devextreme-angular';
import { DxValidatorModule } from 'devextreme-angular';
import { DxCheckBoxModule } from 'devextreme-angular';
import { DxSelectBoxModule } from 'devextreme-angular';
import { DepartmentComponent } from './department/department.component';
import { DesignationComponent } from './designation/designation.component';


@NgModule({
  declarations: [
    
    DepartmentComponent,
    DesignationComponent
    
  ],
  imports: [
    CommonModule,
    MasterRoutingModule,
         // DevExtreme modules
   DxDataGridModule,
    DxPopupModule,
    DxButtonModule,
    DxFormModule,
    DxTextBoxModule,
    DxValidatorModule,
    DxCheckBoxModule,
    DxSelectBoxModule
  ]
})
export class MasterModule { }
