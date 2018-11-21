<%@ Page Title="" Language="C#" MasterPageFile="../User.Master" AutoEventWireup="true" CodeBehind="GSTR5.aspx.cs" Inherits="UserInterface.GSTR5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%-- <div class="row">
        <div class="col-lg-12">
            <h2 class="page-header">GSTR-5 RETURN FOR NON RESIDENT TAXABLE PERSONS (FOREIGNERS)</h2>
        </div>
        <!-- /.col-lg-12 -->
    </div>--%>


    <%--<div class="row">--%>
    <section class="content-header">
        <h1>GSTR5
        <small>RETURN FOR NON RESIDENT TAXABLE PERSONS (FOREIGNERS)</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">RETURN</a></li>
            <li class="active">GSTR5</li>
        </ol>
    </section>

    <section class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">GSTR5</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>

            <div class="col-lg-12">
                <%--<div class="panel panel-primary">--%>
                <table style="width: 100%">
                    <tr>

                        <td style="width: 15%; padding-bottom: 2px;">
                            <asp:Label ID="Label2" runat="server" Text="From Date"></asp:Label><br />
                            <asp:TextBox ID="txtFromDate" runat="server" class="form-control" Width="100px" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td style="width: 85%; padding-bottom: 2px;">
                            <asp:Label ID="Label3" runat="server" Text="To Date"></asp:Label><br />
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Width="100px" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        </td>
                    </tr>
                </table>
                <%-- </div>--%>
            </div>


            <div class="box-body" style="display: block;">
                <div class="row">
                    <div class="col-lg-6" style="width: 100%">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                Taxpayer Details
                            </div>

                            <div class="panel-body">
                                <asp:Label ID="lblGSTIN" runat="server"></asp:Label><br />
                                <asp:Label ID="lbltaxpayerName" runat="server"></asp:Label><br />
                                <asp:Label ID="lblPeriod" runat="server"></asp:Label>
                                <asp:Label ID="lblGSTINVal" runat="server" Style="display: none;"></asp:Label><br />
                            </div>


                        </div>
                    </div>
                </div>
            </div>

            <div class="box-body" style="display: block;">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel-group" id="accordionGSTR1A5a">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion_goods" href="#collapse5_Goods">5.Goods imported</a>
                                    </h4>
                                </div>
                                <div id="collapse5_Goods" class="panel-collapse collapse ">
                                    <div class="panel-body">
                                        <asp:GridView ID="gv_5_Inward" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                            <Columns>
                                                <asp:BoundField DataField=""
                                                    HeaderText="Description of goods" />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Bill Entry No." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Bill Entry Date." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="HSN code." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="UQC." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Quantity." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Value " />
                                                <asp:BoundField DataField=""
                                                    HeaderText="IGST paid, if any." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Eligibility for ITC as input" />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Total IGST " />
                                                <asp:BoundField DataField=""
                                                    HeaderText="ITC available this month " />
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
            <div class="box-body" style="display: block;">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel-group" id="accordionGSTR1A81">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordionInward5" href="#collapse5_Inward">5A. Amendments in Goods imported of earlier tax periods</a>
                                    </h4>
                                </div>
                                <div id="collapse5_Inward" class="panel-collapse collapse ">
                                    <div class="panel-body">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                            <Columns>
                                                <asp:BoundField DataField=""
                                                    HeaderText="Original Bill no." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Original Bill date." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Revised Bill no." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Revised Bill date." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Revised Bill value." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="HSN" />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Taxable value" />
                                                <asp:BoundField DataField=""
                                                    HeaderText="IGST Rate" />
                                                <asp:BoundField DataField=""
                                                    HeaderText="IGST Amt" />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Eligibility for ITC as inputs" />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Total IGST available as  ITC" />
                                                <asp:BoundField DataField=""
                                                    HeaderText="ITC available this month" />
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

            <div class="box-body" style="display: block;">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel-group" id="accordionGSTR1A81A">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion6_Services" href="#collapse6_services">6. Services received from a supplier located outside India (Import of services)</a>
                                    </h4>
                                </div>
                                <div id="collapse6_services" class="panel-collapse collapse ">
                                    <div class="panel-body">
                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                            <Columns>
                                                <asp:BoundField DataField=""
                                                    HeaderText="Invoice No." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Invoice Date." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Invoice Value." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="SAC" />

                                                <asp:BoundField DataField=""
                                                    HeaderText="Taxable Value." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="IGST Rate" />
                                                <asp:BoundField DataField=""
                                                    HeaderText="IGST Amt" />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Total ITC Admissible as input " />
                                                <asp:BoundField DataField=""
                                                    HeaderText="ITC admissiblethis month" />
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
            <div class="box-body" style="display: block;">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel-group" id="accordionGSTR1A81A">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion6A_Amendments" href="#collapse6A_Amendments">6A. Amendments in Services received from a supplier located outside India (Import of services) of earlier tax periods</a>
                                    </h4>
                                </div>
                                <div id="collapse6A_Amendments" class="panel-collapse collapse ">
                                    <div class="panel-body">
                                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                            <Columns>

                                                <asp:BoundField DataField=""
                                                    HeaderText="Original Invoice No." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Original Invoice Date." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Revised Invoice No." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Revised Invoice Date." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Invoice Value." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Invoice Goods." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="SAC." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Taxable Value." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="IGST Rate" />
                                                <asp:BoundField DataField=""
                                                    HeaderText="IGST Amt" />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Total ITC Admissible as input " />
                                                <asp:BoundField DataField=""
                                                    HeaderText=" ITC admissiblethis month" />
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
            <div class="box-body" style="display: block;">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="panel-group" id="accordionGSTR1A81A">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion_Outward" href="#collapse7_Outward">7. Outward supplies made:</a>
                                    </h4>
                                </div>
                                <div id="collapse7_Outward" class="panel-collapse collapse ">
                                    <div class="panel-body">
                                        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                            <Columns>
                                                <asp:BoundField DataField=""
                                                    HeaderText="GSTIN if any" />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Invoice No." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Invoice Date." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Invoice Value." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Invoice Goods." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="HSN." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Taxable Value." />
                                                <asp:BoundField DataField=""
                                                    HeaderText="IGST Rate" />
                                                <asp:BoundField DataField=""
                                                    HeaderText="IGST Amt" />
                                                <asp:BoundField DataField=""
                                                    HeaderText="CGST Rate" />
                                                <asp:BoundField DataField=""
                                                    HeaderText="CGST Amt" />
                                                <asp:BoundField DataField=""
                                                    HeaderText="SGST Rate" />
                                                <asp:BoundField DataField=""
                                                    HeaderText="SGST Amt" />
                                                <asp:BoundField DataField=""
                                                    HeaderText="POS" />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Indicate if supply  attracts reverse charge $" />
                                                <asp:BoundField DataField=""
                                                    HeaderText="Date of time of supply if it is before  date of invoice" />
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

            <div class="box-body" style="display: block;">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion7A_Amendments" href="#collapse7A_Amendments">7A. Amendments to details in Outward supplies </a>
                        </h4>
                    </div>
                    <div id="collapse7A_Amendments" class="panel-collapse collapse ">
                        <div class="panel-body">
                            <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                <Columns>
                                    <asp:BoundField DataField=""
                                        HeaderText="Original Invoice" />
                                    <asp:BoundField DataField=""
                                        HeaderText="GSTIN" />
                                    <asp:BoundField DataField=""
                                        HeaderText="Revised Invoice No." />
                                    <asp:BoundField DataField=""
                                        HeaderText="Revised Invoice Date." />

                                    <asp:BoundField DataField=""
                                        HeaderText="Revised Invoice Goods." />
                                    <asp:BoundField DataField=""
                                        HeaderText="HSN." />
                                    <asp:BoundField DataField=""
                                        HeaderText="Taxable Value." />
                                    <asp:BoundField DataField=""
                                        HeaderText="IGST Rate" />
                                    <asp:BoundField DataField=""
                                        HeaderText="IGST Amt" />
                                    <asp:BoundField DataField=""
                                        HeaderText="CGST Rate" />
                                    <asp:BoundField DataField=""
                                        HeaderText="CGST Amt" />
                                    <asp:BoundField DataField=""
                                        HeaderText="SGST Rate" />
                                    <asp:BoundField DataField=""
                                        HeaderText="SGST Amt" />
                                    <asp:BoundField DataField=""
                                        HeaderText="POS" />
                                    <asp:BoundField DataField=""
                                        HeaderText="Date of time of supply if different from date of invoice" />
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
            <div class="box-body" style="display: block;">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion7_Details" href="#collapse7_Details">Details of Credit/Debit Notes </a>
                        </h4>
                    </div>
                    <div id="collapse7_Details" class="panel-collapse collapse ">
                        <div class="panel-body">
                            <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                <Columns>
                                    <asp:BoundField DataField=""
                                        HeaderText="GSTIN " />
                                    <asp:BoundField DataField=""
                                        HeaderText="Type of note ." />
                                    <asp:BoundField DataField=""
                                        HeaderText="Debit Note No." />
                                    <asp:BoundField DataField=""
                                        HeaderText="Debit Note Date." />
                                    <asp:BoundField DataField=""
                                        HeaderText="Invoice No." />
                                    <asp:BoundField DataField=""
                                        HeaderText="Invoice Date." />
                                    <asp:BoundField DataField=""
                                        HeaderText="Differential Value (Plus or Minus" />
                                    <asp:BoundField DataField=""
                                        HeaderText="IGST Rate" />
                                    <asp:BoundField DataField=""
                                        HeaderText="IGST Amt" />
                                    <asp:BoundField DataField=""
                                        HeaderText="CGST Rate" />
                                    <asp:BoundField DataField=""
                                        HeaderText="CGST Amt" />
                                    <asp:BoundField DataField=""
                                        HeaderText="SGST Rate" />
                                    <asp:BoundField DataField=""
                                        HeaderText="SGST Amt" />
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
                    <div>
                        <strong>Note: </strong>Information about Credit Note / Debit Note to be submitted only if issued as a supplier
                    </div>
                </div>
            </div>
            <div class="box-body" style="display: block;">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion7A_Amendment" href="#collapse7_Amendment">7A. Amendment to Details of Credit/Debit Notes of earlier tax periods</a>
                        </h4>
                    </div>
                    <div id="collapse7_Amendment" class="panel-collapse collapse ">
                        <div class="panel-body">
                            <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                <Columns>
                                    <asp:BoundField DataField=""
                                        HeaderText="GSTIN " />
                                    <asp:BoundField DataField=""
                                        HeaderText="Type of note " />
                                    <asp:BoundField DataField=""
                                        HeaderText="Original Debit No." />
                                    <asp:BoundField DataField=""
                                        HeaderText="Original Debit Date." />
                                    <asp:BoundField DataField=""
                                        HeaderText="Revised Debit No." />
                                    <asp:BoundField DataField=""
                                        HeaderText="Revised Debit Date." />
                                    <asp:BoundField DataField=""
                                        HeaderText="Differential Value (Plus or Minus)" />
                                    <asp:BoundField DataField=""
                                        HeaderText="IGST Rate" />
                                    <asp:BoundField DataField=""
                                        HeaderText="IGST Amt" />
                                    <asp:BoundField DataField=""
                                        HeaderText="CGST Rate" />
                                    <asp:BoundField DataField=""
                                        HeaderText="CGST Amt" />
                                    <asp:BoundField DataField=""
                                        HeaderText="SGST Rate" />
                                    <asp:BoundField DataField=""
                                        HeaderText="SGST Amt" />
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
                    <div>
                        <strong>Note: </strong>Information about Credit Note / Debit Note to be submitted only if issued as a supplier.
                    </div>
                </div>
            </div>
            <div class="box-body" style="display: block;">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion8A_Tax" href="#collapse8A_Tax">8. Tax paid (figures in Rs.) </a>
                        </h4>
                    </div>
                    <div id="collapse8A_Tax" class="panel-collapse collapse ">
                        <div class="panel-body">
                            <asp:GridView ID="GridView8" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                <Columns>
                                    <asp:BoundField DataField=""
                                        HeaderText="Description" />
                                    <asp:BoundField DataField=""
                                        HeaderText="Tax payable " />
                                    <asp:BoundField DataField=""
                                        HeaderText="Debit no. in ITC ledger" />
                                    <asp:BoundField DataField=""
                                        HeaderText="ITC (IGST) utilized." />
                                    <asp:BoundField DataField=""
                                        HeaderText="Debit no. in cash ledger" />
                                    <asp:BoundField DataField=""
                                        HeaderText="Tax paid in cash (after adjusting  ITC)." />
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
            <div class="box-body" style="display: block;">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion9_Goods" href="#collapse9_Goods">9. Closing stock of Goods </a>
                        </h4>
                    </div>
                    <div id="collapse9_Goods" class="panel-collapse collapse ">
                        <div class="panel-body">
                            <asp:GridView ID="GridView9" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                <Columns>
                                    <asp:BoundField DataField=""
                                        HeaderText="Description of goods" />
                                    <asp:BoundField DataField=""
                                        HeaderText="HSN" />
                                    <asp:BoundField DataField=""
                                        HeaderText="UQC" />
                                    <asp:BoundField DataField=""
                                        HeaderText="Value (Rs.)" />
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
            <div class="box-body" style="display: block;">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordionInward5" href="#collapse10_Refund">10. Refund Claimed from Cash Ledger</a>
                        </h4>
                    </div>
                    <div id="collapse10_Refund" class="panel-collapse collapse ">
                        <div class="panel-body">
                            <asp:GridView ID="GridView10" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                <Columns>
                                    <asp:BoundField DataField=""
                                        HeaderText="Details" />
                                    <asp:BoundField DataField=""
                                        HeaderText="IGST " />
                                    <asp:BoundField DataField=""
                                        HeaderText="CGST " />
                                    <asp:BoundField DataField=""
                                        HeaderText="SGST" />
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
    </section>
</asp:Content>
