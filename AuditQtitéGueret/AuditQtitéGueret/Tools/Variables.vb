Imports System.Data

Module Variables

    Public resReq As String

    Public DTAudit As DataTable


    Public Const REPONSE_OK As Integer = 6
    Public Const HEIGHT_SCREEN_VGA As Integer = 640
    Public Const WIDTH_SCREEN_VGA As Integer = 480

    Public currentScreenHeight As Integer
    Public currentScreenWidth As Integer

    Public cheminBase As String = "\My Documents\AuditQtiteGueret\"
    Public cheminDossier As String = cheminBase & Format(Now, "yyyyMMdd")
    Public cheminDossierFichier As String = cheminBase & Format(Now, "yyyyMMdd") & "\SaveSaisie.xls"

#Region "Chaine de connexion aux différentes bases de données"
    Public Const connS3SQL As String = "SERVER=10.163.14.203;UID=sa;PWD=Passw0rd;DATABASE=SAISIE_CONT;Connection Timeout=2;"
#End Region
End Module
