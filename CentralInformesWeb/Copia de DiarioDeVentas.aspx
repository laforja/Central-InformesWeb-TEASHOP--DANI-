<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DiarioDeVentas1.aspx.vb" Inherits="CentralInformesWeb.DiarioDeVentas" %>

<%@ Register assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>






<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style3
        {
            width: 138px;
        }
        .style4
        {
            width: 38px;
        }
        .style6
        {
            width: 54px;
        }
        .style7
        {
            width: 51px;
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
            width: 8px;
        }
        .style14
        {
            width: 731px;
        }
    </style>
    <script type="text/javascript">
        function checkDate(s, e) {
            var fromDate = new Date('09/01/2007');
           var selectedDate = new Date(e.value);
            var toDate = new Date('12/31/9999');

            if (s.GetValue() == null) {
                s.SetValue(new Date());
                selectedDate = new Date();
                e.IsValid = true;
            }
           if (selectedDate.getTime() > toDate.getTime() || selectedDate.getTime() < fromDate.getTime()) {
                e.isValid = false;
                e.errorText = 'Enter a valid date';

            }
        }

        function compareDates(s, e) {
            if (cmbFechaHasta.GetValue() < cmbFechaDesde.GetValue()) {
                cmbFechaHasta.SetIsValid(false);
                cmbFechaHasta.SetErrorText('End Date should be greater than Start Date.');
                e.processOnServer = false;
            }
        
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
        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Diario de Ventas" 
            Font-Bold="True" Font-Size="Small" ForeColor="Black">
    </dx:ASPxLabel>      
                </td>
                <td>
                    <dx:ASPxMenu ID="ASPxMenu2" runat="server" OnItemClick="ASPxMenu2_ItemClick" 
                        Theme="Office2010Blue">
                        <Items>
                            <dx:MenuItem DropDownMode="True" Text="Exportar a:">
                                <Items>
                                    <dx:MenuItem Name="Excel" Text="Excel">
                                        <Image Url="~/Images/ExportToXLS_32x32.png">
                                        </Image>
                                    </dx:MenuItem>
                                    <dx:MenuItem Name="Pdf" Text="Pdf">
                                        <Image Url="~/Images/ExportToPDF_32x32.png">
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
                <td class="style13">
                    &nbsp;</td>
                <td class="style6">
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Desde:">
                    </dx:ASPxLabel>
                </td>
                <td class="style3">
                    <dx:ASPxDateEdit ID="cmbFechaDesde" ClientInstanceName="cmbFechaDesde" UseMaskBehavior="true" EditFormat="Custom" EditFormatString="dd/MM/yyyy" runat="server" Theme="Office2010Blue" AutoPostBack="true">
                        <ValidationSettings ValidationGroup="myGroup" ErrorDisplayMode="ImageWithTooltip">
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
                </td>
                <td class="style4">
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Hasta:">
                    </dx:ASPxLabel>
                </td>
                <td class="style7">
                    <dx:ASPxDateEdit ID="cmbFechaHasta" ClientInstanceName="cmbFechaHasta" UseMaskBehavior="true" EditFormat="Custom" EditFormatString="dd/MM/yyyy" runat="server" Theme="Office2010Blue" AutoPostBack="true">
                        <ValidationSettings ValidationGroup="myGroup" ErrorDisplayMode="ImageWithTooltip">
                        </ValidationSettings>
                    </dx:ASPxDateEdit>
                </td>
                <td class="style9">
                    <dx:ASPxButton ID="btnFiltrar" runat="server" 
                        OnClick="btnFiltrar_Click" ValidationGroup="myGroup" UseSubmitBehavior="false"
                        Theme="Office2010Blue" Width="38px" VerticalAlign="Middle">
                        
                        <Image Url="~/Images/Search.png">
                        </Image>
                    </dx:ASPxButton>
                </td>
                <td class="style8">
                    &nbsp;</td>
            </tr>
    
    </table>
    
           </p>
       
           <dx:ASPxGridView ID="GridDiarioVentas" runat="server" ClientInstanceName="grid"
            AutoGenerateColumns="False" Width="927px" Theme="Office2010Blue" onsummarydisplaytext="ASPxGridView1_SummaryDisplayText">
            <TotalSummary>
                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="Importe"></dx:ASPxSummaryItem>
                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="IVA"></dx:ASPxSummaryItem>
                <dx:ASPxSummaryItem SummaryType="Sum" FieldName="Total"></dx:ASPxSummaryItem>
            </TotalSummary>
            <Columns>
                <dx:GridViewDataTextColumn Caption="Tienda" Name="Tienda" VisibleIndex="1" 
                    Width="230px" FieldName="Nombre">
                    <EditCellStyle HorizontalAlign="Center">
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Caja" Name="Caja" VisibleIndex="2" 
                    FieldName="Caja">
                    <EditCellStyle HorizontalAlign="Center">
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Fecha" Name="Fecha" VisibleIndex="3" Width="150px"
                    FieldName="Fecha">
                    <EditCellStyle HorizontalAlign="Center">
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="ID Concepto" Name="IdConcepto" 
                    VisibleIndex="4" FieldName="ConceptoId">
                    <EditCellStyle HorizontalAlign="Center">
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Center" />
                    <CellStyle HorizontalAlign="Center">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Ticket" Name="Ticket" VisibleIndex="5" 
                    FieldName="Ticket">
                    <EditCellStyle HorizontalAlign="Center">
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Ventas (€)" 
                    Name="Ventas" VisibleIndex="7" FieldName="Importe">
                    <EditCellStyle HorizontalAlign="Center">
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Center" />
                    <FooterCellStyle ForeColor="Brown" Font-Bold=true  />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Impuestos (€)" Name="Impuestos" VisibleIndex="8" 
                    FieldName="IVA">
                    <EditCellStyle HorizontalAlign="Center">
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Center" />
                    <FooterCellStyle ForeColor="Brown" Font-Bold=true  />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Ventas Iva Incluido (€)" Name="VentasIvaIncluido" 
                    VisibleIndex="9" FieldName="Total">
                    <EditCellStyle HorizontalAlign="Center">
                    </EditCellStyle>
                    <HeaderStyle HorizontalAlign="Center" />
                    <FooterCellStyle ForeColor="Brown" Font-Bold=true  />
                     <PropertiesTextEdit DisplayFormatString="f2"></PropertiesTextEdit>
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
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="GridDiarioVentas">
            </dx:ASPxGridViewExporter>
</asp:Content>

