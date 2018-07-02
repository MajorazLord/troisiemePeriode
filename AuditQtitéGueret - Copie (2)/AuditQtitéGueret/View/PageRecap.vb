Imports System.Data
Imports System.Data.SqlClient

Public Class PageRecap

    Private Sub PageRecap_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PBRetour.Image = New Bitmap(My.Resources.Fleche_retour)

        'Liaison de la dataGrid avec la dataTable'
        DGIO.DataSource = DTAudit

        'Permet d'avoir des colonnes dont la taille est redéfinie, sinon c'est impossible de changer la taille des colonnes'
        Dim tableStyle As New DataGridTableStyle

        'Redimensionnement des colonnes'
        If isScreenVGA() Then
            initSizeColumnVGA(tableStyle)
        Else
            initSizeColumn(tableStyle)
        End If

        DGIO.TableStyles.Add(tableStyle)

        'On enleve la selection de base de la première case'
        DGIO.CurrentCell = Nothing

    End Sub


    Private Sub BDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BDelete.Click
        If DTAudit.Rows.Count <> 0 Then
            If DGIO.CurrentRowIndex = DGIO.CurrentCell.RowNumber And DGIO.IsSelected(DGIO.CurrentRowIndex) Then
                Dim iNB As Integer
                iNB = MsgBox("Voulez-vous supprimer cette ligne ?", vbYesNo + vbQuestion, "Suppression")
                If iNB = REPONSE_OK Then
                    'On se trouve ici si tout va bien'
                    'Recuperation de la ligne correspondante entre la vue et la DTIORECAP'
                    Dim row As DataRow
                    row = DTAudit.Rows(DGIO.CurrentRowIndex)

                    removeAuditFromBDD(row(0), row(1), row(2), row(3), row(4))
                    removeAuditFromDT(row(0), row(1), row(2), row(3), row(4))
                    removeAuditFromExcel()
                Else
                    MsgBox("Suppression annulée", MsgBoxStyle.Information, "Suppression annulée")
                End If
            Else
                MsgBox("Aucune ligne séléctionnée.", MsgBoxStyle.Exclamation, "Impossible")
                Exit Sub
            End If
        Else
            MsgBox("Aucune ligne dans le tableau.", MsgBoxStyle.Exclamation, "Impossible")
            Exit Sub
        End If
    End Sub
    Private Sub PBRetour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PBRetour.Click
        Me.Close()
    End Sub
    Private Sub DGIO_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGIO.CurrentCellChanged
        If DGIO.CurrentCell.RowNumber = DGIO.CurrentRowIndex Then
            DGIO.Select(DGIO.CurrentRowIndex)
        End If
    End Sub

    ''' <summary>
    ''' Fonction utiliser pour les ecrans "classiques" (skorpio x3, falcon x3)
    ''' </summary>
    ''' <param name="tableStyle"></param>
    ''' <remarks></remarks>
    Public Sub initSizeColumn(ByRef tableStyle As DataGridTableStyle)
        For Each item As DataColumn In DTAudit.Columns
            Dim col As New DataGridTextBoxColumn
            col.MappingName = item.ColumnName

            If item.ColumnName.Equals("CodeCont") Then
                col.Width = 110
                col.HeaderText = item.ColumnName
            ElseIf item.ColumnName.Equals("QtiteEcrite") Then
                col.Width = 95
                col.HeaderText = item.ColumnName
            ElseIf item.ColumnName.Equals("QtitePesée") Then
                col.Width = 95
                col.HeaderText = item.ColumnName
            ElseIf item.ColumnName.Equals("QtiteSaisie") Then
                col.Width = 95
                col.HeaderText = item.ColumnName
            ElseIf item.ColumnName.Equals("QtiteThéorique") Then
                col.Width = 130
                col.HeaderText = item.ColumnName
            End If
            tableStyle.GridColumnStyles.Add(col)
        Next

        For Each vBar As VScrollBar In DGIO.Controls.OfType(Of VScrollBar)()
            ' Evite le chevauchement des scrollBars verticale et horizontale
            vBar.Width = 25
            vBar.Height = DGIO.Height - 25
        Next

        For Each hBar As HScrollBar In DGIO.Controls.OfType(Of HScrollBar)()
            hBar.Height = 25
        Next
    End Sub

    ''' <summary>
    ''' Fonction utiliser pour les ecrans VGA (falcon x3+)
    ''' </summary>
    ''' <param name="tableStyle"></param>
    ''' <remarks></remarks>
    Public Sub initSizeColumnVGA(ByRef tableStyle As DataGridTableStyle)
        For Each item As DataColumn In DTAudit.Columns
            Dim col As New DataGridTextBoxColumn
            col.MappingName = item.ColumnName

            If item.ColumnName.Equals("CodeCont") Then
                col.Width = 110 * 2
                col.HeaderText = item.ColumnName
            ElseIf item.ColumnName.Equals("QtiteEcrite") Then
                col.Width = 95 * 2
                col.HeaderText = item.ColumnName
            ElseIf item.ColumnName.Equals("QtitePesée") Then
                col.Width = 95 * 2
                col.HeaderText = item.ColumnName
            ElseIf item.ColumnName.Equals("QtiteSaisie") Then
                col.Width = 95 * 2
                col.HeaderText = item.ColumnName
            ElseIf item.ColumnName.Equals("QtiteThéorique") Then
                col.Width = 130 * 2
                col.HeaderText = item.ColumnName
            End If

            tableStyle.GridColumnStyles.Add(col)
        Next

        For Each vBar As VScrollBar In DGIO.Controls.OfType(Of VScrollBar)()
            ' Evite le chevauchement des scrollBars verticale et horizontale
            vBar.Width = 25 * 2
            vBar.Height = DGIO.Height - (25 * 2)
        Next

        For Each hBar As HScrollBar In DGIO.Controls.OfType(Of HScrollBar)()
            hBar.Height = 25 * 2
        Next
    End Sub

End Class