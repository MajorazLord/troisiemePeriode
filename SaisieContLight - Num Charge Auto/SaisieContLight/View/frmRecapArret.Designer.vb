﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmRecapArret
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
        Me.DGArret = New System.Windows.Forms.DataGrid
        Me.PBRetour = New System.Windows.Forms.PictureBox
        Me.BDelete = New System.Windows.Forms.Button
        Me.LblRecap = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'DGArret
        '
        Me.DGArret.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DGArret.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular)
        Me.DGArret.Location = New System.Drawing.Point(0, 55)
        Me.DGArret.Name = "DGArret"
        Me.DGArret.PreferredRowHeight = 25
        Me.DGArret.RowHeadersVisible = False
        Me.DGArret.Size = New System.Drawing.Size(240, 167)
        Me.DGArret.TabIndex = 1
        '
        'PBRetour
        '
        Me.PBRetour.Location = New System.Drawing.Point(0, 228)
        Me.PBRetour.Name = "PBRetour"
        Me.PBRetour.Size = New System.Drawing.Size(69, 66)
        Me.PBRetour.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        '
        'BDelete
        '
        Me.BDelete.BackColor = System.Drawing.Color.Red
        Me.BDelete.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.BDelete.Location = New System.Drawing.Point(75, 228)
        Me.BDelete.Name = "BDelete"
        Me.BDelete.Size = New System.Drawing.Size(162, 66)
        Me.BDelete.TabIndex = 2
        Me.BDelete.Text = "Supprimer la ligne"
        '
        'LblRecap
        '
        Me.LblRecap.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.LblRecap.ForeColor = System.Drawing.Color.Blue
        Me.LblRecap.Location = New System.Drawing.Point(0, 2)
        Me.LblRecap.Name = "LblRecap"
        Me.LblRecap.Size = New System.Drawing.Size(240, 50)
        Me.LblRecap.Text = "Récap temps d'arrêt machine"
        Me.LblRecap.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmRecapArret
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.Controls.Add(Me.BDelete)
        Me.Controls.Add(Me.LblRecap)
        Me.Controls.Add(Me.PBRetour)
        Me.Controls.Add(Me.DGArret)
        Me.Name = "frmRecapArret"
        Me.Text = "Récap temps arrêt"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DGArret As System.Windows.Forms.DataGrid
    Friend WithEvents PBRetour As System.Windows.Forms.PictureBox
    Friend WithEvents BDelete As System.Windows.Forms.Button
    Friend WithEvents LblRecap As System.Windows.Forms.Label
End Class
