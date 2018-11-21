<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_hsn.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_hsn" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>


<div class="box box-primary">
    <div class="box-body table-responsive no-padding">
        <asp:ListView ID="lv_HSN" runat="server" InsertItemPosition="LastItem" OnItemInserting="lv_HSN_ItemInserting" OnItemCreated="lv_HSN_ItemCreated" OnItemDataBound="lv_HSN_ItemDataBound" OnItemUpdating="lv_HSN_ItemUpdating" OnItemEditing="lv_HSN_ItemEditing" OnPagePropertiesChanging="lv_HSN_PagePropertiesChanging" DataKeyNames="ValueId">
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
                        <asp:CheckBox ID="chkSelect" CssClass="selectone" runat="server" />

                    </td>
                    <td>
                        <asp:TextBox ID="txt_HSN" Text='<%# Eval("GST_TRN_OFFLINE_INVOICE.HSN") %>' Class="form-control input-sm" Width="100%" runat="server" ReadOnly="true"></asp:TextBox>

                    </td>
                    <td>
                        <asp:TextBox ID="txt_Description" Text='<%# Eval("GST_TRN_OFFLINE_INVOICE.HSNDescription") %>' Width="100%" ReadOnly="true" Class="form-control input-sm" runat="server"></asp:TextBox>

                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_UQC" Class="form-control input-sm" Width="100%" runat="server" ReadOnly="true"></asp:DropDownList>
                        <asp:HiddenField ID="hdnUQC" runat="server" Value='<%# Eval("GST_TRN_OFFLINE_INVOICE.UQC") %>' />

                    </td>
                    <td><%--<%# Eval("GST_TRN_OFFLINE_INVOICE_DATAITEM.TotalQuantity") %>----%>
                        <asp:TextBox ID="txt_Quantity" Text='<%# DataBinder.Eval(Container.DataItem,"TotalQuantity") %>' Class="form-control input-sm" ReadOnly="true" Width="100%" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Value" Text='<%# DataBinder.Eval(Container.DataItem,"TotalValue") %>' Class="form-control input-sm" ReadOnly="true" Width="100%" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Taxable_value" Text='<%# DataBinder.Eval(Container.DataItem,"TotalTaxableValue") %>' Class="form-control input-sm" ReadOnly="true" Width="100%" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_IGST" Text='<%# DataBinder.Eval(Container.DataItem,"IGSTAmt") %>' Class="form-control input-sm" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_SGSTUTGST" Text='<%# DataBinder.Eval(Container.DataItem,"SGSTAmt") %>' Class="form-control input-sm" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_CGST" Text='<%# DataBinder.Eval(Container.DataItem,"CGSTAmt") %>' Class="form-control input-sm" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Cess" Text='<%# DataBinder.Eval(Container.DataItem,"CessAmt") %>' Class="form-control input-sm" Width="100%" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:LinkButton ID="lkbEdit" runat="server" CssClass="btn btn-primary btn-xs" CommandName="Edit" data-trigger="hover" data-placement="right" title="Edit"><i class="fa fa-pencil-square-o"></i></asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <EditItemTemplate>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkSelect" CssClass="selectone" runat="server" />

                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_HSN" Text='<%# Eval("GST_TRN_OFFLINE_INVOICE.HSN") %>' autocomplete="off" onkeypress="return isDigitKey(event,this);" onpaste="return false;" Class="form-control input-sm" Width="100%" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvHSN" runat="server" Display="Dynamic" ControlToValidate="txt_HSN" CssClass="help-block" ErrorMessage="Please specify HSN" ValidationGroup="vgHSN"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_Description" Text='<%# Eval("GST_TRN_OFFLINE_INVOICE.HSNDescription") %>' Width="100%" Class="form-control input-sm" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" Display="Dynamic" ControlToValidate="txt_Description" CssClass="help-block" ErrorMessage="Please specify Description" ValidationGroup="vgHSN"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_UQC" Class="form-control input-sm" Width="110px" runat="server"></asp:DropDownList>
                        <asp:HiddenField ID="hdnUQC" runat="server" Value='<%# Eval("GST_TRN_OFFLINE_INVOICE.UQC") %>' />
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_Quantity" Text='<%# DataBinder.Eval(Container.DataItem,"TotalQuantity") %>' Class="form-control input-sm" Width="100%" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvQuanity" runat="server" Display="Dynamic" ControlToValidate="txt_Quantity" CssClass="help-block" ErrorMessage="Please specify Total quantity" ValidationGroup="vgHSN"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_Value" Text='<%# DataBinder.Eval(Container.DataItem,"TotalValue") %>' autocomplete="off" onkeypress="return isNumberKey(event,this);" onpaste="return false;" Class="form-control input-sm" Width="100%" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvValue" runat="server" Display="Dynamic" ControlToValidate="txt_Value" CssClass="help-block" ErrorMessage="Please specify Total value" ValidationGroup="vgHSN"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Taxable_value" autocomplete="off" onkeypress="return isNumberKey(event,this);" onpaste="return false;" Text='<%# DataBinder.Eval(Container.DataItem,"TotalTaxableValue") %>' Class="form-control input-sm" Width="100%" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_IGST" autocomplete="off" onkeypress="return isNumberKey(event,this);" onpaste="return false;" Text='<%# DataBinder.Eval(Container.DataItem,"IGSTAmt") %>' Class="form-control input-sm" Width="100%" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_SGSTUTGST" autocomplete="off" onkeypress="return isNumberKey(event,this);" onpaste="return false;" Text='<%# DataBinder.Eval(Container.DataItem,"SGSTAmt") %>' Class="form-control input-sm" Width="100%" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_CGST" autocomplete="off" onkeypress="return isNumberKey(event,this);" onpaste="return false;"  Text='<%# DataBinder.Eval(Container.DataItem,"CGSTAmt") %>' Class="form-control input-sm" Width="100%" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Cess" Text='<%# DataBinder.Eval(Container.DataItem,"CessAmt") %>' Class="form-control input-sm" Width="100%" runat="server"></asp:TextBox>
                    </td>

                    <td>
                        <asp:LinkButton ID="lkbUpdate" runat="server" CssClass="btn btn-success btn-xs" ValidationGroup="vgHSN" CommandName="Update" ToolTip="Update" CommandArgument='<%# Eval("OfflineDataID") %>' data-trigger="hover" data-placement="right"><i class="fa fa-save"></i></asp:LinkButton>
                    </td>
                </tr>
            </EditItemTemplate>
            <InsertItemTemplate>
                <tr>
                    <td></td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_HSN" Class="form-control input-sm" onkeypress="return onlyNos(event,this);" onpaste="return false;" Width="100%" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvHSN" runat="server" Display="Dynamic" ControlToValidate="txt_HSN" CssClass="help-block" ErrorMessage="Please specify HSN" ValidationGroup="vgHSNSave"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_Description" Width="100%" Class="form-control input-sm" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" Display="Dynamic" ControlToValidate="txt_Description" CssClass="help-block" ErrorMessage="Please specify Description" ValidationGroup="vgHSNSave"></asp:RequiredFieldValidator>
                        </div>

                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_UQC" Class="form-control input-sm" Width="100%" runat="server"></asp:DropDownList>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_Quantity" Class="form-control input-sm" autocomplete="off" Width="100%" onkeypress="return onlyDecNos(event,this);" onpaste="return false;" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvQuanity" runat="server" Display="Dynamic" ControlToValidate="txt_Quantity" CssClass="help-block" ErrorMessage="Please specify Total quantity" ValidationGroup="vgHSNSave"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_Value" Class="form-control input-sm" autocomplete="off" Width="100%" onkeypress="return onlyDecNos(event,this);" onpaste="return false;" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvValue" runat="server" Display="Dynamic" ControlToValidate="txt_Value" CssClass="help-block" ErrorMessage="Please specify Total value" ValidationGroup="vgHSNSave"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Taxable_value" Class="form-control input-sm" autocomplete="off" Width="100%" onkeypress="return onlyDecNos(event,this);" onpaste="return false;" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_IGST" Class="form-control input-sm" Width="100%" autocomplete="off" onkeypress="return onlyDecNos(event,this);" onpaste="return false;" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_SGSTUTGST" Class="form-control input-sm" Width="100%" autocomplete="off" onkeypress="return onlyDecNos(event,this);" onpaste="return false;" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_CGST" Class="form-control input-sm" Width="100%" autocomplete="off" onkeypress="return onlyDecNos(event,this);" onpaste="return false;" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Cess" Class="form-control input-sm" Width="100%" autocomplete="off" onkeypress="return onlyDecNos(event,this);" onpaste="return false;" runat="server"></asp:TextBox>
                    </td>

                    <td>
                        <asp:LinkButton ID="lkbInsert" CssClass="btn btn-success btn-xs" runat="server" ValidationGroup="vgHSNSave" ToolTip="Update" CommandName="Insert"><i class="fa fa-save"></i></asp:LinkButton>
                    </td>
                </tr>
            </InsertItemTemplate>
            <LayoutTemplate>
                <table class="table table-responsive table-condensed">
                    <thead>
                        <tr>
                            <th style="vertical-align: bottom; width: 2%" rowspan="2">
                                <asp:CheckBox ID="Chk1" CssClass="selectall" runat="server" /></th>
                            <th style="vertical-align: bottom; width: 10%" rowspan="2">HSN</th>
                            <th style="vertical-align: bottom; width: 10%" rowspan="2">Description</th>
                            <th style="vertical-align: bottom; width: 9%" rowspan="2">UQC</th>
                            <th style="vertical-align: bottom; width: 5%" rowspan="2">Total Qty</th>
                            <th style="vertical-align: bottom; width: 9%" rowspan="2">Total Value (<i class="fa fa-rupee" style="font-size: 10px;"></i>)</th>
                            <th style="vertical-align: bottom; width: 10%" rowspan="2">TotalTaxable Value (<i class="fa fa-rupee" style="font-size: 10px;"></i>)</th>
                            <th style="text-align: center; vertical-align: bottom" colspan="4">Amount</th>
                            <th style="vertical-align: bottom; width: 4%" rowspan="2">Actions</th>
                        </tr>
                        <tr>
                            <th style="width: 10%">IGST(<i class="fa fa-rupee" style="font-size: 10px;"></i>)</th>
                            <th style="width: 10%">SGST/UTGST(<i class="fa fa-rupee" style="font-size: 10px;"></i>)</th>
                            <th style="width: 10%">CGST(<i class="fa fa-rupee" style="font-size: 10px;"></i>)</th>
                            <th style="width: 10%">Cess(<i class="fa fa-rupee" style="font-size: 10px;"></i>)</th>
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
        <asp:LinkButton ID="lkbDelete" OnClick="lkbDelete_Click" CssClass="btn btn-danger pull-left" runat="server"><i class="fa fa-trash"></i>&nbsp;Delete</asp:LinkButton>
        <div class="pagination pagination-sm no-margin pull-right">
            <%--<div class="box-body dataTables_paginate paging_simple_numbers">--%>
            <asp:DataPager ID="dpHSN" runat="server" PagedControlID="lv_HSN" PageSize="10" class="btn-group-sm pager-buttons">
                <Fields>
                    <asp:NextPreviousPagerField ShowLastPageButton="False" ShowNextPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                    <asp:NumericPagerField ButtonType="Button" RenderNonBreakingSpacesBetweenControls="false" NumericButtonCssClass="btn btn-primary" CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn" />
                    <asp:NextPreviousPagerField ShowFirstPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                </Fields>
            </asp:DataPager>
        </div>
    </div>
      <uc1:uc_sucess runat="server" ID="uc_sucess" />
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
