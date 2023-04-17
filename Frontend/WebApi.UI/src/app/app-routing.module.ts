import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddEmployeeComponent } from './components/employees/add-employee/add-employee.component';
import { EmployeesListComponent } from './components/employees/employees-list/employees-list.component';
import { UpdateEmployeeComponent } from './components/employees/update-employee/update-employee.component';

const routes: Routes = [{
  path:'',
  component:EmployeesListComponent
},{
  path:'employees',
  component:EmployeesListComponent
},{
  path:'employees/add',
  component:AddEmployeeComponent
},{
  path:'employees/update/:id',
  component:UpdateEmployeeComponent
}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
