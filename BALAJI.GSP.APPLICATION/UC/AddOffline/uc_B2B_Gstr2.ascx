<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_B2B_Gstr2.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.AddOffline.uc_B2B_Gstr2" %>
<div class="box box-primary">
    <div class="box-header">
        <div class="form-inline">
            <div class="col-md-3">
                <div class="form-group">
                    <label>Section</label>
                    <asp:TextBox ID="txtSection" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <label>Invoice Number</label>
                    <asp:TextBox ID="txtInvoiceNo" runat="server"></asp:TextBox>
                </div>
              
            </div>
        </div>
    </div>
            <div class="box-body">
                <asp:ListView ID="lv_B2BUR" runat="server">
                    <EmptyDataTemplate>
                        <table class="table table-responsive">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                             <td>
                                <asp:TextBox ID="txtRate"  runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_TotalTaxableValue" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtIGST" runat="server"></asp:TextBox> 
                            </td>
                            <td>
                                <asp:TextBox ID="txtCess" runat="server"></asp:TextBox>
                            </td>
                             <td>
                             <asp:DropDownList ID="txtITC" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                            <asp:TextBox ID="txtIGSTAmt" runat="server"></asp:TextBox>
                            </td>
                            <td>
                            <asp:TextBox ID="txtCessAmt" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive">
                            <thead>
                                <tr>
                                    <th rowspan="2">Rate</th>
                                    <th rowspan="2">Total Taxable Value</th>
                                    <th colspan="2">Amount of Tax</th>
                                    <th rowspan="2">Eligibility for ITC</th>
                                    <th colspan="2">Amount of ITC Availiable</th>
                                </tr>
                                <tr>
                               <th>Integrated Tax Amount</th>
                               <th>Cess Amount</th>
                               <th>Integrated Tax Amount</th>
                               <th>Cess Amount</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr id="itemPlaceholder" runat="server" />
                            </tbody>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
            </div>
        </div>