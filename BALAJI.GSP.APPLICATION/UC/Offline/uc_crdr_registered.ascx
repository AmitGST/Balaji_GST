<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_crdr_registered.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_crdr_registered" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>
<%@ Register Src="~/UC/Offline/Controls/uc_SupplyType.ascx" TagPrefix="uc1" TagName="uc_SupplyType" %>



<div class="box box-primary">
    <div class="box-body table-responsive no-padding">
        <asp:ListView ID="lv_crdrRegister" InsertItemPosition="LastItem" OnItemCreated="lv_crdrRegister_ItemCreated" runat="server" OnItemDataBound="lv_crdrRegister_ItemDataBound" OnItemEditing="lv_crdrRegister_ItemEditing" OnItemUpdating="lv_crdrRegister_ItemUpdating" OnItemInserting="lv_crdrRegister_ItemInserting" DataKeyNames="ValueId" OnPagePropertiesChanging="lv_crdrRegister_PagePropertiesChanging">
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
                        <asp:CheckBox ID="chk" CssClass="selectone" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtRcvrGSTIN" class="form-control input-sm" Text='<%# Eval("ReceiverGSTIN") %>' runat="server" Disabled></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtVoucherNo" class="form-control input-sm" Text='<%# Eval("Voucher_No") %>' runat="server" Disabled></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_VoucherDate" Text='<%#Eval("Voucher_date","{0:dd/MM/yyyy}") %>' placeholder="MM/DD/YYY" CssClass="form-control input-sm date-picker_offline" runat="server" Disabled></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_invoiceno" class="form-control input-sm" Text='<%# Eval("InvoiceNo") %>' CssClass="form-control" runat="server" Disabled></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_InvoiceDate" Text='<%#Eval("InvoiceDate","{0:dd/MM/yyyy}") %>' CssClass="form-control input-sm date-picker_offline" placeholder="MM/DD/YYY" runat="server" Disabled></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_DocumentType" class="form-control input-sm" runat="server" Disabled></asp:DropDownList>
                        <asp:HiddenField ID="hdnDoc" runat="server" Value='<%# Eval("Document_Type") %>' />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_IssuingNote" class="form-control input-sm" runat="server" Disabled></asp:DropDownList>
                        <asp:HiddenField ID="hdnIssuingNote" runat="server" Value='<%# Eval("Issuing_Note") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="txt_VoucherValue" class="form-control input-sm" Text='<%# Eval("Voucher_Value") %>' runat="server" Disabled></asp:TextBox>
                    </td>

                    <uc1:uc_SupplyType runat="server" ID="uc_SupplyType" />
                    <asp:HiddenField ID="hdnPos" runat="server" Value='<%# Eval("PlaceOfSupply") %>' />
                    <asp:HiddenField ID="hdnSupplyType" runat="server" Value='<%# Eval("SupplyType") %>' />
                    <td>
                        <asp:CheckBox ID="chk_PreGST" Checked='<%# Eval("Pre_GST")==null?false:Convert.ToBoolean(Eval("Pre_GST")) %>' Enabled="false" runat="server" />
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
                        <div class="form-group">
                            <asp:TextBox ID="txtRcvrGSTIN" class="form-control input-sm" autocomplete="off" Text='<%# Eval("ReceiverGSTIN") %>' runat="server" disabled></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRcvrGSTIN" runat="server" Display="Dynamic" ControlToValidate="txtRcvrGSTIN" CssClass="help-block" ErrorMessage="Please specify Receiver GSTIN" ValidationGroup="vgcrdrRegisteredSave"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revRcvrGSTIN" runat="server" Display="Dynamic" ControlToValidate="txtRcvrGSTIN" CssClass="help-block" ErrorMessage="Invalid GSTIN" ValidationGroup="vgcrdrRegisteredSave" ValidationExpression="^([0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1})$"></asp:RegularExpressionValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txtVoucherNo" class="form-control input-sm" Text='<%# Eval("Voucher_No") %>' autocomplete="off" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvVoucherNo" runat="server" Display="Dynamic" ControlToValidate="txtVoucherNo" CssClass="help-block" ErrorMessage="Please specify Voucher No" ValidationGroup="vgcrdrRegisteredSave"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_VoucherDate" Text='<%#Eval("Voucher_date","{0:dd/MM/yyyy}") %>' autocomplete="off" placeholder="DD/MM/YYYY" Width="95px" CssClass="form-control input-sm date-picker_offline" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvVoucherDate" runat="server" Display="Dynamic" ControlToValidate="txt_VoucherDate" CssClass="help-block" ErrorMessage="Please specify Voucher date" ValidationGroup="vgcrdrRegisteredSave"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_invoiceno" class="form-control" Text='<%# Eval("InvoiceNo") %>' autocomplete="off" runat="server" disabled></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvInvoiceNo" runat="server" Display="Dynamic" ControlToValidate="txt_invoiceno" CssClass="help-block" ErrorMessage="Please specify Invoice No" ValidationGroup="vgcrdrRegisteredSave"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_InvoiceDate" Text='<%#Eval("InvoiceDate","{0:dd/MM/yyyy}") %>' autocomplete="off" CssClass="form-control input-sm date-picker_offline" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvInvoiceDate" runat="server" Display="Dynamic" ControlToValidate="txt_InvoiceDate" CssClass="help-block" ErrorMessage="Please specify Invoice date" ValidationGroup="vgcrdrRegisteredSave"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:DropDownList ID="ddl_DocumentType" class="form-control input-sm" runat="server"></asp:DropDownList>
                            <asp:HiddenField ID="hdnDoc" runat="server" Value='<%# Eval("Document_Type") %>' />
                            <asp:RequiredFieldValidator ID="rfvDocumentType" runat="server" Display="Dynamic" ControlToValidate="ddl_DocumentType" InitialValue="-1" CssClass="help-block" ErrorMessage="Please specify Document type" ValidationGroup="vgcrdrRegisteredSave"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:DropDownList ID="ddl_IssuingNote" class="form-control input-sm" runat="server"></asp:DropDownList>
                            <asp:HiddenField ID="hdnIssuingNote" runat="server" Value='<%# Eval("Issuing_Note") %>' />
                            <asp:RequiredFieldValidator ID="rfvIssuingNote" runat="server" Display="Dynamic" ControlToValidate="ddl_IssuingNote" InitialValue="-1" CssClass="help-block" ErrorMessage="Please specify Issuing Note" ValidationGroup="vgcrdrRegisteredSave"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_VoucherValue" class="form-control input-sm" autocomplete="off" onkeypress="return isNumberKey(event,this);" onpaste="return false;" Text='<%# Eval("Voucher_Value") %>' runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvVoucherValue" runat="server" Display="Dynamic" ControlToValidate="txt_VoucherValue" CssClass="help-block" ErrorMessage="Please specify Voucher Value" ValidationGroup="vgcrdrRegisteredSave"></asp:RequiredFieldValidator>
                        </div>
                    </td>

                    <uc1:uc_SupplyType runat="server" ID="uc_SupplyType" />
                    <asp:HiddenField ID="hdnPos" runat="server" Value='<%# Eval("PlaceOfSupply") %>' />
                    <asp:HiddenField ID="hdnSupplyType" runat="server" Value='<%# Eval("SupplyType") %>' />

                    <td>
                        <asp:CheckBox ID="chk_PreGST" Checked='<%# Eval("Pre_GST")==null?false:Convert.ToBoolean(Eval("Pre_GST")) %>' runat="server" />
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lkbUpdate" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:LinkButton ID="btnAddMore" runat="server" CssClass="btn btn-primary btn-xs" CommandArgument='<%# Eval("ValueId") %>' ToolTip="Add Items" OnClick="btnAddMore_Click"><i class="fa fa-plus"></i></asp:LinkButton>
                        <asp:LinkButton ID="lkbUpdate" CssClass="btn btn-success btn-xs" CommandArgument='<%# Eval("ValueId") %>' ValidationGroup="vgcrdrRegisteredSave" ToolTip="Update" runat="server" CommandName="Update"><i class="fa fa-save"></i></asp:LinkButton>
                    </td>
                </tr>
            </EditItemTemplate>
            <InsertItemTemplate>
                <tr>
                    <td></td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txtRcvrGSTIN" Class="form-control input-sm" autocomplete="off" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvRcvrGSTIN" runat="server" Display="Dynamic" ControlToValidate="txtRcvrGSTIN" CssClass="help-block" ErrorMessage="Please specify Receiver GSTIN" ValidationGroup="vgcrdrRegistered"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revRcvrGSTIN" runat="server" Display="Dynamic" ControlToValidate="txtRcvrGSTIN" CssClass="help-block" ErrorMessage="Invalid GSTIN" ValidationGroup="vgcrdrRegistered" ValidationExpression="^([0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1})$"></asp:RegularExpressionValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txtVoucherNo" class="form-control input-sm" autocomplete="off" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvVoucherNo" runat="server" Display="Dynamic" ControlToValidate="txtVoucherNo" CssClass="help-block" ErrorMessage="Please specify Voucher No" ValidationGroup="vgcrdrRegistered"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_VoucherDate" placeholder="DD/MM/YYYY" autocomplete="off" CssClass="form-control input-sm date-picker_offline" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvVoucherDate" runat="server" Display="Dynamic" ControlToValidate="txt_VoucherDate" CssClass="help-block" ErrorMessage="Please specify Voucher date" ValidationGroup="vgcrdrRegistered"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_invoiceno" class="form-control input-sm" autocomplete="off" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvInvoiceNo" runat="server" Display="Dynamic" ControlToValidate="txt_invoiceno" CssClass="help-block" ErrorMessage="Please specify Voucher No" ValidationGroup="vgcrdrRegistered"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_InvoiceDate" placeholder="DD/MM/YYYY" autocomplete="off" CssClass="form-control input-sm date-picker_offline" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvInvoiceDate" runat="server" Display="Dynamic" ControlToValidate="txt_InvoiceDate" CssClass="help-block" ErrorMessage="Please specify Invoice date" ValidationGroup="vgcrdrRegistered"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:DropDownList ID="ddl_DocumentType" class="form-control input-sm" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDocumentType" runat="server" Display="Dynamic" ControlToValidate="ddl_DocumentType" CssClass="help-block" ErrorMessage="Please specify Document type" InitialValue="-1" ValidationGroup="vgcrdrRegistered"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:DropDownList ID="ddl_IssuingNote" class="form-control input-sm" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvIssuingNote" runat="server" Display="Dynamic" ControlToValidate="ddl_IssuingNote" CssClass="help-block" ErrorMessage="Please specify Issuing Note" InitialValue="-1" ValidationGroup="vgcrdrRegistered"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_VoucherValue" class="form-control input-sm" autocomplete="off" onkeypress="return isNumberKey(event,this);" onpaste="return false;" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvVoucherValue" runat="server" Display="Dynamic" ControlToValidate="txt_VoucherValue" CssClass="help-block" ErrorMessage="Please specify Voucher Value" ValidationGroup="vgcrdrRegistered"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <uc1:uc_SupplyType runat="server" ID="uc_SupplyType" />
                    <td>
                        <asp:CheckBox ID="chk_PreGST" runat="server" />
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lkbInsert" />
                            </Triggers>
                        </asp:UpdatePanel>

                        <asp:LinkButton ID="lkbInsert" CssClass="btn btn-success btn-xs" ValidationGroup="vgcrdrRegistered" runat="server" CommandName="Insert"><i class="fa fa-plus"></i></asp:LinkButton>
                    </td>
                </tr>
            </InsertItemTemplate>
            <LayoutTemplate>
                <table class="table table-responsive">
                    <thead>
                        <tr>
                            <th style="width: 4%;">
                                <asp:CheckBox ID="Chk1" CssClass="selectall" runat="server" /></th>
                            <th style="width: 10%;">Receiver GSTIN/UIN</th>
                            <th style="width: 5%;">Note/ Refund Voucher No.</th>
                            <th style="width: 5%;">Note/Refund Voucher Date</th>
                            <th style="width: 10%;">Invoice No.</th>
                            <th style="width: 10%;">Invoice Date</th>
                            <th style="width: 10%;">Document Type</th>
                            <th style="width: 10%;">Reason For Issuing Note </th>
                            <th style="width: 5%;">Note/ Refund Voucher Value</th>
                            <th style="width: 10%;">Place Of Supply</th>
                            <th style="width: 10%;">Supply Type</th>
                            <th style="width: 5%;">Pre GST</th>
                            <th style="width: 6%;">Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="itemPlaceholder" runat="server" />
                    </tbody>
                </table>
            </LayoutTemplate>
        </asp:ListView>
    </div>
    <div class="box-footer clearfix">
        <asp:LinkButton ID="lkbDelete" CssClass="btn btn-danger" OnClick="lkbDelete_Click" runat="server"><i class="fa fa-trash"></i>&nbsp;Delete</asp:LinkButton>
        <div class="pagination pagination-sm no-margin pull-right">
            <%--<div class="box-body dataTables_paginate paging_simple_numbers">--%>
            <asp:DataPager ID="dpcrdr_registered" runat="server" PagedControlID="lv_crdrRegister" PageSize="10" class="btn-group-sm pager-buttons">
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
            </div>
            <div class="modal-body">
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


