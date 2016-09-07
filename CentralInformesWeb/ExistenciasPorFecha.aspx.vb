Imports DevExpress.Web
Imports System.Drawing
Imports System.IO
Imports DevExpress.XtraPrinting
Imports System.Net.Mime
Imports System.Threading

Public Class ExistenciasPorFecha
    Inherits System.Web.UI.Page

    Protected Overrides Sub InitializeCulture()
        UICulture = If(Global_asax.strIdioma Is Nothing, "ES", Global_asax.strIdioma)
        MyBase.InitializeCulture()
    End Sub

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'If Not IsPostBack Then
        'Session.Clear()
        CmbAlmacen.Value = Session("Almacen")
        ASPxFecha.Value = Date.Now
        'End If
    End Sub

    Protected Sub cmbAlmacen_Init(ByVal sender As Object, ByVal e As EventArgs)
        'Cargar Inventarios para la tienda
        If Session("Almacen") = 800 Then
            CmbAlmacen.DataSource = CargarComboAlmacen()
            CmbAlmacen.TextField = "CodigoDeAlmacen"
            CmbAlmacen.ValueField = "CodigoDeAlmacen"
            CmbAlmacen.DataBind()

            CmbAlmacen.Enabled = True
        Else
            CmbAlmacen.Enabled = False
        End If
    End Sub

    Private Function CargarComboAlmacen() As DataSet
        Dim strSQL As String = Nothing

        strSQL = "SELECT CodigoDeAlmacen, NombreDelAlmacen FROM Almacenes ORDER BY CodigoDeAlmacen"
        Dim dbws As New com.serveftp.laforja.VgeServicios()
        Dim ds As DataSet = dbws.ObtenerDatosF(0, strSQL)

        Return ds
    End Function

    Protected Sub GridExistencias_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        ViewState("needBind") = True
        GridExistencias.DataBind()
        GridExistencias.PageIndex = 0


        'Agrupar por Familias si se marca el Check Agrupar
        GridExistencias.BeginUpdate()
        GridExistencias.ClearSort()
        If chkAgrupar.Checked Then
            GridExistencias.GroupBy(GridExistencias.Columns("Familia"))
        End If
        GridExistencias.EndUpdate()
        GridExistencias.CollapseAll()

    End Sub

    'Actualizar datos del grid.
    Protected Sub GridExistencias_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
        If ViewState("needBind") IsNot Nothing AndAlso CBool(ViewState("needBind")) Then
            GridExistencias.DataSource = InicializarGrid()
        Else
            If GridExistencias.VisibleRowCount > 0 Then
                ViewState("needBind") = True
                GridExistencias.DataSource = InicializarGrid()
            Else
                ViewState("needBind") = False
            End If
        End If
    End Sub

    'Ordenación columnas
    Private Sub GridExistencias_BeforeColumnSortingGrouping(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewBeforeColumnGroupingSortingEventArgs) Handles GridExistencias.BeforeColumnSortingGrouping
        GridExistencias.PageIndex = 0
    End Sub

    Protected Sub GridExistencias_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridExistencias.Init
        'Crear columna para Total 
        Dim colTotal As GridViewDataTextColumn = New GridViewDataTextColumn()
        colTotal.Caption = "Total"
        colTotal.FieldName = "Total"
        colTotal.Width = 40
        colTotal.VisibleIndex = 6
        colTotal.UnboundType = DevExpress.Data.UnboundColumnType.Integer
        colTotal.VisibleIndex = GridExistencias.VisibleColumns.Count
        colTotal.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        colTotal.CellStyle.Font.Bold = True
        colTotal.PropertiesTextEdit.DisplayFormatString = "c2"
        colTotal.FooterCellStyle.Font.Bold = True
        colTotal.FooterCellStyle.ForeColor = Color.Brown
        GridExistencias.Columns.Add(colTotal)
    End Sub

    Private Function InicializarGrid()
        Dim strXmlOutputData As String = String.Empty
        Dim dbws As New com.serveftp.laforja.VgeServicios()
        Dim ds As New DataSet
        Dim dr As DataRow
        Dim strSQL As String = String.Empty
        Dim strSQLWhere As String = String.Empty
        Dim strSQLTotal As String = String.Empty

        'If IsPostBack = False Then
        '    ds.Tables.Add(0)
        '    'Añadir campos al dataset para rellenarlos despues
        '    ds.Tables(0).Columns.Add("Almacen", GetType(Integer))
        '    ds.Tables(0).Columns.Add("StockReal", GetType(Double))
        '    ds.Tables(0).Columns.Add("Familia", GetType(String))
        '    ds.Tables(0).Columns.Add("Precio", GetType(Double))
        '    ds.Tables(0).Columns.Add("Importe", GetType(Double))
        'Else
        'Pasar XML devuelto a un dataset Parámetros: 0, Almacén, Fecha, Stock, Obsoletos.
        strXmlOutputData = dbws.ListadoDeExistenciasEnFecha(0, CmbAlmacen.Value, ASPxFecha.Text, chkStock.Checked, "False")
        Dim sr As StringReader = New StringReader(strXmlOutputData)
        ds.ReadXml(sr)
        sr.Dispose()

        'Añadir campos al dataset para rellenarlos despues
        ds.Tables(0).Columns.Add("Almacen", GetType(Integer))
        ds.Tables(0).Columns.Add("StockReal", GetType(Double))
        ds.Tables(0).Columns.Add("Familia", GetType(String))
        ds.Tables(0).Columns.Add("Precio", GetType(Double))
        ds.Tables(0).Columns.Add("Importe", GetType(Double))

        'Recuperar Precio Coste Real de la Tienda
        strSQL = "SELECT PrecioCosteReal " & _
                 "FROM Tiendas WHERE NumeroDeTienda = " & CmbAlmacen.Value
        Dim dsTda As DataSet = dbws.ObtenerDatosF(0, strSQL)

        'Recuperar Precio de los artículos según el PrecioCosteReal de la tienda
        strSQL = "SELECT FamiliasDeArticulos.CodigoDeFamilia, FamiliasDeArticulos.Descripcion, Tarifas.Precio, Articulos.CodigoDeArticulo " & _
                 "FROM Articulos INNER JOIN Tarifas ON Articulos.CodigoDeArticulo = Tarifas.CodigoDeArticulo " & _
                                "INNER JOIN FamiliasDeArticulos ON Articulos.Familia = FamiliasDeArticulos.CodigoDeFamilia " & _
                 "WHERE Tarifas.Tarifa = '" & dsTda.Tables(0).Rows(0).Item("PrecioCosteReal") & "' AND ((FamiliasDeArticulos.CodigoDeFamilia <> 904) " & _
                                "OR (FamiliasDeArticulos.CodigoDeFamilia <> 901) OR (FamiliasDeArticulos.CodigoDeFamilia <> 908) " & _
                                "OR (FamiliasDeArticulos.CodigoDeFamilia <> 903) OR (FamiliasDeArticulos.CodigoDeFamilia <> 905) " & _
                                "OR (FamiliasDeArticulos.CodigoDeFamilia <> 097) OR (FamiliasDeArticulos.CodigoDeFamilia <> 907) " & _
                                "OR (FamiliasDeArticulos.CodigoDeFamilia <> 009) OR (FamiliasDeArticulos.CodigoDeFamilia <> 902) "

        If Session("Almacen") = 8001 Then 'Bilbao
            strSQLWhere = "OR (FamiliasDeArticulos.CodigoDeFamilia <> 097))"
        ElseIf Session("Almacen") = 181 Then 'Italia
            strSQLWhere = "OR (FamiliasDeArticulos.CodigoDeFamilia <> 801))"
        Else 'Resto
            strSQLWhere = "OR (FamiliasDeArticulos.CodigoDeFamilia <> 801) OR (FamiliasDeArticulos.CodigoDeFamilia <> 097))"


        End If
       
        strSQLTotal = strSQL & strSQLWhere
        Dim dsArt As DataSet = dbws.ObtenerDatosF(0, strSQLTotal)

        'Rellenar los campos del dataset para mostrarlo en el grid
        For Each dr In ds.Tables(0).Rows
            Dim foundrows() As DataRow
            foundrows = dsArt.Tables(0).Select("CodigoDeArticulo = '" & dr.Item("Id") & "'")
            If foundrows.Length > 0 Then
                If Session("Almacen") = 8001 Then 'Bilbao
                    If foundrows(0).Item("CodigoDeFamilia") = 904 Or foundrows(0).Item("CodigoDeFamilia") = 901 Or foundrows(0).Item("CodigoDeFamilia") = 908 Or foundrows(0).Item("CodigoDeFamilia") = 903 Or foundrows(0).Item("CodigoDeFamilia") = 905 Or foundrows(0).Item("CodigoDeFamilia") = 97 Or foundrows(0).Item("CodigoDeFamilia") = 907 Or foundrows(0).Item("CodigoDeFamilia") = 9 Or foundrows(0).Item("CodigoDeFamilia") = 902 Or foundrows(0).Item("CodigoDeFamilia") = 97 Then
                        foundrows(0).Delete()
                    Else
                        Dim decDecimal As Decimal = dr.Item("Stock").ToString().Replace(".", ",")
                        dr.Item("StockReal") = Format(decDecimal, "##,###0.000")
                        dr.Item("Almacen") = CmbAlmacen.Text
                        dr.Item("Precio") = IIf(IsDBNull(foundrows(0).Item("Precio")), 0, foundrows(0).Item("Precio"))
                        dr.Item("Familia") = foundrows(0).Item("Descripcion")
                    End If
                ElseIf Session("Almacen") = 181 Then 'Italia
                    If foundrows(0).Item("CodigoDeFamilia") = 904 Or foundrows(0).Item("CodigoDeFamilia") = 901 Or foundrows(0).Item("CodigoDeFamilia") = 908 Or foundrows(0).Item("CodigoDeFamilia") = 903 Or foundrows(0).Item("CodigoDeFamilia") = 905 Or foundrows(0).Item("CodigoDeFamilia") = 97 Or foundrows(0).Item("CodigoDeFamilia") = 907 Or foundrows(0).Item("CodigoDeFamilia") = 9 Or foundrows(0).Item("CodigoDeFamilia") = 902 Or foundrows(0).Item("CodigoDeFamilia") = 801 Then
                        foundrows(0).Delete()
                    Else
                        Dim decDecimal As Decimal = dr.Item("Stock").ToString().Replace(".", ",")
                        dr.Item("StockReal") = Format(decDecimal, "##,###0.000")
                        dr.Item("Almacen") = CmbAlmacen.Text
                        dr.Item("Precio") = IIf(IsDBNull(foundrows(0).Item("Precio")), 0, foundrows(0).Item("Precio"))
                        dr.Item("Familia") = foundrows(0).Item("Descripcion")
                    End If
                Else
                    If foundrows(0).Item("CodigoDeFamilia") = 904 Or foundrows(0).Item("CodigoDeFamilia") = 901 Or foundrows(0).Item("CodigoDeFamilia") = 908 Or foundrows(0).Item("CodigoDeFamilia") = 903 Or foundrows(0).Item("CodigoDeFamilia") = 905 Or foundrows(0).Item("CodigoDeFamilia") = 97 Or foundrows(0).Item("CodigoDeFamilia") = 907 Or foundrows(0).Item("CodigoDeFamilia") = 9 Or foundrows(0).Item("CodigoDeFamilia") = 902 Or foundrows(0).Item("CodigoDeFamilia") = 97 Or foundrows(0).Item("CodigoDeFamilia") = 801 Then
                        foundrows(0).Delete()
                    Else
                        Dim decDecimal As Decimal = dr.Item("Stock").ToString().Replace(".", ",")
                        dr.Item("StockReal") = Format(decDecimal, "##,###0.000")
                        dr.Item("Almacen") = CmbAlmacen.Text
                        dr.Item("Precio") = IIf(IsDBNull(foundrows(0).Item("Precio")), 0, foundrows(0).Item("Precio"))
                        dr.Item("Familia") = foundrows(0).Item("Descripcion")
                    End If
                End If
            End If
        Next

        'Return foundrows1.CopyToDataTable
        Return ds
    End Function

    Protected Sub GridExistencias_CustomUnboundColumnData(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewColumnDataEventArgs)
        Dim Precio As Decimal

        'Multiplicar Precio X Stock si se marca el check de Valorado.
        If e.Column.FieldName = "Total" Then
            If chkValorado.Checked Then
                If IsDBNull(e.GetListSourceFieldValue("StockReal")) Then
                    e.Value = 0
                Else
                    If IsDBNull((e.GetListSourceFieldValue("Precio"))) Then
                        e.Value = 0
                    Else
                        Precio = CDec(e.GetListSourceFieldValue("Precio"))
                    End If
                    Dim Stock As Decimal = CDec(e.GetListSourceFieldValue("StockReal"))
                    e.Value = Precio * Stock
                End If
            Else
                e.Value = 0
            End If
        End If
    End Sub

    Protected Sub ASPxGridView1_SummaryDisplayText(ByVal sender As Object, ByVal e As ASPxGridViewSummaryDisplayTextEventArgs)
        'Formato de los Totales del Grid.
        Select Case e.Item.FieldName
            Case "StockReal"
                e.Text = "CAN: " & String.Format("{0:N2}", e.Value)
            Case "Precio"
                e.Text = "TOT: " & String.Format("{0:N2}", e.Value)
            Case "PrecioCosteReal"
                e.Text = "TOT: " & String.Format("{0:N2}", e.Value)
            Case "Total"
                e.Text = "TOT: " & String.Format("{0:N2}", e.Value)
        End Select
    End Sub

    'Exportar datos del Grid
    Protected Sub ASPxMenu2_ItemClick(ByVal source As Object, ByVal e As MenuItemEventArgs)
        If GridExistencias.VisibleRowCount > 0 Then
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
        End If
    End Sub

    Public Shared Sub WriteResponseXLS(ByVal response As HttpResponse, ByVal filearray() As Byte, ByVal type As String)
        response.ClearContent()
        response.Buffer = True
        response.Cache.SetCacheability(HttpCacheability.Private)
        response.ContentType = "application/xls"
        Dim contentDisposition As New ContentDisposition()
        contentDisposition.FileName = "ExistenciasPorFecha.xlsx"
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
        contentDisposition.FileName = "ExistenciasPorFecha.pdf"
        contentDisposition.DispositionType = type
        response.AddHeader("Content-Disposition", contentDisposition.ToString())
        response.BinaryWrite(filearray)
        HttpContext.Current.ApplicationInstance.CompleteRequest()
        Try
            response.End()
        Catch e1 As System.Threading.ThreadAbortException
        End Try
    End Sub

    'Marcar en rojo los importes negativos.
    Protected Sub gv_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewTableDataCellEventArgs)
        If e.DataColumn.FieldName = "StockReal" Or e.DataColumn.FieldName = "Importe" Or e.DataColumn.FieldName = "Total" Then
            If CDec(e.CellValue) < 0 Then
                e.Cell.ForeColor = Color.Red
            End If
        End If
    End Sub

    Private Sub GridExistencias_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridExistencias.PageIndexChanged
        ViewState("needBind") = True
        GridExistencias.DataBind()
    End Sub

    Private Sub GridExistencias_PageSizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridExistencias.PageSizeChanged
        'ViewState("needBind") = True
            GridExistencias.DataBind()
    End Sub

End Class