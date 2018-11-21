<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_B2CL.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Offline.uc_B2CL" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>

<div class="box box-primary">
    <div class="box-body table-responsive no-padding">
        <asp:ListView ID="lv_B2CL" InsertItemPosition="LastItem" OnItemCreated="lv_B2CL_ItemCreated" OnItemDataBound="lv_B2CL_ItemDataBound" OnItemInserting="lv_B2CL_ItemInserting" runat="server" OnItemEditing="lv_B2CL_ItemEditing" OnItemUpdating="lv_B2CL_ItemUpdating" OnPagePropertiesChanging="lv_B2CL_PagePropertiesChanging" DataKeyNames="ValueId">
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
                        <asp:CheckBox ID="chkSelect" CssClass="selectone" runat="server" /></td>
                    <td>
                        <asp:TextBox ID="txtInvoiceNo" class="form-control input-sm" Width="100%" Text='<%# Eval("InvoiceNo") %>' runat="server" Disabled></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txt_InvoiceDate" Width="100%" Text='<%#Eval("InvoiceDate","{0:dd/MM/yyyy}") %>' placeholder="MM/DD/YYY" CssClass="form-control input-sm date-picker_offline" runat="server" Disabled></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtInvoiceValue" class="form-control input-sm" Width="100%" Text='<%# Eval("TotalInvoiceValue") %>' runat="server" Disabled></asp:TextBox></td>
                    <td>
                        <asp:DropDownList ID="ddlPos" Width="100%" class="form-control input-sm" runat="server" Disabled></asp:DropDownList>
                        <asp:HiddenField ID="hdnPos" runat="server" Value='<%# Eval("PlaceOfSupply") %>' />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_SupplyType" class="form-control input-sm" Width="100%" runat="server" Disabled></asp:DropDownList>
                        <asp:HiddenField ID="hdnSupplyType" runat="server" Value='<%# Eval("SupplyType") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="txtECommerce" class="form-control input-sm" Width="100%" Text='<%# Eval("ECommerce_GSTIN") %>' runat="server" Disabled></asp:TextBox></td>
                    <td>
                        <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn btn-primary btn-xs" CommandName="Edit" CommandArgument='<%# Eval("OfflineId") %>'  data-trigger="hover" data-placement="right" title="Edit"><i class="fa fa-pencil-square-o"></i></asp:LinkButton></td>
                </tr>
            </ItemTemplate>
            <EditItemTemplate>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkSelect" CssClass="selectone" runat="server" />
                        <td>
                            <div class="form-group">
                                <asp:TextBox ID="txtInvoiceNo" class="form-control input-sm" Width="100%" Text='<%# Eval("InvoiceNo") %>' runat="server" disabled></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfcInvoiceNo" runat="server" Display="Dynamic" ControlToValidate="txtInvoiceNo" CssClass="help-block" ErrorMessage="Please specify Invoice No" ValidationGroup="vgB2CLInvoiceEdit"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                        <td>
                            <div class="form-group">
                                <asp:TextBox ID="txt_InvoiceDate" Width="100%" Text='<%#Eval("InvoiceDate","{0:dd/MM/yyyy}") %>' placeholder="DD/MM/YYYY" CssClass="form-control input-sm date-picker_offline" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfcInvoiceDate" runat="server" Display="Dynamic" ControlToValidate="txt_InvoiceDate" CssClass="help-block" ErrorMessage="Please specify Invoice Date" ValidationGroup="vgB2CLInvoiceEdit"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                        <td>
                            <div class="form-group">
                                <asp:TextBox ID="txtInvoiceValue" class="form-control input-sm" Width="100%" Text='<%# Eval("TotalInvoiceValue") %>' autocomplete="off" onkeypress="return isNumberKey(event,this);" onpaste="return false;" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfcInvoiceValue" runat="server" Display="Dynamic" ControlToValidate="txtInvoiceValue" CssClass="help-block" ErrorMessage="Please specify Invoice Value" ValidationGroup="vgB2CLInvoiceEdit"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="rvInvoiceValue" runat="server" Display="Dynamic" CssClass="help-block" ControlToValidate="txtInvoiceValue" ErrorMessage="Invoice Value needs to be greater than 2,50,000" MinimumValue="250000" MaximumValue="99999999" ValidationGroup="vgB2CLInvoiceEdit" Type="Integer"></asp:RangeValidator>
                            </div>
                        </td>
                        <td>
                            <div class="form-group">
                                <asp:DropDownList ID="ddlPos" Width="100%" class="form-control input-sm" runat="server" disabled></asp:DropDownList>
                                <asp:HiddenField ID="hdnPos" runat="server" Value='<%# Eval("PlaceOfSupply") %>' />
                                <asp:RequiredFieldValidator ID="rfcPos" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlPos" CssClass="help-block" ErrorMessage="Please specify Place of Supply" ValidationGroup="vgB2CLInvoiceEdit"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_SupplyType" class="form-control input-sm" Width="100%" runat="server" disabled></asp:DropDownList>
                        <asp:HiddenField ID="hdnSupplyType" runat="server" Value='<%# Eval("SupplyType") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="txtECommerce" class="form-control input-sm" autocomplete="off" Width="100%" Text='<%# Eval("ECommerce_GSTIN") %>' runat="server" disabled></asp:TextBox></td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lkbUpdate" />
                            </Triggers>
                        </asp:UpdatePanel>
                            <asp:LinkButton ID="btnAddMore" CssClass="btn btn-primary btn-xs" CommandArgument='<%# Eval("ValueId") %>' ToolTip="Add Items" OnClick="btnAddMore_Click" runat="server"><i class="fa fa-plus-circle"></i></asp:LinkButton>
                            <asp:LinkButton ID="lkbUpdate" CssClass="btn btn-success btn-xs" CommandArgument='<%# Eval("ValueId") %>' ValidationGroup="vgB2CLInvoiceEdit" ToolTip="Update" CausesValidation="true" runat="server" CommandName="Update"><i class="fa fa-save"></i></asp:LinkButton>
                    </td>
                </tr>
            </EditItemTemplate>
            <InsertItemTemplate>
                <tr>
                    <td></td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txtInvoiceNo" class="form-control input-sm" autocomplete="off" Width="100%" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvInvoiceNo" runat="server" Display="Dynamic" ControlToValidate="txtInvoiceNo" CssClass="help-block" ErrorMessage="Please specify Invoice No" ValidationGroup="vgB2CLInvoice"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txt_InvoiceDate" Width="100%" autocomplete="off" placeholder="DD/MM/YYYY" CssClass="form-control input-sm date-picker_offline" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfcInvoiceDate" runat="server" Display="Dynamic" ControlToValidate="txt_InvoiceDate" CssClass="help-block" ErrorMessage="Please specify Invoice Date" ValidationGroup="vgB2CLInvoice"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:TextBox ID="txtInvoiceValue" class="form-control input-sm" Width="100%" autocomplete="off" onkeypress="return isNumberKey(event,this);" onpaste="return false;" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfcInvoiceValue" runat="server" Display="Dynamic" ControlToValidate="txtInvoiceValue" CssClass="help-block" ErrorMessage="Please specify Invoice Value" ValidationGroup="vgB2CLInvoice"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="rvInvoiceValue" runat="server" Display="Dynamic" ControlToValidate="txtInvoiceValue" CssClass="help-block" MinimumValue="250000" MaximumValue="99999999" ErrorMessage="Invoice Value needs to be greater than 2,50,000" ValidationGroup="vgB2CLInvoice" Type="Integer"></asp:RangeValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlPos" Width="100%" AutoPostBack="true" class="form-control input-sm" runat="server"></asp:DropDownList>
                            <%--<asp:HiddenField ID="hdnPos" runat="server" Value='<%# Eval("PlaceOfSupply") %>' />--%>
                            <asp:RequiredFieldValidator ID="rfcPos" runat="server" Display="Dynamic" ControlToValidate="ddlPos" InitialValue="0" CssClass="help-block" ErrorMessage="Please specify Place of Supply" ValidationGroup="vgB2CLInvoice"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <div class="form-group">
                            <asp:DropDownList ID="ddl_SupplyType" class="form-control input-sm" AutoPostBack="true" Width="100%" runat="server" disabled></asp:DropDownList>
                            <%--<asp:HiddenField ID="hdnSupplyType" runat="server" Value='<%# Eval("SupplyType") %>' />--%>
                            <asp:RequiredFieldValidator ID="rfcSupplyType" runat="server" Display="Dynamic" ControlToValidate="ddlPos" CssClass="help-block" ErrorMessage="Please specify Supply type" ValidationGroup="vgB2CLInvoice"></asp:RequiredFieldValidator>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="txtECommerce" class="form-control input-sm" autocomplete="off" Width="100%" runat="server" disabled></asp:TextBox></td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lkbInsert" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:LinkButton ID="lkbInsert" CssClass="btn btn-success btn-xs" CausesValidation="true" ValidationGroup="vgB2CLInvoice" runat="server" CommandName="Insert"><i class="fa fa-plus"></i></asp:LinkButton>
                    </td>
                </tr>
            </InsertItemTemplate>
            <LayoutTemplate>
                <table class="table table-responsive">
                    <thead>
                        <tr>
                            <th style="width: 5%;">
                                <asp:CheckBox ID="Chk1" CssClass="selectall" runat="server" /></th>
                            <th style="width: 15%;">Invoice No.</th>
                            <th style="width: 15%;">Invoice Date</th>
                            <th style="width: 13%;">Total Invoice Value &nbsp;(<i class="fa fa-rupee" style="font-size: 10px;"></i>)</th>
                            <th style="width: 10%;">Place of Supply</th>
                            <th style="width: 11%;">Supply Type</th>
                            <th style="width: 15%;">E-Commerce GSTIN</th>
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
        <asp:LinkButton ID="lkbDelete"  CssClass="btn btn-danger" OnClick="lkbDelete_Click" runat="server"><i class="fa fa-trash"></i>&nbsp;Delete</asp:LinkButton>
        <div class="pagination pagination-sm no-margin pull-right">
            <%--<div class="box-body dataTables_paginate paging_simple_numbers">--%>
            <asp:DataPager ID="dpB2CL" runat="server" PagedControlID="lv_B2CL" PageSize="10" class="btn-group-sm pager-buttons">
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
