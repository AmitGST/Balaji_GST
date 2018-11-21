<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_B2B_invoice_GSTR2.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_B2B_invoice_GSTR2" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>

<div class="box box-primary">
    <div class="box-body table-responsive no-padding">
        <asp:ListView ID="lv_B2b_Invoice_Gstr2" InsertItemPosition="LastItem" runat="server" OnItemDataBound="lv_B2b_Invoice_Gstr2_ItemDataBound" OnItemCreated="lv_B2b_Invoice_Gstr2_ItemCreated" OnItemEditing="lv_B2b_Invoice_Gstr2_ItemEditing" OnItemUpdating="lv_B2b_Invoice_Gstr2_ItemUpdating" OnItemInserting="lv_B2b_Invoice_Gstr2_ItemInserting" DataKeyNames="ValueId" >
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
                        <asp:CheckBox ID="chk" CssClass="selectone" runat="server" /></td>
                    <td>
                        <asp:TextBox ID="txtGSTIN" Class="form-control" Width="140px" Text='<%# Eval("SupplierGSTIN") %>' runat="server" disabled></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtInvoiceNo" Class="form-control" Text='<%# Eval("InvoiceNo") %>' runat="server" disabled></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txt_InvoiceDate" Class="form-control" Text='<%#DateTimeAgo.GetFormatDate(Eval("InvoiceDate")) %>' Width="100px" runat="server" disabled></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtInvoiceValue" Class="form-control" Width="100px" Text='<%# Eval("TotalInvoiceValue") %>' runat="server" disabled></asp:TextBox></td>
                    <td>
                        <asp:DropDownList ID="ddlPos" Class="form-control" Width="130px" runat="server" disabled></asp:DropDownList></td>
                    <asp:HiddenField ID="hdnPos" runat="server" Value='<%# Eval("PlaceOfSupply") %>' />
                    <td>
                        <asp:DropDownList ID="ddl_SupplyType" Class="form-control" Width="90px" runat="server" disabled></asp:DropDownList></td>
                    <asp:HiddenField ID="hdnSupplyType" runat="server" Value='<%# Eval("SupplyType") %>' />
                    <td>
                        <asp:DropDownList ID="ddl_InvoiceType" Class="form-control" Width="90px" runat="server" disabled></asp:DropDownList></td>
                    <asp:HiddenField ID="hdnInvoiceType" runat="server" Value='<%# Eval("InvoiceType") %>' />
                    <td>
                        <asp:CheckBox ID="chkReverse" Checked='<%# Eval("ReverseCharge")==null?false:Convert.ToBoolean(Eval("ReverseCharge")) %>' Enabled="false" runat="server" /></td>
                    <td>
                        <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary btn-xs" CommandName="Edit" data-trigger="hover" data-placement="right" title="Edit"><i class="fa fa-pencil-square-o"></i></asp:LinkButton></td>
                </tr>
            </ItemTemplate>
            <EditItemTemplate>
                <tr>
                    <td>
                        <asp:CheckBox ID="chk" CssClass="selectone" runat="server" /></td>
                    <td>
                        <asp:TextBox ID="txtGSTIN" Class="form-control input-sm" Text='<%# Eval("ReceiverGSTIN") %>' Width="120px" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtInvoiceNo" Class="form-control input-sm" runat="server" Width="110px" Text='<%# Eval("InvoiceNo") %>'></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txt_InvoiceDate" Text='<%#DateTimeAgo.GetFormatDate(Eval("InvoiceDate")) %>' Width="90px" CssClass="form-control input-sm" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtInvoiceValue" Class="form-control input-sm" Width="80px" Text='<%# Eval("TotalInvoiceValue") %>' runat="server"></asp:TextBox></td>
                    <td>
                        <asp:DropDownList ID="ddlPos" Class="form-control input-sm" Width="100px" runat="server"></asp:DropDownList></td>
                    <asp:HiddenField ID="hdnPos" runat="server" Value='<%# Eval("PlaceOfSupply") %>' />

                    <td>
                        <asp:DropDownList ID="ddl_SupplyType" Class="form-control input-sm" Width="90px" runat="server"></asp:DropDownList></td>
                    <asp:HiddenField ID="hdnSupplyType" runat="server" Value='<%# Eval("SupplyType") %>' />
                    <td>
                        <asp:DropDownList ID="ddl_InvoiceType" Class="form-control input-sm" Width="100px" runat="server"></asp:DropDownList></td>
                    <asp:HiddenField ID="hdnInvoiceType" runat="server" Value='<%# Eval("InvoiceType") %>' />

                    <td>
                        <asp:CheckBox ID="chkReverse" Checked='<%# Eval("ReverseCharge")==null?false:Convert.ToBoolean(Eval("ReverseCharge")) %>' Enabled="false" runat="server" /></td>
                    <td>
                        <asp:LinkButton ID="btnAddMore" CssClass="btn btn-primary btn-xs" OnClick="btnAddMore_Click" runat="server"><i class="fa fa-plus"></i></asp:LinkButton>
                        <asp:LinkButton ID="lkbUpdate" CssClass="btn btn-success btn-xs" CommandArgument='<%# Eval("ValueId") %>' runat="server" CommandName="Update"><i class="fa fa-save"></i></asp:LinkButton>
                    </td>
                </tr>
            </EditItemTemplate>
            <InsertItemTemplate>
                <tr>
                    <td></td>
                    <td>
                        <asp:TextBox ID="txtGSTIN" Class="form-control input-sm" Width="120px" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtInvoiceNo" class="form-control input-sm" Width="110px" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txt_InvoiceDate" Width="90px" placeholder="MM/DD/YYY" CssClass="form-control date-picker" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtInvoiceValue" class="form-control input-sm" Width="80px" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:DropDownList ID="ddlPos" Width="100px" AutoPostBack="true" class="form-control input-sm" runat="server"></asp:DropDownList></td>
                    <%--<asp:HiddenField ID="hdnPos" runat="server" />--%>
                    <td>
                        <asp:DropDownList ID="ddl_SupplyType" class="form-control input-sm" AutoPostBack="true" Width="90px" runat="server"></asp:DropDownList></td>
                    <%--<asp:HiddenField ID="hdnSupplyType" runat="server" Value='<%# Eval("SupplyType") %>' />--%>
                    <td>
                        <asp:DropDownList ID="ddl_InvoiceType" Class="form-control input-sm" Width="100px" runat="server"></asp:DropDownList></td>
                    <%--<asp:HiddenField ID="hdnInvoiceType" runat="server" Value='<%# Eval("InvoiceType") %>' />--%>
                    <td>
                        <asp:CheckBox ID="chkReverse" runat="server" /></td>
                    <td>
                        <asp:LinkButton ID="lkbInsert" CssClass="btn btn-success btn-xs" runat="server" CommandName="Insert"><i class="fa fa-save"></i></asp:LinkButton>
                    </td>
                </tr>
            </InsertItemTemplate>
            <LayoutTemplate>
                <table class="table table-responsive ">
                    <thead>
                        <tr>
                            <th>
                                <asp:CheckBox ID="Chk1" CssClass="selectall" runat="server" /></th>
                            <th>Supplier's GSTIN</th>
                            <th>Invoice No.</th>
                            <th>Invoice Date</th>
                            <th>Total Invoice Value &nbsp;<i class="fa fa-rupee"></i></th>
                            <th>Place of Supply</th>
                            <th>Supply Type</th>
                            <th>Invoice Type</th>
                            <th>Reverse Charge</th>
                            <th>Actions</th>
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
        <asp:LinkButton ID="lkbDelete" OnClientClick="return confirm('Are you sure?\n You want to delete!')" CssClass="btn btn-danger" OnClick="lkbDelete_Click" runat="server"><i class="fa fa-trash"></i>&nbsp;Delete</asp:LinkButton>
        <uc1:uc_sucess runat="server" ID="uc_sucess" />
    </div>
</div>
