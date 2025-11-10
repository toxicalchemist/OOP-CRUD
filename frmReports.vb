Imports MySqlConnector
Imports System.IO
Imports System.Text

Public Class frmReports
    Private Sub frmReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpStartDate.Value = DateTime.Now.AddDays(-30)
        dtpEndDate.Value = DateTime.Now
        cboReportType.SelectedIndex = 0
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        Select Case cboReportType.SelectedIndex
            Case 0 ' Patients Report
                GeneratePatientsReport()
            Case 1 ' Appointments Report
                GenerateAppointmentsReport()
            Case 2 ' Revenue Report
                GenerateRevenueReport()
            Case 3 ' Staff Report
                GenerateStaffReport()
        End Select
    End Sub

    Private Sub GeneratePatientsReport()
        Try
            OpenConnection()
            Dim query As String = "SELECT patient_id AS 'ID', CONCAT(first_name, ' ', last_name) AS 'Patient Name', " &
                                  "birthdate AS 'Birth Date', gender AS 'Gender', contact AS 'Contact', " &
                                  "created_at AS 'Registered Date' FROM patients ORDER BY patient_id"
            Dim adapter As New MySqlDataAdapter(query, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvReport.DataSource = dt
            lblRecordCount.Text = $"Total Records: {dt.Rows.Count}"
            CloseConnection()
        Catch ex As Exception
            MessageBox.Show("Error generating report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CloseConnection()
        End Try
    End Sub

    Private Sub GenerateAppointmentsReport()
        Try
            OpenConnection()
            Dim query As String = "SELECT a.appointment_id AS 'ID', CONCAT(p.first_name, ' ', p.last_name) AS 'Patient', " &
                                  "s.full_name AS 'Doctor/Staff', a.appt_datetime AS 'Date/Time', " &
                                  "a.reason AS 'Reason', a.status AS 'Status' " &
                                  "FROM appointments a " &
                                  "JOIN patients p ON a.patient_id = p.patient_id " &
                                  "JOIN staff s ON a.staff_id = s.staff_id " &
                                  "WHERE DATE(a.appt_datetime) BETWEEN @start AND @end " &
                                  "ORDER BY a.appt_datetime DESC"
            Dim adapter As New MySqlDataAdapter(query, conn)
            adapter.SelectCommand.Parameters.AddWithValue("@start", dtpStartDate.Value.ToString("yyyy-MM-dd"))
            adapter.SelectCommand.Parameters.AddWithValue("@end", dtpEndDate.Value.ToString("yyyy-MM-dd"))
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvReport.DataSource = dt
            lblRecordCount.Text = $"Total Records: {dt.Rows.Count}"
            CloseConnection()
        Catch ex As Exception
            MessageBox.Show("Error generating report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CloseConnection()
        End Try
    End Sub

    Private Sub GenerateRevenueReport()
        Try
            OpenConnection()
            Dim query As String = "SELECT b.bill_id AS 'Bill ID', CONCAT(p.first_name, ' ', p.last_name) AS 'Patient', " &
                                  "a.appt_datetime AS 'Appointment Date', b.amount AS 'Amount', " &
                                  "IF(b.paid, 'Paid', 'Unpaid') AS 'Status', b.created_at AS 'Bill Date' " &
                                  "FROM bills b " &
                                  "JOIN appointments a ON b.appointment_id = a.appointment_id " &
                                  "JOIN patients p ON a.patient_id = p.patient_id " &
                                  "WHERE DATE(b.created_at) BETWEEN @start AND @end " &
                                  "ORDER BY b.created_at DESC"
            Dim adapter As New MySqlDataAdapter(query, conn)
            adapter.SelectCommand.Parameters.AddWithValue("@start", dtpStartDate.Value.ToString("yyyy-MM-dd"))
            adapter.SelectCommand.Parameters.AddWithValue("@end", dtpEndDate.Value.ToString("yyyy-MM-dd"))
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvReport.DataSource = dt

            ' Calculate totals
            Dim totalPaid As Decimal = 0
            Dim totalUnpaid As Decimal = 0
            For Each row As DataRow In dt.Rows
                If row("Status").ToString() = "Paid" Then
                    totalPaid += Convert.ToDecimal(row("Amount"))
                Else
                    totalUnpaid += Convert.ToDecimal(row("Amount"))
                End If
            Next

            lblRecordCount.Text = $"Total Records: {dt.Rows.Count} | Paid: {totalPaid:C2} | Unpaid: {totalUnpaid:C2} | Total: {(totalPaid + totalUnpaid):C2}"
            CloseConnection()
        Catch ex As Exception
            MessageBox.Show("Error generating report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CloseConnection()
        End Try
    End Sub

    Private Sub GenerateStaffReport()
        Try
            OpenConnection()
            Dim query As String = "SELECT staff_id AS 'ID', username AS 'Username', full_name AS 'Full Name', " &
                                  "role AS 'Role', contact AS 'Contact', created_at AS 'Join Date' " &
                                  "FROM staff ORDER BY role, full_name"
            Dim adapter As New MySqlDataAdapter(query, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvReport.DataSource = dt
            lblRecordCount.Text = $"Total Records: {dt.Rows.Count}"
            CloseConnection()
        Catch ex As Exception
            MessageBox.Show("Error generating report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CloseConnection()
        End Try
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        If dgvReport.Rows.Count = 0 Then
            MessageBox.Show("No data to export. Please generate a report first.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "CSV Files (*.csv)|*.csv|Text Files (*.txt)|*.txt"
            saveDialog.FileName = $"{cboReportType.Text.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}"

            If saveDialog.ShowDialog() = DialogResult.OK Then
                ExportToCSV(saveDialog.FileName)
                MessageBox.Show($"Report exported successfully to:{vbCrLf}{saveDialog.FileName}", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error exporting report: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ExportToCSV(filePath As String)
        Dim sb As New StringBuilder()

        ' Add headers
        Dim headers As New List(Of String)
        For Each column As DataGridViewColumn In dgvReport.Columns
            headers.Add(column.HeaderText)
        Next
        sb.AppendLine(String.Join(",", headers))

        ' Add rows
        For Each row As DataGridViewRow In dgvReport.Rows
            If Not row.IsNewRow Then
                Dim cells As New List(Of String)
                For Each cell As DataGridViewCell In row.Cells
                    Dim value As String = If(cell.Value IsNot Nothing, cell.Value.ToString().Replace(",", ";"), "")
                    cells.Add($"""{value}""")
                Next
                sb.AppendLine(String.Join(",", cells))
            End If
        Next

        File.WriteAllText(filePath, sb.ToString())
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If dgvReport.Rows.Count = 0 Then
            MessageBox.Show("No data to print. Please generate a report first.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        MessageBox.Show("Print functionality will be implemented with PrintDocument control.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ' TODO: Implement printing functionality
    End Sub
End Class
