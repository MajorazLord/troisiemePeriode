Imports System.Data

Public Class AuditUnit
    Public codeCont As String
    Public qtiteEcrite As Integer
    Public qtitePesee As Integer
    Public qtiteSaisie As Integer
    Public qtiteTheorique As Integer

    Public Sub New(ByVal codeC As String, ByVal qtiteE As Integer, ByVal qtiteP As Integer, ByVal qtiteS As Integer, ByVal qtiteT As Integer)
        codeCont = codeC
        qtiteEcrite = qtiteE
        qtitePesee = qtiteP
        qtiteSaisie = qtiteS
        qtiteTheorique = qtiteT
    End Sub

End Class
