Imports MySql.Data.MySqlClient
Public Class FrmModificarProvedor
    Dim conex As New MySqlConnection
    Dim da As MySqlDataAdapter
    Dim dt As DataTable
    Dim dat As DataSet
    Dim sql As String
    Dim posicion As Integer
    Dim comando As MySqlCommand
    Private Sub FrmModificarProvedor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            conex.ConnectionString = "server=localhost;user id=root; password=''; database=inventario"
            conex.Open()
            txtRuc.Text = FrmProvedores.ruc
            txtNombre.Text = FrmProvedores.nombre
            txtTelefono.Text = FrmProvedores.telefono
            txtDireccion.Text = FrmProvedores.direccion
            txtCiudad.Text = FrmProvedores.ciudad

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub FrmModificarProvedor_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        FrmProvedores.mostrardatos()
        conex.Close()
    End Sub

    Private Sub btnModificar_Click_1(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try
            Dim Sqlactualizar As String
            Dim pregunta As Byte
            pregunta = MsgBox("Desea actualizar este registro", vbYesNo, "Actualizar")
            If pregunta = 6 Then
                Sqlactualizar = "UPDATE provedores SET nombres=@nombre, ciudad=@ciudad, telefono=@telefono, direccion=@direccion WHERE ruc_provedores=@ruc"

                comando = New MySqlCommand(Sqlactualizar, conex)

                comando.Parameters.AddWithValue("@ruc", txtRuc.Text)
                comando.Parameters.AddWithValue("@nombre", txtNombre.Text)
                comando.Parameters.AddWithValue("@ciudad", txtCiudad.Text)
                comando.Parameters.AddWithValue("@telefono", txtTelefono.Text)
                comando.Parameters.AddWithValue("@direccion", txtDireccion.Text)

                comando.ExecuteNonQuery()
                MsgBox("Registro Actualizado")
            Else
                MsgBox("NO se actualizo")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnRegresar_Click_1(sender As Object, e As EventArgs) Handles btnRegresar.Click
        FrmModificarClientes.ActiveForm.Close()
    End Sub
End Class