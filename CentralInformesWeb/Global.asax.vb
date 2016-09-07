Imports System.Web.SessionState
Imports DevExpress.DashboardWeb

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Public Shared strSQLWhere As String = Nothing
    Public Shared intComboSelExist As String

    Public Shared strNomEmpresa As String
    'Public Shared intCodCliente As Integer
    'Public Shared intTienda As Integer
    'Public Shared intAlmacen As Integer
    Public Shared bolDatos As Boolean = False

    Public Shared strIdioma As String = Nothing

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Se desencadena al iniciar la aplicación
        Dim newDashboardStorage As New DashboardFileStorage("~/App_Data")
        DashboardService.SetDashboardStorage(newDashboardStorage)
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Se desencadena al iniciar la sesión
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Se desencadena al comienzo de cada solicitud
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Se desencadena al intentar autenticar el uso
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Se desencadena cuando se produce un error
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Se desencadena cuando finaliza la sesión
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Se desencadena cuando finaliza la aplicación
    End Sub

End Class