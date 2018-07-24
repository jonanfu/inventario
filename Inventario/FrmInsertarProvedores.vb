Imports MySql.Data.MySqlClient
Public Class FrmInsertarProvedores
    Dim conex As New MySqlConnection
    Dim da As MySqlDataAdapter
    Dim dt As DataTable
    Dim dat As DataSet
    Dim sql As String
    Dim posicion As Integer
    Dim comando As MySqlCommand
    Private Sub btnIngresar_Click(sender As Object, e As EventArgs)
        Call añadir()

    End Sub

    Private Sub FrmInsertarProvedores_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            ' codigo para cargar los datos en el datagrid1 estudiantes
            sql = "select SELECT ruc_provedores as 'Ruc', nombres as 'Nombre del provedor', ciudad as 'Ciudad', telefono as 'Telefono', direccion as 'Direccion' FROM provedores from provedores"
            da = New MySqlDataAdapter(sql, conex)
            dt = New DataTable

            da.Fill(dt)
            DataGridView1.DataSource = dt

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub añadir()
        Try
            comando = New MySqlCommand("INSERT INTO ``(`ruc_provedores`, `nombres`, `ciudad`, `telefono`, `direccion`) VALUES (@ruc,@nombre,@ciudad,@telefo,@direccion)", conex)
            comando.Parameters.AddWithValue("@ruc", txtRuc.Text)
            comando.Parameters.AddWithValue("@nombre", txtNombre.Text)
            comando.Parameters.AddWithValue("@ciudad", txtCiudad)
            comando.Parameters.AddWithValue("@telefono", txtTelefono.Text)
            comando.Parameters.AddWithValue("@direccion", txtDireccion.Text)
            comando.ExecuteNonQuery()
            MsgBox("Datos Guardados")
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox("no se puede guardar porque ya existe un proveedor con ese ruc")
        End Try

        txtRuc.Text = ""
        txtNombre.Text = ""
        txtCiudad.Text = ""
        txtTelefono.Text = ""
        txtDireccion.Text = ""
        txtRuc.Focus()
        Call mostrardatos()
    End Sub


    Private Sub FrmInsertarProvedores_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        conex.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        FrmInsertarProvedores.ActiveForm.Close()
    End Sub

    Private Sub btnIngresar_Click_1(sender As Object, e As EventArgs) Handles btnIngresar.Click
        Call añadir()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        FrmInsertarProvedores.ActiveForm.Close()
    End Sub
End Class