Imports Datalogic.API

Imports System.Data.SqlClient
Imports System.IO
Imports System.Data

Module Functions

#Region "Outils douchette"
    Public Sub affichePointVert()
        Device.EnableLED(Device.LEDs.GreenSpot, True)
        pause(1)
        Device.EnableLED(Device.LEDs.GreenSpot, False)
    End Sub

    ''' <summary>
    ''' Fonction qui essaye de se connecter à la BDD en utilisant la connexion dispo (cable ou wifi)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function isConnectionOK() As Boolean
        If isDeviceOnSocle() Or Microsoft.WindowsMobile.Status.SystemState.ConnectionsNetworkCount <> 0 Then
            Dim testConnexion As New SqlCommand("", New SqlConnection(connS3SQL))
            testConnexion.CommandTimeout = 2
            Try
                testConnexion.Connection.Open()
                testConnexion.Connection.Close()
                Return True
            Catch 'Provient de test.connection.open'
                testConnexion.Connection.Close()
                Return False
            End Try
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Renvoie vrai ou faux suivant si l'appareil est sur le socle ou non
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function isDeviceOnSocle() As Boolean
        Return Datalogic.API.Device.GetIsInCradle()
    End Function

    Public Sub pause(ByVal coef As Integer)
        Threading.Thread.Sleep(300 * coef)
    End Sub

    ''' <summary>
    ''' Vérifie si l'écran est un écran VGA
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function isScreenVGA() As Boolean
        Return (currentScreenHeight = HEIGHT_SCREEN_VGA And currentScreenWidth = WIDTH_SCREEN_VGA)
    End Function



