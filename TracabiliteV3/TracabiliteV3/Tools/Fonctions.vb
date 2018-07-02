Imports System.Data.SqlClient
Imports System.Security.Permissions
Imports TracabiliteV3.Model

Module Fonctions

    Public Sub FindTracabilite(ByVal noetiq As String)
        'Todo changer en numéro d'of directement
        InitDataFromSql(noetiq.Split("/")(0))



        Dim type As String
        Try
            type = GetTypeOfEtiq(noetiq)
        Catch ex As Exception
            MessageBox.Show("Le programme s'est arreté : une erreur s'est produite dans la requeteSQL trouvant le type de l'étiquette. Verifier le numéro d'étiquette saisie.", "Erreur SQL", MessageBoxButton.OK, MessageBoxImage.Warning)
            Exit Sub
        End Try

        Select Case type
            Case "Entree"




            Case "Sortie"
                FindSuivs(noetiq)
        End Select








    End Sub

    ''' <summary>
    '''     Initialise les datas a traiter pour faire la tracabilité
    ''' </summary>
    ''' <param name="noOf">Numéro d'OF dont on veut la tracabilité</param>
    Public Sub InitDataFromSql(ByVal noOf As String)
        'Todo
        'Dim requestInit As New SqlCommand("SELECT * FROM IO WHERE IONUMOF = '" & noOf & "' ORDER BY IODATESCAN ASC", New SqlConnection(ConnectionS3Sql))
        'Pour le test vvv
        Dim requestInit As New SqlCommand("SELECT * FROM IO WHERE IONUMOF = '" & noOf & "' and (IONUMPOINTAGE = '9870' or IONUMPOINTAGE = '9871' or IONUMPOINTAGE = '9872' or IONUMPOINTAGE = '9873' or IONUMPOINTAGE = '9874' or IONUMPOINTAGE = '9875' or IONUMPOINTAGE = '9876' or IONUMPOINTAGE = '9877' or IONUMPOINTAGE = '9878') ORDER BY IODATESCAN ASC", New SqlConnection(ConnectionS3Sql))
        requestInit.CommandTimeout = 2
        requestInit.Connection.Open()

        Dim lec = requestInit.ExecuteReader
        While lec.Read
            Dim qtiteR = lec(9)
            Dim qtiteT = lec(10)
            Dim pdcSuivant = lec(17)
            If lec(9).GetType = GetType(DBNull) Then
                qtiteR = -1
            End If
            If lec(10).GetType = GetType(DBNull) Then
                qtiteT = -1
            End If
            If lec(17).GetType = GetType(DBNull) Then
                pdcSuivant = -1
            End If

            DataSql.Add(New Etiquette(lec(0), lec(1), lec(2), lec(3), lec(4), lec(5), lec(6), lec(7), lec(8), qtiteR, qtiteT, lec(11), lec(13), lec(16), pdcSuivant, New List(Of Etiquette), New List(Of Etiquette)))
        End While
        Debug.WriteLine("Lignes recupérées : " & DataSql.Count)
    End Sub


    ''' <summary>
    '''     Retourne le type d'une etiquette en fonction de son numéro
    ''' </summary>
    ''' <param name="noEtiq">Numéro d'étiquette à checker"</param>
    ''' <returns>Le type ou une erreur</returns>
    Public Function GetTypeOfEtiq(ByVal noEtiq As String) As String
        Dim requestType As New SqlCommand("SELECT IOTYPE FROM IO WHERE IONUMETIQ = '" & noEtiq & "' ORDER BY IODATESCAN", New SqlConnection(ConnectionS3Sql))
        requestType.CommandTimeout = 2

        requestType.Connection.Open()

        Dim lec = requestType.ExecuteReader
        If lec.Read Then
            Return lec(0)
        Else
            Throw New Exception("Request don't work")
        End If
    End Function

    Public Sub FindSuivs(ByVal noEtiq As String)
        'Todo On essaie de partir de la première de la liste pour recupérer tout les suivants. Une fois à la fin on fera le parcours en sens inverse pour verifier tout les cas.
        'Dim etiqSource As Etiquette = DataSql.Item(0)

        'Recupère l'index de notre étiquette de départ dans la list des étiquettes "DataSql"
        Dim indexNoEtiq = DataSql.FindIndex(Function(e) (e.NumEtiquette = noEtiq))



        Debug.WriteLine(indexNoEtiq)


    End Sub

End Module