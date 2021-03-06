﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ExistenciasPorFecha.aspx.vb" Inherits="CentralInformesWeb.ExistenciasPorFecha" %>

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
        .style18
        {
            height: 6px;
            width: 57px;
        }
        .style19
        {
            height: 6px;
            width: 149px;
        }
        .style20
        {
            width: 149px;
        }
        .style21
        {
            height: 6px;
            width: 173px;
        }
        .style22
        {
            width: 173px;
        }
        .style24
        {
            width: 144px;
        }
        .style25
        {
            height: 6px;
            width: 144px;
        }
        .style26
        {
            height: 6px;
            width: 163px;
        }
        .style27
        {
            height: 6px;
            width: 197px;
        }
        .style28
        {
            width: 197px;
        }
    </style>
    <script type="text/javascript">
      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    </dx:ASPxCallback>
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
                    ForeColor="Black" Text="Existencias por fecha" meta:resourcekey="ASPxLabel1">
                </dx:ASPxLabel>
            </td>
            <td class="style17">
                <dx:ASPxMenu ID="ASPxMenu2" runat="server" Theme="Office2010Blue" 
                    OnItemClick="ASPxMenu2_ItemClick">
                    <Items>
                        <dx:MenuItem Text="Exportar a:" DropDownMode="True" >
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
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Almacén:" 
                                Theme="Office2010Blue" meta:resourcekey="ASPxLabel2">
                            </dx:ASPxLabel>
                        </td>
                        <td class="style25">
                            <dx:ASPxComboBox ID="CmbAlmacen" runat="server" Enabled="False" 
                                Height="16px" style="margin-bottom: 0px" 
                                Width="86px" OnInit="cmbAlmacen_Init"
                                Theme="Office2010Blue" AutoPostBack="False" EnableCallbackMode="False" 
                                PopupHorizontalAlign="NotSet">
                            </dx:ASPxComboBox>
                        </td>
                        <td class="style18">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Fecha:" 
                                Theme="Office2010Blue" meta:resourcekey="ASPxLabel2">
                            </dx:ASPxLabel>
                        </td>
                        <td class="style26">
                            <dx:ASPxDateEdit ID="ASPxFecha" runat="server" Theme="Office2010Blue" 
                                Width="120px">
                            </dx:ASPxDateEdit>
                        </td>
                        <td class="style21">
                            <dx:ASPxCheckBox ID="chkStock" runat="server" Text="Art. con stock" 
                                Theme="Office2010Blue" ToolTip="Muestra sólo artículos con stock" 
                                Checked="True" CheckState="Checked">
                            </dx:ASPxCheckBox>
                        </td>
                        <td class="style27">
                            <dx:ASPxCheckBox ID="chkAgrupar" runat="server" Text="Agrupar por família" 
                                Theme="Office2010Blue" ToolTip="Muestra totales por família">
                            </dx:ASPxCheckBox>
                        </td>
                        <td class="style19">
                            <dx:ASPxCheckBox ID="chkValorado" runat="server" Text="Valorado" 
                                Theme="Office2010Blue" ToolTip="Valorado a precio coste real">
                            </dx:ASPxCheckBox>
                        </td>
                        <td class="style4">
                            <dx:ASPxButton ID="btnFiltrar" runat="server" AutoPostBack ="False" UseSubmitBehavior="false"
                        Theme="Office2010Blue" Width="80px" VerticalAlign="Top" Text="Filtrar">
                        <Image Url="~/Images/view.png">
                        </Image>
                        <ClientSideEvents Click="function(s, e) {
                        GridExistencias.PerformCallback();
                        }" />
                    </dx:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="style6">
                            &nbsp;</td>
                        <td class="style8">
                            &nbsp;</td>
                        <td class="style24">
                            &nbsp;</td>
                        <td class="style12" colspan="2">
                            &nbsp;</td>
                        <td class="style22">
                            &nbsp;</td>
                        <td class="style28">
                            &nbsp;</td>
                        <td class="style20">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                <dx:ASPxGridView ID="GridExistencias" ClientInstanceName="GridExistencias" 
                    onhtmldatacellprepared="gv_HtmlDataCellPrepared" OnDataBinding="GridExistencias_DataBinding" OnInit="GridExistencias_Init" OnCustomUnboundColumnData="GridExistencias_CustomUnboundColumnData"  OnCustomCallback="GridExistencias_CustomCallBack" 
                    runat="server" AutoGenerateColumns="False"
                    Theme="Office2010Blue" Width="100%" 
                    onsummarydisplaytext="ASPxGridView1_SummaryDisplayText">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Almacén" VisibleIndex="0" 
                            FieldName="Almacen" Width="60px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Cód.Art." VisibleIndex="1" FieldName="Id"
                            Width="90px" SortIndex="0" SortOrder="Ascending">
                            <HeaderStyle HorizontalAlign="Center" />
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Descripción" VisibleIndex="2"
                            FieldName="Articulo" Width="250px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Familia" VisibleIndex="3"
                            FieldName="Familia" Width="170px">
                            <HeaderStyle HorizontalAlign="Center" />
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Stock real" VisibleIndex="4"
                            FieldName="StockReal" Width="60px" UnboundType="Decimal">
                             <PropertiesTextEdit DisplayFormatString="f3" NullText="0">
                                 <MaskSettings Mask="f3" />
                             </PropertiesTextEdit>
                             <HeaderStyle HorizontalAlign="Center" />
                             <CellStyle HorizontalAlign="Right">
                             </CellStyle>
                            <FooterCellStyle Font-Bold="True" ForeColor="Brown"></FooterCellStyle>
                            <Settings AutoFilterCondition="Contains" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Precio (€)" FieldName="Precio"  
                            Width="60px"
                            Name="Precio" VisibleIndex="5">
                             <PropertiesTextEdit DisplayFormatString="c4" NullText="0"></PropertiesTextEdit>
                             <HeaderStyle HorizontalAlign="Center" />
                             <FooterCellStyle Font-Bold="True" ForeColor="Brown"></FooterCellStyle>
                             <Settings AutoFilterCondition="Contains" />
                             <CellStyle HorizontalAlign="Right">
                             </CellStyle>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager PageSize="20" AlwaysShowPager="True">
            <PageSizeItemSettings Visible="True">
            </PageSizeItemSettings>
         </SettingsPager>
             <GroupSummary>
                <dx:ASPxSummaryItem FieldName="StockReal" ShowInGroupFooterColumn="StockReal" SummaryType="Sum"/>
                <dx:ASPxSummaryItem FieldName="Total" ShowInGroupFooterColumn="Total" SummaryType="Sum" />
                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="StockReal"></dx:ASPxSummaryItem>
                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="Total"></dx:ASPxSummaryItem>
            </GroupSummary>

          <Settings ShowFooter="True" GridLines="Vertical" ShowFilterRow="True"></Settings>
          <SettingsDataSecurity AllowInsert="False" AllowEdit="False" AllowDelete="False"></SettingsDataSecurity>

          <Styles>
            <AlternatingRow Enabled="True"/>
         </Styles>
         <TotalSummary>
            <dx:ASPxSummaryItem FieldName="StockReal" SummaryType="Sum" />
            <dx:ASPxSummaryItem FieldName="Precio" SummaryType="Sum" />
            <dx:ASPxSummaryItem FieldName="PrecioCosteReal" SummaryType="Sum" />
            <dx:ASPxSummaryItem FieldName="Total" SummaryType="Sum" />
        </TotalSummary>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
    <dx:ASPXLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="True"></dx:ASPXLoadingPanel>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="GridExistencias" ReportHeader="{\rtf1\ansi\ansicpg1252\deff0\deflang3082{\fonttbl{\f0\fnil\fcharset0 Times New Roman;}}
\viewkind4\uc1\pard\qc\b\f0\fs40 Listado Existencias por fecha\par
\par
\b0\fs20\par
}
" PaperKind="A4">
                        <Styles>
                            <AlternatingRowCell BackColor="#E2DDDC">
                            </AlternatingRowCell>
                        </Styles>
                        <PageHeader Right="[Date Printed]">
                        </PageHeader>
                        <PageFooter Left="[Page # of Pages #]">
                        </PageFooter>
                </dx:ASPxGridViewExporter>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
