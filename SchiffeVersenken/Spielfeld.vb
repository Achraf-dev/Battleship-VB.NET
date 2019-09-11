Public Class Spielfeld
    '########################################################
    '# erstellen eines Arrays zur Abbildung des Spielfeldes #
    '########################################################
    Friend SFeld(9, 9) As KlSchiff
    '###########################################################
    '# erstellen eines Arrays zur Abbildung des Schiffvorrates #
    '###########################################################
    Dim SchiffsVorrat As New ArrayList
    '############################################################
    '# erstellen eine Variable zur Nutzung für Kontrolle um die #
    '# Platzierbarkeit eines neuen Schiffes zu testen           #
    '# (ist es regelkonform?)                                   #
    '############################################################
    Dim possible As Boolean = False

    '#######################################
    '# was soll beim Laden des Spielfeldes #
    '#######################################



    Private Sub Spielfeld_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '######################################################
        '# Spalten wurden manuell eingefügt und hier wird per #
        '# Schleife noch die fehlenden Zeilen eingefügt und   #
        '# die Höhe wird festgelegt um ein quadratisches Feld #
        '# zu erstellen                                       #
        '######################################################
        For i As Int16 = 0 To 9

            Me.DataGridView1.Rows.Add(1)
            Me.DataGridView1.Rows(i).Height = 40
            For j As Int16 = 0 To Me.DataGridView1.Columns.Count - 1
                Me.DataGridView1.Item(j, i).Tag = 1
            Next
            Form1.DataGridView1.Rows.Add(1)
            Form1.DataGridView1.Rows(i).Height = 40
            For j As Int16 = 0 To Form1.DataGridView1.Columns.Count - 1
                Form1.DataGridView1.Item(j, i).Value = 1
            Next
        Next

        '########################################################
        '# Befüllung des Schiffsvorrates mit den gewünschten    #
        '# (eindeutigen) Namen festlegen, der Name muss mit der #
        '# der gewünschten Größe als Ziffer anfangen.           #
        '########################################################

        SchiffsVorrat.Add("5 Schlachtschiff")
        SchiffsVorrat.Add("4 Flugzeugträger1")
        SchiffsVorrat.Add("4 Flugzeugträger2")
        SchiffsVorrat.Add("3 Kreuzer1")
        SchiffsVorrat.Add("3 Kreuzer2")
        SchiffsVorrat.Add("3 Kreuzer3")
        SchiffsVorrat.Add("2 U-Boot1")
        SchiffsVorrat.Add("2 U-Boot2")
        SchiffsVorrat.Add("2 U-Boot3")
        SchiffsVorrat.Add("2 U-Boot4")


    End Sub

    '############################################################
    '# was soll beim einem Doppelklick in einer Zelle geschehen #
    '############################################################

    Private Sub DataGridView1_CellMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick

        Dim Prüfwert As Int16 = Me.DataGridView1.Item(e.ColumnIndex, e.RowIndex).Tag
        Select Case Prüfwert
            Case 1
                '  My.Computer.Audio.Play("C:\Users\....\Downloads\Sound\water001.wav", AudioPlayMode.Background)
                Me.DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value = My.Resources.Explosion_Wasser_Textur_200x200px
            Case 2 To 5
                Me.DataGridView1.Item(e.ColumnIndex, e.RowIndex).Value = My.Resources.Explosion_Treffer_Textur_200x200px
                Dim aktT2 As Int16 = CInt(Form1.LabelTrefferAnz.Text) + 1
                Form1.LabelTrefferAnz.Text = aktT2

        End Select

    End Sub


    '#############################################################
    '# was soll geschehen wenn ein Mousebutton losgelassen wird? #
    '#############################################################

    Private Sub DataGridView1_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseUp
        '#############################################################
        '# als erstes wird geprüft ob die rechte oder                #
        '# die mittlere Maustaste gedrückt wurde.                    #
        '# In diesem Fall wird die Sub-Routine abgebrochen.          #
        '# Denn es soll nur auf die linke Maustaste reagiert werden. #
        '#############################################################
        If e.Button = Windows.Forms.MouseButtons.Right Or e.Button = Windows.Forms.MouseButtons.Middle Then Exit Sub

        '############################################
        '# Ist im Kontexmenu das Häkchen gesetzt?   #
        '# Liegt die Zahl zwischen zwei und fünf?   #
        '# Ist der Zelleninhalt leer(keine Ziffer)? #
        '# Wenn nicht wird die Routine beendet.     #
        '############################################
        If Me.ToolStripMenuItem1.Checked = False Then Exit Sub
        If Me.DataGridView1.SelectedCells.Count < 2 Then Exit Sub
        If Me.DataGridView1.SelectedCells.Count > 5 Then Exit Sub
        For i As Int16 = 0 To Me.DataGridView1.SelectedCells.Count - 1
            Select Case Me.DataGridView1.SelectedCells.Item(i).Tag
                Case 2 - 5
                    Exit Sub
            End Select

        Next

        '##################################################
        '# Schleife, die den Schiffsvorrat "kontrolliert" #
        '##################################################

        For i As Int16 = 0 To SchiffsVorrat.Count - 1

            '######################################################
            '# Vergleich der Anzahl der markierten Zellen         #
            '# mit dem ersten String des Schiffsnamen vergleichen #
            '# und den dazugehörigen Position merken              #
            '######################################################
            If Me.DataGridView1.SelectedCells.Count - 1 = SchiffsVorrat(i).ToString.Substring(0, 1) - 1 Then

                '#######################################################
                '# anlegen einer neuen Variablen mit der Klasse Schiff #
                '# und den Namen des Schiffes übergeben                #
                '#######################################################

                Dim bSchiff As New KlSchiff(Me.DataGridView1.SelectedCells.Count)
                bSchiff.Schiffsname = SchiffsVorrat(i)

                '#######################################################
                '# anlegen eines neuen Buttons für das Schiff anlegen  #
                '# und den Namen des Schiffes übergeben                #
                '#######################################################

                Dim nB As New Button
                nB.Width = 40 * Me.DataGridView1.SelectedCells.Count + 40
                nB.Height = 30
                nB.Text = SchiffsVorrat(i)
                nB.AutoEllipsis = True
                Form1.FlowLayoutPanel1.Controls.Add(nB)

                '#######################################################
                '# mit Hilfe dieser Schleife wird sich die Position im #
                '# Spielfeld gemerkt bzw. der Wert des Zellencounters  #
                '# in die Zelle als Text eingetragen und danach        #
                '# aus dem Schiffsvorrat gelöscht.                     #
                '#######################################################



                For j As Int16 = 0 To Me.DataGridView1.SelectedCells.Count - 1

                    If Me.DataGridView1.SelectedCells.Item(0).ColumnIndex > Me.DataGridView1.SelectedCells.Item(0 + Me.DataGridView1.SelectedCells.Count - 1).ColumnIndex Then
                        '##############
                        '# waagerecht #
                        '##############

                        If Me.DataGridView1.SelectedCells.Item(0).RowIndex > 0 Then
                            Form1.DataGridView1.Item(Me.DataGridView1.SelectedCells(j).ColumnIndex, Me.DataGridView1.SelectedCells(j).RowIndex - 1).Value = 11
                        End If
                        If Me.DataGridView1.SelectedCells.Item(0).RowIndex < 9 Then
                            Form1.DataGridView1.Item(Me.DataGridView1.SelectedCells(j).ColumnIndex, Me.DataGridView1.SelectedCells(j).RowIndex + 1).Value = 22
                        End If
                        If Me.DataGridView1.SelectedCells.Item(0).ColumnIndex > 0 Then
                            Form1.DataGridView1.Item(Me.DataGridView1.SelectedCells(j).ColumnIndex - 1, Me.DataGridView1.SelectedCells(j).RowIndex).Value = 44
                        End If
                        If Me.DataGridView1.SelectedCells.Item(0).RowIndex > 0 Then
                            If Me.DataGridView1.SelectedCells.Item(0).ColumnIndex > 0 Then
                                Form1.DataGridView1.Item(Me.DataGridView1.SelectedCells(j).ColumnIndex - 1, Me.DataGridView1.SelectedCells(j).RowIndex - 1).Value = 33
                            End If
                        End If
                        If Me.DataGridView1.SelectedCells.Item(0).RowIndex < 9 Then
                            If Me.DataGridView1.SelectedCells.Item(0).ColumnIndex > 0 Then
                                Form1.DataGridView1.Item(Me.DataGridView1.SelectedCells(j).ColumnIndex - 1, Me.DataGridView1.SelectedCells(j).RowIndex + 1).Value = 55
                            End If
                        End If
                        Form1.DataGridView1.Item(Me.DataGridView1.SelectedCells(j).ColumnIndex, Me.DataGridView1.SelectedCells(j).RowIndex).Value = Me.DataGridView1.SelectedCells.Count

                    Else
                        '#############
                        '# senkrecht #
                        '#############

                        Form1.DataGridView1.Item(Me.DataGridView1.SelectedCells(j).ColumnIndex - 1, Me.DataGridView1.SelectedCells(j).RowIndex).Value = 99
                        Form1.DataGridView1.Item(Me.DataGridView1.SelectedCells(j).ColumnIndex + 1, Me.DataGridView1.SelectedCells(j).RowIndex).Value = 99
                        Form1.DataGridView1.Item(Me.DataGridView1.SelectedCells(j).ColumnIndex, Me.DataGridView1.SelectedCells(j).RowIndex - 1).Value = 99
                        Form1.DataGridView1.Item(Me.DataGridView1.SelectedCells(j).ColumnIndex - 1, Me.DataGridView1.SelectedCells(j).RowIndex - 1).Value = 99
                        Form1.DataGridView1.Item(Me.DataGridView1.SelectedCells(j).ColumnIndex + 1, Me.DataGridView1.SelectedCells(j).RowIndex - 1).Value = 99
                        Form1.DataGridView1.Item(Me.DataGridView1.SelectedCells(j).ColumnIndex, Me.DataGridView1.SelectedCells(j).RowIndex).Value = Me.DataGridView1.SelectedCells.Count

                    End If


                    Dim Z As Int16 = Me.DataGridView1.SelectedCells(j).RowIndex
                    Dim S As Int16 = Me.DataGridView1.SelectedCells(j).ColumnIndex

                    SFeld(S, Z) = bSchiff
                Next
                If Me.DataGridView1.SelectedCells.Item(0).ColumnIndex > Me.DataGridView1.SelectedCells.Item(0 + Me.DataGridView1.SelectedCells.Count - 1).ColumnIndex Then
                    '##############
                    '# waagerecht #
                    '##############

                    Form1.DataGridView1.Item(Me.DataGridView1.SelectedCells(0).ColumnIndex + Me.DataGridView1.SelectedRows.Count + 1, Me.DataGridView1.SelectedCells(0).RowIndex).Value = 66
                    If Me.DataGridView1.SelectedCells.Item(0).RowIndex < 9 Then
                        Form1.DataGridView1.Item(Me.DataGridView1.SelectedCells(0).ColumnIndex + Me.DataGridView1.SelectedRows.Count + 1, Me.DataGridView1.SelectedCells(0).RowIndex + 1).Value = 77
                    End If

                    If Me.DataGridView1.SelectedCells.Item(0).RowIndex > 0 Then
                        Form1.DataGridView1.Item(Me.DataGridView1.SelectedCells(0).ColumnIndex + Me.DataGridView1.SelectedRows.Count + 1, Me.DataGridView1.SelectedCells(0).RowIndex - 1).Value = 88
                    End If

                    SchiffsVorrat.RemoveAt(i)

                Else
                    '#############
                    '# senkrecht #
                    '#############

                    Form1.DataGridView1.Item(Me.DataGridView1.SelectedCells(0).ColumnIndex + Me.DataGridView1.SelectedRows.Count, Me.DataGridView1.SelectedCells(0).RowIndex + 1).Value = 99
                    Form1.DataGridView1.Item(Me.DataGridView1.SelectedCells(0).ColumnIndex + Me.DataGridView1.SelectedRows.Count + 1, Me.DataGridView1.SelectedCells(0).RowIndex + 1).Value = 99
                    Form1.DataGridView1.Item(Me.DataGridView1.SelectedCells(0).ColumnIndex + Me.DataGridView1.SelectedRows.Count - 1, Me.DataGridView1.SelectedCells(0).RowIndex + 1).Value = 99

                    SchiffsVorrat.RemoveAt(i)

                End If

                Exit For
            End If
        Next

    End Sub

    Private Sub WennNeuerButtonGeklicktWird(sender As Object, e As EventArgs)
        Dim NB As Button = sender
        Dim BText As String = NB.Text

    End Sub

    Private Sub BeiTreffer(sender As Object, e As EventArgs)
        Dim NB As Button = sender
        Dim BText As String = NB.Text

    End Sub

    '###########################################################
    '# was soll beim Klick im Kontextmenupunkt eins geschehen? #
    '###########################################################

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        If Me.ToolStripMenuItem1.Checked = False Then
            Me.DataGridView1.MultiSelect = False
            For i As Int16 = 0 To 9
                For j As Int16 = 0 To 9
                    Me.DataGridView1.Item(j, i).Value = My.Resources.Wasser_Textur_200x200px

                Next
            Next
        Else
            Me.DataGridView1.MultiSelect = True
        End If
    End Sub

    '####################################################
    '# Subroutine für die Kontrolle ob ein neues Schiff #
    '# an der angegebenen Position platzierbar ist.     #
    '# (ist es regelkonform?)                           #
    '####################################################
    Private Sub placepossible()

        For j As Int16 = 0 To Me.DataGridView1.SelectedCells.Count - 1

            If Me.DataGridView1.SelectedCells(j).Tag = 1 Then
                possible = True
            End If


        Next

    End Sub



End Class
