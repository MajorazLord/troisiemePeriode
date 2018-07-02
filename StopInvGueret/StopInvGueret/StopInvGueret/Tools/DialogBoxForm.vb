Public Class DialogBoxForm

    ' What we'll pass in:
    Public imageToDisplay As Image = Nothing
    Public textToShow As String = ""
    Public okText As String = ""
    Public cancelText As String = ""

    ' What we'll pass back:
    Public userButtonClickChoice As String = ""



    Private Sub DialogBoxForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        OkButton.Text = okText
        CancelButton.Text = cancelText

        If textToShow IsNot Nothing AndAlso textToShow IsNot String.Empty Then
            Label1.Text = textToShow
        Else
            Label1.Visible = False
        End If
        OkButton.Focus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OkButton.Click
        userButtonClickChoice = OkButton.Text
        Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        userButtonClickChoice = CancelButton.Text
        Close()
    End Sub
End Class