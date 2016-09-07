<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Existencias.aspx.vb" Inherits="CentralInformesWeb.Existencias" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>








<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
        }
        .style4
        {
            height: 6px;
        }
        .style5
        {
            height: 6px;
            width: 45px;
        }
        .style6
        {
            width: 45px;
        }
        .style7
        {
            height: 6px;
            width: 75px;
        }
        .style8
        {
            width: 75px;
        }
        .style9
        {
            height: 6px;
            width: 271px;
        }
        .style10
        {
            width: 271px;
        }
        .style11
        {
            height: 6px;
            width: 268px;
        }
        .style12
        {
            width: 268px;
        }
    .style13
    {}
    .style14
    {
        width: 40px;
            height: 12px;
        }
        .style15
        {
            width: 16px;
            height: 12px;
        }
    .style16
    {
        width: 711px;
            height: 12px;
        }
        .style17
        {
            height: 12px;
        }
    </style>
    <script type="text/javascript">
      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <table style="width:100%;">
        <tr>
            <td class="style15">
                </td>
            <td class="style14">
                <dx:ASPxImage ID="ASPxImage1" runat="server" ShowLoadingImage="true" 
                    ImageUrl="~/Images/cubes.png">
                </dx:ASPxImage>
            </td>
            <td class="style16">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Size="Small" 
                    ForeColor="Black" Text="Existencias" meta:resourcekey="ASPxLabel1">
                </dx:ASPxLabel>
            </td>
            <td class="style17">
                <dx:ASPxMenu ID="ASPxMenu2" runat="server" Theme="Office2010Blue" OnItemClick="ASPxMenu2_ItemClick" >
                    <Items>
                        <dx:MenuItem Text="Exportar a:">
                            <Items>
                                <dx:MenuItem Text="Pdf">
                                    <image url="~/Images/ExportToPDF_32x32.png">
                                    </image>
                                </dx:MenuItem>
                                <dx:MenuItem Text="Excel">
                                    <image url="~/Images/ExportToXLS_32x32.png">
                                    </image>
                                </dx:MenuItem>
                            </Items>
                        </dx:MenuItem>
                    </Items>
                </dx:ASPxMenu>
            </td>
        </tr>
        <tr>
            <td class="style13" colspan="4">
                <hr style="color: #516B92" />
            </td>
        </tr>
      </table>
      <table style="width:100%;">
        <tr>
            <td class="style1" colspan="3">
                <table style="width:100%;">
                    <tr>
                        <td class="style5">
                        </td>
                        <td class="style7">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Valorado a:" 
                                Theme="Office2010Blue" meta:resourcekey="ASPxLabel2">
                            </dx:ASPxLabel>
                        </td>
                        <td class="style9">
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" EnableTheming="True" 
                                SelectedIndex="0" Theme="Office2010Blue" AutoPostBack="True" 
                                EnableCallbackMode="True" meta:resourcekey="ASPxComboBox1"
                                onselectedindexchanged="ASPxComboBox1_SelectedIndexChanged" >
                                <Items>
                                    <dx:ListEditItem Selected="True" Text="Sin valorar" Value="Sin valorar" meta:resourcekey="SinValorar"/>
                                    <dx:ListEditItem Text="Precio coste real" Value="Precio coste real" meta:resourcekey="PrecioCoste"/>
                                    <dx:ListEditItem Text="PVP" Value="PVP" meta:resourcekey="Pvp"/>
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                        <td class="style11">
                            <dx:ASPxCheckBox ID="chkStock" runat="server" Text="Artículos con stock" AutoPostBack="True" OnCheckedChanged="chkStock_CheckedChanged"
                                Theme="Office2010Blue" meta:resourcekey="chkStock">
                            </dx:ASPxCheckBox>
                        </td>
                        <td class="style4">
                            <dx:ASPxCheckBox ID="chkMasInfo" runat="server" Text="Más información" AutoPostBack="True" OnCheckedChanged="chkMasInfo_CheckedChanged"
                                Theme="Office2010Blue" meta:resourcekey="chkMasInfo">
                            </dx:ASPxCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style6">
                            &nbsp;</td>
                        <td class="style8">
                            &nbsp;</td>
                        <td class="style10">
                            &nbsp;</td>
                        <td class="style12">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                <dx:ASPxGridView ID="GridExistencias" ClientInstanceName="GridExistencias" onhtmldatacellprepared="gv_HtmlDataCellPrepared"
                    runat="server" AutoGenerateColumns="False" meta:resourcekey="GridExistencias"
                    Theme="Office2010Blue" Width="100%" 
                    OnCustomUnboundColumnData ="GridExistencias_CustomUnboundColumnData" 
                    onsummarydisplaytext="ASPxGridView1_SummaryDisplayText">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Almacén" VisibleIndex="1" 
                            FieldName="Almacen" Width="60px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ID" VisibleIndex="2" FieldName="ID"
                            Width="90px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Artículo" VisibleIndex="3"
                            FieldName="Articulo" Width="200px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Familia" VisibleIndex="4"
                            FieldName="Familia" Width="170px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Stock real" VisibleIndex="5"
                            FieldName="StockReal" Width="60px">
                             <PropertiesTextEdit DisplayFormatString="f3"></PropertiesTextEdit>
                             <HeaderStyle HorizontalAlign="Center" />
                            <FooterCellStyle Font-Bold="True" ForeColor="Brown"></FooterCellStyle>
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Precio (€)" FieldName="Precio"
                            Name="Precio" VisibleIndex="11" Visible="False">
                             <PropertiesTextEdit DisplayFormatString="f2"></PropertiesTextEdit>
                             <HeaderStyle HorizontalAlign="Center" />
                             <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn UnboundType="decimal" FieldName="Importe"
                           VisibleIndex="12" Visible="False">
                            <PropertiesTextEdit DisplayFormatString="f2"></PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Center" />
                            <FooterCellStyle Font-Bold="True" ForeColor="Brown"></FooterCellStyle>
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Precio (€)" FieldName="PrecioCosteReal"
                            Name="PrecioCosteReal" Visible="False" VisibleIndex="10">
                             <PropertiesTextEdit DisplayFormatString="f2"></PropertiesTextEdit>
                             <HeaderStyle HorizontalAlign="Center" />
                             <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Pte.Servir"
                            FieldName="PendienteServir" VisibleIndex="7" Visible="False">
                            <HeaderStyle HorizontalAlign="Center" />
                            <PropertiesTextEdit DisplayFormatString="f3"></PropertiesTextEdit>
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Pte.Recibir"
                            FieldName="PendienteRecibir" VisibleIndex="6" Visible="False">
                            <HeaderStyle HorizontalAlign="Center" />
                            <PropertiesTextEdit DisplayFormatString="f3"></PropertiesTextEdit>
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Disponible" FieldName="StockDisponible"
                            VisibleIndex="9" Visible="False">
                            <HeaderStyle HorizontalAlign="Center" />
                            <PropertiesTextEdit DisplayFormatString="f3"></PropertiesTextEdit>
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Reservado" FieldName="StockReservado"
                            VisibleIndex="8" Visible="False">
                            <HeaderStyle HorizontalAlign="Center" />
                            <PropertiesTextEdit DisplayFormatString="f3"></PropertiesTextEdit>
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager PageSize="20" AlwaysShowPager="True" >
            <PageSizeItemSettings Visible="True">
            </PageSizeItemSettings>
         </SettingsPager>
            <Settings ShowFilterRow="True" GridLines="Vertical" />
            <Settings  ShowFooter="True" />
            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" 
                AllowInsert="False" />

<Settings ShowFilterRow="True" ShowFooter="True" GridLines="Vertical"></Settings>

<SettingsDataSecurity AllowInsert="False" AllowEdit="False" AllowDelete="False"></SettingsDataSecurity>

             <Styles>
            <AlternatingRow Enabled="True"/>
<AlternatingRow Enabled="True"></AlternatingRow>
         </Styles>
         <TotalSummary>
            <dx:ASPxSummaryItem FieldName="StockReal" SummaryType="Sum" />
            <dx:ASPxSummaryItem FieldName="Importe" SummaryType="Sum" />
        </TotalSummary>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
     <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="GridExistencias" Landscape="true" LeftMargin="3" RightMargin="3">
     </dx:ASPxGridViewExporter>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
