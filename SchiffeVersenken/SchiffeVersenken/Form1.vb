Public Class Form1

    Private Sub BeendenButton_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub


    Private Sub NewButton_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Application.Restart()
    End Sub
End Class
