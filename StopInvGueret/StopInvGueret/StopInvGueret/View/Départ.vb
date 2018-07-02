Imports Datalogic.API
Imports System.Data
Imports System.Data.SqlClient

Public Class Départ
    Private WithEvents dcdEvent As DecodeEvent
    Private hDcd As DecodeHandle

    Private Sub Départ_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dateSaisie = Now

        loadFullDecodeur(hDcd, Me, dcdEvent)
        If isConnectionOK(Nothing) Then
            Dim databMngr As New DataBaseManager
            If databMngr.checkIfCrashOrPb() Then
                Dim confirm As New DialogBoxForm
                confirm.textToShow = "Voulez-vous reprendre la dernière saisie en cours ou démarrer une nouvelle saisie ?"
                confirm.okText = "Reprendre"
                confirm.cancelText = "Nouvelle Saisie"
                confirm.ShowDialog()

                If confirm.userButtonClickChoice.Contains("Reprendre") Then
                    idUniqueJour = initIDUniqueJour()
                    listDesDepartExp.Clear()
                    recupDepartForReprendre(listDesDepartExp)
                    poidsCamionActuel = recupPoidsCamion()
                    LbNewPoids.Text = poidsCamionActuel
                    LbOldPoids.Text = LbNewPoids.Text

                ElseIf confirm.userButtonClickChoice.Contains("Nouvelle Saisie") Then
                    idUniqueJour = initIDUniqueJour() + 1
                    listDesDepartExp.Clear()
                    databMngr.majFlagDebutSaisieSiPrecedentCrashed()
                Else

                End If
            Else
                listDesDepartExp.Clear()
                idUniqueJour = initIDUniqueJour() + 1
            End If
            TbSaisirScan.Focus()
        Else
            MessageBox.Show("Utilisation impossible hors-connection. Verifier la connection Wifi et réessayer.", "HORS CONNECTION", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1)
            dcdEvent.Dispose()
            Me.Close()
        End If
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
                    If ctrl.Name = "TbSaisirScan" Then
                        Dim regex As System.Text.RegularExpressions.Regex
                        regex = New System.Text.RegularExpressions.Regex("(^(G|A)([0-9]+))|([0-9]+\/[0-9]{1,3}\/[0-9]{1,3})|([0-9]+\/[0-9]{1,3}\-[0-9]{1,2}\/[0-9]{1,3})")
                        If regex.IsMatch(dcdData) Then
                            affichePointVert()
                            TbSaisirScan.Text = dcdData
                            TbSaisirQtite.Enabled = False

                            'Check si
                            verifIfCV(TbSaisirScan.Text)

                            setQtite()
                            poidsCamionActuel += Convert.ToInt32(TbSaisirPoids.Text)
                            'poids conteneur classique
                            'poidsCamionActuel += 50

                            LbNewPoids.Text = poidsCamionActuel.ToString
                            'calcPoidsTheorique()
                            TbSaisirQtite.Enabled = True
                        Else
                            TbSaisirScan.Focus()
                        End If
                    End If
                    Exit For
                End If
            Next
        Else
            Exit Sub
        End If
    End Sub

    Private Sub setQtite()
        Dim res = getQtiteFromNumEtiqInTableIO(TbSaisirScan.Text)
        TbSaisirQtite.Text = res.ToString
        If TbSaisirQtite.Text = "A Saisir" Or TbSaisirQtite.Text = "" Then


            'recup quantite theorique
            TbSaisirQtite.Text() = qtiteTheoriqueCont(TbSaisirScan.Text)


            TbSaisirQtite.Focus()
        Else
            TbSaisirPoids.Focus()
        End If
    End Sub

    Private Sub verifIfCV(ByVal numEtiquette As String)
        Dim res = getCVOrNot(numEtiquette)
        If res = 42 Or res = 0 Then
            CbControleV.Checked = False
        ElseIf res = 1 Then
            CbControleV.Checked = True
            CbControleV.Enabled = False
        Else
            CbControleV.Checked = False
        End If
    End Sub

    Private Sub calcPoidsTheorique()
        Dim qtite = TbSaisirQtite.Text
        Dim noProd As String = ""
        Dim noOp As String = ""
        Dim numEtiq As String = ""
        numEtiq = TbSaisirScan.Text
        'getElemForPoidsUFromIO(TbSaisirScan.Text, noProd, noOp)

        Dim numE = numEtiq.Split("/")
        If numE.Length = 3 Then
            noOp = numE(1)
            noProd = getNoProdFromNoOF(numE(0))
            poidsUnitaireInstantT = findPoidUnitaire(noProd, noOp)
        Else
            poidsUnitaireInstantT = "errG"
        End If

        If poidsUnitaireInstantT.ToString = "err" Then
            setDTPoidsU(noProd)
            Dim newNoOp As String = ""
            newNoOp = noOp.Split("-")(0)
            delHGSaufScan(newNoOp)

            myUser.DTPoidsUnitaire.DefaultView.Sort = "NoOp DESC"
            myUser.DTPoidsUnitaire = myUser.DTPoidsUnitaire.DefaultView.ToTable

            Debug.WriteLine("Res : " & myUser.DTPoidsUnitaire.Rows(1)(1))
            Dim newOP = myUser.DTPoidsUnitaire.Rows(1)(1)
            poidsUnitaireInstantT = findPoidUnitaire(noProd, newOP)
            'Rechercher avec la nouvelle op ci dessus
            TbSaisirPoids.Text = calcPoidsTotal(qtite, poidsUnitaireInstantT)

        ElseIf poidsUnitaireInstantT.ToString = "errG" Then
            TbSaisirPoids.Text = "A Saisir"
        Else
            TbSaisirPoids.Text = calcPoidsTotal(qtite, poidsUnitaireInstantT)
        End If
        'TbSaisirPoids.Focus()
    End Sub

    Private Function verifChampSaisie() As Boolean
        If (TbSaisirPoids.Text = "" Or (TbSaisirQtite.Text = "" Or TbSaisirQtite.Text = "0" Or TbSaisirQtite.Text = "A Saisir") Or (TbSaisirPoids.Text = "" Or TbSaisirPoids.Text = "0" Or TbSaisirPoids.Text = "A Saisir")) Then
            Return False
        End If
        Return True
    End Function

    Private Sub resetChamps()
        TbSaisirScan.Text = ""
        TbSaisirQtite.Text = ""
        TbSaisirPoids.Text = ""
        CbControleV.Enabled = True
        CbControleV.Checked = False
        TbSaisirScan.Focus()
    End Sub

