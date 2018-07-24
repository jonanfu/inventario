Imports MySql.Data.MySqlClient

Public Class FrmInsertarClientes
    Dim conex As New MySqlConnection
    Dim da As MySqlDataAdapter
    Dim dt As DataTable
    Dim dat As DataSet
    Dim sql As String
    Dim comando As MySqlCommand
    Private Sub FrmInsertarClientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            sql = "select cedula as 'Cedula', nombre as 'Nombre', apellido as 'Apellido', direccion as 'Direccion', ciudad as 'Ciudad' from clientes"
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
            comando = New MySqlCommand("INSERT INTO `clientes`(`cedula`, `nombre`, `correo`, `direccion`, `ciudad`) VALUES (@cedula, @nombre, @correo,@direccion, @ciudad)", conex)
            comando.Parameters.AddWithValue("@cedula", txtCedula.Text)
            comando.Parameters.AddWithValue("@nombre", txtNombre.Text)
            comando.Parameters.AddWithValue("@correo", txtCorreo.Text)
            comando.Parameters.AddWithValue("@direccion", txtDireccion.Text)
            comando.Parameters.AddWithValue("@ciudad", txtCiudad.Text)
            comando.ExecuteNonQuery()
            MsgBox("Datos Guardados")
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox("no se puede guardar porque ya existe un usuario con esa cédula")
        End Try
        Call mostrardatos()
        txtCedula.Text = ""
        txtNombre.Text = ""
        txtCorreo.Text = ""
        txtDireccion.Text = ""
        txtCiudad.Text = ""
        txtCedula.Focus()
        Call mostrardatos()
    End Sub

    Private Sub btnIngresar_Click(sender As Object, e As EventArgs)
        Call añadir()
    End Sub

    Private Sub FrmInsertarClientes_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        conex.Close()
    End Sub



    Private Sub btnIngresar_Click_1(sender As Object, e As EventArgs) Handles btnIngresar.Click
        Call añadir()
    End Sub

    Private Sub btnRegresar_Click_1(sender As Object, e As EventArgs) Handles btnRegresar.Click
        FrmInsertarClientes.ActiveForm.Close()
    End Sub
End Class