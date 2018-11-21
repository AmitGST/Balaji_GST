<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_CreditAddItems.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_CreditAddItems" %>
<div class="box box-primary">
    <div class="box-body">
        <div class="row">
            <div class="col-md-3">
                <div class="form-group">
                    <label>Section</label>
                    <asp:TextBox ID="txtSection" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <label>InvoiceNumber</label>
                    <asp:TextBox ID="txtInvoiceNo" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
        <asp:ListView ID="lv_creditItems" runat="server">
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
                        <asp:CheckBox ID="chk" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtRate" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTaxableValue" runat="server"></asp:TextBox>
                    </td>

                    <td>
                        <asp:TextBox ID="txtCGST" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtSGST" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtCess" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtEligibility" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtCGSTAmt" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtSGSTAmt" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtCessAmt" runat="server"></asp:TextBox></td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table class="table table-responsive ">
                    <thead>
                        <tr>
                            <th>
                                <asp:CheckBox ID="Chk1" runat="server" /></th>
                            <th rowspan="2">Rate</th>
                            <th rowspan="2">Total Taxable Value</th>
                            <th colspan="3">Amount of Tax</th>
                            <th rowspan="2">Eligibility for ITC</th>
                            <th colspan="3">Amount of ITC available</th>
                        </tr>
                        <tr>
                            <th>CGST</th>
                            <th>SGST/UTGST</th>
                            <th>Cess</th>
                            <th>CGST</th>
                            <th>SGST/UTGST</th>
                            <th>Cess</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="itemPlaceholder" runat="server" />
                    </tbody>
                </table>
            </LayoutTemplate>
        </asp:ListView>


    </div>
    <div class="box-footer">
        <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary" ToolTip="Save" runat="server"><i class="fa fa-save"></i></asp:LinkButton>
    </div>
</div>
