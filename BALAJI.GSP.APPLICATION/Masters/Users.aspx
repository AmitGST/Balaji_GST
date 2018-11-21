<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Masters.Users" %>

<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>
<%@ Register Src="~/Account/uc_UserList.ascx" TagPrefix="uc1" TagName="uc_UserList" %>
<%@ Register Src="~/UC/uc_modelUser.ascx" TagPrefix="uc1" TagName="uc_modelUser" %>
<%@ Register Src="~/UC/uc_modalUserList.ascx" TagPrefix="uc1" TagName="uc_modalUserList" %>



<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
        <h1>Users
            <small>Receiver/Consignee</small>
        </h1>

        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Masters</a></li>
            <li class="active">Users</li>
        </ol>
    </section>
    <div class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Users</h3>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-3">
                        <%--<div class="form-group">
                            <label for="txtUserName">Login ID</label>
                            <asp:TextBox TabIndex="0" ID="txtUserName" runat="server" class="form-control" autocomplete="off" placeholder="Login ID" MaxLength="20" AutoCompleteType="None" ViewStateMode="Disabled"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" Display="Dynamic" ControlToValidate="txtUserName" CssClass="help-block" ErrorMessage="Please specify user name." ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revUserName" runat="server" Display="Dynamic" ControlToValidate="txtUserName" CssClass="help-block" ErrorMessage="Minimum user name length is 8 character" ValidationExpression=".{8}.*" ValidationGroup="createUser"></asp:RegularExpressionValidator>
                        </div>--%>
                        <div class="form-group">
                            <label for="txtGSTNNo">GSTIN</label>
                            <asp:TextBox ID="txtGSTNNo" class="form-control" autocomplete="off" placeholder="GSTIN" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvGSTNNo" runat="server" Display="Dynamic" ControlToValidate="txtGSTNNo" CssClass="help-block" ErrorMessage="Please specify GSTIN" ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revGSTNO" runat="server" Display="Dynamic" ControlToValidate="txtGSTNNo" CssClass="help-block" ErrorMessage="Invalid GSTIN No..!!" ValidationExpression="^([0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1})$" ValidationGroup="createUser" />
                            <%-- <span id="spnGstnno" style="color: Red; display: none">Please specify GSTIN in proper format</span>--%>
                        </div>
                        <div class="form-group">
                            <label for="txtFirstName">First Name</label>
                            <asp:TextBox ID="txtFirstName" class="form-control" onkeypress="return isAlpha(event);" autocomplete="off" placeholder="First Name" onpaste="return false;" runat="server" MaxLength="15"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" Display="Dynamic" ControlToValidate="txtFirstName" CssClass="help-block" ErrorMessage="Please specify first name." ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="txtLastName">Last Name</label>
                            <asp:TextBox ID="txtLastName" class="form-control" onkeypress="return isAlpha(event);" autocomplete="off" placeholder="Last Name" onpaste="return false;" runat="server" MaxLength="15"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" Display="Dynamic" ControlToValidate="txtLastName" CssClass="help-block" ErrorMessage="Please specify last name." ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label>User Type</label>
                            <asp:DropDownList AutoPostBack="true" ID="ddluser_type" Class="form-control" placeholder="Select User Type" DataTextField="User-type" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvusertype" runat="server" Display="Dynamic" ControlToValidate="ddluser_type" CssClass="help-block" ErrorMessage="Value Required." ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtEmailID">Email ID</label>
                            <asp:TextBox ID="txtEmailID" class="form-control" autocomplete="off" placeholder="Email Id" MaxLength="30" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" Display="Dynamic" ControlToValidate="txtEmailID" CssClass="help-block" ErrorMessage="Please specify email id." ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmailid" runat="server" Display="Dynamic" ErrorMessage="Please specify valid email id" CssClass="help-block" ControlToValidate="txtEmailID" ValidationGroup="vgcreateUser" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            <%-- <span id="spnmail" style="color: Red; display: none">Please specify valid email id</span>--%>
                        </div>

                        <div class="form-group">
                            <label for="txtGstId">GSTIN User ID</label>
                            <asp:TextBox ID="txtGstId" class="form-control" autocomplete="off" placeholder="GSTIN User ID" MaxLength="20" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvGstId" runat="server" ErrorMessage="Please specify the GSTIN Id" Display="Dynamic" ControlToValidate="txtGstId" CssClass="help-block" ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                            <%--<span id="spnGstId" style="color: Red; display: none">Please enter alphanumeric and special characters only</span>--%>
                        </div>
                        <div class="form-group">
                            <label for="txt_statecode">State Name</label>
                            <asp:DropDownList ID="ddlStateCode" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvStateCode" runat="server" ValidationGroup="vgcreateUser" CssClass="help-block" Display="Dynamic" ErrorMessage="Value Required" InitialValue="0" ControlToValidate="ddlStateCode"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label>Registered with us</label>
                            <asp:DropDownList AutoPostBack="true" ID="ddlRegistered" Class="form-control" placeholder="Select Option" DataTextField="Registered with us" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvReigster" runat="server" ValidationGroup="vgcreateUser" CssClass="help-block" Display="Dynamic" ErrorMessage="Value Required" InitialValue="0" ControlToValidate="ddlRegistered"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txtOrganization">Name of Organization</label>
                            <asp:TextBox ID="txtOrganization" class="form-control" autocomplete="off" placeholder="Name of the Organization" MaxLength="30" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvOrganization" runat="server" Display="Dynamic" ControlToValidate="txtOrganization" CssClass="help-block" ErrorMessage="Please specify Organization name" ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                            <%--   <span id="spnOrganization" style="color: Red; display: none">Only alphanumeric & special characters are allowed</span>--%>
                        </div>
                        <div class="form-group">
                            <label for="ddlDesig">Designation</label>
                            <asp:DropDownList ID="ddlDesig" CssClass="form-control" runat="server" Placeholder="Select Designation"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDesig" runat="server" ErrorMessage="Please specify the Designation" Display="Dynamic" ControlToValidate="ddlDesig" InitialValue="0" CssClass="help-block" ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="txtAddress">Address</label>
                            <asp:TextBox ID="txtAddress" class="form-control" autocomplete="off" placeholder="Address" MaxLength="100" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAddress" runat="server" Display="Dynamic" ErrorMessage="Field cannot be left blank" CssClass="help-block" ControlToValidate="txtAddress" ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                            <%--  <span id="spnAddress" style="color: Red; display: none">Please enter valid address</span--%>
                        </div>
                        <%--  <div class="form-group">
                    <label>Select Role</label>
                    <asp:DropDownList AutoPostBack="true" class="form-control" placeholder="Select User in Role" ID="ddlRolesList" DataTextField="Name" DataValueField="ID" ItemType="BALAJI.GSP.APPLICATION.Infrastructure.ApplicationRole" runat="server"></asp:DropDownList>
                </div>--%>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="txt_Grossturnover">Gross TurnOver</label>
                            <asp:TextBox ID="txt_Grossturnover" Class="form-control" autocomplete="off" onkeypress="return isNumberKey(event,this);" onpaste="return false;" placeholder="Gross Turn_over" MaxLength="15" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvGrossTurnover" runat="server" Display="Dynamic" ErrorMessage="Field cannot be left blank" CssClass="help-block" ControlToValidate="txt_Grossturnover" ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="txt_ITC">ITC</label>
                            <asp:TextBox ID="txt_ITC" class="form-control" onkeypress="return isNumberKey(event,this);" autocomplete="off" placeholder="ITC" onpaste="return false;" runat="server" MaxLength="12"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvITC" runat="server" Display="Dynamic" ErrorMessage="Field cannot be left blank" CssClass="help-block" ControlToValidate="txt_ITC" ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="txtPhoneNo">Mobile No.</label>
                            <asp:TextBox ID="txtPhoneNo" class="form-control" autocomplete="off" placeholder="Mobile No." onkeypress="return isNumberKey(event,this);" onpaste="return false;" runat="server" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPhoneNo" runat="server" Display="Dynamic" CssClass="help-block" ControlToValidate="txtPhoneno" ErrorMessage="Please specify the Mobile No." ValidationGroup="vgcreateUser"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revPhoneNo" runat="server" Display="Dynamic" CssClass="help-block" ControlToValidate="txtPhoneno" ErrorMessage="Invalid Mobile No." ValidationGroup="vgcreateUser" ValidationExpression="^[6-9]\d{9}$"></asp:RegularExpressionValidator>
                            <%--<span id="spnphoneno" style="color: Red; display: none">Please enter valid phone no</span>--%>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <uc1:uc_sucess runat="server" ID="uc_sucess" />
                        <asp:Button ID="btnCreateUsers" class="btn btn-primary" ValidationGroup="vgcreateUser" OnClick="btnCreateUsers_Click" runat="server" Text="Create User" />
                    </div>
                </div>
            </div>
            <div class="box-body table-responsive no-padding">
                <asp:ListView ID="lvUsers" runat="server" OnPagePropertiesChanging="lvUsers_PagePropertiesChanging">
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

                            <td>
                                <asp:Label ID="GSTNNoLabel" runat="server" Text='<%# Eval("GSTNNo") %>' />
                            </td>
                            <td>
                                <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName") +" "+ Eval("LastName") %>' />
                            </td>
                            <td>
                                <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>' />

                            </td>
                            <td>
                                <%--<asp:Label ID="UserType" runat="server" Text='<%#Common.UserType(DataBinder.Eval(Container.DataItem,"UserType").ToString()) %>' /><%--#UserStateName(Eval("StateCode").ToString())--%>
                                <%#Common.UserType(DataBinder.Eval(Container.DataItem,"UserType").ToString()) %>
                            </td>
                            <td>
                                <%#UserStateName(Eval("StateCode")) %>
                            </td>
                            <td>
                                <asp:Label ID="Organization" runat="server" Text='<%# Eval("OrganizationName") %>' /></td>
                            <td>
                                <asp:LinkButton ID="lkbAddSignatory" data-toggle="modal" data-target='<%#"#"+ Container.DataItemIndex +"_myModalSIGN"%>' runat="server" CssClass="btn" data-trigger="hover" data-placement="right" ToolTip="Add Signatory"><i class="fa fa-eye text-orange"></i></asp:LinkButton>
                                <div class="modal" id='<%# Container.DataItemIndex +"_myModalSIGN"%>' role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                    <div class="modal-dialog" style="width: 60%;">
                                        <asp:UpdatePanel ID="upviewPurchaseRegigsterModel" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                            &times;
                                                        </button>
                                                        <h4 class="modal-title">
                                                            <b><i class="fa fa-user"></i>
                                                                <asp:Label ID="lblModalTitle" runat="server" Text="Signatory Person"></asp:Label></b>
                                                        </h4>
                                                    </div>
                                                    <div class="modal-body">
                                                        <uc1:uc_modelUser UserID='<%# Eval("ID") %>' runat="server" ID="uc_modelUser" />
                                                    </div>
                                                    <div class="modal-footer">
                                                        <button class="btn btn-info pull-right" data-dismiss="modal" aria-hidden="true">Close</button>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-responsive">
                            <tr>
                                <th style="width: 10px">#</th>
                                <th>GSTIN</th>
                                <th>Name</th>
                                <th>Email</th>
                                <th>User Type</th>
                                <th>State Name</th>
                                <th>Organization</th>
                                <th>Signatory</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>

                <div class="box-footer">
                    <div class="dataTables_paginate paging_simple_numbers pull-right">
                        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvUsers" PageSize="10" class="btn-group-sm pager-buttons">
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
        <%--<div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Users</h3>
            </div>
            <div class="box-body">
                <uc1:uc_sucess runat="server" ID="uc_sucess1" />
                <div class="table-responsive no-padding">
                </div>
                <div>
                </div>
            </div>
        </div>--%>
        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">ADD Signatory </h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>User List</label>
                            <asp:DropDownList ID="ddlUserList" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvuserList" runat="server" CssClass="help-block" Display="Dynamic" ValidationGroup="vgSignatory" ErrorMessage="Value Required" ControlToValidate="ddlUserList"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Name of Signatory</label>
                            <asp:TextBox ID="txtSignatory" CssClass="form-control" placeholder="Name of Signatory" autocomplete="off" MaxLength="30" onkeypress="return isAlpha(event,this);" onpaste="return false;" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSignatory" runat="server" CssClass="help-block" Display="Dynamic" ControlToValidate="txtSignatory" ValidationGroup="vgSignatory" ErrorMessage="Please enter the Signatory name"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label for="txt_statecode">State Name</label>
                            <asp:DropDownList ID="ddlState" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvstate" runat="server" CssClass="help-block" Display="Dynamic" ValidationGroup="vgSignatory" ErrorMessage="Value Required" ControlToValidate="ddlState"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Organization Address</label>
                            <asp:TextBox ID="txtOrgAddress" runat="server" CssClass="form-control" autocomplete="off" placeholder="Organization Address" MaxLength="30"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvOrgAdd" runat="server" CssClass="help-block" Display="Dynamic" ControlToValidate="txtOrgAddress" ValidationGroup="vgSignatory" ErrorMessage="Please enter the Organization Address "></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>GSTIN</label>
                            <asp:TextBox ID="txtGSTIN" runat="server" CssClass="form-control" autocomplete="off" MaxLength="16" placeholder="GSTIN"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvGSTIN" runat="server" CssClass="help-block" Display="Dynamic" ControlToValidate="txtGSTIN" ValidationGroup="vgSignatory" ErrorMessage="Please enter theGSTIN"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revGSTIN" runat="server" CssClass="help-block" Display="Dynamic" ControlToValidate="txtGSTIN" ValidationGroup="vgSignatory" ErrorMessage="Invalid GSTIN" ValidationExpression="^([0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1})$"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Email Id</label>
                            <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" autocomplete="off" onpaste="return false;" placeholder="Email"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="help-block" Display="Dynamic" ControlToValidate="txtemail" ValidationGroup="vgSignatory" ErrorMessage="Please enter the Email"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" CssClass="help-block" Display="Dynamic" ControlToValidate="txtemail" ValidationGroup="vgSignatory" ErrorMessage="Invalid Email Id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Pin Code</label>
                            <asp:TextBox ID="txtcode" runat="server" CssClass="form-control" autocomplete="off" onkeypress="return isNumberKey(event,this);" MaxLength="6" onpaste="return false;" placeholder="Pin Code"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvcode" runat="server" CssClass="help-block" ControlToValidate="txtcode" Display="Dynamic" ValidationGroup="vgSignatory" ErrorMessage="Please enter the Pin Code"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revcode" runat="server" CssClass="help-block" ControlToValidate="txtcode" Display="Dynamic" ValidationGroup="vgSignatory" ErrorMessage="Invalid Pincode" ValidationExpression="^[1-9][0-9]{5}$"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Mobile Number</label>
                            <asp:TextBox ID="txtphone" MaxLength="10" runat="server" CssClass="form-control" autocomplete="off" onkeypress="return isNumberKey(event,this);" onpaste="return false;" placeholder="Contact Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvConNo" runat="server" CssClass="help-block" Display="Dynamic" ControlToValidate="txtphone" ValidationGroup="vgSignatory" ErrorMessage="Please enter the Contact Number"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revConNo" runat="server" CssClass="help-block" Display="Dynamic" ControlToValidate="txtphone" ValidationGroup="vgSignatory" ErrorMessage="Invalid Mobile No." ValidationExpression="^[6-9]\d{9}$"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group">
                            <%--  <label >Certificate Key</label>
            <asp:TextBox ID="txtCertKey" runat="server" CssClass="form-control" Visible="false" placeholder="Certificate Key" Width="300px"></asp:TextBox>--%>
                            <%--<asp:RequiredFieldValidator ID="rfvCertKey" runat="server" CssClass="help-block" Display="Dynamic" ValidationGroup="vgSignatory" ErrorMessage="Please enter the Signatory name" ></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                </div>
                <div class="row" style="display: none;">
                    <div class="col-sm-4">
                        <div class="form-group">
                            <label></label>
                            <asp:TextBox ID="txtzbc" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-8">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <uc1:uc_sucess runat="server" ID="uc_sucess1" />
                        <asp:LinkButton ID="lkbAdd" OnClick="lkbAdd_Click" CssClass="btn btn-primary" ValidationGroup="vgSignatory" runat="server">Add Signatory</asp:LinkButton>
                    </div>
                </div>
                <%--  <uc1:uc_modalUserList runat="server" ID="uc_modalUserList" />--%>
            </div>
        </div>
    </div>

</asp:Content>



