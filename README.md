# Coding Test for DAL Technologies: Pschool

A web application for Pschool, which provides two views, one for the parents and the other one for the students, the user can add, edit or delete either one of the parents and students. A student always has a parent.
This project was completed using ASP.net, PostgreSQL with Entity Framework Core and Blazor Webassembly. It has been developed on MacOS so the database uses PostgreSQL.

## Table of Contents
- [Installation](#installation)
- [Usage](#usage)
- [Features](#features)

## Installation
### Pre-requisites
- dotnet
- postgresql

### Build
Coming from Git
```bash
git clone git@github.com:alexandre-van/codingtest-Pschool-V1.git Pschool
```
dotnet run --launch-profile https
Or unzip the file and
```bash
cd Pschool
dotnet restore
dotnet build
cd Pschool.API
dotnet ef database update
dotnet run --launch-profile https
```
Create another terminal instance
```bash
cd Pschool.Client
dotnet run --launch-profile https
```
