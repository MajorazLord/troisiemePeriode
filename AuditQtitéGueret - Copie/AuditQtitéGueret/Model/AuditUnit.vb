Imports System.Data

Public Class AuditUnit
    Public codeCont As String
    Public qtiteSaisie As Integer
    Public qtiteVerif As Integer
    Public dateVerif As DateTime

    Public Sub New(ByVal codeC As String, ByVal qtiteS As Integer, ByVal qtiteV As Integer, ByVal dateV As DateTime)
        codeCont = codeC
        qtiteSaisie = qtiteS
        qtiteVerif = qtiteV
        dateVerif = dateV
    End Sub

End Class
