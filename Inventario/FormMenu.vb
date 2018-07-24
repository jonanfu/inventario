Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class FormMenu
    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs)

    End Sub
    Private Sub Panel8_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Panel9_Paint(sender As Object, e As PaintEventArgs) Handles Panel9.Paint

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub btnProvedor_Click(sender As Object, e As EventArgs) Handles btnProvedor.Click
        FrmProvedores.ShowDialog()

    End Sub

    Private Sub btnProductos_Click(sender As Object, e As EventArgs)
        FrmProductos.ShowDialog()
    End Sub

    Private Sub Btnclientes_Click(sender As Object, e As EventArgs) Handles Btnclientes.Click
        FrmClientes.ShowDialog()
    End Sub

    Private Sub Btnkardex_Click(sender As Object, e As EventArgs) Handles btnProductos.Click
        FrmProductos.ShowDialog()
    End Sub

    Private Sub Btnventas_Click(sender As Object, e As EventArgs) Handles Btnventas.Click
        FrmVentas.ShowDialog()
    End Sub

    Private Sub btnCompras_Click(sender As Object, e As EventArgs) Handles btnCompras.Click
        FrmCompras.ShowDialog()
    End Sub
End Class