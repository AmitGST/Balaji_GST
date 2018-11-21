<%@ Page Title="" Language="C#" MasterPageFile="~/loggedIn_applicationMasterPage.Master" AutoEventWireup="true" CodeBehind="GSTR9.aspx.cs" Inherits="UserInterface.GSTR9" %>
<asp:Content ID="Content1" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h2 class="page-header">GSTR-9 ANNUAL RETURN </h2>
        </div>
        <!-- /.col-lg-12 -->
    </div>

    <div class="row">
        <div class="col-lg-12">

            

        </div>
    </div>

    <div class="row">
        <div class="col-lg-6" style="width: 100%">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Taxpayer Details
                </div>

                <div class="panel-body">
                    <asp:Label ID="lblGSTIN" runat="server"></asp:Label><br />
                    <asp:Label ID="lbtaxableName" runat="server"></asp:Label><br />
                    <%--   <asp:Label ID="lblGrossTurnOver" runat="server"></asp:Label><br />--%>
                   Whether Liable Statuory Audit <asp:RadioButton ID="Yes" runat="server" GroupName="option" />
                    <asp:RadioButton ID="No" runat="server" GroupName="option" />
                    <br />

                    <asp:Label ID="lblDateStatutory" runat="server"></asp:Label><br />
                    <asp:Label ID="lblAuditors" runat="server" Style="display: none;"></asp:Label><br />
                </div>


            </div>
        </div>
    </div>
    <div class="panel-group" id="accordionMainDetails">
    </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a data-toggle="collapse" data-parent="#accordionMainDetails" href="#collapse5details">5. Details of expenditure:</a>
                </h4>
            </div>
            <div id="collapse5details" class="panel-collapse collapse">
                <div class="panel-body">
                    <div class="panel-group" id="accordionchildOutWard">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildDetails" href="#collapse5a_details">5.a	Total value of purchases on which ITC availed (inter-State)</a>
                                </h4>
                                <h4>Goods:</h4>
                            </div>
                            <div id="collapse5a_details" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GV5_Details" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                            <asp:BoundField DataField=""
                                                HeaderText="Description">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IGSTRate"
                                                HeaderText="HSN Code">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="UQC">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Quantity"
                                                HeaderText="IGST">
                                                <ItemStyle Width="50px"></ItemStyle>
                                            </asp:BoundField>
                                           <asp:BoundField DataField=""
                                                HeaderText=" Tax Rate">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                              <asp:BoundField DataField=""
                                                HeaderText=" Taxable Value">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                              <asp:BoundField DataField=""
                                                HeaderText=" IGST Credit">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildDetails" href="#collapseservices">Services</a>
                                </h4>
                            </div>
                            <div id="collapseservices" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GVServices" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                            <asp:BoundField DataField=""
                                                HeaderText="Description">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Accounting Code ">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>

                                            <asp:BoundField DataField=""
                                                HeaderText="Tax Rate">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                           <%-- <asp:BoundField DataField="CGSTAmt"
                                                HeaderText="CGST">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="Taxable Value"
                                                HeaderText="SGST ">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SGSTAmt"
                                                HeaderText="IGST Credit">
                                              <ItemStyle Width="50px"></ItemStyle>
                                            </asp:BoundField>
                                       <%-- <asp:BoundField DataField="TaxableValue"
                                                HeaderText="Value">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>--%>
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildDetails" href="#collapse5b">b) Total value of purchases on which ITC availed (intra-State)</a>
                                </h4>
                                <h4>Goods</h4>
                            </div>
                            <div id="collapse5b" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GVInterStateConsumer" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                            <asp:BoundField DataField=""
                                                HeaderText="Description">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="HSN Code"
                                                HeaderText="HSN CODE">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UQC"
                                                HeaderText="Value">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Quantity">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                          <asp:BoundField DataField=""
                                                HeaderText="Taxable Value">
                                                <ItemStyle Width="50px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Tax CGST Rate">
                                                <ItemStyle Width="50px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Tax SGST Rate">
                                                <ItemStyle Width="50px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Tax Credit CGST Rate">
                                                <ItemStyle Width="50px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Tax Credit SGST Rate">
                                                <ItemStyle Width="50px"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildDetails" href="#collapse5bService">Services</a>
                                </h4>
                            </div>
                            <div id="collapse5bService" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GVIntraStateConsumer" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                            <asp:BoundField DataField=""
                                                HeaderText="Description">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>

                                           
                                            <asp:BoundField DataField=""
                                                HeaderText="SAC">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Taxable Value">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                             <asp:BoundField DataField=""
                                                HeaderText="TaxCGSTRate">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                             
                                             <asp:BoundField DataField=""
                                                HeaderText="TaxSGSTRateT">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Tax Credit CGST"
                                                HeaderText="SGST">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Tax Credit SGST">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildDetails" href="#collapse5c">c. Total value of purchases on which ITC availed (Imports)</a>
                                </h4>
                                <h4>Goods</h4>
                            </div>
                            <div id="collapse5c" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GVExport" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                            <asp:BoundField DataField=""
                                                HeaderText="Description">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="HSN Code"
                                                HeaderText="HSN Code">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="UQC">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            
                                            
                                            <asp:BoundField DataField=""
                                                HeaderText="Quantity">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Tax Rate">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="CIF value">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="IGST">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                             <asp:BoundField DataField=""
                                                HeaderText="Custom Duty">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildDetails" href="#collapse5c_services">Services</a>
                                </h4>
                            </div>
                            <div id="collapse5c_services" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GV_5services" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                            <asp:BoundField DataField=""
                                                HeaderText="Description" />
                                            <asp:BoundField DataField=""
                                                HeaderText="SAC" />
                                            <asp:BoundField DataField=""
                                                HeaderText="Tax Rate" />
                                            <asp:BoundField DataField=""
                                                HeaderText="Taxable Value" />
                                            <asp:BoundField DataField=""
                                                HeaderText="IGST" />
                                            
                                           <%-- <asp:BoundField DataField="AdditionalTax"
                                                HeaderText="Additional Tax" />--%>
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildDetails" href="#collapse5d">(d)	Other Purchases on which no ITC availed</a>
                                </h4>
                            </div>
                            <div id="collapse5d" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GV_5d_ITC" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                             <asp:BoundField DataField=""
                                                HeaderText="Goods/ Services" />
                                            <asp:BoundField DataField=""
                                                HeaderText=" Purchase Value" />
                                            
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                       <%-- Ankita--%>

                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchaildOutWard" href="#collapse5e">5e. Sales Returns</a>
                                </h4>
                            </div>
                            <div id="collapse5e" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                             <asp:BoundField DataField=""
                                                HeaderText="Goods" />
                                            <asp:BoundField DataField=""
                                                HeaderText="HSN Code" />
                                            <asp:BoundField DataField=""
                                                HeaderText="Taxable Value" />

                                            <asp:BoundField DataField=""
                                                HeaderText="IGST" />
                                            <asp:BoundField DataField=""
                                                HeaderText="CGST" />
                                            <asp:BoundField DataField=""
                                                HeaderText="SGST" />
                                         <%--   <asp:BoundField DataField=""
                                                HeaderText="Additional Tax" />--%>
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <%--ankita--%>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildDetails" href="#collapse5f">5f. Other Expenditure (Expenditure other than purchases) </a>
                                </h4>
                            </div>
                            <div id="collapse5f" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GV_5f_Expenditure" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                             <asp:BoundField DataField=""
                                                HeaderText="Specify Head" />
                                            <asp:BoundField DataField=""
                                                HeaderText="Amount" />
                                          
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

     <div class="panel-group" id="accordionMainIncome">
         <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a data-toggle="collapse" data-parent="#accordionMainIncome" href="#collapse6Income">6. Details of INCOME:</a>
                </h4>
            </div>
            <div id="collapse6Income" class="panel-collapse collapse">
                <div class="panel-body">
                    <div class="panel-group" id="accordionchildIncome">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildIncome" href="#collapse6_Income">6a.Total value of supplies on which GST paid (inter-State Supplies)</a>
                                </h4>
                                <h4>Goods:</h4>
                            </div>
                            <div id="collapse6_Income" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                            <asp:BoundField DataField=""
                                                HeaderText="Description">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IGSTRate"
                                                HeaderText="HSN Code">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="UQC">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                           <%-- <asp:BoundField DataField="Quantity"
                                                HeaderText="IGST">
                                                <ItemStyle Width="50px"></ItemStyle>
                                            </asp:BoundField>--%>
                                           <asp:BoundField DataField=""
                                                HeaderText=" Tax Rate">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                              <asp:BoundField DataField=""
                                                HeaderText=" Taxable Value">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                              <asp:BoundField DataField=""
                                                HeaderText=" IGST ">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildIncome" href="#collapse6services">Services</a>
                                </h4>
                            </div>
                            <div id="collapse6services" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GV_6_Services" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                            <asp:BoundField DataField=""
                                                HeaderText="Description">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Accounting Code ">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>

                                            <asp:BoundField DataField=""
                                                HeaderText="Tax Rate">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                         
                                            <asp:BoundField DataField=""
                                                HeaderText="Taxable Value ">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="IGST">
                                              <ItemStyle Width="50px"></ItemStyle>
                                            </asp:BoundField>
                                      
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildIncome" href="#collapse6b">b) Total value of supplies on which GST Paid (intra-State Supplies)</a>
                                </h4>
                                <h4>Goods</h4>
                            </div>
                            <div id="collapse6b" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                            <asp:BoundField DataField=""
                                                HeaderText="Description">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="HSN Code"
                                                HeaderText="HSN CODE">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UQC"
                                                HeaderText="Value">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Quantity">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                          <asp:BoundField DataField=""
                                                HeaderText="Taxable Value">
                                                <ItemStyle Width="50px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Tax CGST Rate">
                                                <ItemStyle Width="50px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Tax SGST Rate">
                                                <ItemStyle Width="50px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText=" CGST Tax">
                                                <ItemStyle Width="50px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText=" SGST Tax">
                                                <ItemStyle Width="50px"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildDetails" href="#collapse6bService">Services</a>
                                </h4>
                            </div>
                            <div id="collapse6bService" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GV_6b_Services" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                            <asp:BoundField DataField=""
                                                HeaderText="Description">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>

                                           
                                            <asp:BoundField DataField=""
                                                HeaderText="SAC">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Taxable Value">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                             <asp:BoundField DataField=""
                                                HeaderText="TaxCGSTRate">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                             
                                             <asp:BoundField DataField=""
                                                HeaderText="TaxSGSTRate">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Tax SGST">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Tax CGST">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Tax SGST">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildIncome" href="#collapse6c">6c.Total value of supplies on which GST Paid (Exports)</a>
                                </h4>
                                <h4>Goods</h4>
                            </div>
                            <div id="collapse6c" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GV_6c" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                            <asp:BoundField DataField=""
                                                HeaderText="Goods">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="HSN Code"
                                                HeaderText="HSN Code">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="UQC">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            
                                            
                                            <asp:BoundField DataField=""
                                                HeaderText="Quantity">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Tax Rate">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="FOB value">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="IGST">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                             <asp:BoundField DataField=""
                                                HeaderText="Custom Duty">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildIncome" href="#collapse6c_services">Services</a>
                                </h4>
                            </div>
                            <div id="collapse6c_services" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GV_6c_Services" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                            <asp:BoundField DataField=""
                                                HeaderText="Services" />
                                            <asp:BoundField DataField=""
                                                HeaderText="SAC" />
                                            <asp:BoundField DataField=""
                                                HeaderText="Tax Rate" />
                                            <asp:BoundField DataField=""
                                                HeaderText="FOB Value" />
                                            <asp:BoundField DataField=""
                                                HeaderText="IGST" />
                                            
                                           <%-- <asp:BoundField DataField="AdditionalTax"
                                                HeaderText="Additional Tax" />--%>
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildIncome" href="#collapse6d">6d.Total value of supplies on which no GST Paid (Exports)	</a>
                                </h4>
                                <h4>Goods</h4>
                            </div>
                            <div id="collapse6d" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                             <asp:BoundField DataField=""
                                                HeaderText="Goods" />
                                            <asp:BoundField DataField=""
                                                HeaderText="HSN CODE" />
                                            <asp:BoundField DataField=""
                                                HeaderText=" UQC" />
                                            <asp:BoundField DataField=""
                                                HeaderText=" Quantity" />
                                            <asp:BoundField DataField=""
                                                HeaderText=" Tax Rate" />
                                            <asp:BoundField DataField=""
                                                HeaderText=" FOB Value" />
                                            
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                       <%-- Ankita--%>

                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildIncome" href="#collapse6d_Services">6d. Services</a>
                                </h4>
                            </div>
                            <div id="collapse6d_Services" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GridView9" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                             <asp:BoundField DataField=""
                                                HeaderText="Services" />
                                            <asp:BoundField DataField=""
                                                HeaderText="SAC" />
                                            <asp:BoundField DataField=""
                                                HeaderText="Tax Rate" />

                                            <asp:BoundField DataField=""
                                                HeaderText="FOB Value" />
                                            
                                         <%--   <asp:BoundField DataField=""
                                                HeaderText="Additional Tax" />--%>
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <%--ankita--%>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildIncome" href="#collapse6e">6e. Value of Other Supplies on which no GST paid </a>
                                </h4>
                            </div>
                            <div id="collapse6e" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GV_6E_GST" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                             <asp:BoundField DataField=""
                                                HeaderText="Goods/Services" />
                                            <asp:BoundField DataField=""
                                                HeaderText="Value" />
                                          
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildIncome" href="#collapse6f">6f.Purchase Returns	</a>
                                </h4>
                                <h4>Goods</h4>
                            </div>
                            <div id="collapse6f" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                             <asp:BoundField DataField=""
                                                HeaderText="Goods" />
                                            <asp:BoundField DataField=""
                                                HeaderText="HSN CODE" />
                                            <asp:BoundField DataField=""
                                                HeaderText="Taxable Value " />
                                            <asp:BoundField DataField=""
                                                HeaderText=" IGST" />
                                            <asp:BoundField DataField=""
                                                HeaderText=" CGST" />
                                            <asp:BoundField DataField=""
                                                HeaderText=" SGST" />
                                            
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildIncome" href="#collapse6fservices">6f.Services</a>
                                </h4>
                            </div>
                            <div id="collapse6fservices" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GridView10" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                            <asp:BoundField DataField=""
                                                HeaderText="Services">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="SAC ">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>

                                            <asp:BoundField DataField=""
                                                HeaderText="Taxable Value">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="IGST">
                                              <ItemStyle Width="50px"></ItemStyle>
                                            </asp:BoundField>
                                               <asp:BoundField DataField=""
                                                HeaderText="CGST">
                                              <ItemStyle Width="50px"></ItemStyle>
                                            </asp:BoundField>
                                               <asp:BoundField DataField=""
                                                HeaderText="SGST">
                                              <ItemStyle Width="50px"></ItemStyle>
                                            </asp:BoundField>
                                      
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                </div>
            </div>
        </div> 
    </div>

    <%--checking--%>

    <div class="panel-group" id="accordionReturn">
         <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a data-toggle="collapse" data-parent="#accordionMainIncome" href="#collapse7Return">7. Return reconciliation Statement </a>
                </h4>
            </div>
            <div id="collapse7Return" class="panel-collapse collapse">
                <div class="panel-body">
                    <div class="panel-group" id="accordionchildReturn">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildReturn" href="#collapse7_Return">7a.IGST </a>
                                </h4>
                                <h4>Goods:</h4>
                            </div>
                            <div id="collapse7_Return" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GV_7_IGST" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                            <asp:BoundField DataField=""
                                                HeaderText="Month">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="TaxPaid">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Tax Payable (As per audited a/c)**">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                           <%-- <asp:BoundField DataField="Quantity"
                                                HeaderText="IGST">
                                                <ItemStyle Width="50px"></ItemStyle>
                                            </asp:BoundField>--%>
                                           <asp:BoundField DataField=""
                                                HeaderText="Difference">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                              <asp:BoundField DataField=""
                                                HeaderText=" Interest">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                              <asp:BoundField DataField=""
                                                HeaderText=" Penalty ">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildReturn" href="#collapse7CGST">7B. CGST</a>
                                </h4>
                            </div>
                            <div id="collapse7CGST" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                           <asp:BoundField DataField=""
                                                HeaderText="Month">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="TaxPaid">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Tax Payable (As per audited a/c)**">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                           <asp:BoundField DataField=""
                                                HeaderText="Difference">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                              <asp:BoundField DataField=""
                                                HeaderText=" Interest">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                              <asp:BoundField DataField=""
                                                HeaderText=" Penalty ">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                      
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildReturn" href="#collapse7c">7c. SGST</a>
                                </h4>
                                
                            </div>
                            <div id="collapse7c" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GV_7_SGST" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                            <asp:BoundField DataField=""
                                                HeaderText="Month">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="TaxPaid">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Tax Payable (As per audited a/c)**">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                           <asp:BoundField DataField=""
                                                HeaderText="Difference">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                              <asp:BoundField DataField=""
                                                HeaderText=" Interest">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                              <asp:BoundField DataField=""
                                                HeaderText=" Penalty ">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        
                       <%-- Ankita--%>

                    </div>
                </div>
            </div>
        </div> 
         
         
    </div>

    <div class="panel-group" id="accordionAmounts">
         <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a data-toggle="collapse" data-parent="#accordionMainAmount" href="#collapse8Amount">8. Other Amounts@@ </a>
                </h4>
            </div>
            <div id="collapse8Amount" class="panel-collapse collapse">
                <div class="panel-body">
                    <div class="panel-group" id="accordionchildAmount">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildAmount" href="#collapse8A_Amounts">8A. Arrears (Audit/Assessment etc.)</a>
                                </h4>
                             
                            </div>
                            <div id="collapse8A_Amounts" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GV_8A_Amounts" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                            <asp:BoundField DataField=""
                                                HeaderText="Details of Order">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Tax Payable">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Interest">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                          
                                           <asp:BoundField DataField=""
                                                HeaderText="Penalty">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                              <asp:BoundField DataField=""
                                                HeaderText="Current Status ofthe Order">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                              <asp:BoundField DataField=""
                                                HeaderText=" Penalty ">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordionchildAmount" href="#collapse8B">8B. Refunds</a>
                                </h4>
                            </div>
                            <div id="collapse8B" class="panel-collapse collapse">
                                <div class="panel-body">
                                    <asp:GridView ID="GV_8B_Refunds" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                        <Columns>
                                           <asp:BoundField DataField=""
                                                HeaderText="Details of Claim">
                                                <ControlStyle Font-Size="Small" />
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Date of Filing">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField=""
                                                HeaderText="Amount ofRefund">
                                                <ItemStyle Width="270px"></ItemStyle>
                                            </asp:BoundField>
                                           <asp:BoundField DataField=""
                                                HeaderText="Current Status of the claim">
                                                <ItemStyle Width="100px"></ItemStyle>
                                            </asp:BoundField>
                                             
                                        </Columns>


                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <div>This may be divided into parts:-
                             i)	amount already paid / refund already received during the year, 
                            ii)	amount payable / refund pending.	
                            </div>
                        </div>
                        
                        
                       <%-- Ankita--%>
                    </div>


                </div>
            </div>
        </div> 
         
         
    </div>
    </asp:Content>
