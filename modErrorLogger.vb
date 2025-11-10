Imports System.IO

Module ErrorLogger
    Private ReadOnly LogFilePath As String = Path.Combine(Application.StartupPath, "error_log.txt")

    ''' <summary>
    ''' Log error to file
    ''' </summary>
    Public Sub LogError(ex As Exception, Optional additionalInfo As String = "")
        Try
            Dim logEntry As New Text.StringBuilder()
            logEntry.AppendLine("================================================================================")
            logEntry.AppendLine($"DateTime: {DateTime.Now:yyyy-MM-dd HH:mm:ss}")
            logEntry.AppendLine($"User: {CurrentUser.FullName} ({CurrentUser.Role})")
            logEntry.AppendLine($"Error Message: {ex.Message}")
            logEntry.AppendLine($"Error Type: {ex.GetType().Name}")
            
            If Not String.IsNullOrWhiteSpace(additionalInfo) Then
                logEntry.AppendLine($"Additional Info: {additionalInfo}")
            End If
            
            logEntry.AppendLine($"Stack Trace: {ex.StackTrace}")
            
            If ex.InnerException IsNot Nothing Then
                logEntry.AppendLine($"Inner Exception: {ex.InnerException.Message}")
            End If
            
            logEntry.AppendLine("================================================================================")
            logEntry.AppendLine()

            File.AppendAllText(LogFilePath, logEntry.ToString())
        Catch logEx As Exception
            ' If we can't log, show a message
            MessageBox.Show($"Error logging failed: {logEx.Message}", "Logging Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Log info message
    ''' </summary>
    Public Sub LogInfo(message As String)
        Try
            Dim logEntry As String = $"[INFO] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{vbCrLf}"
            File.AppendAllText(LogFilePath, logEntry)
        Catch ex As Exception
            ' Silent fail for info logging
        End Try
    End Sub

    ''' <summary>
    ''' Get log file path
    ''' </summary>
    Public Function GetLogFilePath() As String
        Return LogFilePath
    End Function

    ''' <summary>
    ''' Clear old logs (older than 30 days)
    ''' </summary>
    Public Sub ClearOldLogs()
        Try
            If File.Exists(LogFilePath) Then
                Dim fileInfo As New FileInfo(LogFilePath)
                If fileInfo.CreationTime < DateTime.Now.AddDays(-30) Then
                    File.Delete(LogFilePath)
                    LogInfo("Log file cleared - older than 30 days")
                End If
            End If
        Catch ex As Exception
            ' Silent fail
        End Try
    End Sub

    ''' <summary>
    ''' Show error with logging
    ''' </summary>
    Public Sub ShowErrorWithLog(ex As Exception, userMessage As String, Optional additionalInfo As String = "")
        LogError(ex, additionalInfo)
        MessageBox.Show($"{userMessage}{vbCrLf}{vbCrLf}Technical Details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub
End Module
