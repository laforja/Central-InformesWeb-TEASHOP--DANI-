Imports System.Data.SqlClient
Imports DevExpress.Web
Imports System.Globalization
Imports System.Configuration

Public Class Site
    Inherits System.Web.UI.MasterPage

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        MyBase.OnInit(e)

        If (Context.Session IsNot Nothing) Then
            If (Session.IsNewSession) Then
                'Dim cadenaCookieHeader As String = Request.Headers("Cookie")
                'If (cadenaCookieHeader IsNot Nothing) Or (cadenaCookieHeader.IndexOf("ASP.NET_SessionId") >= 0) Then
                FormsAuthentication.SignOut()
                'Response.Redirect("~/Login.aspx")
                FormsAuthentication.RedirectToLoginPage()
                'Response.End()
            End If
            'End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If HttpContext.Current.User.Identity.IsAuthenticated = True Then
            Dim lbl As ASPxLabel = CType(HeadLoginView.FindControl("ASPxLabel1"), ASPxLabel)
            lbl.Text = Global_asax.strNomEmpresa.ToUpper
            ASPxMenu1.Visible = True
        End If
    End Sub

End Class