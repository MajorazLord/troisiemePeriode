<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class PageRemonter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PageRemonter))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.BPlusTard = New System.Windows.Forms.Button
        Me.PBRemake = New System.Windows.Forms.PictureBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(3, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(234, 35)
        Me.Label1.Text = "La sauvegarde finale necessite une connection internet. "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(3, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(234, 30)
        Me.Label2.Text = "Si ce message s'affiche c'est que la connection n'est pas établie."
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'BPlusTard
        '
        Me.BPlusTard.BackColor = System.Drawing.Color.Red
        Me.BPlusTard.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Bold)
        Me.BPlusTard.Location = New System.Drawing.Point(3, 259)
        Me.BPlusTard.Name = "BPlusTard"
        Me.BPlusTard.Size = New System.Drawing.Size(234, 32)
        Me.BPlusTard.TabIndex = 2
        Me.BPlusTard.Text = "Plus tard"
        '
        'PBRemake
        '
        Me.PBRemake.Image = CType(resources.GetObject("PBRemake.Image"), System.Drawing.Image)
        Me.PBRemake.Location = New System.Drawing.Point(42, 74)
        Me.PBRemake.Name = "PBRemake"
        Me.PBRemake.Size = New System.Drawing.Size(150, 146)
        Me.PBRemake.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(8, 223)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(230, 20)
        Me.Label3.Text = "Verifier le WIFI et cliquer ci-dessus"
        '
        'PageRemonter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.PBRemake)
        Me.Controls.Add(Me.BPlusTard)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "PageRemonter"
        Me.Text = "Save"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents BPlusTard As System.Windows.Forms.Button
    Friend WithEvents PBRemake As System.Windows.Forms.PictureBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
