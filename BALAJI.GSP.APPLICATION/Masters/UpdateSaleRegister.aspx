<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdatePurchaseRegister.aspx.cs" Inherits="UserInterface.UpdatePurchaseRegister" %>--%>

<%@ MasterType VirtualPath="~/User/User.master" %>

<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Admin.master" CodeBehind="UpdateSaleRegister.aspx.cs" Inherits="UserInterface.UpdateSaleRegister" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>
<%@ Register Src="~/UC/uc_Purchase_Data.ascx" TagPrefix="uc1" TagName="uc_Purchase_Data" %>



<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content-header">
        <h1>Purchase Register
        <small></small></h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Masters</a></li>
            <li class="active">Sale/Purchase Register</li>
        </ol>
    </div>
    <%--<div class="panel-heading">
                            Update Purchase Register
                        </div>--%>
    <div class="content">
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="PROC_PURCHASE_STOCK_INVENTRY" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter DefaultValue="" Name="USERID" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <div class="nav-tabs-custom">
            <ul class="nav nav-tabs">
                <li class="active"><a data-toggle="tab" href="#Stock">Stock</a></li>
                <li><a data-toggle="tab" href="#saleitem">Sale Item</a></li>
                <li><a data-toggle="tab" href="#add">Add / View</a></li>
                <li><a data-toggle="tab" href="#purchasedata">Add Purchase Item</a></li>

            </ul>

            <div class="tab-content">
                <div id="Stock" class="tab-pane fade in active">
                    <div class="box-body table-responsive">
                        <asp:ListView ID="lvPurchaseStock" runat="server" DataSourceID="SqlDataSource1">
                            <EmptyDataTemplate>
                                <table class="table">
                                    <tr>
                                        <td>No data was returned.</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.DataItemIndex + 1%></td>
                                    <td><%# Eval("ItemCode") %></td>
                                    <td><%# Eval("Description") %></td>
                                    <td><%# Eval("STOCK") %></td>
                                    <td><%# Eval("SALE") %></td>
                                    <td></td>
                                </tr>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <table class="table table-responsive dataTable" id="lvItems">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Item Code</th>
                                            <th>Description</th>
                                            <th>Stock</th>
                                            <th>Sale</th>
                                            <th>View</th>
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
                <div id="add" class="tab-pane fade">
                    <div class="box-body" id="lineItem1">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        Seller GSTIN</label>
                                    <asp:TextBox ID="txtSellerGSTIN" runat="server" class="form-control" placeholder="Seller GSTIN" autocomplete="off" MaxLength="15"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revGSTIN" runat="server" Display="Dynamic" ErrorMessage="Invalid GSTIN No.!!" ValidationExpression="^([0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1})$" CssClass="help-block" ValidationGroup="vgPurchaseregister" ControlToValidate="txtSellerGSTIN"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="rfvGSTIN" runat="server" ValidationGroup="vgPurchaseregister" CssClass="help-block" Display="Dynamic" ControlToValidate="txtSellerGSTIN" ErrorMessage="Please Enter Seller GSTIN"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        Seller Name</label>
                                    <asp:TextBox ID="txtSellerName" placeholder="Seller Name" CssClass="form-control" autocomplete="off" MaxLength="50" onkeypress="return isAlpha(event);" onpaste="return false;" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSellerName" runat="server" ValidationGroup="vgPurchaseregister" CssClass="help-block" Display="Dynamic" ControlToValidate="txtSellerName" ErrorMessage="Please Enter Seller Name"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        Seller Address</label>
                                    <asp:TextBox ID="txtSellerAddress" placeholder="Seller Address" CssClass="form-control" MaxLength="200" autocomplete="off" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSupplierInvoice" runat="server" ValidationGroup="vgPurchaseregister" CssClass="help-block" Display="Dynamic" ControlToValidate="txtSellerAddress" ErrorMessage="Please Enter Seller Address"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revAddress" runat="server" Display="Dynamic" ErrorMessage="Only ,.,/,_,space and alphanumeric characters are allowed.." ValidationExpression="^[A-Za-z0-9 _/.,]+$" CssClass="help-block" ValidationGroup="vgPurchaseregister" ControlToValidate="txtSellerAddress"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        Receiver Name</label>
                                    <asp:TextBox ID="txtRecieverName" placeholder="Receiver Name" CssClass="form-control" MaxLength="50" autocomplete="off" onkeypress="return isAlpha(event);" onpaste="return false;" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtRecieverName" runat="server" ValidationGroup="vgPurchaseregister" CssClass="help-block" Display="Dynamic" ControlToValidate="txtRecieverName" ErrorMessage="Please Enter Reciever Name"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        Receiver Address</label>
                                    <asp:TextBox ID="txtRecieverAddress" placeholder="Receiver Address" CssClass="form-control" MaxLength="200" autocomplete="off" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtRecieverAddress" runat="server" ValidationGroup="vgPurchaseregister" CssClass="help-block" Display="Dynamic" ControlToValidate="txtRecieverAddress" ErrorMessage="Please Enter Reciever Address"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revRecieveraddress" runat="server" Display="Dynamic" ErrorMessage="Only ,.,/,_,space and alphanumeric characters are allowed.." ValidationExpression="^[A-Za-z0-9 _/.,]+$" CssClass="help-block" ValidationGroup="vgPurchaseregister" ControlToValidate="txtRecieverAddress"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        Consignee Name</label>
                                    <asp:TextBox ID="txtConsigneeName" placeholder="Consignee Name" CssClass="form-control" onkeypress="return isAlpha(event,this);" MaxLength="50" autocomplete="off" onpaste="return false;" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtConsigneeName" runat="server" ValidationGroup="vgPurchaseregister" CssClass="help-block" Display="Dynamic" ControlToValidate="txtConsigneeName" ErrorMessage="Please Enter Consignee Name"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        Consignee Address</label>
                                    <asp:TextBox ID="txtConsigneeAddress" placeholder="Consignee Address" CssClass="form-control" autocomplete="off" MaxLength="200" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtConsigneeAddress" runat="server" ValidationGroup="vgPurchaseregister" CssClass="help-block" Display="Dynamic" ControlToValidate="txtConsigneeAddress" ErrorMessage="Please Enter Consignee Address"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revConsigneeaddress" runat="server" Display="Dynamic" ErrorMessage="Only ,.,/,_,space and alphanumeric characters are allowed.." ValidationExpression="^[A-Za-z0-9 _/.,]+$" CssClass="help-block" ValidationGroup="vgPurchaseregister" ControlToValidate="txtConsigneeAddress"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        Stock Inward Date</label>
                                    <asp:TextBox ID="txtStockInwardDate" placeholder="MM/DD/YYY" autocomplete="off" CssClass="form-control date-picker_invertdate" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtStockInwardDate" runat="server" ValidationGroup="vgPurchaseregister" CssClass="help-block" Display="Dynamic" ControlToValidate="txtStockInwardDate" ErrorMessage="Please Enter Stock Inward Date"></asp:RequiredFieldValidator>
                                    <uc1:uc_sucess runat="server" ID="uc_sucess2" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        Stock Order Date</label>
                                    <asp:TextBox ID="txtStockOrderDate" placeholder="MM/DD/YYY" CssClass="form-control date-picker" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvftxtStockOrderDate" runat="server" ValidationGroup="vgPurchaseregister" CssClass="help-block" Display="Dynamic" ControlToValidate="txtStockOrderDate" ErrorMessage="Please Enter Stock Order Date"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        PO Number</label>
                                    <asp:TextBox ID="txtOrderPo" placeholder="Order Po" CssClass="form-control" autocomplete="off" MaxLength="16" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvOrderPo" runat="server" ValidationGroup="vgPurchaseregister" CssClass="help-block" Display="Dynamic" ControlToValidate="txtOrderPo" ErrorMessage="Please Enter PO number"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revOrderPo" runat="server" Display="Dynamic" ErrorMessage=" Only _,- & alphanumeric characters are allowed.." ValidationExpression="^[a-zA-Z0-9 _-]+$" CssClass="help-block" ValidationGroup="vgPurchaseregister" ControlToValidate="txtOrderPo"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        Order Po Date</label>
                                    <asp:TextBox ID="txtOrderPoDate" placeholder="MM/DD/YYY" CssClass="form-control date-picker" autocomplete="off" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvftxtOrderPoDate" runat="server" ValidationGroup="vgPurchaseregister" CssClass="help-block" Display="Dynamic" ControlToValidate="txtOrderPoDate" ErrorMessage="Please Enter Order Po Date"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        Supplier Invoice Number</label>
                                    <asp:TextBox ID="txtSupplierInvoiceNumber" MaxLength="16" placeholder="Supplier Invoice Number" CssClass="form-control" autocomplete="off" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtSupplierInvoiceNumber" runat="server" ValidationGroup="vgPurchaseregister" CssClass="help-block" Display="Dynamic" ControlToValidate="txtSupplierInvoiceNumber" ErrorMessage="Please Enter Supplier Invoice Number"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revtxtSupplierInvoicenumber" runat="server" Display="Dynamic" ErrorMessage=" Only _,- & alphanumeric characters are allowed.." ValidationExpression="^[a-zA-Z0-9 _-]+$" CssClass="help-block" ValidationGroup="vgPurchaseregister" ControlToValidate="txtSupplierInvoiceNumber"></asp:RegularExpressionValidator>
                                     </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        Supplier Invoice Date</label>
                                    <asp:TextBox ID="txtSupplierInvoiceDate" placeholder="MM/DD/YYY" CssClass="form-control date-picker" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtSupplierInvoiceDate" runat="server" ValidationGroup="vgPurchaseregister" CssClass="help-block" Display="Dynamic" ControlToValidate="txtSupplierInvoiceDate" ErrorMessage="Please Enter Supplier Invoice Date"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        Supplier Invoice Month</label>
                                    <%--<asp:TextBox ID="txtSupplierInvoiceMonth" placeholder="Supplier Invoice Month" CssClass="form-control " runat="server"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlmonth" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="rfvmonth" runat="server" ValidationGroup="vgPurchaseregister" CssClass="help-block" Display="Dynamic" ControlToValidate="ddlmonth" ErrorMessage="Please Enter Supplier Invoice Month"></asp:RequiredFieldValidator>--%>
                                    <asp:RequiredFieldValidator ID="rfvmonth" runat="server" CssClass="help-block" Display="Dynamic" ErrorMessage="Value Required" InitialValue="0" ValidationGroup="vgPurchaseregister" ControlToValidate="ddlmonth"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        Freight</label>
                                    <asp:TextBox ID="txtFreight" placeholder="Freight" CssClass="form-control" onkeypress="return isNumberKey(event);" MaxLength="6" onpaste="return false;" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtFreight" runat="server" ValidationGroup="vgPurchaseregister" CssClass="help-block" Display="Dynamic" ControlToValidate="txtFreight" ErrorMessage="Please Enter Freight"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        Insurance</label>
                                    <asp:TextBox ID="txtInsurance" placeholder="Insurance" CssClass="form-control" onkeypress="return isNumberKey(event);" MaxLength="6" onpaste="return false;" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtInsurance" runat="server" ValidationGroup="vgPurchaseregister" CssClass="help-block" Display="Dynamic" ControlToValidate="txtInsurance" ErrorMessage="Please Enter Insurance"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        Packing and Forwarding Charges</label>
                                    <asp:TextBox ID="txtcharges" placeholder="Packing and Forwarding Charges" CssClass="form-control" onkeypress="return isNumberKey(event);" MaxLength="6" onpaste="return false;" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rvftxtcharges" runat="server" ValidationGroup="vgPurchaseregister" CssClass="help-block" Display="Dynamic" ControlToValidate="txtcharges" ErrorMessage="Please Enter Packing and Forwarding Charges"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>
                                        State</label>
                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rvfstate" runat="server" CssClass="help-block" Display="Dynamic" ErrorMessage="Value Required" InitialValue="0" ValidationGroup="vgPurchaseregister" ControlToValidate="ddlState"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box-footer">
                        <asp:LinkButton ID="btnSaveRegister" CssClass="btn btn-success" OnClick="btnSaveRegister_Click" ValidationGroup="vgPurchaseregister" runat="server"><i class="fa fa-save"></i>&nbsp; Submit</asp:LinkButton>
                        <%--<asp:Button ID="" runat="server" class="btn btn-primary" Text="Submit" ></asp:Button>--%>
                        <uc1:uc_sucess runat="server" ID="uc_sucess" />
                    </div>
                    <div class="box-body table-responsive">
                        <asp:ListView ID="lvPurchageItems" runat="server" OnPagePropertiesChanging="lvPurchageItems_PagePropertiesChanging">
                            <EmptyDataTemplate>
                                <table class="table table-responsive">
                                    <tr>
                                        <td>No data was returned.</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.DataItemIndex + 1%>.</td>
                                    <td><%# Eval("SupplierInvoiceNo") %></td>
                                    <td><%# Eval("SellerName") %></td>
                                    <td><%#DateTimeAgo.GetFormatDate(Eval("StockInwardDate")) %></td>
                                    <td><%#DateTimeAgo.GetFormatDate(Eval("StockOrderDate")) %></td>
                                    <td><%# Eval("OrderPo") %></td>
                                    <td>
                                        <asp:LinkButton ID="lkbView" runat="server" data-toggle="modal" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' ToolTip="View"><i class="fa fa-eye text-orange"></i></asp:LinkButton>
                                        <div class="modal fade" id='<%# Container.DataItemIndex +"_myModalINV"%>' role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                            <div class="modal-dialog">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                            &times;
                                                        </button>
                                                        <h4 class="modal-title"><b><i class="fa fa-globe"></i>
                                                            <asp:Label ID="lblModalTitle" runat="server" Text="Item Details"></asp:Label>
                                                        </b></h4>
                                                    </div>
                                                    <div class="modal-body">
                                                        <div class="table table-responsive">
                                                            <asp:ListView ID="lstSaleR" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem,"GST_MST_PURCHASE_DATA")%>' ItemType="DataAccessLayer.GST_MST_PURCHASE_DATA">
                                                                <EmptyDataTemplate>
                                                                    <table class="table  table-bordered table-condensed">
                                                                        <tr>
                                                                            <td>No data was returned.</td>
                                                                        </tr>
                                                                    </table>
                                                                </EmptyDataTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td style="text-align: center;"><%# Container.DataItemIndex + 1%></td>
                                                                        <td style="text-align: center;"><%# DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.ItemCode")%></td>
                                                                        <td><%# DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.Description")%></td>
                                                                        <td style="text-align: center;"><%#DataBinder.Eval(Container.DataItem,"Qty")%>&nbsp; <%# DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.UNIT")%></td>
                                                                        <td style="text-align: center;"><%#DataBinder.Eval(Container.DataItem,"Rate")%></td>
                                                                        <%--<td style="text-align: center;">
                                                                            <%#DataBinder.Eval(Container.DataItem,"Status")%>
                                                                        </td>--%>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <LayoutTemplate>
                                                                    <table class="table table-responsive">
                                                                        <tr>
                                                                            <th>#</th>
                                                                            <th style="text-align: center;">Item</th>
                                                                            <th style="text-align: center;">Description</th>
                                                                            <th style="text-align: center;">Qty.</th>
                                                                            <th style="text-align: center;">Rate (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
                                                                            <%--  <th>Status </th>--%>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server">
                                                                                </tr>
                                                                            </tbody>
                                                                        </tr>
                                                                    </table>
                                                                </LayoutTemplate>
                                                            </asp:ListView>
                                                        </div>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">
                                                            Close
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                    <%-- <td>
                                        <asp:LinkButton ID="lkb" runat="server" OnClick="lkbGroup_Click" CommandArgument='<%# Eval("GroupID") %>' ToolTip="Edit"><i class="fa fa-edit text-green"></i></asp:LinkButton>
                                    </td>--%>
                                </tr>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <table class="table table-responsive">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>SupplierInvoiceNo</th>
                                            <th>Seller Name</th>
                                            <th>Stock Inward Date</th>
                                            <th>Stock Order Date</th>
                                            <th>Order Po</th>
                                            <th>View</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                        </asp:ListView>

                        <div class="box-footer clearfix">
                            <div class="pagination pagination-sm no-margin pull-right">
                                <%--<div class="box-body dataTables_paginate paging_simple_numbers">--%>
                                <asp:DataPager ID="dppurchaseregister" runat="server" PagedControlID="lvPurchageItems" PageSize="10" class="btn-group-sm pager-buttons">
                                    <Fields>
                                        <asp:NextPreviousPagerField ShowLastPageButton="False" ShowNextPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                                        <asp:NumericPagerField ButtonType="Button" RenderNonBreakingSpacesBetweenControls="false" NumericButtonCssClass="btn btn-primary" CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn" />
                                        <asp:NextPreviousPagerField ShowFirstPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                                    </Fields>
                                </asp:DataPager>
                            </div>
                        </div>
                    </div>

                </div>
                <div id="purchasedata" class="tab-pane fade">
                    <div class="box-body table-responsive">
                        <uc1:uc_Purchase_Data runat="server" ID="uc_Purchase_Data" />
                    </div>
                </div>
                <div id="saleitem" class="tab-pane fade">
                    <div class="box-body table-responsive">
                        <asp:ListView ID="lvSaleItems" runat="server" OnPagePropertiesChanging="lvSaleItems_PagePropertiesChanging">
                            <EmptyDataTemplate>
                                <table class="table">
                                    <tr>
                                        <td>No data was returned.</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.DataItemIndex + 1%>.</td>
                                    <td><%# Eval("InvoiceNo") %></td>
                                    <td><%# DateTimeAgo.GetFormatDate(Eval("InvoiceDate").ToString()) %></td>
                                    <%--   <td><%#DateTimeAgo.GetFormatDate(Eval("StockInwardDate")) %></td>
                                    <td><%#DateTimeAgo.GetFormatDate(Eval("StockOrderDate")) %></td>
                                 <td><%# Eval("OrderPo") %></td>--%>
                                    <td>
                                        <asp:LinkButton ID="lkbView" runat="server" data-toggle="modal" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV_S"%>' ToolTip="View"><i class="fa fa-eye text-orange"></i></asp:LinkButton>
                                        <div class="modal fade" id='<%# Container.DataItemIndex +"_myModalINV_S"%>' role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                            <div class="modal-dialog">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                            &times;
                                                        </button>
                                                        <h4 class="modal-title"><b><i class="fa fa-globe"></i>
                                                            <asp:Label ID="lblModalTitle" runat="server" Text="Item Details"></asp:Label>
                                                        </b></h4>
                                                    </div>
                                                    <div class="modal-body">
                                                        <div class="table table-responsive">
                                                            <asp:ListView ID="lstSaleR" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_DATA")%>'>
                                                                <EmptyDataTemplate>
                                                                    <table class="table  table-bordered table-condensed">
                                                                        <tr>
                                                                            <td>No data was returned.</td>
                                                                        </tr>
                                                                    </table>
                                                                </EmptyDataTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td style="text-align: center;"><%# Container.DataItemIndex + 1%></td>
                                                                        <td style="text-align: center;"><%# DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.ItemCode")%></td>
                                                                        <td><%# DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.Description")%></td>
                                                                        <td style="text-align: center;"><%#DataBinder.Eval(Container.DataItem,"Qty")%>&nbsp; <%# DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.UNIT")%></td>
                                                                        <td style="text-align: center;"><%#DataBinder.Eval(Container.DataItem,"Rate")%></td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                                <LayoutTemplate>
                                                                    <table class="table table-responsive table-bordered">
                                                                        <tr>
                                                                            <th style="width: 2%;">#</th>
                                                                            <th style="text-align: center; width: 8%;">Item</th>
                                                                            <th style="text-align: center; width: 65%">Description</th>
                                                                            <th style="text-align: center; width: 10%;">Qty.</th>
                                                                            <th style="text-align: center; width: 15%;">Rate (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>

                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server">
                                                                                </tr>
                                                                            </tbody>
                                                                        </tr>
                                                                    </table>
                                                                </LayoutTemplate>
                                                            </asp:ListView>
                                                        </div>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">
                                                            Close
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <table class="table table-responsive table-hover">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Invoice No</th>
                                            <th>Invoice Date</th>
                                            <%-- <th>Stock Inward Date</th>
                                            <th>Stock Order Date</th>
                                            <th>Order Po</th>--%>
                                            <th>View</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                        </asp:ListView>
                        <div class="box-footer clearfix">
                            <div class="pagination pagination-sm no-margin">
                                <%--<div class="box-body dataTables_paginate paging_simple_numbers">--%>
                                <asp:DataPager ID="dpsaleItems" runat="server" PagedControlID="lvSaleItems" PageSize="10" class="btn-group-sm pager-buttons">
                                    <Fields>
                                        <asp:NextPreviousPagerField ShowLastPageButton="False" ShowNextPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                                        <asp:NumericPagerField ButtonType="Button" RenderNonBreakingSpacesBetweenControls="false" NumericButtonCssClass="btn btn-primary" CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn" />
                                        <asp:NextPreviousPagerField ShowFirstPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                                    </Fields>
                                </asp:DataPager>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </div>
    </div>
    <%--<div class="box box-primary">
            <%--<div class="box-header with-border">
                <h3 class="box-title">Add/View</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove">
                        <i class="fa fa-remove"></i>
                    </button>
                </div>
            </div>
              <div class="row">
                <div class="col-md-12">
                    <div class="box-body table">
                    </div>
                </div>
            </div>
        </div>--%>
</asp:Content>

