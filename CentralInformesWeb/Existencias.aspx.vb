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

Public Class Existencias
    Inherits System.Web.UI.Page

    Dim strFormula As String

    Protected Overrides Sub InitializeCulture()
        UICulture = If(Global_asax.strIdioma Is Nothing, "ES", Global_asax.strIdioma)
        MyBase.InitializeCulture()
    End Sub

    Private Sub Existencias_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Dim strSQL As String = "SELECT FormulaDisponible FROM Empresa"
        Dim dbws As New com.serveftp.laforja.VgeServicios()
        Dim ds As DataSet = dbws.ObtenerDatosF(0, strSQL)

        If Not IsDBNull(ds.Tables(0).Rows(0).Item("FormulaDisponible")) Then
            strFormula = ds.Tables(0).Rows(0).Item("FormulaDisponible")
        End If
        InicializarGrid()
    End Sub

    Private Sub InicializarGrid()
        Dim strSQL, strSQLFinal As String

        If IsNothing(Global_asax.strSQLWhere) Then
            Global_asax.strSQLWhere = " WHERE (Stocks.Almacen = " & Session("Almacen") & ") "
        End If

        strSQL = "SELECT Stocks.Almacen, Articulos.CodigoDeArticulo AS ID, Articulos.Descripcion AS Articulo, FamiliasDeArticulos.Descripcion AS Familia, " & _
                                      "Stocks.StockReal, Stocks.PendienteRecibir, Stocks.PendienteServir, Stocks.StockReservado, 0 AS StockDisponible, Articulos.PrecioCosteReal, Tarifas.Precio " & _
                 "FROM Articulos INNER JOIN FamiliasDeArticulos ON Articulos.Familia = FamiliasDeArticulos.CodigoDeFamilia " & _
                                "INNER JOIN Tarifas ON Articulos.CodigoDeArticulo = Tarifas.CodigoDeArticulo " & _
                                "LEFT OUTER JOIN Stocks ON Articulos.CodigoDeArticulo = Stocks.CodigoDeArticulo "

        'If Global_asax.strSQLWhere <> "" Then
        strSQLFinal = strSQL & Global_asax.strSQLWhere
        'End If

        Dim dbws As New com.serveftp.laforja.VgeServicios()
        Dim ds As DataSet = dbws.ObtenerDatosF(0, strSQLFinal)

        For Each dr In ds.Tables(0).Rows
            If IsNothing(strFormula) Then 'SR-RS-PS+PR
                dr.item("StockDisponible") = dr.item("StockReal") - dr.item("StockReservado") - dr.item("PendienteServir") + dr.item("PendienteRecibir")
            Else
                Dim intTotalFormula As String
                intTotalFormula = Replace(strFormula, "SR", dr.item("StockReal"))
                intTotalFormula = Replace(intTotalFormula, "RS", dr.item("StockReservado"))
                intTotalFormula = Replace(intTotalFormula, "PS", dr.item("PendienteServir"))
                intTotalFormula = Replace(intTotalFormula, "PR", dr.item("PendienteRecibir"))

                dr.item("StockDisponible") = intTotalFormula
            End If
        Next

        GridExistencias.DataSource = ds
        GridExistencias.DataBind()
    End Sub

    Protected Sub GridExistencias_CustomUnboundColumnData(sender As Object, e As DevExpress.Web.ASPxGridViewColumnDataEventArgs)
        If e.Column.FieldName = "Importe" Then
            If IsDBNull(e.GetListSourceFieldValue("StockReal")) Then
                e.Value = 0
            Else
                Dim Precio As Decimal

                If Global_asax.intComboSelExist = 1 Then
                    If (IsDBNull(e.GetListSourceFieldValue("PrecioCosteReal"))) Then
                        Precio = 0
                    Else
                        Precio = CDec(e.GetListSourceFieldValue("PrecioCosteReal"))
                    End If
                ElseIf Global_asax.intComboSelExist = 2 Then
                    Precio = CDec(e.GetListSourceFieldValue("Precio"))
                End If

                Dim StockReal As Integer = Convert.ToInt32(e.GetListSourceFieldValue("StockReal"))
                e.Value = Precio * StockReal
                End If
        End If
    End Sub

    Protected Sub ASPxGridView1_SummaryDisplayText(ByVal sender As Object, ByVal e As ASPxGridViewSummaryDisplayTextEventArgs)
        Select Case e.Item.FieldName
            Case "StockReal"
                e.Text = String.Format("{0:N3}", e.Value)
            Case "Importe"
                e.Text = String.Format("{0:N2}", e.Value)
        End Select
    End Sub

    'Combo Valorado a
    Protected Sub ASPxComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim cmbValorado As ASPxComboBox = DirectCast(sender, ASPxComboBox)
        'Session("SelectedIndex") = cmbValorado.SelectedIndex

        Select Case cmbValorado.SelectedIndex
            Case "0" 'Sin Valorar
                GridExistencias.Columns("Precio").Visible = False
                GridExistencias.Columns("PrecioCosteReal").Visible = False
                GridExistencias.Columns("Importe").Visible = False
                Global_asax.intComboSelExist = 0
            Case "1"
                GridExistencias.Columns("Precio").Visible = False
                GridExistencias.Columns("PrecioCosteReal").Visible = True
                GridExistencias.Columns("Importe").Visible = True
                Global_asax.intComboSelExist = 1
            Case "2"
                GridExistencias.Columns("Precio").Visible = True
                GridExistencias.Columns("PrecioCosteReal").Visible = False
                GridExistencias.Columns("Importe").Visible = True
                Global_asax.intComboSelExist = 2
        End Select
    End Sub

    'Check mostrar artículos con Stock
    Protected Sub chkStock_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkStock As ASPxCheckBox = DirectCast(sender, ASPxCheckBox)

        If chkStock.Checked Then
            Global_asax.strSQLWhere = " WHERE (Stocks.StockReal > 0) AND (Stocks.Almacen = " & Session("Almacen") & ") "
        Else
            Global_asax.strSQLWhere = " WHERE (Stocks.Almacen = " & Session("Almacen") & ") "
        End If
        InicializarGrid()
    End Sub

    'Mostrar Columnas al checkear "Más Información".
    Protected Sub chkMasInfo_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkMasInfo As ASPxCheckBox = DirectCast(sender, ASPxCheckBox)

        GridExistencias.Columns("PendienteRecibir").Visible = chkMasInfo.Checked
        GridExistencias.Columns("PendienteServir").Visible = chkMasInfo.Checked
        GridExistencias.Columns("StockReservado").Visible = chkMasInfo.Checked
        GridExistencias.Columns("StockDisponible").Visible = chkMasInfo.Checked
    End Sub

    'Exportar Grid.
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

    'Pintar en rojo los importes negativos.
    Protected Sub gv_HtmlDataCellPrepared(sender As Object, e As DevExpress.Web.ASPxGridViewTableDataCellEventArgs)
        If e.DataColumn.FieldName = "StockReal" Or e.DataColumn.FieldName = "PendienteRecibir" Or e.DataColumn.FieldName = "PendienteServir" Or e.DataColumn.FieldName = "StockReservado" Or e.DataColumn.FieldName = "StockDisponible" Or e.DataColumn.FieldName = "Importe" Then
            If CDec(e.CellValue) < 0 Then
                e.Cell.ForeColor = Color.Red
            End If
        End If
    End Sub

End Class