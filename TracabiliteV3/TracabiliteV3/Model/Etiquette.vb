Namespace Model
    Public Class Etiquette

        Public Property CodeSaisie As Integer
        Public Property NumPointage As Integer
        Public Property Type As String
        Public Property NumMachine As String
        Public Property NumLot As String
        Public Property NumOf As String
        Public Property NumEtiquette As String
        Public Property NoProd As String
        Public Property AbvNomProd As String
        Public Property QtiteRealise As Integer
        Public Property QtiteTotale As Integer
        Public Property DateScan As DateTime
        Public Property Blocked As Boolean
        Public Property NumCharge As String
        Public Property NumMatrice As String
        Public Property NumVague As String
        Public Property NumOp As String
        Public Property PdcSuivant As String

        Public ReadOnly Property Precs As List(Of Etiquette) = New List(Of Etiquette)
        Public ReadOnly Property Suivs As List(Of Etiquette) = New List(Of Etiquette)

        Public Sub New(codeSaisie As Integer, numPointage As Integer, type As String, numMachine As String, numLot As String, numOf As String, numEtiquette As String, noProd As String, abvNomProd As String, qtiteRealise As Integer, qtiteTotale As Integer, dateScan As Date, numCharge As String, numOp As String, pdcSuivant As String, lPrecs As List(Of Etiquette), lSuivs As List(Of Etiquette))
            Me.CodeSaisie = codeSaisie
            Me.NumPointage = numPointage
            Me.Type = type
            Me.NumMachine = numMachine
            Me.NumLot = numLot
            Me.NumOf = numOf
            Me.NumEtiquette = numEtiquette
            Me.NoProd = noProd
            Me.AbvNomProd = abvNomProd
            Me.QtiteRealise = qtiteRealise
            Me.QtiteTotale = qtiteTotale
            Me.DateScan = dateScan
            Me.NumCharge = numCharge
            Me.NumOp = numOp
            Me.PdcSuivant = pdcSuivant
            Me.Blocked = False
            Me.NumMatrice = ""
            Me.NumVague = ""
            Precs.AddRange(lPrecs)
            Suivs.AddRange(lSuivs)
        End Sub
    End Class
End Namespace