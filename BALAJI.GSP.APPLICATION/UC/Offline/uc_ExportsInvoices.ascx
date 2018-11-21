<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_ExportsInvoices.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_ExportsInvoices" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>


<div class="box box-primary">
    <div class="box-body table-responsive no-padding">
        <asp:ListView ID="lv_Export" InsertItemPosition="LastItem" runat="server" OnItemDataBound="lv_Export_ItemDataBound" OnItemCreated="lv_Export_ItemCreated" OnItemEditing="lv_Export_ItemEditing" OnItemUpdating="lv_Export_ItemUpdating" OnItemInserting="lv_Export_ItemInserting" OnPagePropertiesChanging="lv_Export_PagePropertiesChanging" DataKeyNames="ValueId">
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
                        <asp:CheckBox ID="chk" CssClass="selectone" runat="server" disabled />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_GSTPayment" class="form-control input-sm" runat="server" disabled></asp:DropDownList></td>
                    <asp:HiddenField ID="hdnGSTPayment" runat="server" Value='<%# Eval("GST_Payment") %>' />
                    <td>
                        <asp:TextBox ID="txt_invoiceno" Text='<%# Eval("InvoiceNo") %>' class="form-control input-sm" runat="server" disabled></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_InvoiceDate" Text='<%#Eval("InvoiceDate","{0:dd/MM/yyyy}") %>' placeholder="DD/MM/YYYY" CssClass="form-control input-sm date-picker_offline" runat="server" disabled></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_InvoiceValue" class="form-control input-sm" Text='<%# Eval("TotalInvoiceValue") %>' runat="server" disabled></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_PortCode" class="form-control input-sm" Text='<%# Eval("PortCode") %>' runat="server" disabled></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_ShippingBillNo" class="form-control input-sm" Text='<%# Eval("ShippingBillNo") %>' runat="server" disabled></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_ShippingBillDate" Text='<%# Eval("ShippingBillDate","{0:dd/MM/yyyy}") %>' placeholder="DD/MM/YYYY" CssClass="form-control input-sm date-picker_offline" runat="server" disabled></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_SupplyType" class="form-control input-sm" runat="server" disabled></asp:DropDownList>
                        <asp:HiddenField ID="Hdn_SupplyType" runat="server" Value='<%# Eval("SupplyType") %>' />
                    </td>
                    <td>
                        <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary btn-xs" CommandName="Edit" data-trigger="hover" data-placement="right" title="Edit"><i class="fa fa-pencil-square-o"></i></asp:LinkButton></td>
                </tr>
            </ItemTemplate>
            <EditItemTemplate>
                <tr>
                    <td>
                        <asp:CheckBox ID="chk" CssClass="selectone" runat="server" />
                    </td>
                    <td>
                            <asp:DropDownList ID="ddl_GSTPayment" class="form-control input-sm" Enabled="false" runat="server"></asp:DropDownList>
                            <asp:HiddenField ID="hdnGSTPayment" runat="server" Value='<%# Eval("GST_Payment") %>' />
                    </td>

                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_invoiceno" Text='<%# Eval("InvoiceNo") %>' class="form-control input-sm" Enabled="false" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfcInvoiceNo" runat="server" Display="Dynamic" ControlToValidate="txt_invoiceno" CssClass="help-block" ErrorMessage="Please specify Invoice No" ValidationGroup="vgExportInvoiceEdit"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_InvoiceDate" Text='<%#Eval("InvoiceDate","{0:dd/MM/yyyy}") %>' placeholder="DD/MM/YYYY" Enabled="false" CssClass="form-control input-sm date-picker_offline" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfcInvoiceDate" runat="server" Display="Dynamic" ControlToValidate="txt_InvoiceDate" CssClass="help-block" ErrorMessage="Please specify Invoice Date" ValidationGroup="vgExportInvoiceEdit"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_InvoiceValue" class="form-control input-sm" autocomplete="off" onkeypress="return isNumberKey(event,this);" onpaste="return false;" Text='<%# Eval("TotalInvoiceValue") %>' runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfcInvoiceValue" runat="server" Display="Dynamic" ControlToValidate="txt_InvoiceValue" CssClass="help-block" ErrorMessage="Please specify Invoice Value" ValidationGroup="vgExportInvoiceEdit"></asp:RequiredFieldValidator>
                        </div>
                    </td>

                    <td>
                        <asp:TextBox ID="txt_PortCode" class="form-control input-sm" Text='<%# Eval("PortCode") %>' runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_ShippingBillNo" class="form-control input-sm" Text='<%# Eval("ShippingBillNo") %>' runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_ShippingBillDate" placeholder="DD/MM/YYYY" Text='<%# Eval("ShippingBillDate","{0:dd/MM/yyyy}") %>' CssClass="form-control input-sm date-picker_offline" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_SupplyType" class="form-control input-sm" Enabled="false" runat="server"></asp:DropDownList>
                        <asp:HiddenField ID="Hdn_SupplyType" runat="server" Value='<%# Eval("SupplyType") %>' />
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lkbUpdate" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:LinkButton ID="btnAddMore" runat="server" OnClick="btnAddMore_Click" CommandArgument='<%# Eval("ValueId") %>' CssClass="btn btn-primary btn-xs" data-trigger="hover" title="Add Items"><i class="fa fa-plus-circle"></i></asp:LinkButton>
                        <asp:LinkButton ID="lkbUpdate" CssClass="btn btn-success btn-xs" CommandArgument='<%# Eval("ValueId") %>' ValidationGroup="vgExportInvoiceEdit" CausesValidation="true" ToolTip="Update" runat="server" CommandName="Update"><i class="fa fa-save"></i></asp:LinkButton>
                    </td>
                </tr>

            </EditItemTemplate>
            <InsertItemTemplate>
                <tr>
                    <td></td>
                    <td>
                        <div class="form-group">
                            <asp:DropDownList ID="ddl_GSTPayment" class="form-control input-sm" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvGSTPayment" runat="server" InitialValue="-1" Display="Dynamic" ControlToValidate="ddl_GSTPayment" CssClass="help-block" ErrorMessage="Please specify GST Payment" ValidationGroup="vgExportInvoice"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_invoiceno" class="form-control input-sm" autocomplete="off" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfcInvoiceNo" runat="server" Display="Dynamic" ControlToValidate="txt_invoiceno" CssClass="help-block" ErrorMessage="Please specify Invoice No" ValidationGroup="vgExportInvoice"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_InvoiceDate"  placeholder="DD/MM/YYYY" autocomplete="off" CssClass="form-control input-sm date-picker_offline" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfcInvoiceDate" runat="server" Display="Dynamic" ControlToValidate="txt_InvoiceDate" CssClass="help-block" ErrorMessage="Please specify Invoice Date" ValidationGroup="vgExportInvoice"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_InvoiceValue" onkeypress="return isNumberKey(event,this);" autocomplete="off" onpaste="return false;" class="form-control input-sm" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfcInvoiceValue" runat="server" Display="Dynamic" ControlToValidate="txt_InvoiceValue" CssClass="help-block" ErrorMessage="Please specify Invoice Value" ValidationGroup="vgExportInvoice"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_PortCode" class="form-control input-sm" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_ShippingBillNo" class="form-control input-sm" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_ShippingBillDate" placeholder="DD/MM/YYYY" CssClass="form-control input-sm date-picker_offline" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:DropDownList ID="ddl_SupplyType" class="form-control input-sm"  runat="server"></asp:DropDownList></td>
                    <td>
                         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lkbInsert" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:LinkButton ID="lkbInsert" CssClass="btn btn-success btn-xs" runat="server" ValidationGroup="vgExportInvoice" CausesValidation="true" CommandName="Insert"><i class="fa fa-plus"></i></asp:LinkButton>
                    </td>
            </InsertItemTemplate>

            <LayoutTemplate>
                <table class="table table-responsive ">
                    <thead>
                        <tr>
                            <th style="width:4%">
                                <asp:CheckBox ID="Chk1" CssClass="selectall" runat="server" /></th>
                            <th style="width:10%">GST Payment</th>
                            <th style="width:15%">Invoice No.</th>
                            <th style="width:10%">Invoice Date</th>
                            <th style="width:15%">Total Invoice Value</th>
                            <th style="width:10%">Port Code</th>
                            <th style="width:10%">Shipping Bill No.</th>
                            <th style="width:10%">Shipping Bill Date</th>
                            <th style="width:10%">Supply Type</th>
                            <th style="width:6%">Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="itemPlaceholder" runat="server" />
                    </tbody>
                </table>
            </LayoutTemplate>
        </asp:ListView>
        <div class="box-footer clearfix">
            <asp:LinkButton ID="lkbDelete"  CssClass="btn btn-danger" OnClick="lkbDelete_Click" runat="server"><i class="fa fa-trash"></i>&nbsp;Delete</asp:LinkButton>
            <div class="pagination pagination-sm no-margin pull-right">
                <%--<div class="box-body dataTables_paginate paging_simple_numbers">--%>
                <asp:DataPager ID="dpExport" runat="server" PagedControlID="lv_Export" PageSize="10" class="btn-group-sm pager-buttons">
                    <Fields>
                        <asp:NextPreviousPagerField ShowLastPageButton="False" ShowNextPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                        <asp:NumericPagerField ButtonType="Button" RenderNonBreakingSpacesBetweenControls="false" NumericButtonCssClass="btn btn-primary" CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn" />
                        <asp:NextPreviousPagerField ShowFirstPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                    </Fields>
                </asp:DataPager>
            </div>
            <uc1:uc_sucess runat="server" ID="uc_sucess" />
        </div>
    </div>
</div>
<div class="modal modal-warning fade" id="viewInvoiceModelWarningMessage" role="dialog" aria-labelledby="viewInvoiceModelWarningMessage" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                    &times;
                                </button>
                                <h4 class="modal-title">
                                    <i class="fa fa-exclamation-triangle"></i>
                                    <asp:Label ID="Label1" runat="server" Text="Warning Message"></asp:Label>
                                </h4>
                            </div> <div class="modal-body">
                                <p>
                                <asp:Label ID="lblWarning" runat="server"></asp:Label>
                            </p>
                            </div>
                          
                            <div class="modal-footer">
                                <button class="btn btn-outline pull-right" data-dismiss="modal" aria-hidden="true">
                                    Close
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
<script type="text/javascript">
    function Showalert() {
        alert('Call JavaScript function from codebehind');
    }
</script>
