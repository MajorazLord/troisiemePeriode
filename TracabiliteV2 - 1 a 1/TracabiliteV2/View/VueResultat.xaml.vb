Public Class VueResultat

    Public listEtiq As List(Of Etiquette) = New List(Of Etiquette)

    Private WithEvents buttonPrincipal As New Button

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

        LbNumEtiq.Content = myEtiq.getNumEtiq

        buttonPrincipal.Content = myEtiq.toString
        buttonPrincipal.Name = myEtiq.getNumEtiqAvecT
        buttonPrincipal.Height = 60
        buttonPrincipal.HorizontalContentAlignment = HorizontalAlignment.Center
        buttonPrincipal.VerticalContentAlignment = VerticalAlignment.Center

        buttonPrincipal.FontFamily = New FontFamily("Microsoft Sans Serif")
        buttonPrincipal.FontSize = 14

        'PResultat.Children.Add(buttonPrincipal)
        listEtiq.Add(myEtiq)
        AddHandler buttonPrincipal.Click, AddressOf click_Button

        'Dim etiqPrec = myEtiq.getPrecedent

        'While Not etiqPrec Is Nothing
        '    Dim myButton As New Button
        '    myButton.Content = etiqPrec.toString
        '    myButton.Name = etiqPrec.getNumEtiqAvecT
        '    myButton.Height = 35
        '    myButton.HorizontalContentAlignment = HorizontalAlignment.Center
        '    myButton.VerticalContentAlignment = VerticalAlignment.Center
        '    myButton.FontFamily = New FontFamily("Microsoft Sans Serif")
        '    myButton.FontSize = 14

        '    listEtiq.Add(etiqPrec)
        '    'PResultat.Children.Add(myButton)
        '    AddHandler myButton.Click, AddressOf click_Button

        '    etiqPrec = etiqPrec.getPrecedent
        'End While

        'Dim etiqSuiv = myEtiq.getSuivant

        'While Not etiqSuiv Is Nothing
        '    Dim myButton As New Button
        '    myButton.Content = etiqSuiv.toString
        '    myButton.Name = etiqSuiv.getNumEtiqAvecT
        '    myButton.Height = 35
        '    myButton.HorizontalContentAlignment = HorizontalAlignment.Center
        '    myButton.VerticalContentAlignment = VerticalAlignment.Center

        '    myButton.FontFamily = New FontFamily("Microsoft Sans Serif")

        '    listEtiq.Add(etiqSuiv)
        '    'PResultat.Children.Add(myButton)
        '    AddHandler myButton.Click, AddressOf click_Button

        '    etiqSuiv = etiqSuiv.getSuivant
        '    'myButton.BringToFront()
        'End While

        'Dim nbButton = PResultat.Children.Count
        Dim nbButton = listEtiq.Count

        Dim x = listEtiq.OrderBy((Function(m As Etiquette) m.getNumOP))

        For Each elem As Etiquette In x
            Debug.WriteLine("aled" + elem.getNumOP)
            Dim myButton As New Button
            myButton.Content = elem.toString
            myButton.Name = elem.getNumEtiqAvecT
            myButton.Height = 35
            myButton.HorizontalContentAlignment = HorizontalAlignment.Center
            myButton.VerticalContentAlignment = VerticalAlignment.Center

            myButton.FontFamily = New FontFamily("Microsoft Sans Serif")

            PResultat.Children.Add(myButton)
            AddHandler myButton.Click, AddressOf click_Button


        Next

        For Each element As Control In PResultat.Children
            Debug.WriteLine(element.Name)
            element.Height = PResultat.Height / nbButton
        Next
    End Sub

    Private Sub click_Button(sender As Object, e As EventArgs)
        Dim nameToCut As String = sender.Name
        Dim x = nameToCut.TrimStart("T")
        Dim y As String = x.Replace("T", "/")
        etiqDetail = getinfoEtiquette(y)

        Dim details As New VueDetails
        details.Show()
    End Sub




End Class
