Public Class Sortie
    Public Const IOCodeSaisie As String = "2"
    Public Const IONumPointage As Integer = 9999
    Public Const IOType As String = "Sortie"
    Public Const IONumMach As String = "AE9999"
    Public IONumLot As String
    Public IONumOF As String
    Public IONumEtiq As String
    Public IONoProd As String
    Public IOABVNomProd As String
    Public IOQtiteRea As Integer
    Public IOQtiteTot As Integer
    Public IODateScan As String
    Public Const IOBlocked As Integer = 0
    Public Const IONumCharge As String = ""
    Public Const IONumMatrice As String = ""
    Public Const IONumVague As String = ""
    Public IONumOP As String = ""
    Public Const IOPdcSuivant = Nothing

    Public Sub New()
    End Sub

    Public Sub New(ByVal numLot As String, ByVal numOF As String, ByVal numEtiq As String, ByVal noProd As String, ByVal abvNomProd As String, ByVal qtiteR As Integer, ByVal qtiteT As Integer, ByVal dateScan As String, ByVal numOP As String)
        IONumLot = numLot
        IONumOF = numOF
        IONumEtiq = numEtiq
        IONoProd = noProd
        IOABVNomProd = abvNomProd
        IOQtiteRea = qtiteR
        IOQtiteTot = qtiteT
        IODateScan = dateScan
        IONumOP = numOP
    End Sub





End Class
