<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeBehind="StateMaster.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Masters.StateMaster" %>

<%@ Register Src="../UC/uc_sucess.ascx" TagName="uc_sucess" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
        <h1>State Master
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Masters</a></li>
            <li class="active">State</li>
        </ol>
    </section>
    <div class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Add</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>State Code</label>
                            <asp:TextBox ID="txtStateCode" runat="server" onpaste="return false;" CssClass="form-control" onkeypress="return isDigitKey(event,this);" autocomplete="off" MaxLength="2" placeholder="State Code"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvStateCode" ValidationGroup="vgState" CssClass="help-block" runat="server" ErrorMessage="Please enter state code" Display="Dynamic" ControlToValidate="txtStateCode"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>State Name</label>
                            <asp:TextBox ID="txtStateName" runat="server" Style="text-transform: uppercase;" onpaste="return false;" CssClass="form-control" onkeypress="return isAlpha(event,this);" autocomplete="off" MaxLength="30" placeholder="State Name"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvStateName" ValidationGroup="vgState" CssClass="help-block" runat="server" ErrorMessage="Please enter state name" Display="Dynamic" ControlToValidate="txtStateName"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Compounding Ceiling Amount</label>
                            <asp:TextBox ID="txtCompInt" runat="server" onpaste="return false;" CssClass="form-control" onkeypress="return isNumberKey(event,this);" autocomplete="off" MaxLength="10" placeholder="Compounding Interest"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCompInt" ValidationGroup="vgState" CssClass="help-block" runat="server" ErrorMessage="Please enter Compounding Interest" Display="Dynamic" ControlToValidate="txtCompInt"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-goup">
                            <label>IS Exempted</label>
                            <asp:DropDownList ID="ddlExempted" CssClass="form-control" runat="server"></asp:DropDownList>
                           <%--  <asp:RequiredFieldValidator ID="rfvExempted" runat="server" CssClass="help-block" Display="Dynamic" ValidationGroup="vgState" ErrorMessage="Value Required" InitialValue="0" ControlToValidate="ddlExempted">--%></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-2"></div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group">
                            <asp:CheckBox ID="chkUT" runat="server" />
                            <label>Union Territory</label>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <uc1:uc_sucess ID="uc_sucess" runat="server" />
                        <asp:Button ID="btnAddState" CssClass="btn btn-primary" ValidationGroup="vgState" runat="server" Text="Submit" OnClick="btnAddState_Click" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="box-body table-responsive no-padding">
                        <asp:ListView ID="lvstate" runat="server" OnPagePropertiesChanging="lvstate_PagePropertiesChanging">
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
                                    <td>
                                        <asp:Label ID="lblStateCode" runat="server" Text='<%# Eval("StateCode") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStateName" runat="server" Text='<%# Eval("StateName") %>' />
                                    </td>
                                    <td>
                                        <%# Eval("CompCeilingAmount") %>
                                    </td>
                                    <td><%#Eval("UT").ToString()=="False" ? "<span class='label label-danger'>" : "<span class='label label-success'>" %><%# Eval("UT") %></span>
                                    </td>
                                    <td>
                                        <%#Eval("CreatedDate")==null?"": DateTimeAgo.GetFormatDate(Eval("CreatedDate").ToString()) %>
                                        <%-- <%# Eval("CreatedDate") %>--%>
                                    </td>
                                    <td><%#Eval("Status").ToString()=="False" ? "<span class='label label-danger'>" : "<span class='label label-success'>" %><%# Eval("Status") %></span>
                                    </td>
                                    <td><%#Eval("IsExempted").ToString()=="False" ? "<span class='label label-danger'>" : "<span class='label label-success'>" %><%# Eval("IsExempted") %></td>
                                    <td>
                                        <asp:LinkButton ID="lkbState_action" runat="server" CommandArgument='<%# Eval("StateID") %>' OnClick="lkbState_action_Click" ToolTip="Edit"><i class="fa fa-edit text-green"></i></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <table class="table">
                                    <tr>
                                        <th style="width: 3%">#</th>
                                        <th style="width: 10%">State Code</th>
                                        <th style="width: 26%">State Name</th>
                                        <th style="width: 18%">Compounding Ceiling Amt</th>
                                        <th style="width: 10%">UT</th>
                                        <th style="width: 10%">Created Date</th>
                                        <th style="width: 10%">Status</th>
                                        <th style="width: 10%">Is Exempted</th>
                                        <th style="width: 3%">#</th>
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
                                <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvstate" PageSize="20" class="btn-group-sm pager-buttons">
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
