Imports Datalogic.API

Imports System.Data.SqlClient
Imports System.IO
Imports System.Data

Module Functions
    Public Sub affichePointVert()
        Device.EnableLED(Device.LEDs.GreenSpot, True)
        pause(1)
        Device.EnableLED(Device.LEDs.GreenSpot, False)
    End Sub

    Public Sub pause(ByVal coef As Integer)
        Threading.Thread.Sleep(300 * coef)
    End Sub

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

    Public Sub CreateExcelSave()

        'Creation du dossier AuditQtiteGueret dans My Documents
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
        sLigne = "CodeCont;QtiteBDD;QtiteVERIF"
        x.WriteLine(sLigne)
        x.Close()
    End Sub

    Public Sub WriteInSave(ByVal audit As AuditUnit)
        Dim sLigne As String
        Dim x As New System.IO.StreamWriter(cheminDossierFichier, True, System.Text.Encoding.UTF8)

        sLigne = audit.codeCont & ";" & audit.qtiteSaisie & ";" & audit.qtiteVerif


        x.WriteLine(sLigne)

        x.Close()
    End Sub

    'A utiliser quand on supprime une ligne dans le recap
    Public Sub ReplaceAllInSave(ByVal audits As List(Of AuditUnit))
        Dim sLigne As String
        
        File.Delete(cheminDossierFichier)

        CreateExcelSave()

        Dim x As New System.IO.StreamWriter(cheminDossierFichier, True, System.Text.Encoding.UTF8)

        For Each audit As AuditUnit In audits
            sLigne = audit.codeCont & ";" & audit.qtiteSaisie & ";" & audit.qtiteVerif
            x.WriteLine(sLigne)
        Next
        x.Close()
    End Sub

    Public Sub initDT()
        DTAudit = New DataTable
        DTAudit.Columns.Add("CodeCont", GetType(String))
        DTAudit.Columns.Add("QtiteBDD", GetType(Integer))
        DTAudit.Columns.Add("QtiteVERIF", GetType(Integer))
    End Sub

    Public Function DataTableToListOfAudit(ByVal dt As DataTable) As List(Of AuditUnit)
        Dim listToFill As New List(Of AuditUnit)
        For Each row As DataRow In dt.Rows
            listToFill.Add(New AuditUnit(row(0), row(1), row(2)))
        Next

        Return listToFill


    End Function


    ''' <summary>
    ''' Vérifie si l'écran est un écran VGA
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function isScreenVGA() As Boolean
        Return (currentScreenHeight = HEIGHT_SCREEN_VGA And currentScreenWidth = WIDTH_SCREEN_VGA)
    End Function

    Public Sub addAudit(ByVal auditToAdd As AuditUnit)
        DTAudit.Rows.Add(auditToAdd.codeCont, auditToAdd.qtiteSaisie, auditToAdd.qtiteVerif)
    End Sub

    Public Sub removeAudit(ByVal codeCont As String, ByVal qtiteBDD As Integer, ByVal qtiteVERIF As Integer)
        Dim rows() As DataRow
        rows = DTAudit.Select("CodeCont = '" & codeCont & "' and QtiteBDD = '" & qtiteBDD & "' and QtiteVERIF = '" & qtiteVERIF & "'")

        For Each row As DataRow In rows
            row.Delete()
        Next

        ReplaceAllInSave(DataTableToListOfAudit(DTAudit))
    End Sub

End Module
