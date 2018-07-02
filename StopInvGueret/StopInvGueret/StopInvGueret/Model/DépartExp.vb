Public Class DépartExp
    Public Pièces As String 'IOABVNomProd
    Public NoProduit As String
    Public NoOf As Integer
    Public NoOp As String
    Public DesignOp As String
    Public Quantite As Integer
    Public PoidsNet As Integer
    Public NbCont As Integer
    Public Remarque As String 'NoCharge
    Public Flag_CV As Boolean
    Public NumEtiq As String

    Public Sub New()
    End Sub

    Public Sub New(ByVal pieces As String, ByVal noProd As String, ByVal nOf As Integer, ByVal nOp As String, ByVal qtite As Integer, ByVal poids As Integer, ByVal nbC As Integer, ByVal remarq As String, ByVal flagcv As Boolean, ByVal noEtiq As String, ByVal designO As String)
        Pièces = pieces
        NoProduit = noProd
        NoOf = nOf
        NoOp = nOp
        Quantite = qtite
        PoidsNet = poids
        NbCont = nbC
        Remarque = remarq
        Flag_CV = flagcv
        NumEtiq = noEtiq
        DesignOp = designO
    End Sub

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Dim dep = TryCast(obj, DépartExp)
        Return Me.Pièces.Equals(dep.Pièces) And Me.NoProduit.Equals(dep.NoProduit) And Me.NoOf.Equals(dep.NoOf) And Me.NoOp.Equals(dep.NoOp) And Me.Remarque.Equals(dep.Remarque)
    End Function
End Class
