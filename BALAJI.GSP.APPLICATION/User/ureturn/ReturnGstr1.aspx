<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeBehind="ReturnGstr1.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.User.ureturn.ReturnGstr1" %>


<%@ Register Src="~/UC/UC_Invoice/uc_invoiceMonth.ascx" TagPrefix="uc1" TagName="uc_invoiceMonth" %>
<%@ Register Src="~/UC/UC_Gstr/uc_Gstr_Tileview.ascx" TagPrefix="uc1" TagName="uc_Gstr_Tileview" %>
<%@ Register Src="~/UC/uc_GSTNUsers.ascx" TagPrefix="uc1" TagName="uc_GSTNUsers" %>



<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
       
        <div class="box box-primary">
            <div class="box-body ">
                <div class="row" style="padding-top:15px" >
                    <uc1:uc_GSTNUsers runat="server" ID="uc_GSTNUsers" />
                    </div>
               
                <div class="row">
                    <div class="col-md-4">
                       
                            <label>Financial Year:</label>
                            <asp:DropDownList ID="ddlfinYear" runat="server"  OnSelectedIndexChanged="ddlfinYear_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                        
                    </div>
                    <div class="col-md-4">
                      
                            <label>Return Filing Period</label>
                            <%--<asp:DropDownList ID="ddlmonth" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                            <uc1:uc_invoiceMonth runat="server" ID="uc_invoiceMonth" />
                        
                    </div>
                    <div class="col-md-2">
                       <div class="form-group">
                          
                        <asp:Button ID="btnSubmit" Text="Submit" style="margin-top:22px !important;" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click"></asp:Button>
                               </div>
                          
                        </div>
                    <div class="col-md-2"></div>
                    </div>
               </div>


        
            </div>
        <div class="box box-default">    
             <uc1:uc_Gstr_Tileview runat="server" ID="uc_Gstr_Tileview" />

        </div>
           
               
</div>
</asp:Content>
