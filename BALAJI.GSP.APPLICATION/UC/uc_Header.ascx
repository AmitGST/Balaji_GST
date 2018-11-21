<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_Header.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.uc_Header" %>
<%@ Register Src="~/UC/uc_GSTNUsers.ascx" TagPrefix="uc1" TagName="uc_GSTNUsers" %>



<%--<div id="Header1" runat="server" visible='<%#Common.GetTheme() == "EZ" ? true : false%>'>
   THIS COMMENT FOR EZ 
    <a class="logo">THIS COMMENT FOR BALA <span class="logo-mini"></span>
        <span class="logo-lg">EZOLLUTION-

 
          THIS COMMENT FOR EZ </span>
    </a>
</div>--%>
<%--<div id="Header2" runat="server" visible='<%#Common.GetTheme() == "BALAJI" ? true : false%>'>
    <a class="logo" style="text-align: left !important; line-height: 0px !important; padding: 0px !important;">
       

        <span class="logo-mini"></span>

        <span class="logo-lg">
          
            <asp:Image ID="Image2" ImageUrl="~/Images/logo.jpg" Height="50px" Width="250px" BackColor="white" runat="server" />
           </span>
    </a>
</div>--%>
<a class="logo" style="text-align: left !important; line-height: 0px !important; padding: 0px !important;"> <%--THIS COMMENT FOR EZ --%>
    <%--<a class="logo" >--%><%--THIS COMMENT FOR BALA--%>
    
    <span class="logo-mini"></span>
    
    <span class="logo-lg">
      <%--  EZOLLUTION<--%><%--THIS COMMENT FOR BALA--%>
 <asp:Image ID="Image2" ImageUrl="~/Images/logo.png" Height="50px" Width="250px" BackColor="white" runat="server" />
          <%--THIS COMMENT FOR EZ --%></span>
</a>

