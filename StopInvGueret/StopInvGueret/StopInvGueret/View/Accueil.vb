Imports Datalogic.API

Public Class Accueil

    Private Sub BtnEntree_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEntree.Click
        Dim frmEntree As New Arrivée
        frmEntree.ShowDialog()
    End Sub

    Private Sub BtnSortie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSortie.Click
        Dim frmSortie As New Départ
        frmSortie.ShowDialog()
    End Sub

    Private Sub Accueil_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        currentScreenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height
        currentScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width

        myUser = New User()

    End Sub

    Private Sub BtnQuitApp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnQuitApp.Click
        Dim res = MessageBox.Show("Êtes-vous sur de vouloir quitter l'application ?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If res = Windows.Forms.DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub
End Class
