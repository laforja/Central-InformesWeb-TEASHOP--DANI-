﻿<%@ Page Title="Acerca de nosotros" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="About.aspx.vb" Inherits="CentralInformesWeb.About" %>

    <%@ Register assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style2
        {
        }
        .style3
        {
            height: 5px;
        }
        .style4
        {
            width: 322px;
        }
        .style5
        {
            height: 5px;
            width: 322px;
        }
        .style7
        {
            height: 20px;
        }
        .style8
        {
            width: 322px;
            height: 111px;
        }
        .style9
        {
            height: 111px;
        }
        .style10
        {
            width: 101px;
        }
        .style14
        {
        }
        .style15
        {
            width: 32px;
        }
        .style16
        {
            height: 20px;
            width: 85%;
        }
        .style17
        {
            height: 20px;
            width: 10%;
        }
        .style18
        {
            width: 20px;
        }
        .style19
        {
            width: 322px;
            height: 53px;
        }
        .style20
        {
            height: 53px;
        }
        .style21
        {
            width: 95px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table style="width:100%;">
    <tr>
            <td style="padding-right: 4px" class="style15">
                <dx:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/Images/help32.png" 
                    ShowLoadingImage="true">
                </dx:ASPxImage>
            </td>
            <td class="style16">
             
                 <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Size="Small" meta:resourcekey="ASPxLabel1"
                    ForeColor="Black" Text="Acerca de...">
                </dx:ASPxLabel>
             
            </td>
            <td class="style17">
            </td>
        </tr>
    <tr>
            <td style="padding-right: 4px" class="style14" colspan="3">
                <hr style="color: #516B92" />
            </td>
        </tr>
    </table>

     <table style="width:100%;">
        <tr>
            <td style="padding-right: 4px" class="style4">
                &nbsp;</td>
            <td class="style7" colspan="3">
             
            </td>
            <td class="style7">
            </td>
        </tr>
        <tr>
            <td class="style8">
            </td>
            <td class="style9" colspan="3">
                <br /><br />
                <center><img src="images/Contacto.jpg" alt="Laforja Internacional, S.A." align="middle"></a></td></center>
            <td class="style9">
            </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td colspan="3" style="color: #6699FF; font-weight: bold;">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style19">
                </td>
            <td colspan="3" style="color: #000000; font-weight: normal;" align="center" 
                class="style20">
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" meta:resourcekey="ASPxLabel10"
                    Text="Puede contactar con nosotros llamándonos o enviándonos un e-mail. De cualquier forma estaremos encantados de atenderle." 
                    Theme="Office2010Blue">
                </dx:ASPxLabel>
            </td>
            <td class="style20">
                </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td colspan="3" style="color: #6699FF; font-weight: bold;">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td colspan="3" style="color: #6699FF; font-weight: bold;" align="center">
                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Font-Bold="True" Font-Size="Small" 
                    Text="Laforja Internacional, S.A." Theme="Office2010Blue" 
                    ForeColor="#003399">
                </dx:ASPxLabel>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td colspan="3" align="center">
                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="Muntaner 479, 2º 2ª" 
                    Theme="Office2010Blue">
                </dx:ASPxLabel>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style5">
            </td>
            <td class="style3" colspan="3" align="center">
                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="08021 Barcelona" 
                    Theme="Office2010Blue">
                </dx:ASPxLabel>
            </td>
            <td class="style3">
            </td>
        </tr>
        <tr>
            <td class="style5">
                &nbsp;</td>
            <td class="style3" colspan="3">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style21">
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Teléfono:" meta:resourcekey="ASPxLabel4"
                    Theme="Office2010Blue">
                </dx:ASPxLabel>
            </td>
            <td class="style18">
                &nbsp;</td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="+34 93 418 68 68" 
                    Theme="Office2010Blue">
                </dx:ASPxLabel>
                </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style21">
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Fax:" Theme="Office2010Blue">
                </dx:ASPxLabel>
            </td>
            <td class="style18">
                &nbsp;</td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="+34 93 211 26 12" 
                    Theme="Office2010Blue">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style21">
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="E-Mail:" 
                    Theme="Office2010Blue">
                </dx:ASPxLabel>
            </td>
            <td class="style18">
                &nbsp;</td>
            <td>
                <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" NavigateUrl="mailto:retail@laforja.com" Text="retail@laforja.com" Theme="Office2010Blue" Cursor="default" />
            </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style2" colspan="2">
                &nbsp;</td>
            <td class="style10">
                &nbsp;</td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style2" colspan="3">
               
                &nbsp;</td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style2" colspan="2">
                &nbsp;</td>
            <td class="style10">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
