# Employee Application

An application for viewing, adding, updating, and deleting employee data

## Running in Development
- Clone the repository
- Open the solution in Visual Studio
- Build the app. ```dotnet build``` when using cmd or psh
- Run the app (Ctrl + F5)

Problems running the app? See notes below

### Notes:
- Ensure the database engine is running. In my case, I used a docker image for [MS SQL Server](https://hub.docker.com/_/microsoft-mssql-server/) with the configuration from the instructions
- Ensure the cookies are cleared for the app. In the browser F12 > Application > Cookies > Right click on https://localhost:[PORT] > Clear.

 
