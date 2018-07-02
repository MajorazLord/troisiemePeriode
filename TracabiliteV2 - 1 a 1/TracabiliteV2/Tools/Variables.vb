Module Variables
    Public Const connectionS3SQL As String = "SERVER=10.163.14.203;UID=sa;PWD=Passw0rd;DATABASE=SAISIE_CONT;Connection Timeout=3;"
    Public Const connectionM3 As String = "Password=manager;Persist Security Info=True;User ID=system;Data Source=T1BDD"
    Public Const connectionT3BPW As String = "SERVER=10.163.14.1;UID=sa;PWD=Passw0rd;DATABASE=BPW_Datamarts"


    Public myEtiqManager As EtiquetteManager
    Public myEtiq As Etiquette

    Public etiqDetail As Etiquette

    Public currentNumEtiq As String
End Module
