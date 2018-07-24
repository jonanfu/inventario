Imports MySql.Data.MySqlClient
Public Class FrmModifcarProductos
    Dim conex As New MySqlConnection
    Dim da As MySqlDataAdapter
    Dim dt As DataTable
    Dim dat As DataSet
    Dim sql As String
    Dim posicion As Integer
    Dim comando As MySqlCommand
    Private Sub FrmModifcarProductos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            conex.ConnectionString = "server=localhost;user id=root; password=''; database=inventario"
            conex.Open()
            txtProducto.Text = FrmProductos.idProducto
            txtDescripcion.Text = FrmProductos.Descripcion
            txtMarca.Text = FrmProductos.Marca
            txtCosto.Text = FrmProductos.Costo
            txtRuc.Text = FrmProductos.ruc
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub btnIngresar_Click_1(sender As Object, e As EventArgs) Handles btnIngresar.Click
        Try
            Dim Sqlactualizar As String
            Dim pregunta As Byte
            pregunta = MsgBox("Desea actualizar este registro", vbYesNo, "Actualizar")
            If pregunta = 6 Then
                Sqlactualizar = "UPDATE productos SET Descripcion=@descripcion, Marca=@marca, Costo=@costo, RUC_Provedor=@ruc WHERE Id_producto=@idProducto"

                comando = New MySqlCommand(Sqlactualizar, conex)

                comando.Parameters.AddWithValue("@idProducto", txtRuc.Text)
                comando.Parameters.AddWithValue("@descripcion", txtDescripcion.Text)
                comando.Parameters.AddWithValue("@marca", txtMarca.Text)
                comando.Parameters.AddWithValue("@costo", txtCosto.Text)
                comando.Parameters.AddWithValue("@ruc", txtRuc.Text)

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
        FrmModifcarProductos.ActiveForm.Close()
    End Sub
End Class