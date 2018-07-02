Public Class VueDetails

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        InitializeComponent()
        InitTextLabel()
    End Sub

    Private Sub InitTextLabel()
        LbLot.Content = etiqDetail.getLot
        LbOF.Content = etiqDetail.getNumOf
        LbEtiq.Content = etiqDetail.getNumEtiq
        LbMachine.Content = etiqDetail.getNumMachine
        LbProduit.Content = etiqDetail.getProduit
        LbQuantite.Content = etiqDetail.getQuantite
        'LDate.Text = etiqDetail.getDate
        'LPointage.Text = etiqDetail.getNumPointage

    End Sub


End Class
