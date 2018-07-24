Imports MySql.Data.MySqlClient
Public Class FrmLogin
    Dim conex As New MySqlConnection
    Dim datoAdaptador As MySqlDataAdapter
    Dim dataTable As DataTable
    Dim sql As String
    Dim comando As MySqlCommand
    Dim intentos As Integer = 3
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            conex.ConnectionString = "server=localhost;user id=root; password=''; database=inventario"
            conex.Open()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btnAcceder_Click(sender As Object, e As EventArgs) Handles btnAcceder.Click
        Dim user, contraseña As String
        user = txtUsuario.Text
        contraseña = txtContraseña.Text

        Try
            sql = "select contraseña, vigente from usuario where login='" & user & "'"

            datoAdaptador = New MySqlDataAdapter(sql, conex)
            dataTable = New DataTable
            datoAdaptador.Fill(dataTable)
            If dataTable(0).ItemArray.GetValue(1) = True Then

                If dataTable(0).ItemArray.GetValue(0) = contraseña Then
                    FrmLogin.ActiveForm.Visible = False
                    FormMenu.Show()

                Else
                    MsgBox("Contraseña inconrrecta vuelva a ingresar")
                    intentos = intentos - 1
                End If
                If intentos = 0 Then
                    sql = "update usuario set vigente=0 where login='" & user & "'"
                    comando = New MySqlCommand(sql, conex)
                    comando.ExecuteNonQuery()
                End If
            Else
                MsgBox("Lo lamento su cuenta fue bloqueada")
            End If
        Catch ex As Exception
            MsgBox("Nombre de usuario o contraseña incorrectos")
        End Try
    End Sub
End Class
