<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_goodsTXI_Gstr2.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.AddOffline.uc_goodsTXI_Gstr2" %>
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
                <asp:ListView ID="lv_goods" runat="server">
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
                                <asp:TextBox ID="txt_Advance" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtIGST" runat="server"></asp:TextBox> 
                            </td>
                            <td>
                                <asp:TextBox ID="txtCess" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive">
                            <thead>
                                <tr>
                                    <th>Rate</th>
                                    <th>Gross Advance Recieved</th>
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