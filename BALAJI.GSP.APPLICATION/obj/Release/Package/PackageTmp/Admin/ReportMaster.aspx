<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="ReportMaster.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Admin.ReportMaster" %>

<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>
<%@ Register Src="~/UC/uc_ModalReport.ascx" TagPrefix="uc1" TagName="uc_ModalReport" %>


<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-header">
        <h1>Report Master
            <small></small></h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Report Master</li>
        </ol>
    </div>
    <div class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Add Report</h3>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>
                                Report Name</label>
                            <asp:TextBox ID="txtReportName" class="form-control" autocomplete="off" placeholder="Enter Report Name" onkeypress="return isAlpha(event,this);" onpaste="return false;" MaxLength="50" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvReportName" runat="server" ValidationGroup="vgReport" CssClass="help-block" ErrorMessage="Please enter Report name" Display="Dynamic" ControlToValidate="txtReportName"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>
                                Report Path</label>
                            <asp:TextBox ID="txtReportPath" class="form-control" autocomplete="off" placeholder="Enter Report Path" onkeypress="return isAlpha(event,this);" onpaste="return false;" MaxLength="50" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvReportPath" runat="server" ValidationGroup="vgReport" CssClass="help-block" ErrorMessage="Please enter Report path" Display="Dynamic" ControlToValidate="txtReportPath"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>
                                Report Control Name</label>
                            <asp:TextBox ID="txtReportControl" class="form-control" autocomplete="off" placeholder="Enter Report Control Name" onkeypress="return isAlpha(event,this);" onpaste="return false;" MaxLength="50" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvReportControl" runat="server" ValidationGroup="vgReport" CssClass="help-block" ErrorMessage="Please enter Report control name" Display="Dynamic" ControlToValidate="txtReportControl"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>
                                Serial Number</label>
                            <asp:TextBox ID="txtSerial" class="form-control" onkeypress="return isDigitKey(event);" onpaste="return false;" autocomplete="off" placeholder="Enter Serial Number"  MaxLength="20" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSerial" runat="server" ValidationGroup="vgReport" CssClass="help-block" ErrorMessage="Please enter Serial Number" Display="Dynamic" ControlToValidate="txtSerial"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-footer">
                <uc1:uc_sucess runat="server" ID="uc_sucess" />
                <asp:Button ID="btnRegistration" CssClass="btn btn-primary pull-left" ValidationGroup="vgReport" runat="server" Text="Submit" OnClick="btnRegistration_Click" />
            </div>
            <div class="row">
                <div class="modal" id='modal1' role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog" style="overflow-y: auto; max-height:85%;overflow:hidden;">
                        <div class="modal-content" style="height: auto !important;">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                    &times;
                                </button>
                                <h4 class="modal-title"><b><i class="fa fa-globe"></i>
                                    <asp:Label ID="lblModalTitle" runat="server" Text="View Report"></asp:Label>
                                </b></h4>
                            </div>
                                <uc1:uc_ModalReport runat="server" ID="uc_ModalReport" />
                            <%--<div class="modal-footer">
                                <uc1:uc_sucess runat="server" ID="uc_sucess1" />
                                <asp:LinkButton ID="btnSubmit" CssClass="btn btn-info" runat="server" OnClientClick="$('.modal-backdrop').remove();">Submit</asp:LinkButton>
                            </div>--%>
                        </div>
                    </div>
                </div>

                <div class="col-sm-12">
                    <div class="box-body table-responsive no-padding">
                        <asp:ListView ID="lvReport" runat="server" OnPagePropertiesChanging="lvReport_PagePropertiesChanging">
                            <EmptyDataTemplate>
                                <table class="table">
                                    <tr>
                                        <td>No data was returned</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%# Container.DataItemIndex + 1%>.</td>
                                    <td><%# Eval("SerialNo") %></td>
                                    <td><%# Eval("ReportName") %></td>
                                    <td><%# Eval("ReportPath") %></td>
                                    <td><%# Eval("ReportControlName") %></td>
                                    <td><%# DateTimeAgo.GetFormatDate(Eval("CreatedDate").ToString()) %></td>
                                    <td><%#Eval("Status").ToString()== "False" ? "<span class='label label-danger'>" : "<span class='label label-success'>" %><%# Eval("Status") %></td>
                                    <td>
                                        <asp:LinkButton ID="lkbView" OnClick="lkbView_Click" CommandArgument='<%#Eval("ReportId") %>' runat="server" ToolTip="View"><i class="fa fa-eye text-orange"></i></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <table class="table">
                                    <tr>
                                        <th style="width: 10px">#</th>
                                        <th>Serial No.</th>
                                        <th>Report Name</th>
                                        <th>Report Path</th>
                                        <th>Report Control Name</th>
                                        <th>Created Date</th>
                                        <th>Status</th>
                                        <th>View</th>
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

            <div class="box-footer clearfix">
                <div class="pagination pagination-sm no-margin pull-right">
                    <%--<div class="box-body dataTables_paginate paging_simple_numbers">--%>
                    <asp:DataPager ID="dpReportPermission" runat="server" PagedControlID="lvReport" PageSize="10" class="btn-group-sm pager-buttons">
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
</asp:Content>
