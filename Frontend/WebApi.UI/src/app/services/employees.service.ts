import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { baseUrl, myHeaders } from '../app.global';
import { Employee } from '../models/employee.model';

@Injectable({
  providedIn: 'root',
})
export class EmployeesService {
  baseApiUrl: string = baseUrl;

  constructor(private http: HttpClient) { }

  getAllEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.baseApiUrl + 'api/employees', {
      headers: myHeaders,
    });
  }

  getEmployee(id: string): Observable<Employee> {
    return this.http.get<Employee>(this.baseApiUrl + 'api/employees/' + id, {
      headers: myHeaders,
    });
  }

  addEmployee(employee: Employee): Observable<Employee> {
    return this.http.post<Employee>(
      this.baseApiUrl + 'api/employees',
      employee,
      {
        headers: myHeaders,
      }
    );
  }

  updateEmployee(employee: Employee): Observable<Employee> {
    return this.http.put<Employee>(
      this.baseApiUrl + 'api/employees/' + employee.id,
      employee,
      {
        headers: myHeaders,
      }
    );
  }

  deleteEmployee(id: string): Observable<Employee> {
    return this.http.delete<Employee>(this.baseApiUrl + 'api/employees/' + id, {
      headers: myHeaders,
    });
  }
}
