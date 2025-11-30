import { DepartmentComponent } from './department/department.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DesignationComponent } from './designation/designation.component';

const routes: Routes = [
  
   { path: 'department', component: DepartmentComponent },   
   { path: 'designation', component: DesignationComponent }   
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MasterRoutingModule { }
