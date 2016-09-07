<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DiarioFacturacionResumido.aspx.vb" Inherits="CentralInformesWeb.DiarioFacturacionResumido" %>

<%@ Register assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>






<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style3
        {
            width: 193px;
        }
        .style6
        {
            width: 54px;
        }
        .style8
        {
            width: 122px;
        }
        .style9
        {
            width: 294px;
        }
        .style10
        {
        }
        .style11
        {
            width: 39px;
        }
        .style12
        {
            width: 5px;
        }
        .style13
        {
            width: 75px;
        }
        .style14
        {
            width: 731px;
        }
        .style15
        {
            width: 183px;
        }
        .style16
        {
            width: 46px;
        }
        .style17
        {
            width: 60px;
        }
        .style18
        {
            width: 88px;
        }
    </style>
    <script type="text/javascript">
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <table style="width:100%;">
            <tr>
                <td class="style12">
                    &nbsp;</td>
                <td class="style11">
<dx:ASPxImage ID="ASPxImage1" runat="server" 
            ImageUrl="~/Images/document_chart32.png" ShowLoadingImage="true" 
                        style="margin-left: 0px">  
        </dx:ASPxImage>
                </td>
                <td class="style14">
        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Diario de facturación (resumido)" meta:resourcekey="ASPxLabel3"
            Font-Bold="True" Font-Size="Small" ForeColor="Black">
    </dx:ASPxLabel>      
                </td>
                <td>
                    <dx:ASPxMenu ID="ASPxMenu2" runat="server" OnItemClick="ASPxMenu2_ItemClick" 
                        Theme="Office2010Blue">
                        <Items>
                            <dx:MenuItem DropDownMode="True" Text="Exportar a:">
                                <Items>
                                    <dx:MenuItem Name="Pdf" Text="Pdf">
                                        <Image Url="~/Images/ExportToPDF_32x32.png">
                                        </Image>
                                    </dx:MenuItem>
                                    <dx:MenuItem Name="Excel" Text="Excel">
                                        <Image Url="~/Images/ExportToXLS_32x32.png">
                                        </Image>
                                    </dx:MenuItem>
                                </Items>
                            </dx:MenuItem>
                        </Items>
                    </dx:ASPxMenu>
                </td>
            </tr>
            <tr>
                <td class="style10" colspan="4">
                    <hr style="color: #1E395B" />
                </td>
            </tr>
        </table>

         &nbsp;
    <table style="width:100%;">
        
            <tr>
                <td>
                    &nbsp;</td>
                <td class="style13">
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Fecha:" meta:resourcekey="ASPxLabel4">
                    </dx:ASPxLabel>
                </td>
                <td class="style15">
                    <dx:ASPxComboBox ID="cmbFecha" runat="server" EnableTheming="True" meta:resourcekey="cmbFecha"
                        OnValueChanged="cmbFecha_Valuechanged" AutoPostBack="true"
                        SelectedIndex="0" Theme="Office2010Blue" DropDownRows="9">
                        <Items>
                            <dx:ListEditItem Selected="True" Text="Hoy" Value="Hoy" meta:resourcekey="Hoy" />
                            <dx:ListEditItem Text="Día anterior" Value="Día anterior" meta:resourcekey="Ayer"/>
                            <dx:ListEditItem Text="Semana actual" Value="Semana actual" meta:resourcekey="SemanaActual"/>
                            <dx:ListEditItem Text="Mes actual" Value="Mes actual" meta:resourcekey="MesActual"/>
                            <dx:ListEditItem Text="Año actual" Value="Año actual" meta:resourcekey="AñoActual"/>
                            <dx:ListEditItem Text="Última semana" Value="Última semana" meta:resourcekey="UltimaSemana"/>
                            <dx:ListEditItem Text="Último mes" Value="Último mes" meta:resourcekey="UltimoMes"/>
                            <dx:ListEditItem Text="Último año" Value="Último año" meta:resourcekey="UltimoAño"/>
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <td class="style6">
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Desde:" meta:resourcekey="ASPxLabel1">
                    </dx:ASPxLabel>
                </td>
                <td class="style3">
                    <dx:ASPxDateEdit ID="cmbFechaDesde" ClientInstanceName="cmbFechaDesde" UseMaskBehavior="true" EditFormat="Custom" EditFormatString="dd/MM/yyyy" runat="server" Theme="Office2010Blue" OnInit="cmbfechaDesde_Init" OnValueChanged="cmbFechaDesde_Valuechanged"
                                EnableCallbackMode="True" AutoPostBack="True">
                    </dx:ASPxDateEdit>
                </td>
                <td class="style16">
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Hasta:" meta:resourcekey="ASPxLabel2">
                    </dx:ASPxLabel>
                </td>
                <td class="style18">
                    <dx:ASPxDateEdit ID="cmbFechaHasta" ClientInstanceName="cmbFechaHasta" UseMaskBehavior="true" EditFormat="Custom" EditFormatString="dd/MM/yyyy" runat="server" Theme="Office2010Blue" OnInit="cmbfechaDesde_Init" OnValueChanged="cmbFechaHasta_Valuechanged"
                                EnableCallbackMode="True" AutoPostBack="True">
                    </dx:ASPxDateEdit>
                </td>
                <td class="style17">
                    Vendedor:</td>
                <td class="style9">
                    <dx:ASPxComboBox ID="cmbVendedor" runat="server" OnInit="cmbVendedor_Init"
                                Theme="Office2010Blue" AutoPostBack="True"  
                                EnableCallbackMode="True" PopupHorizontalAlign="NotSet" 
                        ValueType="System.String">
                    </dx:ASPxComboBox>
                </td>
                <td class="style8">
                    <dx:ASPxButton ID="btnFiltrar" runat="server" 
                        UseSubmitBehavior="false" AutoPostBack ="false"
                        Theme="Office2010Blue" Width="78px" VerticalAlign="Top" Text="Filtrar">
                        <Image Url="~/Images/view.png">
                        </Image>
                        <ClientSideEvents Click="function(s, e) {GridDiarioVentas.PerformCallback();}" />
                    </dx:ASPxButton>
                </td>
            </tr>
      </table>
        </p>
                 <dx:ASPxGridView ID="GridDiarioVentas" runat="server" ClientInstanceName="GridDiarioVentas"  OnDataBinding="GridDiarioVentas_DataBinding" oncustomcallback="GridDiarioVentas_CustomCallback"
            AutoGenerateColumns="False" Width="100%" Theme="Office2010Blue" onsummarydisplaytext="ASPxGridView1_SummaryDisplayText" onhtmldatacellprepared="gv_HtmlDataCellPrepared" meta:resourcekey="GridDiarioVentas">
            <TotalSummary>
                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="Importe"></dx:ASPxSummaryItem>
                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="IVA"></dx:ASPxSummaryItem>
                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="Total"></dx:ASPxSummaryItem>
            </TotalSummary>
            <Columns>
                <dx:GridViewDataTextColumn Caption="Base 21%" Name="Base21" VisibleIndex="2" 
                    Width="150px" FieldName="Base21" meta:resourcekey="Tienda">
                    <EditCellStyle HorizontalAlign="Center">
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Center" />
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Iva 21%" Name="Iva21" VisibleIndex="3" meta:resourcekey="Caja"
                    FieldName="Iva21" Width="150px">
                    <EditCellStyle HorizontalAlign="Center">
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Fecha" Name="Fecha" VisibleIndex="0" 
                    Width="150px" meta:resourcekey="Fecha"
                    FieldName="FechaFactura">
                    <PropertiesTextEdit DisplayFormatString="d"></PropertiesTextEdit>
                     <HeaderStyle HorizontalAlign="Center" />
                    <EditCellStyle HorizontalAlign="Center">
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Center" />
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Base 10%" Name="Base10" meta:resourcekey="IdConcepto"
                    VisibleIndex="4" FieldName="Base10" Width="150px">
                    <EditCellStyle HorizontalAlign="Center">
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Iva 10%" Name="Iva10" VisibleIndex="5" meta:resourcekey="Ticket"
                    FieldName="Iva10" Width="150px">
                    <EditCellStyle HorizontalAlign="Center">
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Center" />
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Base 4%" meta:resourcekey="Ventas"
                    Name="Base4" VisibleIndex="6" FieldName="Base4" Width="150px">
                    <EditCellStyle HorizontalAlign="Center">
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Center" />
                    <FooterCellStyle ForeColor="Brown" Font-Bold="true"  />
                    <Settings AutoFilterCondition="Contains" />
                    <PropertiesTextEdit DisplayFormatString="n2"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Iva 4%" Name="Iva4" VisibleIndex="7" meta:resourcekey="Impuestos"
                    FieldName="Iva4" Width="150px">
                    <EditCellStyle HorizontalAlign="Center">
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Center" />
                    <FooterCellStyle ForeColor="Brown" Font-Bold="true"  />
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Vendedor" FieldName="Nombre" 
                    Name="Vendedor" VisibleIndex="1" Width="200px">
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
             <Styles>
            <AlternatingRow Enabled="True"/>
         </Styles>
        </dx:ASPxGridView> 
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="GridDiarioVentas" LeftMargin="5" RightMargin="5" >
    </dx:ASPxGridViewExporter>
</asp:Content>

