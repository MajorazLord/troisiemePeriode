Imports System.Data.SqlClient

Module Fonctions

    Public Function getinfoEtiquette(ByVal etiq As String) As Etiquette
        For Each list As List(Of Etiquette) In listOfListOfEtiquette
            For Each etiquette As Etiquette In list
                If etiquette.getNumEtiq = etiq Then
                    Return etiquette
                End If
            Next
        Next
    End Function

    Public Function isValideEtiquette(ByVal etiq As String) As Boolean
        If etiq = "" Then
            Return False
        Else
            Dim reg As New System.Text.RegularExpressions.Regex("[&'(_)=.+*]")

            If reg.IsMatch(etiq) Then
                Return False
            End If
            If etiq.StartsWith("G") Then
                Return True
            End If
            If etiq.Split("/").Length = 3 Then
                Return True
            End If

        End If
        Return False
    End Function

    Public Function getSortiePrecFromEntree(ByVal numEtiq As String) As Etiquette
        Dim req As New SqlCommand("SELECT * FROM IO WHERE IONUMETIQ = '" & numEtiq & "' and IOTYPE = 'Sortie' and IONUMMACH <> '???'", New SqlConnection(connectionS3SQL))
        req.Connection.Open()
        Try
            Dim result = req.ExecuteReader
            If result.Read Then
                Dim numC = ""
                If result(13) <> "Null" Or result(13) <> "" Then
                    numC = result(13)
                Else
                    numC = ""
                End If
                Dim x As New Etiquette(result(6), result(5), result(16), result(7), result(8), result(3), result(1), result(11), result(4), result(10), "Sortie", numC)
                Return x
            End If
        Catch ex As Exception
            req.Connection.Close()
            Return Nothing
        End Try
        Return Nothing
    End Function

    Public Function getEntreePrecFromSortie(ByVal numMachine As String, ByVal numOf As String, ByVal dateScan As DateTime) As Etiquette
        Dim req As New SqlCommand("SELECT top 1 IOCODESAISIE, IONUMPOINTAGE, IOTYPE, IONUMMACH, IONUMLOT, IONUMOF, IONUMETIQ, IONOPROD, IOABVNOMPROD, IOQUANTITEREALISE, IOQUANTITETOTALE, IODATESCAN, IOBLOCKED, IONUMCHARGE, IONUMMATRICE, IONUMVAGUE, IONUMOP, IOPDCSUIVANT FROM SAISIE_CONT.dbo.IO WHERE IONUMMACH = '" & numMachine & "' and IONUMOF = '" & numOf & "' and IOTYPE = 'Entree' and IODATESCAN < '" & Format(dateScan, "yyyy-dd-MM HH:mm:ss") & "' order by IODATESCAN desc", New SqlConnection(connectionS3SQL))
        req.Connection.Open()
        Try
            Dim result = req.ExecuteReader
            If result.Read Then
                Dim numC = ""
                If result(13) <> "Null" Or result(13) <> "" Then
                    numC = result(13)
                Else
                    numC = ""
                End If
                Dim x As New Etiquette(result(6), result(5), result(16), result(7), result(8), result(3), result(1), result(11), "Osef", 42, "Entree", numC)
                Return x
            Else
                'Plus d'antécédents
                Return Nothing
            End If
        Catch ex As Exception
            req.Connection.Close()
            Return Nothing
        End Try
        Return Nothing
    End Function

    Public Function getSortieSuivFromEntree(ByVal numMachine As String, ByVal numOf As String, ByVal dateScan As DateTime) As Etiquette
        Dim req As New SqlCommand("SELECT top 1 IOCODESAISIE, IONUMPOINTAGE, IOTYPE, IONUMMACH, IONUMLOT, IONUMOF, IONUMETIQ, IONOPROD, IOABVNOMPROD, IOQUANTITEREALISE, IOQUANTITETOTALE, IODATESCAN, IOBLOCKED, IONUMCHARGE, IONUMMATRICE, IONUMVAGUE, IONUMOP, IOPDCSUIVANT FROM SAISIE_CONT.dbo.IO WHERE IONUMMACH = '" & numMachine & "' and IONUMOF = '" & numOf & "' and IOTYPE = 'Sortie' and IODATESCAN > '" & Format(dateScan, "yyyy-dd-MM HH:mm:ss") & "' order by IODATESCAN asc", New SqlConnection(connectionS3SQL))
        req.Connection.Open()
        Try
            Dim result = req.ExecuteReader
            If result.Read Then
                Dim numC = ""
                If result(13) <> "Null" Or result(13) <> "" Then
                    numC = result(13)
                Else
                    numC = ""
                End If
                Dim x As New Etiquette(result(6), result(5), result(16), result(7), result(8), result(3), result(1), result(11), result(4), result(10), "Sortie", numC)
                Return x
            End If
        Catch ex As Exception
            req.Connection.Close()
            Return Nothing
        End Try
        Return Nothing
    End Function

    Public Function getEntreeSuivFromSortie(ByVal numEtiq As String) As Etiquette
        Dim req As New SqlCommand("SELECT * FROM IO WHERE IONUMETIQ = '" & numEtiq & "' and IOTYPE = 'Entree'", New SqlConnection(connectionS3SQL))
        req.Connection.Open()
        Try
            Dim result = req.ExecuteReader
            If result.Read Then
                Dim numC = ""
                If result(13) <> "Null" Or result(13) <> "" Then
                    numC = result(13)
                Else
                    numC = ""
                End If
                Dim x As New Etiquette(result(6), result(5), result(16), result(7), result(8), result(3), result(1), result(11), "Osef", 42, "Entree", numC)
                Return x
            Else
                'Plus d'antécédents
                Return Nothing
            End If
        Catch ex As Exception
            req.Connection.Close()
            Return Nothing
        End Try
        Return Nothing
    End Function

    Public Sub putDataTableIntoListGlobal(ByVal numOf As String)
        listEtiqTable.Clear()
        Dim req As New SqlCommand("SELECT * FROM IO WHERE IONUMOF = '" & numOf & "' and IONUMMACH <> '???' order by IODATESCAN asc;", New SqlConnection(connectionS3SQL))
        req.Connection.Open()
        Try
            Dim result = req.ExecuteReader
            While result.Read
                Dim numC = ""
                If result(13) <> "Null" Or result(13) <> "" Then
                    numC = result(13)
                Else
                    numC = ""
                End If

                Dim x
                If result(2) = "Entree" Then
                    x = New Etiquette(result(6), result(5), result(16), result(7), result(8), result(3), result(1), result(11), "", 0, result(2), numC)
                Else
                    x = New Etiquette(result(6), result(5), result(16), result(7), result(8), result(3), result(1), result(11), result(4), result(9), result(2), numC)
                End If
                listEtiqTable.Add(x)
            End While
        Catch ex As Exception
            req.Connection.Close()
        End Try
    End Sub

    Public Function createTracabilite() As List(Of List(Of Etiquette))
        Dim listFin As New List(Of List(Of Etiquette))
        Dim listMedium As New List(Of Etiquette)

        Dim lastType As String = ""
        Dim comptTour = 0
        For Each etiq As Etiquette In listEtiqTable
            If lastType <> etiq.getTypeE And listMedium.Count <> 0 Then
                listFin.Add(New List(Of Etiquette))
                listFin(comptTour).AddRange(listMedium)
                listMedium.Clear()
                comptTour += 1
            End If
            listMedium.Add(etiq)
            lastType = etiq.getTypeE
        Next
        If listMedium.Count <> 0 Then
            listFin.Add(New List(Of Etiquette))
            listFin(comptTour).AddRange(listMedium)
            listMedium.Clear()
        End If

STARTLIST:
        Dim nbList = listFin.Count

        For x = 0 To nbList - 1 Step 1
            If x = 0 Or x = (nbList - 1) Then

            Else
                If listFin(x)(0).getTypeE = "Entree" Then
                    listFin.RemoveAt(x)
                    GoTo STARTLIST
                End If
            End If
        Next

        Return listFin
    End Function


End Module