#Region "Old"
    ''Changer le visuel pour qu'il y ait le poids de saisie pour chaque scan de départ

    'Private Sub Départ_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    'Liaison de la dataGrid avec la dataTable'
    '    DGDépart.DataSource = myUser.getDTDépart

    '    'Permet d'avoir des colonnes dont la taille est redéfinie, sinon c'est impossible de changer la taille des colonnes'
    '    Dim tableStyle As New DataGridTableStyle

    '    'Redimensionnement des colonnes'
    '    If isScreenVGA() Then
    '        initSizeColumnVGA(tableStyle)
    '    Else
    '        initSizeColumn(tableStyle)
    '    End If
    '    DGDépart.TableStyles.Add(tableStyle)

    '    'On enleve la selection de base de la première case'
    '    DGDépart.CurrentCell = Nothing
    'End Sub

    'Private Sub BTerm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.Close()
    'End Sub

    '''' <summary>
    '''' Fonction appelée lors du scan d'un code barre, afin d'associé le codebarre au bon TF
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub dcdEvent_Scanned(ByVal sender As System.Object, ByVal e As DecodeEventArgs) Handles dcdEvent.Scanned
    '    Dim cID As CodeId = CodeId.NoData
    '    Dim dcdData As String = ""
    '    Dim bBadRead As Boolean = False
    '    Dim noof As String = ""
    '    Try
    '        dcdData = hDcd.ReadString(e.RequestID, cID)
    '    Catch ex As Exception
    '        MessageBox.Show("Problème lors de la lecture.")
    '        bBadRead = True
    '    End Try

    '    If Not bBadRead Then
    '        For Each ctrl As Control In Me.Controls()
    '            If (dcdData.StartsWith("AE") And dcdData.Length < 10 And dcdData.Length > 5 And IsNumeric(dcdData.Substring(2))) Or (dcdData.Length < 3 And IsNumeric(dcdData)) Then
    '                '
    '            ElseIf dcdData.StartsWith(PdCFourG) Then
    '                '
    '            Else
    '                affichePointVert()
    '                myUser.DTDépart.Rows.Add(dcdData)
    '                'TODO
    '                'Ecrire auto en BDD
    '                Exit For
    '            End If
    '        Next
    '    Else
    '        Exit Sub
    '    End If
    'End Sub

    'Private Sub initSizeColumn(ByRef tableStyle As DataGridTableStyle)
    '    For Each item As DataColumn In myUser.getDTArrivee.Columns
    '        Dim col As New DataGridTextBoxColumn
    '        col.MappingName = item.ColumnName

    '        If item.ColumnName.Equals("NoEtiq") Then
    '            col.Width = 130
    '            col.HeaderText = item.ColumnName
    '        End If

    '        tableStyle.GridColumnStyles.Add(col)
    '    Next

    '    For Each vBar As VScrollBar In DGDépart.Controls.OfType(Of VScrollBar)()
    '        ' Evite le chevauchement des scrollBars verticale et horizontale
    '        vBar.Width = 25
    '        vBar.Height = DGDépart.Height - 25
    '    Next

    '    For Each hBar As HScrollBar In DGDépart.Controls.OfType(Of HScrollBar)()
    '        hBar.Height = 25
    '    Next
    'End Sub

    'Private Sub initSizeColumnVGA(ByRef tableStyle As DataGridTableStyle)
    '    For Each item As DataColumn In myUser.getDTArrivee.Columns
    '        Dim col As New DataGridTextBoxColumn
    '        col.MappingName = item.ColumnName

    '        If item.ColumnName.Equals("NoEtiq") Then
    '            col.Width = 130 * 2
    '            col.HeaderText = item.ColumnName
    '        End If
    '        tableStyle.GridColumnStyles.Add(col)
    '    Next

    '    For Each vBar As VScrollBar In DGDépart.Controls.OfType(Of VScrollBar)()
    '        ' Evite le chevauchement des scrollBars verticale et horizontale
    '        vBar.Width = 25 * 2
    '        vBar.Height = DGDépart.Height - (25 * 2)
    '    Next

    '    For Each hBar As HScrollBar In DGDépart.Controls.OfType(Of HScrollBar)()
    '        hBar.Height = 25 * 2
    '    Next
    'End Sub

    'Private Sub Départ_Deactivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Deactivate
    '    dcdEvent.Dispose()
    'End Sub

    'Private Sub Départ_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.GotFocus
    '    loadFullDecodeur(hDcd, Me, dcdEvent)
    'End Sub

    'Private Sub BtnSuppr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If myUser.getDTDépart.Rows.Count <> 0 Then
    '        If DGDépart.CurrentRowIndex = DGDépart.CurrentCell.RowNumber And DGDépart.IsSelected(DGDépart.CurrentRowIndex) Then
    '            Dim iNB = MsgBox("Voulez-vous supprimer cette ligne ?", vbYesNo + vbQuestion, "Suppression")
    '            If iNB = MsgBoxResult.Yes Then
    '                Dim row As DataRow
    '                row = myUser.getDTDépart.Rows(DGDépart.CurrentRowIndex)
    '                myUser.DTDépart.Rows.Remove(row)
    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Sub DGDépart_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If DGDépart.CurrentCell.RowNumber = DGDépart.CurrentRowIndex Then
    '        DGDépart.Select(DGDépart.CurrentRowIndex)
    '    End If
    'End Sub
