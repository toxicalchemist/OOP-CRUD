Imports System.Text.RegularExpressions

Module ValidationHelper
    ''' <summary>
    ''' Validates email format
    ''' </summary>
    Public Function IsValidEmail(email As String) As Boolean
        If String.IsNullOrWhiteSpace(email) Then Return False
        Dim pattern As String = "^[^@\s]+@[^@\s]+\.[^@\s]+$"
        Return Regex.IsMatch(email, pattern)
    End Function

    ''' <summary>
    ''' Validates phone number (basic format)
    ''' </summary>
    Public Function IsValidPhone(phone As String) As Boolean
        If String.IsNullOrWhiteSpace(phone) Then Return False
        Dim pattern As String = "^[\d\-\+\(\)\s]+$"
        Return Regex.IsMatch(phone, pattern) AndAlso phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "").Length >= 10
    End Function

    ''' <summary>
    ''' Validates that a string contains only letters and spaces
    ''' </summary>
    Public Function IsValidName(name As String) As Boolean
        If String.IsNullOrWhiteSpace(name) Then Return False
        Dim pattern As String = "^[a-zA-Z\s\-\.]+$"
        Return Regex.IsMatch(name, pattern)
    End Function

    ''' <summary>
    ''' Validates password strength
    ''' </summary>
    Public Function ValidatePasswordStrength(password As String) As (isValid As Boolean, message As String)
        If String.IsNullOrWhiteSpace(password) Then
            Return (False, "Password cannot be empty")
        End If

        If password.Length < 6 Then
            Return (False, "Password must be at least 6 characters long")
        End If

        Dim hasUpper As Boolean = password.Any(Function(c) Char.IsUpper(c))
        Dim hasLower As Boolean = password.Any(Function(c) Char.IsLower(c))
        Dim hasDigit As Boolean = password.Any(Function(c) Char.IsDigit(c))

        If Not (hasUpper And hasLower And hasDigit) Then
            Return (False, "Password should contain uppercase, lowercase, and numbers")
        End If

        Return (True, "Strong password")
    End Function

    ''' <summary>
    ''' Validates decimal input
    ''' </summary>
    Public Function IsValidDecimal(value As String, ByRef result As Decimal) As Boolean
        Return Decimal.TryParse(value, result) AndAlso result >= 0
    End Function

    ''' <summary>
    ''' Validates date is not in the future (for birthdate)
    ''' </summary>
    Public Function IsValidBirthdate(birthdate As DateTime) As Boolean
        Return birthdate <= DateTime.Now AndAlso birthdate >= DateTime.Now.AddYears(-150)
    End Function

    ''' <summary>
    ''' Validates appointment date is in the future
    ''' </summary>
    Public Function IsValidAppointmentDate(apptDate As DateTime) As Boolean
        Return apptDate >= DateTime.Now.AddMinutes(-30) ' Allow 30 min grace period
    End Function

    ''' <summary>
    ''' Sanitize input to prevent SQL injection (additional layer)
    ''' </summary>
    Public Function SanitizeInput(input As String) As String
        If String.IsNullOrWhiteSpace(input) Then Return input
        ' Remove potentially dangerous characters
        Return input.Replace("'", "''").Replace(";", "").Replace("--", "").Replace("/*", "").Replace("*/", "")
    End Function

    ''' <summary>
    ''' Show validation error message
    ''' </summary>
    Public Sub ShowValidationError(message As String)
        MessageBox.Show(message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    ''' <summary>
    ''' Highlight textbox with error
    ''' </summary>
    Public Sub HighlightError(textBox As TextBox)
        textBox.BackColor = Color.FromArgb(255, 230, 230)
        textBox.Focus()
    End Sub

    ''' <summary>
    ''' Clear error highlight
    ''' </summary>
    Public Sub ClearError(textBox As TextBox)
        textBox.BackColor = Color.White
    End Sub
End Module
