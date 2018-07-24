Public Class FrmReporteClientes
    Private Sub FrmReporteClientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CrystalReportViewer1.RefreshReport()
    End Sub
End Class