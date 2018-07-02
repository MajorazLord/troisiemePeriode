Imports System.Data

Public Class frmListNumCharge
    Public noOf As Integer
    Private myDT As DataTable

    Public frmParent As frmMouvements

    Public Sub New(ByVal frm As frmMouvements)
        frmParent = frm
        InitializeComponent()
    End Sub

    Private Sub frmListNumCharge_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PBRetour.Image = New Bitmap(My.Resources.Fleche_retour)

        'Liaison de la dataGrid avec la dataTable'
        myDT = getDTNumChargeTTHTMN(noOf)
        DGNumCharge.DataSource = myDT
        DGNumCharge.TableStyles.Clear()

        'Permet d'avoir des colonnes dont la taille est redéfinie, sinon c'est impossible de changer la taille des colonnes'
        Dim tableStyle As New DataGridTableStyle

        'Redimensionnement des colonnes'
        If isScreenVGA() Then
            initSizeColumnVGA(tableStyle)
        Else
            initSizeColumn(tableStyle)
        End If

        DGNumCharge.TableStyles.Add(tableStyle)
    End Sub

    Private Sub PBRetour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PBRetour.Click
        Me.Close()
    End Sub

    Private Sub DGArret_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGNumCharge.CurrentCellChanged
        If DGNumCharge.CurrentCell.RowNumber = DGNumCharge.CurrentRowIndex Then
            DGNumCharge.Select(DGNumCharge.CurrentRowIndex)
        End If
    End Sub

    ''' <summary>
    ''' Fonction utiliser pour les ecrans "classiques" (skorpio x3, falcon x3)
    ''' </summary>
    ''' <param name="tableStyle"></param>
    ''' <remarks></remarks>
    Private Sub initSizeColumn(ByRef tableStyle As DataGridTableStyle)
        For Each item As DataColumn In myDT.Columns
            'Si il s'agit de la colonne N°Machine'
            Dim col As New DataGridTextBoxColumn
            col.MappingName = item.ColumnName

            If item.ColumnName.Equals("NumCharge") Then
                col.Width = 100
                col.HeaderText = "N° Charge"
                tableStyle.GridColumnStyles.Add(col)
            ElseIf item.ColumnName.Equals("NumOf") Then
                col.Width = 70
                col.HeaderText = "N° Of"
                tableStyle.GridColumnStyles.Add(col)
            End If
        Next

        For Each vBar As VScrollBar In DGNumCharge.Controls.OfType(Of VScrollBar)()
            ' Evite le chevauchement des scrollBars verticale et horizontale
            vBar.Width = 25
            vBar.Height = DGNumCharge.Height - 25
        Next

        For Each hBar As HScrollBar In DGNumCharge.Controls.OfType(Of HScrollBar)()
            hBar.Height = 25
        Next
    End Sub

    ''' <summary>
    ''' Fonction utiliser pour les ecran VGA (falcon x3 +)
    ''' </summary>
    ''' <param name="tableStyle"></param>
    ''' <remarks></remarks>
    Private Sub initSizeColumnVGA(ByRef tableStyle As DataGridTableStyle)
        For Each item As DataColumn In myDT.Columns
            'Si il s'agit de la colonne N°Machine'
            Dim col As New DataGridTextBoxColumn
            col.MappingName = item.ColumnName

            If item.ColumnName.Equals("NumCharge") Then
                col.Width = 100 * 2
                col.HeaderText = "N° Charge"
                tableStyle.GridColumnStyles.Add(col)
            ElseIf item.ColumnName.Equals("NumOf") Then
                col.Width = 70 * 2
                col.HeaderText = "N° Of"
                tableStyle.GridColumnStyles.Add(col)
            End If
        Next

        For Each vBar As VScrollBar In DGNumCharge.Controls.OfType(Of VScrollBar)()
            ' Evite le chevauchement des scrollBars verticale et horizontale
            vBar.Width = 25 * 2
            vBar.Height = DGNumCharge.Height - (25 * 2)
        Next

        For Each hBar As HScrollBar In DGNumCharge.Controls.OfType(Of HScrollBar)()
            hBar.Height = 25 * 2
        Next
    End Sub

    Private Sub BVerif_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BVerif.Click
        If myDT.Rows.Count <> 0 Then
            If DGNumCharge.CurrentRowIndex = DGNumCharge.CurrentCell.RowNumber And DGNumCharge.IsSelected(DGNumCharge.CurrentRowIndex) Then
                Dim iNB As Integer
                iNB = MsgBox("Voulez-vous choisir ce numéro de charge ?", vbYesNo + vbQuestion, "Val Num Charge")
                If iNB = REPONSE_OK Then
                    'On se trouve ici si tout va bien'
                    Dim row As DataRow
                    row = myDT.Rows(DGNumCharge.CurrentRowIndex)
                    frmParent.TBCharge.Text = row(0)
                End If
            Else
                MsgBox("Aucune ligne séléctionnée.", MsgBoxStyle.Exclamation, "Impossible")
                Exit Sub
            End If
        Else
            MsgBox("Aucune ligne dans le tableau.", MsgBoxStyle.Exclamation, "Impossible")
            Exit Sub
        End If
        Me.Close()
    End Sub
End Class