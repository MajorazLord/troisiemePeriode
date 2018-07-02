Imports System.IO

Public Class PageAccueil

    Dim lastCode = 0

    Private Sub PageAccueil_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        currentScreenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height
        currentScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width

        lastCode = GetLastCodeSaisie()

        If File.Exists(cheminDossierFichier) Then
            SendSaveToSql()
        End If

        If CheckIfCrashOrNot(lastCode) Or lastCode = -1 Then
            'Not Crashed
            initDT()
            BtnRep.Visible = False
        Else
            'Crashed
            BtnRep.Visible = True
        End If



    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        SetValidationTo1(lastCode)

        'Setter le new code saisie
        codeSaisie = lastCode + 1
        Dim frmSaisie = New PageSaisie
        frmSaisie.ShowDialog()
    End Sub

    Private Sub BtnRep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRep.Click
        'Setter le code saisie à l'ancienne valeur
        codeSaisie = lastCode

        'Recup les elements de la saisie precedentes
        Dim listOfAudit = RecupLastSaisie(codeSaisie)

        'Les mettres dans le DT pour afficher
        InsertListOfAuditIntoDT(listOfAudit)

        Dim frmSaisie = New PageSaisie
        frmSaisie.ShowDialog()
    End Sub
End Class