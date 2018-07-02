Imports System.Data

Public Class AuditUnit
    Public codeCont As String
    Public qtiteSaisie As Integer
    Public qtiteVerif As Integer

    Public Sub New(ByVal codeC As String, ByVal qtiteS As Integer, ByVal qtiteV As Integer)
        codeCont = codeC
        qtiteSaisie = qtiteS
        qtiteVerif = qtiteV
    End Sub

End Class
