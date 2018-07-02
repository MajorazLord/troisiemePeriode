<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmListNumCharge
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
        Me.BVerif = New System.Windows.Forms.Button
        Me.PBRetour = New System.Windows.Forms.PictureBox
        Me.DGNumCharge = New System.Windows.Forms.DataGrid
        Me.SuspendLayout()
        '
        'BVerif
        '
        Me.BVerif.BackColor = System.Drawing.Color.Lime
        Me.BVerif.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BVerif.Location = New System.Drawing.Point(61, 237)
        Me.BVerif.Name = "BVerif"
        Me.BVerif.Size = New System.Drawing.Size(176, 55)
        Me.BVerif.TabIndex = 6
        Me.BVerif.Text = "Valider Num Charge"
        '
        'PBRetour
        '
        Me.PBRetour.Location = New System.Drawing.Point(1, 237)
        Me.PBRetour.Name = "PBRetour"
        Me.PBRetour.Size = New System.Drawing.Size(55, 55)
        Me.PBRetour.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        '
        'DGNumCharge
        '
        Me.DGNumCharge.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGNumCharge.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.DGNumCharge.Location = New System.Drawing.Point(0, 0)
        Me.DGNumCharge.Name = "DGNumCharge"
        Me.DGNumCharge.PreferredRowHeight = 25
        Me.DGNumCharge.RowHeadersVisible = False
        Me.DGNumCharge.Size = New System.Drawing.Size(240, 233)
        Me.DGNumCharge.TabIndex = 5
        '
        'frmListNumCharge
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.Controls.Add(Me.BVerif)
        Me.Controls.Add(Me.PBRetour)
        Me.Controls.Add(Me.DGNumCharge)
        Me.Name = "frmListNumCharge"
        Me.Text = "frmListNumCharge"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BVerif As System.Windows.Forms.Button
    Friend WithEvents PBRetour As System.Windows.Forms.PictureBox
    Friend WithEvents DGNumCharge As System.Windows.Forms.DataGrid
End Class
