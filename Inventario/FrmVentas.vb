Imports MySql.Data.MySqlClient
Public Class FrmVentas
    Dim conex, conexReader As New MySqlConnection
    Dim da As MySqlDataAdapter
    Dim dt As DataTable
    Dim dat As DataSet
    Dim reader As MySqlDataReader
    Dim sql As String
    Dim contenido As String
    Dim comando As MySqlCommand
    Dim producto, cedula, nombre, correo, direccion, ciudad As String
    Public numeroProductos, cantidad, numeroFactura As Integer

    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click

    End Sub

    Dim fecha As Date
    Dim subtotal, iva, total As Single

    Private Sub btnReporte_Click(sender As Object, e As EventArgs) Handles btnReporte.Click

        conexReader.ConnectionString = "server=localhost;user id=root; password=''; database=inventario"
        sql = "select max(nro_factura) from factura_detalle"
        comando = New MySqlCommand(sql, conexReader)
        conexReader.Open()
        reader = comando.ExecuteReader
        reader.Read()
        numeroFactura = 1
        conexReader.Close()
        For i = 0 To numeroProductos - 1
            Try
                comando = New MySqlCommand("INSERT INTO `factura_detalle`(`nro_factura`, `id_producto`, `precio`, `cantidad`, `subtotal`) VALUES (@factura,@idProducto,@precio,@cantidad,@subtotal)", conex)
                comando.Parameters.AddWithValue("@factura", numeroFactura)
                comando.Parameters.AddWithValue("@idProducto", DataGridView1.Rows(i).Cells(0).Value)
                comando.Parameters.AddWithValue("@precio", DataGridView1.Rows(i).Cells(1).Value)
                comando.Parameters.AddWithValue("@cantidad", DataGridView1.Rows(i).Cells(2).Value)
                comando.Parameters.AddWithValue("@subtotal", DataGridView1.Rows(i).Cells(3).Value)
                comando.ExecuteNonQuery()
            Catch ex As Exception
            End Try

        Next
        conexReader.ConnectionString = "server=localhost;user id=root; password=''; database=inventario"
        sql = "select sum(subtotal) from factura_detalle where ='" & numeroFactura & "'"
        comando = New MySqlCommand(sql, conexReader)
        conexReader.Open()
        reader = comando.ExecuteReader
        subtotal = reader.GetInt32(0)
        iva = subtotal * 0.12
        total = subtotal + iva
        fecha = Date.Now
        Try
            comando = New MySqlCommand("INSERT INTO `factura_cabezera`(`nro_factura`, `cedula`, `nombre`, `direccion`, `telefono`, `subtotal`, `iva`, `total`, `fecha`)" &
                   "VALUES (@factura,@cedula,@nombre,@direccion,@telefono,@subtotal,@iva,@total,@fecha)", conex)
            comando.Parameters.AddWithValue("@factura", numeroFactura)
            comando.Parameters.AddWithValue("@cedula", cedula)
            comando.Parameters.AddWithValue("@nombre", nombre)
            comando.Parameters.AddWithValue("@direccion", direccion)
            comando.Parameters.AddWithValue("@telefono", "0982642857")
            comando.ExecuteNonQuery()

            Dim sqlReporte As String

            sqlReporte = "select * from factura_cabezera where nro_factura='" & numeroFactura & "'"
            Try
                dat = New DataSet
                da.SelectCommand = New MySqlCommand(sqlReporte, conex)
                da.Fill(dat)
                dat.WriteXml(CurDir() & "\FacturaCabezera.xml", XmlWriteMode.WriteSchema)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            sqlReporte = "select * from factura_detalle where nro_factura='" & numeroFactura & "'"
            Try
                dat = New DataSet
                da.SelectCommand = New MySqlCommand(sqlReporte, conex)
                da.Fill(dat)
                dat.WriteXml(CurDir() & "\FacturaDetalle.xml", XmlWriteMode.WriteSchema)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        Catch ex As Exception
        End Try


    End Sub



    Private Sub btnAgrega_Click(sender As Object, e As EventArgs) Handles btnAgrega.Click
        FrmInsertarVenta.ShowDialog()
    End Sub


    Private Sub FrmVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            conex.ConnectionString = "server=localhost;user id=root; password=''; database=inventario"
            conex.Open()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            conexReader.ConnectionString = "server=localhost;user id=root; password=''; database=inventario"
            sql = "select * from clientes where cedula='" & txtBuscar.Text & "'"
            comando = New MySqlCommand(sql, conexReader)

            conexReader.Open()
            reader = comando.ExecuteReader
            reader.Read()
            cedula = reader.GetString(0)
            nombre = reader.GetString(1)
            correo = reader.GetString(2)
            direccion = reader.GetString(3)
            ciudad = reader.GetString(4)
            txtNombre.Text = nombre

        Catch ex As Exception
            MsgBox("No existe el cliente en la base de datos")
        End Try
        conexReader.Close()
    End Sub


End Class