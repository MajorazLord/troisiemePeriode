Imports Datalogic.API
Imports System.IO

Public Class PageSaisie
    Private WithEvents dcdEvent As DecodeEvent
    Private hDcd As DecodeHandle

    Private Sub PageSaisie_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PBRecap.Image = New Bitmap(My.Resources.recapitulatif)
        CreateExcelSave()
        TbCodeCont.Focus()
    End Sub

    ''' <summary>
    ''' Fonction appelée lors du scan d'un code barre, afin d'associé le codebarre au bon TF
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dcdEvent_Scanned(ByVal sender As System.Object, ByVal e As DecodeEventArgs) Handles dcdEvent.Scanned
        Dim cID As CodeId = CodeId.NoData
        Dim dcdData As String = ""
        Dim bBadRead As Boolean = False
        Dim noof As String = ""

        Try
            dcdData = hDcd.ReadString(e.RequestID, cID)
        Catch ex As Exception
            MessageBox.Show("Problème lors de la lecture.")
            bBadRead = True
        End Try

        If Not bBadRead Then
            affichePointVert()
            TbCodeCont.Text = dcdData
        Else
            Exit Sub
        End If
    End Sub


    Private Sub BtnDelCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelCode.Click
        TbCodeCont.Text = ""
        TbCodeCont.Focus()
    End Sub

    Private Sub BtnDelQtiteVerif_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelQtiteVerif.Click
        TbQtiteVerif.Text = ""
        TbQtiteVerif.Focus()
    End Sub

    Private Sub TbQtiteVerif_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TbQtiteVerif.GotFocus
        rechercherQtiteByNumEtiq(TbCodeCont.Text)
        lb_qtiteBdd.Text = resReq
    End Sub


    Private Function CheckValues() As Boolean
        If (TbCodeCont.Text = "" Or lb_qtiteBdd.Text = "" Or TbQtiteVerif.Text = "") Then
            Return False
        End If
        Return True
    End Function

    Private Sub SaveCurrentAudit()
        Dim auditToSave As New AuditUnit(TbCodeCont.Text, Convert.ToInt32(lb_qtiteBdd.Text), Convert.ToInt32(TbQtiteVerif.Text), DateTime.Now)
        addAudit(auditToSave)
        Dim saved = WriteInSave(auditToSave)
        If saved = True Then
            File.Delete(cheminDossierFichier)
        End If
        Dim FAV As New PageAjoutValid
        FAV.Show()
        FAV.Refresh()
        pause(1)
        FAV.Close()
    End Sub

    Private Sub ResetWindow()
        TbCodeCont.Text = ""
        TbQtiteVerif.Text = ""
        lb_qtiteBdd.Text = ""
        TbCodeCont.Focus()
    End Sub

    Private Sub Btn_suiv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_suiv.Click
        If CheckValues() Then
            SaveCurrentAudit()
            ResetWindow()
            Exit Sub
        End If
        MessageBox.Show("Merci de bien saisir tout les champs.")
    End Sub

    Private Sub Btn_finSaisie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_finSaisie.Click
        Btn_suiv_Click(sender, e)

        Dim res = MessageBox.Show("Voulez-vous arreter la saisie ?", "Erreur", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)
        If res = Windows.Forms.DialogResult.Yes Then
            SetValidationTo1(codeSaisie)
            Application.Exit()
        End If
    End Sub

    Private Sub PBRecap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PBRecap.Click
        Dim frmRecap As New PageRecap
        frmRecap.Show()
    End Sub
End Class
