Public Class About
    Inherits System.Web.UI.Page

    Protected Overrides Sub InitializeCulture()
        UICulture = If(Global_asax.strIdioma Is Nothing, "ES", Global_asax.strIdioma)
        MyBase.InitializeCulture()
    End Sub

End Class