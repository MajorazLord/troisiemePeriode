Public Class EtiquetteManager
    Private listEntree As Dictionary(Of String, String)
    Private listSortie As Dictionary(Of String, String)
    Private dicolienES As Dictionary(Of String, List(Of String))

    Public Sub New()
        listEntree = New Dictionary(Of String, String)
        listSortie = New Dictionary(Of String, String)
        dicolienES = New Dictionary(Of String, List(Of String))
    End Sub

    Public Sub addEntree(ByVal etiq As String, ByVal dateEntree As String)
        listEntree.Add(etiq, dateEntree)
    End Sub

    Public Sub addSortie(ByVal etiq As String, ByVal dateSortie As String)
        listSortie.Add(etiq, dateSortie)
    End Sub

    Public Sub creationLienES()
        Dim entree As String = ""
        For Each element In listEntree.Keys
            If Not entree = "" Then
                For Each sortie In listSortie.Keys
                    If Convert.ToDateTime(listSortie.Item(sortie)) < Convert.ToDateTime(listEntree.Item(element)) Then
                        If Not isAlreadySortie(sortie) Then
                            addLienES(entree, sortie)
                        End If
                    End If
                Next
            End If
            entree = element
        Next
    End Sub

    Private Sub addLienES(ByVal entree As String, ByVal sortie As String)
        If dicolienES.ContainsKey(entree) Then
            dicolienES.Item(entree).Add(sortie)
        Else
            Dim list = New List(Of String)
            list.Add(sortie)
            dicolienES.Add(entree, list)
        End If
    End Sub

    Private Function isAlreadySortie(ByVal sortie As String)
        For Each entre In dicolienES.Keys
            For Each value In dicolienES.Item(entre)
                If value = sortie Then
                    Return True
                End If
            Next
        Next
        Return False
    End Function

    'Public Sub afficherTracabilite(ByVal etiquette As Etiquette)
    '    Debug.WriteLine(etiquette.toString)
    '    While Not etiquette.getPrecedent Is Nothing
    '        Debug.WriteLine(etiquette.getPrecedent.toString)
    '        etiquette = etiquette.getPrecedent
    '    End While
    'End Sub

End Class

