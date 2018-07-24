Imports MySql.Data.MySqlClient
Public Class FrmInsertarVenta
    Dim conex, conexReader As New MySqlConnection
    Dim da As MySqlDataAdapter
    Dim dt As DataTable
    Dim dat As DataSet
    Dim reader As MySqlDataReader
    Dim sql As String
    Dim contenido As String
    Dim comando As MySqlCommand
    Dim cantidad As Integer
    Dim precio, subtotal As Single
    Private Sub btnIngresar_Click(sender As Object, e As EventArgs) Handles btnIngresar.Click
        Try
            cantidad = Val(txtCantidad.Text)
            precio = Val(txtPrecio.Text)
            conexReader.ConnectionString = "server=localhost;user id=root; password=''; database=inventario"
            sql = "select cantidad from almacen where id_producto='" & txtCodigo.Text & "'"
            comando = New MySqlCommand(sql, conexReader)


            conexReader.Open()

            reader = comando.ExecuteReader
            If cantidad < reader.GetInt32(0) Then
                subtotal = cantidad * precio
                FrmVentas.DataGridView1.Rows.Add(txtCodigo.Text, cantidad, precio, subtotal)
                MsgBox("Se ha agregado un producto a la venta")
                FrmInsertarVenta.ActiveForm.Close()
                FrmVentas.numeroProductos = FrmVentas.numeroProductos + 1
            Else
                MsgBox("No puedes realizar la venta porque solo tienes " & reader.GetInt32(0) & "productos en el inventario")
            End If

            ' MsgBox(co)
        Catch ex As Exception
            MsgBox("No existe el producto en la base de datos")
        End Try
        txtCodigo.Text = ""
        txtCantidad.Text = ""
        txtPrecio.Text = ""
    End Sub


End Class