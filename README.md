# Coding Test for DAL Technologies: Pschool

A web application for Pschool, which provides two views, one for the parents and the other one for the students, the user can add, edit or delete either one of the parents and students. A student always has a parent.
This project was completed using ASP.net, PostgreSQL with Entity Framework Core and Blazor Webassembly. It has been developed on MacOS so the database uses PostgreSQL.

## Installation
### Pre-requisites
- dotnet
- dotnet-ef
- postgresql

### Build
Coming from Git
```bash
git clone git@github.com:alexandre-van/codingtest-Pschool-V1.git Pschool
```
Or unzip the file and
```bash
cd Pschool
dotnet restore
dotnet build
cd Pschool.API
```
___________________________
Make sure postgresql is running and depending on your postgres user and password, you can change the values
in Pschool.API/appsettings.json at line 3
```c#
{
  "ConnectionStrings": {
    "DefaultConnectionString": "Host=localhost;Database=pschooldb;Username=<YOUR_USERNAME>;Password=<YOUR_PASSWORD>"
  },
```
OR
You can create a new postgresql user
```bash
psql postgres
CREATE ROLE pschool WITH LOGIN PASSWORD 'daltech';
ALTER ROLE pschool CREATEDB;
\q
```
___________________________
As it is a development project we would have to trust the self-signed https certs
```bash
dotnet dev-certs https --trust
```

Make sure dotnet-ef package is installed before
```bash
dotnet ef database update
dotnet run --launch-profile https
```
Create another terminal instance
```bash
cd Pschool.Client
dotnet run --launch-profile https
```
You can copy and paste this on your browser to reach the website
```bash
https://localhost:7113
```
