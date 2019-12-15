# CatFactory.TypeScript ==^^==

## What Is CatFactory?

CatFactory is a scaffolding engine for .NET Core built with C#.

## How does it Works?

The concept behind CatFactory is to import an existing database from SQL Server instance and then to scaffold a target technology.

We can also replace the database from SQL Server instance with an in-memory database.

The flow to import an existing database is:

1. Create Database Factory
2. Import Database
3. Create instance of Project (Entity Framework Core, Dapper, etc)
4. Build Features (One feature per schema)
5. Scaffold objects, these methods read all objects from database and create instances for code builders

Currently, the following technologies are supported:

+ [`Entity Framework Core`](https://github.com/hherzl/CatFactory.EntityFrameworkCore)
+ [`ASP.NET Core`](https://github.com/hherzl/CatFactory.AspNetCore)
+ [`Dapper`](https://github.com/hherzl/CatFactory.Dapper)

This package is the core for child packages, additional packages have created with this naming convention: CatFactory.**PackageName**.

* CatFactory.SqlServer
* CatFactory.NetCore
* CatFactory.EntityFrameworkCore
* CatFactory.AspNetCore
* CatFactory.Dapper

## Donation

You can make a donation via PayPal using this link: [![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=XB49JFNSMGY6U&item_name=CatFactory&currency_code=USD&source=url)

Thanks for your help! ==^^==

## Roadmap

There will be a lot of improvements for CatFactory on road:

* Scaffolding Services Layer
* Dapper Integration for ASP.NET Core
* MD files
* Scaffolding C# Client for ASP.NET Web API
* Scaffolding Unit Tests for ASP.NET Core
* Scaffolding Integration Tests for ASP.NET Core
* Scaffolding Angular

## Concepts behind CatFactory

    Database Type
    Project Selection
    Event Handlers to Scaffold
    Database Object Model
    Import Bag

Read more on: [`Concepts behind CatFactory`](https://github.com/hherzl/CatFactory/wiki/Concepts-behind-CatFactory)

## Packages

    CatFactory
    CatFactory.SqlServer
    CatFactory.PostgreSql
    CatFactory.NetCore
    CatFactory.EntityFrameworkCore
    CatFactory.AspNetCore
    CatFactory.Dapper
    CatFactory.TypeScript

Read more on: [`Packages Features Chart`](https://github.com/hherzl/CatFactory/wiki/Packages-Features-Chart)

## History

In 2005 year, I was on my college days and I worked on my final project that included a lot of tables, for those days C# didn't have automatic properties also I worked on store procedures that included a lot of columns, I thought if there was a way to generate all that code because it was repetitive and I wasted time in wrote a lot of code.

In 2006 beggining I've worked for a company and I worked in a prototype to generate code but I didn't have experience and I was a junior developer, so I developed a version in WebForms that didn't allow to save the structure ha,ha,ha that project it was my first project in C# because I came from VB world but I bought a book about Web Services in DotNet and that book used C# code, that was new for me but it got me a very important idea, learn C# and I wrote all first code generation form in C#.

Later, there was a prototype of Entity for SQL, the grandfather of entity framework and I develop a simple ORM because I had table class and other classes such as Column, so after of reviewed Entity for SQL I decided to add the logic to read database and provide a simple way to read the database also of code generation.

In 2008 I built the first ORM based on my code generation engine, in that time it was called F4N1, I worked on an ORM must endure different databases engines such as SQL Server, Sybase and Oracle; so I generated a lot of classes with that engine, for that time the automated unit tests did not exist, I had a webform page that generated that code ha,ha,ha I know it was ugly and crappy but in that time that was my knowledge allowed me.

In 2011 I worked on a demo for a person that worked in his company and that person used another tool for code generation, so my code generation engine wasn't use for his work.

In 2012 I worked for a company needed to rebuilt all system with new technologies (ASP.NET MVC and Entity Framework) so I invested time about MVC and EF learning but as usual, there isn't time for that ha,ha,ha and again my code generation it wasn't considered for that upgrade =(

In 2014, I thought to make a nuget package to my code generation but in those days I didn't have the focus to accomplish that feature and always I used my code generation as a private tool, in some cases I shared my tool with some coworkers to generate code and reduce the time for code writing.

In 2016, I decided to create a nuget package and integrates with EF Core, using all experience from 10 years ago :D Please remember that from the beginning I was continuing improve the way of code generation, my first code was a crap but with the timeline I've improved the design and naming for objects.

Why I named CatFactory? It was I had a cat, her name was Mindy and that cat had manny kittens (sons), so the basic idea it was the code generation engine generates the code as fast Mindy provided kittens ha,ha,ha

## Trivia

+ The name for this framework it was F4N1 before than CatFactory
+ Framework's name is related to kitties
+ Import logic uses sp_help stored procedure to retrieve the database object's definition, I learned that in my database course at college
+ Load mapping for entities with MEF, it's inspired in "OdeToCode" (Scott Allen) article for Entity Framework 6.x
+ Expose all settings in one class inside of project's definition is inspired on DevExpress settings for Web controls (Web Forms)
+ There are three alpha versions for CatFactory as reference for Street Fighter Alpha fighting game.
+ There will be two beta versions for CatFactory: Sun and Moon as reference for characters from The King of Fighters game: Kusanagi Kyo and Yagami Iori.

## Quick Starts

[`Scaffolding View Models with CatFactory`](https://www.codeproject.com/Tips/1164636/Scaffolding-View-Models-with-CatFactory)

[`Scaffolding Entity Framework Core 2 with CatFactory`](https://www.codeproject.com/Articles/1160615/Scaffolding-Entity-Framework-Core-with-CatFactory)

[`Scaffolding Dapper with CatFactory`](https://www.codeproject.com/Articles/1213355/Scaffolding-Dapper-with-CatFactory)

[`Scaffolding ASP.NET Core 2 with CatFactory`](https://www.codeproject.com/Tips/1229909/Scaffolding-ASP-NET-Core-with-CatFactory)
