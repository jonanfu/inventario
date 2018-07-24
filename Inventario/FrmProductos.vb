Imports MySql.Data.MySqlClient
Public Class FrmProductos
    Dim conex As New MySqlConnection
    Dim da As MySqlDataAdapter
    Dim dt As DataTable
    Dim dat As DataSet
    Dim sql As String
    Dim posicion As Integer
    Dim comando As MySqlCommand
    Public idProducto, Descripcion, Marca, Costo, ruc As String

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellMouseEventArgs) 
        idProducto = DataGridView1.CurrentRow.Cells(0).Value
        Descripcion = DataGridView1.CurrentRow.Cells(1).Value
        Marca = DataGridView1.CurrentRow.Cells(2).Value
        Costo = DataGridView1.CurrentRow.Cells(3).Value
        ruc = DataGridView1.CurrentRow.Cells(4).Value

    End Sub
    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) 
        Dim sqleliminar As String
        Dim pregunta As Byte
        Try
            pregunta = MsgBox("Desea eliminar el este registro", vbYesNo, "Eliminar")
            If pregunta = 6 Then
                sqleliminar = "delete from productos where Id_producto='" & idProducto & "'"
                comando = New MySqlCommand(sqleliminar, conex)
                comando.ExecuteNonQuery()
                MsgBox("Registro Eliminado")
            Else
                MsgBox("no se elimino ningun Registro")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Call mostrardatos()
    End Sub

    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) 
        FrmProductos.ActiveForm.Close()
    End Sub

    Private Sub btnInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        FrmInsertarProductos.ShowDialog()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        FrmModifcarProductos.ShowDialog()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim sqlReporte As String
        ' sqlReporte = "select cedulas as 'Cedula', nombre as 'Nombre', correo as 'Correo', direccion as 'Direccion', ciudad as 'Ciudad' from clientes"
        sqlReporte = "select * from productos"
        Try
            dat = New DataSet
            da.SelectCommand = New MySqlCommand(sqlReporte, conex)
            da.Fill(dat)
            dat.WriteXml(CurDir() & "\ReporteProductos.xml", XmlWriteMode.WriteSchema)
            MsgBox("Reporte de productos creado")
            FrmReporteProductos.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub bntBuscar_Click_1(sender As Object, e As EventArgs) Handles bntBuscar.Click
        Dim sqlbuscar As String
        sqlbuscar = ""
        Try
            If cmbBusquedad.Text = "Codigo producto" Then
                'sqlbuscar = "select * from estudiantes where cedula='" & TxtCriterioBusqueda.Text & "'"
                sqlbuscar = "select Id_producto as 'Codigo producto', descripcion as 'Descripcion', marca as 'Marca', costo as 'Costo', ruc_provedores as 'Ruc Provedor' from productos where Id_prducto like '%" + txtBusquedad.Text + "%';"
            ElseIf cmbBusquedad.Text = "Descripcion" Then
                sqlbuscar = "select Id_producto as 'Codigo producto', descripcion as 'Descripcion', marca as 'Marca', costo as 'Costo', ruc_provedores as 'Ruc Provedor' from productos where descripcion like '%" + txtBusquedad.Text + "%';"
            ElseIf cmbBusquedad.Text = "Marca" Then
                sqlbuscar = "select Id_producto as 'Codigo producto', descripcion as 'Descripcion', marca as 'Marca', costo as 'Costo', ruc_provedores as 'Ruc Provedor' from productos where marca like '%" + txtBusquedad.Text + "%';"
            ElseIf cmbBusquedad.Text = "Costo" Then
                sqlbuscar = "select Id_producto as 'Codigo producto', descripcion as 'Descripcion', marca as 'Marca', costo as 'Costo', ruc_provedores as 'Ruc Provedor' from productos where costo like '%" + txtBusquedad.Text + "%';"
            ElseIf cmbBusquedad.Text = "Ruc" Then
                sqlbuscar = "select Id_producto as 'Codigo producto', descripcion as 'Descripcion', marca as 'Marca', costo as 'Costo', ruc_provedores as 'Ruc Provedor' from productos where ruc_provedores like '%" + txtBusquedad.Text + "%';"
            End If
            da = New MySqlDataAdapter(sqlbuscar, conex)
            dt = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt
        Catch ex As Exception

        End Try
    End Sub


    Private Sub FrmProductos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            conex.ConnectionString = "server=localhost;user id=root; password=''; database=inventario"
            conex.Open()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Call mostrardatos()
    End Sub
    Public Sub mostrardatos()
        Try

            'sql = "SELECT 'Id_producto.productos', 'Descripcion.productos', 'Marca.productos', 'Costo.productos', 'nombres.provedores', 'RUC_Provedores.provedores' from provedores INNER JOIN productos on 'RUC_Provedores.productos' = 'RUC_Provedores.provedores'"
            sql = "SELECT Id_producto as 'Codigo producto', descripcion as 'Descripcion', marca as 'Marca', costo as 'Costo', ruc_provedores as 'Ruc Provedor' FROM productos"
            da = New MySqlDataAdapter(sql, conex)
            dt = New DataTable

            da.Fill(dt)
            DataGridView1.DataSource = dt

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmProductos_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        conex.Close()
    End Sub
End Class