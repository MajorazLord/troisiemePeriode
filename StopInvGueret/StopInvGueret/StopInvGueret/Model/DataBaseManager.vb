Imports System.Data.SqlClient

Public Class DataBaseManager

    Public Sub insertOrUpdtExp(ByVal dep As DépartExp)

        Dim reqExpFromFE As SqlCommand

        reqExpFromFE = New SqlCommand("Select * from FICHE_EXPEDITION where PIECES = '" & dep.Pièces & "' and NOPROD = '" & dep.NoProduit & "' and NOOF = '" & dep.NoOf & "' and NOOP = '" & dep.NoOp & "' and REMARQUE= '" & dep.Remarque & "' and DATE= '" & Format(dateSaisie, "dd/MM/yyyy") & "' and IDUNIQUE= " & idUniqueJour & "", New SqlConnection(connS3SQL))
        reqExpFromFE.CommandTimeout = 2

        reqExpFromFE.Connection.Open()

        Dim data = reqExpFromFE.ExecuteReader()

        If data.Read Then 'Il y a deja une ligne pour cette exp, on va la mettre à jour '
            Dim update As New SqlCommand("Update FICHE_EXPEDITION set QUANTITE = " & dep.Quantite & ", POIDSNET= " & dep.PoidsNet & ", NBCONTENANT = " & dep.NbCont & " where PIECES = '" & dep.Pièces & "' and NOPROD = '" & dep.NoProduit & "' and NOOF = '" & dep.NoOf & "' and NOOP = '" & dep.NoOp & "' and REMARQUE= '" & dep.Remarque & "' and DATE= '" & Format(dateSaisie, "dd/MM/yyyy") & "' and IDUNIQUE= " & idUniqueJour & " AND FLAG_R = 'False'", New SqlConnection(connS3SQL))
            update.CommandTimeout = 2

            update.Connection.Open()
            update.ExecuteNonQuery()
            update.Connection.Close()

        Else 'Aucune ligne trouvée, on insert simplement'

            Dim insert As New SqlCommand("Insert into FICHE_EXPEDITION (PIECES, NOPROD, NOOF, NOOP, QUANTITE, POIDSNET, NBCONTENANT, REMARQUE, DATE, IDUNIQUE, FLAG_R, FLAG_CV, DESIGNOP) values ('" & dep.Pièces & "', '" & dep.NoProduit & "', '" & dep.NoOf & "', '" & dep.NoOp & "', " & dep.Quantite & "," & dep.PoidsNet & "," & dep.NbCont & ",'" & dep.Remarque & "','" & Format(dateSaisie, "dd/MM/yyyy") & "'," & idUniqueJour & ", 'False', '" & dep.Flag_CV & "', '" & dep.DesignOp & "')", New SqlConnection(connS3SQL))
            insert.CommandTimeout = 2

            insert.Connection.Open()
            insert.ExecuteNonQuery()
            insert.Connection.Close()
        End If
        reqExpFromFE.Connection.Close()

    End Sub

    Public Function checkIfCrashOrPb() As Boolean
        Dim reqCrash As SqlCommand

        reqCrash = New SqlCommand("Select FLAG_R from FICHE_EXPEDITION ORDER BY IDUNIQUE DESC", New SqlConnection(connS3SQL))
        reqCrash.CommandTimeout = 2

        reqCrash.Connection.Open()

        Dim data = reqCrash.ExecuteReader()

        If data.Read Then 'Il y a deja une ligne pour cette exp, on va la mettre à jour '
            If (data(0) = True) Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If
        Return True
        reqCrash.Connection.Close()

    End Function

    Public Sub majFlagFinSaisie()
        Dim update As New SqlCommand("Update FICHE_EXPEDITION set FLAG_R = 'True' where DATE = '" & Format(dateSaisie, "dd/MM/yyyy") & "' AND IDUNIQUE = (SELECT TOP 1 IDUNIQUE FROM FICHE_EXPEDITION ORDER BY IDUNIQUE DESC)", New SqlConnection(connS3SQL))
        Try
            update.CommandTimeout = 2

            update.Connection.Open()
            update.ExecuteNonQuery()
            update.Connection.Close()
        Catch ex As Exception
            'Sur les conseils de Taoufik on laisse vide
        End Try
    End Sub

    Public Sub majFlagDebutSaisieSiPrecedentCrashed()
        Dim update As New SqlCommand("Update FICHE_EXPEDITION set FLAG_R = 'True' where IDUNIQUE = (SELECT TOP 1 IDUNIQUE FROM FICHE_EXPEDITION ORDER BY DATE, IDUNIQUE DESC)", New SqlConnection(connS3SQL))
        Try
            update.CommandTimeout = 2

            update.Connection.Open()
            update.ExecuteNonQuery()
            update.Connection.Close()
        Catch ex As Exception
            'Sur les conseils de Taoufik on laisse vide
        End Try
    End Sub


End Class
