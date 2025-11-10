# Clinic Management System ??

A comprehensive Windows Forms application built with **VB.NET** and **MySQL** for managing clinic operations including patients, appointments, staff, and billing.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![MySQL](https://img.shields.io/badge/MySQL-8.0-4479A1?logo=mysql&logoColor=white)
![Windows Forms](https://img.shields.io/badge/Windows%20Forms-blue?logo=windows)
![License](https://img.shields.io/badge/License-MIT-green)

## ?? Screenshots

### Dashboard
![Dashboard](docs/screenshots/dashboard.png)
*Real-time statistics and quick navigation*

### Patient Management
![Patients](docs/screenshots/patients.png)
*Complete CRUD operations with search functionality*

### Appointments
![Appointments](docs/screenshots/appointments.png)
*Schedule and manage appointments*

### Billing
![Billing](docs/screenshots/billing.png)
*Track payments and generate invoices*

## ? Features

### ?? Authentication & Security
- Staff login with role-based access control
- Secure session management
- Admin-only staff management
- Input validation and SQL injection prevention

### ?? Dashboard
- **Live Statistics Display**
  - Total Patients
  - Today's Appointments
  - Pending Bills
  - Total Revenue
- Real-time clock
- Auto-refresh capabilities

### ?? Patient Management (CRUD)
- Add, view, update, and delete patient records
- Real-time search by name or contact
- Store demographics: name, birthdate, gender, contact, address
- Input validation with visual feedback

### ?? Appointment Scheduling (CRUD)
- Schedule appointments with patients and doctors
- Track status: Scheduled, Completed, Cancelled
- Date/time validation
- Link to staff members
- View appointment history

### ????? Staff Management (CRUD - Admin Only)
- Manage doctors, nurses, receptionists, and admins
- Username and password management
- Contact information storage
- Role-based permissions

### ?? Billing System (CRUD)
- Create bills for appointments
- Track payment status (Paid/Unpaid)
- Revenue tracking
- View billing history

### ?? Reports & Analytics
- **4 Report Types:**
  1. Patients Report
  2. Appointments Report (date range)
  3. Revenue Report (financial summary)
  4. Staff Directory
- **Export to CSV** for Excel/Google Sheets
- Print-ready reports

### ? Advanced Features
- Comprehensive input validation
- Error logging to file
- Setup wizard for first-time use
- Modern, color-coded UI
- Confirmation dialogs
- Success/error notifications

## ?? Getting Started

### Prerequisites

- **Windows** 10/11
- **.NET 8.0 SDK** or later
- **MySQL Server** 8.0 or later
- **Visual Studio 2022** (Community Edition or higher)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/YOUR_USERNAME/clinic-management-system.git
   cd clinic-management-system
   ```

2. **Set up MySQL Database**
   
   Run the following SQL script to create the database:
   
   ```sql
   CREATE DATABASE clinicdb CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
   USE clinicdb;

   -- Create tables
   CREATE TABLE patients (
     patient_id INT AUTO_INCREMENT PRIMARY KEY,
     first_name VARCHAR(100) NOT NULL,
     last_name VARCHAR(100) NOT NULL,
     birthdate DATE,
     gender ENUM('Male','Female','Other'),
     contact VARCHAR(50),
     address TEXT,
     created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
   );

   CREATE TABLE staff (
     staff_id INT AUTO_INCREMENT PRIMARY KEY,
     username VARCHAR(50) UNIQUE,
     password_hash VARCHAR(255),
     full_name VARCHAR(150),
     role ENUM('Doctor','Nurse','Admin','Receptionist'),
     contact VARCHAR(50),
     created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
   );

   CREATE TABLE appointments (
     appointment_id INT AUTO_INCREMENT PRIMARY KEY,
     patient_id INT NOT NULL,
     staff_id INT NOT NULL,
     appt_datetime DATETIME NOT NULL,
     reason TEXT,
     status ENUM('Scheduled','Completed','Cancelled') DEFAULT 'Scheduled',
     created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
     FOREIGN KEY (patient_id) REFERENCES patients(patient_id) ON DELETE CASCADE,
     FOREIGN KEY (staff_id) REFERENCES staff(staff_id) ON DELETE SET NULL
   );

   CREATE TABLE bills (
     bill_id INT AUTO_INCREMENT PRIMARY KEY,
     appointment_id INT,
     amount DECIMAL(10,2),
     paid BOOLEAN DEFAULT FALSE,
     created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
     FOREIGN KEY (appointment_id) REFERENCES appointments(appointment_id) ON DELETE SET NULL
   );
   ```

3. **Configure Database Connection**
   
   Open `modDB.vb` and update the connection string if needed:
   ```vb
   Public conn As New MySqlConnection("server=localhost;user id=root;password=YOUR_PASSWORD;database=clinicdb")
   ```

4. **Open in Visual Studio**
   - Open `CRUD APP Clinic Management System.sln`
   - Restore NuGet packages (right-click solution ? Restore NuGet Packages)
   - Build the solution (Ctrl+Shift+B)

5. **Run the Application**
   - Press F5 or click Start
   - The setup wizard will guide you through first-time setup
   - Create an admin user when prompted

### First Login

**Default Credentials** (after running setup wizard):
- Username: `admin`
- Password: `admin123`

**?? Important:** Change the default password immediately after first login!

## ?? Usage Guide

### Adding Sample Data

**Method 1: Setup Wizard**
1. Run the application
2. Click "Create Sample Data" in the setup wizard

**Method 2: SQL Script**
```bash
mysql -u root -p clinicdb < SampleData.sql
```

### Daily Operations

1. **Login** with your staff credentials
2. **Dashboard** shows real-time statistics
3. **Manage Patients** - Add new patients or search existing
4. **Schedule Appointments** - Book appointments with doctors
5. **Process Billing** - Create bills and track payments
6. **Generate Reports** - Export data for analysis
7. **Logout** when done

## ??? Project Structure

```
CRUD APP Clinic Management System/
??? modDB.vb                    # Database connection
??? modCurrentUser.vb           # Session management
??? modValidation.vb            # Input validation helpers
??? modErrorLogger.vb           # Error logging
??? frmLogin.vb                 # Login form
??? frmSetup.vb                 # Setup wizard
??? frmMain.vb                  # Dashboard
??? frmPatients.vb              # Patient management
??? frmAppointments.vb          # Appointment scheduling
??? frmStaff.vb                 # Staff management
??? frmBilling.vb               # Billing system
??? frmReports.vb               # Reports & analytics
??? SampleData.sql              # Test data
??? README.md                   # Documentation
??? docs/                       # Additional documentation
```

## ?? Technologies Used

| Technology | Version | Purpose |
|------------|---------|---------|
| Visual Basic .NET | 17.0+ | Programming Language |
| .NET Framework | 8.0 | Application Framework |
| Windows Forms | - | UI Framework |
| MySQL | 8.0+ | Database |
| MySqlConnector | 2.4.0 | Database Driver |

## ?? Security Notes

**This is an educational/demonstration project.** For production use:

- ? Implement password hashing (BCrypt, Argon2)
- ? Use SSL/TLS for database connections
- ? Add session timeout
- ? Implement audit logging
- ? Enable HTTPS
- ? Add data encryption at rest
- ? Follow OWASP security guidelines
- ? Implement HIPAA compliance (if handling real patient data)

## ?? Troubleshooting

### Connection Failed
- Ensure MySQL service is running
- Verify connection credentials in `modDB.vb`
- Check firewall settings (port 3306)

### Login Failed
- Run the setup wizard to create admin user
- Verify staff table has records: `SELECT * FROM staff;`

### No Appointments in Billing
- Create appointments first in Appointments module
- Appointments will appear in billing dropdown

### Error Logs
- Check `error_log.txt` in application directory
- Contains detailed error information with timestamps

## ?? Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## ?? License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## ????? Author

**Your Name**
- GitHub: [@YOUR_USERNAME](https://github.com/YOUR_USERNAME)
- LinkedIn: [Your LinkedIn](https://linkedin.com/in/YOUR_PROFILE)

## ?? Acknowledgments

- Built as a learning project for database management and CRUD operations
- Inspired by real-world clinic management needs
- Thanks to the VB.NET and MySQL communities

## ?? Support

For questions or issues:
1. Check the [Issues](https://github.com/YOUR_USERNAME/clinic-management-system/issues) page
2. Read the documentation in the `docs/` folder
3. Review `error_log.txt` for detailed error information

## ?? Roadmap

- [ ] Add PDF report generation
- [ ] Implement email notifications
- [ ] Add SMS appointment reminders
- [ ] Create dashboard charts and graphs
- [ ] Add calendar view for appointments
- [ ] Implement password reset functionality
- [ ] Add two-factor authentication
- [ ] Create mobile companion app
- [ ] Add prescription management
- [ ] Implement medical records

---

**? If you find this project helpful, please give it a star!**

*Built with ?? using Visual Basic .NET and MySQL*
