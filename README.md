#SAOnlineMart

SAOnlineMart is an e-commerce platform developed using ASP.NET Core MVC, designed to provide a seamless shopping experience with a South African flavor. This repository contains the complete source code and documentation for the project.

Table of Contents:
Introduction
Features
Technologies Used
Getting Started
Prerequisites
Installation
Database Setup
Running the Application
Project Structure
Usage
Acknowledgments

#Introduction
SAOnlineMart is an e-commerce website that allows users to browse and purchase products online. It features a comprehensive admin dashboard for managing products, orders, and users. The platform is built with ASP.NET Core MVC and Entity Framework Core, ensuring a robust and scalable architecture.

#Features
Product Management: Add, edit, and remove products from the catalog.
User Authentication: Secure login and registration with role-based access control.
Shopping Cart: Add products to the cart, update quantities, and proceed to checkout.
Order Management: Track pending and completed orders, with an option to mark orders as shipped.
Responsive Design: Fully responsive design that works on desktops, tablets, and mobile devices. -- Future Improvement
Admin Dashboard: A powerful dashboard for administrators to manage products, orders, and users.

#Technologies Used
ASP.NET Core MVC: Backend framework.
Entity Framework Core: ORM for database management.
Bootstrap: Frontend framework for responsive design.
jQuery: JavaScript library for AJAX and DOM manipulation.
SQL Server: Database management system.
Identity: For authentication and authorization. -- Future improvement

#Getting Started
#Prerequisites
Before you begin, ensure you have met the following requirements:

.NET 6.0 SDK or later installed on your machine.
SQL Server installed and running.
Visual Studio 2022 (recommended) or any other compatible IDE.

#Installation
Clone the repository:

git clone https://github.com/Done-Brand/SAOnlineMart.git
cd SAOnlineMart

#Restore the NuGet packages:

dotnet restore
Database Setup

#Update the connection string:

In appsettings.json, update the ConnectionStrings to point to your SQL Server instance:

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SAOnlineMart;Trusted_Connection=True;MultipleActiveResultSets=true"
}

#Apply migrations and seed the database:

dotnet ef database update

#Running the Application
To run the application locally:
dotnet run

Open your browser and navigate to https://localhost:3000.

Project Structure

SAOnlineMart/
├── Controllers/         # MVC controllers
├── Data/                # Database context and initial seed data
├── Models/              # Data models and view models
├── Views/               # Razor views
├── wwwroot/             # Static files (CSS, JS, images)
├── appsettings.json     # Configuration file
├── Program.cs           # Application entry point
├── Startup.cs           # Configuration of services and middleware
└── README.md            # Project documentation


#Usage
#Admin Dashboard
Navigate to /AdminDashboard/Index to access the admin features like managing products, orders, and users.

#User Features
Register or log in to start shopping.
Add products to your cart and proceed to checkout.
Track your orders through the user dashboard.

Acknowledgments
Thanks to the ASP.NET Core community for the tools and libraries used in this project.
Special thanks to Done-Brand for the development of this platform.
