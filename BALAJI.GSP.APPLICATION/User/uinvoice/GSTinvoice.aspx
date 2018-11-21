<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/User/User.master" CodeBehind="GSTinvoice.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.User.uinvoice.GSTinvoice" ViewStateMode="Enabled" %>

<%@ MasterType VirtualPath="~/User/User.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>

<%--<%@ Register Src="~/UC/uc_OTP.ascx" TagPrefix="uc1" TagName="uc_OTP" %>--%>
<%@ Register Src="~/UC/UC_Invoice/uc_invoiceR.ascx" TagPrefix="uc1" TagName="uc_invoiceR" %>
<%@ Register Src="~/UC/uc_GSTNUsers.ascx" TagPrefix="uc1" TagName="uc_GSTNUsers" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>
<%@ Register Src="~/UC/UC_Invoice/uc_PerchaseRegister.ascx" TagPrefix="uc1" TagName="uc_PerchaseRegister" %>
<%@ Register Src="~/UC/uc_Header.ascx" TagPrefix="uc1" TagName="uc_Header" %>



<%--<%@ Register Src="~/UC/UC_Invoice/uc_seller.ascx" TagPrefix="uc1" TagName="uc_seller" %>--%>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%--<script src="JavaScript/GSTinvoice.js"></script>--%>
    <%--<div class="row">
        <div class="col-lg-12">
            <h2 class="page-header">Add Invoice</h2>
        </div>
        <!-- /.col-lg-12 -->
    </div>--%>

    <div class="content-header">
        <h1>Add Invoice

        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Invoice</a></li>
            <li class="active">Add Invoice</li>
        </ol>
    </div>
    <%-- ankita--%>

    <div class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <div class="row">
                    <div id="InvoiceReturn" runat="server" visible="false">
                        <uc1:uc_GSTNUsers runat="server" ID="uc_GSTNUsers" />
                    </div>
                    <%--modal popup--%>
                    <%--  <div class="modal modal-warning fade"  id="addInvoiceModelWarningMessage" role="dialog" aria-labelledby="addInvoiceModelWarningMessage"  aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                    &times;
                                </button>
                                <h4 class="modal-title">
                                    <i class="fa fa-exclamation-triangle"></i>
                                    <asp:Label ID="Label3" runat="server" Text="Warning Message"></asp:Label>
                                </h4>
                            </div>
                            <div class="modal-body">
                                <p>
                                    <asp:Label ID="lblWarningadd" runat="server"></asp:Label>
                                </p>
                            </div>
                            <div class="modal-footer">
                                <asp:Button class="btn btn-outline pull-left" ID="btnAccept" runat="server" Text="Accept" OnClick="btnAccept_Click" OnClientClick="$('.modal-backdrop').remove(); $('body').removeClass('modal-open');" />
                                <asp:Button class="btn btn-outline pull-right" ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click" OnClientClick="$('.modal-backdrop').remove(); $('body').removeClass('modal-open');"/>
                            </div>
                        </div>
                    </div>
                </div>--%>
                </div>

                <%--<div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove">
                        <i class="fa fa-remove"></i>
                    </button>
                </div>--%>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>
                                Invoice :</label>
                            <asp:DropDownList ID="ddlInvoiceType" CssClass="form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlInvoiceType_SelectedIndexChanged" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>
                                Invoice Type :</label>
                            <asp:DropDownList ID="rblInvoicePriority" CssClass="form-control input-sm" TabIndex="0" AutoPostBack="true" OnSelectedIndexChanged="rblInvoicePriorityIndex_Changed" runat="server">
                            </asp:DropDownList>
                            <%-- <asp:RadioButtonList ID="rblInvoicePriority"  AutoPostBack="true" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblInvoicePriorityIndex_Changed">
                                </asp:RadioButtonList>--%>
                        </div>
                    </div>
                    <div class="col-md-1">
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>
                                <asp:Label ID="lblOrderDate" Visible="false" runat="server" Text="Order Date :"></asp:Label>
                            </label>
                            <asp:TextBox ID="txtOrderDate" Visible="false" runat="server" Text='<%#DateTime.Now %>' CssClass="form-control input-sm pull-right" autocomplete="off"></asp:TextBox>
                            <cc3:CalendarExtender ID="ceOrderDate" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate" TodaysDateFormat="dd/MM/yyyy" SelectedDate='<%#DateTime.Now %>' TargetControlID="txtOrderDate" runat="server" />
                            <label>
                                <asp:Label ID="lblRegularChallanMapped" Visible="true" runat="server" Text="Advance Challan No :"></asp:Label>
                            </label>
                            <asp:ListBox ID="lboxRegularChallanMapped" Style="width: 150px;" Visible="true" runat="server" SelectionMode="multiple" Rows="2"></asp:ListBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>
                                <asp:Label ID="lblinvoiceNo" Visible="false" runat="server" TabIndex="1" Text="Invoice No :"></asp:Label>
                            </label>
                            <asp:TextBox ID="txtInvoiceNumber" Visible="false" autocomplete="off" CssClass="form-control input-sm pull-right" TabIndex="1" runat="server" MaxLength="16" ReadOnly="true"></asp:TextBox>
                            <label>
                                <asp:Label ID="lblRegularMapped" Visible="true" runat="server" TabIndex="1" Text="Advance Voucher No :"></asp:Label>
                            </label>
                            <asp:ListBox ID="lboxRegularMapped" Style="width: 150px;" TabIndex="1" Visible="true" runat="server" SelectionMode="multiple" Rows="2"></asp:ListBox>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>
                                Invoice Date :</label>
                            <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="form-control input-sm" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-xs-12">
                                <h5><i class="fa fa-user"></i>&nbsp;SELLER </h5>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5">
                                <div class="form-group">
                                    <asp:Literal ID="litSelletGSTIN" runat="server">GSTIN</asp:Literal>
                                    <asp:TextBox ID="txtSellerGSTIN" runat="server" class="form-control input-sm" MaxLength="15" TabIndex="1" ReadOnly="true" autocomplete="off" OnTextChanged="txtSellerGSTIN_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xs-7">
                                <div class="form-group">
                                    <asp:Literal ID="litSellerName" runat="server">Name</asp:Literal>
                                    <asp:TextBox ID="txtSellerName" runat="server" class="form-control input-sm" ReadOnly="true" TabIndex="2" autocomplete="off" OnTextChanged="txtSellerName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="form-group">
                                    <asp:Literal ID="litAddress" runat="server">Address</asp:Literal>
                                    <asp:TextBox ID="txtSellerAddress" runat="server" CssClass="form-control input-sm" autocomplete="off" TabIndex="3" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-xs-12">
                                <h5><i class="fa fa-user"></i>&nbsp;RECEIVER (Billed to) </h5>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5">
                                <div class="form-group">
                                    <asp:Literal ID="litRecieverGSTIN" runat="server">GSTIN</asp:Literal>
                                    <asp:TextBox ID="txtRecieverGSTIN" runat="server" class="form-control input-sm" TabIndex="4" placeholder="Enter Receiver's GSTIN No." MaxLength="15" autocomplete="off" OnTextChanged="txtRecieverGSTIN_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xs-7">
                                <div class="form-group">
                                    <asp:Literal ID="Literal2" runat="server">Name</asp:Literal>
                                    <asp:TextBox ID="txtRecieverName" runat="server" class="form-control input-sm alphanumeric" autocomplete="off" TabIndex="5" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="form-group">
                                    <asp:Literal ID="Literal6" runat="server">Address</asp:Literal>
                                    <asp:TextBox ID="txtRecieverAddress" runat="server" CssClass="form-control input-sm" autocomplete="off" TabIndex="6" ReadOnly="true"></asp:TextBox>
                                    <div style="display: none;">
                                        <asp:Label ID="lblReceiverState" runat="server" Text="State"></asp:Label>
                                        <asp:TextBox ID="txtRecieverState" runat="server" CssClass="form-control input-sm" TabIndex="8" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div style="display: none;">
                                        <label>
                                            State Code</label>
                                        <asp:TextBox ID="txtRecieverStateCode" runat="server" TabIndex="8" Visible="false" CssClass="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-xs-12">
                                <h5><i class="fa fa-user"></i>&nbsp;CONSIGNEE (Shipped to) </h5>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-5">
                                <div class="form-group">
                                    <asp:Literal ID="litConsigneeGSTIN" runat="server">GSTIN</asp:Literal>
                                    <asp:TextBox ID="txtConsigneeGSTIN" runat="server" class="form-control input-sm" placeholder="Enter Consignee's GSTIN No." MaxLength="15" autocomplete="off" OnTextChanged="txtConsigneeGSTIN_TextChanged" AutoPostBack="true" TabIndex="7"></asp:TextBox>
                                    <%--<asp:RegularExpressionValidator ControlToValidate="txtConsigneeGSTIN" ID="RegularExpressionValidator1" ValidationGroup="saveInvoice" Display="Dynamic" ValidationExpression="[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1}" runat="server" ErrorMessage="GSTIN no is invalid."></asp:RegularExpressionValidator>
                                    --%>
                                </div>
                            </div>
                            <div class="col-xs-7">
                                <div class="form-group">
                                    <asp:Literal ID="Literal4" runat="server">Name</asp:Literal>
                                    <asp:TextBox ID="txtConsigneeName" runat="server" class="form-control input-sm alphanumeric" TabIndex="8" autocomplete="off" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="form-group">
                                    <asp:Literal ID="Literal3" runat="server">Address</asp:Literal>
                                    <asp:TextBox ID="txtConsigneeAddress" runat="server" CssClass="form-control  input-sm" autocomplete="off" TabIndex="9" ReadOnly="true"></asp:TextBox>
                                    <div style="display: none;">
                                        <asp:Label ID="lblConsigneeState" runat="server" Text="State"></asp:Label>
                                        <asp:TextBox ID="txtConsigneeState" runat="server" CssClass="form-control input-sm" TabIndex="11" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div style="display: none;">
                                        <label>
                                            State Code</label>
                                        <asp:TextBox ID="txtConsigneeStateCode" runat="server" Visible="false" TabIndex="12" CssClass="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <%--<div class="box-header">
                        <h3 class="box-title">Details of Goods</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                            <button type="button" class="btn btn-box-tool" data-widget="remove">
                                <i class="fa fa-remove"></i>
                            </button>
                        </div>
                    </div>--%>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:GridView ID="gvItems" CssClass="table table-responsive no-padding table-striped table-bordered table-condensed" runat="server" Width="100%" UseAccessibleHeader="true" AutoGenerateColumns="False" OnSelectedIndexChanged="gvItems_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="#" Visible="false">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HSN/SAC" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGoodService" autocomplete="off" MaxLength="8" onkeypress="return onlyNos(event,this);" CssClass="form-control input-sm" OnTextChanged="GetGoodsOrServiceInfo" AutoPostBack="true" runat="server"></asp:TextBox>
                                            <cc3:AutoCompleteExtender
                                                runat="server"
                                                ID="AutoCompleteExtender1"
                                                TargetControlID="txtGoodService"
                                                ServicePath="~/Service/AutoPopulate.asmx"
                                                ServiceMethod="GetItems"
                                                MinimumPrefixLength="2"
                                                CompletionInterval="10"
                                                EnableCaching="true"
                                                CompletionSetCount="10">
                                            </cc3:AutoCompleteExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGoodServiceDesciption" CssClass="form-control input-sm" ReadOnly="true" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty." ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQty" MaxLength="6" autocomplete="off" CssClass="form-control input-sm" AutoPostBack="true" onkeypress="return isNumberKey(event,this);" OnTextChanged="QtyCalculate" runat="server"></asp:TextBox>
                                            <asp:RangeValidator ID="rvQTY" ForeColor="Red" Font-Size="10px" Type="Double" ControlToValidate="txtQty" Display="Dynamic" MinimumValue="1.00" MaximumValue="789065343216543330.00" SetFocusOnError="true" ValidationGroup="saveInvoice" runat="server" ErrorMessage="Enter Valid Qty"></asp:RangeValidator>
                                            <%--<cc3:maskededitextender id="metxtQty" runat="server" targetcontrolid="txtQty" mask="9999.99" masktype="Number" inputdirection="RightToLeft" />--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUnit" autocomplete="off" CssClass="form-control input-sm" ReadOnly="true" AutoPostBack="true" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate(per item)" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRate" CssClass="form-control input-sm" MaxLength="10" autocomplete="off" onkeypress="return onlyDecNos(event,this);" OnTextChanged="QtyCalculate" AutoPostBack="true" runat="server"></asp:TextBox>
                                            <asp:RangeValidator ID="rvRate" ForeColor="Red" Font-Size="10px" Type="Double" ControlToValidate="txtRate" Display="Dynamic" MinimumValue="0.01" MaximumValue="789065343216543330.00" SetFocusOnError="true" ValidationGroup="saveInvoice" runat="server" ErrorMessage="Enter Valid Rate"></asp:RangeValidator>
                                            <%--  <cc3:maskededitextender id="metxtRate" runat="server" targetcontrolid="txtRate" mask="999.99" masktype="Number" inputdirection="RightToLeft" />--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total(<i class='fa fa-inr' aria-hidden='true'></i>)" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="txtTotal" autocomplete="off" CssClass="form-control input-sm" runat="server"></asp:Label>
                                            <%--  <asp:TextBox ID="txtTotal" ReadOnly="true" runat="server"></asp:TextBox>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discount(<i class='fa fa-percent' aria-hidden='true' style='font-size:10px;' ></i>)" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDiscount" MaxLength="5" CssClass="form-control input-sm" autocomplete="off" onkeypress="return onlyDecNos(event,this);" OnTextChanged="QtyCalculate" AutoPostBack="true" runat="server"></asp:TextBox>
                                            <asp:RangeValidator ID="rvDiscount" ForeColor="Red" Font-Size="10px" Type="Double" ControlToValidate="txtDiscount" Display="Dynamic" MinimumValue="0.00" MaximumValue="100.00" SetFocusOnError="true" ValidationGroup="saveInvoice" runat="server" ErrorMessage="0-100"></asp:RangeValidator>
                                            <%--<cc3:maskededitextender id="meDsicount" runat="server" targetcontrolid="txtDiscount" mask="99.99" masktype="Number" inputdirection="RightToLeft" />--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="10%" HeaderText="Taxable Val.(<i class='fa fa-inr' aria-hidden='true'></i>)">
                                        <ItemTemplate>
                                            <asp:Label ID="txtTaxableValue" CssClass="form-control input-sm" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        Vendor List:</label>
                                    <asp:DropDownList ID="ddlVendor" AutoPostBack="true" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>
                                      E-Way Bill No:</label>
                                    <asp:DropDownList ID="ddlTransShipment" AutoPostBack="true" OnSelectedIndexChanged="ddlTrans_SelectedIndexChanged" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>
                                        Freight:</label>
                                    <asp:TextBox ID="txtFreight" autocomplete="off" runat="server" placeholder="Freight" onkeypress="return isNumberKey(event,this);" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>
                                        Insurance:</label>
                                    <asp:TextBox ID="txtInsurance" autocomplete="off" runat="server" MaxLength="8" placeholder="Insurance" onkeypress="return isNumberKey(event,this);" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        Packing/Forwarding Charges:</label>
                                    <asp:TextBox ID="txtPackingCharges" autocomplete="off" runat="server" MaxLength="8" placeholder="Packing And Forwarding Charges" onkeypress="return isNumberKey(event,this);" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <asp:LinkButton ID="btnSaveInvoice" ValidationGroup="saveInvoice" Style="margin-right: 10px;" runat="server" UseSubmitBehavior="false"
                                OnClientClick="this.disabled='true'; this.value='Please wait...'" OnClick="btnSaveInvoice_Click" CssClass="btn btn-primary pull-right"><i class="fa fa-save lg"></i><span> Save Invoice</span></asp:LinkButton>
                            &nbsp;&nbsp;
                              &nbsp; &nbsp;  
                            <asp:LinkButton ID="lkbPreview" ValidationGroup="saveInvoice" runat="server" CssClass="btn btn-success pull-right" Style="margin-right: 10px;" OnClick="btnPreview_Click"><i class="fa fa-eye lg"></i><span> Preview</span></asp:LinkButton>

                            <%--    <asp:Button ID="btnSaveInvoice" runat="server" Text="Save Invoice" CssClass="btn btn-primary pull-right" />--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <%--ankita--%>


        <div id="HSNDtls" style="display: none;" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-danger">
                        <table class="table" style="display: none;">
                            <tbody>
                                <tr>
                                    <%--<td>
                                        <asp:CheckBox ID="chkAdvancePayment" runat="server" Text="Advance Payment" OnCheckedChanged="chkAdvancePayment_CheckedChanged" AutoPostBack="true" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkExport" runat="server" Text="Export" OnCheckedChanged="chkExport_CheckedChanged" AutoPostBack="true" />
                                    </td>--%>
                                </tr>
                                <tr>
                                    <th style="width: 10%;"><span>HSN/SAC</span></th>
                                    <th style="width: 30%;"><span>Description</span></th>
                                    <th style="width: 10%;"><span>Qty.</span></th>
                                    <th style="width: 10%;"><span>Unit</span></th>
                                    <th style="width: 10%;"><span>Rate (per item)</span></th>
                                    <th style="width: 10%;"><span>Total</span> (<i class='fa fa-inr' aria-hidden='true'></i>)</th>
                                    <th style="width: 10%;">Discount (%)</th>
                                    <th style="width: 10%;"><span>Taxable Value</span> (<i class='fa fa-inr' aria-hidden='true'></i>)</th>
                                    <%-- <th>#</th>--%>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtGoodService1" runat="server" ReadOnly="true" class="form-control input-sm" autocomplete="off" MaxLength="8" onkeypress="return isDigitKey(event,this);" OnTextChanged="GetGoodsOrServiceInfo" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGoodServiceDesciption1" runat="server" class="form-control input-sm" ReadOnly="true" MaxLength="170"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQty1" runat="server" class="form-control input-sm" ReadOnly="true" MaxLength="4" onkeypress="return isNumberKey(event,this);" OnTextChanged="QtyCal" autocomplete="off" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUnit1" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRate1" runat="server" class="form-control input-sm" ReadOnly="true" MaxLength="10" onkeypress="return isNumberKey(event,this);" autocomplete="off" OnTextChanged="GetTotalAmnt" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotal1" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDiscount1" runat="server" class="form-control input-sm" ReadOnly="true" MaxLength="5" onkeypress="return isNumberKey(event,this);" autocomplete="off" OnTextChanged="GetTaxValue" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTaxableValue1" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <%-- <td id="img_edit" runat="server" visible="false">

                                        <asp:ImageButton ID="imageEdit1" runat="server" ImageAlign="Left" Enabled="false" OnClick="imageEdit" Style="position: absolute;" ToolTip="Edit Line Item 1"></asp:ImageButton>
                                    </td>--%>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtGoodService2" runat="server" ReadOnly="true" autocomplete="off" class="form-control input-sm" MaxLength="8" onkeypress="return isDigitKey(event,this);" OnTextChanged="GetGoodsOrServiceInfo" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGoodServiceDesciption2" runat="server" ReadOnly="true" class="form-control input-sm" MaxLength="200"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQty2" runat="server" autocomplete="off" ReadOnly="true" class="form-control input-sm" onkeypress="return isNumberKey(event,this);" MaxLength="4" OnTextChanged="QtyCal" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUnit2" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRate2" runat="server" autocomplete="off" ReadOnly="true" class="form-control input-sm" onkeypress="return isNumberKey(event,this);" MaxLength="10" OnTextChanged="GetTotalAmnt" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotal2" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDiscount2" runat="server" autocomplete="off" ReadOnly="true" class="form-control input-sm" MaxLength="5" onkeypress="return isNumberKey(event,this);" OnTextChanged="GetTaxValue" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTaxableValue2" runat="server" class="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <%-- <td>
                                        <asp:ImageButton ID="imageEdit2" runat="server" ImageAlign="Left" Enabled="false" OnClick="imageEdit" ImageUrl="../../Images/orderedList1.png" Style="position: absolute;" ToolTip="Edit Line Item 2" />
                                    </td>--%>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtGoodService3" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="8" onkeypress="return isDigitKey(event,this);" OnTextChanged="GetGoodsOrServiceInfo" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGoodServiceDesciption3" runat="server" ReadOnly="true" class="form-control" MaxLength="200"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQty3" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="4" OnTextChanged="QtyCal" AutoPostBack="true" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUnit3" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRate3" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="10" onkeypress="return isNumberKey(event,this);" OnTextChanged="GetTotalAmnt" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotal3" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDiscount3" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="5" onkeypress="return isNumberKey(event,this);" OnTextChanged="GetTaxValue" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTaxableValue3" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtGoodService4" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="8" onkeypress="return isDigitKey(event,this);" OnTextChanged="GetGoodsOrServiceInfo" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGoodServiceDesciption4" runat="server" ReadOnly="true" class="form-control" MaxLength="200"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQty4" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="4" OnTextChanged="QtyCal" AutoPostBack="true" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUnit4" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRate4" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="10" onkeypress="return isNumberKey(event,this);" OnTextChanged="GetTotalAmnt" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotal4" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDiscount4" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="5" onkeypress="return isNumberKey(event,this);" OnTextChanged="GetTaxValue" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTaxableValue4" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <%-- <td>
                                        <asp:ImageButton ID="imageEdit4" runat="server" Enabled="false" ImageAlign="Left" OnClick="imageEdit" ImageUrl="../../Images/orderedList1.png" Style="position: absolute;" ToolTip="Edit Line Item 4" />
                                    </td>--%>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtGoodService5" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="8" onkeypress="return isDigitKey(event,this);" OnTextChanged="GetGoodsOrServiceInfo" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGoodServiceDesciption5" runat="server" ReadOnly="true" class="form-control" MaxLength="200"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQty5" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="4" OnTextChanged="QtyCal" AutoPostBack="true" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUnit5" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRate5" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="10" onkeypress="return isNumberKey(event,this);" OnTextChanged="GetTotalAmnt" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotal5" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDiscount5" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="5" onkeypress="return isNumberKey(event,this);" OnTextChanged="GetTaxValue" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTaxableValue5" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <%-- <td>
                                        <asp:ImageButton ID="imageEdit5" runat="server" Enabled="false" ImageAlign="Left" OnClick="imageEdit" ImageUrl="../../Images/orderedList1.png" Style="position: absolute;" ToolTip="Edit Line Item 5" />
                                    </td>--%>
                                </tr>
                                <tr id="lineItem6" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtGoodService6" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="8" onkeypress="return isDigitKey(event,this);" OnTextChanged="GetGoodsOrServiceInfo" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGoodServiceDesciption6" runat="server" ReadOnly="true" class="form-control" MaxLength="200"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQty6" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="4" OnTextChanged="QtyCal" AutoPostBack="true" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUnit6" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRate6" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="10" onkeypress="return isNumberKey(event,this);" OnTextChanged="GetTotalAmnt" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotal6" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDiscount6" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="5" onkeypress="return isNumberKey(event,this);" OnTextChanged="GetTaxValue" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTaxableValue6" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                    </td>
                                    <%-- <td>
                                        <asp:ImageButton ID="imageEdit6" runat="server" Enabled="false" ImageAlign="Left" OnClick="imageEdit" ImageUrl="../../Images/orderedList1.png" Style="position: absolute;" ToolTip="Edit Line Item 6" />
                                    </td>--%>
                                </tr>
                                <tr id="lineItem7" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtGoodService7" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="8" onkeypress="return isDigitKey(event,this);" OnTextChanged="GetGoodsOrServiceInfo" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGoodServiceDesciption7" runat="server" ReadOnly="true" class="form-control" MaxLength="200"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQty7" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="4" OnTextChanged="QtyCal" AutoPostBack="true" onkeypress="return isDigitKey(event,this);"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUnit7" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRate7" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="10" onkeypress="return isNumberKey(event,this);" OnTextChanged="GetTotalAmnt" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotal7" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDiscount7" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="5" onkeypress="return isNumberKey(event,this);" OnTextChanged="GetTaxValue" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTaxableValue7" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <%--<td>
                                        <asp:ImageButton ID="imageEdit7" runat="server" Enabled="false" ImageAlign="Left" OnClick="imageEdit" ImageUrl="../../Images/orderedList1.png" Style="position: absolute;" ToolTip="Edit Line Item 7" />
                                    </td>--%>
                                </tr>
                                <tr id="lineItem8" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtGoodService8" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="8" onkeypress="return isDigitKey(event,this);" OnTextChanged="GetGoodsOrServiceInfo" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGoodServiceDesciption8" runat="server" ReadOnly="true" class="form-control" MaxLength="200"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQty8" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="4" OnTextChanged="QtyCal" AutoPostBack="true" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUnit8" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRate8" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="10" onkeypress="return isNumberKey(event,this);" OnTextChanged="GetTotalAmnt" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotal8" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDiscount8" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="5" onkeypress="return isNumberKey(event,this);" OnTextChanged="GetTaxValue" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTaxableValue8" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <%--<td>
                                        <asp:ImageButton ID="imageEdit8" runat="server" Enabled="false" ImageAlign="Left" OnClick="imageEdit" ImageUrl="../../Images/orderedList1.png" Style="position: absolute;" ToolTip="Edit Line Item 8" />
                                    </td>--%>
                                </tr>
                                <tr id="lineItem9" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtGoodService9" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="8" onkeypress="return isDigitKey(event,this);" OnTextChanged="GetGoodsOrServiceInfo" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGoodServiceDesciption9" runat="server" ReadOnly="true" class="form-control" MaxLength="200"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQty9" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="4" OnTextChanged="QtyCal" AutoPostBack="true" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUnit9" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRate9" runat="server" ReadOnly="true" class="form-control" MaxLength="10" onkeypress="return isNumberKey(event,this);" OnTextChanged="GetTotalAmnt" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotal9" runat="server" autocomplete="off" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDiscount9" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="5" onkeypress="return isNumberKey(event,this);" OnTextChanged="GetTaxValue" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTaxableValue9" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <%--<td>
                                        <asp:ImageButton ID="imageEdit9" runat="server" Enabled="false" ImageAlign="Left" OnClick="imageEdit" ImageUrl="../../Images/orderedList1.png" Style="position: absolute;" ToolTip="Edit Line Item 9" />
                                    </td>--%>
                                </tr>
                                <tr id="lineItem10" runat="server">
                                    <td>
                                        <asp:TextBox ID="txtGoodService10" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="8" onkeypress="return isDigitKey(event,this);" OnTextChanged="GetGoodsOrServiceInfo" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGoodServiceDesciption10" runat="server" ReadOnly="true" class="form-control" MaxLength="200"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQty10" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="4" OnTextChanged="QtyCal" AutoPostBack="true" onkeypress="return isNumberKey(event,this);"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUnit10" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRate10" runat="server" autocomplete="off" ReadOnly="true" class="form-control" MaxLength="10" onkeypress="return isNumberKey(event,this);" OnTextChanged="GetTotalAmnt" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotal10" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDiscount10" autocomplete="off" runat="server" ReadOnly="true" class="form-control" MaxLength="5" onkeypress="return isNumberKey(event,this);" OnTextChanged="GetTaxValue" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTaxableValue10" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <%--<td>
                                        <asp:ImageButton ID="imageEdit10" runat="server" Enabled="false" ImageAlign="Left" OnClick="imageEdit" ImageUrl="../../Images/orderedList1.png" Style="position: absolute;" ToolTip="Edit Line Item 10" />
                                    </td>--%>
                                </tr>
                            </tbody>
                        </table>
                        <div class="row" style="display: none;">
                            <div class="col-lg-2 col-md-2 col-xs-2">
                                <br />
                                <asp:LinkButton ID="btnAddMore" runat="server" CssClass="btn" OnClick="btnAddMore_Click" data-trigger="hover" data-placement="right" title="Add More Line Items"><span class="glyphicon glyphicon-plus-sign" style="color:green"></span></asp:LinkButton>
                                <asp:LinkButton ID="btnRemoveMore" runat="server" CssClass="btn" OnClick="btnRemoveMore_Click" data-trigger="hover" data-placement="right" title="Remove Additional Line Items"><span class="glyphicon glyphicon-minus-sign" style="color:red"></span></asp:LinkButton>
                            </div>
                        </div>
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary" Visible="false" OnClick="btnBack_Click" Width="100px" />
                        <asp:Button ID="btnDiscard" runat="server" Text="Discard" CssClass="btn btn-primary" Visible="false" OnClick="btnDiscardClick" Width="100px" />

                        <%-- </div>--%>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <%--<uc1:uc_OTP runat="server" ID="uc_OTP" />--%>
    </div>
    <asp:HiddenField ID="hdnIsFirstLineEntry" runat="server" Value="0" />
    <asp:HiddenField ID="hdnLID" runat="server" Value="0" />
    <asp:HiddenField ID="hdnSConsigneeStateName" runat="server" />
    <asp:HiddenField ID="hdnReceiverStateName" runat="server" />
    <asp:HiddenField ID="hdnSellerStateName" runat="server" />
    <asp:HiddenField ID="hdnSellerStateCode" runat="server" />
    <asp:Button ID="btnTargetHSNO" runat="server" Text="Button" Visible="false" OnClick="btnTargetHSNO_Click" />
    <div class="modal fade" id="myModalhsn" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;
                            </button>
                            <h4 class="modal-title">
                                <asp:Label ID="lblModalTitle" runat="server" Text="Notified HSN No."></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <asp:ListView ID="lvHSNData" DataKeyNames="Tarrif,SerialNo" runat="server">
                                <EmptyDataTemplate>
                                    <table class="table table-striped">
                                        <tr>
                                            <td>No data was returned.</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Container.DataItemIndex + 1%>.</td>
                                        <td>
                                            <asp:Label ID="lblSN" runat="server" Text='<%# Eval("SerialNo") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblHSN" runat="server" Text='<%# Eval("HSNNumber") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNN" runat="server" Text='<%# Eval("NotificationNo") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNSNO" runat="server" Text='<%# Eval("NotificationSNo") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTariff" runat="server" Text='<%# Eval("Tarrif") %> '></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSelect" runat="server" CommandArgument='<%# Eval("Tarrif") %>' OnClick="rblHSNID_CheckedChanged" CssClass="btn btn-primary btn-xs" Text="Select" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <table class="table table-striped">
                                        <tr>
                                            <th style="width: 10px">#</th>
                                            <th>S.No.</th>
                                            <th>HSN No.</th>
                                            <th>Notifi. No</th>
                                            <th>Notifi. S.No</th>
                                            <th>Tarrif/Plan</th>
                                            <th></th>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server">
                                                </tr>
                                            </tbody>
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                            </asp:ListView>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">
                                Close
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="viewInvoiceModel" role="dialog" aria-labelledby="viewInvoiceLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 90%;">
            <asp:UpdatePanel ID="upviewInvoiceModel" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;
                            </button>
                            <h4 class="modal-title"><i class="fa fa-globe"></i>
                                <asp:Label ID="Label1" runat="server" Text=" Invoice View"></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <uc1:uc_invoiceR runat="server" ID="uc_invoiceR" />
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">
                                Close
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="viewPurchaseRegigsterModel" role="dialog" aria-labelledby="viewPurchaseLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upviewPurchaseRegigsterModel" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;
                            </button>
                            <h4 class="modal-title"><i class="fa fa-globe"></i>
                                <asp:Label ID="Label2" runat="server" Text="Update Purchase Register"></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <uc1:uc_PerchaseRegister runat="server" ID="uc_PerchaseRegister" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdatePurchaseRegister" Visible="false" CssClass="btn btn-info" runat="server" Text="Update Purchase Register" />
                            <button id="up_tb8" type="button" class="btn btn-info" data-dismiss="modal" runat="server">
                                Update Purchase Register
                            </button>
                            <%--<button class="btn btn-info" data-dismiss="modal" aria-hidden="true">
                                Close
                            </button>--%>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:Panel ID="pnlHSNO" runat="server" CssClass="modal-content" Style="display: none;" DefaultButton="btnTargetHSNO">
        <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel"><b>Notification</b></h5>
        </div>
        <div class="modal-body">
            <asp:GridView ID="gvHSNNo11" runat="server"
                HeaderStyle-BackColor="green"
                AutoGenerateColumns="false" DataKeyNames="Tarrif,SerialNo" Font-Names="Arial"
                Font-Size="11pt" AlternatingRowStyle-BackColor="#C2D69B"
                AllowPaging="true">
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <div style="padding: 5px;">
                                <asp:Button ID="btnSelect" runat="server" OnClick="rblHSNID_CheckedChanged" CssClass="btn btn-primary btn-xs" Text="Select" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField ItemStyle-Width="30px" DataField="SerialNo"
                        HeaderText="SerialNo" />
                    <asp:BoundField ItemStyle-Width="50px" DataField="HSNNumber"
                        HeaderText="HSN Number" />
                    <asp:BoundField ItemStyle-Width="100px" DataField="NotificationNo"
                        HeaderText="Notification No" />
                    <asp:BoundField ItemStyle-Width="100px" DataField="NotificationSNo"
                        HeaderText="NotificationSNo" />
                    <%--    <asp:BoundField ItemStyle-Width="150px" DataField="Description"
                            HeaderText="Description" />--%>
                    <asp:BoundField ItemStyle-Width="50px" DataField="Tarrif"
                        HeaderText="Tarrif/Duty" />
                </Columns>
                <RowStyle BackColor="White" ForeColor="#330099" />
                <SelectedRowStyle BackColor="#FFCC66"
                    Font-Bold="True" ForeColor="#663399" />
                <PagerStyle BackColor="#FFFFCC"
                    ForeColor="#330099" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#990000"
                    Font-Bold="True" ForeColor="#FFFFCC" />
            </asp:GridView>
        </div>
        <div class="modal-footer">
            <asp:Button ID="btnOK" runat="server" CssClass="btn btn-primary" Text="OK" OnClick="btnOK_Click" />
            <asp:HiddenField ID="HiddenField1" runat="server" />
        </div>
    </asp:Panel>
    <%-- <cc3:ModalPopupExtender ID="HSNModalPopupExtender" runat="server" CancelControlID="HiddenField1"
        PopupControlID="pnlHSNO" TargetControlID="btnTargetHSNO">
    </cc3:ModalPopupExtender>--%>

    <%--    <script type="text/javascript">
            $(function () {
                $("[id$=txtSearch]").autocomplete({
                    source: function (request, response) {
                        debugger;
                        $.ajax({
                        url: '<%=ResolveUrl("~/GSTinvoice.aspx/GetItems") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=hfCustomerId]").val(i.item.val);
                },
                minLength: 1
            });
        });
    </script>--%>

    <%--    <script type="text/javascript">
         $(document).ready(function () {
             $("#txtGoodService").autocomplete({
                 source: function (request, response) {
                     debugger;
                     $.ajax({
                         type: "POST",
                         contentType: "application/json; charset=utf-8",
                         url: "AutoPopulate.asmx/GetItems",
                         data: "{'DName':'" + document.getElementById('txtGoodService').value + "'}",
                         dataType: "json",
                         success: function (data) {
                             response(data.d);
                         },
                         error: function (result) {
                             alert("Error......");
                         }
                     });
                 }
             });
         });
   </script>--%>
    <script>
        $(document).ready(function () {
            $('#viewInvoiceModel').on('shown.bs.modal', function () {
                $(this).find('.modal-dialog').css({
                    width: 'auto',
                    height: 'auto',
                    'max-height': '100%'
                });
            });
        });

    </script>
    <%--<script>
        $("#form1").submit(function () {
            $("#WaitDialog").modalDialog();
        });
    </script>--%>

    <script type="text/javascript">
        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select a future day than today!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
            var currentmonth = new Date().getMonth();
            var selectedmonth = sender._selectedDate.getMonth();
            //alert(selectedmonth);
            //alert(currentmonth);
            if (selectedmonth < currentmonth - 2) {
                alert("You cannot select a date earlier than two month!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }

        }
    </script>

</asp:Content>
