Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Data
Imports System.Configuration
Imports System.Xml
Imports System.Reflection
Imports System.Globalization
Imports System.Threading
Imports DevExpress.Web

Public Class Login
    Inherits System.Web.UI.Page

    Public Shared idusuario As Integer = 0

    Protected Overrides Sub InitializeCulture()
        UICulture = If(Request.QueryString("lang") Is Nothing, "ES", Request.QueryString("lang"))

        If (Request.QueryString("lang")) Is Nothing Then
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-ES")
            Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-ES")
            Global_asax.strIdioma = "ES"
        Else
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Request.QueryString("lang"))
            Thread.CurrentThread.CurrentUICulture = New CultureInfo(Request.QueryString("lang"))
            Global_asax.strIdioma = Request.QueryString("lang")
        End If

        MyBase.InitializeCulture()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Private Sub LoginUser_Authenticate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.AuthenticateEventArgs) Handles LoginUser.Authenticate
        'consulta a la base de datos
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet
        Dim strSQL As String = "SELECT * FROM Usuarios  WHERE UsuarioEmail = '" & LoginUser.UserName & "' AND Password = '" & LoginUser.Password & "'"
        'cadena conexion
        Using conn As New SqlConnection(ConfigurationManager.ConnectionStrings("Default").ToString())
            conn.Open()
            da = New SqlDataAdapter(strSQL, conn)
            da.Fill(ds)


            'Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            'devuelve la fila afectada
            If ds.Tables(0).Rows.Count = 0 Then
                e.Authenticated = False
            Else
                'Dim da As New SqlDataAdapter
                strSQL = "SELECT * FROM Empresas WHERE IDEmpresa = " & ds.Tables(0).Rows(0).Item("IDEmpresa")
                Dim dt As New DataTable

                da = New SqlDataAdapter(strSQL, conn)
                da.Fill(dt)

                Global_asax.strNomEmpresa = dt.Rows(0).Item("Nombre")

                Session("IDCliente") = ds.Tables(0).Rows(0).Item("CodigoDeCliente")
                Session("Tienda") = ds.Tables(0).Rows(0).Item("Tienda")
                ' Global_asax.intAlmacen = ds.Tables(0).Rows(0).Item("Almacen")
                Session("Almacen") = ds.Tables(0).Rows(0).Item("Almacen")
                Session("NombreTienda") = ds.Tables(0).Rows(0).Item("NombreTienda")

                'URL WS
                My.Settings("CentralInformesWeb_com_serveftp_laforja_VgeServicios") = dt.Rows(0).Item("RutaWs")


                My.Settings.Save()

                ''DATABASE
                'Dim myConfiguration As Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
                'myConfiguration.ConnectionStrings.ConnectionStrings("Default1").ConnectionString = "server=NS205146;database=Teashop;uid=vg;password=Vg2008;"
                ''myConfiguration.AppSettings.Settings.Item("Default1").Value = "Adios"
                'myConfiguration.Save()

                idusuario = ds.Tables(0).Rows(0).Item("IDUsuario")
                e.Authenticated = True
            End If
        End Using
    End Sub

    Private Sub LoginUser_LoggedIn(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoginUser.LoggedIn
        Response.Redirect("~")
        'Response.Redirect("DiferenciasDeInventario.aspx")
    End Sub

End Class