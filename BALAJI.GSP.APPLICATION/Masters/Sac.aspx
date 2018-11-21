<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="Sac.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Masters.Sac" %>

<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>


<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-header">
        <h1>SAC
            <small>Service Accounting Code</small> </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Masters</a></li>
            <li class="active">SAC</li>
        </ol>
    </div>
    <div class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Add</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove">
                        <i class="fa fa-remove"></i>
                    </button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>Sub-Class Name</label>
                            <asp:DropDownList ID="ddlSubClass" CssClass="form-control" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSubClass" runat="server" ValidationGroup="vgSave" CssClass="help-block" Display="Dynamic" ErrorMessage="Value Required" InitialValue="0" ControlToValidate="ddlSubClass"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>SAC Code</label>
                            <asp:TextBox ID="txtSacCode" CssClass="form-control" onkeypress="return isDigitKey(event,this);" onpaste="return false;" placeholder="SAC Code" autocomplete="off" MaxLength="10" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSacCode" runat="server" ValidationGroup="vgSave" CssClass="help-block" Display="Dynamic" ErrorMessage="This field cannot be left empty" ControlToValidate="txtSacCode"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-1">
                        <div class="form-group">
                            <label>IGST</label>
                            <asp:TextBox ID="txtIgst" class="form-control" onkeypress="return isNumberKey(event,this);" onpaste="return false;" placeholder="IGST" autocomplete="off" MaxLength="5" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvIgst" runat="server" ValidationGroup="vgSave" CssClass="help-block" Display="Dynamic" ErrorMessage="Specify the IGST." ControlToValidate="txtIgst"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-1">
                        <div class="form-group">
                            <label>CGST</label>
                            <asp:TextBox ID="txtCgst" class="form-control" onkeypress="return isNumberKey(event,this);" onpaste="return false;" placeholder="CGST" autocomplete="off" MaxLength="5" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCgst" runat="server" ValidationGroup="vgSave" CssClass="help-block" Display="Dynamic" ErrorMessage="Specify the CGST." ControlToValidate="txtCgst"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-1">
                        <div class="form-group">
                            <label>SGST</label>
                            <asp:TextBox ID="txtSgst" class="form-control" onkeypress="return isNumberKey(event,this);" onpaste="return false;" placeholder="SGST" autocomplete="off" MaxLength="5" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSgst" runat="server" ValidationGroup="vgSave" CssClass="help-block" Display="Dynamic" ErrorMessage="Specify the SGST." ControlToValidate="txtSgst"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-1">
                        <div class="form-group">
                            <label>UTGST</label>
                            <%--<asp:TextBox ID="txtUgst" class="form-control" onkeypress="return onlyDecNos(event,this);" onpaste="return false;" placeholder="UTGST" MaxLength="5" runat="server"></asp:TextBox>--%>
                            <asp:TextBox ID="txtUgst" class="form-control" onkeypress="return isNumberKey(event,this);" onpaste="return false;" placeholder="UTGST" MaxLength="5" autocomplete="off" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUgst" runat="server" ValidationGroup="vgSave" CssClass="help-block" Display="Dynamic" ErrorMessage="Specify the SGST." ControlToValidate="txtUgst"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-1">
                        <div class="form-group">
                            <label>Cess</label>
                            <asp:TextBox ID="txtCess" class="form-control" onkeypress="return isNumberKey(event,this);" onpaste="return false;" placeholder="Cess" autocomplete="off" MaxLength="5" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCess" runat="server" ValidationGroup="vgSave" CssClass="help-block" Display="Dynamic" ErrorMessage="Specify the Cess." ControlToValidate="txtCess"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Spl Cond. Applied</label>
                            <asp:DropDownList ID="ddlSpclCondition" DataTextField="Special-Conition" AutoPostBack="true" CssClass="form-control nullAdd" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSpclCondition" runat="server" ValidationGroup="vgSave" CssClass="help-block" Display="Dynamic" InitialValue="0" ErrorMessage="Value Required." ControlToValidate="ddlSpclCondition"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>IS Notified</label>
                            <asp:DropDownList ID="ddlNotified" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                         <%--   <asp:RequiredFieldValidator ID="rfvNotified" runat="server" ValidationGroup="vgSave" CssClass="help-block" Display="Dynamic" InitialValue="0" ErrorMessage="Value Required." ControlToValidate="ddlNotified"></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>

                    <div class="col-md-2">
                        <div class="form-group">
                            <label>IS Non GST Goods</label>
                            <asp:DropDownList ID="ddlNonGSTGoods" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                           <%-- <asp:RequiredFieldValidator ID="rfvNonGSTGoods" runat="server" ValidationGroup="vgSave" CssClass="help-block" Display="Dynamic" InitialValue="0" ErrorMessage="Value Required." ControlToValidate="ddlNonGSTGoods"></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>Is Nil Rated</label>
                            <asp:DropDownList ID="ddlIsNilRated" CssClass="form-control" runat="server"></asp:DropDownList>
                          <%--  <asp:RequiredFieldValidator ID="rfvIsNilRated" runat="server" ValidationGroup="vgSave" CssClass="help-block" Display="Dynamic" InitialValue="0" ErrorMessage="Value Required." ControlToValidate="ddlIsNilRated"></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>IS Exempted</label>
                            <asp:DropDownList ID="ddlExempted" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                           <%-- <asp:RequiredFieldValidator ID="rfvExempted" runat="server" ValidationGroup="vgSave" CssClass="help-block" Display="Dynamic" InitialValue="0" ErrorMessage="Value Required." ControlToValidate="ddlExempted"></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label>IS Zero Rated</label>
                            <asp:DropDownList ID="ddlZeroRated" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                          <%--  <asp:RequiredFieldValidator ID="rfvZeroRated" runat="server" ValidationGroup="vgSave" CssClass="help-block" Display="Dynamic" InitialValue="0" ErrorMessage="Value Required." ControlToValidate="ddlZeroRated"></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group">
                            <label>Description</label>
                            <asp:TextBox ID="txtDescription" class="form-control" placeholder="Description limit is 500 characters" MaxLength="500" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ValidationGroup="vgSave" CssClass="help-block" Display="Dynamic" ErrorMessage="This field cannot be left empty" ControlToValidate="txtDescription"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-4">
                        <uc1:uc_sucess runat="server" ID="uc_sucess_sac" />
                        <asp:Button ID="btnsave" CssClass="btn btn-primary" ValidationGroup="vgSave" runat="server" Text="Submit" OnClick="btnsave_Click" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="box-group" id="accordion">
                        <!-- we are adding the .panel class so bootstrap.js collapse plugin detects it -->
                        <div class="panel box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title"><a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="false" class="collapsed">Notified
                                </a></h3>
                            </div>
                            <div id="collapseOne" class="panel-collapse collapse" aria-expanded="false" style="height: 0px;">
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label>Notified SAC</label>
                                                <asp:DropDownList ID="ddlNotifiedItem" CssClass="form-control" runat="server"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvNotifiedItem" runat="server" CssClass="help-block" Display="Dynamic" ValidationGroup="vgNotified" ErrorMessage="Value Required" InitialValue="0" ControlToValidate="ddlNotifiedItem"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label>Notification No.</label>
                                                <asp:TextBox ID="txtNotifNo" class="form-control" onkeypress="return isDigitKey(event,this);" onpaste="return false;" autocomplete="off" placeholder="Notification No." MaxLength="20" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvNotifNo" runat="server" CssClass="help-block" Display="Dynamic" ValidationGroup="vgNotified" ErrorMessage="Please specify the Notification No." ControlToValidate="txtNotifNo"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label>Notification S.No.</label>
                                                <asp:TextBox ID="txtNotifSerialNo" class="form-control" onkeypress="return isDigitKey(event,this);" autocomplete="off" onpaste="return false;" placeholder="Notification S. No" MaxLength="10" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvSerialNo" runat="server" CssClass="help-block" Display="Dynamic" ValidationGroup="vgNotified" ErrorMessage="Please specify the Notification Serial No." ControlToValidate="txtNotifSerialNo"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label>Tariff Duty</label>
                                                <asp:TextBox ID="txtTariff" class="form-control" onkeypress="return isNumberKey(event,this);" onpaste="return false;" autocomplete="off" placeholder="Tariff Duty" MaxLength="5" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvTariff" runat="server" CssClass="help-block" Display="Dynamic" ValidationGroup="vgNotified" ErrorMessage="Please specify the Tariff Duty." ControlToValidate="txtTariff"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label>Is Condition</label>
                                                <asp:DropDownList ID="ddlIsCondition" CssClass="form-control" runat="server"></asp:DropDownList>
                                               <%-- <asp:RequiredFieldValidator ID="rfvIsCondition" runat="server" CssClass="help-block" Display="Dynamic" ValidationGroup="vgNotified" InitialValue="0" ErrorMessage="Value Required" ControlToValidate="ddlIsCondition"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label>Description</label>
                                                <asp:TextBox ID="txtDescr" class="form-control" placeholder="Description limit is 500 characters." MaxLength="500" TextMode="MultiLine" Rows="2" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDescr" runat="server" CssClass="help-block" ValidationGroup="vgNotified" Display="Dynamic" ErrorMessage="This field cannot be left empty." ControlToValidate="txtDescr"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <uc1:uc_sucess runat="server" ID="uc_sucess_notified" />
                                            <asp:Button ID="btnNotified" CssClass="btn btn-primary" OnClick="btnNotified_Click" ValidationGroup="vgNotified" runat="server" Text="Save Notified" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="box-group" id="accordion1">
                        <!-- we are adding the .panel class so bootstrap.js collapse plugin detects it -->
                        <div class="panel box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" class="collapsed">Condition
                                    </a>
                                </h3>
                            </div>
                            <div id="collapseTwo" class="panel-collapse collapse" aria-expanded="false" style="height: 0px;">
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label>
                                                    Notified Item</label>
                                                <asp:DropDownList ID="ddlConditionItems" CssClass="form-control" runat="server">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="help-block" Display="Dynamic" ErrorMessage="Value Required" InitialValue="0" ValidationGroup="vgCondition" ControlToValidate="ddlConditionItems"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label>
                                                    Condition No.</label>
                                                <asp:TextBox ID="txtCondition" class="form-control" onkeypress="return isDigitKey(event,this);" autocomplete="off" onpaste="return false;" placeholder="Condition No." MaxLength="20" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvCondition" runat="server" CssClass="help-block" Display="Dynamic" ValidationGroup="vgCondition" ErrorMessage="Please specify the Condition No." ControlToValidate="txtCondition"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <label>
                                                    Condition S. No.</label>
                                                <asp:TextBox ID="txtConditionSerial" class="form-control" onkeypress="return isDigitKey(event,this);" autocomplete="off" onpaste="return false;" placeholder="Condition S. No." MaxLength="20" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvConditionSerial" runat="server" CssClass="help-block" Display="Dynamic" ValidationGroup="vgCondition" ErrorMessage="Please specify the Condition S. No." ControlToValidate="txtConditionSerial"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label>Description</label>
                                                <asp:TextBox ID="txtDesc" class="form-control" placeholder="Description limit is 500 characters" MaxLength="500" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDesc" runat="server" CssClass="help-block" Display="Dynamic" ValidationGroup="vgCondition" ErrorMessage="This field cannot be left empty" ControlToValidate="txtDesc"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <uc1:uc_sucess runat="server" ID="uc_sucess_condition" />
                                                <asp:Button ID="btnCondition" CssClass="btn btn-primary" ValidationGroup="vgCondition" runat="server" Text="Submit" OnClick="btnCondition_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="box-body table-responsive no-padding">
                        <asp:ListView ID="lvSac" runat="server" OnPagePropertiesChanging="lvSac_PagePropertiesChanging">
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
                                    <td><%#DataBinder.Eval(Container.DataItem,"GST_MST_SUBCLASS.SubClassCode")%></td>
                                    <td><%# Eval("ItemCode") %></td>
                                    <td><%# Eval("Description") %></td>
                                    <%-- <td><%# Eval("IsCondition") %></td>--%>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem,"IsNilRated").ToString() == "False" ? "<span class='label label-danger'>"+ Eval("IsNilRated") + "</span>": "<span class='label label-success'>"+ Eval("IsNilRated") + "</span>" %></td>
                                    <td><%#Eval("IsExempted").ToString()== "False" ? "<span class='label label-danger'>" : "<span class='label label-success'>" %><%# Eval("IsExempted") %></td>
                                    <td><%#Eval("IsZeroRated").ToString()== "False" ? "<span class='label label-danger'>" : "<span class='label label-success'>" %><%# Eval("IsZeroRated") %></td>
                                    <td><%#Eval("IsNonGSTGoods").ToString()== "False" ? "<span class='label label-danger'>" : "<span class='label label-success'>" %><%# Eval("IsNonGSTGoods") %></td>

                                    <td>
                                        <asp:LinkButton ID="lkbSac" OnClick="lkbSac_Click" CommandArgument='<%# Eval("Item_ID") %>' runat="server" ToolTip="Edit"><i class="fa fa-edit text-green"></i></asp:LinkButton>
                                        <span class="separator"></span>
                                        <div class="modal fade" id='<%# Container.DataItemIndex +"_myModalhsn"%>' role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                            <div class="modal-dialog">
                                                <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="modal-content">
                                                            <div class="modal-header">
                                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                                <h4 class="modal-title">
                                                                    <asp:Label ID="lblModalTitle" runat="server" Text="----"></asp:Label></h4>
                                                            </div>
                                                            <div class="modal-body">
                                                                Ankita
                                                                <%--   <asp:ListView ID="lvHSNData" DataKeyNames="Tarrif,SerialNo" runat="server">
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
                                                                    </asp:ListView>--%>
                                                            </div>
                                                            <div class="modal-footer">
                                                                <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </td>
                                    <%-- <td>
                                        <asp:LinkButton ID="lkb1" runat="server" data-toggle="modal" data-target='<%#"#"+ Container.DataItemIndex +"_myModalhsn"%>' ToolTip="View"><i class="fa fa-eye text-orange""></i></asp:LinkButton>
                                    </td>--%>
                                </tr>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <table class="table">
                                    <tr>
                                        <th style="width: 4%">#</th>
                                        <th style="width: 10%">SubClass Code</th>
                                        <th style="width: 10%">SAC Code</th>
                                        <th style="width: 32%">Description</th>
                                        <th style="width: 10%">Nil-Rated</th>
                                        <th style="width: 10%">Exempted</th>
                                        <th style="width: 10%">Zero Rated</th>
                                        <th style="width: 10%">Non-GST Goods</th>
                                        <th style="width: 2%"></th>
                                        <th style="width: 2%"></th>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server">
                                            </tr>
                                        </tbody>
                                    </tr>
                                </table>
                            </LayoutTemplate>
                        </asp:ListView>
                        <div class="box-footer clearfix">
                            <div class="pagination pagination-sm no-margin">
                                <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvSac" PageSize="10" class="btn-group-sm pager-buttons">
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


</asp:Content>
