# WebApi_Angular_Crud

1.Backend
* The backend application is a .NET 6 WEB API which acts as a server for the front end client application.
* The Employees controller has all the CRUD API's with server side validation.
* It uses EntityFrameworkCore(v7) to talk to the SQL Server database.
* Database is updated using dotnet migrations.
* Implemented AutoMapper for better reliability.

2.Frontend
* The fronend application is an Angular(v15) application written in HTML, CSS and TypeScript.
* All types of validations are applied using ReactiveFormsModule validation.
* It shows the usage of the pop-up while deleting an employee using NgxAwesomePopupModule.
* Used rxjs for handling disconnect cases using timer and consume web api's using Observable.
* Used Pipes to format data before preview.

