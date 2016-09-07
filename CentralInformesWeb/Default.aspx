<%@ Page Title="" Language="vb" AutoEventWireup="True" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.vb" Inherits="CentralInformesWeb._Default" %>

<%@ Register Assembly="DevExpress.Dashboard.v16.1.Web, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.DashboardWeb" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx1" %>
<%@ Register assembly="DevExpress.XtraCharts.v16.1.Web, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web.Designer" tagprefix="dxchartdesigner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
       
    <style type="text/css">
        .auto-style2 {
            width: 44px;
        }
        .auto-style3 {
            width: 5px;
        }
    </style>
       
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <table style="width:100%;">
            <tr>
                <td class="auto-style3">
                    &nbsp;</td>
                <td class="auto-style2">
<dx:ASPxImage ID="ASPxImage1" runat="server" 
            ImageUrl="~/Images/presentation_chart32.png" ShowLoadingImage="true" 
                        style="margin-left: 0px">  
        </dx:ASPxImage>
                </td>
                <td class="style14">
        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Inicio" meta:resourcekey="ASPxLabel3"
            Font-Bold="True" Font-Size="Small" ForeColor="Black">
    </dx:ASPxLabel>      
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style10" colspan="4">
                    <hr style="color: #1E395B" />
                </td>
            </tr>
            <tr>
                <td class="style10" colspan="4">
                    <dx:ASPxLabel ID="txtTienda" runat="server" Text="Tienda:" Theme="Default">
                    </dx:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td class="style10" colspan="4">
                    <dx:ASPxLabel ID="txtFecha" runat="server" Text="Fecha:" Theme="Default">
                    </dx:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td class="style10" colspan="4">
                    &nbsp;</td>
            </tr>
        </table>
    
         <Table ID="Table1" runat="server" Width="100%">
     
        <tr>
                  
        <td>
                     <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="FACTURACIÓN" Theme="Default">
                         <HeaderStyle Font-Bold="False" />
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxTextBox ID="txtFacturacion" runat="server" HorizontalAlign="Center" Width="100%" Height="60px" Font-Size="14pt" ReadOnly="True" BackColor="#F7F7F7" Theme="Default" NullText="0,00 €">
                            <Border BorderStyle="None" />
                        </dx:ASPxTextBox>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel>
                    </td>
                <td class="style12"  >
                    <dx:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" HeaderText="Nº TICKETS" Theme="Default">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxTextBox ID="txtNTickets" runat="server" HorizontalAlign="Center" Width="100%" Height="60px" Font-Size="14pt" ReadOnly="True" BackColor="#F7F7F7" NullText="0">
                            <Border BorderStyle="None" />
                        </dx:ASPxTextBox>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel>
                    </td>
                <td class="style12"  >
                    <dx:ASPxRoundPanel ID="ASPxRoundPanel3" runat="server" HeaderText="Nª LINEAS" Theme="Default">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxTextBox ID="txtNLineas" runat="server" HorizontalAlign="Center" Width="100%" Height="60px" Font-Size="14pt" ReadOnly="True" BackColor="#F7F7F7" NullText="0">
                            <Border BorderStyle="None" />
                        </dx:ASPxTextBox>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel>
                    </td>
               <td class="style12"  >
                   <dx:ASPxRoundPanel ID="ASPxRoundPanel4" runat="server" HeaderText="TICKET MEDIO" Theme="Default">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxTextBox ID="txtTicketMedio" runat="server" HorizontalAlign="Center" Width="100%" Height="60px" Font-Size="14pt" ReadOnly="True" BackColor="#F7F7F7" NullText="0,00">
                            <Border BorderStyle="None" />
                        </dx:ASPxTextBox>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel>
                    </td>
               <td class="style12"  >
                   <dx:ASPxRoundPanel ID="ASPxRoundPanel5" runat="server" HeaderText="UPT" Theme="Default">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxTextBox ID="txtUpt" runat="server" HorizontalAlign="Center" Width="100%" Height="60px" Font-Size="14pt" ReadOnly="True" BackColor="#F7F7F7" NullText="0,00">
                            <Border BorderStyle="None" />
                        </dx:ASPxTextBox>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel>
                    </td>
               <td class="style12"  >
                   <dx:ASPxRoundPanel ID="ASPxRoundPanel6" runat="server" HeaderText="PMA" Theme="Default">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxTextBox ID="txtPma" runat="server" HorizontalAlign="Center" Width="100%" Height="60px" Font-Size="14pt" ReadOnly="True" BackColor="#F7F7F7" NullText="0,00">
                            <Border BorderStyle="None" />
                        </dx:ASPxTextBox>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel>
                    </td>
            </tr>  
    </Table>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
       <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Laforja Internacional, S.A." meta:resourcekey="ASPxLabel3"
            Font-Bold="True" ForeColor="Black">
    </dx:ASPxLabel>      
</asp:Content>

