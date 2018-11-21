<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeBehind="InvoiceNumber.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Masters.InvoiceNumber" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-header">
        <h1>Invoice Number
            <small></small></h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Masters</a></li>
            <li class="active">Invoice Number</li>
        </ol>
    </div>
    <div class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Invoice number</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove">
                        <i class="fa fa-remove"></i>
                    </button>
                </div>
            </div>
            <div class="box-body">
                <asp:ListView ID="lv_InvoiceType" runat="server" OnItemUpdating="lv_InvoiceType_ItemUpdating" OnItemDataBound="lv_InvoiceType_ItemDataBound">
                    <EmptyDataTemplate>
                        <table class="table">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("Value") %></td>
                            <td>
                                <asp:HiddenField ID="hFInvoiceKey" Value='<%#Bind("Key") %>' runat="server" />
                                StateCode
                                <asp:TextBox ID="txtInvoiceType" Width="50"  runat="server"></asp:TextBox>
                                <asp:Label ID="lblInvoiceType" runat="server"></asp:Label>
                                /<asp:Label ID="lblYear" runat="server"></asp:Label>
                                /<asp:TextBox ID="txtInvoiceNo" Text="0001" Width="50" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table">
                            <tr>
                                <th>Invoice Type</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
            </div>
            <div class="box-footer">
                <asp:LinkButton ID="lbSave" CssClass="btn btn-primary" runat="server" OnClick="lbSave_Click">Submit</asp:LinkButton>
            </div>
        </div>
    </div>
    
</asp:Content>
