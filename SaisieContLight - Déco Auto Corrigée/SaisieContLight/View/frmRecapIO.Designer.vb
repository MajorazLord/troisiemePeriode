﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmRecapIO
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
        Me.PBRetour = New System.Windows.Forms.PictureBox
        Me.LbTitre = New System.Windows.Forms.Label
        Me.DGIO = New System.Windows.Forms.DataGrid
        Me.BDelete = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'PBRetour
        '
        Me.PBRetour.Location = New System.Drawing.Point(0, 228)
        Me.PBRetour.Name = "PBRetour"
        Me.PBRetour.Size = New System.Drawing.Size(69, 66)
        Me.PBRetour.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        '
        'LbTitre
        '
        Me.LbTitre.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.LbTitre.ForeColor = System.Drawing.Color.LimeGreen
        Me.LbTitre.Location = New System.Drawing.Point(0, 2)
        Me.LbTitre.Name = "LbTitre"
        Me.LbTitre.Size = New System.Drawing.Size(240, 25)
        Me.LbTitre.Text = "Récap Entrées/Sorties"
        Me.LbTitre.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'DGIO
        '
        Me.DGIO.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGIO.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.DGIO.Location = New System.Drawing.Point(0, 30)
        Me.DGIO.Name = "DGIO"
        Me.DGIO.PreferredRowHeight = 25
        Me.DGIO.RowHeadersVisible = False
        Me.DGIO.Size = New System.Drawing.Size(240, 192)
        Me.DGIO.TabIndex = 3
        '
        'BDelete
        '
        Me.BDelete.BackColor = System.Drawing.Color.Red
        Me.BDelete.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BDelete.Location = New System.Drawing.Point(75, 228)
        Me.BDelete.Name = "BDelete"
        Me.BDelete.Size = New System.Drawing.Size(162, 66)
        Me.BDelete.TabIndex = 6
        Me.BDelete.Text = "Supprimer la ligne"
        '
        'frmRecapIO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.Controls.Add(Me.BDelete)
        Me.Controls.Add(Me.DGIO)
        Me.Controls.Add(Me.LbTitre)
        Me.Controls.Add(Me.PBRetour)
        Me.KeyPreview = True
        Me.Name = "frmRecapIO"
        Me.Text = "Récap IO"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PBRetour As System.Windows.Forms.PictureBox
    Friend WithEvents LbTitre As System.Windows.Forms.Label
    Friend WithEvents DGIO As System.Windows.Forms.DataGrid
    Friend WithEvents BDelete As System.Windows.Forms.Button
End Class
