Imports System.Data.SqlClient

Module Fonctions

    Public Function getinfoEtiquette(ByVal etiq As String) As Etiquette
        Dim etiqTmp = myEtiq
        While Not etiqTmp Is Nothing
            If etiqTmp.getNumEtiq = etiq Then
                Return etiqTmp
            End If
        End While

        Dim etipTmpSuiv = myEtiq
        While Not etipTmpSuiv Is Nothing
            If etipTmpSuiv.getNumEtiq = etiq Then
                Return etipTmpSuiv
            End If
        End While
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
        Dim req As New SqlCommand("SELECT * FROM IO WHERE IONUMETIQ = '" & numEtiq & "' and IOTYPE = 'Sortie'", New SqlConnection(connectionS3SQL))
        req.Connection.Open()
        Try
            Dim result = req.ExecuteReader
            If result.Read Then
                Dim x As New Etiquette(result(6), result(5), result(16), result(7), result(8), result(3), result(1), result(11), result(4), result(10))
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
                Dim x As New Etiquette(result(6), result(5), result(16), result(7), result(8), result(3), result(1), result(11), "Osef", 42)
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
                Dim x As New Etiquette(result(6), result(5), result(16), result(7), result(8), result(3), result(1), result(11), result(4), result(10))
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
                Dim x As New Etiquette(result(6), result(5), result(16), result(7), result(8), result(3), result(1), result(11), "Osef", 42)
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

End Module
