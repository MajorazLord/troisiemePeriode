Module Variables
    Public Const PdCFourG As String = "AGCIEFFE"

    Public Const HEIGHT_SCREEN_VGA As Integer = 640
    Public Const WIDTH_SCREEN_VGA As Integer = 480

    Public currentScreenHeight As Integer
    Public currentScreenWidth As Integer

    Public myUser As User

    Public cheminBase As String = "\My Documents\StopInvGueret\"
    Public cheminDossier As String = cheminBase & Format(Now, "yyyyMMdd")
    Public cheminDossierFichier As String = cheminDossier & "\SaveInventaire.xls"
    Public cheminDossierFichierDepart As String = cheminDossier & "\SaveDepart.xls"

    Public listDesSorties As New List(Of Sortie)
    Public listDesDepartExp As New List(Of DépartExp)

    Public idUniqueJour As Integer = 0
    Public dateSaisie As Date

    Public poidsUnitaireInstantT As String = ""
    Public poidsCamionActuel As Integer = 0

#Region "Chaine de connexion aux différentes bases de données"
    Public Const connS3SQL As String = "SERVER=10.163.14.203;UID=sa;PWD=Passw0rd;DATABASE=SAISIE_CONT;Connection Timeout=2;"
    Public Const connGOPAL As String = "SERVER=10.163.14.203;UID=sa;PWD=Passw0rd;DATABASE=GOPAL;Connection Timeout=2;"
    Public Const connT3BPW As String = "SERVER=10.163.14.1;UID=sa;PWD=Passw0rd;DATABASE=BPW_Datamarts;Connection Timeout=2;"
    Public Const connS3BPWFC As String = "SERVER=192.9.199.128;UID=sa;PWD=Passw0rd;DATABASE=BPW_STAGING;Connection Timeout=2;"
    Public Const connS3SQLMOVEOP As String = "SERVER=10.163.14.203;UID=sa;PWD=Passw0rd;DATABASE=MOVEOP;Connection Timeout=2;"
#End Region


End Module