<!-- Header Navbar: style can be found in header.less -->
<nav class="navbar navbar-static-top">
    <!-- Sidebar toggle button-->
    <a href="#" class="sidebar-toggle" data-toggle="offcanvas" role="button">
        <span class="sr-only">Toggle navigation</span>
    </a>
    <!-- Navbar Right Menu -->
    <div class="container-fluid">
        <div class="navbar-custom-menu">

            <ul class="nav navbar-nav input-group input-group-lg">
                 <li class="dropdown" runat="server" visible="false" id="liUserMgmt">
                       <%-- <a href="#" class="dropdown-toggle" data-toggle="dropdown" aria-expanded="true">
                            <i class="fa fa-globe"></i>
                            <span class="">ASP Management<b class="caret"></b></span>
                        </a>--%>
                  <ul class="dropdown-menu">
                      <li id="li1"  runat="server"><asp:HyperLink ID="ASPUser" NavigateUrl="~/Masters/UserMgmt.aspx" runat="server"><i class="fa fa-file"></i>Asp User Management</asp:HyperLink></li>
                  </ul>
                  </li>
                <%--<li class="dropdown" runat="server" visible="false" id="logManagement">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" aria-expanded="true">
                            <i class="fa fa-globe"></i>
                            <span class="">Log Management<b class="caret"></b></span>
                        </a>--%>
                  <%--<ul class="dropdown-menu">
                      <li id="li2"  runat="server"><asp:HyperLink ID="HyperLink9" NavigateUrl="~/Masters/ErrorLog.aspx" runat="server"><i class="fa fa-file"></i>Error Handling</asp:HyperLink></li>
                       <li id="li3"  runat="server"><asp:HyperLink ID="HyperLink10" NavigateUrl="~/LogManagement/ExceptionLog.aspx" runat="server"><i class="fa fa-file"></i>Exception Handling</asp:HyperLink></li>
                      <li id="li4"  runat="server"><asp:HyperLink ID="HyperLink11" NavigateUrl="~/LogManagement/MessageLog.aspx" runat="server"><i class="fa fa-file"></i>Message Handling</asp:HyperLink></li>
                </ul>--%>
                  </li>
                <%--<li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" aria-expanded="true">
                            <i class="fa fa-globe"></i>
                            <span class="">GSTN<b class="caret"></b></span>
                        </a>
                  <ul class="dropdown-menu">
                      <li id="liDSC" runat="server"><asp:HyperLink ID="DSC" NavigateUrl="~/Masters/Dsc.aspx" runat="server"><i class="fa fa-file"></i>DSC Registration</asp:HyperLink></li>
                  </ul>
                  </li>--%>
              
          <%--  <li runat="server" visible="false" id="liGSTNNO" >
                <uc1:uc_GSTNUsers runat="server" ID="uc_GSTNUsers"  />   
            </li>
              --%>
                <li class="dropdown">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" aria-expanded="true">
                        <i class="fa fa-database"></i>
                        <span class="">Masters<b class="caret"></b></span>
                    </a>
                    <ul class="dropdown-menu">
                        <li id="ligroup" runat="server" visible="false">
                            <asp:HyperLink ID="Group" runat="server" NavigateUrl="~/Masters/Group.aspx"> <i class="fa fa-object-group"></i><span>Group</span></asp:HyperLink></li>
                        <li id="liSubGroup" runat="server" visible="false">
                            <asp:HyperLink ID="SubGroup" runat="server" NavigateUrl="~/Masters/SubGroup.aspx"><i class="fa fa-object-ungroup"></i><span>Sub-Group</span></asp:HyperLink></li>
                        <li id="liClass" runat="server" visible="false">
                            <asp:HyperLink ID="Class" runat="server" NavigateUrl="~/Masters/Class.aspx"><i class="fa fa-file-o"></i><span>Class</span></asp:HyperLink></li>
                        <li id="liSubClass" runat="server" visible="false">
                            <asp:HyperLink ID="SubClass" runat="server" NavigateUrl="~/Masters/SubClass.aspx"><i class="fa fa-files-o"></i><span>Sub-Class</span></asp:HyperLink></li>
                        <li id="liState" runat="server" visible="false">
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Masters/StateMaster.aspx"><i class="fa fa-map"></i><span>State</span></asp:HyperLink></li>

                        <li id="liHSN" runat="server" visible="false" class="dropdown dropdown-submenu">
                            <asp:HyperLink ID="HyperLink2" NavigateUrl="~/Masters/Hsn.aspx" runat="server"><i class="fa fa-barcode"></i><span>Harmonized Serial Number (HSN)</span></asp:HyperLink></li>
                        <%--<ul class="dropdown-menu">
													<li><asp:HyperLink ID="HyperLink7"  NavigateUrl="#" runat="server">HSN</asp:HyperLink></li>
                                                    <li><asp:HyperLink ID="HyperLink8"  NavigateUrl="#" runat="server">Notified HSN</asp:HyperLink></li>
	                        </ul>--%>
                        <li id="liSAC" runat="server" visible="false">
                            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Masters/Sac.aspx"><i class="fa fa-cog"></i><span>Service Accounting Code (SAC)</span></asp:HyperLink></li>
                        <%--<li><asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Masters/UpdateSaleRegister.aspx"><i class="fa fa-book"></i><span>Sale Register</span></asp:HyperLink></li>--%>
                        <li id="liPerchaseRegister" runat="server" visible="false">
                            <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Masters/UpdateSaleRegister.aspx"><i class="fa fa-shopping-cart"></i><span>Purchase Register</span></asp:HyperLink></li>
                        <li id="liVendorRegister" runat="server" visible="false">
                            <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/Masters/VendorRegistration.aspx"><i class="fa fa-registered"></i><span>Vendor Registration</span></asp:HyperLink></li>
                        <%--<li class="divider"></li>--%>
                        <li id="liUsers" runat="server" visible="false">
                            <asp:HyperLink ID="HyperLink7" NavigateUrl="~/Masters/Users.aspx" runat="server"><i class="fa fa-user-circle"></i><span>Users</span></asp:HyperLink>
                        </li>
                        <li id="liReports" runat="server" visible="false">
                            <asp:HyperLink ID="HyperLink4" NavigateUrl="~/Admin/ReportMaster.aspx" runat="server"><i class="fa fa-user-circle"></i><span>Reports</span></asp:HyperLink>
                        </li>
                        <li id="liMapUserBusiness" runat="server" visible="false">
                            <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/Masters/MapUserBusiness.aspx"> <i class="fa fa-object-group"></i><span>Map Business Type</span></asp:HyperLink></li>
                         <%-- <li id="liShowReport" runat="server" visible="false">
                            <asp:HyperLink ID="HyperLink8" NavigateUrl="~/Admin/ShowReport.aspx" runat="server"><i class="fa fa-user-circle"></i><span>Show Reports</span></asp:HyperLink>
                        </li>--%>
                    </ul>
                </li>

                <li id="liUser" runat="server" visible="false" class="dropdown tasks-menu">
                    <asp:HyperLink ID="hlUsers" NavigateUrl="~/Account/Manage.aspx" runat="server">
                    <i class="fa fa-users"></i>
                    <span class="">Users</span>
                    </asp:HyperLink>
                </li>
                <!-- User Account: style can be found in dropdown.less -->
                <li class="dropdown user user-menu">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                          <asp:Image ID="imgUser" class="user-image" alt="User Image"  runat="server" />
                        <%--<asp:Image ID="imgUser" ImageUrl="~/dist/img/balaji.jpg" alt="User Image" class="user-image" runat="server" />--%>
                        <asp:LoginView runat="server" ViewStateMode="Disabled">
                            <LoggedInTemplate>
                                Hello! 
                                <span class="hidden-xs">
                                    <asp:LoginName runat="server" CssClass="username" />
                                </span>

                                <%--   <asp:LoginStatus runat="server" OnLoggingOut="Unnamed_LoggingOut" LogoutAction="Redirect" LogoutText="Log off" LogoutPageUrl="~/" />
                                --%>
                            </LoggedInTemplate>
                        </asp:LoginView>

                        <%-- <span class="hidden-xs">Alexander Pierce</span>--%>
                    </a>
                    <ul class="dropdown-menu">
                        <!-- User image -->
                        <li class="user-header">
                             <asp:Image ID="UserImage" alt="User Image"  runat="server" />
                          <%--  <asp:Image ID="Image1" ImageUrl="~/dist/img/balaji.jpg" alt="User Image" runat="server" />--%>

                            <p>
                                <asp:LoginName runat="server" CssClass="username" />
                                <small><%=DateTime.Now %></small>
                            </p>
                        </li>


                        <!-- Menu Body -->
                        <%-- <li class="user-body">
                        <div class="row">
                            <div class="col-xs-4 text-center">
                                <a href="#">Chan</a>
                            </div>
                            <div class="col-xs-4 text-center">
                                <a href="#">Sales</a>
                            </div>
                            <div class="col-xs-4 text-center">
                                <a href="#">Friends</a>
                            </div>
                        </div>
                        <!-- /.row -->
                    </li>--%>
                        <!-- Menu Footer-->
                        <li class="user-footer">
                            <div class="pull-left">
                                <asp:HyperLink ID="hlLinkProfile" NavigateUrl="~/Account/profile" CssClass="btn btn-default btn-flat" runat="server">Profile</asp:HyperLink>

                            </div>
                            <div class="pull-right">

                                <asp:LoginStatus CssClass="btn btn-default btn-flat" runat="server" OnLoggingOut="Unnamed_LoggingOut" LogoutAction="Redirect" LogoutText="Log off" LogoutPageUrl="~/" />

                            </div>
                        </li>
                    </ul>
                </li>
                <!-- Control Sidebar Toggle Button -->
                <%--  <li>
                <a href="#" data-toggle="control-sidebar"><i class="fa fa-gears"></i></a>
            </li>--%>
            </ul>
        </div>
    </div>
</nav>
