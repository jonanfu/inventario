Imports MySql.Data.MySqlClient
Public Class FrmInsertarProductos
    Dim conex As New MySqlConnection
    Dim da As MySqlDataAdapter
    Dim dt As DataTable
    Dim dat As DataSet
    Dim sql As String
    Dim posicion As Integer
    Dim comando As MySqlCommand

    Private Sub FrmInsertarProductos_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            conex.ConnectionString = "server=localhost;user id=root; password=''; database=inventario"
            conex.Open()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Call mostrardatos()
    End Sub

    Private Sub añadir()
        Try
            comando = New MySqlCommand("INSERT INTO `productos`(`Id_producto`, `Descripcion`, `Marca`, `Costo`, `RUC_Provedores`) VALUES (@idCodigo,@descripcion,@marca,@costo,@ruc)", conex)
            comando.Parameters.AddWithValue("@ruc", txtRuc.Text)
            comando.Parameters.AddWithValue("@descripcion", txtDescripcion.Text)
            comando.Parameters.AddWithValue("@marca", txtMarca.Text)
            comando.Parameters.AddWithValue("@costo", txtCosto.Text)
            comando.Parameters.AddWithValue("@ruc", txtRuc.Text)
            comando.ExecuteNonQuery()
            MsgBox("Datos Guardados")
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox("no se puede guardar porque ya existe un producto con este codigo")
        End Try

        txtProducto.Text = ""
        txtDescripcion.Text = ""
        txtMarca.Text = ""
        txtCosto.Text = ""
        txtRuc.Text = ""
        txtProducto.Focus()
        Call mostrardatos()
    End Sub

    Private Sub mostrardatos()
        Try
            ' codigo para cargar los datos en el datagrid1 estudiantes
            sql = "SELECT Id_producto as 'Codigo producto', descripcion as 'Descripcion', marca as 'Marca', costo as 'Costo', ruc_provedores as 'Ruc Provedor' FROM productos"
            da = New MySqlDataAdapter(sql, conex)
            dt = New DataTable

            da.Fill(dt)
            DataGridView1.DataSource = dt

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnIngresar_Click_1(sender As Object, e As EventArgs) Handles btnIngresar.Click
        Call añadir()
    End Sub

    Private Sub btnProcedores_Click_1(sender As Object, e As EventArgs) Handles btnProcedores.Click
        FrmInsertarProductos.ActiveForm.Close()
    End Sub
End Class