Imports System.Data

Public Class User

    Public DTArrivee As DataTable
    Public DTDépart As DataTable

    Public DTPoidsUnitaire As DataTable

    Public Sub New()
        initDT()
    End Sub

    Private Sub initDT()
        DTArrivee = New DataTable
        DTArrivee.Columns.Add("NoEtiq", GetType(String))
        DTArrivee.Columns.Add("Quantité", GetType(String))

        DTDépart = New DataTable
        DTDépart.Columns.Add("NoEtiq", GetType(String))
        DTDépart.Columns.Add("Quantité", GetType(String))
        DTDépart.Columns.Add("Poids", GetType(String))
        DTDépart.Columns.Add("Flag_CV", GetType(Boolean))

        DTPoidsUnitaire = New DataTable
        DTPoidsUnitaire.Columns.Add("NoProd", GetType(String))
        DTPoidsUnitaire.Columns.Add("NoOp", GetType(Integer))

        'Dim keys(1) As DataColumn
        'Dim datacol = New DataColumn()
        'datacol.DataType = GetType(Integer)
        'datacol.ColumnName = "NoOp"
        'DTPoidsUnitaire.Columns.Add(datacol)

        'keys(0) = datacol
        'DTPoidsUnitaire.PrimaryKey = keys
        DTPoidsUnitaire.Columns.Add("HgStatus", GetType(String))
        DTPoidsUnitaire.Columns.Add("PoidsU", GetType(Integer))

    End Sub

    Public Function getDTArrivee() As DataTable
        Return DTArrivee
    End Function

    Public Function getDTDépart() As DataTable
        Return DTDépart
    End Function

    Public Function getDTPoidsU() As DataTable
        Return DTPoidsUnitaire
    End Function

End Class
