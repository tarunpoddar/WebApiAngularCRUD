import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Employee } from 'src/app/models/employee.model';
import { EmployeesService } from 'src/app/services/employees.service';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css'],
})

// Employee component class.
export class AddEmployeeComponent implements OnInit {
  //#region Variables

  submitted = false;

  // Create an empty employee to be added.
  addEmployee: Employee = {
    id: '',
    employeeName: '',
    employeeEmail: '',
    employeePhone: '',
    employeeDateOfBirth: '',
  };
  //#endregion

  //#region Contructor
  constructor(
    private employeeService: EmployeesService,
    private router: Router
  ) {}
  //#endregion

  //#region Methods
  // Executes when this page initializes for first time.
  ngOnInit(): void {}

  // Create a new form group for the validation.
  registerFormGroup = new FormGroup({
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

  // Gets the value of control with name 'name'.
  get name() {
    return this.registerFormGroup.get('name');
  }

  // Gets the value of control with name 'email'.
  get email() {
    return this.registerFormGroup.get('email');
  }

  // Gets the value of control with name 'phone'.
  get phone() {
    return this.registerFormGroup.get('phone');
  }

  // Gets the value of control with name 'dateOfBirth'.
  get dateOfBirth() {
    return this.registerFormGroup.get('dateOfBirth');
  }

  // Called when Save button clicked from the html page.
  createEmployee(): void {
    this.submitted = true;

    if (this.registerFormGroup.invalid) {
      return;
    }

    this.addEmployee.id = '00000000-0000-0000-0000-000000000000';

    this.addEmployee.employeeName = this.registerFormGroup.controls['name'].value
      ? this.registerFormGroup.controls['name'].value
      : '';
    this.addEmployee.employeeEmail = this.registerFormGroup.controls['email'].value
      ? this.registerFormGroup.controls['email'].value
      : '';
    this.addEmployee.employeePhone = this.registerFormGroup.controls['phone'].value
      ? this.registerFormGroup.controls['phone'].value
      : '';
    this.addEmployee.employeeDateOfBirth = this.registerFormGroup.controls[
      'dateOfBirth'
    ].value
      ? this.registerFormGroup.controls['dateOfBirth'].value
      : '';

    this.employeeService.addEmployee(this.addEmployee).subscribe({
      next: (employee) => {
        this.router.navigate(['employees']);
      },
      error: (response) => {
        console.log(response);
      },
    });
  }
  //#endregion
}
