Imports Datalogic.API

Public Class PageSaisie
    Private WithEvents dcdEvent As DecodeEvent
    Private hDcd As DecodeHandle

    Private Sub PageSaisie_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        initDT()
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

#Region "Btn Delete"
    Private Sub BtnDelCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelCode.Click
        TbCodeCont.Text = ""
        TbCodeCont.Focus()
    End Sub

    Private Sub BtnDelQtiteVerif_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelQtiteVerif.Click
        TbQtitePesee.Text = ""
        TbQtitePesee.Focus()
    End Sub

    Private Sub BtnDelQtiteEcrite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelQtiteEcrite.Click
        TbQuantiteEcrite.Text = ""
        TbQuantiteEcrite.Focus()
    End Sub
#End Region

    Private Function CheckValues() As Boolean
        If (TbCodeCont.Text = "" Or TbQtitePesee.Text = "") Then
            Return False
        End If
        Return True
    End Function

    Private Sub SaveCurrentSaisieToDT()
        Dim auditToSave As New AuditUnit(TbCodeCont.Text, Convert.ToInt32(TbQuantiteEcrite.Text), Convert.ToInt32(TbQtitePesee.Text), -1, -1)
        Try
            addAuditToDT(auditToSave)
        Catch ex As Exception
            MessageBox.Show("Ajout impossible dans la DT")
        End Try
    End Sub

    Private Sub SaveCurrentSaisieToExcel()
        Dim auditToSave As New AuditUnit(TbCodeCont.Text, Convert.ToInt32(TbQuantiteEcrite.Text), Convert.ToInt32(TbQtitePesee.Text), -1, -1)

        Try
            addAuditToExcel(auditToSave)
        Catch ex As Exception
            MessageBox.Show("Ajout impossible dans le fichier Excel")
        End Try
    End Sub

    Private Sub SaveCurrentSaisieToBDD3Sur5()
        Dim auditToSave As New AuditUnit(TbCodeCont.Text, Convert.ToInt32(TbQuantiteEcrite.Text), Convert.ToInt32(TbQtitePesee.Text), -1, -1)

        Try
            addAuditToBDD(auditToSave)
        Catch ex As Exception
            MessageBox.Show("Ajout impossible dans la base de donnée")
        End Try
    End Sub



    Private Sub ResetWindow()
        TbCodeCont.Text = ""
        TbQtitePesee.Text = ""
        TbQuantiteEcrite.Text = ""
        TbCodeCont.Focus()
    End Sub

    Private Sub Btn_suiv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_suiv.Click
        If CheckValues() Then
            SaveCurrentSaisieToDT()
            SaveCurrentSaisieToExcel()
            SaveCurrentSaisieToBDD3Sur5()
            ResetWindow()
            Exit Sub
        End If
        MessageBox.Show("Merci de bien saisir tout les champs.")
    End Sub

    Private Sub Btn_finSaisie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_finSaisie.Click
        Btn_suiv_Click(sender, e)

        Dim res = MessageBox.Show("Voulez-vous arreter la saisie ?", "Erreur", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)

        If res = Windows.Forms.DialogResult.Yes Then
Ici:
            If isConnectionOK() Then
                traitementFin()
            Else
                Dim frmRemonter As New PageRemonter
                frmRemonter.ShowDialog()
                'Si on revient içi c'est qu'on a demandé de reessayer donc on refait le test
                GoTo Ici
            End If
            Application.Exit()
        End If
    End Sub

    Private Sub PBRecap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PBRecap.Click
        Dim frmRecap As New PageRecap
        frmRecap.Show()
    End Sub

End Class
