# Clinic Management System

A comprehensive Windows Forms application built with VB.NET and MySQL for managing clinic operations.

## Features

- **Patient Management** - Add, view, update, and delete patient records with search functionality
- **Appointment Scheduling** - Schedule and manage appointments with status tracking
- **Staff Management** - Manage clinic staff with role-based access control (Admin only)
- **Billing System** - Track payments and generate invoices
- **Reports & Analytics** - Generate and export reports to CSV
- **Dashboard** - Real-time statistics display with live updates

## Technologies

- Visual Basic .NET 8.0
- Windows Forms
- MySQL 8.0+
- MySqlConnector 2.4.0

## Setup

1. Install MySQL and create database using `SampleData.sql`
2. Update connection string in `modDB.vb`
3. Open solution in Visual Studio 2022
4. Build and run (F5)
5. Use setup wizard to create admin account

**Default Login**: admin / admin123

## Project Structure

- `modDB.vb` - Database connection
- `frmLogin.vb` - Login form
- `frmMain.vb` - Dashboard
- `frmPatients.vb` - Patient management
- `frmAppointments.vb` - Appointments
- `frmStaff.vb` - Staff management
- `frmBilling.vb` - Billing
- `frmReports.vb` - Reports

## License

MIT License - See LICENSE file for details
