Public Class Arrivee

#Region "Declaration Attributs"
    Public Const IOCodeSaisie As String = "01"
    Public Const IONumPointage As String = "9999"
    Public Const IOType As String = "Sortie"
    Public IONumMach As String
    Public IONumLot As String
    Public IONumOF As String
    Public IONumEtiq As String
    Public IONoProd As String
    Public IOQtiteRea As String
    Public IOQtiteTot As String
    Public IODateScan As Date
    Public Const IOBlocked As Integer = 0
    Public Const IONumCharge As String = ""
    Public Const IONumMatrice As String = ""
    Public Const IONumVague As String = ""
    Public IONumOP As Integer
    Public IOPdcSuivant As String
#End Region

#Region "Constructeur"
    Public Sub New(ByVal numMach As String, ByVal numLot As String, ByVal numOF As String, ByVal numEtiq As String, ByVal noProd As String, ByVal qtiteR As String, ByVal qtiteT As String, ByVal dateScan As Date, ByVal numOP As Integer, ByVal pdcSuivant As String)
        IONumMach = numMach
        IONumLot = numLot
        IONumOF = numOF
        IONumEtiq = numEtiq
        IONoProd = noProd
        IOQtiteRea = qtiteR
        IOQtiteTot = qtiteT
        IODateScan = dateScan
        IONumOP = numOP
        IOPdcSuivant = pdcSuivant
    End Sub
#End Region

End Class
