
Imports MySql.Data.MySqlClient
Public Class FrmClientes
    Dim conex As New MySqlConnection
    Dim da As MySqlDataAdapter
    Dim dt As DataTable
    Dim dat As DataSet
    Dim sql As String
    Dim posicion As Integer
    Dim comando As MySqlCommand
    Public cedula, nombre, apellido, direccion, ciudad As String

    Private Sub FrmMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            ' codigo para cargar los datos en el datagrid1 estudiantes
            sql = "SELECT cedula as 'Cedula', nombre as 'Nombre', correo as 'Correo', direccion as 'Direccion', ciudad as 'Ciudad' FROM clientes"
            da = New MySqlDataAdapter(sql, conex)
            dt = New DataTable

            da.Fill(dt)
            DataGridView1.DataSource = dt

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnRegresar_Click(sender As Object, e As EventArgs)
        FrmClientes.ActiveForm.Close()
    End Sub

    Private Sub bntBuscar_Click_1(sender As Object, e As EventArgs) Handles bntBuscar.Click
        Dim sqlbuscar As String
        sqlbuscar = ""
        Try
            If cmbBusquedad.Text = "Cedula" Then

                sqlbuscar = "select * from clientes where cedula='" + txtBusquedad.Text + "'"
            ElseIf cmbBusquedad.Text = "Nombre" Then
                sqlbuscar = "select * from clientes where nombre like '%" + txtBusquedad.Text + "%';"
            ElseIf cmbBusquedad.Text = "Correo" Then
                sqlbuscar = "select * from clientes where correo like '%" + txtBusquedad.Text + "%';"
            ElseIf cmbBusquedad.Text = "Direccion" Then
                sqlbuscar = "select * from clientes where direccion like '%" + txtBusquedad.Text + "%';"
            ElseIf cmbBusquedad.Text = "Ciudad" Then
                sqlbuscar = "select * from clientes where ciudad like '%" + txtBusquedad.Text + "%';"
            End If
            da = New MySqlDataAdapter(sqlbuscar, conex)
            dt = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnInsertar_Click_1(sender As Object, e As EventArgs) Handles btnInsertar.Click
        FrmInsertarClientes.ShowDialog()
    End Sub

    Private Sub btnEliminar_Click_1(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim sqleliminar As String
        Dim pregunta As Byte
        Try
            pregunta = MsgBox("Desea eliminar el este registro", vbYesNo, "Eliminar")
            If pregunta = 6 Then
                sqleliminar = "delete from clientes where cedula='" & cedula & "'"
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

    Private Sub btnReporte_Click_1(sender As Object, e As EventArgs) Handles btnReporte.Click
        Dim sqlReporte As String
        ' sqlReporte = "select cedulas as 'Cedula', nombre as 'Nombre', correo as 'Correo', direccion as 'Direccion', ciudad as 'Ciudad' from clientes"
        sqlReporte = "select * from clientes"
        Try
            dat = New DataSet
            da.SelectCommand = New MySqlCommand(sqlReporte, conex)
            da.Fill(dat)
            dat.WriteXml(CurDir() & "\ReporteClientes.xml", XmlWriteMode.WriteSchema)
            MsgBox("Reporte de clientes creado")
            FrmReporteClientes.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        FrmModificarClientes.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        FrmModificarClientes.ShowDialog()

    End Sub



    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellMouseEventArgs)
        cedula = DataGridView1.CurrentRow.Cells(0).Value
        nombre = DataGridView1.CurrentRow.Cells(1).Value
        apellido = DataGridView1.CurrentRow.Cells(2).Value
        direccion = DataGridView1.CurrentRow.Cells(3).Value
        ciudad = DataGridView1.CurrentRow.Cells(4).Value

    End Sub

    Private Sub FrmClientes_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        conex.Close()

    End Sub
End Class
