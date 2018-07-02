<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class PageAjoutValid
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
    Private mainMenu1 As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.mainMenu1 = New System.Windows.Forms.MainMenu
        Me.PBPAV = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'PBPAV
        '
        Me.PBPAV.Location = New System.Drawing.Point(30, 3)
        Me.PBPAV.Name = "PBPAV"
        Me.PBPAV.Size = New System.Drawing.Size(182, 188)
        Me.PBPAV.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(3, 198)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(234, 70)
        Me.Label1.Text = "Saisie Enregistrée !"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'PageAjoutValid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(240, 268)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PBPAV)
        Me.Menu = Me.mainMenu1
        Me.Name = "PageAjoutValid"
        Me.Text = "PageAjoutValid"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PBPAV As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
