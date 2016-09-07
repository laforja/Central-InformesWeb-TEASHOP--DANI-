Imports DevExpress.DashboardCommon
Imports CentralInformesWeb.com.serveftp.laforja


'Namespace Dashboard_DataLoading
Partial Public Class _Default
    'Inherits DevExpress.DashboardCommon.Dashboard
    Inherits System.Web.UI.Page

    Public Sub ASPxDashboardViewer1_DataLoading(ByVal sender As Object, ByVal e As DashboardDataLoadingEventArgs) 'Handles MyBase.DataLoading
        If e.DataSource.ComponentName = "dashboardObjectDataSource1" Then
            e.Data = InicializarGrid()
        End If
    End Sub

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim ds As DataSet = InicializarGrid()

        txtTienda.Text = "Tienda: " & Session("Almacen") & " - " & Session("NombreTienda")
        txtFecha.Text = "Fecha: " & Now.Date

        If ds.Tables(0).Rows.Count > 0 Then
            With ds.Tables(0).Rows(0)
                txtFacturacion.Text = Math.Round(.Item("Facturacion"), 2) & " €"
                txtNTickets.Text = .Item("NTickets")
                txtNLineas.Text = .Item("NLineas")
                txtTicketMedio.Text = Math.Round(.Item("TicketMedio"), 2)
                txtUpt.Text = Math.Round(.Item("Upt"), 2)
                txtPma.Text = Math.Round(.Item("Pma"), 2)
            End With
        End If
    End Sub

    Private Function InicializarGrid() As DataSet
        Dim strSQL, strGroupOrder, strSQLTotal As String

        strSQL = "SELECT DATEADD(DAY, 0, DATEDIFF(day, 0, Estadisticas.FechaFactura)) AS FechaFactura, '' AS Nombre, " &
                       "SUM(Estadisticas.ImporteFactura + Estadisticas.ImporteIva) AS Facturacion, " &
                       "COUNT(DISTINCT Estadisticas.NumeroDeFactura) AS NTickets, " &
                       "COUNT(Estadisticas.NumeroDeFactura) AS NLineas, " &
                       "SUM(Estadisticas.ImporteFactura + Estadisticas.ImporteIva) / COUNT(DISTINCT Estadisticas.NumeroDeFactura) AS TicketMedio, " &
                       "CONVERT(DECIMAL(16,2),COUNT(NumeroDeFactura)) / CONVERT(DECIMAL(16, 2), COUNT(DISTINCT NumeroDeFactura))  AS Upt, " &
                       "AVG(Estadisticas.ImporteFactura + Estadisticas.ImporteIva) AS Pma " &
                 "FROM Estadisticas  " &
                 "WHERE FechaFactura >= '" & Now.Date & " 00:00:00" & "' and FechaFactura <= '" & Now.Date & " 23:59:59" & "' And NumeroDeTienda = '" & Session("Tienda") & "' "

        strGroupOrder = " GROUP BY DATEADD(DAY, 0, DATEDIFF(day, 0, FechaFactura))"

        strSQLTotal = strSQL & strGroupOrder

        Dim dbws As New com.serveftp.laforja.VgeServicios()
        Dim ds As DataSet = dbws.ObtenerDatosF(0, strSQLTotal)

        Return ds
    End Function

End Class
'End Namespace