<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Accueil
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.BtnEntree = New System.Windows.Forms.Button
        Me.BtnSortie = New System.Windows.Forms.Button
        Me.BtnQuitApp = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'BtnEntree
        '
        Me.BtnEntree.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.BtnEntree.Font = New System.Drawing.Font("Tahoma", 30.0!, System.Drawing.FontStyle.Bold)
        Me.BtnEntree.Location = New System.Drawing.Point(3, 13)
        Me.BtnEntree.Name = "BtnEntree"
        Me.BtnEntree.Size = New System.Drawing.Size(234, 108)
        Me.BtnEntree.TabIndex = 0
        Me.BtnEntree.Text = "Arrivée"
        '
        'BtnSortie
        '
        Me.BtnSortie.BackColor = System.Drawing.Color.LightGreen
        Me.BtnSortie.Font = New System.Drawing.Font("Tahoma", 30.0!, System.Drawing.FontStyle.Bold)
        Me.BtnSortie.Location = New System.Drawing.Point(3, 127)
        Me.BtnSortie.Name = "BtnSortie"
        Me.BtnSortie.Size = New System.Drawing.Size(234, 108)
        Me.BtnSortie.TabIndex = 1
        Me.BtnSortie.Text = "Départ"
        '
        'BtnQuitApp
        '
        Me.BtnQuitApp.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.BtnQuitApp.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Bold)
        Me.BtnQuitApp.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.BtnQuitApp.Location = New System.Drawing.Point(3, 241)
        Me.BtnQuitApp.Name = "BtnQuitApp"
        Me.BtnQuitApp.Size = New System.Drawing.Size(234, 50)
        Me.BtnQuitApp.TabIndex = 2
        Me.BtnQuitApp.Text = "Quitter l'application"
        '
        'Accueil
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.Controls.Add(Me.BtnQuitApp)
        Me.Controls.Add(Me.BtnSortie)
        Me.Controls.Add(Me.BtnEntree)
        Me.Name = "Accueil"
        Me.Text = "Accueil"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnEntree As System.Windows.Forms.Button
    Friend WithEvents BtnSortie As System.Windows.Forms.Button
    Friend WithEvents BtnQuitApp As System.Windows.Forms.Button

End Class
