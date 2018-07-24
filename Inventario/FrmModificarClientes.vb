Imports MySql.Data.MySqlClient
Public Class FrmModificarClientes
    Dim conex As New MySqlConnection
    Dim da As MySqlDataAdapter
    Dim dt As DataTable
    Dim dat As DataSet
    Dim sql As String
    Dim posicion As Integer
    Dim comando As MySqlCommand
    Private Sub FrmModificarClientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            conex.ConnectionString = "server=localhost;user id=root; password=''; database=inventario"
            conex.Open()
            txtCedula.Text = FrmClientes.cedula
            txtNombre.Text = FrmClientes.nombre
            txtCorreo.Text = FrmClientes.apellido
            txtDireccion.Text = FrmClientes.direccion
            txtCiudad.Text = FrmClientes.ciudad

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub FrmModificarClientes_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        conex.Close()
        FrmClientes.mostrardatos()

    End Sub

    Private Sub btnModificar_Click_1(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try
            Dim Sqlactualizar As String
            Dim pregunta As Byte
            pregunta = MsgBox("Desea actualizar este registro", vbYesNo, "Actualizar")
            If pregunta = 6 Then
                Sqlactualizar = "UPDATE clientes SET nombre=@nombre, correo=@correo, direccion=@direccion, ciudad=@ciudad WHERE cedula=@cedula"

                comando = New MySqlCommand(Sqlactualizar, conex)

                comando.Parameters.AddWithValue("@cedula", txtCedula.Text)
                comando.Parameters.AddWithValue("@nombre", txtNombre.Text)
                comando.Parameters.AddWithValue("@correo", txtCorreo.Text)
                comando.Parameters.AddWithValue("@direccion", txtDireccion.Text)
                comando.Parameters.AddWithValue("@ciudad", txtCiudad.Text)

                comando.ExecuteNonQuery()
                MsgBox("Registro Actualizado")
            Else
                MsgBox("NO se actualizo el registro")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FrmModificarClientes.ActiveForm.Close()
    End Sub
End Class