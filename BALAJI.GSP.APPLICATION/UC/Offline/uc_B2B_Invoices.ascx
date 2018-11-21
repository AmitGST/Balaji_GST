<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_B2B_Invoices.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_B2B_Invoices" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>
<%@ Register Src="~/UC/Offline/Controls/uc_SupplyType.ascx" TagPrefix="uc1" TagName="uc_SupplyType" %>



<div class="box box-primary">
    <div class="box-body table-responsive no-padding">
        <asp:ListView ID="lv_B2b_Invoice" InsertItemPosition="LastItem" OnItemCreated="lv_B2b_Invoice_ItemCreated" OnItemDataBound="lv_B2b_Invoice_ItemDataBound" runat="server" OnItemEditing="lv_B2b_Invoice_ItemEditing" OnItemUpdating="lv_B2b_Invoice_ItemUpdating" OnItemInserting="lv_B2b_Invoice_ItemInserting" OnPagePropertiesChanging="lv_B2b_Invoice_PagePropertiesChanging" DataKeyNames="ValueId,OfflineID">
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
                        <asp:TextBox ID="txtGSTIN" Class="form-control input-sm" Text='<%# Eval("ReceiverGSTIN") %>' Width="100%" runat="server" disabled></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtInvoiceNo" Class="form-control input-sm" runat="server" Width="100%" Text='<%# Eval("InvoiceNo") %>' disabled></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txt_InvoiceDate" Text='<%#Eval("InvoiceDate","{0:dd/MM/yyyy}") %>' Width="100%" placeholder="MM/DD/YYY" CssClass="form-control input-sm date-picker_offline" runat="server" disabled></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtInvoiceValue" Class="form-control input-sm" Width="100%" Text='<%# Eval("TotalInvoiceValue") %>' runat="server" disabled></asp:TextBox></td>

                    <uc1:uc_SupplyType runat="server" ID="uc_SupplyType" />
                    <asp:HiddenField ID="hdnPos" runat="server" Value='<%# Eval("PlaceOfSupply") %>' />
                    <asp:HiddenField ID="hdnSupplyType" runat="server" Value='<%# Eval("SupplyType") %>' />

                    <td>
                        <asp:DropDownList ID="ddl_InvoiceType" Class="form-control input-sm" Width="100%" runat="server" Disabled></asp:DropDownList>
                        <asp:HiddenField ID="hdnInvoiceType" runat="server" Value='<%# Eval("InvoiceType") %>' />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkReverse" Width="100%" Checked='<%# Eval("ReverseCharge")==null?false:Convert.ToBoolean(Eval("ReverseCharge")) %>' Enabled="false" runat="server" /></td>
                    <td>
                        <asp:TextBox ID="txtECommerce" Class="form-control input-sm" Width="100%" Text='<%# Eval("ECommerce_GSTIN") %>' runat="server" Disabled></asp:TextBox></td>
                    <td>
                        <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary btn-xs" CommandName="Edit" data-trigger="hover" data-placement="right" title="Edit"><i class="fa fa-pencil-square-o"></i></asp:LinkButton></td>
                </tr>
            </ItemTemplate>
            <EditItemTemplate>
                <tr>
                    <td>
                        <div class="form-group">
                            <asp:CheckBox ID="chk" CssClass="selectone" runat="server" />
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txtGSTIN" Class="form-control input-sm" Text='<%# Eval("ReceiverGSTIN") %>' Width="100%" runat="server" disabled></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvGSTIN" runat="server" Display="Dynamic" ControlToValidate="txtGSTIN" CssClass="help-block" ErrorMessage="Please specify GSTIN" ValidationGroup="vgB2BInvoiceEdit"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txtInvoiceNo" Class="form-control input-sm" runat="server" Width="100%" Text='<%# Eval("InvoiceNo") %>' disabled></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvInvoiceNo" runat="server" Display="Dynamic" ControlToValidate="txtInvoiceNo" CssClass="help-block" ErrorMessage="Please specify Invoice No" ValidationGroup="vgB2BInvoiceEdit"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_InvoiceDate" Text='<%#Eval("InvoiceDate","{0:dd/MM/yyyy}") %>' Width="100%" CssClass="form-control input-sm date-picker_offline" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvInvoiceDate" runat="server" Display="Dynamic" ControlToValidate="txt_InvoiceDate" CssClass="help-block" ErrorMessage="Please specify Invoice Date" ValidationGroup="vgB2BInvoiceEdit"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txtInvoiceValue" autocomplete="off" onkeypress="return isNumberKey(event,this);" onpaste="return false;" Class="form-control input-sm" Width="100%" Text='<%# Eval("TotalInvoiceValue") %>' runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvInvoiceValue" runat="server" Display="Dynamic" ControlToValidate="txtInvoiceValue" CssClass="help-block" ErrorMessage="Please specify Invoice Value" ValidationGroup="vgB2BInvoiceEdit"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <uc1:uc_SupplyType runat="server" ID="uc_SupplyType" />
                    <asp:HiddenField ID="hdnPos" runat="server" Value='<%# Eval("PlaceOfSupply") %>' />
                    <asp:HiddenField ID="hdnSupplyType" runat="server" Value='<%# Eval("SupplyType") %>' />

                    <td>
                        <div class="form-group">
                            <asp:DropDownList ID="ddl_InvoiceType" Class="form-control input-sm" Width="100%" runat="server"></asp:DropDownList>
                            <asp:HiddenField ID="hdnInvoiceType" runat="server" Value='<%# Eval("InvoiceType") %>' />
                            <asp:RequiredFieldValidator ID="rfvInvoiceType" runat="server" Display="Dynamic" InitialValue="-1" ControlToValidate="ddl_InvoiceType" CssClass="help-block" ErrorMessage="Please specify Invoice Type" ValidationGroup="vgB2BInvoiceEdit"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkReverse" Width="100%" Checked='<%# Eval("ReverseCharge")==null?false:Convert.ToBoolean(Eval("ReverseCharge")) %>' runat="server" /></td>
                    <td>
                        <asp:TextBox ID="txtECommerce" Class="form-control input-sm" onpaste="return false;" Width="100%" Text='<%# Eval("ECommerce_GSTIN") %>' runat="server" disabled></asp:TextBox></td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lkbUpdate" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:LinkButton ID="btnAddMore" CssClass="btn btn-primary btn-xs" OnClick="btnAddMore_Click" ToolTip="Add Items" CommandArgument='<%# Eval("ValueId") %>' runat="server"><i class="fa fa-plus"></i></asp:LinkButton>
                        <asp:LinkButton ID="lkbUpdate" CssClass="btn btn-success btn-xs" ValidationGroup="vgB2BInvoiceEdit" ToolTip="Update" CausesValidation="true" CommandArgument='<%# Eval("ValueId") %>' runat="server" CommandName="Update"><i class="fa fa-save"></i></asp:LinkButton>
                    </td>

                </tr>
            </EditItemTemplate>
            <InsertItemTemplate>
                <tr>
                    <td></td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txtGSTIN" Class="form-control input-sm" Width="100%" autocomplete="off" MaxLength="15" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvGSTIN" runat="server" Display="Dynamic" ControlToValidate="txtGSTIN" CssClass="help-block" ErrorMessage="Please specify GSTIN" ValidationGroup="vgB2BInvoice"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revGSTIN" runat="server" Display="Dynamic" ControlToValidate="txtGSTIN" CssClass="help-block" ErrorMessage="Invalid format" ValidationExpression="^([0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1})$" ValidationGroup="vgB2BInvoice"></asp:RegularExpressionValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txtInvoiceNo" class="form-control input-sm" Width="100%" autocomplete="off" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvInvoiceNo" runat="server" Display="Dynamic" ControlToValidate="txtInvoiceNo" CssClass="help-block" ErrorMessage="Please specify Invoice No" ValidationGroup="vgB2BInvoice"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_InvoiceDate" Width="100%" placeholder="MM/DD/YYYY" autocomplete="off" CssClass="form-control input-sm date-picker_offline" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvInvoiceDate" runat="server" Display="Dynamic" ControlToValidate="txt_InvoiceDate" CssClass="help-block" ErrorMessage="Please specify Invoice Date" ValidationGroup="vgB2BInvoice"></asp:RequiredFieldValidator>
                        </div>
                    </td>

                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txtInvoiceValue" class="form-control input-sm" autocomplete="off" onkeypress="return isNumberKey(event,this);" onpaste="return false;" Width="100%" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvInvoiceValue" runat="server" Display="Dynamic" ControlToValidate="txtInvoiceValue" CssClass="help-block" ErrorMessage="Please specify Invoice Value" ValidationGroup="vgB2BInvoice"></asp:RequiredFieldValidator>
                        </div>
                    </td>

                    <uc1:uc_SupplyType runat="server" ID="uc_SupplyType" />
                    <td>
                        <div class="form-group">
                            <asp:DropDownList ID="ddl_InvoiceType" Class="form-control input-sm" Width="100%" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvInvoiceType" runat="server" Display="Dynamic" ControlToValidate="ddl_InvoiceType" CssClass="help-block" ErrorMessage="Please specify Invoice Type" ValidationGroup="vgB2BInvoice"></asp:RequiredFieldValidator>
                        </div>
                    </td>

                    <%--<asp:HiddenField ID="hdnInvoiceType" runat="server" Value='<%# Eval("InvoiceType") %>' />--%>
                    <td>
                        <asp:CheckBox ID="chkReverse" Width="100%" runat="server" /></td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txtECommerce" class="form-control input-sm" MaxLength="15" autocomplete="off" Width="100%" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revEcommerce" runat="server" Display="Dynamic" ControlToValidate="txtECommerce" CssClass="help-block" ErrorMessage="Invalid format" ValidationExpression="^([0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1})$" ValidationGroup="vgB2BInvoice"></asp:RegularExpressionValidator>
                        </div>
                    </td>

                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lkbInsert" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:LinkButton ID="lkbInsert" CssClass="btn btn-success btn-xs" CausesValidation="true" ValidationGroup="vgB2BInvoice" runat="server" CommandName="Insert"><i class="fa fa-plus"></i></asp:LinkButton>
                    </td>
                </tr>

            </InsertItemTemplate>
            <LayoutTemplate>
                <table class="table table-responsive">
                    <thead>
                        <tr>
                            <th style="width: 3%">
                                <asp:CheckBox ID="Chk1" CssClass="selectall" runat="server" /></th>
                            <th style="width: 15%">Receiver GSTIN/UIN</th>
                            <th style="width: 10%">Invoice No.</th>
                            <th style="width: 10%">Invoice Date</th>
                            <th style="width: 10%">Total Invoice Value &nbsp;(<i class="fa fa-rupee" style="font-size: 10px;"></i>)</th>
                            <th style="width: 10%">Place of Supply</th>
                            <th style="width: 10%">Supply Type</th>
                            <th style="width: 10%">Invoice Type</th>
                            <th style="width: 5%">Rev Chg</th>
                            <th style="width: 10%">E-Comm GSTIN</th>
                            <th style="width: 5%">Actions</th>
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
            <asp:DataPager ID="dpB2bInvoices" runat="server" PagedControlID="lv_B2b_Invoice" PageSize="10" class="btn-group-sm pager-buttons">
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


