<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="VendorRegistration.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Masters.VendorRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>


<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-header">
        <h1>Vendor Registration
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Masters</a></li>
            <li class="active">Vendor Registration</li>
        </ol>
    </div>
    <div class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Add Vendor</h3>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Entity Name (As in GSTIN)</label>
                            <asp:TextBox ID="txtEntityName" class="form-control" onkeypress="return isAlpha(event,this);" placeholder="Entity Name" onpaste="return false;" autocomplete="off" MaxLength="100" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEntityName" runat="server" ValidationGroup="vgRegistration" CssClass="help-block" ErrorMessage="Please enter entity name" Display="Dynamic" ControlToValidate="txtEntityName"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Address</label>
                            <asp:TextBox ID="txtAddress" class="form-control" placeholder="Address" autocomplete="off" MaxLength="150" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ValidationGroup="vgRegistration" CssClass="help-block" ErrorMessage="Please enter the address" Display="Dynamic" ControlToValidate="txtAddress"></asp:RequiredFieldValidator>
                             <asp:RegularExpressionValidator ID="revAddress" runat="server" Display="Dynamic" ErrorMessage="Only ,.,/,_,space and alphanumeric characters are allowed.." ValidationExpression="^[A-Za-z0-9 _/.,]+$" CssClass="help-block" ValidationGroup="vgRegistration" ControlToValidate="txtAddress"></asp:RegularExpressionValidator>
                             </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Email-Id</label>
                            <asp:TextBox ID="txtEmail" class="form-control" placeholder="Email-Id " autocomplete="off" MaxLength="50" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ValidationGroup="vgRegistration" CssClass="help-block" ErrorMessage="Please enter E-mail Id" Display="Dynamic" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="rfvEmailEx" runat="server" ErrorMessage="Please enter valid E-mail Id" ControlToValidate="txtEmail" CssClass="help-block" Display="Dynamic" ValidationGroup="vgRegistration" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>GSTIN No.</label>
                            <asp:TextBox ID="txtGSTNnum" class="form-control" placeholder="GSTIN No" autocomplete="off" MaxLength="15" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvGstn" runat="server" ValidationGroup="vgRegistration" CssClass="help-block" ErrorMessage="Please enter GSTIN No." Display="Dynamic" ControlToValidate="txtGSTNnum"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revGSTNnum" runat="server" Display="Dynamic" ErrorMessage="Invalid GSTIN No.!!" ValidationExpression="^([0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1})$" CssClass="help-block" ValidationGroup="vgRegistration" ControlToValidate="txtGSTNnum"></asp:RegularExpressionValidator>

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Name of the Signatory</label>
                            <asp:TextBox ID="txtSignatory" class="form-control" onkeypress="return isAlpha(event,this);" placeholder="Name of Signatory" autocomplete="off" onpaste="return false;" MaxLength="50" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSignatory" runat="server" ValidationGroup="vgRegistration" CssClass="help-block" ErrorMessage="Please enter name of the signatory" Display="Dynamic" ControlToValidate="txtSignatory"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Designation</label>
                            <asp:DropDownList ID="ddlDesignation" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ErrorMessage="Please specify the Designation" Display="Dynamic" ControlToValidate="ddlDesignation" InitialValue="0" CssClass="help-block" ValidationGroup="vgRegistration"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>OwnerShip Type</label>
                            <asp:DropDownList ID="ddl_OwnerType" AutoPostBack="true" placeholder="OwnerShip Type" ValidationGroup="vgRegistration" DataTextField="Ownership_type" Class="form-control nullvalue" runat="server"></asp:DropDownList>
                            <%--     <asp:RequiredFieldValidator ID="rfvOwnership" runat="server" ErrorMessage="Please specify the Ownership Type" Display="Dynamic" ControlToValidate="ddl_OwnerType" InitialValue="0" CssClass="help-block" ValidationGroup="vgRegistration"></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>State</label>
                            <asp:DropDownList ID="ddl_State" AutoPostBack="true" placeholder="State" ValidationGroup="vgRegistration" DataTextField="State" Class="form-control nullvalue" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvState" runat="server" ErrorMessage="Please select the State" Display="Dynamic" ControlToValidate="ddl_State" InitialValue="0" CssClass="help-block" ValidationGroup="vgRegistration"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3">
                        <div class="input-group">
                            <asp:TextBox ID="txtItem" placeholder="HSN/SAC" CssClass="form-control" runat="server"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfvHSN" runat="server" ErrorMessage="Please select the HSN" Display="Dynamic" ControlToValidate="txtItem" CssClass="help-block" ValidationGroup="vgRegistration"></asp:RequiredFieldValidator>--%>
                            <ajaxToolkit:AutoCompleteExtender
                                runat="server"
                                ID="AutoCompleteExtender1"
                                TargetControlID="txtItem"
                                ServicePath="~/Service/AutoPopulate.asmx"
                                ServiceMethod="GetItems"
                                MinimumPrefixLength="2"
                                CompletionInterval="10"
                                EnableCaching="true"
                                CompletionSetCount="10">
                            </ajaxToolkit:AutoCompleteExtender>
                            <%--    <input id="" placeholder="Item"  type="text"  class="form-control"  />--%>
                            <div class="input-group-btn">
                                <%-- <button id="btnAdd" onclick="btnAdd_Click" type="button" runat="server"  class="btn btn-primary btn-flat" style="background-color: rgb(114, 175, 210); border-color: rgb(114, 175, 210);">Add</button>--%>

                                <asp:Button ID="btnAd" CssClass="btn btn-primary" runat="server" Text="Add" ValidationGroup="vgVendor" OnClick="btnAd_Click" />
                                <uc1:uc_sucess runat="server" ID="uc_sucess1" />
                            </div>
                        </div>

                    </div>
                    <div class="col-sm-3 pull-left"></div>
                </div>
                <%-- <div class="col-sm-3">
                        <div class="form-group">
                                 <asp:Button ID="" CssClass="btn btn-primary btn-flat" style="background-color: rgb(114, 175, 210); border-color: rgb(114, 175, 210);" runat="server" Text="Add" OnClick="btnAdd_Click" />
                             <asp:RequiredFieldValidator ID="rfvItem" runat="server" ValidationGroup="vgRegistration" CssClass="help-block" ErrorMessage="Please enter Item" Display="Dynamic" ControlToValidate="txtItem"></asp:RequiredFieldValidator>
                            </div>--%>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group" style="margin-top: 15px;">
                            <asp:ListBox ID="lvAdd" runat="server" Width="150" Height="60"></asp:ListBox>
                            <asp:RequiredFieldValidator ID="rfvAdd" CssClass="help-block" ControlToValidate="lvAdd" ValidationGroup="vgRegistration" Display="Dynamic" ErrorMessage="Please add and select the HSN" runat="server"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-footer">

                <uc1:uc_sucess runat="server" ID="uc_sucess" />
                <asp:Button ID="btnRegistration" CssClass="btn btn-primary pull-left" ValidationGroup="vgRegistration" runat="server" Text="Submit" OnClick="btnRegistration_Click" />

            </div>

            <div class="row">
                <div class="col-sm-12">
                    <div class="box-body table-responsive no-padding">
                        <asp:ListView ID="lvAdd1" runat="server">
                            <EmptyDataTemplate>
                                <table class="table ">
                                    <tr>
                                        <td>No data was returned.</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.DataItemIndex + 1%>.</td>
                                    <td>
                                        <%# Eval("ItemCode") %>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lkb1" runat="server" ToolTip="Edit"><i class="fa fa-edit text-green"></i></asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lkb2" runat="server" ToolTip="Delete"><i class="fa fa-edit text-red"></i></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <table class="table">
                                    <tr>
                                        <th style="width: 10px">#</th>
                                        <th>Item Code</th>
                                        <th>#</th>
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
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="box-body table-responsive no-padding">
                        <asp:ListView ID="lvVendor" runat="server" OnPagePropertiesChanging="lvVendor_PagePropertiesChanging">
                            <EmptyDataTemplate>
                                <table class="table ">
                                    <tr>
                                        <td>No data was returned.</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.DataItemIndex + 1%>.</td>
                                    <td>
                                        <%# Eval("VendorName") %>
                                    </td>
                                    <td>
                                        <%# Eval("EmailID") %>
                                    </td>
                                    <td>
                                        <%# Eval("GSTNNo") %>
                                    </td>
                                    <td><%#DataBinder.Eval(Container.DataItem,"GST_MST_STATE.StateName")%></td>
                                    <td>
                                        <%# DateTimeAgo.GetFormatDate(Eval("CreatedDate").ToString()) %>
                                    </td>
                                    <td><%#Eval("Status").ToString()== "False" ? "<span class='label label-danger'>" : "<span class='label label-success'>" %><%# Eval("Status") %></td>
                                    <td>
                                        <asp:LinkButton ID="lkbView" runat="server" data-toggle="modal" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV_vs"%>' ToolTip="View"><i class="fa fa-eye text-orange"></i></asp:LinkButton>
                                    </td>
                                    <td>
                                        <div class="modal fade" id='<%# Container.DataItemIndex +"_myModalINV_vs"%>' role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                            <div class="modal-dialog">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                            &times;
                                                        </button>
                                                        <h4 class="modal-title"><b><i class="fa fa-globe"></i>
                                                            <asp:Label ID="lblModalTitle" runat="server" Text="Vendor Service Accounting Code"></asp:Label>
                                                        </b></h4>
                                                    </div>
                                                    <div class="modal-body">
                                                        <div class="table table-responsive">
                                                            <asp:ListView ID="lstVendorService" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_VENDOR_SERVICE")%>'>
                                                                <EmptyDataTemplate>
                                                                    <table class="table table-bordered table-condensed">
                                                                        <tr>
                                                                            <td>No data was returned.</td>
                                                                        </tr>
                                                                    </table>
                                                                </EmptyDataTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td><%# Container.DataItemIndex + 1%></td>
                                                                        <td><%# DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.ItemCode")%></td>
                                                                        <td><%# DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.Description")%></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <LayoutTemplate>
                                                                    <table class="table table responsive table-condensed table-bordered">
                                                                        <tr>
                                                                            <th>#</th>
                                                                            <th>Item Code</th>
                                                                            <th>Description</th>
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
                                        <asp:LinkButton ID="lkb" runat="server" ToolTip="Edit"><i class="fa fa-edit text-green"></i></asp:LinkButton>
                                    </td>--%>
                                </tr>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <table class="table">
                                    <tr>
                                        <th style="width: 10px">#</th>
                                        <th>Vendor Name</th>
                                        <th>Email</th>
                                        <th>GSTIN No.</th>
                                        <th>State Name</th>
                                        <th>Registered Date</th>
                                        <th>Status</th>
                                        <th>Action</th>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </tbody>
                                    </tr>
                                </table>
                            </LayoutTemplate>
                        </asp:ListView>
                    </div>
                    <div class="box-footer clearfix">
                        <div class="pagination pagination-sm no-margin pull-right">
                            <%--<div class="box-body dataTables_paginate paging_simple_numbers">--%>
                            <asp:DataPager ID="dpvendorservice" runat="server" PagedControlID="lvVendor" PageSize="10" class="btn-group-sm pager-buttons">
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

        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Trans-shipment</h3>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-sm-2">
                                <div class="form-group">
                                    <label>Vendor Name</label>
                                    <asp:DropDownList ID="ddlVendorName" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlVendorName_SelectedIndexChanged" TabIndex="1" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvVendor" runat="server" CssClass="help-block" Display="Dynamic" ValidationGroup="vgTrans" ErrorMessage="Value Required" InitialValue="0" ControlToValidate="ddlVendorName"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <div class="form-group">
                                    <label>Item</label>
                                    <asp:DropDownList ID="ddlItem" CssClass="form-control" TabIndex="2" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvItem1" runat="server" CssClass="help-block" Display="Dynamic" ValidationGroup="vgTrans" ErrorMessage="Value Required" InitialValue="0" ControlToValidate="ddlItem"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <div class="form-group">
                                    <label>Vehicle Registration No.</label>
                                    <asp:TextBox ID="txtRegNo" class="form-control" onkeypress="return alphanumeric(event,this);" onpaste="return false;" placeholder="Vehicle Reg No" autocomplete="off" TabIndex="3" MaxLength="20" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvRegistraion" runat="server" CssClass="help-block" Display="Dynamic" ValidationGroup="vgTrans" ErrorMessage="Please enter vehicle Registration Number" ControlToValidate="txtRegNo"></asp:RequiredFieldValidator>
                                  <asp:RegularExpressionValidator ID="revRegNo" runat="server" Display="Dynamic" ErrorMessage="Only - and alphanumeric characters are allowed.." ValidationExpression="^[A-Za-z0-9-]+$" CssClass="help-block" ValidationGroup="vgRegistration" ControlToValidate="txtRegNo"></asp:RegularExpressionValidator>
                                     </div>
                            </div>
                            <div class="col-sm-2">
                                <div class="form-group">
                                    <label>Driver Licence No.</label>
                                    <asp:TextBox ID="txtDriverNo" class="form-control" onkeypress="return alphanumeric(event,this);" onpaste="return false;" placeholder="Driver Licence No." autocomplete="off" TabIndex="4" MaxLength="20" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvdriverNo" runat="server" CssClass="help-block" Display="Dynamic" ValidationGroup="vgTrans" ErrorMessage="Please specify the Driver License No." ControlToValidate="txtDriverNo"></asp:RequiredFieldValidator>
                               <asp:RegularExpressionValidator ID="revdriverNo" runat="server" Display="Dynamic" ErrorMessage="Only - and alphanumeric characters are allowed.." ValidationExpression="^[A-Za-z0-9-]+$" CssClass="help-block" ValidationGroup="vgRegistration" ControlToValidate="txtDriverNo"></asp:RegularExpressionValidator>
                                     </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label>Is International Warehouse Facility Availed</label>
                                    <asp:DropDownList ID="ddlfacility" class="form-control" placeholder="Availed International Facility" ValidationGroup="vgTrans" MaxLength="10" TabIndex="5" runat="server"></asp:DropDownList>
                                   <%-- <asp:RequiredFieldValidator ID="rfvfacility" runat="server" CssClass="help-block" Display="Dynamic" ValidationGroup="vgTrans" ErrorMessage="Value Required" InitialValue="0" ControlToValidate="ddlfacility"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-2">
                                <div class="form-group">
                                    <label>From Location</label>
                                    <asp:TextBox ID="txtlocation" Class="form-control" onkeypress="return isAlpha(event,this);" MaxLength="50" onpaste="return false;" autocomplete="off" placeholder="From Location" TabIndex="6" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvLocation" runat="server" CssClass="help-block" Display="Dynamic" ValidationGroup="vgTrans" ErrorMessage="Please enter location" ControlToValidate="txtlocation"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <div class="form-group">
                                    <label>To Location</label>
                                    <asp:TextBox ID="TxtToLocation" Class="form-control" onkeypress="return isAlpha(event,this);" MaxLength="50" onpaste="return false;" autocomplete="off" placeholder="To Location" TabIndex="7" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTolocation" runat="server" CssClass="help-block" ValidationGroup="vgTrans" Display="Dynamic" ErrorMessage="Please enter location" ControlToValidate="TxtToLocation"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <div class="form-group">
                                    <label>Bill Amount</label>
                                    <asp:TextBox ID="txtBillAmt" Class="form-control" onkeypress="return isNumberKey(event,this);" MaxLength="15" onpaste="return false;" autocomplete="off" placeholder="Bill Amount" TabIndex="8" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvBillAmt" runat="server" CssClass="help-block" ValidationGroup="vgTrans" Display="Dynamic" ErrorMessage="Please enter amount" ControlToValidate="txtBillAmt"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <div class="form-group">
                                    <label>Trans Shipment No.</label>
                                    <asp:TextBox ID="txtTransShipmentNo" Class="form-control" onkeypress="return alphanumerics(event,this);" placeholder="Trans Shipment No." MaxLength="15" autocomplete="off" TabIndex="9" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTransShipment" runat="server" CssClass="help-block" ValidationGroup="vgTrans" Display="Dynamic" ErrorMessage="Please enter location" ControlToValidate="txtTransShipmentNo"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <div class="form-group">
                                    <label>Date</label>
                                    <asp:TextBox ID="txtDate" MaxLength="10" Class="form-control" placeholder="mm/DD/yyyy" CssClass="form-control date-picker_venderdate" autocomplete="off" TabIndex="10" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <div class="form-group">
                                    <label>Distance (in Kms)</label>
                                    <asp:TextBox ID="txtdistance" Class="form-control" onkeypress="return isDigitKey(event,this);" autocomplete="off" MaxLength="10" onpaste="return false;" placeholder="Distance" TabIndex="11" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvdistance" runat="server" CssClass="help-block" ValidationGroup="vgTrans" Display="Dynamic" ErrorMessage="Please enter Distance" ControlToValidate="txtdistance"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <uc1:uc_sucess runat="server" ID="uc_sucess_trans" />
                                <asp:LinkButton ID="btntransShipment" runat="server" ValidationGroup="vgTrans" OnClick="btnTransShipment_Click" CssClass="btn btn-primary pull-left"><i class="fa fa-save"></i><span style="margin:3px;"> Submit</span></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="box-body table-responsive">
                        <asp:ListView ID="lvTransShipment" runat="server" OnPagePropertiesChanging="lvTransShipment_PagePropertiesChanging1">
                            <EmptyDataTemplate>
                                <table class="table ">
                                    <tr>
                                        <td>No data was returned.</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.DataItemIndex + 1%>.</td>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem,"GST_MST_VENDOR.VendorName")%> 
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.ItemCode") %>
                                    </td>
                                    <td>
                                        <%# Eval("VehicleRegistrationNumber") %>
                                    </td>
                                    <td>
                                        <%# Eval("DriverLicenceNumber") %>
                                    </td>
                                    <td>
                                        <%#Eval("IsInterNationalWarehouseAvailed")==null?"-":Eval("IsInterNationalWarehouseAvailed").ToString()== "False" ? "<span class='label label-danger'>" : "<span class='label label-success'>" %><%# Eval("IsInterNationalWarehouseAvailed") %> 
                                    </td>
                                    <td>
                                        <%# Eval("FromLocation") %>
                                    </td>
                                    <td>
                                        <%# Eval("ToLocation") %>
                                    </td>
                                    <td>
                                        <%# Eval("BillAmount") %>
                                    </td>
                                    <%-- <td>
                                        <%# Eval("TransShipmentStatus") %>
                                    </td>--%>
                                    <td>
                                        <%# DateTimeAgo.GetFormatDate(Eval("CreatedDate").ToString()) %>
                                    </td>
                                    <td>
                                        <%# Eval("Distance_ID") %>
                                    </td>
                                    <td><%#Eval("Status").ToString()== "False" ? "<span class='label label-danger'>" : "<span class='label label-success'>" %><%# Eval("Status") %></td>

                                    <td>
                                        <asp:LinkButton ID="lkbShip" runat="server" OnClick="lkbShip_Click" CommandArgument='<%# Eval("TransShipment_ID") %>' ToolTip="Edit"><i class="fa fa-edit text-green"></i></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <table class="table">
                                    <tr>
                                        <th style="width: 10px">#</th>
                                        <th>Vendor Name</th>
                                        <th>Item</th>
                                        <th>Vehicle Reg. No.</th>
                                        <th>D.L. No.</th>
                                        <th>Is Warehouse Availed</th>
                                        <th>From Location</th>
                                        <th>To Location</th>
                                        <th>Bill Amount</th>
                                        <%-- <th>Trans Shipment Status</th>--%>
                                        <th>Created Date</th>
                                        <th>Distance</th>
                                        <th>Status</th>
                                        <th>#</th>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </tbody>
                                    </tr>
                                </table>
                            </LayoutTemplate>
                        </asp:ListView>
                        <div class="box-footer clearfix">
                            <div class="pagination pagination-sm no-margin pull-right">

                                <asp:DataPager ID="dpTranshipment" runat="server" PagedControlID="lvTransShipment" PageSize="5" class="btn-group-sm pager-buttons">
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


    <%--   <script type="text/javascript">
        if ($('#idOfFeild').val() == '') {
            $('#idOfFeild').val('Please input your data');
        }

        mydropdown = $('#myDropdown');
        if (mydropdown.length == 0 || $(mydropdown).val() == "") {
            alert("Please select anyone");
        }
    </script>--%>
</asp:Content>

