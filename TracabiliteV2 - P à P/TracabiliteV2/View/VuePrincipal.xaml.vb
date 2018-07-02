Public Class VuePrincipal



    Private Sub BtnRech_Click(sender As Object, e As RoutedEventArgs)
        Dim result As New VueResultat
        Dim listRes As New List(Of Etiquette)
        myEtiqManager = New EtiquetteManager()

        If Not isValideEtiquette(TbNumEtiq.Text) Then
            MsgBox("Attention le format de l'étiquette est invalide", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        currentNumEtiq = TbNumEtiq.Text
        myEtiq = New Etiquette(currentNumEtiq)

        Dim cpt = myEtiq.IsInIO()
        Select Case cpt
            Case 0
                MsgBox("Cette étiquette n'existe pas en base de donnée", MsgBoxStyle.Exclamation)
                Exit Sub
            Case 1
                Dim etiqType = myEtiq.FindTypeOfEtiq()


                Select Case etiqType
                    Case "Entree"
                        'Gerer cas Que les suivants

                        'On va afficher les sorties uniquement car plus d'infos que les entrées
                        'Sortie de telle ou telle machine avec les infos

                        Dim etiqDepart = New Etiquette(myEtiq.getNumEtiq)
                        listRes.Add(etiqDepart)
                        Dim etiqSortieSuiv = getSortieSuivFromEntree(etiqDepart.getNumMachine, etiqDepart.getNumOf, etiqDepart.getDateAvecHeure)

                        If etiqSortieSuiv Is Nothing Then
                            Exit Select
                        End If

                        If Not listRes.Contains(etiqSortieSuiv) Then
                            listRes.Add(etiqSortieSuiv)
                        End If
                        Dim etiqEntreeSuiv = getEntreeSuivFromSortie(etiqSortieSuiv.getNumEtiq)

                        'Quand on sort d'ici il n'y a plus de suivants
                        While Not etiqEntreeSuiv Is Nothing
                            etiqSortieSuiv = getSortieSuivFromEntree(etiqEntreeSuiv.getNumMachine, etiqEntreeSuiv.getNumOf, etiqEntreeSuiv.getDateAvecHeure)
                            If etiqSortieSuiv Is Nothing Then
                                Exit While
                            End If
                            listRes.Add(etiqSortieSuiv)
                            etiqEntreeSuiv = getEntreeSuivFromSortie(etiqSortieSuiv.getNumEtiq)
                        End While

                    Case "Sortie"
                        'Gerer cas Que les précédents

                        Dim etiqSortiePrec = getSortiePrecFromEntree(myEtiq.getNumEtiq)
                        If etiqSortiePrec Is Nothing Then
                            listRes.Insert(0, myEtiq)
                            Exit Select
                        End If
                        If Not listRes.Contains(etiqSortiePrec) Then
                            listRes.Insert(0, etiqSortiePrec)
                        End If
                        Dim etiqEntreePrec = getEntreePrecFromSortie(etiqSortiePrec.getNumMachine, etiqSortiePrec.getNumOf, etiqSortiePrec.getDateAvecHeure)

                        'Quand on sort d'ici il n'y a plus d'antécédent
                        While Not etiqEntreePrec Is Nothing
                            etiqSortiePrec = getSortiePrecFromEntree(etiqEntreePrec.getNumEtiq)
                            If etiqSortiePrec Is Nothing Then
                                listRes.Insert(0, etiqEntreePrec)
                                Exit While
                            Else
                                listRes.Insert(0, etiqSortiePrec)
                            End If
                            etiqEntreePrec = getEntreePrecFromSortie(etiqSortiePrec.getNumMachine, etiqSortiePrec.getNumOf, etiqSortiePrec.getDateAvecHeure)
                        End While

                End Select

            Case 2
                'Suivant et Précédent

                'On va afficher les sorties uniquement car plus d'infos que les entrées
                'Sortie de telle ou telle machine avec les infos

                'On commence par les précédents :

                Dim etiqSortiePrec = getSortiePrecFromEntree(myEtiq.getNumEtiq)
                If etiqSortiePrec Is Nothing Then
                    GoTo GOTOSUIV
                End If
                If Not listRes.Contains(etiqSortiePrec) Then
                    listRes.Insert(0, etiqSortiePrec)
                End If
                Dim etiqEntreePrec = getEntreePrecFromSortie(etiqSortiePrec.getNumMachine, etiqSortiePrec.getNumOf, etiqSortiePrec.getDateAvecHeure)

                'Quand on sort d'ici il n'y a plus d'antécédent
                While Not etiqEntreePrec Is Nothing
                    etiqSortiePrec = getSortiePrecFromEntree(etiqEntreePrec.getNumEtiq)
                    If etiqSortiePrec Is Nothing Then
                        listRes.Insert(0, etiqEntreePrec)
                        Exit While
                    Else
                        listRes.Insert(0, etiqSortiePrec)
                    End If
                    etiqEntreePrec = getEntreePrecFromSortie(etiqSortiePrec.getNumMachine, etiqSortiePrec.getNumOf, etiqSortiePrec.getDateAvecHeure)
                End While

                '------------------------------------------------------------------------------------------------------------------
                'On Calcule les suivants
                '------------------------------------------------------------------------------------------------------------------
GOTOSUIV:
                Dim etiqDepart = New Etiquette(myEtiq.getNumEtiq)

                Dim etiqSortieSuiv = getSortieSuivFromEntree(etiqDepart.getNumMachine, etiqDepart.getNumOf, etiqDepart.getDateAvecHeure)

                If etiqSortieSuiv Is Nothing Then
                    Exit Select
                End If

                If Not listRes.Contains(etiqSortieSuiv) Then
                    listRes.Add(etiqSortieSuiv)
                End If
                Dim etiqEntreeSuiv = getEntreeSuivFromSortie(etiqSortieSuiv.getNumEtiq)

                'Quand on sort d'ici il n'y a plus de suivants
                While Not etiqEntreeSuiv Is Nothing
                    etiqSortieSuiv = getSortieSuivFromEntree(etiqEntreeSuiv.getNumMachine, etiqEntreeSuiv.getNumOf, etiqEntreeSuiv.getDateAvecHeure)
                    If etiqSortieSuiv Is Nothing Then
                        Exit While
                    End If
                    listRes.Add(etiqSortieSuiv)
                    etiqEntreeSuiv = getEntreeSuivFromSortie(etiqSortieSuiv.getNumEtiq)
                End While

                'On redemarre depuis le debut
                '¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤¤

                putDataTableIntoListGlobal(myEtiq.getNumOf)

                listOfListOfEtiquette = createTracabilite()

                'Dim listResSortie = getListSortieFromListEntree()
                Dim compteur As Integer = 0
                For Each listEtiq As List(Of Etiquette) In listOfListOfEtiquette
                    compteur += 1
                    For Each etiq As Etiquette In listEtiq
                        Debug.WriteLine(compteur & " --- " & etiq.getNumEtiq)
                    Next
                Next


        End Select

        'For Each x As Etiquette In listRes


        '    Debug.WriteLine("AffRes --- " & x.getNumEtiq)
        'Next

        result.ShowDialog()
    End Sub

    Private Sub BtnQuitter_Click(sender As Object, e As RoutedEventArgs)
        System.Windows.Application.Current.Shutdown()
    End Sub

    Private Sub TbNumEtiq_KeyDown(sender As Object, e As KeyEventArgs) Handles TbNumEtiq.KeyDown
        If e.Key = Key.Enter Then
            BtnRech_Click(sender, e)
        End If
    End Sub
End Class
