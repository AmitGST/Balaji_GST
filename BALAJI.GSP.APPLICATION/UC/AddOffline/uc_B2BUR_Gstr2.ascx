<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_B2BUR_Gstr2.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.AddOffline.uc_B2BUR_Gstr2" %>
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
                <asp:ListView ID="lv_B2CL" runat="server" OnSelectedIndexChanged="lv_B2CL_SelectedIndexChanged">
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
                                <asp:TextBox ID="txtCGST" runat="server"></asp:TextBox> 
                            </td>
                            <td>
                                <asp:TextBox ID="txtSGST" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCess" runat="server"></asp:TextBox>
                            </td>
                             <td>
                             <asp:DropDownList ID="txtITC" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:TextBox ID="txtCGSTAmt" runat="server"></asp:TextBox> 
                            </td>
                            <td>
                                <asp:TextBox ID="txtSGSTAmt" runat="server"></asp:TextBox>
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
                                    <th colspan="3">Amount of Tax</th>
                                    <th rowspan="2">Eligibility for ITC</th>
                                    <th colspan="3">Amount of ITC Availiable</th>
                                </tr>
                                <tr>
                               <th>CGST Amount</th>
                               <th>SGST/UTGST Amount</th>
                               <th>Cess Amount</th>
                               <th>CGST Amount</th>
                               <th>SGST/UTGST Amount</th>
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