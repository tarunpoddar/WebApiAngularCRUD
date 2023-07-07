import { Component, OnInit } from '@angular/core';
import { EmployeesService } from './services/employees.service';
import { timer } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title: string = 'WebApi.UI';
  isOnline: boolean = true;

  constructor(private employeeService: EmployeesService) {}

  ngOnInit(): void {
    const source = timer(1000, 5000);
    const subscribe = source.subscribe((val) => {
      this.employeeService.getAllEmployees().subscribe({
        next: (employees) => {
          this.isOnline = true;
        },
        error: (response) => {
          this.isOnline = false;
        },
      });
    });
  }
}
