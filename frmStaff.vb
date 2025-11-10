Imports MySqlConnector

Public Class frmStaff
    Private Sub frmStaff_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStaff()
        If cboRole.Items.Count > 0 Then cboRole.SelectedIndex = 0
    End Sub

    Private Sub LoadStaff()
        Try
            OpenConnection()
            Dim query As String = "SELECT * FROM staff ORDER BY staff_id DESC"
            Dim adapter As New MySqlDataAdapter(query, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvStaff.DataSource = dt
            CloseConnection()
        Catch ex As Exception
            MessageBox.Show("Error loading staff: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CloseConnection()
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If String.IsNullOrWhiteSpace(txtUsername.Text) Or String.IsNullOrWhiteSpace(txtPassword.Text) Then
            MessageBox.Show("Please enter username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            OpenConnection()
            Dim query As String = "INSERT INTO staff (username, password_hash, full_name, role, contact) " &
                                  "VALUES (@user, @pass, @name, @role, @contact)"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@user", txtUsername.Text)
            cmd.Parameters.AddWithValue("@pass", txtPassword.Text)
            cmd.Parameters.AddWithValue("@name", txtFullName.Text)
            cmd.Parameters.AddWithValue("@role", cboRole.Text)
            cmd.Parameters.AddWithValue("@contact", txtContact.Text)
            cmd.ExecuteNonQuery()
            CloseConnection()

            MessageBox.Show("Staff added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadStaff()
            ClearFields()
        Catch ex As Exception
            MessageBox.Show("Error adding staff: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CloseConnection()
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If String.IsNullOrWhiteSpace(txtStaffId.Text) Then
            MessageBox.Show("Please select a staff member to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            OpenConnection()
            Dim query As String = "UPDATE staff SET username=@user, password_hash=@pass, full_name=@name, role=@role, contact=@contact WHERE staff_id=@id"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@id", CInt(txtStaffId.Text))
            cmd.Parameters.AddWithValue("@user", txtUsername.Text)
            cmd.Parameters.AddWithValue("@pass", txtPassword.Text)
            cmd.Parameters.AddWithValue("@name", txtFullName.Text)
            cmd.Parameters.AddWithValue("@role", cboRole.Text)
            cmd.Parameters.AddWithValue("@contact", txtContact.Text)
            cmd.ExecuteNonQuery()
            CloseConnection()

            MessageBox.Show("Staff updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadStaff()
            ClearFields()
        Catch ex As Exception
            MessageBox.Show("Error updating staff: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CloseConnection()
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrWhiteSpace(txtStaffId.Text) Then
            MessageBox.Show("Please select a staff member to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("Are you sure you want to delete this staff member?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                OpenConnection()
                Dim query As String = "DELETE FROM staff WHERE staff_id=@id"
                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@id", CInt(txtStaffId.Text))
                cmd.ExecuteNonQuery()
                CloseConnection()

                MessageBox.Show("Staff deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadStaff()
                ClearFields()
            Catch ex As Exception
                MessageBox.Show("Error deleting staff: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                CloseConnection()
            End Try
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        txtStaffId.Clear()
        txtUsername.Clear()
        txtPassword.Clear()
        txtFullName.Clear()
        If cboRole.Items.Count > 0 Then cboRole.SelectedIndex = 0
        txtContact.Clear()
    End Sub

    Private Sub dgvStaff_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvStaff.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvStaff.Rows(e.RowIndex)
            txtStaffId.Text = row.Cells("staff_id").Value.ToString()
            txtUsername.Text = If(IsDBNull(row.Cells("username").Value), "", row.Cells("username").Value.ToString())
            txtPassword.Text = If(IsDBNull(row.Cells("password_hash").Value), "", row.Cells("password_hash").Value.ToString())
            txtFullName.Text = If(IsDBNull(row.Cells("full_name").Value), "", row.Cells("full_name").Value.ToString())
            cboRole.Text = If(IsDBNull(row.Cells("role").Value), "", row.Cells("role").Value.ToString())
            txtContact.Text = If(IsDBNull(row.Cells("contact").Value), "", row.Cells("contact").Value.ToString())
        End If
    End Sub
End Class
