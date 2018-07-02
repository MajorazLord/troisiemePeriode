<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class RecapDépart
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
        Me.LbTitre = New System.Windows.Forms.Label
        Me.DGDEP = New System.Windows.Forms.DataGrid
        Me.BDelete = New System.Windows.Forms.Button
        Me.PBRetour = New System.Windows.Forms.PictureBox
        Me.SuspendLayout()
        '
        'LbTitre
        '
        Me.LbTitre.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.LbTitre.ForeColor = System.Drawing.Color.LimeGreen
        Me.LbTitre.Location = New System.Drawing.Point(0, 1)
        Me.LbTitre.Name = "LbTitre"
        Me.LbTitre.Size = New System.Drawing.Size(240, 25)
        Me.LbTitre.Text = "Récap Départs"
        Me.LbTitre.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'DGDEP
        '
        Me.DGDEP.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGDEP.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.DGDEP.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.DGDEP.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DGDEP.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(201, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.DGDEP.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(201, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.DGDEP.HeaderForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.DGDEP.Location = New System.Drawing.Point(-3, 30)
        Me.DGDEP.Name = "DGDEP"
        Me.DGDEP.PreferredRowHeight = 25
        Me.DGDEP.RowHeadersVisible = False
        Me.DGDEP.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(55, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.DGDEP.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DGDEP.Size = New System.Drawing.Size(240, 192)
        Me.DGDEP.TabIndex = 11
        '
        'BDelete
        '
        Me.BDelete.BackColor = System.Drawing.Color.Red
        Me.BDelete.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BDelete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BDelete.Location = New System.Drawing.Point(77, 226)
        Me.BDelete.Name = "BDelete"
        Me.BDelete.Size = New System.Drawing.Size(162, 66)
        Me.BDelete.TabIndex = 13
        Me.BDelete.Text = "Supprimer la ligne"
        '
        'PBRetour
        '
        Me.PBRetour.Location = New System.Drawing.Point(2, 226)
        Me.PBRetour.Name = "PBRetour"
        Me.PBRetour.Size = New System.Drawing.Size(69, 66)
        Me.PBRetour.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        '
        'RecapDépart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.Controls.Add(Me.BDelete)
        Me.Controls.Add(Me.PBRetour)
        Me.Controls.Add(Me.DGDEP)
        Me.Controls.Add(Me.LbTitre)
        Me.Name = "RecapDépart"
        Me.Text = "RecapDépart"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LbTitre As System.Windows.Forms.Label
    Friend WithEvents DGDEP As System.Windows.Forms.DataGrid
    Friend WithEvents BDelete As System.Windows.Forms.Button
    Friend WithEvents PBRetour As System.Windows.Forms.PictureBox
End Class
