<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeBehind="UserMgmt.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Masters.UserMgmt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">

        <div id="divMain" runat="server">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">ASSISTANCE SUVIDHA PROVIDER</h3>
                </div>
                <div class="box-body">
                    <%-- <asp:ListView ID="lvUserManagement" runat="server">
    <EmptyDataTemplate>
        <table class="table table-responsive ">
            <tr>
                <td>No data was returned.</td>
            </tr>
        </table>
    </EmptyDataTemplate>
    <ItemTemplate>--%>

                    <%-- </ItemTemplate>

    <LayoutTemplate>--%>

                    <table class="table table-responsive no-padding table-bordered">

                        <tr>
                            <th>#</th>
                            <th>Asp Client Name</th>
                            <th>Asp Client ID</th>
                            <th>Status</th>
                            <th>Activated Date</th>
                            <th>Expiry Date</th>
                            <th>History</th>
                        </tr>

                        <tr>
                            <td>1.</td>
                            <td>Work Place Synery Pvt. Ltd.</td>
                            <td>388d844e-2496-4508-b5ff-3e130311aca7</td>
                            <td>True</td>
                            <td>June 1, 2017</td>
                            <td>Dec 31, 2017</td>
                            <td>
                                <asp:LinkButton ID="lkbHistory" runat="server" OnClick="lkbHistory_Click"><i class="fa fa-eye text-orange"></i></asp:LinkButton></td>

                        </tr>
                        <tr>
                            <td>2.</td>
                            <td>Sys Tools Pvt. Ltd.</td>
                            <td>38a55526-83fa-407d-a38d-0d43b619c0aa</td>
                            <td>False</td>
                            <td>June 1, 2017</td>
                            <td>Dec 31, 2017</td>
                            <td><i class="fa fa-eye text-orange"></i></td>
                        </tr>


                    </table>
                    <%-- </LayoutTemplate>
