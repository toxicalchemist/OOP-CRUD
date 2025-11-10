Imports MySqlConnector

Public Class frmMain
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblUser.Text = "User: " & CurrentUser.FullName
        lblRole.Text = "Role: " & CurrentUser.Role
        lblDateTime.Text = DateTime.Now.ToString("MMM dd, yyyy hh:mm:ss tt")
        LoadStatistics()
    End Sub

    Private Sub LoadStatistics()
        Try
            OpenConnection()
            
            ' Total Patients
            Dim cmdPatients As New MySqlCommand("SELECT COUNT(*) FROM patients", conn)
            lblTotalPatients.Text = cmdPatients.ExecuteScalar().ToString()
            
            ' Today's Appointments
            Dim cmdToday As New MySqlCommand("SELECT COUNT(*) FROM appointments WHERE DATE(appt_datetime) = CURDATE()", conn)
            lblTodayAppts.Text = cmdToday.ExecuteScalar().ToString()
            
            ' Pending Bills
            Dim cmdPending As New MySqlCommand("SELECT COUNT(*) FROM bills WHERE paid = FALSE", conn)
            lblPendingBills.Text = cmdPending.ExecuteScalar().ToString()
            
            ' Total Revenue
            Dim cmdRevenue As New MySqlCommand("SELECT IFNULL(SUM(amount), 0) FROM bills WHERE paid = TRUE", conn)
            Dim revenue As Decimal = Convert.ToDecimal(cmdRevenue.ExecuteScalar())
            lblTotalRevenue.Text = revenue.ToString("C2")
            
            CloseConnection()
        Catch ex As Exception
            MessageBox.Show("Error loading statistics: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CloseConnection()
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblDateTime.Text = DateTime.Now.ToString("MMM dd, yyyy hh:mm:ss tt")
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadStatistics()
        MessageBox.Show("Statistics refreshed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnPatients_Click(sender As Object, e As EventArgs) Handles btnPatients.Click, PatientsToolStripMenuItem.Click
        Dim frmPat As New frmPatients()
        frmPat.ShowDialog()
        LoadStatistics() ' Refresh stats after closing
    End Sub

    Private Sub btnAppointments_Click(sender As Object, e As EventArgs) Handles btnAppointments.Click, AppointmentsToolStripMenuItem.Click
        Dim frmAppt As New frmAppointments()
        frmAppt.ShowDialog()
        LoadStatistics() ' Refresh stats after closing
    End Sub

    Private Sub btnStaff_Click(sender As Object, e As EventArgs) Handles btnStaff.Click, StaffToolStripMenuItem.Click
        If CurrentUser.Role = "Admin" Then
            Dim frmStf As New frmStaff()
            frmStf.ShowDialog()
            LoadStatistics() ' Refresh stats after closing
        Else
            MessageBox.Show("Access denied. Only Admins can manage staff.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnBilling_Click(sender As Object, e As EventArgs) Handles btnBilling.Click, BillingToolStripMenuItem.Click
        Dim frmBill As New frmBilling()
        frmBill.ShowDialog()
        LoadStatistics() ' Refresh stats after closing
    End Sub

    Private Sub ReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportsToolStripMenuItem.Click
        Dim frmRpt As New frmReports()
        frmRpt.ShowDialog()
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        If MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            CurrentUser.StaffId = 0
            CurrentUser.FullName = ""
            CurrentUser.Role = ""
            
            Dim loginForm As New frmLogin()
            loginForm.Show()
            Me.Close()
        End If
    End Sub
End Class
