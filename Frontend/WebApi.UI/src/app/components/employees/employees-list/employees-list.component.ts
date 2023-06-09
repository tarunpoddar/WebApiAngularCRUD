import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ConfirmBoxEvokeService } from '@costlydeveloper/ngx-awesome-popup';
import { Employee } from 'src/app/models/employee.model';
import { EmployeesService } from 'src/app/services/employees.service';
import { timer } from 'rxjs';

@Component({
  selector: 'app-employees-list',
  templateUrl: './employees-list.component.html',
  styleUrls: ['./employees-list.component.css'],
})

// Class EmployeesListComponent which handles logic of showing list of all employees.
export class EmployeesListComponent implements OnInit {
  employees: Employee[] = [];
  status: string = 'Loading...';
  isMessageVisible: boolean = false;
  constructor(
    private employeeService: EmployeesService,
    private router: Router,
    private confirmBoxEvokeService: ConfirmBoxEvokeService
  ) {}

  // Calls when the component inits for first time.
  ngOnInit(): void {
    this.isMessageVisible = true;
    this.employeeService.getAllEmployees().subscribe({
      next: (employees) => {
        this.employees = employees;
        if (this.employees.length == 0) {
          this.status = 'No records in database.';
        } else {
          this.isMessageVisible = false;
        }
      },
      error: (response) => {
        console.log(response);
      },
    });
  }

  // Deletes the employee.
  deleteEmployee(id: string) {
    this.employeeService.getEmployee(id).subscribe({
      next: (employee) => {
        // Show a confirmation dialog to user.
        const subscription = this.confirmBoxEvokeService
          .danger(
            'Are you sure?',
            `Delete the employee: ${employee.employeeName}`,
            'Yes',
            'No'
          )
          .subscribe((dialogResult) => {
            if (dialogResult.success) {
              // Call the delete employee API.
              this.employeeService.deleteEmployee(id).subscribe({
                next: (employee) => {
                  this.ngOnInit();
                  this.router.navigate(['employees']);
                  console.log(`Deleted employee: ${employee.employeeName} `);
                },
                error: (response) => {
                  console.log(response);
                },
              });
            }
          });
      },
    });
  }
}
