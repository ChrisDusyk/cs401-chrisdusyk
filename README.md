# cs401-200268705
CS401 Repo for Chris Dusyk

## Folder Structure
This repo places the source code for each application in the /src/ folder. PlatformTestApplication contains the code for the code that tests the platform research task, in this case AWS Aurora DBaaS. AzureWebAppComponent contains the code for the web application that will be used in the final project for the class.

## PlatformTestApplication
This is a basic ASP.NET application that will display data as a demo for AWS Aurora.

# Needed Components/Applications

## Visual Studio 2015 Community
The .NET solutions can be viewed and worked with using Microsoft Visual Studio 2015 Community Edition, which can be downloaded for free [here](https://www.visualstudio.com/).

### Setting Up a Project For MySQL and .NET
To use the MySQL Connector with a .NET project, you will need to add references to the Connector DLL's. You can do this by right-clicking on the References item in the Solution Explorer, browsing to {C:\Program Files (x86)}\MySQL\Connector.NET 6.9\Assemblies\v4.5 and adding the MySql.Data.dll. If you're using Entity Framework, also add the MySql.Data.EF6.dll file as well.

### MySQL Connector
The MySQL Connector for .NET (ADO.NET driver) can be downloaded at [here](https://dev.mysql.com/downloads/connector/net/6.9.html). This is needed to connect the apps to AWS Aurora. If you're planning on doing a fair bit of work with MySQL and Visual Studio, it's work installing the full MySQL for Windows tooling from [here](http://dev.mysql.com/downloads/installer/). It will give you options for what you wish to install, so install what you need.