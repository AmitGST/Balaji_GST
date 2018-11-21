<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Account.Registration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
      <div class="content">
      <div class="row">
                <div class="col-sm-3">
            <div class="form-group">
            <label> Email</label>
           <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
           </div>
                 </div>
                <div class="col-sm-3">
                    <div class="form-group">
                        <label>Contact Number</label>
                         <asp:TextBox ID="txtContactNo" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="form-group">
                        <label>FirstName</label>
                        <asp:TextBox ID="txtFirstNo" CssClass="form-control" runat="server"></asp:TextBox>

                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="form-group">
                        <label>LasttName</label>
                        <asp:TextBox ID="txtLadtName" CssClass="form-control" runat="server"></asp:TextBox>

                    </div>
                </div>
            </div>
      <div class="row">
                <div class="col-sm-3">
                    <div class="form-group">
                        <label>LasttName</label>
                        <asp:TextBox ID="txtLastName" CssClass="form-control" runat="server"></asp:TextBox>

                    </div>
                </div>
                <div class="col-sm-3">
             <div class="form-group">
             <label>GSTN NO</label>
             <asp:TextBox ID="txtGSTNNO" CssClass="form-control" runat="server"></asp:TextBox>
                 </div>
         </div>
                <div class="col-sm-3">
             <div class="form-group">
             <label>Organization Name</label>
             <asp:TextBox ID="txtOrganizationName" CssClass="form-control" runat="server"></asp:TextBox>
                 </div>
         </div>
                <div class="col-sm-3">
             <div class="form-group">
                 <label>Address</label>
                 <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server"></asp:TextBox>
             </div>
         
        </div>
        </div>
      <div class="row">
               <div class="col-sm-3">
            <div class="form-group">
             <label>State</label>
                 <asp:TextBox ID="txtStateName" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
               <div class="col-sm-3">
                <div class="form-group">
                 <label>Designation</label>
                 <asp:TextBox ID="txtDesignation" CssClass="form-control" runat="server"></asp:TextBox>
             </div>
            </div>
            </ div>
     <asp:Button ID="btnUserRegistraion" class="btn btn-primary" Text="Submit" runat="server" /></div>


          
     
     </asp:Content>
