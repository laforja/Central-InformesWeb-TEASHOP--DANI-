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

Imports DevExpress.XtraCharts
Imports DevExpress.XtraCharts.Web

Public Class PedidosDeReposicion
    Inherits System.Web.UI.Page
    Private ds As DataSet = Nothing

    Protected Overrides Sub InitializeCulture()
        UICulture = If(Global_asax.strIdioma Is Nothing, "ES", Global_asax.strIdioma)
        MyBase.InitializeCulture()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        Dim f_expression As String = GetFilterExpression()
        Dim ds As New DataSet
        Dim dtCabecera, dtDetalle As DataTable
        If (Not IsPostBack) AndAlso (Not IsCallback) Then
            ds = New DataSet()
            dtCabecera = InicializarGrid(f_expression)
            dtDetalle = InicializarGridDetalle(f_expression)

            ds.Tables.AddRange(New DataTable() {dtCabecera, dtDetalle})

            GridPedidos.DataSource = ds
            GridPedidos.DataBind()
        End If
    End Sub

    Protected Sub GridDetallePedido_BeforePerformDataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Dim f_expression As String = GetFilterExpression()

        Dim detailTable As DataTable = InicializarGridDetalle(f_expression)
        Dim dv As DataView = New DataView(detailTable)
        Dim detailGridView As ASPxGridView = CType(sender, ASPxGridView)
        Dim key As Object = detailGridView.GetMasterRowKeyValue()
        Dim keyValues As String() = key.ToString().Split("|")

        dv.RowFilter = "Numero = " & Convert.ToInt32(keyValues(2)) & " AND Año = " & Convert.ToInt32(keyValues(0)) & "And Serie = '" & Convert.ToString(keyValues(1)) & "'"
        detailGridView.DataSource = dv
    End Sub

    Protected Sub GridDetallePedido_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Session("Numero") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    End Sub

    Private Function GetFilterExpression() As String
        Dim res_str As String = String.Empty
        Dim lst_operator As New List(Of CriteriaOperator)()

        If (Not String.IsNullOrEmpty(Session("Tienda"))) Then
            lst_operator.Add(New BinaryOperator("NumeroDeTienda", String.Format("{0}", Session("Tienda")), BinaryOperatorType.Equal))
        End If

        If Not chkFinalizados.Checked Then
            lst_operator.Add(New BinaryOperator("Situacion", String.Format("{0}", "P"), BinaryOperatorType.Equal))
        End If

        If chkAnulados.Checked Then
            lst_operator.Add(New BinaryOperator("Estado", String.Format("{0}", "C"), BinaryOperatorType.Equal))
        End If

        If (chkPrt.Checked) Then
            lst_operator.Add(New BinaryOperator("PedidosClientes.SuPedido", String.Format("{0}", "PRT%"), BinaryOperatorType.Like))
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

    Private Function InicializarGrid(ByVal strWhere As String) As DataTable
        Dim strSQL As String

        strSQL = "SELECT PedidosClientes.Numero, PedidosClientes.Año, PedidosClientes.Serie, CONVERT(varchar(10), Tiendas.NumeroDeTienda) + ' - ' + Tiendas.Nombre AS Tienda, PedidosClientes.Numero AS Documento, PedidosClientes.FechaDocumento, PedidosClientes.Nombre AS Cliente, PedidosClientes.Situacion, " & _
                        "PedidosClientes.Total, PedidosClientes.Observaciones, PedidosClientes.SuPedido " & _
                 "FROM PedidosClientes INNER JOIN Tiendas ON PedidosClientes.Tienda = Tiendas.NumeroDeTienda "
        strSQL = strSQL & strWhere

        Dim dbws As New com.serveftp.laforja.VgeServicios()
        Dim ds As DataSet = dbws.ObtenerDatosF(0, strSQL)

        Dim dt As New DataTable
        dt = ds.Tables(0)
        Return dt
    End Function

    Private Function InicializarGridDetalle(ByVal strWhere As String) As DataTable
        Dim strSQL, strSQLOrder As String

        strSQL = "SELECT Numero, Año, Serie, CodigoDeArticulo, Descripcion, Cantidad, CantidadServida, CantidadPteServir, PrecioVenta, PorDescuento, Importe " & _
                 "FROM DetallePedidosCliente "
        strSQLOrder = "ORDER BY NumLinea"
        strSQL = strSQL

        Dim dbws As New com.serveftp.laforja.VgeServicios()
        Dim ds As DataSet = dbws.ObtenerDatosF(0, strSQL)

        Dim dt As New DataTable
        dt = ds.Tables(0)
        Return dt
    End Function

    Protected Sub ASPxGridView1_SummaryDisplayText(ByVal sender As Object, ByVal e As ASPxGridViewSummaryDisplayTextEventArgs)
        Select Case e.Item.FieldName
            Case "Total"
                e.Text = String.Format("{0:N2}", e.Value)
        End Select
    End Sub

    Protected Sub chkPrt_CheckedChanged(sender As Object, e As EventArgs)
        GridPedidos.DataBind()
    End Sub

    Protected Sub chkFinalizados_CheckedChanged(sender As Object, e As EventArgs)
        GridPedidos.DataBind()
    End Sub

    Protected Sub chkAnulados_CheckedChanged(sender As Object, e As EventArgs)
        GridPedidos.DataBind()
    End Sub

    Protected Sub GridPedidos_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        GridPedidos.DataBind()
    End Sub

    Protected Sub GridPedidos_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
        Dim f_expression As String = GetFilterExpression()

        GridPedidos.DataSource = InicializarGrid(f_expression)
    End Sub

    Protected Sub GridDetallePedido_SummaryDisplayText(ByVal sender As Object, ByVal e As ASPxGridViewSummaryDisplayTextEventArgs)
        Select Case e.Item.FieldName
            Case "Cantidad"
                e.Text = String.Format("{0:N3}", e.Value)
            Case "Importe"
                e.Text = String.Format("{0:N2} €", e.Value)
        End Select
    End Sub

End Class