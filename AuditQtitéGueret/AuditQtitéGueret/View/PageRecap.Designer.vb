<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class PageRecap
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
        Me.DGIO = New System.Windows.Forms.DataGrid
        Me.LbTitre = New System.Windows.Forms.Label
        Me.PBRetour = New System.Windows.Forms.PictureBox
        Me.BDelete = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'DGIO
        '
        Me.DGIO.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGIO.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.DGIO.Location = New System.Drawing.Point(0, 31)
        Me.DGIO.Name = "DGIO"
        Me.DGIO.PreferredRowHeight = 25
        Me.DGIO.RowHeadersVisible = False
        Me.DGIO.Size = New System.Drawing.Size(240, 192)
        Me.DGIO.TabIndex = 4
        '
        'LbTitre
        '
        Me.LbTitre.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.LbTitre.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LbTitre.Location = New System.Drawing.Point(0, 2)
        Me.LbTitre.Name = "LbTitre"
        Me.LbTitre.Size = New System.Drawing.Size(240, 25)
        Me.LbTitre.Text = "Récap Audits Saisies"
        Me.LbTitre.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'PBRetour
        '
        Me.PBRetour.Location = New System.Drawing.Point(3, 228)
        Me.PBRetour.Name = "PBRetour"
        Me.PBRetour.Size = New System.Drawing.Size(69, 66)
        Me.PBRetour.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        '
        'BDelete
        '
        Me.BDelete.BackColor = System.Drawing.Color.Red
        Me.BDelete.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BDelete.Location = New System.Drawing.Point(78, 228)
        Me.BDelete.Name = "BDelete"
        Me.BDelete.Size = New System.Drawing.Size(162, 66)
        Me.BDelete.TabIndex = 9
        Me.BDelete.Text = "Supprimer la ligne"
        '
        'PageRecap
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.Controls.Add(Me.BDelete)
        Me.Controls.Add(Me.PBRetour)
        Me.Controls.Add(Me.LbTitre)
        Me.Controls.Add(Me.DGIO)
        Me.Name = "PageRecap"
        Me.Text = "PageRecap"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DGIO As System.Windows.Forms.DataGrid
    Friend WithEvents LbTitre As System.Windows.Forms.Label
    Friend WithEvents PBRetour As System.Windows.Forms.PictureBox
    Friend WithEvents BDelete As System.Windows.Forms.Button
End Class
