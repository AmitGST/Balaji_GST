<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeBehind="ITC.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.User.Trans.ITC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-header">
        <h1>ITC
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">LEDGERS</a></li>
            <li class="#">ITC</li>
        </ol>

    </div>
  
    <div class="content">
        <div class="box box-solid margin-bottom" style="height:130px; padding:15px; background-color:#3c8dbc;">
          <div class="row">
          <div class="col-md-6">
              <div class="info-box" style="float:left;">
            <div class="box-content">
              <h5 style="color:black !important; padding-top:1px; font-weight:bold; text-align:center;">TOTAL CREDIT</h5>
              <div class="row">
            <!-- /.info-box-content -->
            <div class="col-md-2">
              <span style="color:black !important; padding-top:2px; text-align:center;" class="info-box-text">IGST&nbsp;(<i class="fa fa-inr" style="font-size:10px;"></i>)</span>
              <span  style="color:black !important; text-align:center; font-size:12px;" class="info-box-number"><asp:Literal ID="litTotalIGST" runat="server"></asp:Literal></span>
                  </div>
                  <div class="col-md-2">
              <span style="color:black !important; padding-top:2px; text-align:center;" class="info-box-text">CGST&nbsp;(<i class="fa fa-inr" style="font-size:10px;"></i>)</span>
              <span  style="color:black !important; text-align:center; font-size:12px;" class="info-box-number"><asp:Literal ID="litTotalCGST" runat="server"></asp:Literal></span>
                  </div>
                  <div class="col-md-2">
              <span style="color:black !important; padding-top:2px; text-align:center;" class="info-box-text"> SGST&nbsp;(<i class="fa fa-inr" style="font-size:10px;"></i>)</span>
              <span  style="color:black !important; text-align:center; font-size:12px;" class="info-box-number"><asp:Literal ID="litTotalSGST" runat="server"></asp:Literal></span>
                  </div>
                  <div class="col-md-2">
              <span style="color:black !important; padding-top:2px; text-align:center;" class="info-box-text">Cess&nbsp;(<i class="fa fa-inr" style="font-size:10px;"></i>)</span>
              <span  style="color:black !important; text-align:center; font-size:12px;" class="info-box-number"><asp:Literal ID="litTotalCess" runat="server"></asp:Literal></span>
                  </div>
                  <div class="col-md-4">
              <span style="color:black !important; padding-top:2px; text-align:center;" class="info-box-text">ITC&nbsp;(<i class="fa fa-inr" style="font-size:10px;"></i>)</span>
              <span  style="color:black !important; text-align:center; font-size:12px;" class="info-box-number"><asp:Literal ID="litTotalItc" runat="server"></asp:Literal></span>
              </div>
                  </div>
                
                      </div>
              </div>
          
              </div>
         
                <div class="col-md-6">
              <div class="info-box" style="float:left;">
            <div class="box-content">
              <h5 style="color:black !important; font-weight:bold; padding-top:1px; text-align:center;">TOTAL DEBIT</h5>
              <div class="row">
                  
            <!-- /.info-box-content -->
          <div class="col-md-2">
              <span style="color:black !important; padding-top:5px; text-align:center;" class="info-box-text">IGST&nbsp;(<i class="fa fa-inr" style="font-size:10px;"></i>)</span>
              <span  style="color:black !important; text-align:center; font-size:12px;" class="info-box-number"><asp:Literal ID="litDRIGST" runat="server"></asp:Literal></span>
                  </div>
                  <div class="col-md-2">
              <span style="color:black !important; padding-top:5px; text-align:center;" class="info-box-text">CGST&nbsp;(<i class="fa fa-inr" style="font-size:10px;"></i>)</span>
              <span  style="color:black !important; text-align:center; font-size:12px;" class="info-box-number"><asp:Literal ID="litDRCGST" runat="server"></asp:Literal></span>
                  </div>
                  <div class="col-md-2">
              <span style="color:black !important; padding-top:5px; text-align:center;" class="info-box-text">SGST&nbsp;(<i class="fa fa-inr" style="font-size:10px;"></i>)</span>
              <span  style="color:black !important; text-align:center; font-size:12px;" class="info-box-number"><asp:Literal ID="litDRSGST" runat="server"></asp:Literal></span>
                  </div>
                   <div class="col-md-2">
              <span style="color:black !important; padding-top:5px; text-align:center;" class="info-box-text">CESS&nbsp;(<i class="fa fa-inr" style="font-size:10px;"></i>)</span>
              <span  style="color:black !important; text-align:center; font-size:12px;" class="info-box-number"><asp:Literal ID="litDrCESS" runat="server"></asp:Literal></span>
                  </div>
                  <div class="col-md-4">
              <span style="color:black !important; padding-top:5px; text-align:center;" class="info-box-text">ITC&nbsp;(<i class="fa fa-inr" style="font-size:10px;"></i>)</span>
              <span  style="color:black !important; text-align:center; font-size:12px;" class="info-box-number"><asp:Literal ID="litDrTotalITC" runat="server"></asp:Literal></span>
            </div>
                  </div>
                      </div>
              </div>
          
              </div>  
             
              </div>
    </div>
        <div class="box box-primary">
            <div class="box-body">
                <asp:ListView ID="lvITC" runat="server">
                    <EmptyDataTemplate>
                        <table class="table table-responsive ">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.DataItemIndex + 1%></td>
                            <td class="pull-left"><%#DataBinder.Eval(Container.DataItem,"GST_TRN_INVOICE.InvoiceNo") %></td>
                            <td><%#DateTimeAgo.GetFormatDate(Eval("ITCDate")) %> </td>
                            <td style="text-align:left"><%#(GST.Utility.EnumConstants.ITCVoucherType)Convert.ToInt32(Eval("ITCVoucherType").ToString()) %> </td>
                             <td><%#BusinessLogic.Repositories.cls_ITC.ITCColor(Eval("ITCMovement")) %></td>
                            <td style="text-align: right;"><%# Eval("Amount")%></td>
                            <td style="text-align: right;"><%# Eval("IGST") %></td>
                            <td style="text-align: right;"><%# Eval("CGST") %></td>
                            <td style="text-align: right;"><%# Eval("SGST") %></td>
                            <td style="text-align: right;"><%# Eval("Cess") %></td>
                        </tr
                    </ItemTemplate>

                    <LayoutTemplate>
                        <table class="table table-responsive dataTable" id="lvItems">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th style="text-align: left;">Invoice No.</th>
                                    <th>Date</th>
                                    <th style="text-align: left;">Voucher</th>
                                   <th>ITC Type</th>
                                    <th style="text-align: right;">Amount (<i style="font-size: 10px;" class="fa fa-rupee"></i>)</th>
                                    <th style="text-align: right;">IGST (<i style="font-size: 10px;" class="fa fa-rupee"></i>)</th>
                                    <th style="text-align: right;">CGST (<i style="font-size: 10px;" class="fa fa-rupee"></i>)</th>
                                    <th style="text-align: right;">SGST / UTGST (<i style="font-size: 10px;" class="fa fa-rupee"></i>)</th>
                                     <th style="text-align: right;">Cess (<i style="font-size: 10px;" class="fa fa-rupee"></i>)</th>
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
    </div>
    <script>
        $(document).ready(function () {
            $('#lvItems').DataTable(); // js goes in here.
        });
    </script>
</asp:Content>
