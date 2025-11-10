Imports MySqlConnector

Public Class frmBilling
    Private Sub frmBilling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAppointments()
        LoadBills()
        chkPaid.Checked = False
    End Sub

    Private Sub LoadAppointments()
        Try
            OpenConnection()
            Dim query As String = "SELECT a.appointment_id, CONCAT(p.first_name, ' ', p.last_name, ' - ', DATE_FORMAT(a.appt_datetime, '%Y-%m-%d %H:%i'), ' (', a.status, ')') AS display_text " &
                                  "FROM appointments a " &
                                  "JOIN patients p ON a.patient_id = p.patient_id " &
                                  "ORDER BY a.appt_datetime DESC"
            Dim cmd As New MySqlCommand(query, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            
            cboAppointment.Items.Clear()
            Dim itemCount As Integer = 0
            While reader.Read()
                cboAppointment.Items.Add(New With {.Value = reader("appointment_id"), .Text = reader("display_text").ToString()})
                itemCount += 1
            End While
            cboAppointment.DisplayMember = "Text"
            cboAppointment.ValueMember = "Value"
            reader.Close()
            CloseConnection()
            
            ' Show message if no appointments found
            If itemCount = 0 Then
                MessageBox.Show("No appointments found. Please create appointments first in the Appointments module.", "No Appointments", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading appointments: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CloseConnection()
        End Try
    End Sub

    Private Sub LoadBills()
        Try
            OpenConnection()
            Dim query As String = "SELECT b.bill_id, b.appointment_id, CONCAT(p.first_name, ' ', p.last_name) AS patient_name, " &
                                  "a.appt_datetime, b.amount, IF(b.paid, 'Paid', 'Unpaid') AS payment_status " &
                                  "FROM bills b " &
                                  "JOIN appointments a ON b.appointment_id = a.appointment_id " &
                                  "JOIN patients p ON a.patient_id = p.patient_id " &
                                  "ORDER BY b.bill_id DESC"
            Dim adapter As New MySqlDataAdapter(query, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvBills.DataSource = dt
            CloseConnection()
        Catch ex As Exception
            MessageBox.Show("Error loading bills: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CloseConnection()
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If cboAppointment.SelectedIndex < 0 Then
            MessageBox.Show("Please select an appointment.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim amount As Decimal
        If Not Decimal.TryParse(txtAmount.Text, amount) Then
            MessageBox.Show("Please enter a valid amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            OpenConnection()
            Dim query As String = "INSERT INTO bills (appointment_id, amount, paid) VALUES (@appt, @amount, @paid)"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@appt", CType(cboAppointment.SelectedItem, Object).Value)
            cmd.Parameters.AddWithValue("@amount", amount)
            cmd.Parameters.AddWithValue("@paid", chkPaid.Checked)
            cmd.ExecuteNonQuery()
            CloseConnection()

            MessageBox.Show("Bill added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadBills()
            ClearFields()
        Catch ex As Exception
            MessageBox.Show("Error adding bill: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CloseConnection()
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If String.IsNullOrWhiteSpace(txtBillId.Text) Then
            MessageBox.Show("Please select a bill to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim amount As Decimal
        If Not Decimal.TryParse(txtAmount.Text, amount) Then
            MessageBox.Show("Please enter a valid amount.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            OpenConnection()
            Dim query As String = "UPDATE bills SET appointment_id=@appt, amount=@amount, paid=@paid WHERE bill_id=@id"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@id", CInt(txtBillId.Text))
            cmd.Parameters.AddWithValue("@appt", CType(cboAppointment.SelectedItem, Object).Value)
            cmd.Parameters.AddWithValue("@amount", amount)
            cmd.Parameters.AddWithValue("@paid", chkPaid.Checked)
            cmd.ExecuteNonQuery()
            CloseConnection()

            MessageBox.Show("Bill updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadBills()
            ClearFields()
        Catch ex As Exception
            MessageBox.Show("Error updating bill: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CloseConnection()
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrWhiteSpace(txtBillId.Text) Then
            MessageBox.Show("Please select a bill to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("Are you sure you want to delete this bill?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                OpenConnection()
                Dim query As String = "DELETE FROM bills WHERE bill_id=@id"
                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", CInt(txtBillId.Text))
                cmd.ExecuteNonQuery()
                CloseConnection()

                MessageBox.Show("Bill deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadBills()
                ClearFields()
            Catch ex As Exception
                MessageBox.Show("Error deleting bill: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                CloseConnection()
            End Try
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        txtBillId.Clear()
        If cboAppointment.Items.Count > 0 Then cboAppointment.SelectedIndex = 0
        txtAmount.Clear()
        chkPaid.Checked = False
    End Sub

    Private Sub dgvBills_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBills.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvBills.Rows(e.RowIndex)
            txtBillId.Text = row.Cells("bill_id").Value.ToString()

            Dim apptId As Integer = Convert.ToInt32(row.Cells("appointment_id").Value)
            For i As Integer = 0 To cboAppointment.Items.Count - 1
                If CType(cboAppointment.Items(i), Object).Value = apptId Then
                    cboAppointment.SelectedIndex = i
                    Exit For
                End If
            Next

            txtAmount.Text = row.Cells("amount").Value.ToString()
            chkPaid.Checked = (row.Cells("payment_status").Value.ToString() = "Paid")
        End If
    End Sub
End Class
