Imports Datalogic.API
Imports System.IO

Public Class Arrivée
    Private WithEvents dcdEvent As DecodeEvent
    Private hDcd As DecodeHandle

    Private Sub Arrivée_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateExcelSave()
        listDesSorties.Clear()
        loadFullDecodeur(hDcd, Me, dcdEvent)
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

        Dim numOF As String = ""
        Dim numEtiq As String = ""
        Dim noProd As String = ""
        Dim numOP As String = ""
        Dim numLot As String = ""

        Try
            dcdData = hDcd.ReadString(e.RequestID, cID)
        Catch ex As Exception
            MessageBox.Show("Problème lors de la lecture.")
            bBadRead = True
        End Try

        If Not bBadRead Then
            For Each ctrl As Control In Me.Controls()
                'Est-ce celui sur lequel on est'
                If ctrl.Focused Then
                    If ctrl.Name = "TbScan" Then
                        Dim regex As System.Text.RegularExpressions.Regex
                        regex = New System.Text.RegularExpressions.Regex("(^(G|A)([0-9]+))|([0-9]+\/[0-9]{1,3}\/[0-9]{1,3})|([0-9]+\/[0-9]{1,3}\-[0-9]{1,2}\/[0-9]{1,3})")
                        If regex.IsMatch(dcdData) Then
                            affichePointVert()
                            TbScan.Text = dcdData
                            TbQtité.Focus()

                            TbQtité.Text = qtiteTheoriqueCont(dcdData)
                        Else
                            TbScan.Focus()
                        End If
                    End If
                    Exit For
                End If
            Next
        Else
            Exit Sub
        End If
    End Sub

    Private Sub BtnRecap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRecap.Click
        Dim frmRecapArrivee As New RecapArrivee
        dcdEvent.Dispose()
        frmRecapArrivee.ShowDialog()
        loadFullDecodeur(hDcd, Me, dcdEvent)
        TbScan.Focus()
    End Sub

    Private Sub BtnCont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCont.Click
        If (((TbScan.Text <> "") Or (TbQtité.Text <> "")) And (TbQtité.Text <> "A Saisir")) Then
            'Si le regex passe verif que c'est pas un numero de lot
            If TbScan.Text.Length <> 15 Then
                myUser.DTArrivee.Rows.Add(TbScan.Text, TbQtité.Text)
                Dim sortieToAdd = createSortie(TbScan.Text, TbQtité.Text)
                If sortieToAdd Is Nothing Then
                    GoTo GOTOELSE
                End If
                listDesSorties.Add(sortieToAdd)
                insertSortieToFile(sortieToAdd)
            Else
GOTOELSE:
                MessageBox.Show("Attention, code scanné non valide !", "Erreur scan", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1)
            End If
            TbScan.Text = ""
            TbQtité.Text = ""
            TbScan.Focus()
        Else
            MessageBox.Show("Attention, verifier les champs saisies !", "Erreur Continuer", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1)
        End If
        TbScan.Focus()
    End Sub

    Private Sub BTerm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTerm.Click
        Dim res = MessageBox.Show("Êtes-vous sur de vouloir arreter la saisie ?", "Fin Saisie ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If res = Windows.Forms.DialogResult.Yes Then
            Dim status As String = ""
            If isConnectionOK(status) Then

                'MessageBox.Show(status)
                Debug.WriteLine("Status : " & status)

                'Vide et zero saisie
                If TbScan.Text = "" And TbQtité.Text = "" And myUser.DTArrivee.Rows.Count = 0 Then
                    MsgBox("Rien n'a été saisie ! Annulation de la saisie.", MsgBoxStyle.OkOnly, "Attention Saisie")
                    File.Delete(cheminDossierFichier)

                    'Pas vide et zero saisie
                ElseIf TbScan.Text = "" And TbQtité.Text = "" And myUser.DTArrivee.Rows.Count <> 0 Then
                    WriteSQL()

                    'Pas tout saisie
                ElseIf TbScan.Text = "" Or TbQtité.Text = "" Then
                    MessageBox.Show("Verifier les champs saisies ajout impossible donc impossible de terminer la saisie.", "Erreur Champs", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)
                    Exit Sub
                    'Pas vide et des saisies
                Else
                    myUser.DTArrivee.Rows.Add(TbScan.Text, TbQtité.Text)
                    Dim sortieToSave = createSortie(TbScan.Text, TbQtité.Text)
                    listDesSorties.Add(sortieToSave)
                    insertSortieToFile(sortieToSave)
                    TbScan.Text = ""
                    TbQtité.Text = ""

                    WriteSQL()
                End If
            Else
                Debug.WriteLine("No Connection !")
                MessageBox.Show("Erreur connection, merci de vous rapprocher d'une borne Wifi ou de vous brancher sur un socle.", "Erreur Connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
                Exit Sub
            End If
            dcdEvent.Dispose()
            Me.Close()
        End If
    End Sub

    Private Sub WriteSQL()
        Dim listResTempo = VerifSortieBeforeSave(listDesSorties)

        For Each l In listResTempo
            listDesSorties.Add(l)
        Next

        'Ecrire les sorties dans SQL
        If saveListSortieToSQL(listDesSorties) Then
            MessageBox.Show("Sauvegarde effectuée avec succés", "Sauvegarde OK", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1)
            myUser.DTArrivee.Clear()
            listDesSorties.Clear()
            File.Delete(cheminDossierFichier)
        Else
            MessageBox.Show("Erreur lors de l'écriture en Base de Donnée. Merci de réessayer ultérieurement", "Erreur BDD", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            dcdEvent.Dispose()
            Me.Close()
        End If
    End Sub

    Private Function VerifSortieBeforeSave(ByVal list As List(Of Sortie)) As List(Of Sortie)
        Dim listCopy As New List(Of Sortie)
        For Each elemToCopy As Sortie In list
            listCopy.Add(elemToCopy)
        Next
        Dim listNew As New List(Of Sortie)
        For Each sToCheck As Sortie In listCopy
            'sToCheck = New Sortie("", "31676", "31676/110/129", "", "", 425, 425, "23-02-2018", 110)
            If sToCheck.IONumLot = "" Or sToCheck.IONumOF = "" Then
                'Reremplir les champs
                Dim newSortie As Sortie = createSortie(sToCheck.IONumEtiq, sToCheck.IOQtiteRea)

                'Supprime la sortie existante de la liste et du fichier excel
                deleteSortieInFileAndInList(sToCheck)

                'Ajout de la nouvelle sortie dans la liste et dans le fichier excel
                'listDesSorties.Add(newSortie)
                listNew.Add(newSortie)
                insertSortieToFile(newSortie)
            End If
        Next
        Return listNew
    End Function

    Private Sub BtnDelDétach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelDétach.Click
        TbScan.Text = ""
        TbQtité.Text = ""
        TbScan.Focus()
    End Sub

    Private Sub BtnDelQtité_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelQtité.Click
        TbQtité.Text = ""
        TbQtité.Focus()
    End Sub
End Class