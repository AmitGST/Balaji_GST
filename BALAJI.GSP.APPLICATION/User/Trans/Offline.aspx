<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeBehind="Offline.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.User.Trans.Offline" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>
<%@ Register Src="~/UC/Offline/uc_B2B_Invoices.ascx" TagPrefix="uc1" TagName="uc_B2B_Invoices" %>


<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-header">
        <h1>Offline</h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Offline</a></li>
        </ol>
    </div>
    <div class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">File Returns</h3>
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
                <div class="row">
                    <div class="col-sm-4">
                        <div class="form-group">
                            <asp:Label ID="lblGST" AssociatedControlID="ddlGST" runat="server">GST Statement/Returns&nbsp;<span class="required" style="color:red;">*</span></asp:Label>
                            <asp:DropDownList ID="ddlGST" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="form-group">
                            <asp:Label ID="Label1" AssociatedControlID="txtGstNo" runat="server">GSTIN Of Supplier&nbsp;<span class="required" style="color:red;">*</span></asp:Label>
                            <asp:TextBox ID="txtGstNo" class="form-control" MaxLength="15" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ControlToValidate="txtGstNo" ID="revGstNo" ValidationGroup="vgGstNo" Display="Dynamic" ValidationExpression="[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9A-Za-z]{1}[Z]{1}[0-9a-zA-Z]{1}" runat="server" ErrorMessage="GSTIN no is invalid."></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ControlToValidate="txtGstNo" ID="reqGST" ValidationGroup="vgGstNo" Display="Dynamic" runat="server" ErrorMessage="Please Specify GSTN NO"></asp:RequiredFieldValidator>

                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="form-group">
                            <asp:Label ID="label2" AssociatedControlID="ddlfinYear" runat="server">Financial Year&nbsp;<span class="required" style="color:red;">*</span></asp:Label>
                            <asp:DropDownList ID="ddlfinYear" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>

                </div>
            
                         <div class="row">
                             <uc1:uc_B2B_Invoices Visible="false" runat="server" ID="uc_B2B_Invoices" />
<%--                    <div class="col-sm-4">
                        <div class="form-group">
                            <asp:Label ID="Label3" AssociatedControlID="ddlMonth" runat="server">Tax Period&nbsp;<span class="required" style="color:red;">*</span></asp:Label>
                            <asp:DropDownList ID="ddlMonth" CssClass="form-control" runat="server">
                            </asp:DropDownList>

                        </div>
                    </div>--%>
                    <div class="col-sm-4">
                        <div class="form-group">
                            <asp:Label ID="Label4" AssociatedControlID="txtTurnover" runat="server">Aggr. Turnover in Preceding Fin. Yr.&nbsp;<span class="required" style="color:red;">*</span></asp:Label>
                            <asp:TextBox ID="txtTurnover" CssClass="form-control" onkeypress="return onlyNos(event,this);" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtTurnover" ID="ReqTurnover" ValidationGroup="vgGstNo" Display="Dynamic" runat="server" ErrorMessage="Please Specify TurnOver"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="form-group">
                            <asp:Label ID="Label5" AssociatedControlID="txtTurnoverAToJ" runat="server">Aggregate Turnover &nbsp;<span class="required" style="color:red;">*</span></asp:Label>
                            <asp:TextBox ID="txtTurnoverAToJ" CssClass="form-control" onkeypress="return onlyNos(event,this);" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtTurnoverAToJ" ID="ReqTurnoverQuarter" ValidationGroup="vgGstNo" Display="Dynamic" runat="server" ErrorMessage="Please Specify Turnover Quarter"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                     </div>

                    <div class="row">
                        <div class="col-sm-4">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <ajaxToolkit:AsyncFileUpload Width="400px" ID="AsyncFileUpload1" ThrobberID="myThrobber" maximumnumberoffiles="1" OnUploadedComplete="AsyncFileUpload1_UploadedComplete" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lbImport" />
                            </Triggers>
                        </asp:UpdatePanel>
                            </div>
                        </div>
                </div>
                        <div class="box-footer">
                            <uc1:uc_sucess runat="server" ID="uc_sucess" />
                            <asp:LinkButton ID="lbImport" CssClass="btn btn-primary pull-right" ValidationGroup="vgGstNo" OnClick="lbImport_Click" runat="server"><i class="fa fa-upload"></i>&nbsp;Upload Excel</asp:LinkButton>
                            <asp:LinkButton ID="lkbBack" CssClass="btn btn-danger pull-left" OnClick="lkbBack_Click" runat="server"><i class="fa fa-backward"></i>&nbsp; Back </asp:LinkButton>
                        </div>
        </div>
    </div>
</asp:Content>
