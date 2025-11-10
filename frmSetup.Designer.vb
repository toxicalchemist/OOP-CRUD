<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSetup
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblStaffCount = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnCreateAdmin = New System.Windows.Forms.Button()
        Me.btnCreateSample = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblStaffCount)
        Me.GroupBox1.Controls.Add(Me.lblStatus)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 70)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(460, 100)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Database Status"
        '
        'lblStaffCount
        '
        Me.lblStaffCount.AutoSize = True
        Me.lblStaffCount.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblStaffCount.Location = New System.Drawing.Point(20, 60)
        Me.lblStaffCount.Name = "lblStaffCount"
        Me.lblStaffCount.Size = New System.Drawing.Size(102, 19)
        Me.lblStaffCount.TabIndex = 1
        Me.lblStaffCount.Text = "Staff records: -"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblStatus.Location = New System.Drawing.Point(20, 30)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(132, 19)
        Me.lblStatus.TabIndex = 0
        Me.lblStatus.Text = "Checking database..."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(100, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(280, 30)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Database Setup Assistant"
        '
        'btnCreateAdmin
        '
        Me.btnCreateAdmin.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(167, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.btnCreateAdmin.Enabled = False
        Me.btnCreateAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCreateAdmin.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnCreateAdmin.ForeColor = System.Drawing.Color.White
        Me.btnCreateAdmin.Location = New System.Drawing.Point(12, 240)
        Me.btnCreateAdmin.Name = "btnCreateAdmin"
        Me.btnCreateAdmin.Size = New System.Drawing.Size(220, 50)
        Me.btnCreateAdmin.TabIndex = 2
        Me.btnCreateAdmin.Text = "Create Admin User"
        Me.btnCreateAdmin.UseVisualStyleBackColor = False
        '
        'btnCreateSample
        '
        Me.btnCreateSample.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCreateSample.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCreateSample.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnCreateSample.ForeColor = System.Drawing.Color.White
        Me.btnCreateSample.Location = New System.Drawing.Point(252, 240)
        Me.btnCreateSample.Name = "btnCreateSample"
        Me.btnCreateSample.Size = New System.Drawing.Size(220, 50)
        Me.btnCreateSample.TabIndex = 3
        Me.btnCreateSample.Text = "Create Sample Data"
        Me.btnCreateSample.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.Gray
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(170, 310)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(140, 40)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 190)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(195, 15)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "? Create admin user (if no staff exists)"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(252, 190)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(220, 15)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "? Add sample patients and appointments"
        '
        'frmSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 361)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnCreateSample)
        Me.Controls.Add(Me.btnCreateAdmin)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Database Setup"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblStatus As Label
    Friend WithEvents lblStaffCount As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btnCreateAdmin As Button
    Friend WithEvents btnCreateSample As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
End Class