#End Region

    Private Sub BtnDelScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelScan.Click
        TbSaisirScan.Text = ""
        TbSaisirQtite.Text = ""
        CbControleV.Enabled = True
        CbControleV.Checked = False
        If TbSaisirPoids.Text = "" Then
            poidsCamionActuel -= 0
        Else
            poidsCamionActuel -= Convert.ToInt32(TbSaisirPoids.Text)
            TbSaisirPoids.Text = ""
        End If
        LbNewPoids.Text = poidsCamionActuel
        TbSaisirScan.Focus()
    End Sub

    Private Sub BtnDelQtite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelQtite.Click
        TbSaisirQtite.Text = ""
        TbSaisirQtite.Focus()
    End Sub

    Private Sub BtnDelPoids_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelPoids.Click
        If TbSaisirPoids.Text = "" Then

        Else
            poidsCamionActuel -= Convert.ToInt32(TbSaisirPoids.Text)
            LbNewPoids.Text = poidsCamionActuel
            TbSaisirPoids.Text = ""
        End If
        TbSaisirPoids.Focus()
    End Sub

    Private Sub TbSaisirQtite_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TbSaisirQtite.TextChanged
        Dim regex As System.Text.RegularExpressions.Regex
        regex = New System.Text.RegularExpressions.Regex("[0-9]+((\.)?[0-9]+)?")
        If regex.IsMatch(TbSaisirQtite.Text) And poidsUnitaireInstantT.ToString <> "err" Then
            calcPoidsTheorique()
        End If
    End Sub

    Private Sub BtnQtiteAuto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnQtiteAuto.Click
        setQtite()
    End Sub

    Private Sub BtnCont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCont.Click
        Dim dbMngr As New DataBaseManager
        Dim connStatus = ""
        If isConnectionOK(connStatus) Then
            If verifChampSaisie() Then
                Dim numEti As String = TbSaisirScan.Text
                Dim qtite As String = TbSaisirQtite.Text
                Dim poids As String = TbSaisirPoids.Text
                Dim isCV As Boolean = CbControleV.CheckState

                myUser.DTDépart.Rows.Add(numEti, qtite, poids, isCV)
                insertFakeEntreInIO(numEti)
                Dim createdDep = createDepartExp(numEti, qtite, poids, isCV)
                Dim index = verifExistDep(createdDep)
                If index = -1 Then
                    listDesDepartExp.Add(createdDep)
                Else
                    fusionDepartExp(index, createdDep)
                End If

                dbMngr.insertOrUpdtExp(listDesDepartExp(verifExistDep(createdDep)))

                Dim insertNoEtiq As New SqlCommand("Insert into FICHE_EXP_NOETIQ (NOOF, NOOP, NOETIQ, IDUNIQUE, QUANTITE) values ('" & createdDep.NoOf & "', '" & createdDep.NoOp & "', '" & createdDep.NumEtiq & "', " & idUniqueJour & ", " & createdDep.Quantite & ")", New SqlConnection(connS3SQL))
                insertNoEtiq.CommandTimeout = 2

                insertNoEtiq.Connection.Open()
                insertNoEtiq.ExecuteNonQuery()
                insertNoEtiq.Connection.Close()


                Dim frmValid As New ValideImg
                frmValid.Show()
                frmValid.Refresh()
                pause(4)
                frmValid.Close()
                If CbControleV.Checked = False Then
                    poidsCamionActuel += 50
                End If
                LbNewPoids.Text = poidsCamionActuel
                LbOldPoids.Text = LbNewPoids.Text
                resetChamps()
            Else
                MessageBox.Show("Merci de bien saisir tout les champs", "Erreur Saisie", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            End If
        Else
            MessageBox.Show("Verifier la connection Wifi, fonctionnement impossible hors connection !", "Ajout impossible", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
        End If
    End Sub

    Private Sub BtnTerm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTerm.Click
        Dim dataM = New DataBaseManager
        If isConnectionOK(Nothing) Then
            Dim res = MessageBox.Show("Attention vous êtes sur le point d'arreter la saisie des départs ! Êtes-vous sur de vouloir quitter ?", "Attention fin départ", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            If res = Windows.Forms.DialogResult.Yes Then
                dataM.majFlagFinSaisie()

                dcdEvent.Dispose()
                Me.Close()
            End If
        Else
            Dim resH = MessageBox.Show("Attention vous êtes hors connection, le conteneur en cours sera perdu. Reconnecter vous au Wifi pour continuer convenablement. Êtes-vous sur de vouloir quitter ?", "Attention fin HORS CO", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
            If resH = Windows.Forms.DialogResult.Yes Then
                dcdEvent.Dispose()
                Me.Close()
            End If
        End If

    End Sub

    Private Sub BtnVoirRecap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnVoirRecap.Click
        Dim frmRecapDepart As New RecapDépart
        dcdEvent.Dispose()
        frmRecapDepart.ShowDialog()
        loadFullDecodeur(hDcd, Me, dcdEvent)
        LbOldPoids.Text = poidsCamionActuel
        LbNewPoids.Text = poidsCamionActuel
        TbSaisirScan.Focus()
    End Sub

    Private Sub Panel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel1.Click
        If CbControleV.Enabled = True Then
            If CbControleV.Checked = True Then
                CbControleV.Checked = False
                'poidsCamionActuel -= 50
                LbNewPoids.Text = poidsCamionActuel
                Exit Sub
            End If
            CbControleV.Checked = True
            'poidsCamionActuel += 50
            LbNewPoids.Text = poidsCamionActuel
        End If
    End Sub

    'Private Sub CbControleV_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CbControleV.CheckStateChanged
    '    If CbControleV.Checked = True Then
    '        poidsCamionActuel -= 50
    '    Else
    '        poidsCamionActuel += 50
    '    End If
    '    LbNewPoids.Text = poidsCamionActuel
    'End Sub

    'Private Sub TbSaisirPoids_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TbSaisirPoids.TextChanged
    '    If TbSaisirPoids.Text = "" Then
    '        poidsCamionActuel = Convert.ToInt32(LbOldPoids.Text) + 0
    '        LbNewPoids.Text = poidsCamionActuel.ToString
    '        Exit Sub
    '    End If
    '    poidsCamionActuel = Convert.ToInt32(LbOldPoids.Text) + Convert.ToInt32(TbSaisirPoids.Text)
    '    LbNewPoids.Text = poidsCamionActuel.ToString
    'End Sub
End Class