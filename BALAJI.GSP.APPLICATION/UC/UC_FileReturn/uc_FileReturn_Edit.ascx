<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_FileReturn_Edit.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.UC_FileReturn.uc_FileReturn_Edit" %>


<asp:GridView ID="gvInvoice_Edit" CssClass="table table-responsive no-padding table-striped table-bordered table-condensed" AutoGenerateColumns="false" runat="server">
    <Columns>
        <asp:TemplateField HeaderText="#" Visible="false">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Receiver GSTIN" ItemStyle-Width="10%">
            <ItemTemplate>
                <asp:TextBox ID="txtReceivergstn" Text='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.ReceiverUserID") %>' AutoPostBack="true"  runat="server"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Invoice No." ItemStyle-Width="30%">
            <ItemTemplate>
                <asp:TextBox ID="txtinvoiceno"  CssClass="form-control input-sm" ReadOnly="true" runat="server"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Invoice Date" ItemStyle-Width="10%">
            <ItemTemplate>
                <asp:TextBox ID="txtinvoicedate" MaxLength="6" AutoPostBack="true" runat="server"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Total Invoice Value" ItemStyle-Width="10%">
            <ItemTemplate>
                <asp:TextBox ID="txtinvoicevalue" CssClass="form-control input-sm"  ReadOnly="true" AutoPostBack="true" runat="server"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Total Taxable Value" ItemStyle-Width="10%">
            <ItemTemplate>
                <asp:TextBox ID="txttaxablevalue" AutoPostBack="true" CssClass="form-control input-sm"  runat="server"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="IGST" ItemStyle-Width="10%">
            <ItemTemplate>
                <asp:TextBox ID="txtigst" AutoPostBack="true"  CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="CGST" ItemStyle-Width="10%">
            <ItemTemplate>
                <asp:TextBox ID="txtcgst" AutoPostBack="true"  CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-Width="10%" HeaderText="SGST/UTGST">
            <ItemTemplate>
                <asp:TextBox ID="txtsgstutgst" AutoPostBack="true"  CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-Width="10%" HeaderText="CESS">
            <ItemTemplate>
                <asp:TextBox ID="txtCess" AutoPostBack="true"  runat="server"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
</asp:GridView>
