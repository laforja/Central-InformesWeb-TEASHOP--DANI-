﻿Imports DevExpress.Web
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

Public Class EntradaMercanciasProveedor
    Inherits System.Web.UI.Page

    Protected Overrides Sub InitializeCulture()
        UICulture = If(Global_asax.strIdioma Is Nothing, "ES", Global_asax.strIdioma)
        'Dim CultureInfo As CultureInfo = CultureInfo.CreateSpecificCulture(UICulture)
        'Thread.CurrentThread.CurrentCulture = CultureInfo
        'Thread.CurrentThread.CurrentUICulture = CultureInfo
        MyBase.InitializeCulture()
    End Sub

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            ' Dim f_expression As String = GetFilterExpression()
            GridPedidos.DataSource = InicializarGrid(Nothing)
            GridPedidos.DataBind()
        End If
    End Sub

    Protected Sub cmbCodArt_Init(ByVal sender As Object, ByVal e As EventArgs)
        'Dim cb As ASPxComboBox = TryCast(sender, ASPxComboBox)
        'Dim container As GridViewDataItemTemplateContainer = TryCast(cb.NamingContainer, GridViewDataItemTemplateContainer)

        'cb.ClientInstanceName = String.Format("cb{0}", container.VisibleIndex)
        'cb.ClientSideEvents.SelectedIndexChanged = String.Format("function (s, e) {{ OnSelectedIndexChanged(s, e, txt{0}); }}", container.VisibleIndex)

        'cb.DataSource = CargarComboArticulos()
        'cb.ValueField = "CodigoDeArticulo"
        'cb.ValueType = GetType(Int32)
        'cb.TextField = "Descripcion"
        'cb.DataBindItems()
    End Sub

    Protected Sub txtDescArt_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim txt As ASPxTextBox = TryCast(sender, ASPxTextBox)
        Dim container As GridViewDataItemTemplateContainer = TryCast(txt.NamingContainer, GridViewDataItemTemplateContainer)

        txt.ClientInstanceName = String.Format("txt{0}", container.VisibleIndex)
    End Sub

    Private Function CargarComboArticulos() As DataSet
        Dim strSQL As String

        strSQL = "SELECT TOP(100) CodigoDeArticulo, Descripcion FROM Articulos"
        Dim dbws As New com.serveftp.laforja.VgeServicios()
        Dim ds As DataSet = dbws.ObtenerDatosF(0, strSQL)

        Return ds
    End Function

    Private Function CargarComboAlmacenes() As DataSet
        Dim strSQL As String

        strSQL = "SELECT CodigoDeAlmacen, NombreDelAlmacen FROM Almacenes"
        Dim dbws As New com.serveftp.laforja.VgeServicios()
        Dim ds As DataSet = dbws.ObtenerDatosF(0, strSQL)

        Return ds
    End Function

    Private Function CargarComboProveedores() As DataSet
        Dim strSQL As String

        strSQL = "SELECT CodigoDeProveedor, Nombre FROM Proveedores"
        Dim dbws As New com.serveftp.laforja.VgeServicios()
        Dim ds As DataSet = dbws.ObtenerDatosF(0, strSQL)

        Return ds
    End Function

    Private Function InicializarGrid(ByVal strWhere As String) As DataSet
        Dim ds As New DataSet
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("CodigoDeArticulo")
        dt.Columns.Add("Descripcion")
        For i As Integer = 1 To 1
            dt.Rows.Add(New Object() {Nothing, Nothing})
        Next i

        ds.Tables.Add(dt)
        Return ds
    End Function

    Protected Sub GridPedidos_CellEditorInitialize(ByVal sender As Object, ByVal e As ASPxGridViewEditorEventArgs)
        Select Case e.Column.FieldName
            Case "CodigoDeArticulo"
                Dim cb As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
                'Dim container As GridViewDataItemTemplateContainer = TryCast(cb.NamingContainer, GridViewDataItemTemplateContainer)

                cb.ClientInstanceName = String.Format("cb{0}", e.VisibleIndex)
                cb.ClientSideEvents.SelectedIndexChanged = String.Format("function (s, e) {{ OnSelectedIndexChanged(s, e, txt{0}); }}", e.VisibleIndex)

                cb.DataSource = CargarComboArticulos()
                cb.ValueField = "CodigoDeArticulo"
                cb.ValueType = GetType(Int32)
                cb.TextField = "Descripcion"
                cb.DataBindItems()
            Case "Proveedor"
                Dim cmb As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
                cmb.DataSource = CargarComboProveedores()
                cmb.ValueField = "CodigoDeProveedor"
                cmb.ValueType = GetType(Int32)
                cmb.TextField = "Nombre"
                cmb.DataBindItems()
            Case "Almacen"
                Dim cmb As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
                cmb.DataSource = CargarComboAlmacenes()
                cmb.ValueField = "CodigoDeAlmacen"
                cmb.ValueType = GetType(Int32)
                cmb.TextField = "NombreDelAlmacen"
                cmb.DataBindItems()
        End Select
    End Sub

    Protected Sub ASPxButton1_Click(sender As Object, e As System.EventArgs)
        GridPedidos.AddNewRow()
    End Sub

End Class