</asp:ListView>--%>
                </div>
            </div>
        </div>
        <div id="divSecondary" runat="server" visible="false">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">ASP Logger</h3>
                    <asp:LinkButton ID="lkbBack" CssClass="btn btn-default pull-right" OnClick="lkbBack_Click" runat="server">&nbsp;<i class="fa fa-backward"></i> Back</asp:LinkButton>
                </div>


                <%-- <div class="box">
            <div class="box-header with-border">
              <h3 class="box-title">Monthly Recap Report</h3>

              <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
                <div class="btn-group">
                  <button type="button" class="btn btn-box-tool dropdown-toggle" data-toggle="dropdown">
                    <i class="fa fa-wrench"></i></button>
                  <ul class="dropdown-menu" role="menu">
                    <li><a href="#">Action</a></li>
                    <li><a href="#">Another action</a></li>
                    <li><a href="#">Something else here</a></li>
                    <li class="divider"></li>
                    <li><a href="#">Separated link</a></li>
                  </ul>
                </div>
                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
              </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <div class="row">
                <div class="col-md-8">
                  <p class="text-center">
                    <strong>Sales: 1 Jan, 2014 - 30 Jul, 2014</strong>
                  </p>

                  <div class="chart">
                    <!-- Sales Chart Canvas -->
                    <canvas id="salesChart" style="height: 180px;"></canvas>
                  </div>
                  <!-- /.chart-responsive -->
                </div>
                <!-- /.col -->
                <div class="col-md-4">
                  <p class="text-center">
                    <strong>Goal Completion</strong>
                  </p>

                  <div class="progress-group">
                    <span class="progress-text">Add Products to Cart</span>
                    <span class="progress-number"><b>160</b>/200</span>

                    <div class="progress sm">
                      <div class="progress-bar progress-bar-aqua" style="width: 80%"></div>
                    </div>
                  </div>
                  <!-- /.progress-group -->
                  <div class="progress-group">
                    <span class="progress-text">Complete Purchase</span>
                    <span class="progress-number"><b>310</b>/400</span>

                    <div class="progress sm">
                      <div class="progress-bar progress-bar-red" style="width: 80%"></div>
                    </div>
                  </div>
                  <!-- /.progress-group -->
                  <div class="progress-group">
                    <span class="progress-text">Visit Premium Page</span>
                    <span class="progress-number"><b>480</b>/800</span>

                    <div class="progress sm">
                      <div class="progress-bar progress-bar-green" style="width: 80%"></div>
                    </div>
                  </div>
                  <!-- /.progress-group -->
                  <div class="progress-group">
                    <span class="progress-text">Send Inquiries</span>
                    <span class="progress-number"><b>250</b>/500</span>

                    <div class="progress sm">
                      <div class="progress-bar progress-bar-yellow" style="width: 80%"></div>
                    </div>
                  </div>
                  <!-- /.progress-group -->
                </div>
                <!-- /.col -->
              </div>
              <!-- /.row -->
            </div>




            
        </div>--%>
                <div class="box-body">

                    <asp:Image ID="Image1" ImageUrl="~/Images/image.png" Width="1070px" runat="server" />

                    <table class="table table-responsive no-padding table-bordered" id="tbLogger">

                        <tr>
                            <th>#</th>
                            <th>Function</th>
                            <th>Total Invoice</th>
                            <th>Date</th>
                            <th>Error</th>
                        </tr>

                        <tr>
                            <td>1.</td>
                            <td>Save GSTR1 data</td>
                            <td>18</td>
                            <td>22/08/2017</td>
                            <td>RET11400</td>
                        </tr>
                        <tr>
                            <td>2.</td>
                            <td>Get B2B Invoices</td>
                            <td>18</td>
                            <td>22/08/2017</td>
                            <td>RET11401</td>
                        </tr>
                        <tr>
                            <td>3.</td>
                            <td>Get B2BA Invoices</td>
                            <td>18</td>
                            <td>22/08/2017</td>
                            <td>RET11402</td>
                        </tr>
                        <tr>
                            <td>4.</td>
                            <td>Get B2CL Invoices</td>
                            <td>18</td>
                            <td>22/08/2017</td>
                            <td>RET11404</td>
                        </tr>
                        <tr>
                            <td>5.</td>
                            <td>Get B2CLA Invoices</td>
                            <td>18</td>
                            <td>22/08/2017</td>
                            <td>RET12501</td>
                        </tr>
                        <tr>
                            <td>6.</td>
                            <td>Get B2CS Invoices</td>
                            <td>18</td>
                            <td>22/08/2017</td>
                            <td>RET12502</td>
                        </tr>
                        <tr>
                            <td>7.</td>
                            <td>Get B2CSA Invoices</td>
                            <td>18</td>
                            <td>22/08/2017</td>
                            <td>RET12503</td>
                        </tr>
                        <tr>
                            <td>8.</td>
                            <td>Get CDNR Invoices</td>
                            <td>18</td>
                            <td>22/08/2017</td>
                            <td>RET12504</td>
                        </tr>
                        <tr>
                            <td>9.</td>
                            <td>Get CDNRA Invoices</td>
                            <td>18</td>
                            <td>22/08/2017</td>
                            <td>RET12505</td>
                        </tr>
                        <tr>
                            <td>10.</td>
                            <td>Get Nil Rated Supplies</td>
                            <td>18</td>
                            <td>22/08/2017</td>
                            <td>RET13501</td>
                        </tr>
                        <tr>
                            <td>11.</td>
                            <td>Get EXP</td>
                            <td>18</td>
                            <td>22/08/2017</td>
                            <td>RET13502</td>
                        </tr>
                        <tr>
                            <td>12.</td>
                            <td>Get EXPA</td>
                            <td>18</td>
                            <td>22/08/2017</td>
                            <td>RET13503</td>
                        </tr>
                        <tr>
                            <td>13.</td>
                            <td>Get AT</td>
                            <td>18</td>
                            <td>22/08/2017</td>
                            <td>RET13504</td>
                        </tr>
                        <tr>
                            <td>14.</td>
                            <td>Get ATA</td>
                            <td>18</td>
                            <td>22/08/2017</td>
                            <td>RET13505</td>
                        </tr>
                        <tr>
                            <td>15.</td>
                            <td>Get TXP</td>
                            <td>18</td>
                            <td>22/08/2017</td>
                            <td>RET13506	</td>
                        </tr>
                        <tr>
                            <td>16.</td>
                            <td>Get HSN Summary details</td>
                            <td>18</td>
                            <td>22/08/2017</td>
                            <td>RET13507</td>
                        </tr>
                        <tr>
                            <td>17.</td>
                            <td>Get GSTR1 Summary</td>
                            <td>18</td>
                            <td>22/08/2017</td>
                            <td>RET13506</td>
                        </tr>
                        <tr>
                            <td>18.</td>
                            <td>File Gstr1</td>
                            <td>18</td>
                            <td>22/08/2017</td>
                            <td>RET13508</td>
                        </tr>

                    </table>
                    <%-- </LayoutTemplate>
</asp:ListView>--%>

                    <%--<div class="box-footer clearfix">
                
                <div class="pagination pagination-sm no-margin pull-right">
                    <asp:DataPager ID="dpInvoice" runat="server" PagedControlID="tbLogger" PageSize="10" class="btn-group-sm pager-buttons ">
                        <Fields>
                            <asp:NextPreviousPagerField ShowLastPageButton="False" ShowNextPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                            <asp:NumericPagerField ButtonType="Button" RenderNonBreakingSpacesBetweenControls="false" NumericButtonCssClass="btn btn-primary" CurrentPageLabelCssClass="btn disabled" NextPreviousButtonCssClass="btn" />
                            <asp:NextPreviousPagerField ShowFirstPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="btn btn-primary" RenderNonBreakingSpacesBetweenControls="false" />
                        </Fields>
                    </asp:DataPager>
                </div>
            </div>--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
