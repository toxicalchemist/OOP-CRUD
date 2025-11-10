Imports MySqlConnector

Public Class frmSetup
    Private Sub frmSetup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckDatabaseSetup()
    End Sub

    Private Sub CheckDatabaseSetup()
        Try
            OpenConnection()

            ' Check if tables exist
            Dim cmd As New MySqlCommand("SHOW TABLES", conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            Dim tables As New List(Of String)
            While reader.Read()
                tables.Add(reader(0).ToString())
            End While
            reader.Close()

            lblStatus.Text = $"Found {tables.Count} tables in database."

            ' Check if staff exists
            If tables.Contains("staff") Then
                Dim cmdCount As New MySqlCommand("SELECT COUNT(*) FROM staff", conn)
                Dim staffCount As Integer = Convert.ToInt32(cmdCount.ExecuteScalar())
                lblStaffCount.Text = $"Staff records: {staffCount}"

                If staffCount = 0 Then
                    lblStaffCount.ForeColor = Color.Red
                    btnCreateAdmin.Enabled = True
                Else
                    lblStaffCount.ForeColor = Color.Green
                    btnCreateAdmin.Enabled = False
                End If
            End If

            CloseConnection()
        Catch ex As Exception
            MessageBox.Show("Error checking database: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CloseConnection()
        End Try
    End Sub

    Private Sub btnCreateAdmin_Click(sender As Object, e As EventArgs) Handles btnCreateAdmin.Click
        Try
            OpenConnection()

            ' Create default admin user
            Dim query As String = "INSERT INTO staff (username, password_hash, full_name, role, contact) VALUES (@user, @pass, @name, @role, @contact)"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@user", "admin")
            cmd.Parameters.AddWithValue("@pass", "admin123")
            cmd.Parameters.AddWithValue("@name", "System Administrator")
            cmd.Parameters.AddWithValue("@role", "Admin")
            cmd.Parameters.AddWithValue("@contact", "555-0000")
            cmd.ExecuteNonQuery()

            CloseConnection()

            MessageBox.Show("Admin user created successfully!" & vbCrLf & vbCrLf & "Username: admin" & vbCrLf & "Password: admin123", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CheckDatabaseSetup()
        Catch ex As Exception
            MessageBox.Show("Error creating admin: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CloseConnection()
        End Try
    End Sub

    Private Sub btnCreateSample_Click(sender As Object, e As EventArgs) Handles btnCreateSample.Click
        If MessageBox.Show("This will add sample staff, patients, appointments, and bills. Continue?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                OpenConnection()

                ' Insert sample staff
                ExecuteInsert("INSERT INTO staff (username, password_hash, full_name, role, contact) VALUES ('doctor1', 'doc123', 'Dr. Michael Chen', 'Doctor', '555-0102')")
                ExecuteInsert("INSERT INTO staff (username, password_hash, full_name, role, contact) VALUES ('doctor2', 'doc123', 'Dr. Emily Rodriguez', 'Doctor', '555-0103')")
                ExecuteInsert("INSERT INTO staff (username, password_hash, full_name, role, contact) VALUES ('nurse1', 'nurse123', 'Maria Santos', 'Nurse', '555-0104')")

                ' Insert sample patients
                ExecuteInsert("INSERT INTO patients (first_name, last_name, birthdate, gender, contact, address) VALUES ('James', 'Wilson', '1985-03-15', 'Male', '555-1001', '123 Main St')")
                ExecuteInsert("INSERT INTO patients (first_name, last_name, birthdate, gender, contact, address) VALUES ('Lisa', 'Anderson', '1990-07-22', 'Female', '555-1002', '456 Oak Ave')")
                ExecuteInsert("INSERT INTO patients (first_name, last_name, birthdate, gender, contact, address) VALUES ('Robert', 'Martinez', '1978-11-08', 'Male', '555-1003', '789 Pine Rd')")

                ' Insert sample appointments
                ExecuteInsert("INSERT INTO appointments (patient_id, staff_id, appt_datetime, reason, status) VALUES (1, 2, '2025-01-25 09:00:00', 'Annual checkup', 'Scheduled')")
                ExecuteInsert("INSERT INTO appointments (patient_id, staff_id, appt_datetime, reason, status) VALUES (2, 3, '2025-01-25 10:30:00', 'Follow-up', 'Scheduled')")

                CloseConnection()

                MessageBox.Show("Sample data created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CheckDatabaseSetup()
            Catch ex As Exception
                MessageBox.Show("Error creating sample data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                CloseConnection()
            End Try
        End If
    End Sub

    Private Sub ExecuteInsert(query As String)
        Dim cmd As New MySqlCommand(query, conn)
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
