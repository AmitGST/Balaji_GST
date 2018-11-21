<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master"  AutoEventWireup="true" CodeBehind="GSTR3BPreview.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.User.ureturn.GSTR3BPreview" %>
<%@ MasterType VirtualPath="~/User/User.master" %>
<%@ Register Src="~/UC/uc_GSTR_Taxpayer.ascx" TagPrefix="uc1" TagName="uc_GSTR_Taxpayer" %>
<%@ Register Src="~/UC/uc_GSTNUsers.ascx" TagPrefix="uc1" TagName="uc_GSTNUsers" %>


<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content-header">
        <h1>GSTR3B</h1>
        <%--   <small>Outward supplies made by the taxpayer</small> </h1>--%>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Return</a></li>
            <li class="active">GSTR3B</li>
        </ol>
    </div>

    <!-- Main content -->
    <div class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <div class="row">
                <uc1:uc_GSTNUsers runat="server" ID="uc_GSTNUsers" />
                    </div>
            </div>
            <div class="box-body">
                <h4><strong>Taxpayer Details</strong></h4>
                <uc1:uc_GSTR_Taxpayer runat="server" ID="uc_GSTR_Taxpayer" />
            </div>
            <div class="box-footer">
               <div class="col-sm-2">
                    <asp:LinkButton ID="lkbFileGSTR3B" OnClick="lkbFileGSTR3B_Click" CssClass="btn btn-success" runat="server"><i class="fa fa-cloud-upload"></i> File GSTR-3B</asp:LinkButton>
             </div>
              <div class="col-sm-2">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:LinkButton ID="lkbDownloadJSON" OnClick="lkbDownload3B_Click" CssClass="btn btn-primary pull-left" runat="server"><i class="fa fa-download">&nbsp;</i>DownloadJSON 3B</asp:LinkButton>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lkbDownloadJSON" />
                        </Triggers>
                    </asp:UpdatePanel>
               </div>
                <div class="col-md-3">
                    <asp:LinkButton ID="lkbDownload3B" CssClass="btn btn-primary" Visible="false" OnClick="lkbDownload3B_Click" runat="server"><i class="fa fa-download">&nbsp;</i>Download 3B</asp:LinkButton>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lkbDownload3B" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
             </div>   
            </div>
     

        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">3.1  Details of Outward Supplies and Inward Supplies liable to reverse charge.</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove">
                        <i class="fa fa-remove"></i>
                    </button>
                </div>
            </div>

            <div class="box-body table-responsive">
                <%-- <h5 style="font-weight: bold;">3A.&nbsp; Outward taxable  supplies  (other than zero rated, nil rated and exempted)</h5>--%>
                <asp:ListView ID="lv_GSTR3B_3_1_A" runat="server" GroupItemCount="3">
                    <%--  <LayoutTemplate>
                        <table>
                            <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                        </table>
                    </LayoutTemplate>
                    <GroupTemplate>
                        <tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                        </tr>
                    </GroupTemplate>
                    <ItemTemplate>
                        <td>
                            <table cellpadding="2" cellspacing="0" border="1" style="width: 200px; height: 100px; border: dashed 2px #04AFEF; background-color: #B0E2F5">
                                <tr>
                                    <td>
                                        <b><u><span class="name">
                                            <%# Eval("NatureOfSupplies") %></span></u></b>
                                    </td>
                                </tr>
                                <tr>
                                    <layouttemplate>
                        <table>
                            <asp:PlaceHolder runat="server" ID="groupPlaceHolder1">
                           </ asp:PlaceHolder>
                        </table>
                    </layouttemplate>
                                    <grouptemplate>
                        <tr>
                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                        </tr>
                    </grouptemplate>
                                    <itemtemplate>
                        <td>
                            <table cellpadding="2" cellspacing="0" border="1" style="width: 200px; height: 100px; border: dashed 2px #04AFEF; background-color: #B0E2F5">
                                <tr>
                                    <td>
                                        <b><u><span class="name">
                                            <%# Eval("NatureOfSupplies") %></span>u></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>GSTIN: </b>
                                        <span class="GSTIN">
                                            <%# Eval("GSTNNO") %></span>
                                        <br />
                                        <b>Total Taxable value: </b>
                                        <span class="Taxable"><%# Eval("TAXABLEAMOUNT") %></span>
                                        <br />
                                        <b>IGST:</b>
                                        <span class="Taxable"><%# Eval("IGSTAMT")%></span>
                                        <br />
                                        <b>CGST: </b>
                                        <span class="CGST"><%# Eval("CGSTAMT")%></span>
                                        <br />
                                        <b>UTGST/SGST: </b>
                                        <span class="SGST"><%# Eval("SGSTAMT")%></span>
                                        <br />
                                        <b>CESS: </b>
                                        <span class="CESS"><%# Eval("CESSAMT")%></span>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </itemtemplate>
                                </tr>
                            </table>
                        </td>
                    </ItemTemplate>--%>

                    <EmptyDataTemplate>
                        <table class="table">
                            <tr>

                                <td id="itemPlaceholder" runat="server"></td>

                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <GroupTemplate>
                        <tr id="itemPlaceHolderContainer" runat="server">
                            <td id="itemPlaceHolder" runat="server"></td>
                        </tr>
                    </GroupTemplate>
                    <ItemTemplate>
                        <tr>

                            <td><%# Container.DataItemIndex + 1%></td>
                            <td><%# Eval("NatureOfSupplies") %></td>
                            <td>
                                <%# Eval("GSTNNO") %>
                            </td>
                            <td>
                                <asp:Label ID="lblTaxamt" runat="server" Text='<%# Eval("TAXABLEAMOUNT") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblIgstamt" runat="server" Text='<%# Eval("IGSTAMT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCgstamt" runat="server" Text='<%# Eval("CGSTAMT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblSgstamt" runat="server" Text='<%# Eval("SGSTAMT") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCessamt" runat="server" Text='<%# Eval("CESSAMT") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table id="GroupPlaceholderContainer" class="table table-bordered table-responsive">
                            <tr>

                                <th style="vertical-align: bottom;">#</th>
                                <th style="vertical-align: bottom;">Description</th>
                                <th style="vertical-align: bottom;">GSTIN No</th>
                                <th style="text-align: center;">Total Taxable value</th>
                                <th style="text-align: center;">IGST</th>
                                <th style="text-align: center;">CGST</th>
                                <th style="text-align: center;">UTGST/SGST</th>
                                <th style="vertical-align: bottom;">CESS</th>
                                <tbody>
                                    <tr id="GroupPlaceholder" runat="server">
                                        <th style="vertical-align: bottom;">Description</th>
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
            </div>
        </div>

        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">3B.&nbsp;Of the supplies shown in 3.1(a) above, details of inter state supplies made to unregistered persons, composition taxable persons and UIN holders.</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove">
                        <i class="fa fa-remove"></i>
                    </button>
                </div>
            </div>

            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR3B_3_2" runat="server">
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
                            <td>
                                <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("Description") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblState" runat="server" Text='<%# Eval("STATENAME") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblTaxAmt" runat="server" Text='<%# Eval("TAXABLEAMOUNT") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblIgstAmt" runat="server" Text='<%# Eval("IGSTAMT") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-responsive">
                            <tr>
                                <th colspan="12">Supplies made to Unregistered Persons</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">#</th>
                                <th style="vertical-align: bottom;">Description</th>
                                <th style="vertical-align: bottom;">Place of Supply (State/UT)</th>
                                <th style="text-align: center;">Total Taxable value</th>
                                <th style="text-align: center;">Amount of Integrated Tax</th>
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

        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">4.Eligible ITC</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove">
                        <i class="fa fa-remove"></i>
                    </button>
                </div>
            </div>

            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR3B_4A" runat="server">
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
                            <td>
                                <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("Details") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblIgstTax" runat="server" Text='<%# Eval("INTEGRATED_TAX") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblCgstTax" runat="server" Text='<%# Eval("CENTRAL_TAX") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblSgstTax" runat="server" Text='<%# Eval("STATE_UT_TAX") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblCess" runat="server" Text='<%# Eval("CESS") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-responsive">
                            <tr>
                                <th colspan="12">A.ITC Availiable (whether in full or part)</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">#</th>
                                <th style="vertical-align: bottom;">Description</th>
                                <th style="text-align: center;">IGST</th>
                                <th style="text-align: center;">CGST</th>
                                <th style="text-align: center;">SGST/UTGST</th>
                                <th style="text-align: center;">CESS</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR3B_4B" runat="server">
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
                            <td>
                                <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("Details") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblIgstTax" runat="server" Text='<%# Eval("INTEGRATED_TAX") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblCgstTax" runat="server" Text='<%# Eval("CENTRAL_TAX") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblSgstTax" runat="server" Text='<%# Eval("STATE_UT_TAX") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblCess" runat="server" Text='<%# Eval("CESS") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-responsive">
                            <tr>
                                <th colspan="12">B. ITC Reversed</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">#</th>
                                <th style="vertical-align: bottom;">Description</th>
                                <th style="text-align: center;">IGST</th>
                                <th style="text-align: center;">CGST</th>
                                <th style="text-align: center;">SGST/UTGST</th>
                                <th style="text-align: center;">CESS</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR3B_4C" runat="server">
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
                            <td>
                                <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("Details") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblIgstTax" runat="server" Text='<%# Eval("INTEGRATED_TAX") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblCgstTax" runat="server" Text='<%# Eval("CENTRAL_TAX") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblSgstTax" runat="server" Text='<%# Eval("STATE_UT_TAX") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblCess" runat="server" Text='<%# Eval("CESS") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-responsive">
                            <tr>
                                <th colspan="12">C. Net ITC Avaliable(A)-(B)</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">#</th>
                                <th style="text-align: center;">IGST</th>
                                <th style="text-align: center;">CGST</th>
                                <th style="text-align: center;">SGST/UTGST</th>
                                <th style="text-align: center;">CESS</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ListView ID="lv_GSTR3B_4D" runat="server">
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
                            <td>
                                <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("Details") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblIgstTax" runat="server" Text='<%# Eval("IntregratedTax") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblCgstTax" runat="server" Text='<%# Eval("CentralTax") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblSgstTax" runat="server" Text='<%# Eval("StateTax") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblCess" runat="server" Text='<%# Eval("Cess") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-responsive">
                            <tr>
                                <th colspan="12">C.Ineligible ITC</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom;">#</th>
                                <th style="text-align: center;">Description</th>
                                <th style="text-align: center;">IGST</th>
                                <th style="text-align: center;">CGST</th>
                                <th style="text-align: center;">SGST/UTGST</th>
                                <th style="text-align: center;">CESS</th>
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

        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">5. Values of exempt, Nil rated and Non GST Inward supplies</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove">
                        <i class="fa fa-remove"></i>
                    </button>
                </div>
            </div>
            <div class="box-body table-responsive">
                <asp:ListView ID="lv_GSTR3B_5" runat="server">
                    <EmptyDataTemplate>
                        <table class="table table-striped">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.DataItemIndex + 1%></td>
                            <td>
                                <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("NatureofSupplies") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblInter" runat="server" Text='<%# Eval("InterState_Supplies") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblIntra" runat="server" Text='<%# Eval("Intra_StateSupplies") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-striped">
                            <tr>
                                <th>#</th>
                                <th>Nature of supplies</th>
                                <th>Inter-State Supplies</th>
                                <th>Intra-State Supplies</th>
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

        <div class="box box-danger">
            <div class="box-header with-border">
                <h3 class="box-title">6.1&nbsp; Payment of Tax </h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                    <button type="button" class="btn btn-box-tool" data-widget="remove">
                        <i class="fa fa-remove"></i>
                    </button>
                </div>
            </div>

            <div class="box-body table-responsive">
                <asp:ListView ID="lvGSTR1_6A" runat="server">
                    <EmptyDataTemplate>
                        <table class="table table-striped">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.DataItemIndex + 1%></td>
                            <td>
                                <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("DESCRIPTION") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblTaxPay" runat="server" Text='<%# Eval("TAX_PAYABLE") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblIgst_itc" runat="server" Text='<%# Eval("PAIDITC_IGST") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblCgst_itc" runat="server" Text='<%# Eval("PAIDITC_CGST") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblSgst_itc" runat="server" Text='<%# Eval("PAIDITC_SGST") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblUtgst_itc" runat="server" Text='<%# Eval("PAIDITC_UTGST") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lbless" runat="server" Text='<%# Eval("CESS") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblTcs_Tds" runat="server" Text='<%# Eval("TAXPAID_TCS_TDS") %>' />
                            </td>
                            <td style="text-align: center;">
                                <asp:Label ID="lblCess_PaidCash" runat="server" Text='<%# Eval("CESSPAID_CASH") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblInterest" runat="server" Text='<%# Eval("INTEREST") %>' />
                            </td>
                            <%--<td style="text-align: center;">
                                <asp:Label ID="lblIGSTAmt" runat="server" Text='<%# Eval("IGSTAMT").ToString()=="0.00"? " - " :Eval("IGSTAMT") %>' />
                            </td>--%>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th rowspan="2">#</th>
                                <th rowspan="2">Description</th>
                                <th colspan="5">Paid through ITC</th>
                                <th rowspan="2">Tax Paid TCS/TDS</th>
                                <th rowspan="2">Interest</th>
                                <th rowspan="2">Late Fees</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom">Tax Payable</th>
                                <th style="vertical-align: bottom">IGST</th>
                                <th style="vertical-align: bottom;">CGST</th>
                                <th style="vertical-align: bottom">SGST</th>
                                <th style="vertical-align: bottom">UTGST</th>
                                <th style="vertical-align: bottom">CESS</th>
                            </tr>
                            <tr>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server"></tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <div>
                    <h5 style="font-weight: bold;">6.2.&nbsp; TDS/TCS Credit</h5>
                </div>
                <asp:ListView ID="lvGSTR1_6B" runat="server">
                    <EmptyDataTemplate>
                        <table class="table table-striped">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.DataItemIndex + 1%></td>
                            <td>
                                <asp:Label ID="lblInvoiceID" runat="server" Text='<%# Eval("Details") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Eval("IGST") %>' />
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("CGST") %>' />
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("SGST") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered table-condensed">
                            <tr>
                                <th colspan="12">6.2.A&nbsp; TDS</th>
                            </tr>
                            <tr>
                                <th style="vertical-align: bottom">#</th>
                                <th style="vertical-align: bottom">Details</th>
                                <th style="text-align: center;">IGST</th>
                                <th style="text-align: center;">CGST</th>
                                <th style="text-align: center;">SGST/UTGST</th>
                                <tbody>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </tbody>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
