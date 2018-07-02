Imports System.Data.SqlClient
Imports System.Data.OracleClient

Public Class Etiquette
    Private _numEtiq As String
    Public Property NumEtiq As String
        Get
            Return _numEtiq
        End Get
        Set(value As String)
            _numEtiq = value
        End Set
    End Property
    Private _numOF As String
    Public Property NumOf As String
        Get
            Return _numOF
        End Get
        Set(value As String)
            _numOF = value
        End Set
    End Property

    Private _numOP As String
    Public Property NumOP As String
        Get
            Return _numOP
        End Get
        Set(value As String)
            _numOP = value
        End Set
    End Property

    Private _numProd As String
    Private _nomProd As String

    Public Property DescProd As String
        Get
            Return _numProd & " --- " & _nomProd
        End Get
        Set(value As String)

        End Set
    End Property

    Private _numMachine As String
    Public Property NumMachine As String
        Get
            Return _numMachine
        End Get
        Set(value As String)
            _numMachine = value
        End Set
    End Property

    Private _numPointage As String
    Public Property NumPointage As String
        Get
            Return _numPointage
        End Get
        Set(value As String)
            _numPointage = value
        End Set
    End Property

    Private _dateEtiq As DateTime
    Public Property DateEtiq As String
        Get
            Return _dateEtiq
        End Get
        Set(value As String)
            _dateEtiq = value
        End Set
    End Property

    Private _numLot As String
    Public Property NumLot As String
        Get
            Return _numLot
        End Get
        Set(value As String)
            _numLot = value
        End Set
    End Property

    Private _quantite As Integer
    Public Property Quantite As String
        Get
            Return _quantite
        End Get
        Set(value As String)
            _quantite = value
        End Set
    End Property

    Private _numCharge As String
    Public Property NumCharge As String
        Get
            Return _numCharge
        End Get
        Set(value As String)
            _numCharge = value
        End Set
    End Property


    Private _type As String

    Private _numContainer As String

    Public ReadOnly Property ListPrec As List(Of Etiquette) = New List(Of Etiquette)
    Public ReadOnly Property ListSuiv As List(Of Etiquette) = New List(Of Etiquette)

    Public Sub New(ByVal numEtiq As String)
        Me._numEtiq = numEtiq
        If Not numEtiq.StartsWith("G") Then
            _numOF = numEtiq.Split("/")(0)
            _numOP = numEtiq.Split("/")(1)
            _numContainer = numEtiq.Split("/")(2)
            getDetailEtiq()
        Else
            getDetailEtiqGOPAL()
        End If
        getMachineEtiq()
    End Sub

    Public Sub New(ByVal numEtiq As String, ByVal numOF As String, ByVal numOP As String, ByVal numProd As String, ByVal nomProd As String, ByVal numMach As String, ByVal numPointage As String, ByVal dateScan As String, ByVal numLot As String, ByVal quantite As String, ByVal type As String, ByVal numCharge As String)
        Me._numEtiq = numEtiq
        _numOF = numOF
        _numOP = numOP
        _numProd = numProd
        _nomProd = nomProd
        _numMachine = numMach
        _numPointage = numPointage
        _dateEtiq = dateScan
        _numLot = numLot
        _quantite = quantite
        _type = type
        _numCharge = numCharge
    End Sub

    Private Sub getMachineEtiq()
        Dim req As New SqlCommand("SELECT DISTINCT IONUMMACH, IONUMPOINTAGE, IODATESCAN, SESECTEUR, ioquantitetotale FROM SAISIE_CONT.dbo.IO INNER JOIN SAISIE_CONT.dbo.SESSION_SAISIE ON IOCODESAISIE = SECODESAISIE WHERE IONUMETIQ = '" & _numEtiq & "' and not ionummach='???'", New SqlConnection(connectionS3SQL))
        Try
            req.Connection.Open()
            Dim lec = req.ExecuteReader
            While lec.Read
                _numMachine = lec.GetString(0)

                If Not lec.IsDBNull(1) Then
                    _numPointage = lec.GetInt32(1)
                End If

                If lec.IsDBNull(4) Then
                    _quantite = 0
                Else
                    _quantite = lec.GetInt32(4)
                End If
                _dateEtiq = lec.GetSqlDateTime(2)
            End While

            req.Connection.Close()
        Catch ex As Exception
            req.Connection.Close()
        End Try
    End Sub

    ''' <summary>
    ''' Permet de récupérer les détails d'une étiquette 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getDetailEtiq()

        Dim req As New SqlCommand("select NOPROD, NOLOT from GOPAL.dbo.DEMANDE where NOLOT in (select distinct nolot FROM GOPAL.dbo.DETAIL_ETIQ WHERE NOOF='" & _numOF & "' AND NOOP='" & _numOP & "' AND NO_ETIQ='" & _numContainer & "')", New SqlConnection(connectionS3SQL))
        Try
            req.Connection.Open()
            Dim lec = req.ExecuteReader
            If lec.Read Then
                _numProd = lec.GetString(0)
                _numLot = lec.GetString(1)
            End If

            req.Connection.Close()
        Catch ex As Exception
            req.Connection.Close()
        End Try

        getABVNomProd(_numProd, _nomProd)
    End Sub

    ''' <summary>
    ''' Permet de récupérer les détails d'une étiquette GOPAL
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getDetailEtiqGOPAL()
        Dim req As New SqlCommand("SELECT DISTINCT NOLOT, NOOF, NOOP, NO_ETIQ FROM GOPAL.dbo.DETAIL_ETIQ WHERE NO_GOPAL = '" & _numEtiq & "'", New SqlConnection(connectionS3SQL))
        Try
            req.Connection.Open()
            Dim lec = req.ExecuteReader
            While lec.Read
                _numLot = lec.GetString(0)
                _numOF = lec.GetInt64(1)
                _numOP = lec.GetString(2)
                _numContainer = lec.GetInt32(3)
            End While
            req.Connection.Close()
        Catch ex As Exception
            req.Connection.Close()
        End Try

        If Not _numLot = "" Then
            getDetailsEtiquette(_numLot, _numProd)
            getABVNomProd(_numProd, _nomProd)
        Else
            Exit Sub
        End If
    End Sub

    Public Sub getDetailsEtiquette(ByVal nolot As String, ByRef noProd As String)
        Dim req As New SqlCommand("select NOPROD from GOPAL.dbo.DEMANDE where NOLOT='" & nolot & "'", New SqlConnection(connectionS3SQL))
        Try
            req.Connection.Open()
            Dim lec = req.ExecuteReader
            If lec.Read Then
                noProd = lec.GetString(0)
            End If
            req.Connection.Close()
        Catch ex As Exception
            req.Connection.Close()
        End Try
    End Sub

    Public Sub getABVNomProd(ByVal noProd As String, ByRef abvProd As String)
        Dim req As New SqlCommand("SELECT Name FROM DT_Articles where [Item number]= '" & noProd & "'", New SqlConnection(connectionT3BPW))
        Try
            req.Connection.Open()
            Dim lec = req.ExecuteReader
            If lec.Read Then
                abvProd = lec.GetString(0)
            End If
            req.Connection.Close()
        Catch ex As Exception
            req.Connection.Close()
        End Try
    End Sub

    Public Function getNumOf() As String
        Return _numOF
    End Function

    Public Function getNumOP() As String
        Return _numOP
    End Function

    Public Function getNumCont() As String
        Return _numContainer
    End Function

    Public Function getNumEtiqAvecT() As String
        Dim numEtiqWithOutSlash = _numEtiq

        numEtiqWithOutSlash = _numEtiq.Replace("/", "T")
        numEtiqWithOutSlash = numEtiqWithOutSlash.Replace("-", "X")

        Return "T" + numEtiqWithOutSlash
    End Function

    Public Function getNumEtiq() As String
        Return _numEtiq
    End Function

    Public Function getNumMachine() As String
        Return _numMachine
    End Function

    Public Function getNumPointage() As String
        Return _numPointage
    End Function

    'Public Function getDate() As String
    '    Return _dateEtiq.Split(" ")(0)
    'End Function

    Public Function getDateAvecHeure() As DateTime
        Return _dateEtiq
    End Function

    Public Function getLot() As String
        Return _numLot
    End Function

    Public Function getQuantite() As Integer
        Return _quantite
    End Function

    Public Function getProduit() As String
        Return _numProd & " -- " & _nomProd
    End Function

    Public Function getTypeE() As String
        Return _type
    End Function

    'Public Overrides Function toString() As String
    '    If getNumMachine() = "" And getDate() = "" Then
    '        Return _numEtiq
    '    End If
    '    Return _numEtiq & " + " & getNumMachine() & " + " & getDate()
    'End Function

    Public Function test() As Boolean
        Return getNumOf() & "/" & getNumOP() & "/" & getNumCont() = getNumEtiq()
    End Function


    ''' <summary>
    '''     Retourne le nombre de ligne correspondant au numéro d'étiquette dans la table IO
    ''' </summary>
    ''' <returns>Le nombre de ligne correspondant au nombre de ligne de cette étiquette</returns>
    Public Function IsInIO() As Integer
        Dim req As New SqlCommand("SELECT COUNT(*) FROM IO WHERE IONUMETIQ = '" & _numEtiq & "'", New SqlConnection(connectionS3SQL))
        Try
            req.Connection.Open()
            Dim lec = req.ExecuteReader
            If lec.Read Then
                Dim x = lec.GetInt32(0)
                Return x
            End If
            Return 0
        Catch ex As Exception
            req.Connection.Close()
            Return 0
        End Try
    End Function


    ''' <summary>
    '''     Retourne le type de la ligne d'une etiquette donnée uniquement lorsqu'il y a une seule ligne
    ''' </summary>
    ''' <returns>Le Type "Entree" ou "Sortie" ou ""</returns>
    Public Function FindTypeOfEtiq() As String
        Dim req As New SqlCommand("SELECT IOTYPE FROM IO WHERE IONUMETIQ = '" & _numEtiq & "'", New SqlConnection(connectionS3SQL))
        Try
            req.Connection.Open()
            Dim lec = req.ExecuteReader
            If lec.Read Then
                Dim x = lec.GetString(0)
                Return x
            End If
        Catch ex As Exception
            req.Connection.Close()
            Return ""
        End Try
        Return ""
    End Function


    'Public Sub initOPPrecedent()
    '    Dim req As New OracleCommand("SELECT VOOPNO FROM MVXJDTA.MWOOPE WHERE VOCONO='52' AND VOFACI='009' AND VOPRNO='" & numProd & "' AND VOMFNO= " & numOF & " AND VOOPNO IN (SELECT MAX(VOOPNO) FROM MVXJDTA.MWOOPE WHERE VOCONO='52' AND VOFACI='009' AND VOPRNO='" & numProd & "' AND VOMFNO= " & numOF & " AND VOOPNO < '" & numOP & "' AND VODOID <> 'HG' AND VOPLGR <> 'AVDPSST') ", New OracleConnection(connectionM3))
    '    Try
    '        req.Connection.Open()
    '        Dim lec = req.ExecuteReader
    '        If lec.Read Then
    '            numOPPrecedent = lec.GetInt32(0)
    '        End If

    '        req.Connection.Close()
    '    Catch ex As Exception
    '        req.Connection.Close()
    '    End Try
    'End Sub

    'Public Sub initOPSuivant()
    '    Dim req As New OracleCommand("SELECT VOOPNO FROM MVXJDTA.MWOOPE WHERE VOCONO='52' AND VOFACI='009' AND VOPRNO='" & numProd & "' AND VOMFNO= " & numOF & " AND VOOPNO IN (SELECT MIN(VOOPNO) FROM MVXJDTA.MWOOPE WHERE VOCONO='52' AND VOFACI='009' AND VOPRNO='" & numProd & "' AND VOMFNO= " & numOF & " AND VOOPNO > '" & numOP & "' AND VODOID <> 'HG' AND VOPLGR <> 'AVDPSST') ", New OracleConnection(connectionM3))
    '    Try
    '        req.Connection.Open()
    '        Dim lec = req.ExecuteReader
    '        If lec.Read Then
    '            numOPSuivant = lec.GetString(0)
    '        End If
    '        req.Connection.Close()
    '    Catch ex As Exception
    '        req.Connection.Close()
    '    End Try
    'End Sub

    'Public Sub getEtiqPrecedent()
    '    Dim noEtiq As String = ""
    '    Dim req As New SqlCommand("SELECT DISTINCT IONUMETIQ FROM SAISIE_CONT.dbo.IO WHERE IONUMOF='" & numOF & "' AND IONUMOP= '" & numOPPrecedent & "' AND IODATESCAN IN (SELECT max(IODATESCAN) FROM SAISIE_CONT.dbo.IO WHERE IODATESCAN < '" & dateEtiq & "' AND IONUMOF='" & numOF & "' AND IONUMOP='" & numOPPrecedent & "' AND IOTYPE='Entree' )", New SqlConnection(connectionS3SQL))
    '    Try
    '        req.Connection.Open()
    '        Dim lec = req.ExecuteReader
    '        If lec.Read Then
    '            noEtiq = lec.GetString(0)
    '            precedente = New Etiquette(lec.GetString(0))
    '        End If
    '        req.Connection.Close()
    '        If Not noEtiq = "" Then
    '            precedente.initOPPrecedent()
    '            precedente.getEtiqPrecedent()
    '        End If
    '    Catch ex As Exception
    '        req.Connection.Close()
    '    End Try

    'End Sub

    'Public Sub getEtiqSuivante()
    '    Dim noEtiq As String = ""
    '    Dim req As New SqlCommand("SELECT DISTINCT IONUMETIQ FROM SAISIE_CONT.dbo.IO WHERE IONUMOF='" & numOF & "' AND IONUMOP= '" & numOPSuivant & "' AND IODATESCAN IN (SELECT min(IODATESCAN) FROM SAISIE_CONT.dbo.IO WHERE IODATESCAN > '" & dateEtiq & "' AND IONUMOF='" & numOF & "' AND IONUMOP='" & numOPSuivant & "' and iotype='Sortie')", New SqlConnection(connectionS3SQL))
    '    Try
    '        req.Connection.Open()
    '        Dim lec = req.ExecuteReader
    '        If lec.Read Then
    '            noEtiq = lec.GetString(0)
    '            suivante = New Etiquette(lec.GetString(0))
    '        End If
    '        req.Connection.Close()
    '        If Not noEtiq = "" Then
    '            suivante.initOPSuivant()
    '            suivante.getEtiqSuivante()
    '        End If
    '    Catch ex As Exception
    '        req.Connection.Close()
    '    End Try
    'End Sub

End Class
