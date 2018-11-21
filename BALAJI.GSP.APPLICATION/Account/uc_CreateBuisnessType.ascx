<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_CreateBuisnessType.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.Account.uc_CreateBuisnessType" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>


<div class="box box-primary">
    <div class="box-header with-border">
        <h3 class="box-title">Create Business Type</h3>
    </div>
    <div class="box-body">
        <asp:HiddenField ID="hdn_BusinessTypeId" runat="server" />
        <div class="row">
            <div class="col-md-3">
                <div class="form-group">
                    <label for="txtBuisnessTypeName">Business Type Name</label>
                    <asp:TextBox ID="txtBuisnessTypeName" class="form-control" onkeypress="return isAlpha(event,this);" onpaste="return false;" autocomplete="off" placeholder="Business Type Name" runat="server" MaxLength="15" TabIndex="1"></asp:TextBox>
                </div>
            </div>


        </div>
    </div>
    <div class="box-footer">
        <uc1:uc_sucess runat="server" ID="uc_sucess" />
        <asp:Button ID="btnCreateBuisnessType" class="btn btn-primary" ValidationGroup="vgcreateUser" OnClick="btnCreateBuisnessType_Click" runat="server" Text="Submit" TabIndex="2" />
    </div>
</div>


<div class="box box-primary">
    <div class="box-header with-border">
        <h3 class="box-title">Business Type List</h3>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <div class="box-body table-responsive no-padding">
                <asp:ListView ID="lvBusiness" runat="server" OnPagePropertiesChanging="lvBusinessType" >
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
                            <td><%# Eval("BusinessType") %></td>
                            <td>
                                <asp:LinkButton ID="lkbEdit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("BusinessId") %>' OnClick="lkbEdit_Click"><i class="fa fa-edit text-green"></i></asp:LinkButton>
                            <asp:LinkButton ID="lkbRemoveButton" runat="server"  ToolTip="Delete" OnClick="RemoveBusinessType" CommandArgument='<%# Eval("BusinessId") %>' OnClientClick="return confirm('Are you sure you want delete');" CausesValidation="false"><i class="fa fa-trash-o text-red"></i></asp:LinkButton>
                            <%--   <asp:Button runat="server" Text="Remove"  />--%> 
                        </td>
                            <%--<td><%# DateTimeAgo.GetFormatDate(Eval("CreatedDate").ToString()) %></td>
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
                                                            <asp:Label ID="lblModalTitle" runat="server" Text="View Report"></asp:Label>
                                                            </b></h4>
                                                    </div>
                                                    <div class="modal-body">
                                                        <div class="form-group">
                                                            <label>
                                                            Add</label>
                                                            <asp:CheckBoxList ID="chkbox" runat="server">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                       <asp:CheckBox runat="server" ID="ChckBStatus" Text="Status" OnCheckedChanged="ChckBStatus_CheckedChanged" />
                                                        <div class="table table-responsive">
                                                            <asp:ListView ID="lvReportPermission" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem,"GST_MST_REPORT_PERMISSION") %>'>
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
                                                                        <td><%# Eval("ReportName") %></td>
                                                                        <td><%# Eval("ReportPath") %></td>
                                                                        
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <LayoutTemplate>
                                                                    <table class="table table responsive table-condensed table-bordered">
                                                                        <tr>
                                                                            <th>#</th>
                                                                            <th>Report Name</th>
                                                                            <th>Report Path</th>
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
                                    </td>--%>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table">
                            <tr>
                                <th style="width: 10px">#</th>
                                <th>Business type</th>
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
                    <asp:DataPager ID="dpBusinessType" runat="server" PagedControlID="lvBusiness" PageSize="10" class="btn-group-sm pager-buttons">
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
