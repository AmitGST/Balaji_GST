<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/User/User.master" CodeBehind="ViewInvoice.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.User.uinvoice.ViewInvoice" %>
<%@ MasterType VirtualPath="~/User/User.master" %>
<%@ Register Src="~/UC/UC_Invoice/uc_invoiceR.ascx" TagPrefix="uc1" TagName="uc_invoiceR" %>
<%@ Register Src="~/UC/UC_Invoice/uc_InvoiceView.ascx" TagPrefix="uc1" TagName="uc_InvoiceView" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>



<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%--<div class="row">
        <div class="col-lg-12">
            <h2 class="page-header">View Invoice</h2>
        </div>
        <!-- /.col-lg-12 -->
    </div>--%>
    <div class="content-header">
        <h1>View Invoice List</h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Return</a></li>
            <li class="active">View Invoice</li>
        </ol>
    </div>
    <div class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Details of All Invoice</h3>
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
                        <%-- <div class="box-group" id="accordion">
                               <!-- we are adding the .panel class so bootstrap.js collapse plugin detects it -->
                            <div class="panel box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="false" class="collapsed">
                                        </a>
                                    </h3>
                                </div>
                                <div id="collapseOne" class="panel-collapse collapse" aria-expanded="false" style="height: 0px;">
                                    sadasd
                                </div>
                            </div>
                            </div>--%>

                        <asp:ListView ID="lvInvoices" runat="server" ItemType="DataAccessLayer.GST_TRN_INVOICE" DataKeyNames="InvoiceID" OnPagePropertiesChanging="lvInvoices_PagePropertiesChanging">
                            <EmptyDataTemplate>
                                <table class="table">
                                    <tr>
                                        <td>No data was returned.</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr>
                                    <%-- <td style="display:none;"><%# Container.DataItemIndex + 1%>.</td>--%>
                                    <td>
                                        <asp:CheckBox ID="chkSelect" Visible='<%#!IsUpload(Eval("InvoiceID").ToString()) %>' runat="server" />
                                    </td>
                                   <%-- <td><%#DataBinder.Eval(Container.DataItem,"AspNetUser.OrganizationName")%></td>
                                    <td><%#DataBinder.Eval(Container.DataItem,"AspNetUser2.OrganizationName")%></td>
                                    <td><%#DataBinder.Eval(Container.DataItem,"AspNetUser1.OrganizationName")%></td>--%>
                                    <td>
                                        <asp:Label ID="lblSeller" runat="server" Text='<%# Eval("InvoiceNo") %>' />
                                    </td>
                                   <%-- <%#Eval("Status").ToString()=="False" ? "<span class='label label-danger'>" : "<span class='label label-success'>" %><%# Eval("Status") %></span>--%>
                                    <td><%#IsUpload(Eval("InvoiceID").ToString())==false ? "<span class='label label-danger'>" : "<span class='label label-success'>" %><%#IsUpload(Eval("InvoiceID").ToString()) %></span></td>
                                    <td><%#DateTimeAgo.GetFormatDate(Eval("Invoicedate").ToString())%></td>
                                    <td><%#Common.InvoiceTypeColor(Eval("InvoiceSpecialCondition").ToString(),Eval("InvoiceType").ToString())%></td>
                                    <td>
                                        <%--  <asp:LinkButton ID="lkb_action" runat="server" CommandArgument='<%# Eval("InvoiceID") %>' OnClick="lkb_action_Click"><i class="fa fa-eye"></i></asp:LinkButton>--%>
                                        <asp:LinkButton ID="lkb1" runat="server" data-toggle="modal" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' ToolTip="View"><i class="fa fa-eye text-orange"></i></asp:LinkButton>
                                       &nbsp; <asp:LinkButton ID="lkb" runat="server" Visible='<%#!IsEditable(Eval("InvoiceID").ToString()) %>' OnClick="lkb_Click" CommandArgument='<%# Eval("InvoiceID") %>' ToolTip="Edit"><i class="fa fa-edit text-green"></i></asp:LinkButton>
                                        <div class="modal fade" id='<%# Container.DataItemIndex +"_myModalINV"%>' role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                                            <div class="modal-dialog" style="width: 90%;">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                                            &times;
                                                        </button>
                                                        <h4 class="modal-title">
                                                            <b><i class="fa fa-globe"></i>
                                                                <asp:Label ID="lblModalTitle" runat="server" Text="Invoice Product Details"></asp:Label></b>
                                                        </h4>
                                                    </div>
                                                    <div class="modal-body">
                                                        <uc1:uc_InvoiceView runat="server" ID="uc_InvoiceViewInvoice" InvoiceDataHasSet='<%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE_DATA")%>' />

                                                    </div>
                                                    <div class="modal-footer">
                                                        <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">
                                                            Close
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                       <%--   &nbsp;  <asp:LinkButton ID="lkb1" runat="server" data-toggle="modal" data-target='<%#"#"+ Container.DataItemIndex +"_myModalINV"%>' ToolTip="View"><i class="fa fa-eye text-orange"></i></asp:LinkButton>
                                --%>    </td>
                                </tr>
                            </ItemTemplate>
                            <LayoutTemplate>
                                <table class="table table-responsive">
                                    <thead>
                                        <tr>
                                            <th style="width: 10px">#</th>
                                          
                                            <%--<th>Reciever</th>
                                            <th>Consignee</th>--%>
                                            <th>Invoice</th>  <th>Upload</th>
                                            <th>Invoice Date</th>
                                            <th>Inv. Sub-Type</th>
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
                    <div class="box-footer">
                        <div class="clearfix">
                            <div class="pagination pagination-sm no-margin pull-right">
                                <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvInvoices" PageSize="10" class="btn-group-sm pager-buttons">
                                    <Fields>
                                        <asp:NextPreviousPagerField ShowLastPageButton="False" ShowNextPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                                        <asp:NumericPagerField ButtonType="Button" RenderNonBreakingSpacesBetweenControls="false" NumericButtonCssClass="btn btn-primary" CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn" />
                                        <asp:NextPreviousPagerField ShowFirstPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary"  RenderNonBreakingSpacesBetweenControls="false" />
                                    </Fields>
                                </asp:DataPager>
                            </div>
                            <div>

                <asp:LinkButton ID="btnUpload" OnClick="btnUpload_Click" CssClass="btn btn-success pull-left" runat="server"><i class="fa fa-upload"></i><span style="margin:3px;"> Upload Invoice(s)</span></asp:LinkButton>
                <uc1:uc_sucess runat="server" ID="uc_sucess" />
            </div>
                        </div>
                           
                    </div>
         
                <asp:GridView ID="gvInvoice_Items" CssClass="table table-responsive no-padding table-striped table-bordered table-condensed" AutoGenerateColumns="false"  runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="#" Visible="false">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HSN/SAC" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:TextBox ID="txtItemCode" AutoPostBack="true" Text='<%#DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.ItemCode") %>' OnTextChanged="txtItemCode_TextChanged"  autocomplete="off" MaxLength="8" onpaste="return false;" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:TextBox ID="txtGoodService" Text='<%#DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.Description") %>' CssClass="form-control input-sm" ReadOnly="true" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty." ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQty" MaxLength="4" AutoPostBack="true" Text='<%#DataBinder.Eval(Container.DataItem,"Qty") %>' OnTextChanged="txtQty_TextChanged"  onpaste="return false;" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:TextBox ID="txtUnit" CssClass="form-control input-sm" Text='<%#DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.Unit") %>' ReadOnly="true" AutoPostBack="true" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate(per item)" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRate" AutoPostBack="true" CssClass="form-control input-sm" OnTextChanged="txtQty_TextChanged" Text='<%#DataBinder.Eval(Container.DataItem,"Rate") %>' onkeypress="return onlyNos(event,this);"  onpaste="return false;" MaxLength="10" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total(<i class='fa fa-inr' aria-hidden='true'></i>)" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="txtTotal" Text='<%#DataBinder.Eval(Container.DataItem,"TotalAmount") %>' ReadOnly="true" CssClass="form-control input-sm" runat="server"></asp:Label>
                                <%--  <asp:TextBox ID="txtTotal" ReadOnly="true" runat="server"></asp:TextBox>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Discount" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:TextBox ID="txtDiscount" AutoPostBack="true" OnTextChanged="txtQty_TextChanged" Text='<%#DataBinder.Eval(Container.DataItem,"Discount") %>' MaxLength="5"  CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="Taxable Val.(<i class='fa fa-inr' aria-hidden='true'></i>)">
                            <ItemTemplate>
                                <asp:Label ID="txtTaxableValue" Text='<%#DataBinder.Eval(Container.DataItem,"TaxableAmount") %>' CssClass="form-control input-sm" ReadOnly="true" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                    <div class="box-footer">
                <asp:LinkButton ID="lkbUpdateInvoice" OnClick="lkbUpdateInvoice_Click" Visible="false" CssClass="btn btn-primary pull-left" runat="server"><i class="fa fa-save"></i><span style="margin:3px;"> Update Invoice</span></asp:LinkButton>
                </div>
            </div>
         
                <%--   <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate></ItemTemplate>
                </asp:Repeater>
                <asp:Repeater ID="fvInvoice" runat="server">
                   
                    <ItemTemplate>--%>

                
                <%--</ItemTemplate>
                </asp:Repeater>--%>
            
            
           


             </div>

        </div>
    </div>
    <div style="display: none;">
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <label>
                        GSTIN</label>
                    <asp:Label ID="lblSellerGSTIN" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <label>
                        Seller Name</label>
                    <asp:Label ID="lblSellerName" runat="server"></asp:Label>
                    <br />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="LblFromDate" runat="server" Text="From Date : "></asp:Label>
                    <asp:TextBox ID="txtFromDt" runat="server" class="form-control" Width="100px" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="lblToDate" runat="server" Text="To Date : "></asp:Label>
                    <asp:TextBox ID="txtToDt" runat="server" CssClass="form-control" Width="100px" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Label ID="lblSelectInvoice" runat="server" Text="Select Invoice:"></asp:Label>
                    <asp:DropDownList ID="ddlInvoiceNo" runat="server" class="ddlcls" OnSelectedIndexChanged="ddlInvoiceNo_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <asp:Button ID="btnGetInvoice" runat="server" Text="Get Invoice" CssClass="btn btn-primary" OnClick="btnGetInvoice_Click" />
                </div>
            </div>
        </div>
    </div>
    <div style="display: none;" class="box box-primary">
        <div class="box-header with-border">
            <h3 class="box-title">Invoice Detail</h3>
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
            <uc1:uc_InvoiceView runat="server" ID="uc_InvoiceView" />
            <uc1:uc_invoiceR runat="server" ID="uc_invoiceR" />
        </div>
    </div>
    <div style="display: none;">
        <div class="row">
            <div class="col-md-6">
                <div class="box box-danger">
                    <div class="box-header with-border">
                        <h3 class="box-title">Details of Reciever (Billed to)</h3>
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
                        <asp:Label ID="lblRecieverGSTIN" runat="server" Text="GSTIN"></asp:Label>
                        <asp:TextBox ID="txtReceiverGSTIN" class="form-control" Width="320px" MaxLength="20" onkeypress="return isAlphaNumeric(event)" AutoPostBack="true" runat="server"></asp:TextBox>
                        <asp:Label ID="lblRecieverName" runat="server" Text="Receiver Name"></asp:Label>
                        <asp:TextBox ID="txtReceivername" class="form-control" Width="320px" ReadOnly="true" runat="server"></asp:TextBox>
                        <asp:Label ID="lblRecieverAddress" runat="server" Text="Receiver Address"></asp:Label>
                        <asp:TextBox ID="txtaddress" class="form-control" ReadOnly="true" Width="320px" runat="server"></asp:TextBox>
                        <asp:Label ID="lblReceiverState" runat="server" Text="Receiver State"></asp:Label>
                        <asp:TextBox ID="txtstate" class="form-control" ReadOnly="true" Width="320px" runat="server"></asp:TextBox>
                        <asp:Label ID="lblRecieverStateCode" runat="server" Text="Receiver State Code"></asp:Label>
                        <asp:TextBox ID="txtstatecode" class="form-control" ReadOnly="true" Width="320px" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="box box-danger">
                    <div class="box-header with-border">
                        <h3 class="box-title">Details of Consignee (Shipped to)</h3>
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
                        <asp:Label ID="lblConsigneeGSTIN" runat="server" Text="GSTIN"></asp:Label>
                        <asp:TextBox ID="txtConsigneeGSTIN" class="form-control" ReadOnly="true" Width="320px" runat="server"></asp:TextBox>
                        <asp:Label ID="lblConsigneeName" runat="server" Text="Consignee Name"></asp:Label>
                        <asp:TextBox ID="txtConsigneename" class="form-control" ReadOnly="true" Width="320px" runat="server"></asp:TextBox>
                        <asp:Label ID="lblConsigneeAddress" runat="server" Text="Consignee Address"></asp:Label>
                        <asp:TextBox ID="txtcosigneeaddress" class="form-control" ReadOnly="true" Width="320px" runat="server"></asp:TextBox>
                        <asp:Label ID="lblConsigneeState" runat="server" Text="Consignee State"></asp:Label>
                        <asp:TextBox ID="txtconsigneestate" class="form-control" ReadOnly="true" Width="320px" runat="server"></asp:TextBox>
                        <asp:Label ID="lblConsigneeStateCode" runat="server" Text="Consignee StateCode"></asp:Label>
                        <asp:TextBox ID="txtconsigneestatecode" class="form-control" ReadOnly="true" Width="320px" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="box box-danger">
                    <div class="box-header with-border">
                        <h3 class="box-title">Details of Goods</h3>
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
                        <table style="width: 100%" border="1" id="tblPreview" runat="server">
                            <tr>
                                <td rowspan="2" style="width: 3%; padding-left: 2px;">
                                    <asp:Label ID="Label1" runat="server" Style="font-weight: bold" Text="Sr. No"></asp:Label>
                                </td>
                                <td rowspan="2" id="tdDesc" runat="server" style="width: 19%; padding-left: 2px;">
                                    <asp:Label ID="Label2" runat="server" Style="font-weight: bold" Text="Description of Goods"></asp:Label>
                                </td>
                                <td rowspan="2" style="width: 7%; padding-left: 2px;">
                                    <asp:Label ID="Label3" runat="server" Style="font-weight: bold" Text="HSN"></asp:Label>
                                </td>
                                <td rowspan="2" style="width: 4%; padding-left: 2px;">
                                    <asp:Label ID="Label4" runat="server" Style="font-weight: bold" Text="Qty."></asp:Label>
                                </td>
                                <td rowspan="2" style="width: 4%; padding-left: 2px;">
                                    <asp:Label ID="Label5" runat="server" Style="font-weight: bold" Text="Unit"></asp:Label>
                                </td>
                                <td rowspan="2" style="width: 6%; padding-left: 2px;">
                                    <asp:Label ID="Label6" runat="server" Style="font-weight: bold" Text="Rate"></asp:Label>
                                </td>
                                <td rowspan="2" style="width: 8%; padding-left: 2px; color: gray;">
                                    <asp:Label ID="Label7" runat="server" Style="font-weight: bold" Text="Total(<i class='fa fa-inr' aria-hidden='true'></i>)"></asp:Label>
                                </td>
                                <td rowspan="2" style="width: 6%; padding-left: 2px;">
                                    <asp:Label ID="Label8" runat="server" Style="font-weight: bold" Text="Discount(%)"></asp:Label>
                                </td>
                                <td rowspan="2" style="width: 9%; padding-left: 2px; color: gray;">
                                    <asp:Label ID="Label9" runat="server" Style="font-weight: bold" Text="Taxable value(<i class='fa fa-inr' aria-hidden='true'></i>)"></asp:Label>
                                </td>
                                <td colspan="2" style="width: 12%; padding-left: 2px; text-align: center">
                                    <asp:Label ID="Label10" runat="server" Style="font-weight: bold" Text="CGST(<i class='fa fa-inr' aria-hidden='true'></i>)"></asp:Label>
                                </td>
                                <td colspan="2" style="width: 12%; padding-left: 2px; text-align: center">
                                    <asp:Label ID="Label11" runat="server" Style="font-weight: bold" Text="SGST(<i class='fa fa-inr' aria-hidden='true'></i>)"></asp:Label>
                                </td>
                                <td colspan="2" style="width: 12%; padding-left: 2px; text-align: center">
                                    <asp:Label ID="Label12" runat="server" Style="font-weight: bold" Text="IGST(<i class='fa fa-inr' aria-hidden='true'></i>)"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 4%; padding-left: 2px;">
                                    <asp:Label ID="Label13" runat="server" Style="font-weight: bold" Text="Rate"></asp:Label>
                                </td>
                                <td style="width: 8%; padding-left: 2px;">
                                    <asp:Label ID="Label14" runat="server" Style="font-weight: bold" Text="Amt."></asp:Label>
                                </td>
                                <td style="width: 4%; padding-left: 2px;">
                                    <asp:Label ID="Label15" runat="server" Style="font-weight: bold" Text="Rate"></asp:Label>
                                </td>
                                <td style="width: 8%; padding-left: 2px;">
                                    <asp:Label ID="Label16" runat="server" Style="font-weight: bold" Text="Amt."></asp:Label>
                                </td>
                                <td style="width: 4%; padding-left: 2px;">
                                    <asp:Label ID="Label17" runat="server" Style="font-weight: bold" Text="Rate"></asp:Label>
                                </td>
                                <td style="width: 8%; padding-left: 2px;">
                                    <asp:Label ID="Label18" runat="server" Style="font-weight: bold" Text="Amt."></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:Label ID="lblFreight" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblInsurance" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblPackingAndForwadingCharges" runat="server"></asp:Label>
                        <hr />
                        <asp:Label ID="AmtInFigure" Text="Total Taxable Value(In Figure) : " runat="server"></asp:Label>
                        <asp:Label ID="AmtInFigureVal" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="AmtInWords" Text="Total Taxable Value(In Words) : " runat="server"></asp:Label>
                        <asp:Label ID="AmtInWordsVal" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="box-footer">
            <div align="right" style="margin-right: 15px;">
                <asp:Button ID="btnBack" runat="server" Text="Back" Width="100px" OnClick="btnBack_Click" Visible="false" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>



   
</asp:Content>

