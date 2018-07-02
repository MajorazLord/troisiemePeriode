Imports Datalogic.API
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.WindowsMobile
Imports System.Drawing

Module Fonctions

#Region "Tools For Douchette"
    Public Sub loadFullDecodeur(ByRef hDcd As DecodeHandle, ByRef ecran As Form, ByRef dcdEvent As DecodeEvent)
        Try
            'DecodeHandle correspond à l'identification du decodeur de code barre'
            hDcd = New DecodeHandle(DecodeDeviceCap.Exists Or DecodeDeviceCap.Barcode)
        Catch ex As DecodeException
            MessageBox.Show("Impossible de charger le décodeur de code barre.", "Chargement")
            ecran.Close()
        End Try

        'Determine le type de requete envoyé par le scanner'
        Dim reqType As DecodeRequest = CType(1, DecodeRequest) Or DecodeRequest.PostRecurring

        'Necessite aussi <l'ecran> afin de lier l'evenement de lecture d'un code barre à une fenetre'
        dcdEvent = New DecodeEvent(hDcd, reqType, ecran)
    End Sub

    Public Function isDeviceOnSocle() As Boolean
        Return Datalogic.API.Device.GetIsInCradle()
    End Function

    Public Sub affichePointVert()
        Device.EnableLED(Device.LEDs.GreenSpot, True)
        pause(1)
        Device.EnableLED(Device.LEDs.GreenSpot, False)
    End Sub

    Public Function isScreenVGA() As Boolean
        Return (currentScreenHeight = HEIGHT_SCREEN_VGA And currentScreenWidth = WIDTH_SCREEN_VGA)
    End Function

    Public Sub pause(ByVal coef As Integer)
        Threading.Thread.Sleep(300 * coef)
    End Sub

    Public Function isConnectionOK(ByRef status) As Boolean
        If isDeviceOnSocle() Then
            Dim testConnexion As New SqlCommand("", New SqlConnection(connS3SQL))
            testConnexion.CommandTimeout = 2
            Try
                testConnexion.Connection.Open()
                testConnexion.Connection.Close()
                status = "Socle"
                Return True
            Catch 'Provient de test.connection.open'
                testConnexion.Connection.Close()
                Return False
            End Try
        ElseIf Microsoft.WindowsMobile.Status.SystemState.ConnectionsNetworkCount <> 0 Then
            Dim testConnexion As New SqlCommand("", New SqlConnection(connS3SQL))
            testConnexion.CommandTimeout = 2
            Try
                testConnexion.Connection.Open()
                testConnexion.Connection.Close()
                status = "Wifi"
                Return True
            Catch 'Provient de test.connection.open'
                testConnexion.Connection.Close()
                Return False
            End Try
        End If
        Return False
    End Function
#End Region

