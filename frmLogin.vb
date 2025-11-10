Imports MySqlConnector

Public Class frmLogin
    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Check if database has users
        CheckForUsers()
    End Sub

    Private Sub CheckForUsers()
        Try
            OpenConnection()
            Dim cmd As New MySqlCommand("SELECT COUNT(*) FROM staff", conn)
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            CloseConnection()

            If count = 0 Then
                ' Show setup button if no users exist
                Dim result = MessageBox.Show("No staff members found in database. Would you like to run the setup wizard?", "Setup Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    Dim setupForm As New frmSetup()
                    setupForm.ShowDialog()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Database check failed: " & ex.Message & vbCrLf & vbCrLf & "Please ensure the database and tables are created.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CloseConnection()
        End Try
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If String.IsNullOrWhiteSpace(txtUsername.Text) Or String.IsNullOrWhiteSpace(txtPassword.Text) Then
            MessageBox.Show("Please enter username and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            OpenConnection()
            Dim cmd As New MySqlCommand("SELECT staff_id, full_name, role FROM staff WHERE username=@user AND password_hash=@pass", conn)
            cmd.Parameters.AddWithValue("@user", txtUsername.Text)
            cmd.Parameters.AddWithValue("@pass", txtPassword.Text) ' In production, use proper password hashing!

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Dim staffId As Integer = Convert.ToInt32(reader("staff_id"))
                Dim fullName As String = reader("full_name").ToString()
                Dim role As String = reader("role").ToString()
                reader.Close()
                CloseConnection()

                ' Store login info globally
                CurrentUser.StaffId = staffId
                CurrentUser.FullName = fullName
                CurrentUser.Role = role

                ' Open main form
                Dim mainForm As New frmMain()
                mainForm.Show()
                Me.Hide()
            Else
                reader.Close()
                CloseConnection()
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CloseConnection()
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Application.Exit()
    End Sub
End Class
