Imports MySql.Data.MySqlClient
Public Class FrmInsertarCompra
    Dim conex, conexReader As New MySqlConnection
    Dim da As MySqlDataAdapter
    Dim dt As DataTable
    Dim dat As DataSet
    Dim reader As MySqlDataReader
    Dim sql As String
    Dim comando As MySqlCommand
    Dim idProducto, contenido As String
    Dim cantidad, idCompras As Integer
    Dim costo, subtotal, iva, total As Double
    Dim fecha As Date
    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        FrmInsertarCompra.ActiveForm.Close()
    End Sub

    Private Sub FrmInsertarCompra_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        conex.Close()
    End Sub

    Private Sub FrmInsertarCompra_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            conex.ConnectionString = "server=localhost;user id=root; password=''; database=inventario"
            conex.Open()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Call mostrardatos()
    End Sub
    Private Sub mostrardatos()
        Try

            'contenido = "id_compras as 'Nro Compra', id_producto as 'Codigo producto', cantidad as 'Cantidad',costo as 'Costo', subtotal as 'SubTotal', iva as 'IVA', total as 'Total', fecha as 'Fecha'"
            sql = "SELECT * FROM compras"
            da = New MySqlDataAdapter(sql, conex)
            dt = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Public Function codigo(ByVal idProducto As Integer) As String
        Dim sqlCodigo As String
        Try
            sqlCodigo = "select id_producto from productos where id_producto='" & idProducto & "'"
            comando = New MySqlCommand(sqlCodigo, conex)
            reader = comando.ExecuteReader
            Return reader.GetString(0)
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub btnIngresar_Click(sender As Object, e As EventArgs) Handles btnIngresar.Click
        idProducto = txtCodigo.Text
        cantidad = Val(txtCantidad.Text)
        costo = Val(txtCosto.Text)
        If idProducto = codigo(idProducto) Then
            subtotal = cantidad * costo
            iva = subtotal * 0.12
            total = subtotal + iva
            fecha = Date.Now
            comando = New MySqlCommand("select MAX(id_compras) from compras", conex)
            reader = comando.ExecuteReader
            idCompras = reader.GetInt32(0) + 1
            Try
                comando = New MySqlCommand("INSERT INTO `compras`(`id_producto`, `cantidad`, `costo`, `subtotal`, `iva`, `total`, `id_compras`, `fecha`) VALUES 
                                            " & "(@idProducto,@cantidad,@costo,@subtotal,@iva,@total,@idCompras,@fecha)", conex)
                comando.Parameters.AddWithValue("@idProducto", idProducto)
                comando.Parameters.AddWithValue("@cantidad", cantidad)
                comando.Parameters.AddWithValue("@costo", costo)
                comando.Parameters.AddWithValue("@subtotal", subtotal)
                comando.Parameters.AddWithValue("@iva", iva)
                comando.Parameters.AddWithValue("@total", total)
                comando.Parameters.AddWithValue("@idCompras", idCompras)
                comando.Parameters.AddWithValue("@fecha", fecha)
                comando.ExecuteNonQuery()
                MsgBox("Datos de compras guardados")
            Catch ex As Exception
                MsgBox(ex.Message)

            End Try

            Try

                sql = "select cantidad from almacen where id_producto='" & idProducto & "'"
                comando = New MySqlCommand(sql, conex)
                reader = comando.ExecuteReader


                Dim sqlActualizar As String
                sqlActualizar = "UPDATE `almacen` SET cantidad=@cantidad WHERE id_producto=@idProducto"

                comando = New MySqlCommand(sqlActualizar, conex)

                comando.Parameters.AddWithValue("@idProducto", idProducto)
                comando.Parameters.AddWithValue("@cantidad", reader.GetInt32(0) + cantidad)

                comando.ExecuteNonQuery()
                MsgBox("Se actualizado el almacen")

            Catch ex As Exception


                sql = "select descripcion from productos where id_producto='" & idProducto & "'"
                comando = New MySqlCommand(sql, conex)
                reader = comando.ExecuteReader


                Dim sqlIngresar As String
                sqlIngresar = "INSERT INTO `almacen`(`id_producto`, `cantidad`, `descripcion`) VALUES (@idProducto,@cantidad,@descripcion)"

                comando = New MySqlCommand(sqlIngresar, conex)

                comando.Parameters.AddWithValue("@idProducto", idProducto)
                comando.Parameters.AddWithValue("@cantidad", cantidad)
                comando.Parameters.AddWithValue("@descripcion", reader.GetString(0))

                comando.ExecuteNonQuery()
                MsgBox("Se ha agregado un nuevo el producto en el almacen")
            End Try
        End If
        txtCodigo.Text = ""
        txtCantidad.Text = ""
        txtCosto.Text = ""
        txtCodigo.Focus()
        Call mostrardatos()
    End Sub


End Class