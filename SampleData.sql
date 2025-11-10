-- Sample data for testing Clinic Management System

USE clinicdb;

-- Insert sample staff (passwords are plain text for testing - should be hashed in production)
INSERT INTO staff (username, password_hash, full_name, role, contact) VALUES
('admin', 'admin123', 'Dr. Sarah Johnson', 'Admin', '555-0101'),
('doctor1', 'doc123', 'Dr. Michael Chen', 'Doctor', '555-0102'),
('doctor2', 'doc123', 'Dr. Emily Rodriguez', 'Doctor', '555-0103'),
('nurse1', 'nurse123', 'Maria Santos', 'Nurse', '555-0104'),
('reception', 'recep123', 'John Smith', 'Receptionist', '555-0105');

-- Insert sample patients
INSERT INTO patients (first_name, last_name, birthdate, gender, contact, address) VALUES
('James', 'Wilson', '1985-03-15', 'Male', '555-1001', '123 Main St, City, State'),
('Lisa', 'Anderson', '1990-07-22', 'Female', '555-1002', '456 Oak Ave, City, State'),
('Robert', 'Martinez', '1978-11-08', 'Male', '555-1003', '789 Pine Rd, City, State'),
('Jennifer', 'Taylor', '1995-05-30', 'Female', '555-1004', '321 Elm St, City, State'),
('David', 'Brown', '1982-09-12', 'Male', '555-1005', '654 Maple Dr, City, State');

-- Insert sample appointments
INSERT INTO appointments (patient_id, staff_id, appt_datetime, reason, status) VALUES
(1, 2, '2025-01-20 09:00:00', 'Annual checkup', 'Completed'),
(2, 3, '2025-01-20 10:30:00', 'Follow-up consultation', 'Completed'),
(3, 2, '2025-01-21 14:00:00', 'Blood pressure check', 'Scheduled'),
(4, 3, '2025-01-22 11:00:00', 'Flu symptoms', 'Scheduled'),
(5, 2, '2025-01-23 15:30:00', 'Diabetes management', 'Scheduled');

-- Insert sample bills
INSERT INTO bills (appointment_id, amount, paid) VALUES
(1, 150.00, TRUE),
(2, 200.00, FALSE);

-- Display all data
SELECT 'Staff Members:' AS Info;
SELECT * FROM staff;

SELECT 'Patients:' AS Info;
SELECT * FROM patients;

SELECT 'Appointments:' AS Info;
SELECT * FROM appointments;

SELECT 'Bills:' AS Info;
SELECT * FROM bills;
