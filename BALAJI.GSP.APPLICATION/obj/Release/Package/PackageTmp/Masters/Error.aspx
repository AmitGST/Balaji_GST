<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.Masters.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content">
      <div class="error-page">
       <%-- <h2 class="headline text-yellow"> 404</h2>--%>

        <div class="error-content">
          <h3><i class="fa fa-warning text-yellow"></i> Oops! Something went wrong.</h3>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
          <p>
            We could not find the page you were looking for.
            Meanwhile, you may <%--<a href="~/User/Dashboard.aspx"></a>--%>
              <asp:HyperLink NavigateUrl="~/User/Dashboard.aspx" runat="server">return to dashboard</asp:HyperLink>
              and Try again.
          </p>

          <div class="search-form">
            <div class="input-group">
             
               <%-- <asp:TextBox ID="TextBox1" Text="Search" class="form-control" Placeholder="Search" runat="server"></asp:TextBox>--%>
              <div class="input-group-btn">
                <%--<button type="submit" name="submit" class="btn btn-warning btn-flat"><i class="fa fa-search"></i>
                </button>--%>
              </div>
            </div>
            <!-- /.input-group -->
          </div>
        </div>
        <!-- /.error-content -->
      </div>
        </div>
</asp:Content>
