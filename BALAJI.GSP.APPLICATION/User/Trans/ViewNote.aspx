<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeBehind="ViewNote.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.User.Trans.ViewNote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-header">
        <h1>CREDIT / DEBIT NOTES  
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">LEDGRES</a></li>
            <li class="#">CREDIT / DEBIT NOTES</li>
            <li class="active">View</li>
        </ol>

    </div>
    <div class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="nav-tabs-custom">
                    <ul class="nav nav-tabs">
                        <li class="active">
                            <a href="#Received" data-toggle="tab">Received</a></li>
                        <li>
                            <a href="#Issued" data-toggle="tab">Issued</a></li>

                    </ul>

                    <div class="tab-content">
                        <div class="tab-pane active" id="Received">
                            <div class="box-body table no-padding">
                                <asp:ListView ID="lvNote_Received" runat="server">
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
                                            <td><%# Eval("NoteNumber") %> </td>
                                            <td><%# Eval("Description") %> </td>
                                            <td><%#DataBinder.Eval(Container.DataItem,"AspNetUser.OrganizationName") %></td>
                                            <td><%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.InvoiceNo")  %> </td>
                                            <td><%#DateTimeAgo.GetFormatDate(Eval("CDN_Date")) %></td>
                                            <td><%#BusinessLogic.Repositories.cls_CreditDebit_Note.CreditColor(Eval("NoteType")) %></td>
                                            <%--   <td><%#Eval("NoteType").ToString()== "Credit" ? "<span class='label label-primary'>" : "<span class='label label-success'>" %><%# Eval("NoteType") %></td>--%>
                                            <%-- <td><%# Eval("NoteTypeStatus")%>  </td>--%>
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
                                                                    <asp:Label ID="lblModalTitle" runat="server" Text="CDN Items"></asp:Label>
                                                                </b></h4>
                                                            </div>
                                                            <div class="modal-body">
                                                                <div class="table table-responsive">
                                                                    <asp:ListView ID="lstCDNNote" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_CRDR_NOTE_DATA")%>' ItemType="DataAccessLayer.GST_TRN_CRDR_NOTE_DATA">
                                                                        <EmptyDataTemplate>
                                                                            <table class="table  table-bordered table-condensed">
                                                                                <tr>
                                                                                    <td>No data was returned.</td>
                                                                                </tr>
                                                                            </table>
                                                                        </EmptyDataTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td><%# Container.DataItemIndex + 1%></td>
                                                                                <td style="text-align: right;">
                                                                                    <%# DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.ItemCode")%>
                                                                                </td>
                                                                                <td style="text-align: right;">
                                                                                    <%# DataBinder.Eval(Container.DataItem,"Qty")%>
                                                                                </td>
                                                                                <td style="text-align: right;">
                                                                                    <%# DataBinder.Eval(Container.DataItem,"Rate")%>
                                                                                </td>
                                                                                <td style="text-align: right;">
                                                                                    <%#DataBinder.Eval(Container.DataItem,"TotalAmount")%>
                                                                                </td>
                                                                                <%--<td style="text-align: center;">
                                                                            <%#DataBinder.Eval(Container.DataItem,"Status")%>
                                                                        </td>--%>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <LayoutTemplate>
                                                                            <table class="table table-responsive table-condensed">
                                                                                <tr>
                                                                                    <th style="width: 2%;">#</th>
                                                                                    <th style="text-align: right;">Item Code</th>
                                                                                    <th style="text-align: right;">Qty.</th>
                                                                                    <th style="text-align: right;">Rate</th>
                                                                                    <th style="text-align: right;">Total Amount (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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
                                        </tr>
                                    </ItemTemplate>

                                    <LayoutTemplate>
                                        <table class="table table-responsive dataTable" id="lvItems">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Note Number </th>
                                                    <th>Description</th>
                                                    <th>Received From</th>
                                                    <th>Invoice No.</th>
                                                    <th>CDN Date</th>
                                                    <th>Note </th>
                                                    <th>Action</th>

                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server">
                                                </tr>
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                        <div class="tab-pane" id="Issued">
                            <div class="box-body table no-padding">
                                <asp:ListView ID="lv_Issued" runat="server">
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
                                            <td><%# Eval("NoteNumber") %> </td>
                                            <td><%# Eval("Description") %> </td>
                                            <td><%#DataBinder.Eval(Container.DataItem,"AspNetUser1.OrganizationName") %></td>
                                            <td><%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.InvoiceNo")  %> </td>
                                            <td><%#DateTimeAgo.GetFormatDate(Eval("CDN_Date")) %></td>
                                            <td><%#BusinessLogic.Repositories.cls_CreditDebit_Note.CreditColor(Eval("NoteType")) %></td>
                                            <%--   <td><%#Eval("NoteType").ToString()== "Credit" ? "<span class='label label-primary'>" : "<span class='label label-success'>" %><%# Eval("NoteType") %></td>--%>
                                            <%-- <td><%# Eval("NoteTypeStatus")%>  </td>--%>
                                            <td>
                                                <asp:LinkButton ID="lkbView" runat="server" data-toggle="modal" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' ToolTip="View"><i class="fa fa-eye text-orange"></i></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="lkbEdit" runat="server" OnClick="lkbEdit_Click" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"CreditDebitID")  %>' ToolTip="Edit"><i class="fa fa-edit text-green"></i></asp:LinkButton>
                                                <div class="modal fade" id='<%# Container.DataItemIndex +"_myModalINV"%>' role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                                    <div class="modal-dialog">
                                                        <div class="modal-content">
                                                            <div class="modal-header">
                                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                                    &times;
                                                                </button>
                                                                <h4 class="modal-title"><b><i class="fa fa-globe"></i>
                                                                    <asp:Label ID="lblModalTitle" runat="server" Text="CDN Items"></asp:Label>
                                                                </b></h4>
                                                            </div>
                                                            <div class="modal-body">
                                                                <div class="table table-responsive">
                                                                    <asp:ListView ID="lstCDNNote" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_CRDR_NOTE_DATA")%>' ItemType="DataAccessLayer.GST_TRN_CRDR_NOTE_DATA">
                                                                        <EmptyDataTemplate>
                                                                            <table class="table  table-bordered table-condensed">
                                                                                <tr>
                                                                                    <td>No data was returned.</td>
                                                                                </tr>
                                                                            </table>
                                                                        </EmptyDataTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td><%# Container.DataItemIndex + 1%></td>
                                                                                <td style="text-align: right;">
                                                                                    <%# DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.ItemCode")%>
                                                                                </td>
                                                                                <td style="text-align: right;">
                                                                                    <%# DataBinder.Eval(Container.DataItem,"Qty")%>
                                                                                </td>
                                                                                <td style="text-align: right;">
                                                                                    <%# DataBinder.Eval(Container.DataItem,"Rate")%>
                                                                                </td>
                                                                                <td style="text-align: right;">
                                                                                    <%#DataBinder.Eval(Container.DataItem,"TotalAmount")%>
                                                                                </td>
                                                                                <%--<td style="text-align: center;">
                                                                            <%#DataBinder.Eval(Container.DataItem,"Status")%>
                                                                        </td>--%>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <LayoutTemplate>
                                                                            <table class="dataTable table-responsive table-condensed">
                                                                                <tr>
                                                                                    <th style="width: 2%;">#</th>
                                                                                    <th style="text-align: right;">Item Code</th>
                                                                                    <th style="text-align: right;">Qty.</th>
                                                                                    <th style="text-align: right;">Rate</th>
                                                                                    <th style="text-align: right;">Total Amount (<i style="font-size: 10px;" class="fa fa-inr"></i>)</th>
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

                                        </tr>
                                    </ItemTemplate>

                                    <LayoutTemplate>
                                        <table class="table table-responsive dataTable" id="lvItems">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Note Number </th>
                                                    <th>Description</th>
                                                    <th>Issue To</th>
                                                    <th>Invoice No.</th>
                                                    <th>CDN Date</th>
                                                    <th>Note </th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server">
                                                </tr>
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                </asp:ListView>
                            </div>
                        </div>




                        
                                                            <div class="modal-body">
                                                                <div class="table table-responsive">
                                                                    <asp:GridView ID="gvEditViewNote"   CssClass="table table-responsive no-padding table-striped table-bordered table-condensed" AutoGenerateColumns="false" runat="server">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="#" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <%# Container.DataItemIndex + 1%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Item Code" ItemStyle-Width="10%">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtItemCode" AutoPostBack="true" Text='<%#DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.ItemCode") %>' autocomplete="off" MaxLength="8" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Qty." ItemStyle-Width="10%">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtQty" MaxLength="6" AutoPostBack="true" Text='<%#DataBinder.Eval(Container.DataItem,"Qty") %>'  CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Rate" ItemStyle-Width="10%">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtRate" CssClass="form-control input-sm" Text='<%#DataBinder.Eval(Container.DataItem,"Rate") %>'  runat="server"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Total Amount" ItemStyle-Width="10%">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtTotalAmount" CssClass="form-control input-sm" Text='<%#DataBinder.Eval(Container.DataItem,"TotalAmount") %>' onkeypress="return onlyDecNos(event,this);" MaxLength="10" runat="server"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>


                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $('#lvItems').DataTable(); // js goes in here.
        });
    </script>
</asp:Content>
