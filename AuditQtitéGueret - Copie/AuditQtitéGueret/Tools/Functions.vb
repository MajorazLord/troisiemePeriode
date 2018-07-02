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
                resReq = "-1"
            End If
        Catch ex As Exception
            resReq = "-1"
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

        sLigne = "CodeCont;QtiteBDD;QtiteVERIF;DateVERIF"
        x.WriteLine(sLigne)
        x.Close()
    End Sub

    Public Function WriteInSave(ByVal audit As AuditUnit) As Boolean
        Dim resSave As Boolean
        Dim sLigne As String
        Dim x As New System.IO.StreamWriter(cheminDossierFichier, True, System.Text.Encoding.UTF8)
        Try
            sLigne = audit.codeCont & ";" & audit.qtiteSaisie & ";" & audit.qtiteVerif & ";" & audit.dateVerif
            x.WriteLine(sLigne)
            x.Close()
            resSave = True
        Catch ex As Exception
            x.Close()
            resSave = False
        End Try

        Dim resSaveSql = WriteInSql(audit)

        Return (resSave And resSaveSql)
    End Function

    Public Function SendSaveToSql() As Boolean
        Dim stream As New StreamReader(cheminDossierFichier)
        Try
            'Lecture intitulé useless
            stream.ReadLine()

            'Lecture des données
            Dim line As String
            line = stream.ReadLine()

            Do While (Not line Is Nothing)
                Debug.WriteLine(line)

                line = stream.ReadLine()
            Loop



            Debug.WriteLine(stream.ReadLine())
            stream.Close()
        Catch ex As Exception

        End Try





        Return False
    End Function

    Public Function WriteInSql(ByVal audit As AuditUnit) As Boolean
        Dim reqSelect As New SqlCommand("INSERT INTO AUDIT_QTITE_CONT VALUES ('" & audit.codeCont & "', '" & audit.qtiteSaisie & "', '" & audit.qtiteVerif & "', '" & audit.dateVerif & "', 0, " & codeSaisie & ")", New SqlConnection(connS3SQL))
        reqSelect.CommandTimeout = 2
        Try
            reqSelect.Connection.Open()
            reqSelect.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            reqSelect.Connection.Close()
            Return False
        End Try

    End Function

    Public Function GetLastCodeSaisie() As Integer
        Dim reqCode As New SqlCommand("SELECT MAX(CODE_SAISIE) FROM AUDIT_QTITE_CONT", New SqlConnection(connS3SQL))
        reqCode.CommandTimeout = 2
        Try
            reqCode.Connection.Open()
            Dim lec = reqCode.ExecuteReader()
            If lec.Read Then
                Return lec.GetInt32(0)
            Else
                Return -1
            End If
        Catch ex As Exception
            reqCode.Connection.Close()
            Return -1
        End Try
    End Function

    ''' <summary>
    ''' Return 0 If it crash
    ''' </summary>
    ''' <param name="precCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckIfCrashOrNot(ByVal precCode) As Boolean
        Dim reqCode As New SqlCommand("SELECT VALIDATION FROM AUDIT_QTITE_CONT WHERE CODE_SAISIE = " & precCode, New SqlConnection(connS3SQL))
        reqCode.CommandTimeout = 2
        Try
            reqCode.Connection.Open()
            Dim lec = reqCode.ExecuteReader()
            If lec.Read Then
                Return lec.GetBoolean(0)
            Else
                Return 0
            End If
        Catch ex As Exception
            reqCode.Connection.Close()
            Return 0
        End Try
    End Function

    Public Function RecupLastSaisie(ByVal code As Integer) As List(Of AuditUnit)
        Dim listTemp As New List(Of AuditUnit)
        Dim reqCode As New SqlCommand("SELECT * FROM AUDIT_QTITE_CONT WHERE CODE_SAISIE = " & code, New SqlConnection(connS3SQL))
        reqCode.CommandTimeout = 2
        Try
            reqCode.Connection.Open()
            Dim lec = reqCode.ExecuteReader()
            If lec.Read Then
                listTemp.Add(New AuditUnit(lec(0), lec(1), lec(2), lec(3)))
            End If
        Catch ex As Exception
            reqCode.Connection.Close()
            Return listTemp
        End Try

        Return listTemp
    End Function

    Public Sub InsertListOfAuditIntoDT(ByVal listToInsert As List(Of AuditUnit))
        initDT()
        DTAudit.Clear()
        For Each audit As AuditUnit In listToInsert
            addAudit(audit)
        Next
    End Sub

    Public Sub SetValidationTo1(ByVal codeSaisie As Integer)
        Dim reqCode As New SqlCommand("UPDATE AUDIT_QTITE_CONT SET VALIDATION = 1 WHERE CODE_SAISIE = " & codeSaisie, New SqlConnection(connS3SQL))
        reqCode.CommandTimeout = 2
        Try
            reqCode.Connection.Open()
            reqCode.ExecuteNonQuery()
        Catch ex As Exception
            reqCode.Connection.Close()
        End Try
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
        DTAudit.Columns.Add("DateVERIF", GetType(DateTime))
    End Sub

    Public Function DataTableToListOfAudit(ByVal dt As DataTable) As List(Of AuditUnit)
        Dim listToFill As New List(Of AuditUnit)
        For Each row As DataRow In dt.Rows
            listToFill.Add(New AuditUnit(row(0), row(1), row(2), row(3)))
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
        DTAudit.Rows.Add(auditToAdd.codeCont, auditToAdd.qtiteSaisie, auditToAdd.qtiteVerif, auditToAdd.dateVerif)
        Debug.WriteLine("compteur : " & DTAudit.Rows.Count)
    End Sub

    Public Sub removeAudit(ByVal codeCont As String, ByVal qtiteBDD As Integer, ByVal qtiteVERIF As Integer)
        Dim rows() As DataRow
        rows = DTAudit.Select("CodeCont = '" & codeCont & "' and QtiteBDD = '" & qtiteBDD & "' and QtiteVERIF = '" & qtiteVERIF & "'")

        For Each row As DataRow In rows
            row.Delete()
        Next

        ReplaceAllInSave(DataTableToListOfAudit(DTAudit))
    End Sub

    Public Sub removeFromSql(ByVal codeCont As String, ByVal qtiteBDD As Integer, ByVal qtiteVERIF As Integer)
        Dim reqDelete = New SqlCommand("DELETE FROM AUDIT_QTITE_CONT WHERE NUMETIQ = '" & codeCont & "' and QTITE_SAISIE = " & qtiteBDD & " and QTITE_VERIF = " & qtiteVERIF & " and CODE_SAISIE = " & codeSaisie, New SqlConnection(connS3SQL))
        reqDelete.CommandTimeout = 2
        Try
            reqDelete.Connection.Open()

            reqDelete.ExecuteNonQuery()
        Catch ex As Exception
            reqDelete.Connection.Close()
        End Try
        

    End Sub

End Module
