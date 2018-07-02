Public Class PageAccueil

    Private Sub PageAccueil_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        currentScreenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height
        currentScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width
    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Dim frmSaisie = New PageSaisie
        frmSaisie.ShowDialog()
    End Sub
End Class