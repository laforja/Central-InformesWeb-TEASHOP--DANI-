Imports DevExpress.Web
Imports System.Globalization

Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections
Imports System.Collections.Generic
Imports Microsoft.VisualBasic
Imports System
Imports System.Web.UI
Imports DevExpress.Web.Data

Public Class DiarioDeVentas
    Inherits System.Web.UI.Page

    Private Sub DiarioDeVentas_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        If Not IsPostBack Then
            cmbFechaHasta.Value = Date.Today
            cmbFechaDesde.Value = Date.Today

            Global_asax.datFechaDesde = cmbFechaDesde.Text & " 00:00:00"
            Global_asax.datFechaHasta = cmbFechaHasta.Value & " 23:59:59"

            InicializarGrid()
        End If
    End Sub

    Private Sub InicializarGrid()
        Dim strSQL As String = "SELECT Tiendas.Nombre, DiarioCaja.Caja, DiarioCaja.Fecha, DiarioCaja.ConceptoId, DiarioCaja.Ticket, SUM(Estadisticas.ImporteFactura) AS Importe, " & _
                        "SUM(Estadisticas.ImporteIva) AS IVA, DiarioCaja.Importe AS Total " & _
                 "FROM DiarioCaja INNER JOIN Estadisticas ON DiarioCaja.Ticket = Estadisticas.NumeroDeFactura " & _
                                 "INNER JOIN Tiendas ON DiarioCaja.Tienda = Tiendas.NumeroDeTienda " & _
                 "WHERE (DiarioCaja.Fecha >= '" & Global_asax.datFechaDesde & "') AND (DiarioCaja.Fecha <= '" & Global_asax.datFechaHasta & "') AND (Tiendas.NumeroDeTienda = " & Global_asax.intTienda & ") " & _
                 "GROUP BY Tiendas.Nombre, DiarioCaja.Caja, DiarioCaja.Fecha, DiarioCaja.ConceptoId, DiarioCaja.Ticket, DiarioCaja.Importe " & _
                 "ORDER BY DiarioCaja.Fecha"

        Dim dbws As New com.serveftp.laforja.VgeServicios()
        Dim ds As DataSet = dbws.ObtenerDatosF(0, strSQL)

        GridDiarioVentas.DataSource = ds
        GridDiarioVentas.DataBind()
    End Sub

    Protected Sub ASPxMenu2_ItemClick(source As Object, e As MenuItemEventArgs)
        Select Case e.Item.Text
            Case "Excel"
                ASPxGridViewExporter1.WriteXlsToResponse()
                Exit Select
            Case "Pdf"
                ASPxGridViewExporter1.WritePdfToResponse()
                Exit Select
        End Select
    End Sub

    Protected Sub ASPxGridView1_SummaryDisplayText(ByVal sender As Object, ByVal e As ASPxGridViewSummaryDisplayTextEventArgs)
        Select Case e.Item.FieldName
            Case "Importe"
                e.Text = String.Format("{0:N2}", e.Value)
            Case "IVA"
                e.Text = String.Format("{0:N2}", e.Value)
            Case "Total"
                e.Text = String.Format("{0:N2}", e.Value)
        End Select
    End Sub

    Protected Sub btnFiltrar_Click(sender As Object, e As EventArgs)
        Global_asax.datFechaDesde = cmbFechaDesde.Date
        Global_asax.datFechaHasta = cmbFechaHasta.Date
        InicializarGrid()
    End Sub

    Protected Sub cmbFechaDesde_Init(sender As Object, e As System.EventArgs)
        Dim cmbFechaDesde As ASPxDateEdit = DirectCast(sender, ASPxDateEdit)
        'cmbFechaDesde.Value = DateTime.Now.AddDays(-5)
        cmbFechaDesde.Value = Now.Date & " 00:00:00"
        Global_asax.datFechaDesde = cmbFechaDesde.Value
    End Sub

    Protected Sub cmbFechaHasta_Init(sender As Object, e As System.EventArgs)
        Dim cmbFechaHasta As ASPxDateEdit = DirectCast(sender, ASPxDateEdit)
        'cmbFechaDesde.Value = DateTime.Now.AddDays(-5)
        cmbFechaHasta.Value = Now.Date & " 23:59:59"
        Global_asax.datFechaHasta = cmbFechaHasta.Value
    End Sub

 

End Class