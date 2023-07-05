import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Employee } from 'src/app/models/employee.model';
import { EmployeesService } from 'src/app/services/employees.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-update-employee',
  templateUrl: './update-employee.component.html',
  styleUrls: ['./update-employee.component.css'],
})
export class UpdateEmployeeComponent implements OnInit {
  //#region Variables declarations
  submitted = false;

  // Declare an empty employee to be updated.
  anExistingEmployee: Employee = {
    id: '',
    employeeName: '',
    employeeEmail: '',
    employeePhone: '',
    employeeDateOfBirth: '',
  };

  // Declare a new form group for the validation.
  updateFormGroup = new FormGroup({
    id: new FormControl(''),
    name: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    phone: new FormControl('', [
      Validators.required,
      Validators.pattern('^[0-9]*$'),
      Validators.minLength(10),
      Validators.maxLength(10),
    ]),
    dateOfBirth: new FormControl('', [Validators.required]),
  });
  //#endregion

  //#region Constructor
  constructor(
    private employeeService: EmployeesService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {}
  //#endregion

  //#region OnInit members
  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');

        if (id) {
          // Call the API to get all info for this id.
          this.employeeService.getEmployee(id).subscribe({
            next: (employee) => {
              console.log('Return data from the API: ' + [{employee}]);
              console.log(employee);

              // Updaye the employee id only.
              this.anExistingEmployee.id = employee.id;

              // Set the values to the form controls.
              this.updateFormGroup.get('id')?.setValue(employee.id);
              this.updateFormGroup.get('name')?.setValue(employee.employeeName);
              this.updateFormGroup.get('email')?.setValue(employee.employeeEmail);
              this.updateFormGroup.get('phone')?.setValue(employee.employeePhone);

              // Transform the date format.
              var datePipe = new DatePipe('en-US');
              var formatedyear = datePipe.transform(
                employee.employeeDateOfBirth,
                'yyyy-MM-dd'
              );

              if (formatedyear) {
                this.updateFormGroup
                  .get('dateOfBirth')
                  ?.setValue(employee.employeeDateOfBirth);
              }
            },
            error: (response) => {
              console.log(response);
            },
          });
        }
      },
    });
  }
  //#endregion

  //#region Methods

  // Gets the value of control with name 'id'.
  get id() {
    return this.updateFormGroup.get('id');
  }

  // Gets the value of control with name 'name'.
  get name() {
    return this.updateFormGroup.get('name');
  }

  // Gets the value of control with name 'email'.
  get email() {
    return this.updateFormGroup.get('email');
  }

  // Gets the value of control with name 'phone'.
  get phone() {
    return this.updateFormGroup.get('phone');
  }

  // Gets the value of control with name 'dateOfBirth'.
  get dateOfBirth() {
    return this.updateFormGroup.get('dateOfBirth');
  }

  // Calls the update employee service for HTTPPUT if form group is valid.
  updateEmployee() {
    this.submitted = true;

    if (this.updateFormGroup.invalid) {
      return;
    }

    this.anExistingEmployee.employeeName = this.updateFormGroup.controls['name'].value
      ? this.updateFormGroup.controls['name'].value
      : '';
    this.anExistingEmployee.employeeEmail = this.updateFormGroup.controls['email'].value
      ? this.updateFormGroup.controls['email'].value
      : '';
    this.anExistingEmployee.employeePhone = this.updateFormGroup.controls['phone'].value
      ? this.updateFormGroup.controls['phone'].value
      : '';
    this.anExistingEmployee.employeeDateOfBirth = this.updateFormGroup.controls[
      'dateOfBirth'
    ].value
      ? this.updateFormGroup.controls['dateOfBirth'].value
      : '';

    this.employeeService.updateEmployee(this.anExistingEmployee).subscribe({
      next: (employee) => {
        console.log(employee);
        this.router.navigate(['employees']);
      },
      error: (response) => {
        console.log(response);
      },
    });
  }

  //#endregion
}