#Region "BDD"

    Public Function initIDUniqueJour() As Integer
        Dim req As New SqlClient.SqlCommand("SELECT IDUNIQUE FROM FICHE_EXPEDITION ORDER BY IDUNIQUE DESC", New SqlConnection(connS3SQL))
        req.CommandTimeout = 2
        Try
            req.Connection.Open()
            Dim result = req.ExecuteReader
            If result.Read Then
                Return result(0)
            End If
        Catch ex As Exception
            Return 0
            req.Connection.Close()
        End Try
        req.Connection.Close()
    End Function


    'Recupère la quantité dans IO en fonction d'un code d'étiquete
    Public Function getQtiteFromNumEtiqInTableIO(ByVal numEtiq As String) As String
        Dim req As New SqlClient.SqlCommand("SELECT IOQUANTITEREALISE FROM IO WHERE IONUMETIQ = '" & numEtiq & "' ", New SqlConnection(connS3SQL))
        req.CommandTimeout = 2
        Try
            req.Connection.Open()
            Dim result = req.ExecuteReader
            If result.Read Then
                If result(0) Is DBNull.Value Then
                    Return "A Saisir"
                End If
                Return result(0)
            Else
                Return "A Saisir"
            End If
        Catch ex As Exception
            Return "A Saisir"
            req.Connection.Close()
        End Try
        req.Connection.Close()
    End Function

    Public Sub getElemForPoidsUFromIO(ByVal numEtiq As String, ByRef noProd As String, ByRef noOp As String)
        Dim req As New SqlClient.SqlCommand("SELECT IONOPROD, IONUMOP FROM IO WHERE IONUMETIQ = '" & numEtiq & "' ", New SqlConnection(connS3SQL))
        req.CommandTimeout = 2
        Try
            req.Connection.Open()
            Dim result = req.ExecuteReader
            If result.Read Then
                noProd = result(0)
                noOp = result(1)
            Else
                noProd = ""
                noOp = ""
            End If
        Catch ex As Exception
            noProd = ""
            noOp = ""
            req.Connection.Close()
        End Try
        req.Connection.Close()
    End Sub

    Public Function getNumChargeFromIO(ByVal numEtiq As String) As String
        Dim req As New SqlClient.SqlCommand("SELECT IONUMCHARGE FROM IO WHERE IONUMETIQ = '" & numEtiq & "' ", New SqlConnection(connS3SQL))
        req.CommandTimeout = 2
        Try
            req.Connection.Open()
            Dim result = req.ExecuteReader
            If result.Read Then
                Return result(0)
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
            req.Connection.Close()
        End Try
        req.Connection.Close()
    End Function

    Public Function findPoidUnitaire(ByVal noProd As String, ByVal noOp As String)
        If noOp.Contains("-") Then
            Dim index = noOp.IndexOf("-")

            Dim subS = noOp.Substring(index)

            Dim resF = noOp.Remove(index, subS.Count)

        End If

        Dim req As New SqlClient.SqlCommand("SELECT DISTINCT POINWE FROM MPDOPE WHERE POPRNO = '" & noProd & "' AND POOPNO = '" & noOp & "' AND POSTRT = '001'", New SqlConnection(connS3BPWFC))
        req.CommandTimeout = 2
        Try
            req.Connection.Open()
            Dim result = req.ExecuteReader
            If result.Read Then
                Return result(0)
            Else
                Return "err"
            End If
        Catch ex As Exception
            Return "err"
            req.Connection.Close()
        End Try
        req.Connection.Close()

        Return ""
    End Function


    ' Permet de récupérer le n° de produit grace au n° OF
    Public Function getNoProdFromNoOF(ByVal Noof As String) As String
        Dim req As New SqlClient.SqlCommand("SELECT DISTINCT D.NOPROD FROM DEMANDE AS D INNER JOIN DETAIL_ETIQ AS DET ON D.NOLOT = DET.NOLOT WHERE (DET.NOOF = '" & Noof & "') ", New SqlConnection(connGOPAL))
        req.CommandTimeout = 2
        Try
            req.Connection.Open()
            Dim result = req.ExecuteReader
            If result.Read Then
                Return result(0)
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
            req.Connection.Close()
        End Try
        req.Connection.Close()
    End Function


    'Permet de recupérer le nom du produit selon l'OF
    Public Function getABVFromNoProd(ByVal noProd As String) As String
        Dim req As New SqlCommand("SELECT Name FROM DT_Articles where [Item number]= '" & noProd & "'", New SqlConnection(connT3BPW))
        req.CommandTimeout = 2
        Try
            req.Connection.Open()
            Dim lec = req.ExecuteReader
            If lec.Read Then
                Return lec.GetString(0)
            End If
            Return ""
            req.Connection.Close()
        Catch ex As Exception
            Return ""
            req.Connection.Close()
        End Try
    End Function

    'Permet de récuperer le noLot en fonction de l'of, l'op et le numEtiq
    Public Function getDetailsEtiquette(ByVal noof As Integer, ByVal noop As String, ByVal numEtiq As String) As String
        Dim numE = numEtiq.Split("/")
        Dim req As New SqlCommand("SELECT NOLOT FROM DEMANDE WHERE NOLOT IN (SELECT DISTINCT NOLOT FROM GOPAL.dbo.DETAIL_ETIQ WHERE NOOF=" & noof & " AND NOOP='" & noop & "' AND NO_ETIQ=" & numE(2) & ")", New SqlConnection(connGOPAL))
        req.CommandTimeout = 2
        Try
            req.Connection.Open()
            Dim lec = req.ExecuteReader
            If lec.Read Then
                Return lec.GetString(0)
            End If
            Return ""
            req.Connection.Close()
        Catch ex As Exception
            Return ""
            req.Connection.Close()
        End Try
    End Function

    ' Permet de récupérer le n° de lot si on a une etiquette GOPAL
    Public Sub getDetailEtiquetteGOPAL(ByVal nogopal As String, ByRef nolot As String, ByRef noof As String, ByRef noop As String)
        Dim req As New SqlClient.SqlCommand("select distinct nolot, noof, noop from detail_etiq where no_gopal = '" & nogopal & "'", New SqlClient.SqlConnection(connGOPAL))
        req.CommandTimeout = 2
        Try
            req.Connection.Open()
            Dim result = req.ExecuteReader
            If result.Read Then
                nolot = result(0)
                noof = result(1)
                noop = result(2)
            Else
                nolot = ""
                noof = ""
                noop = 0
            End If
        Catch ex As Exception
            nolot = ""
            noof = ""
            noop = 0
            req.Connection.Close()
        End Try
        req.Connection.Close()
    End Sub

    Public Function save1SortieToSQL(ByVal s As Sortie) As Boolean
        Dim reqSaveSortie As New SqlCommand("Insert into IO (IOCODESAISIE, IONUMPOINTAGE, IOTYPE, IONUMMACH, IONUMLOT, IONUMOF, IONUMETIQ, IONOPROD, IOABVNOMPROD, IOQUANTITEREALISE, IOQUANTITETOTALE, IODATESCAN, IOBLOCKED, IONUMCHARGE, IONUMMATRICE, IONUMVAGUE, IONUMOP, IOPDCSUIVANT) values (" & Sortie.IOCodeSaisie & "," & Sortie.IONumPointage & ",'" & Sortie.IOType & "','" & Sortie.IONumMach & "','" & s.IONumLot & "','" & s.IONumOF & "','" & s.IONumEtiq & "','" & s.IONoProd & "','" & s.IOABVNomProd & "'," & s.IOQtiteRea & "," & s.IOQtiteTot & ",'" & s.IODateScan & "'," & Convert.ToByte(Sortie.IOBlocked) & ",'" & Sortie.IONumCharge & "','" & Sortie.IONumMatrice & "','" & Sortie.IONumVague & "','" & s.IONumOP & "',NULL)", New SqlConnection(connS3SQL))
        reqSaveSortie.CommandTimeout = 2

        Try
            reqSaveSortie.Connection.Open()
            reqSaveSortie.ExecuteNonQuery()
            reqSaveSortie.Connection.Close()
        Catch duplExe As SqlException
            If duplExe.Number = 2627 Then
                MessageBox.Show("La sortie dont le numéro d'étiquette est " & s.IONumEtiq & ", est déja présent dans la table. Ajout impossible.", "Erreur Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1)
                reqSaveSortie.Connection.Close()
                Return False
            End If
            MessageBox.Show("Une erreur s'est produite, l'élément ayant le numéro d'étiquette " & s.IONumEtiq & " n'a pas pu être ajouté a la base de donnée.", "Erreur Autre", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1)
            Return False
            reqSaveSortie.Connection.Close()
        End Try
        Return True
    End Function

    Public Function saveListSortieToSQL(ByVal listS As List(Of Sortie)) As Boolean
        Dim cpt As Integer = 0
        For Each s As Sortie In listS
            If Not save1SortieToSQL(s) Then
                cpt += 1
            End If
        Next
        If cpt <> 0 Then
            MessageBox.Show(cpt & " sorties sur " & listS.Count & " n'ont pas été saisie en BDD", "Resultat saisie")
        End If
        Return True
    End Function


#End Region


    Public Function createDepartExp(ByVal numEtiq As String, ByVal qtite As String, ByVal poids As String, ByVal flagcv As Boolean) As DépartExp
        Dim pieces As String 'IOABVNomProd
        Dim noProduit As String
        Dim noOf As Integer
        Dim noOp As String = ""
        Dim quantite As Integer
        Dim poidsNet As Integer
        Dim nbCont As Integer
        Dim remarque As String 'NoCharge
        Dim designOP As String


        quantite = qtite
        poidsNet = poids

        Dim numE = numEtiq.Split("/")
        If numE.Length = 3 Then
            noOf = numE(0)
            noOp = numE(1)
            nbCont = 1
            noProduit = getNoProdFromNoOF(noOf) 'OK
            pieces = getABVFromNoProd(noProduit) 'OK
            remarque = getNumChargeFromIO(numEtiq)
        Else
            If numEtiq.StartsWith("G") Or numEtiq.StartsWith("A") Then
                Dim nullV As String = ""
                getDetailEtiquetteGOPAL(numE(0), nullV, noOf, noOp) 'OK
                noProduit = getNoProdFromNoOF(noOf) 'OK
                pieces = getABVFromNoProd(noProduit) 'OK
                nbCont = 1
                remarque = getNumChargeFromIO(numEtiq)
            Else
                Return Nothing
            End If
        End If

        If (noOp.Contains("-")) Then
            designOP = getDesignOPHG(noOp, noOf, noProduit)
        Else
            designOP = getDesignOP(noOp, noOf, noProduit)
        End If


        Return New DépartExp(pieces, noProduit, noOf, noOp, quantite, poidsNet, nbCont, remarque, flagcv, numEtiq, designOP)
    End Function

    Public Function getDesignOPHG(ByVal noop As String, ByVal noof As String, ByVal noprod As String) As String
        Dim req As New SqlClient.SqlCommand("select DESIGNOP from HG_OP where NOPROD = '" & noprod & "' and NOOF = '" & noof & "' and NOOP = '" & noop & "'", New SqlClient.SqlConnection(connS3SQLMOVEOP))
        req.CommandTimeout = 2
        Try
            req.Connection.Open()
            Dim result = req.ExecuteReader
            If result.Read Then
                Return result(0)
            Else
                Return "-1"
            End If
        Catch ex As Exception
            Return "-1"
            req.Connection.Close()
        End Try
        req.Connection.Close()
    End Function

    Public Function getDesignOP(ByVal noop As String, ByVal noof As String, ByVal noprod As String) As String
        Dim req As New SqlClient.SqlCommand("SELECT VOOPDS FROM MWOOPE WHERE VOPRNO = '" & noprod & "' AND VOMFNO LIKE '%" & noof & "%' AND VOCONO = '052' AND VOFACI = '009' AND VOOPNO = '" & noop & "'", New SqlClient.SqlConnection(connS3BPWFC))
        req.CommandTimeout = 2
        Try
            req.Connection.Open()
            Dim result = req.ExecuteReader
            If result.Read Then
                Return result(0)
            Else
                Return "-1"
            End If
        Catch ex As Exception
            Return "-1"
            req.Connection.Close()
        End Try
        req.Connection.Close()
    End Function

    Public Sub fusionDepartExp(ByVal indexBase As Integer, ByVal valToCopy As DépartExp)
        Dim valBase As DépartExp = listDesDepartExp(indexBase)
        listDesDepartExp(indexBase).Quantite = valBase.Quantite + valToCopy.Quantite
        listDesDepartExp(indexBase).PoidsNet = valBase.PoidsNet + valToCopy.PoidsNet
        listDesDepartExp(indexBase).NbCont += 1
    End Sub

    Public Function verifExistDep(ByVal valToCheck As DépartExp) As Integer
        Return listDesDepartExp.FindIndex(Function(p) p.Equals(valToCheck))
    End Function

    Public Function verifExistDepInDTB(ByVal valToVerif As DépartExp) As Integer
        Dim req As New SqlClient.SqlCommand("select NBCONTENANT from FICHE_EXPEDITION where NOPROD = '" & valToVerif.NoProduit & "' and NOOF = '" & valToVerif.NoOf & "' and NOOP = '" & valToVerif.NoOp & "' and DATE = '" & Format(Now, "dd/MM/yyyy") & "' and IDUNIQUE = " & idUniqueJour, New SqlClient.SqlConnection(connS3SQL))
        req.CommandTimeout = 2
        Try
            req.Connection.Open()
            Dim result = req.ExecuteReader
            If result.Read Then
                Return result(0)
            Else
                Return "-1"
            End If
        Catch ex As Exception
            Return "-1"
            req.Connection.Close()
        End Try
        req.Connection.Close()

    End Function

    Public Function createSortie(ByVal numEtiquette As String, ByVal qtite As String) As Sortie
        Dim numLot As String = ""
        Dim numOF As String = ""
        Dim numEtiq As String
        Dim noProd As String = ""
        Dim abvNomProd As String = ""
        Dim qtiteRea As Integer
        Dim qtiteTot As Integer
        Dim dateScan As String
        Dim numOP As String = ""

        numEtiq = numEtiquette
        qtiteRea = qtite
        qtiteTot = qtite
        dateScan = Format(Now, "dd/MM/yyyy")

        Dim numE = numEtiquette.Split("/")
        If numE.Length = 3 Then
            numOF = numE(0) 'OK
            numOP = numE(1) 'OK
            numLot = getDetailsEtiquette(numOF, numOP, numEtiq) 'OK
            noProd = getNoProdFromNoOF(numOF) 'OK
            abvNomProd = getABVFromNoProd(noProd) 'OK
        Else
            If numEtiquette.StartsWith("G") Or numEtiq.StartsWith("A") Then
                getDetailEtiquetteGOPAL(numE(0), numLot, numOF, numOP) 'OK
                noProd = getNoProdFromNoOF(numOF) 'OK
                abvNomProd = getABVFromNoProd(noProd) 'OK


0:
            Else
                Return Nothing
            End If
        End If
        Return New Sortie(numLot, numOF, numEtiq, noProd, abvNomProd, qtiteRea, qtiteTot, dateScan, numOP)
    End Function

    Public Function calcPoidsTotal(ByVal qtite As String, ByVal poidsU As String) As String
        Dim poidsTotal As Decimal = (Convert.ToDecimal(qtite) * Convert.ToDecimal(poidsU)) / 1000
        Return Math.Round(poidsTotal)
    End Function

#Region "Excel"

    Public Sub createExcelSave()
        DeleteEmptyDirectory()
        'Creation du dossier StopInvGueret dans My Documents
        If Not Directory.Exists(cheminBase) Then
            Try
                Directory.CreateDirectory(cheminBase)
            Catch ex As Exception
                Debug.WriteLine(ex.Message())
            End Try

        End If

        'Creation du dossier du jour dans AuditQtiteGueret
        If Not Directory.Exists(cheminDossier) Then
            Try
                Directory.CreateDirectory(cheminDossier)
            Catch ex As Exception
                Debug.WriteLine(ex.Message())
            End Try

        End If

        'Creation du fichier du jour dans le dossier du jour
        If Not File.Exists(cheminDossierFichier) Then
            File.Create(cheminDossierFichier).Close()
        End If

        Dim sLigne As String
        Dim x As New System.IO.StreamWriter(cheminDossierFichier, False, System.Text.Encoding.UTF8)

        sLigne = Format(Now, "dd/MM/yyyy")
        x.WriteLine(sLigne)
        x.WriteLine("")
        sLigne = "IOCodeSaisie;IONumPointage;IOType;IONumMach;IONumLot;IONumOF;IONumEtiq;ION°Prod;IOABVProd;IOQuantitéRealise;IOQuantitéTotale;IODateScan;IOBlocked;IONumCharge;IONumMatrice;IONumVague;IONumOP;IOPdcSuivant"
        x.WriteLine(sLigne)
        x.Close()
    End Sub

    Public Sub DeleteEmptyDirectory()
        Dim x = System.IO.Directory.GetDirectories(cheminBase)
        For Each a In x
            Try
                DeleteDirectory(a.ToString)
            Catch ex As System.IO.IOException
                Debug.WriteLine("Pas vide")
            End Try
        Next
    End Sub

    Public Sub DeleteDirectory(ByVal target_dir As String)
        Dim files = Directory.GetFiles(target_dir)
        Dim dirs = Directory.GetDirectories(target_dir)

        If files.Length <> 0 And dirs.Length <> 0 Then
            Exit Sub
        End If
        Directory.Delete(target_dir, False)
    End Sub



    Public Sub insertSortieToFile(ByVal s As Sortie)
        Dim sLigne As String
        Dim x As New System.IO.StreamWriter(cheminDossierFichier, True, System.Text.Encoding.UTF8)
        sLigne = Sortie.IOCodeSaisie & ";" & Sortie.IONumPointage & ";" & Sortie.IOType & ";" & Sortie.IONumMach & ";" & s.IONumLot & ";" & s.IONumOF & ";" & s.IONumEtiq & ";" & s.IONoProd & ";" & s.IOABVNomProd & ";" & s.IOQtiteRea & ";" & s.IOQtiteTot & ";" & s.IODateScan & ";" & Sortie.IOBlocked & ";" & Sortie.IONumCharge & ";" & Sortie.IONumMatrice & ";" & Sortie.IONumVague & ";" & s.IONumOP & ";" & Sortie.IOPdcSuivant
        x.WriteLine(sLigne)
        x.Close()
    End Sub

    Public Sub insertListSortieToFile(ByVal listS As List(Of Sortie))
        For Each sortieToSave As Sortie In listS
            insertSortieToFile(sortieToSave)
        Next
    End Sub

    Public Sub deleteSortieInFileAndInList(ByVal s As Sortie)
        Dim res = listDesSorties.Find(Function(p) p.IONumEtiq = s.IONumEtiq And p.IOQtiteRea = s.IOQtiteRea)
        Debug.WriteLine(listDesSorties.Count)
        listDesSorties.Remove(res)
        Debug.WriteLine(listDesSorties.Count)
        File.Delete(cheminDossierFichier)
        createExcelSave()
        insertListSortieToFile(listDesSorties)
    End Sub

    Public Sub deleteOrUpdateDepInDTB(ByVal depToDel As DépartExp)
        Dim res = verifExistDepInDTB(depToDel)
        If res = -1 Then
            'On oubli l'idée l'élément existe pas en BDD
            Exit Sub
        ElseIf res = 1 Then
            delete1DepFromDTB(depToDel)
        Else
            updateDepIntoDTB(depToDel)
        End If
    End Sub

    Public Sub deleteDepartInListAndInDTB(ByVal d As DépartExp)
        Dim resT = listDesDepartExp.Find(Function(p) p.NoOf = d.NoOf And p.NoOp = d.NoOp And p.NoProduit = d.NoProduit)

        If resT.NbCont = 1 Then
            Debug.WriteLine(listDesDepartExp.Count)
            listDesDepartExp.Remove(resT)
            Debug.WriteLine(listDesDepartExp.Count)
        Else
            Debug.WriteLine(listDesDepartExp.Count)
            Dim index = listDesDepartExp.IndexOf(resT)
            listDesDepartExp(index).NbCont -= 1
            listDesDepartExp(index).PoidsNet -= d.PoidsNet
            listDesDepartExp(index).Quantite -= d.Quantite
            Debug.WriteLine(listDesDepartExp.Count)
        End If
        poidsCamionActuel -= d.PoidsNet
        If d.Flag_CV = 0 Then
            poidsCamionActuel -= 50
        End If
        deleteOrUpdateDepInDTB(d)
        delete1DepFromIOEntree(d)
    End Sub

    Public Sub deleteNoEtiqInFICHE_EXP_NOETIQ(ByVal noEtiq As String)
        Dim req As New SqlClient.SqlCommand("DELETE from FICHE_EXP_NOETIQ where NOETIQ = '" & noEtiq & "'", New SqlClient.SqlConnection(connS3SQL))
        req.CommandTimeout = 2
        Try
            req.Connection.Open()
            Dim result = req.ExecuteNonQuery
        Catch ex As Exception
            req.Connection.Close()
        End Try
        req.Connection.Close()
    End Sub


    Public Sub delete1DepFromDTB(ByVal dep As DépartExp)
        Dim req As New SqlClient.SqlCommand("DELETE from FICHE_EXPEDITION where NOPROD = '" & dep.NoProduit & "' and NOOF = '" & dep.NoOf & "' and NOOP = '" & dep.NoOp & "' and DATE = '" & Format(Now, "dd/MM/yyyy") & "' and IDUNIQUE = " & idUniqueJour, New SqlClient.SqlConnection(connS3SQL))
        req.CommandTimeout = 2
        Try
            req.Connection.Open()
            Dim result = req.ExecuteNonQuery
        Catch ex As Exception
            req.Connection.Close()
        End Try
        req.Connection.Close()
    End Sub

    Public Sub delete1DepFromIOEntree(ByVal dep As DépartExp)
        Dim req As New SqlClient.SqlCommand("DELETE from IO where IONUMETIQ = '" & dep.NumEtiq & "' and IOTYPE = 'Entree'", New SqlClient.SqlConnection(connS3SQL))
        req.CommandTimeout = 2
        Try
            req.Connection.Open()
            Dim result = req.ExecuteNonQuery
        Catch ex As Exception
            req.Connection.Close()
        End Try
        req.Connection.Close()
    End Sub

    Public Sub updateDepIntoDTB(ByVal dep As DépartExp)
        Dim qtite = ""
        Dim poids = ""
        Dim nbCont = ""

        Dim oldQtite = ""
        Dim oldPoids = ""
        Dim oldNbCont = ""

        Dim reqS As New SqlClient.SqlCommand("SELECT QUANTITE, POIDSNET, NBCONTENANT FROM FICHE_EXPEDITION WHERE NOPROD = '" & dep.NoProduit & "' and NOOF = '" & dep.NoOf & "' and NOOP = '" & dep.NoOp & "' and DATE = '" & Format(Now, "dd/MM/yyyy") & "' and IDUNIQUE = " & idUniqueJour, New SqlClient.SqlConnection(connS3SQL))
        reqS.CommandTimeout = 2
        Try
            reqS.Connection.Open()
            Dim result = reqS.ExecuteReader

            If result.Read Then
                oldQtite = result(0)
                oldPoids = result(1)
                oldNbCont = result(2)
            Else
                oldQtite = ""
                oldPoids = ""
                oldNbCont = ""
            End If
        Catch ex As Exception
            reqS.Connection.Close()
        End Try
        reqS.Connection.Close()

        qtite = Convert.ToInt32(oldQtite) - Convert.ToInt32(dep.Quantite)
        poids = Convert.ToInt32(oldPoids) - Convert.ToInt32(dep.PoidsNet)
        nbCont = Convert.ToInt32(oldNbCont) - 1

        'TODOOOOO
        Dim req As New SqlClient.SqlCommand("UPDATE FICHE_EXPEDITION SET QUANTITE = '" & qtite & "', POIDSNET= '" & poids & "', NBCONTENANT= '" & nbCont & "' WHERE PIECES = '" & dep.Pièces & "' and NOPROD = '" & dep.NoProduit & "' and NOOF = '" & dep.NoOf & "' and NOOP = '" & dep.NoOp & "' and REMARQUE = '" & dep.Remarque & "' and FLAG_CV = '" & dep.Flag_CV & "'", New SqlClient.SqlConnection(connS3SQL))
        req.CommandTimeout = 2
        Try
            req.Connection.Open()
            Dim result = req.ExecuteNonQuery
        Catch ex As Exception
            req.Connection.Close()
        End Try
        req.Connection.Close()
    End Sub

    Public Sub recupDepartForReprendre(ByRef listDep As List(Of DépartExp))
        Dim reqS As New SqlClient.SqlCommand("SELECT * FROM FICHE_EXPEDITION WHERE IDUNIQUE = " & idUniqueJour, New SqlClient.SqlConnection(connS3SQL))
        reqS.CommandTimeout = 2
        Try
            reqS.Connection.Open()
            Dim result = reqS.ExecuteReader

            'Si des éléments restes de la saisie en cours
            While result.Read
                'TODO
                'noof 2 noop 3 qtite 4 idunique 9
                Dim pieces = result(0)
                Dim noProduit = result(1)
                Dim noOf = result(2)
                Dim noOp = result(3)
                Dim quantite = result(4)
                Dim poidsNet = result(5)
                Dim nbCont = result(6)
                Dim remarque = result(7)
                Dim flag_CV = result(11)
                Dim numEtiq = ""
                Dim designOp = result(12)

                Dim idunique = result(9)

                'On prend tout les numéros d'étiquettes liées à l'élément trouvé dans la requete precedente
                Dim reqNoE As New SqlClient.SqlCommand("SELECT NOETIQ, QUANTITE FROM FICHE_EXP_NOETIQ WHERE IDUNIQUE = " & idunique & " AND NOOF = '" & noOf & "' AND NOOP = '" & noOp & "' ", New SqlClient.SqlConnection(connS3SQL))
                reqNoE.CommandTimeout = 2
                Try
                    reqNoE.Connection.Open()
                    Dim res = reqNoE.ExecuteReader

                    'On parcourt tout
                    While res.Read()
                        Dim noEtiqToVerif = res(0)

                        For Each elem As DataRow In myUser.DTDépart.Rows
                            Dim r = elem.Item(0)
                            'Si Oui
                            If elem.Item(0) = noEtiqToVerif Then
                                'Est déja dans la database local, on passe à un autre élément
                                GoTo LA
                            End If
                        Next
                        'Si Non
                        Dim poids = calcPoidsTotal(res(1), findPoidUnitaire(noProduit, noOp))
                        myUser.DTDépart.Rows.Add(noEtiqToVerif, res(1), poids, flag_CV)
LA:

                    End While
                Catch ex As Exception
                    reqNoE.Connection.Close()
                End Try
                listDep.Add(New DépartExp(result(0), result(1), result(2), result(3), result(4), result(5), result(6), result(7), result(11), "", result(12)))
            End While
            'Sinon pas d'éléments (pas normal içi)
        Catch ex As Exception
            reqS.Connection.Close()
        End Try


    End Sub

    Public Function recupPoidsCamion() As Integer
        Dim poidsLocal As Integer = 0
        Dim reqS As New SqlClient.SqlCommand("SELECT POIDSNET, FLAG_CV, NBCONTENANT FROM FICHE_EXPEDITION WHERE IDUNIQUE = " & idUniqueJour, New SqlClient.SqlConnection(connS3SQL))
        reqS.CommandTimeout = 2
        Try
            reqS.Connection.Open()
            Dim result = reqS.ExecuteReader

            'Si des éléments restes de la saisie en cours
            While result.Read
                poidsLocal += result(0)
                If result(1) = 0 Then
                    poidsLocal += (50 * result(2))
                End If
            End While
            Return poidsLocal
        Catch ex As Exception
            Return 0
            reqS.Connection.Close()
        End Try

    End Function

    Public Function getCVOrNot(ByVal numEtiq As String)

        Dim reqS As New SqlClient.SqlCommand("SELECT COUNT(*) FROM IO WHERE IONUMETIQ = '" & numEtiq & "' AND IONUMMACH LIKE 'AE2001%'", New SqlClient.SqlConnection(connS3SQL))
        reqS.CommandTimeout = 2
        Try
            reqS.Connection.Open()
            Dim result = reqS.ExecuteReader

            If result.Read Then
                Return result(0)
            End If
            Return 42
        Catch ex As Exception
            Return 42
            reqS.Connection.Close()
        End Try
    End Function


    Public Function recupQtiteTheorique(ByVal noprod As String, ByVal noof As String, ByVal noop As String) As String

        Dim reqS As New SqlClient.SqlCommand("SELECT VOTXT2 FROM MWOOPE WHERE VOCONO = '052' and VOFACI = '009' and VOPRNO = '" & noprod & "' and CAST(VOMFNO AS INT) = " & noof & " and VOOPNO = '" & noop & "'", New SqlClient.SqlConnection(connS3BPWFC))
        reqS.CommandTimeout = 2
        Try
            reqS.Connection.Open()
            Dim result = reqS.ExecuteReader

            If result.Read Then
                Return result(0)
            End If
            Return "A Saisir"
        Catch ex As Exception
            Return "A Saisir"
            reqS.Connection.Close()
        End Try
        reqS.Connection.Close()
        Return 42
    End Function

    Public Function qtiteTheoriqueCont(ByVal dcdData As String)
        Dim numOF As String = ""
        Dim numEtiq As String = ""
        Dim noProd As String = ""
        Dim numOP As String = ""
        Dim numLot As String = ""

        numEtiq = dcdData

        Dim numE = numEtiq.Split("/")
        If numE.Length = 3 Then
            numOF = numE(0) 'OK
            numOP = numE(1) 'OK
            noProd = getNoProdFromNoOF(numOF) 'OK
        Else
            If numEtiq.StartsWith("G") Or numEtiq.StartsWith("A") Then
                getDetailEtiquetteGOPAL(numE(0), numLot, numOF, numOP) 'OK
                noProd = getNoProdFromNoOF(numOF) 'OK
            Else
            End If
        End If

        Dim qtiteTheo = "A Saisir"
        qtiteTheo = recupQtiteTheorique(noProd, numOF, numOP.Split("-")(0)).Trim
        If qtiteTheo = "" Then
            qtiteTheo = "A Saisir"
        End If
        Return qtiteTheo
    End Function

    Public Sub insertFakeEntreInIO(ByVal noEtiq As String)
        Dim reqS As New SqlClient.SqlCommand("INSERT INTO IO (IOCODESAISIE, IONUMPOINTAGE, IOTYPE, IONUMMACH, IONUMLOT, IONUMOF, IONUMETIQ, IONOPROD, IOABVNOMPROD, IOQUANTITEREALISE, IOQUANTITETOTALE, IODATESCAN, IOBLOCKED, IONUMCHARGE, IONUMMATRICE, IONUMVAGUE, IONUMOP, IOPDCSUIVANT) VALUES (2, 9999, 'Entree', 'AE9999', 'Fake', 'Fake', '" & noEtiq & "', 'Fake', 'Fake', 42, 42, '" & Format(Now, "dd/MM/yyyy") & "', '', '', '', '', 'Fake', '')", New SqlClient.SqlConnection(connS3SQL))
        reqS.CommandTimeout = 2
        Try
            reqS.Connection.Open()
            Dim result = reqS.ExecuteNonQuery

        Catch ex As Exception
            reqS.Connection.Close()
        End Try
        reqS.Connection.Close()
    End Sub




#End Region


#Region "PoidsUnitaireTheorique"
    Public Sub setDTPoidsU(ByVal noProd As String)
        myUser.DTPoidsUnitaire.Clear()
        Dim reqS As New SqlClient.SqlCommand("SELECT POPRNO, POOPNO, PODOID, POINWE FROM MPDOPE WHERE POCONO = '052' and POFACI = '009' and POSTRT = '001' and POPRNO = '" & noProd & "'", New SqlClient.SqlConnection(connS3BPWFC))
        reqS.CommandTimeout = 2
        Try
            reqS.Connection.Open()
            Dim result = reqS.ExecuteReader

            While result.Read
                myUser.DTPoidsUnitaire.Rows.Add(result(0), result(1), result(2), result(3))
            End While
        Catch ex As Exception
            reqS.Connection.Close()
        End Try
        reqS.Connection.Close()
    End Sub

    Public Sub delHGSaufScan(ByVal noOP As String)
        Debug.WriteLine("Avant delHGSaufScan : " & myUser.DTPoidsUnitaire.Rows.Count)
        For i As Integer = myUser.DTPoidsUnitaire.Rows.Count - 1 To 0 Step -1
            Dim descHG = myUser.DTPoidsUnitaire.Rows(i)(2).ToString.Trim
            Dim op = myUser.DTPoidsUnitaire.Rows(i)(1)
            If ((descHG = "HG" And op <> noOP) Or op > noOP) Then
                myUser.DTPoidsUnitaire.Rows.Remove(myUser.DTPoidsUnitaire.Rows(i))
            End If
        Next
        Debug.WriteLine("Après delHGSaufScan : " & myUser.DTPoidsUnitaire.Rows.Count)
    End Sub
#End Region


End Module

