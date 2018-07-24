Imports MySql.Data.MySqlClient
Public Class FrmProvedores
    Dim conex As New MySqlConnection
    Dim da As MySqlDataAdapter
    Dim dt As DataTable
    Dim dat As DataSet
    Dim sql As String
    Dim posicion As Integer
    Dim comando As MySqlCommand
    Public ruc, nombre, telefono, direccion, ciudad As String

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs)
        Dim sqleliminar As String
        Dim pregunta As Byte
        Try
            pregunta = MsgBox("Desea eliminar el este registro", vbYesNo, "Eliminar")
            If pregunta = 6 Then
                sqleliminar = "delete from provedores where ruc_provedores='" & ruc & "'"
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

    Private Sub btnModificar_Click(sender As Object, e As EventArgs)
        FrmModificarProvedor.ShowDialog()
    End Sub

    Private Sub bntBuscar_Click(sender As Object, e As EventArgs)
        Dim sqlbuscar As String
        sqlbuscar = ""
        Try
            If cmbBusquedad.Text = "Ruc" Then
                'sqlbuscar = "select * from estudiantes where cedula='" & TxtCriterioBusqueda.Text & "'"
                sqlbuscar = "select * from provedores where ruc_provedores='" + txtBusquedad.Text + "'"
            ElseIf cmbBusquedad.Text = "Nombre" Then
                sqlbuscar = "select * from provedores where nombres like '%" + txtBusquedad.Text + "%';"
            ElseIf cmbBusquedad.Text = "Telefono" Then
                sqlbuscar = "select * from provedores where telefono='" + txtBusquedad.Text + "'"
            ElseIf cmbBusquedad.Text = "Ciudad" Then
                sqlbuscar = "select * from provedores where ciudad like '%" + txtBusquedad.Text + "%';"
            ElseIf cmbBusquedad.Text = "Direccion" Then
                sqlbuscar = "select * from provedores where direccion like '%" + txtBusquedad.Text + "%';"
            End If
            da = New MySqlDataAdapter(sqlbuscar, conex)
            dt = New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        FrmPresentacion.ActiveForm.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim sqlReporte As String
        ' sqlReporte = "select cedulas as 'Cedula', nombre as 'Nombre', correo as 'Correo', direccion as 'Direccion', ciudad as 'Ciudad' from clientes"
        sqlReporte = "select * from provedores"
        Try
            dat = New DataSet
            da.SelectCommand = New MySqlCommand(sqlReporte, conex)
            da.Fill(dat)
            dat.WriteXml(CurDir() & "\ReporteProvedores.xml", XmlWriteMode.WriteSchema)
            MsgBox("Reporte de provedores creado")
            FrmReporteProvedores.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub FrmProvedores_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

            sql = "SELECT * FROM provedores"
            da = New MySqlDataAdapter(sql, conex)
            dt = New DataTable


            da.Fill(dt)
            DataGridView1.DataSource = dt

            


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmProvedores_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        conex.Close()

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        ruc = DataGridView1.CurrentRow.Cells(0).Value
        nombre = DataGridView1.CurrentRow.Cells(1).Value
        ciudad = DataGridView1.CurrentRow.Cells(2).Value
        telefono = DataGridView1.CurrentRow.Cells(3).Value
        direccion = DataGridView1.CurrentRow.Cells(4).Value

    End Sub


End Class