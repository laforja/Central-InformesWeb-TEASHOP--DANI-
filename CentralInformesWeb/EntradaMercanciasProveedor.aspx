<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EntradaMercanciasProveedor.aspx.vb" Inherits="CentralInformesWeb.EntradaMercanciasProveedor" %>

<%@ Register assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>







<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <style type="text/css">
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
        .style14
        {
            width: 731px;
        }
        .style16
        {
            }
    </style>
     <script language="javascript" type="text/javascript">
         function OnSelectedIndexChanged(s, e, txt) {
             var text = s.GetText();
             txt.SetText(text);
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
            ImageUrl="~/Images/box_preferences32.png" ShowLoadingImage="true" 
                        style="margin-left: 0px">  
        </dx:ASPxImage>
                </td>
                <td class="style14">
        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Entrada mercancías de proveedor" meta:resourcekey="ASPxLabel3"
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
        </table>

        <table style="width:100%;">
        
            <tr>
                <td class="style16">
                <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Add New Row" 
            onclick="ASPxButton1_Click">
        </dx:ASPxButton>
                    <dx:ASPxGridView ID="GridPedidos" runat="server" AutoGenerateColumns="False" Theme="Office2010Blue" KeyFieldName="CodigoDeArticulo" OnCellEditorInitialize="GridPedidos_CellEditorInitialize" SettingsBehavior-AllowDragDrop="False">
                        <Columns>
                            <dx:GridViewCommandColumn ShowNewButtonInHeader="True" VisibleIndex="0">
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="Descripcion" VisibleIndex="15" 
                                Visible="False">
                                <DataItemTemplate>
                                    <dx:ASPxTextBox ID="txtDescArt" runat="server" Theme="Office2010Blue" OnInit="txtDescArt_Init" Width="220px">
                                    </dx:ASPxTextBox>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataComboBoxColumn Caption="Proveedor" VisibleIndex="16" 
                                Width="120px">
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataTextColumn Caption="Cantidad" VisibleIndex="17">
                                 <PropertiesTextEdit DisplayFormatString="f3"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Precio coste (€)" VisibleIndex="18">
                                <PropertiesTextEdit DisplayFormatString="n2"></PropertiesTextEdit>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataComboBoxColumn Caption="Almacén" VisibleIndex="19" 
                                Width="100px">
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataDateColumn Caption="Fecha" VisibleIndex="20" Width="85px">
                                <PropertiesDateEdit DisplayFormatString="d">
                                </PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataComboBoxColumn Caption="Artículo" FieldName="CodigoDeArticulo" 
                                VisibleIndex="14" Width="400px">
                                <PropertiesComboBox>
                                    <Columns>
                                        <dx:ListBoxColumn Caption="ID" FieldName="CodigoDeArticulo" />
                                        <dx:ListBoxColumn Caption="Descripción" FieldName="Descripcion" />
                                    </Columns>
                                </PropertiesComboBox>
                            </dx:GridViewDataComboBoxColumn>
                        </Columns>
                        <settingsbehavior AllowDragDrop="False" />
                        <SettingsEditing newitemrowposition="Bottom" Mode="Batch"></SettingsEditing>
                        <Settings GridLines="Vertical" />
                    </dx:ASPxGridView>
                </td>
            </tr>
      </table>
         
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
