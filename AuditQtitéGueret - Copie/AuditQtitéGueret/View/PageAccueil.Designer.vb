<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class PageAccueil
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
        Me.btnStart = New System.Windows.Forms.Button
        Me.BtnRep = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnStart
        '
        Me.btnStart.BackColor = System.Drawing.Color.OrangeRed
        Me.btnStart.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Bold)
        Me.btnStart.Location = New System.Drawing.Point(18, 86)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(205, 50)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "Démarrer l'audit"
        '
        'BtnRep
        '
        Me.BtnRep.BackColor = System.Drawing.Color.DarkCyan
        Me.BtnRep.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Bold)
        Me.BtnRep.Location = New System.Drawing.Point(18, 144)
        Me.BtnRep.Name = "BtnRep"
        Me.BtnRep.Size = New System.Drawing.Size(205, 50)
        Me.BtnRep.TabIndex = 1
        Me.BtnRep.Text = "Reprendre l'audit"
        '
        'PageAccueil
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.Controls.Add(Me.BtnRep)
        Me.Controls.Add(Me.btnStart)
        Me.Name = "PageAccueil"
        Me.Text = "PageAccueil"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents BtnRep As System.Windows.Forms.Button
End Class
