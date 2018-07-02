<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class ValideImg
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
        Me.PbBH = New System.Windows.Forms.PictureBox
        Me.LbDescValid = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'PbBH
        '
        Me.PbBH.Location = New System.Drawing.Point(0, 0)
        Me.PbBH.Name = "PbBH"
        Me.PbBH.Size = New System.Drawing.Size(240, 232)
        Me.PbBH.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        '
        'LbDescValid
        '
        Me.LbDescValid.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold)
        Me.LbDescValid.Location = New System.Drawing.Point(0, 235)
        Me.LbDescValid.Name = "LbDescValid"
        Me.LbDescValid.Size = New System.Drawing.Size(240, 59)
        Me.LbDescValid.Text = "Départ ajouté avec succés !"
        Me.LbDescValid.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'ValideImg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.Controls.Add(Me.LbDescValid)
        Me.Controls.Add(Me.PbBH)
        Me.Name = "ValideImg"
        Me.Text = "ValideImg"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PbBH As System.Windows.Forms.PictureBox
    Friend WithEvents LbDescValid As System.Windows.Forms.Label
End Class
