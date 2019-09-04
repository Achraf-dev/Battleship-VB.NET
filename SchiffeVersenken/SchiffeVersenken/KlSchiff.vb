Public Class KlSchiff
    Dim sTyp As Int16
    Dim Treffer As Int16 = 0
    Friend Schiffsname As String = ""

    Public Sub Treffen()
        Treffer += 1
        If Treffer = sTyp Then

            With Form1.FlowLayoutPanel1
                For i As Int16 = 0 To .Controls.Count - 1
                    If Schiffsname = .Controls(i).Text Then
                        .Controls(i).Dispose()
                        Exit For
                    End If
                Next
            End With
            'MessageBox.Show("Schiff vom Typ " & sTyp & vbCrLf & "int. Schiffsname: " & Schiffsname & " versenkt.", "Versenkt!", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Form1.TextBox1.Text = "Schiff " & Schiffsname & " wurde versenkt."
            '  My.Computer.Audio.Play("C:\", AudioPlayMode.Background) 'habe kein audio datei
        Else
            'MsgBox("Treffer: " & Treffer & vbCrLf & "Typ: " & sTyp & vbCrLf & "int. Schiffsname: " & Schiffsname & " wurde getroffen!")
            Form1.TextBox1.Text = "Schiff " & Schiffsname & " wurde getroffen."
            '   My.Computer.Audio.Play("C:\", AudioPlayMode.Background)  'habe kein audio datei
        End If

    End Sub

    Sub New(ByVal T As Int16)
        sTyp = T


    End Sub
        
End Class
