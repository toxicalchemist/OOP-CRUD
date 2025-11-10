Imports MySqlConnector

Public Class frmAppointments
    Private Sub frmAppointments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPatients()
        LoadStaff()
        LoadAppointments()
        cboStatus.SelectedIndex = 0
    End Sub

    Private Sub LoadPatients()
        Try
            OpenConnection()
            Dim query As String = "SELECT patient_id, CONCAT(first_name, ' ', last_name) AS full_name FROM patients"
            Dim cmd As New MySqlCommand(query, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            
            cboPatient.Items.Clear()
            While reader.Read()
                cboPatient.Items.Add(New With {.Value = reader("patient_id"), .Text = reader("full_name").ToString()})
            End While
            cboPatient.DisplayMember = "Text"
            cboPatient.ValueMember = "Value"
            reader.Close()
            CloseConnection()
        Catch ex As Exception
            CloseConnection()
        End Try
    End Sub

    Private Sub LoadStaff()
        Try
            OpenConnection()
            Dim query As String = "SELECT staff_id, CONCAT(full_name, ' (', role, ')') AS display_name FROM staff WHERE role IN ('Doctor', 'Nurse')"
            Dim cmd As New MySqlCommand(query, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            
            cboStaff.Items.Clear()
            While reader.Read()
                cboStaff.Items.Add(New With {.Value = reader("staff_id"), .Text = reader("display_name").ToString()})
            End While
            cboStaff.DisplayMember = "Text"
            cboStaff.ValueMember = "Value"
            reader.Close()
            CloseConnection()
        Catch ex As Exception
            CloseConnection()
        End Try
    End Sub

    Private Sub LoadAppointments()
        Try
            OpenConnection()
            Dim query As String = "SELECT a.appointment_id, CONCAT(p.first_name, ' ', p.last_name) AS patient_name, " &
                                  "s.full_name AS staff_name, a.appt_datetime, a.reason, a.status " &
                                  "FROM appointments a " &
                                  "JOIN patients p ON a.patient_id = p.patient_id " &
                                  "JOIN staff s ON a.staff_id = s.staff_id " &
                                  "ORDER BY a.appt_datetime DESC"
            Dim adapter As New MySqlDataAdapter(query, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvAppointments.DataSource = dt
            CloseConnection()
        Catch ex As Exception
            MessageBox.Show("Error loading appointments: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CloseConnection()
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If cboPatient.SelectedIndex < 0 Or cboStaff.SelectedIndex < 0 Then
            MessageBox.Show("Please select patient and staff.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            OpenConnection()
            Dim query As String = "INSERT INTO appointments (patient_id, staff_id, appt_datetime, reason, status) " &
                                  "VALUES (@patient, @staff, @datetime, @reason, @status)"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@patient", CType(cboPatient.SelectedItem, Object).Value)
            cmd.Parameters.AddWithValue("@staff", CType(cboStaff.SelectedItem, Object).Value)
            cmd.Parameters.AddWithValue("@datetime", dtpDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss"))
            cmd.Parameters.AddWithValue("@reason", txtReason.Text)
            cmd.Parameters.AddWithValue("@status", cboStatus.Text)
            cmd.ExecuteNonQuery()
            CloseConnection()

            MessageBox.Show("Appointment added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadAppointments()
            ClearFields()
        Catch ex As Exception
            MessageBox.Show("Error adding appointment: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CloseConnection()
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If String.IsNullOrWhiteSpace(txtApptId.Text) Then
            MessageBox.Show("Please select an appointment to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            OpenConnection()
            Dim query As String = "UPDATE appointments SET patient_id=@patient, staff_id=@staff, appt_datetime=@datetime, " &
                                  "reason=@reason, status=@status WHERE appointment_id=@id"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@id", CInt(txtApptId.Text))
            cmd.Parameters.AddWithValue("@patient", CType(cboPatient.SelectedItem, Object).Value)
            cmd.Parameters.AddWithValue("@staff", CType(cboStaff.SelectedItem, Object).Value)
            cmd.Parameters.AddWithValue("@datetime", dtpDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss"))
            cmd.Parameters.AddWithValue("@reason", txtReason.Text)
            cmd.Parameters.AddWithValue("@status", cboStatus.Text)
            cmd.ExecuteNonQuery()
            CloseConnection()

            MessageBox.Show("Appointment updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadAppointments()
            ClearFields()
        Catch ex As Exception
            MessageBox.Show("Error updating appointment: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CloseConnection()
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrWhiteSpace(txtApptId.Text) Then
            MessageBox.Show("Please select an appointment to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("Are you sure you want to delete this appointment?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                OpenConnection()
                Dim query As String = "DELETE FROM appointments WHERE appointment_id=@id"
                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", CInt(txtApptId.Text))
                cmd.ExecuteNonQuery()
                CloseConnection()

                MessageBox.Show("Appointment deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadAppointments()
                ClearFields()
            Catch ex As Exception
                MessageBox.Show("Error deleting appointment: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                CloseConnection()
            End Try
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        txtApptId.Clear()
        If cboPatient.Items.Count > 0 Then cboPatient.SelectedIndex = 0
        If cboStaff.Items.Count > 0 Then cboStaff.SelectedIndex = 0
        dtpDateTime.Value = DateTime.Now
        txtReason.Clear()
        cboStatus.SelectedIndex = 0
    End Sub

    Private Sub dgvAppointments_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAppointments.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvAppointments.Rows(e.RowIndex)
            txtApptId.Text = row.Cells("appointment_id").Value.ToString()
            
            ' Find and select patient
            Dim patientName As String = row.Cells("patient_name").Value.ToString()
            For i As Integer = 0 To cboPatient.Items.Count - 1
                If CType(cboPatient.Items(i), Object).Text.Contains(patientName) Then
                    cboPatient.SelectedIndex = i
                    Exit For
                End If
            Next

            ' Find and select staff
            Dim staffName As String = row.Cells("staff_name").Value.ToString()
            For i As Integer = 0 To cboStaff.Items.Count - 1
                If CType(cboStaff.Items(i), Object).Text.Contains(staffName) Then
                    cboStaff.SelectedIndex = i
                    Exit For
                End If
            Next

            dtpDateTime.Value = Convert.ToDateTime(row.Cells("appt_datetime").Value)
            txtReason.Text = If(IsDBNull(row.Cells("reason").Value), "", row.Cells("reason").Value.ToString())
            cboStatus.Text = row.Cells("status").Value.ToString()
        End If
    End Sub
End Class