#End Region
    
    Public Sub rechercherQtiteByNumEtiq(ByVal numetiq As String)
        Dim reqSelect As New SqlCommand("select IOQUANTITETOTALE from IO where IOTYPE = 'Sortie' AND IONUMETIQ = '" & numetiq & "'", New SqlConnection(connS3SQL))
        reqSelect.CommandTimeout = 2
        Try
            reqSelect.Connection.Open()
            Dim lec = reqSelect.ExecuteReader
            If lec.Read Then
                Debug.WriteLine(lec(0))
                resReq = lec(0)
            Else
                resReq = "errNotFound"
            End If
        Catch ex As Exception
            resReq = "errorRq"
            reqSelect.Connection.Close()
        End Try
    End Sub

    'Ok
    Public Sub initDT()
        DTAudit = New DataTable
        DTAudit.Columns.Add("CodeCont", GetType(String))
        DTAudit.Columns.Add("QtiteEcrite", GetType(Integer))
        DTAudit.Columns.Add("QtitePesée", GetType(Integer))
        DTAudit.Columns.Add("QtiteSaisie", GetType(Integer))
        DTAudit.Columns.Add("QtiteThéorique", GetType(Integer))
    End Sub

    'Ok
    Public Sub CreateExcelSave()

        'Creation du dossier AuditQtiteGueret dans My Documents
        If Not Directory.Exists(cheminBase) Then
            Try
                Directory.CreateDirectory(cheminBase)
            Catch ex As Exception
                Debug.WriteLine(ex.Message())
            End Try

        End If

        'Creation du dossier encours dans AuditQtiteGueret
        If Not Directory.Exists(cheminDossier) Then
            Try
                Directory.CreateDirectory(cheminDossier)
            Catch ex As Exception
                Debug.WriteLine(ex.Message())
            End Try

        End If

        'Creation du fichier de saisie dans le dossier du jour
        If Not File.Exists(cheminDossierFichier) Then
            File.Create(cheminDossierFichier).Close()

            Dim sLigne As String
            Dim x As New System.IO.StreamWriter(cheminDossierFichier, False, System.Text.Encoding.UTF8)

            sLigne = Format(Now, "dd/MM/yyyy")
            dateJour = sLigne
            x.WriteLine(sLigne)
            x.WriteLine("")
            sLigne = "CodeCont;QtiteEcrite;QtitePesée;QtiteSaisie;QtiteTheorique"
            x.WriteLine(sLigne)
            x.Close()
        Else
            'On concidère que ça a crash ICI
            Dim sLigne As String
            Dim y As New System.IO.StreamReader(cheminDossierFichier)

            dateJour = y.ReadLine()
            'Ligne vide
            y.ReadLine()
            'Ligne titre
            y.ReadLine()

            sLigne = y.ReadLine()
            Do While (Not sLigne Is Nothing)
                Dim vals = sLigne.Split(";")
                DTAudit.Rows.Add(vals(0), vals(1), vals(2), vals(3), vals(4))
                addOrUpdateIntoBDD(sLigne)
                sLigne = y.ReadLine()
            Loop
            y.Close()

            'A ce stade, on a récupéré le contenu du fichier Excel deja présent au démarrage 
            'pour en continuer la saisie.
        End If
    End Sub

    Public Sub addOrUpdateIntoBDD(ByVal sLigne As String)
        Dim res = sLigne.Split(";")
        'TODO
        Try
            Dim reqSelect As New SqlCommand("SELECT * FROM AUDIT_QTITE_CONTENANT WHERE CODE_CONT = '" & res(0) & "' and DATE_SCAN = '" & dateJour & "'", New SqlConnection(connS3SQL))
            reqSelect.CommandTimeout = 2

            reqSelect.Connection.Open()

            Dim lec = reqSelect.ExecuteReader()

            If lec.Read Then
                'Update
                Dim reqUpdate As New SqlCommand("UPDATE AUDIT_QTITE_CONTENANT SET QTITE_ECRITE = " & res(1) & ", QTITE_PESEE = " & res(2) & ", QTITE_SAISIE = " & res(3) & ", QTITE_THEORIQUE = " & res(4) & " WHERE CODE_CONT = '" & res(0) & "' and DATE_SCAN = '" & dateJour & "'", New SqlConnection(connS3SQL))
                reqUpdate.CommandTimeout = 2
                reqUpdate.Connection.Open()
                reqUpdate.ExecuteNonQuery()
                reqUpdate.Connection.Close()
            Else
                'Add
                Dim reqAdd As New SqlCommand("INSERT INTO AUDIT_QTITE_CONTENANT VALUES ('" & res(0) & "', " & res(1) & ", " & res(2) & ", " & res(3) & ", " & res(4) & ", '" & dateJour & "', 0)", New SqlConnection(connS3SQL))
                reqAdd.CommandTimeout = 2
                reqAdd.Connection.Open()
                reqAdd.ExecuteNonQuery()
                reqAdd.Connection.Close()
            End If
            reqSelect.Connection.Close()
        Catch ex As Exception
            Debug.WriteLine("Erreur")
        End Try
    End Sub

    Public Sub addAuditToDT(ByVal auditToAdd As AuditUnit)
        DTAudit.Rows.Add(auditToAdd.codeCont, auditToAdd.qtiteEcrite, auditToAdd.qtitePesee, auditToAdd.qtiteSaisie, auditToAdd.qtiteTheorique)
    End Sub

    Public Sub addAuditToExcel(ByVal auditToAdd As AuditUnit)
        Using x As StreamWriter = New System.IO.StreamWriter(cheminDossierFichier, True, System.Text.Encoding.UTF8)
            x.WriteLine(auditToAdd.codeCont & ";" & auditToAdd.qtiteEcrite & ";" & auditToAdd.qtitePesee & ";-1;-1")
        End Using
    End Sub

    Public Sub addAuditToBDD(ByVal auditToAdd As AuditUnit)
        Dim reqAdd As New SqlCommand("INSERT INTO AUDIT_QTITE_CONTENANT VALUES ('" & auditToAdd.codeCont & "', " & auditToAdd.qtiteEcrite & ", " & auditToAdd.qtitePesee & ", " & auditToAdd.qtiteSaisie & ", " & auditToAdd.qtiteTheorique & ", '" & dateJour & "', 0)", New SqlConnection(connS3SQL))
        reqAdd.CommandTimeout = 2
        Try
            reqAdd.Connection.Open()
        Catch ex As Exception
            MessageBox.Show("Connection impossible à la BDD. Vérifier la connection internet")
            Exit Sub
        End Try

        Try
            reqAdd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Requete SQL hors service. Ajout impossible en BDD.")
        End Try

        reqAdd.Connection.Close()
    End Sub

    Public Sub removeAuditFromDT(ByVal codeCont As String, ByVal qtiteE As Integer, ByVal qtiteP As Integer, ByVal qtiteS As Integer, ByVal qtiteT As Integer)
        Dim rows() As DataRow
        rows = DTAudit.Select("CodeCont = '" & codeCont & "' and QtiteEcrite = '" & qtiteE & "' and QtitePesée = '" & qtiteP & "' and QtiteSaisie = '" & qtiteS & "' and QtiteThéorique = '" & qtiteT & "'")

        For Each row As DataRow In rows
            row.Delete()
        Next

        'ReplaceAllInSave(DataTableToListOfAudit(DTAudit))
    End Sub

    Public Sub removeAuditFromExcel()
        Using x As StreamWriter = New System.IO.StreamWriter(cheminDossierFichier, False, System.Text.Encoding.UTF8)
            x.WriteLine(Format(dateJour, "dd/MM/yyyy"))
            x.WriteLine("")
            x.WriteLine("CodeCont;QtiteEcrite;QtitePesée;QtiteSaisie;QtiteThéorique")
            For Each row As DataRow In DTAudit.Rows
                x.WriteLine(row(0) & ";" & row(1) & ";" & row(2) & ";-1;-1")
            Next
        End Using

        'ReplaceAllInSave(DataTableToListOfAudit(DTAudit))
    End Sub

    Public Sub removeAuditFromBDD(ByVal codeCont As String, ByVal qtiteE As Integer, ByVal qtiteP As Integer, ByVal qtiteS As Integer, ByVal qtiteT As Integer)
        Dim reqDelete As New SqlCommand("DELETE FROM AUDIT_QTITE_CONTENANT WHERE CODE_CONT = '" & codeCont & "' and QTITE_ECRITE = " & qtiteE & " and QTITE_PESEE = " & qtiteP & " and QTITE_SAISIE = " & qtiteS & " and QTITE_THEORIQUE = " & qtiteT & "", New SqlConnection(connS3SQL))
        reqDelete.CommandTimeout = 2
        Try
            reqDelete.Connection.Open()
        Catch ex As Exception
            MessageBox.Show("Connection impossible à la BDD. Vérifier la connection internet")
            Exit Sub
        End Try

        Try
            reqDelete.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Requete SQL hors service. Suppression impossible en BDD.")
        End Try

        reqDelete.Connection.Close()
    End Sub

    Public Function findQtiteSaisieInIO(ByVal noEtiq As String)
        Dim reqFind As New SqlCommand("SELECT IOQUANTITEREALISE FROM IO WHERE IONUMETIQ = '" & noEtiq & "' and IOTYPE = 'Sortie'", New SqlConnection(connS3SQL))
        reqFind.CommandTimeout = 2

        reqFind.Connection.Open()

        Dim lec = reqFind.ExecuteReader()
        If lec.Read Then
            If IsDBNull(lec(0)) Then
                'Champs qtité vide
                Return -2
            Else
                Return lec(0)
            End If
        End If
        'Pas de ligne correspondante
        Return -2
    End Function

    Public Function checkIfIsHG(ByVal noEtiq As String)
        Dim noOp As String = ""
        If noEtiq.Contains("/") Then
            Dim noEtiqDec = noEtiq.Split("/")
            noOp = noEtiqDec(1)
        Else
            getNoOpEtiquetteGOPAL(noEtiq, noOp)
        End If

        If noOp.Contains("-") Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Permet de récupérer le n° op si on a une etiquette GOPAL
    ''' </summary>
    ''' <param name="NoGopal"></param>
    ''' <remarks></remarks>
    Public Sub getNoOpEtiquetteGOPAL(ByVal nogopal As String, ByRef noop As String)
        Dim req As New SqlClient.SqlCommand("select distinct noop from detail_etiq where no_gopal = '" & nogopal & "'", New SqlClient.SqlConnection(connGOPAL))
        req.CommandTimeout = 2
        Try
            req.Connection.Open()
            Dim result = req.ExecuteReader
            If result.Read Then
                noop = result(0)
            Else
                noop = ""
            End If
        Catch ex As Exception
            noop = ""
            req.Connection.Close()
        End Try
        req.Connection.Close()
    End Sub

    Public Sub getQtiteTheoFromBPW(ByVal noEtiq As String)
        Dim req As New SqlClient.SqlCommand("select distinct noop from detail_etiq where no_gopal = '" & nogopal & "'", New SqlClient.SqlConnection(connGOPAL))
        req.CommandTimeout = 2
        Try
            req.Connection.Open()
            Dim result = req.ExecuteReader
            If result.Read Then
                noop = result(0)
            Else
                noop = ""
            End If
        Catch ex As Exception
            noop = ""
            req.Connection.Close()
        End Try
        req.Connection.Close()
    End Sub

    Public Sub traitementFin()
        For Each row As DataRow In DTAudit.Rows
            Dim qtiteSaisieBdd = findQtiteSaisieInIO(row(0))
            Dim qtiteTheorique As String = ""
            If checkIfIsHG(row(0)) Then
                qtiteTheorique = "HG"
            Else
                qtiteTheorique = getQtiteTheoFromBPW(row(0))
            End If
        Next



    End Sub

    'Public Function DataTableToListOfAudit(ByVal dt As DataTable) As List(Of AuditUnit)
    '    Dim listToFill As New List(Of AuditUnit)
    '    For Each row As DataRow In dt.Rows
    '        listToFill.Add(New AuditUnit(row(0), row(1), row(2)))
    '    Next

    '    Return listToFill
    'End Function








End Module
