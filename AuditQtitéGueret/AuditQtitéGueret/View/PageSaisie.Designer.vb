<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class PageSaisie
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
        Me.Btn_finSaisie = New System.Windows.Forms.Button
        Me.Btn_suiv = New System.Windows.Forms.Button
        Me.lb_codeCont = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lb_desc_qtite_verif = New System.Windows.Forms.Label
        Me.TbCodeCont = New System.Windows.Forms.TextBox
        Me.TbQtiteVerif = New System.Windows.Forms.TextBox
        Me.lb_qtiteBdd = New System.Windows.Forms.Label
        Me.BtnDelCode = New System.Windows.Forms.Button
        Me.BtnDelQtiteVerif = New System.Windows.Forms.Button
        Me.PBRecap = New System.Windows.Forms.PictureBox
        Me.SuspendLayout()
        '
        'Btn_finSaisie
        '
        Me.Btn_finSaisie.BackColor = System.Drawing.Color.Red
        Me.Btn_finSaisie.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_finSaisie.Location = New System.Drawing.Point(100, 253)
        Me.Btn_finSaisie.Name = "Btn_finSaisie"
        Me.Btn_finSaisie.Size = New System.Drawing.Size(135, 38)
        Me.Btn_finSaisie.TabIndex = 0
        Me.Btn_finSaisie.Text = "Fin de l'audit"
        '
        'Btn_suiv
        '
        Me.Btn_suiv.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Btn_suiv.Font = New System.Drawing.Font("Tahoma", 13.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_suiv.Location = New System.Drawing.Point(35, 172)
        Me.Btn_suiv.Name = "Btn_suiv"
        Me.Btn_suiv.Size = New System.Drawing.Size(174, 46)
        Me.Btn_suiv.TabIndex = 1
        Me.Btn_suiv.Text = "Conteneur suivant"
        '
        'lb_codeCont
        '
        Me.lb_codeCont.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lb_codeCont.Location = New System.Drawing.Point(3, 6)
        Me.lb_codeCont.Name = "lb_codeCont"
        Me.lb_codeCont.Size = New System.Drawing.Size(172, 20)
        Me.lb_codeCont.Text = "Scan Code Conteneur"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(2, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(134, 20)
        Me.Label2.Text = "Quantité Saisie : "
        '
        'lb_desc_qtite_verif
        '
        Me.lb_desc_qtite_verif.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lb_desc_qtite_verif.Location = New System.Drawing.Point(3, 98)
        Me.lb_desc_qtite_verif.Name = "lb_desc_qtite_verif"
        Me.lb_desc_qtite_verif.Size = New System.Drawing.Size(151, 20)
        Me.lb_desc_qtite_verif.Text = "Quantité Vérifiée :"
        '
        'TbCodeCont
        '
        Me.TbCodeCont.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular)
        Me.TbCodeCont.Location = New System.Drawing.Point(3, 29)
        Me.TbCodeCont.Name = "TbCodeCont"
        Me.TbCodeCont.Size = New System.Drawing.Size(206, 24)
        Me.TbCodeCont.TabIndex = 5
        '
        'TbQtiteVerif
        '
        Me.TbQtiteVerif.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular)
        Me.TbQtiteVerif.Location = New System.Drawing.Point(16, 121)
        Me.TbQtiteVerif.Name = "TbQtiteVerif"
        Me.TbQtiteVerif.Size = New System.Drawing.Size(159, 24)
        Me.TbQtiteVerif.TabIndex = 6
        '
        'lb_qtiteBdd
        '
        Me.lb_qtiteBdd.Font = New System.Drawing.Font("Tahoma", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.lb_qtiteBdd.Location = New System.Drawing.Point(135, 62)
        Me.lb_qtiteBdd.Name = "lb_qtiteBdd"
        Me.lb_qtiteBdd.Size = New System.Drawing.Size(100, 20)
        '
        'BtnDelCode
        '
        Me.BtnDelCode.Location = New System.Drawing.Point(211, 29)
        Me.BtnDelCode.Name = "BtnDelCode"
        Me.BtnDelCode.Size = New System.Drawing.Size(24, 24)
        Me.BtnDelCode.TabIndex = 10
        Me.BtnDelCode.Text = "X"
        '
        'BtnDelQtiteVerif
        '
        Me.BtnDelQtiteVerif.Location = New System.Drawing.Point(178, 121)
        Me.BtnDelQtiteVerif.Name = "BtnDelQtiteVerif"
        Me.BtnDelQtiteVerif.Size = New System.Drawing.Size(24, 24)
        Me.BtnDelQtiteVerif.TabIndex = 11
        Me.BtnDelQtiteVerif.Text = "X"
        '
        'PBRecap
        '
        Me.PBRecap.Location = New System.Drawing.Point(8, 235)
        Me.PBRecap.Name = "PBRecap"
        Me.PBRecap.Size = New System.Drawing.Size(59, 56)
        Me.PBRecap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        '
        'PageSaisie
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 294)
        Me.Controls.Add(Me.PBRecap)
        Me.Controls.Add(Me.BtnDelQtiteVerif)
        Me.Controls.Add(Me.BtnDelCode)
        Me.Controls.Add(Me.lb_qtiteBdd)
        Me.Controls.Add(Me.TbQtiteVerif)
        Me.Controls.Add(Me.TbCodeCont)
        Me.Controls.Add(Me.lb_desc_qtite_verif)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lb_codeCont)
        Me.Controls.Add(Me.Btn_suiv)
        Me.Controls.Add(Me.Btn_finSaisie)
        Me.Name = "PageSaisie"
        Me.Text = "Saisie Audit"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Btn_finSaisie As System.Windows.Forms.Button
    Friend WithEvents Btn_suiv As System.Windows.Forms.Button
    Friend WithEvents lb_codeCont As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lb_desc_qtite_verif As System.Windows.Forms.Label
    Friend WithEvents TbCodeCont As System.Windows.Forms.TextBox
    Friend WithEvents TbQtiteVerif As System.Windows.Forms.TextBox
    Friend WithEvents lb_qtiteBdd As System.Windows.Forms.Label
    Friend WithEvents BtnDelCode As System.Windows.Forms.Button
    Friend WithEvents BtnDelQtiteVerif As System.Windows.Forms.Button
    Friend WithEvents PBRecap As System.Windows.Forms.PictureBox

End Class
