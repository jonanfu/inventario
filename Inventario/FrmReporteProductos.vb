﻿Public Class FrmReporteProductos
    Private Sub FrmReporteProductos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CrystalReportViewer1.RefreshReport()
    End Sub
End Class