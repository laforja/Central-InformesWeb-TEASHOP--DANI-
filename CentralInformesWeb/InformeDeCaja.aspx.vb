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
Imports System.Drawing
Imports DevExpress.Data.Filtering
Imports System.Threading

Public Class InformeDeCaja
    Inherits System.Web.UI.Page

    Protected Overrides Sub InitializeCulture()
        UICulture = If(Global_asax.strIdioma Is Nothing, "ES", Global_asax.strIdioma)
        'Dim CultureInfo As CultureInfo = CultureInfo.CreateSpecificCulture(UICulture)
        'Thread.CurrentThread.CurrentCulture = CultureInfo
        'Thread.CurrentThread.CurrentUICulture = CultureInfo
        MyBase.InitializeCulture()
    End Sub

    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        If IsPostBack = False Then
            Dim f_expression As String = GetFilterExpression()
            GridDiarioVentas.DataSource = InicializarGrid(f_expression)
            GridDiarioVentas.DataBind()
        End If
    End Sub

    Protected Sub cmbFechaDesde_Init(sender As Object, e As EventArgs)
        Dim dateEdit As ASPxDateEdit = TryCast(sender, ASPxDateEdit)
        dateEdit.Value = Date.Now
    End Sub

    Protected Sub cmbFechaHasta_Init(sender As Object, e As EventArgs)
        Dim dateEdit As ASPxDateEdit = TryCast(sender, ASPxDateEdit)
        dateEdit.Value = Date.Now
    End Sub

    Private Function InicializarGrid(ByVal strWhere As String) As DataSet
        Dim strSQL, strGroupOrder, strSQLTotal As String

        strSQL = "SELECT DATEADD(DAY, 0, DATEDIFF(day, 0, Fecha)) AS Fecha, " & _
                    "SUM(CASE WHEN (ConceptoID = 'TK') THEN Importe ELSE 0 END) AS VentasTotales, " & _
                    "SUM(CASE WHEN (ConceptoID = 'TK' AND FormaDePago = 11) THEN Importe ELSE 0 END) AS Efectivo, " & _
                    "SUM(CASE WHEN (ConceptoID = 'TK' AND FormaDePago = 12) THEN Importe ELSE 0 END) AS Visa, " & _
                    "SUM(CASE WHEN (ConceptoID = 'TK' AND FormaDePago = 16) THEN Importe ELSE 0 END) AS Amex, " & _
                    "SUM(CASE WHEN (ConceptoID = 'TK' AND FormaDePago = 15) THEN Importe ELSE 0 END) AS Vales, " & _
                    "SUM(CASE WHEN (ConceptoID = 'TK' AND FormaDePago = 14) THEN Importe ELSE 0 END) AS ValesDevolucion, " & _
                    "SUM(CASE WHEN (ConceptoID = 'TK' AND CobroOPago = 'P') THEN Importe ELSE 0 END) AS Pagos, " & _
                    "SUM(CASE WHEN (ConceptoID = 'TK' AND CobroOPago = 'C') THEN Importe ELSE 0 END) AS Cobros, " & _
                    "SUM(CASE WHEN (ConceptoID = 'DE') THEN Importe ELSE 0 END) AS Deficit, " & _
                    "SUM(CASE WHEN (ConceptoID = 'SU') THEN Importe ELSE 0 END) AS Superavit " & _
                 "FROM DiarioCaja "

        strGroupOrder = " GROUP BY DATEADD(DAY, 0, DATEDIFF(day, 0, Fecha)) "

        strSQLTotal = strSQL & strWhere & strGroupOrder

        Dim dbws As New com.serveftp.laforja.VgeServicios()
        Dim ds As DataSet = dbws.ObtenerDatosF(0, strSQLTotal)

        Return ds
    End Function

    'Exportación
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

    'Marcar en rojo los resultados que sean negativos.
    Protected Sub gv_HtmlDataCellPrepared(sender As Object, e As DevExpress.Web.ASPxGridViewTableDataCellEventArgs)
        If e.DataColumn.FieldName = "Importe" Or e.DataColumn.FieldName = "IVA" Or e.DataColumn.FieldName = "Total" Then
            If CDec(e.CellValue) < 0 Then
                e.Cell.ForeColor = Color.Red
            End If
        End If
    End Sub

    'Actualizar datos del grid.
    Protected Sub GridDiarioVentas_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
        Dim f_expression As String = GetFilterExpression()
        GridDiarioVentas.DataSource = InicializarGrid(f_expression)
    End Sub

    Private Function GetFilterExpression() As String
        Dim res_str As String = String.Empty
        Dim lst_operator As New List(Of CriteriaOperator)()

        If (Not String.IsNullOrEmpty(Session("Tienda"))) Then
            lst_operator.Add(New BinaryOperator("DiarioCaja.Tienda", String.Format("{0}", Session("Tienda")), BinaryOperatorType.Equal))
        End If

        If cmbFechaDesde.Value IsNot Nothing Then
            lst_operator.Add(New BinaryOperator("DiarioCaja.Fecha", cmbFechaDesde.Text & " 00:00:00", BinaryOperatorType.GreaterOrEqual))
        End If

        If cmbFechaHasta.Value IsNot Nothing Then
            lst_operator.Add(New BinaryOperator("DiarioCaja.Fecha", cmbFechaHasta.Text & " 23:59:59", BinaryOperatorType.LessOrEqual))
        End If

        If lst_operator.Count > 0 Then
            Dim op(lst_operator.Count - 1) As CriteriaOperator
            For i As Integer = 0 To lst_operator.Count - 1
                op(i) = lst_operator(i)
            Next i
            Dim res_operator As CriteriaOperator = New GroupOperator(GroupOperatorType.And, op)
            res_str = "WHERE " & res_operator.ToString()
            res_str = Replace(res_str, "[", "")
            res_str = Replace(res_str, "]", "")
        End If

        Return res_str
    End Function

    Protected Sub GridDiarioVentas_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        GridDiarioVentas.DataBind()
    End Sub

    Protected Sub cmbFecha_Valuechanged(ByVal sender As Object, ByVal e As EventArgs)
        Select Case cmbFecha.Value
            Case "Hoy"
                cmbFechaDesde.Value = Now.Date
                cmbFechaHasta.Value = Now.Date
            Case "Día anterior"
                cmbFechaDesde.Value = Now.Date.AddDays(-1)
                cmbFechaHasta.Value = Now.Date.AddDays(-1)
            Case "Semana actual"
                cmbFechaDesde.Value = GetFirstOfLastWeek()
                Dim datfechaHasta As Date = cmbFechaDesde.Text
                cmbFechaHasta.Value = datfechaHasta.AddDays(6)
            Case "Mes actual"
                cmbFechaDesde.Value = CDate("01/" & Now.Date.Month & "/" & Now.Date.Year)
                cmbFechaHasta.Value = CDate(Date.DaysInMonth(Now.Date.Year, Now.Date.Month) & "/" & Now.Date.Month & "/" & Now.Date.Year)
            Case "Año actual"
                cmbFechaDesde.Value = CDate("01/01/" & Now.Date.Year)
                cmbFechaHasta.Value = CDate("31/12/" & Now.Date.Year)
            Case "Última semana"
                Dim datFechaDesde As Date = GetFirstOfLastWeek()
                cmbFechaDesde.Value = datFechaDesde.AddDays(-7)
                Dim datfechaHasta As Date = cmbFechaDesde.Text
                cmbFechaHasta.Value = datfechaHasta.AddDays(6)
            Case "Último mes"
                cmbFechaDesde.Value = CDate("01/" & (Now.Date.Month - 1) & "/" & Now.Date.Year)
                cmbFechaHasta.Value = CDate(Date.DaysInMonth(Now.Date.Year, Now.Date.Month) & "/" & (Now.Date.Month - 1) & "/" & Now.Date.Year)
            Case "Último año"
                cmbFechaDesde.Value = CDate("01/01/" & Now.Date.Year - 1)
                cmbFechaHasta.Value = CDate("31/12/" & Now.Date.Year - 1)
        End Select
    End Sub

    'Busca el primer día de la semana
    Public Function GetFirstOfLastWeek() As DateTime
        Dim today As DateTime, daysSinceMonday As Integer
        today = DateTime.Today
        daysSinceMonday = today.DayOfWeek - DayOfWeek.Monday
        If daysSinceMonday < 0 Then
            daysSinceMonday += 7
        End If
        Return today.AddDays(-daysSinceMonday)
    End Function

End Class