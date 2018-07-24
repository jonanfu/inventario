Imports MySql.Data.MySqlClient
Public Class FrmCompras
    Dim conex As New MySqlConnection
    Dim da As MySqlDataAdapter
    Dim dt As DataTable
    Dim dat As DataSet
    Dim sql As String
    Dim contenido As String
    Dim comando As MySqlCommand
    Public idProducto As String
    Public idCompra, cantidad As Integer
    Public costo As Single
    Private Sub btnModificar_Click(sender As Object, e As EventArgs) 

    End Sub

    Private Sub FrmCompras_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

            sql = "SELECT * FROM compras"

            da = New MySqlDataAdapter(sql, conex)
            dt = New DataTable

            da.Fill(dt)
            DataGridView1.DataSource = dt

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim sqleliminar As String
        Dim pregunta As Byte
        Try
            pregunta = MsgBox("Desea eliminar este registro", vbYesNo, "Eliminar")
            If pregunta = 6 Then
                sqleliminar = "delete from compras where id_compras='" & idCompra & "'"
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

    Private Sub btnCompra_Click(sender As Object, e As EventArgs) Handles btnCompra.Click
        FrmInsertarCompra.ShowDialog()
    End Sub

    Private Sub btnReporte_Click(sender As Object, e As EventArgs) Handles btnReporte.Click
        Dim sqlReporte As String
        ' sqlReporte = "select cedulas as 'Cedula', nombre as 'Nombre', correo as 'Correo', direccion as 'Direccion', ciudad as 'Ciudad' from clientes"
        sqlReporte = "select * from compras"
        Try
            dat = New DataSet
            da.SelectCommand = New MySqlCommand(sqlReporte, conex)
            da.Fill(dat)
            dat.WriteXml(CurDir() & "\ReporteCompras.xml", XmlWriteMode.WriteSchema)
            MsgBox("Reporte de compras creado")
            FrmReporteCompras.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        idCompra = DataGridView1.CurrentRow.Cells(0).Value
        idProducto = DataGridView1.CurrentRow.Cells(1).Value
        cantidad = DataGridView1.CurrentRow.Cells(2).Value
        costo = DataGridView1.CurrentRow.Cells(3).Value
    End Sub
End Class