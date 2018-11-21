<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/User/User.master" CodeBehind="GSTUploadInvoice.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.User.uinvoice.GSTUploadInvoice" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    


    <div class="content-header">
        <h1>Upload Invoice</h1>

        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Return</a></li>
            <li class="active">Upload Invoice</li>
        </ol>
    </div>
   <%-- <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>--%>
   
            <div class="content">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Details of Seller</h3>
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
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>
                                        GSTIN</label>
                                    <asp:Label ID="lblGSTIN" runat="server"> </asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>
                                        Period</label>
                                    <asp:Label ID="lblPeriod" runat="server"></asp:Label>
                                </div>
                            </div>
                            <%--<div class="col-md-4">
                        <div class="form-group">
                            <label>
                            GSTIN value</label>
                            <asp:Label ID="" runat="server" Style="display: none;"></asp:Label>
                        </div>
                    </div>--%>
                            
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" Text="From Date"></asp:Label>
                                    <asp:TextBox ID="txtFromDate" runat="server" class="form-control" Width="100px" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" Text="To Date"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Width="100px" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                            </div>
                        </div>
                    </div>
                </div>

                <div class="content-header">
                    <h1>B2B Invoice</h1>
                     </div>
                <div class="box box-danger">
                    <div class="box-header with-border">
                        <h3 class="box-title">Details of Goods</h3>
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
                        <asp:GridView ID="GVUploadInvoice" runat="server" AutoGenerateColumns="False"
                            OnRowCommand="GVUploadInvoice_RowCommand" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="12px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>

                                <asp:BoundField DataField="InvoiceNo"
                                    HeaderText="Invoice No">
                                    <ItemStyle Width="270px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Invoicedate"
                                    HeaderText="Invoice Date">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalQty"
                                    HeaderText="Qty">
                                    <ItemStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalRate"
                                    HeaderText="Rate">
                                    <ItemStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalAmount"
                                    HeaderText="Amt.">
                                    <ItemStyle Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalDiscount"
                                    HeaderText="Discount">
                                    <ItemStyle Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalTaxableAmount"
                                    HeaderText="Total Taxable Amt.">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalCGSTAmount"
                                    HeaderText="CGST Amt.">
                                    <ItemStyle Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalSGSTAmount"
                                    HeaderText="SGST Amt.">
                                    <ItemStyle Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalIGSTAmount"
                                    HeaderText="IGST Amt.">

                                    <ItemStyle Width="90px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TotalAmountWithTax"
                                    HeaderText="Amt. with Tax">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Operation">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkUpload" runat="server" CommandArgument='<%#Eval("InvoiceNo")%>' Text="Upload" Style="font-weight: bold; text-decoration: underline;" CommandName="UploadRow"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>


                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#F7F7DE" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>

                    </div>
                </div>

                <div class="box box-danger">
                    <div class="box-header with-border">
                        <h3 class="box-title">Advance Payment</h3>
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
                        <asp:GridView ID="GVAdvancePayment" runat="server" AutoGenerateColumns="False" CellPadding="4" OnRowCommand="GVUploadInvoice_RowCommand" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="12px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <%--  <asp:TemplateField HeaderText="Select All">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkAll" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="VoucherNo"
                                    HeaderText="Voucher No">
                                    <ItemStyle Width="270px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Voucherdate"
                                    HeaderText="Voucher Date">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalQty"
                                    HeaderText="Qty">
                                    <ItemStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalRate"
                                    HeaderText="Rate">
                                    <ItemStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalAmount"
                                    HeaderText="Amt.">
                                    <ItemStyle Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalDiscount"
                                    HeaderText="Discount">
                                    <ItemStyle Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalTaxableAmount"
                                    HeaderText="Total Taxable Amt.">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalCGSTAmount"
                                    HeaderText="CGST Amt.">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalSGSTAmount"
                                    HeaderText="SGST Amt.">
                                    <ItemStyle Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalIGSTAmount"
                                    HeaderText="IGST Amt.">

                                    <ItemStyle Width="90px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TotalAmountWithTax"
                                    HeaderText="Amt. with Tax">
                                    <ItemStyle Width="90px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Operation">
                                    <ItemTemplate>
                                        <%--   <asp:Button ID="btnUpload" runat="server" CommandName="Upload" Text="Upload" />--%>
                                        <%--  <asp:button Id="btnUpload" onclick="btnRowUpload_click" runat="server" Text="Upload" commandargument='<%#Eval("InvoiceNo")%>'></asp:button>--%>

                                        <%-- <asp:Button ID="AddButton" runat="server"
                                                            CommandName="AddToCart"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            Text="Add to Cart" />--%>

                                        <asp:LinkButton ID="lnkUpload" runat="server" CommandArgument='<%#Eval("VoucherNo")%>' Text="Upload" Style="font-weight: bold; text-decoration: underline;" CommandName="UploadRow"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>


                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#F7F7DE" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                    </div>
                </div>

                <div class="box box-danger">
                    <div class="box-header with-border">
                        <h3 class="box-title">Export</h3>
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
                        <asp:GridView ID="GVExport" runat="server" AutoGenerateColumns="False" CellPadding="4" OnRowCommand="GVUploadInvoice_RowCommand" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="12px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <%--  <asp:TemplateField HeaderText="Select All">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkAll" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="ExportNo"
                                    HeaderText="Export No">
                                    <ItemStyle Width="270px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Exportdate"
                                    HeaderText="Export Date">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalQty"
                                    HeaderText="Qty">
                                    <ItemStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalRate"
                                    HeaderText="Rate">
                                    <ItemStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalAmount"
                                    HeaderText="Amt.">
                                    <ItemStyle Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalDiscount"
                                    HeaderText="Discount">
                                    <ItemStyle Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalTaxableAmount"
                                    HeaderText="Total Taxable Amt.">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalCGSTAmount"
                                    HeaderText="CGST Amt.">
                                    <ItemStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalSGSTAmount"
                                    HeaderText="SGST Amt.">
                                    <ItemStyle Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalIGSTAmount"
                                    HeaderText="IGST Amt.">

                                    <ItemStyle Width="90px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TotalAmountWithTax"
                                    HeaderText="Amt. with Tax">
                                    <ItemStyle Width="90px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Operation">
                                    <ItemTemplate>
                                        <%--   <asp:Button ID="btnUpload" runat="server" CommandName="Upload" Text="Upload" />--%>
                                        <%--  <asp:button Id="btnUpload" onclick="btnRowUpload_click" runat="server" Text="Upload" commandargument='<%#Eval("InvoiceNo")%>'></asp:button>--%>

                                        <%-- <asp:Button ID="AddButton" runat="server"
                                                            CommandName="AddToCart"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            Text="Add to Cart" />--%>

                                        <asp:LinkButton ID="lnkUpload" runat="server" CommandArgument='<%#Eval("ExportNo")%>' Text="Upload" Style="font-weight: bold; text-decoration: underline;" CommandName="UploadRow"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>


                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#F7F7DE" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                    </div>
                </div>
               
                <div class="box-footer">
                    <div align="right" style="margin-right: 15px;">
                            <asp:Button ID="BtnUpload" runat="server" Text="Upload All" CssClass="btn btn-primary" Width="120px" OnClick="BtnUpload_Click" />
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary" Visible="false" OnClick="btnBack_Click" Width="100px" />
                        </div>
                    </div>
                </div>
               
</asp:Content>
