<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PedidosDeReposicion.aspx.vb" Inherits="CentralInformesWeb.PedidosDeReposicion" %>
<%@ Register assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
        }
        .style4
        {
            width: 65px;
        }
        .style5
        {
            width: 249px;
        }
        .style6
        {
            width: 4px;
        }
        .style7
        {
            width: 197px;
        }
        .style8
        {
            width: 105px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <table style="width:100%;">
        <tr>
            <td class="style6">
                <dx:ASPxImage ID="ASPxImage1" runat="server" ShowLoadingImage="true" 
                    ImageUrl="~/Images/desktop.png" >
                </dx:ASPxImage>
                </td>
            <td class="style14">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Size="Small" meta:resourcekey="ASPxLabel1"
                    ForeColor="Black" Text="Pedidos de reposición">
                </dx:ASPxLabel>
        <tr>
            <td class="style1" colspan="2">
               
                <hr style="color: #435463" />
               
                </td>
            <tr>
            <td class="style1" colspan="2">
               
                <table style="width:100%;">
                    <tr>
                        <td class="style4">
                            &nbsp;</td>
                        <td class="style5">
                            <dx:ASPxCheckBox ID="chkPrt" runat="server" CheckState="Checked" EnableTheming="True" meta:resourcekey="chkPrt"
                                Text="Pedido de reposición de tienda" Theme="Office2010Blue"  AutoPostBack="True" EnableCallbackMode="True" OnCheckedChanged ="chkPrt_CheckedChanged">
                            </dx:ASPxCheckBox>
                        </td>
                        <td class="style7" style="text-align: right">
                           
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Mostrar documentos:" meta:resourcekey="ASPxLabel2"
                                Theme="Office2010Blue">
                            </dx:ASPxLabel>
                           
                        </td>
                        <td class="style8">
                           
                            <dx:ASPxCheckBox ID="chkFinalizados" runat="server" CheckState="Unchecked" EnableTheming="True" 
                                Text="Finalizados" Theme="Office2010Blue"  AutoPostBack="True" meta:resourcekey="chkFinalizados"
                                EnableCallbackMode="True" 
                                OnCheckedChanged ="chkFinalizados_CheckedChanged">
                            </dx:ASPxCheckBox>
                           
                        </td>
                        <td>
                           
                            <dx:ASPxCheckBox ID="chkAnulados" runat="server" CheckState="Unchecked" EnableTheming="True" 
                                Text="Anulados" Theme="Office2010Blue"  AutoPostBack="True" 
                                EnableCallbackMode="True" meta:resourcekey="chkAnulados"
                                OnCheckedChanged ="chkAnulados_CheckedChanged">
                            </dx:ASPxCheckBox>
                           
                        </td>
                    </tr>
                </table>
               
                </td>
            </table>
           <dx:ASPxGridView ID="GridPedidos" runat="server" onsummarydisplaytext="ASPxGridView1_SummaryDisplayText" KeyFieldName="Año;Serie;Numero"
         AutoGenerateColumns="False" Width="100%"  OnDataBinding="GridPedidos_DataBinding" oncustomcallback="GridPedidos_CustomCallback"
                    EnableTheming="True" Theme="Office2010Blue" meta:resourcekey="GridPedidos">
            <Settings  ShowFooter="True" />
             <Images>
                <DetailCollapsedButton Width="16px" Url="Images/Expand.png">
                </DetailCollapsedButton>
                <DetailExpandedButton Width="16px" Url="Images/Collapse.png">
                </DetailExpandedButton>
            </Images>
<Settings ShowFilterRow="True" ShowFooter="True" GridLines="Vertical"></Settings>

<SettingsDataSecurity AllowInsert="False" AllowEdit="False" AllowDelete="False"></SettingsDataSecurity>

         <TotalSummary>
            <dx:ASPxSummaryItem FieldName="Total" SummaryType="Sum" />
        </TotalSummary>
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Tienda" VisibleIndex="0" FieldName="Tienda" meta:resourcekey="Tienda"
                            Width="180px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Documento" VisibleIndex="1" meta:resourcekey="Documento"
                            FieldName="Documento" Width="80px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Fecha" VisibleIndex="2" meta:resourcekey="Fecha"
                            FieldName="FechaDocumento" Width="100px">
                            <PropertiesTextEdit DisplayFormatString="d"></PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Center" />
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Cliente" VisibleIndex="3" meta:resourcekey="Cliente"
                            FieldName="Cliente" Width="200px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Situación" VisibleIndex="5" meta:resourcekey="Situacion"
                            FieldName="Situacion" Width="50px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Total (€)" VisibleIndex="6" meta:resourcekey="Total"
                            FieldName="Total" Width="70px">
                            <PropertiesTextEdit DisplayFormatString="f2"></PropertiesTextEdit>
                            <HeaderStyle HorizontalAlign="Center" />
                            <FooterCellStyle Font-Bold="True" ForeColor="Brown"></FooterCellStyle>
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Observaciones" VisibleIndex="7" meta:resourcekey="Observaciones"
                            FieldName="Observaciones">
                            <HeaderStyle HorizontalAlign="Center" />
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Su pedido" FieldName="SuPedido" meta:resourcekey="SuPedido"
                            Name="Su pedido" VisibleIndex="4" Width="90px">
                             <HeaderStyle HorizontalAlign="Center" />
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager PageSize="20" AlwaysShowPager="True" >
            <PageSizeItemSettings Visible="True">
            </PageSizeItemSettings>
         </SettingsPager>
            <Settings ShowFilterRow="True" GridLines="Vertical" />
                    <SettingsDetail ShowDetailRow="True" />
            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" 
                AllowInsert="False" />

             <Styles>
            <AlternatingRow Enabled="True"/>
<AlternatingRow Enabled="True"></AlternatingRow>
         </Styles>
                    <Templates>
                        <DetailRow>
                            <dx:ASPxGridView ID="GridDetallePedido" runat="server" KeyFieldName="Año;Serie;Numero" OnBeforePerformDataSelect="GridDetallePedido_BeforePerformDataSelect"
                                AutoGenerateColumns="False" EnableTheming="True" Theme="Office2010Blue" Width="100%" onsummarydisplaytext="GridDetallePedido_SummaryDisplayText" meta:resourcekey="GridDetallePedido">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Artículo" FieldName="CodigoDeArticulo" meta:resourcekey="CodigoDeArticulo"
                                        Name="Artículo" VisibleIndex="0" Width="110px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Descripción" FieldName="Descripcion" meta:resourcekey="Descripcion"
                                        Name="Descripción" VisibleIndex="1" Width="220px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Cantidad" FieldName="Cantidad" meta:resourcekey="Cantidad"
                                        Name="Cantidad" VisibleIndex="2" Width="40px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <FooterCellStyle Font-Bold="True" ForeColor="Brown"></FooterCellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Cantidad servida" meta:resourcekey="CantidadServida"
                                        FieldName="CantidadServida" Name="Cantidad servida" VisibleIndex="3">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Pdte.Servir" FieldName="CantidadPteServir" meta:resourcekey="CantidadPteServir"
                                        Name="Pdte.Servir" VisibleIndex="4">
                                        <FooterCellStyle Font-Bold="True" ForeColor="Brown"></FooterCellStyle>
                                        <PropertiesTextEdit DisplayFormatString="f3"></PropertiesTextEdit>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Precio" FieldName="PrecioVenta" meta:resourcekey="PrecioVenta"
                                        Name="Precio" VisibleIndex="5" Width="75px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Dto." FieldName="PorDescuento" Name="Dto." meta:resourcekey="PorDescuento"
                                        VisibleIndex="6" Width="40px">
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Importe" FieldName="Importe" Name="Importe" UnboundType="Decimal" meta:resourcekey="Importe"
                                        VisibleIndex="7" Width="80px">
                                         <PropertiesTextEdit DisplayFormatString="c" />
                                          <HeaderStyle HorizontalAlign="Center" />
                                          <FooterCellStyle Font-Bold="True" ForeColor="Brown"></FooterCellStyle>
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Settings ShowFooter="True" />
                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="Cantidad" SummaryType="Sum" />
                                    <dx:ASPxSummaryItem FieldName="Importe" SummaryType="Sum" />
                                </TotalSummary> 
                                 <Styles>
                                    <AlternatingRow Enabled="True"></AlternatingRow>
                                 </Styles>
                                <SettingsDetail IsDetailGrid="True" />
                                <SettingsPager Visible="False">
                                </SettingsPager>
                                <Settings GridLines="Vertical" />
                                <SettingsDataSecurity AllowDelete="False" AllowEdit="False" 
                                    AllowInsert="False" />
                            </dx:ASPxGridView>
                        </DetailRow>
                    </Templates>
                </dx:ASPxGridView>
    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
