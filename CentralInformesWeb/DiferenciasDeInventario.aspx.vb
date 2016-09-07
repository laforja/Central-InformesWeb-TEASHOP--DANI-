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

Imports System.IO
Imports System.Linq
Imports System.Web
Imports System.Web.UI.WebControls
Imports DevExpress.XtraPrinting
Imports System.Net.Mime

Public Class DiferenciasDeInventario
    Inherits System.Web.UI.Page

    Protected Overrides Sub InitializeCulture()
        UICulture = If(Global_asax.strIdioma Is Nothing, "ES", Global_asax.strIdioma)
        MyBase.InitializeCulture()
    End Sub

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'GridDiferenciasInv.DataSource = Cargargrid(Nothing)
    End Sub

    Protected Sub cmbInventario_Init(ByVal sender As Object, ByVal e As EventArgs)
        cmbInventario.DataSource = CargarComboInventario()
        cmbInventario.TextField = "Inventario"
        cmbInventario.ValueField = "IdInventario"
        cmbInventario.DataBind()
    End Sub

    Private Function CargarComboInventario() As DataSet
        Dim strSQL As String = Nothing

        strSQL = "SELECT IdInventario FROM InventarioFisico WHERE Almacen = " & Session("Almacen") & " AND Incorporado = 0 GROUP BY IdInventario"
        Dim dbws As New com.serveftp.laforja.VgeServicios()
        Dim ds As DataSet = dbws.ObtenerDatosF(0, strSQL)

        Return ds
    End Function

    'Botones Exportar 
    Protected Sub ASPxMenu2_ItemClick(ByVal source As Object, ByVal e As MenuItemEventArgs)
        'Select Case e.Item.Text
        '    Case "Excel"
        '        ASPxGridViewExporter1.WriteXlsToResponse()
        '        Exit Select
        '    Case "Pdf"
        '        ASPxGridViewExporter1.WritePdfToResponse()
        '        Exit Select
        'End Select
        Select Case e.Item.Text
            Case "Excel"
                Using ms As New MemoryStream()
                    Dim pcl As New PrintableComponentLink(New PrintingSystem())
                    pcl.Component = ASPxGridViewExporter1
                    pcl.Margins.Right = 70
                    pcl.Margins.Left = pcl.Margins.Right
                    pcl.Landscape = False
                    pcl.CreateDocument(True)
                    pcl.PrintingSystem.Document.AutoFitToPagesWidth = 1
                    pcl.ExportToXlsx(ms)
                    WriteResponseXLS(Me.Response, ms.ToArray(), System.Net.Mime.DispositionTypeNames.Inline.ToString())
                End Using
            Case "Pdf"
                Using ms As New MemoryStream()
                    Dim pcl As New PrintableComponentLink(New PrintingSystem())
                    pcl.Component = ASPxGridViewExporter1
                    pcl.Margins.Right = 70
                    pcl.Margins.Left = pcl.Margins.Right
                    pcl.Landscape = False
                    pcl.CreateDocument(True)
                    pcl.PrintingSystem.Document.AutoFitToPagesWidth = 1
                    pcl.ExportToPdf(ms)
                    WriteResponsePDF(Me.Response, ms.ToArray(), System.Net.Mime.DispositionTypeNames.Inline.ToString())
                End Using
        End Select
    End Sub

    Public Shared Sub WriteResponseXLS(ByVal response As HttpResponse, ByVal filearray() As Byte, ByVal type As String)
        response.ClearContent()
        response.Buffer = True
        response.Cache.SetCacheability(HttpCacheability.Private)
        response.ContentType = "application/xls"
        Dim contentDisposition As New ContentDisposition()
        contentDisposition.FileName = "DiferenciasInventario.xlsx"
        contentDisposition.DispositionType = type
        response.AddHeader("Content-Disposition", contentDisposition.ToString())
        response.BinaryWrite(filearray)
        HttpContext.Current.ApplicationInstance.CompleteRequest()
        Try
            response.End()
        Catch e1 As System.Threading.ThreadAbortException
        End Try
    End Sub

    Public Shared Sub WriteResponsePDF(ByVal response As HttpResponse, ByVal filearray() As Byte, ByVal type As String)
        response.ClearContent()
        response.Buffer = True
        response.Cache.SetCacheability(HttpCacheability.Private)
        response.ContentType = "application/pdf"
        Dim contentDisposition As New ContentDisposition()
        contentDisposition.FileName = "DiferenciasInventario.pdf"
        contentDisposition.DispositionType = type
        response.AddHeader("Content-Disposition", contentDisposition.ToString())
        response.BinaryWrite(filearray)
        HttpContext.Current.ApplicationInstance.CompleteRequest()
        Try
            response.End()
        Catch e1 As System.Threading.ThreadAbortException
        End Try
    End Sub

    Protected Sub cmbInventario_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim cmbInventario As ASPxComboBox = DirectCast(sender, ASPxComboBox)

        ' GridDiferenciasInv.DataSource = Cargargrid(cmbInventario.Text)
        Dim f_expression As String = GetFilterExpression()
        GridDiferenciasInv.DataSource = Cargargrid(f_expression)
        GridDiferenciasInv.DataBind()

        If cmbInventario.SelectedIndex = -1 Then
            ASPxComboBox1.Enabled = False
            chkAgrupar.Enabled = False
            ASPxMenu2.Enabled = False
            GridDiferenciasInv.Enabled = False
        Else
            ASPxComboBox1.Enabled = True
            chkAgrupar.Enabled = True
            ASPxMenu2.Enabled = True
            GridDiferenciasInv.Enabled = True
        End If
    End Sub

    Private Function Cargargrid(ByVal strSQLWhere As String)
        Dim strSQL, strSQLTotal As String


        strSQL = "SELECT InventarioFisico.Articulo, Articulos.Descripcion, Stocks.StockReal, InventarioFisico.Cantidad, Articulos.PrecioCosteReal, Tarifas.Precio, FamiliasDeArticulos.CodigoDeFamilia, FamiliasDeArticulos.Descripcion AS DescFamilia " & _
                 "FROM FamiliasDeArticulos INNER JOIN Articulos ON FamiliasDeArticulos.CodigoDeFamilia = Articulos.Familia " & _
                                      "RIGHT OUTER JOIN InventarioFisico LEFT OUTER JOIN Stocks ON InventarioFisico.Articulo = Stocks.CodigoDeArticulo ON Articulos.CodigoDeArticulo = InventarioFisico.Articulo " & _
                                      "LEFT OUTER JOIN Tarifas ON Articulos.CodigoDeArticulo = Tarifas.CodigoDeArticulo "

        strSQLTotal = strSQL & strSQLWhere

        Dim dbws As New com.serveftp.laforja.VgeServicios()
        Dim ds As DataSet = dbws.ObtenerDatosF(0, strSQLTotal)

        If Global_asax.intComboSelExist = "0" Or IsNothing(Global_asax.intComboSelExist) Then 'SIN VALORAR
            For Each dr In ds.Tables(0).Rows
                dr.item("Precio") = 0
            Next
        End If

        If Mid(cmbInventario.Value, 1, 1) = "T" Then
            'Recuperar Todos los articulos
            Dim strTarifa As String
            If ASPxComboBox1.Value <> "Sin valorar" Then
                strTarifa = ASPxComboBox1.Text
            Else
                strTarifa = "1"
            End If

            strSQL = "SELECT Articulos.CodigoDeArticulo AS Articulo, Articulos.Descripcion, Articulos.PrecioCosteReal, Stocks.StockReal, 0 AS Cantidad, Tarifas.Precio, FamiliasDeArticulos.CodigoDeFamilia, " & _
                     "FamiliasDeArticulos.Descripcion AS DescFamilia " & _
                     "FROM Articulos INNER JOIN " & _
                     "Stocks ON Articulos.CodigoDeArticulo = Stocks.CodigoDeArticulo INNER JOIN " & _
                     "Tarifas ON Articulos.CodigoDeArticulo = Tarifas.CodigoDeArticulo INNER JOIN " & _
                     "FamiliasDeArticulos ON Articulos.Familia = FamiliasDeArticulos.CodigoDeFamilia " & _
                     "WHERE(Stocks.Almacen = 800) And (Tarifas.Tarifa = '" & strTarifa & "')"
            Dim dbws2 As New com.serveftp.laforja.VgeServicios()
            Dim ds1 As DataSet = dbws.ObtenerDatosF(0, strSQL)

            For Each dr In ds.Tables(0).Rows
                Dim foundrows() As Data.DataRow
                foundrows = ds1.Tables(0).Select("Articulo = '" & dr.item("Articulo") & "'")
                If foundrows.Length > 0 Then
                    foundrows(0).Item("Cantidad") = dr.item("Cantidad")
                End If
            Next
            Return ds1
        End If

        Return ds
    End Function

    Protected Sub GridDiferenciasInv_CustomUnboundColumnData(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewColumnDataEventArgs)
        Select Case e.Column.FieldName
            Case "Diferencia"
                If IsDBNull(e.GetListSourceFieldValue("StockReal")) Then
                    e.Value = 0
                Else
                    Dim decStockReal, decCantidad As Decimal

                    decStockReal = CDec(e.GetListSourceFieldValue("StockReal"))
                    decCantidad = CDec(e.GetListSourceFieldValue("Cantidad"))

                    e.Value = decCantidad - decStockReal
                End If
            Case "Importe"
                If IsDBNull(e.GetListSourceFieldValue("Diferencia")) Then
                    e.Value = 0
                Else
                    Dim Precio As Decimal
                    If Global_asax.intComboSelExist = "0" Then 'Si es sin Valorar
                        e.Value = 0
                    Else
                        If (IsDBNull(e.GetListSourceFieldValue("Precio"))) Then
                            Precio = 0
                        Else
                            Precio = CDec(e.GetListSourceFieldValue("Precio"))
                        End If
                        Dim Diferencia As Decimal = (e.GetListSourceFieldValue("Diferencia"))
                        e.Value = Precio * Diferencia
                    End If
                End If
        End Select
    End Sub

    Protected Sub GridDiferenciasInv_SummaryDisplayText(ByVal sender As Object, ByVal e As ASPxGridViewSummaryDisplayTextEventArgs)
        Select Case e.Item.FieldName
            Case "StockReal"
                e.Text = "CAN.:" & String.Format("{0:N3}", e.Value)
            Case "Cantidad"
                e.Text = "INV.:" & String.Format("{0:N3}", e.Value)
            Case "Diferencia"
                e.Text = "DIF.:" & String.Format("{0:N3}", e.Value)
            Case "Precio"
                e.Text = "PREC.:" & String.Format("{0:N3}", e.Value)
            Case "Importe"
                e.Text = "TOTAL:" & String.Format("{0:N3}", e.Value)
        End Select
    End Sub

    Protected Sub GridDiferenciasInv_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewTableDataCellEventArgs)
        If e.DataColumn.FieldName = "StockReal" Or e.DataColumn.FieldName = "Cantidad" Or e.DataColumn.FieldName = "Diferencia" Or e.DataColumn.FieldName = "Importe" Then
            If CDec(e.CellValue) < 0 Then
                e.Cell.ForeColor = Color.Red
            End If
        End If
    End Sub

    Protected Sub GridDiferenciasInv_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        If cmbInventario.SelectedIndex >= 0 Then
            GridDiferenciasInv.DataBind()
        End If
    End Sub

    Protected Sub GridDiferenciasInv_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
        If cmbInventario.SelectedIndex >= 0 Then
            Dim f_expression As String = GetFilterExpression()
            GridDiferenciasInv.DataSource = Cargargrid(f_expression)
        End If
    End Sub

    Private Function GetFilterExpression() As String
        Dim res_str As String = String.Empty
        Dim lst_operator As New List(Of CriteriaOperator)()

        If (Not String.IsNullOrEmpty(Session("Almacen"))) Then
            lst_operator.Add(New BinaryOperator("Stocks.Almacen", String.Format("{0}", Session("Almacen")), BinaryOperatorType.Equal))
        End If

        If cmbInventario.Value IsNot Nothing Then
            lst_operator.Add(New BinaryOperator("InventarioFisico.IdInventario", cmbInventario.Text, BinaryOperatorType.Equal))
        End If

        If ASPxComboBox1.Value <> "Sin valorar" Then
            lst_operator.Add(New BinaryOperator("Tarifas.Tarifa", ASPxComboBox1.Text, BinaryOperatorType.Equal))
        Else
            lst_operator.Add(New BinaryOperator("Tarifas.Tarifa", "1", BinaryOperatorType.Equal))
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

    Protected Sub ASPxComboBox1_Init(ByVal sender As Object, ByVal e As EventArgs)
        'Mostrar otras tarifas si se selecciona la tienda de Italia
        If Session("Almacen") = "181" Then
            ASPxComboBox1.Items.Add("Sin valorar")
            ASPxComboBox1.Items.Add("PVPI")
            ASPxComboBox1.Items.Add("PVPSI")
            ASPxComboBox1.Items.Add("TIL")
        Else
            ASPxComboBox1.Items.Add("Sin valorar")
            ASPxComboBox1.Items.Add("1")
            ASPxComboBox1.Items.Add("3")
            ASPxComboBox1.Items.Add("TN")
        End If
    End Sub

    'Combo Valorado a
    Protected Sub ASPxComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim cmbValorado As ASPxComboBox = DirectCast(sender, ASPxComboBox)
        'Session("SelectedIndex") = cmbValorado.SelectedIndex

        Select Case cmbValorado.SelectedItem.ToString
            Case "0" 'Sin Valorar
                GridDiferenciasInv.Columns("Precio").Visible = False
                'GridDiferenciasInv.Columns("PrecioCosteReal").Visible = False
                GridDiferenciasInv.Columns("Importe").Visible = False
                Global_asax.intComboSelExist = 0
            Case "1"
                GridDiferenciasInv.Columns("Precio").Visible = True
                'GridDiferenciasInv.Columns("PrecioCosteReal").Visible = True
                GridDiferenciasInv.Columns("Importe").Visible = True
                Global_asax.intComboSelExist = 1
            Case "2"
                GridDiferenciasInv.Columns("Precio").Visible = True
                'GridDiferenciasInv.Columns("PrecioCosteReal").Visible = False
                GridDiferenciasInv.Columns("Importe").Visible = True
                Global_asax.intComboSelExist = 3
            Case "3" 'TN
                GridDiferenciasInv.Columns("Precio").Visible = True
                'GridDiferenciasInv.Columns("PrecioCosteReal").Visible = False
                GridDiferenciasInv.Columns("Importe").Visible = True
                Global_asax.intComboSelExist = "TN"
            Case "PVPI"
                GridDiferenciasInv.Columns("Precio").Visible = True
                'GridDiferenciasInv.Columns("PrecioCosteReal").Visible = False
                GridDiferenciasInv.Columns("Importe").Visible = True
                Global_asax.intComboSelExist = "PVPI"
            Case "PVPSI"
                GridDiferenciasInv.Columns("Precio").Visible = True
                'GridDiferenciasInv.Columns("PrecioCosteReal").Visible = False
                GridDiferenciasInv.Columns("Importe").Visible = True
                Global_asax.intComboSelExist = "PVPSI"
            Case "TIL"
                GridDiferenciasInv.Columns("Precio").Visible = True
                'GridDiferenciasInv.Columns("PrecioCosteReal").Visible = False
                GridDiferenciasInv.Columns("Importe").Visible = True
                Global_asax.intComboSelExist = "TIL"
        End Select
        Dim f_expression As String = GetFilterExpression()
        GridDiferenciasInv.DataSource = Cargargrid(f_expression)
        GridDiferenciasInv.DataBind()
    End Sub

    'Check Agrupar por Famílias
    Protected Sub chkAgrupar_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        GridDiferenciasInv.BeginUpdate()
        GridDiferenciasInv.ClearSort()
        If chkAgrupar.Checked Then
            GridDiferenciasInv.GroupBy(GridDiferenciasInv.Columns("DescFamilia"))
        End If
        GridDiferenciasInv.EndUpdate()
        GridDiferenciasInv.CollapseAll()
    End Sub

End Class