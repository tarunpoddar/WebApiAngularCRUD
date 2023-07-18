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

Here are few screenshots to the project in action.

Home - Details 

![image](https://github.com/tarunpoddar/WebApiAngularCRUD/assets/62183124/b254d778-d48d-4a1d-8847-b5015eaa6d43)

Registration -

![image](https://github.com/tarunpoddar/WebApiAngularCRUD/assets/62183124/022187cd-a2ae-474d-918a-9a0fdf4cc2ae)

![image](https://github.com/tarunpoddar/WebApiAngularCRUD/assets/62183124/f35c5c1f-6af9-4b72-a920-efff6d170ad5)

Update -

![image](https://github.com/tarunpoddar/WebApiAngularCRUD/assets/62183124/cd24a206-4395-45ea-aadf-dad1884a2776)

Delete - Using Pop-up 

![image](https://github.com/tarunpoddar/WebApiAngularCRUD/assets/62183124/17af6672-f811-4233-9a40-864f9a46e071)

Git Repository Commits

![image](https://github.com/tarunpoddar/WebApiAngularCRUD/assets/62183124/becf717c-caa0-482a-9fde-d17891430b5e)


