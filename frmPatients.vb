Imports MySqlConnector

Public Class frmPatients
    Private Sub frmPatients_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPatients()
        cboGender.SelectedIndex = 0
    End Sub

    ' Clear error highlighting when user starts typing
    Private Sub txtFirstName_TextChanged(sender As Object, e As EventArgs) Handles txtFirstName.TextChanged
        ClearError(txtFirstName)
    End Sub

    Private Sub txtLastName_TextChanged(sender As Object, e As EventArgs) Handles txtLastName.TextChanged
        ClearError(txtLastName)
    End Sub

    Private Sub txtContact_TextChanged(sender As Object, e As EventArgs) Handles txtContact.TextChanged
        ClearError(txtContact)
    End Sub

    Private Sub LoadPatients()
        Try
            OpenConnection()
            Dim query As String = "SELECT * FROM patients ORDER BY patient_id DESC"
            Dim adapter As New MySqlDataAdapter(query, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvPatients.DataSource = dt
            CloseConnection()
        Catch ex As Exception
            ShowErrorWithLog(ex, "Error loading patients")
            CloseConnection()
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ' Validation
        If String.IsNullOrWhiteSpace(txtFirstName.Text) Then
            ShowValidationError("Please enter first name.")
            HighlightError(txtFirstName)
            Return
        End If

        If Not IsValidName(txtFirstName.Text) Then
            ShowValidationError("First name should contain only letters.")
            HighlightError(txtFirstName)
            Return
        End If

        If String.IsNullOrWhiteSpace(txtLastName.Text) Then
            ShowValidationError("Please enter last name.")
            HighlightError(txtLastName)
            Return
        End If

        If Not IsValidName(txtLastName.Text) Then
            ShowValidationError("Last name should contain only letters.")
            HighlightError(txtLastName)
            Return
        End If

        If Not IsValidBirthdate(dtpBirthdate.Value) Then
            ShowValidationError("Please enter a valid birthdate.")
            Return
        End If

        If Not String.IsNullOrWhiteSpace(txtContact.Text) AndAlso Not IsValidPhone(txtContact.Text) Then
            ShowValidationError("Please enter a valid phone number (10+ digits).")
            HighlightError(txtContact)
            Return
        End If

        Try
            OpenConnection()
            Dim query As String = "INSERT INTO patients (first_name, last_name, birthdate, gender, contact, address) " &
                                  "VALUES (@fname, @lname, @bdate, @gender, @contact, @address)"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@fname", txtFirstName.Text.Trim())
            cmd.Parameters.AddWithValue("@lname", txtLastName.Text.Trim())
            cmd.Parameters.AddWithValue("@bdate", dtpBirthdate.Value.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("@gender", cboGender.Text)
            cmd.Parameters.AddWithValue("@contact", txtContact.Text.Trim())
            cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim())
            cmd.ExecuteNonQuery()
            CloseConnection()

            MessageBox.Show("Patient added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LogInfo($"Patient added: {txtFirstName.Text} {txtLastName.Text}")
            LoadPatients()
            ClearFields()
        Catch ex As Exception
            ShowErrorWithLog(ex, "Error adding patient", $"Name: {txtFirstName.Text} {txtLastName.Text}")
            CloseConnection()
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If String.IsNullOrWhiteSpace(txtPatientId.Text) Then
            MessageBox.Show("Please select a patient to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            OpenConnection()
            Dim query As String = "UPDATE patients SET first_name=@fname, last_name=@lname, birthdate=@bdate, " &
                                  "gender=@gender, contact=@contact, address=@address WHERE patient_id=@id"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@id", CInt(txtPatientId.Text))
            cmd.Parameters.AddWithValue("@fname", txtFirstName.Text)
            cmd.Parameters.AddWithValue("@lname", txtLastName.Text)
            cmd.Parameters.AddWithValue("@bdate", dtpBirthdate.Value.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("@gender", cboGender.Text)
            cmd.Parameters.AddWithValue("@contact", txtContact.Text)
            cmd.Parameters.AddWithValue("@address", txtAddress.Text)
            cmd.ExecuteNonQuery()
            CloseConnection()

            MessageBox.Show("Patient updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadPatients()
            ClearFields()
        Catch ex As Exception
            MessageBox.Show("Error updating patient: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CloseConnection()
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrWhiteSpace(txtPatientId.Text) Then
            MessageBox.Show("Please select a patient to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("Are you sure you want to delete this patient?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                OpenConnection()
                Dim query As String = "DELETE FROM patients WHERE patient_id=@id"
                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", CInt(txtPatientId.Text))
                cmd.ExecuteNonQuery()
                CloseConnection()

                MessageBox.Show("Patient deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadPatients()
                ClearFields()
            Catch ex As Exception
                MessageBox.Show("Error deleting patient: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                CloseConnection()
            End Try
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        txtPatientId.Clear()
        txtFirstName.Clear()
        txtLastName.Clear()
        dtpBirthdate.Value = DateTime.Now
        cboGender.SelectedIndex = 0
        txtContact.Clear()
        txtAddress.Clear()
    End Sub

    Private Sub dgvPatients_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPatients.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvPatients.Rows(e.RowIndex)
            txtPatientId.Text = row.Cells("patient_id").Value.ToString()
            txtFirstName.Text = row.Cells("first_name").Value.ToString()
            txtLastName.Text = row.Cells("last_name").Value.ToString()
            If Not IsDBNull(row.Cells("birthdate").Value) Then
                dtpBirthdate.Value = Convert.ToDateTime(row.Cells("birthdate").Value)
            End If
            cboGender.Text = row.Cells("gender").Value.ToString()
            txtContact.Text = If(IsDBNull(row.Cells("contact").Value), "", row.Cells("contact").Value.ToString())
            txtAddress.Text = If(IsDBNull(row.Cells("address").Value), "", row.Cells("address").Value.ToString())
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            OpenConnection()
            Dim query As String = "SELECT * FROM patients WHERE first_name LIKE @search OR last_name LIKE @search OR contact LIKE @search"
            Dim adapter As New MySqlDataAdapter(query, conn)
            adapter.SelectCommand.Parameters.AddWithValue("@search", "%" & txtSearch.Text & "%")
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvPatients.DataSource = dt
            CloseConnection()
        Catch ex As Exception
            CloseConnection()
        End Try
    End Sub

    Private Sub ShowValidationError(message As String)
        MessageBox.Show(message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    Private Sub HighlightError(control As Control)
        control.BackColor = Color.LightCoral
        control.Focus()
    End Sub

    Private Sub ClearError(control As Control)
        control.BackColor = Color.White
    End Sub

    Private Function IsValidName(name As String) As Boolean
        Return System.Text.RegularExpressions.Regex.IsMatch(name, "^[a-zA-Z]+$")
    End Function

    Private Function IsValidBirthdate(birthdate As DateTime) As Boolean
        ' Birthdate should be before today and not more than 150 years ago
        Dim minDate As New DateTime(DateTime.Now.Year - 150, 1, 1)
        Return birthdate < DateTime.Now AndAlso birthdate > minDate
    End Function

    Private Function IsValidPhone(phone As String) As Boolean
        Return System.Text.RegularExpressions.Regex.IsMatch(phone, "^\d{10,15}$")
    End Function
End Class
