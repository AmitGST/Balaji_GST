<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_LeftMenu.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.uc_LeftMenu" %>
<div class="main-sidebar">
    <!-- sidebar: style can be found in sidebar.less -->

    <div class="sidebar">
        <!-- Sidebar user panel -->
        <div class="user-panel">
            <div class="pull-left image">
                <asp:Image ID="uc_Header_Image1" runat="server" />
                <%--<asp:Image ID="uc_Header_Image1" ImageUrl="~/dist/img/balaji.jpg" runat="server" />--%>
            </div>
            <div class="pull-left info">
                <asp:LoginView runat="server" ViewStateMode="Disabled">
                    <LoggedInTemplate>
                        <%-- <span class="hidden-xs">--%>
                        <span>
                            <asp:LoginName runat="server" CssClass="username" />
                        </span>
                        <a href="#">
                    </LoggedInTemplate>
                </asp:LoginView>
                <div style="font-size: 11px; margin-top: 10px;"><a href="#"><i class="fa fa-circle text-green"></i>&nbsp;&nbsp;Online</a></div>
            </div>
        </div>
        <!-- search form -->
        <div class="sidebar-form">
            <div class="input-group">
                <input type="text" name="q" class="form-control" placeholder="Search...">
                <span class="input-group-btn">
                    <button type="submit" name="search" id="search-btn" class="btn btn-flat">
                        <i class="fa fa-search"></i>
                    </button>
                </span>
            </div>
        </div>
        <!-- /.search form -->
        <!-- sidebar menu: : style can be found in sidebar.less -->
        <ul class="sidebar-menu">

            <li class="header">
                <i class="fa fa-home"></i><span>MAIN NAVIGATION</span>
            </li>


            <li id="liInvoice" runat="server" visible="false" class="treeview">
                <asp:HyperLink ID="hl" NavigateUrl="#" runat="server">
               
                    <i class="fa fa-handshake-o"></i><span>INVOICE</span>
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                </asp:HyperLink>

                <ul class=" active treeview-menu">
                    <%--<li><a NavigateUrl="#"><i class="fa fa-circle-o"></i>ADD</a></li>--%>
                    <li>
                        <asp:HyperLink NavigateUrl="~/User/uinvoice/GSTinvoice.aspx" InvoiceType="B2BInvoice" ID="hlB2BInvoice" runat="server"><i class="fa fa-circle-o text-green"></i>ADD</asp:HyperLink></li>
                    <li>
                        <asp:HyperLink NavigateUrl="~/Offline/Auditrail.aspx" runat="server"><i class="fa fa-circle-o text-aqua"></i>OFFLINE</asp:HyperLink></li>
                    <li>
                        <asp:HyperLink NavigateUrl="~/User/uinvoice/ViewInvoice.aspx" runat="server"><i class="fa fa-circle-o text-orange"></i>VIEW / UPLOAD</asp:HyperLink></li>

                    <%--  <ul class="treeview-menu">--%>
                    <%--<asp:HyperLink ID="HyperLink1" runat="server">HyperLink</asp:HyperLink>--%>

                    <%-- <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>MODIFY</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="~/User/uinvoice/ViewInvoice.aspx" runat="server"><i class="fa fa-circle-o"></i>VIEW</asp:HyperLink></li>--%>
                    <%--<li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREDIT NOTES 
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                                </asp:HyperLink>
                                <ul class="treeview-menu">
                                    <li>
                                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                                    <li>
                                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>
                                    <li>
                                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED CREDIT INVOICE"></i>MODIFY</asp:HyperLink></li>
                                </ul>
                            </li>

                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>DEBIT NOTES 
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                                </asp:HyperLink>
                                <ul class="treeview-menu">
                                    <li>
                                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                                    <li>
                                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>
                                    <li>
                                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED DEBIT INVOICE"></i>MODIFY</asp:HyperLink></li>
                                </ul>
                            </li>--%>
                    <%-- <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>EXPORT INVOICE 
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                                </asp:HyperLink>
                                <ul class="treeview-menu">
                                    <li>
                                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                                    <li>
                                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>
                                    <li>
                                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" ondragover="AMENDED EXPORT INVOICE"></i>MODIFY</asp:HyperLink></li>
                                </ul>
                            </li>--%>
                    <%--</ul>--%>

                    <%-- <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o text-aqua"></i>B2C 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>ADD</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED EXPORT INVOICE"></i>MODIFY</asp:HyperLink></li>
                        </ul>
                    </li>--%>
                </ul>



                <%-- --%>
                <%-- <asp:HyperLink NavigateUrl="#" runat="server">
                    <i class="fa fa-handshake-o"></i><span>B2C</span>
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                </asp:HyperLink>--%>

            </li>


            <%-- <li class="header">RETURN SECTION</li>
            <li class="active treeview"></li>--%>

            <li class="treeview">
                <asp:HyperLink NavigateUrl="#" runat="server">
                    <i class="fa fa-share"></i><span>RETURN</span>
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                </asp:HyperLink>


                <ul class="treeview-menu">
                    <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>GSTR1 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <%-- <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>View Invoice</asp:HyperLink></li>--%>
                            <li>
                                <asp:HyperLink NavigateUrl="~/User/ureturn/GSTR1PreviewB2B.aspx" runat="server"><i class="fa fa-circle-o"></i>View GSTR-1</asp:HyperLink></li>
                            <%-- <li>
                                <asp:HyperLink NavigateUrl="" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>--%>
                        </ul>
                    </li>
                    <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>GSTR2A 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="~/User/ureturn/GSTR2A.aspx" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>View Invoice</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="~/User/ureturn/GSTR2APreviewB2B.aspx" runat="server"><i class="fa fa-circle-o"></i>View GSTR-2A</asp:HyperLink></li>
                            <%-- <li>
                                <asp:HyperLink NavigateUrl="" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>--%>
                        </ul>
                    </li>
                    <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>GSTR2 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="~/User/ureturn/GSTR2.aspx" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>View Invoice</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="~/User/ureturn/GSTR2PreviewB2B.aspx" runat="server"><i class="fa fa-circle-o"></i>File GSTR-2</asp:HyperLink></li>
                            <%--  <li>
                                <asp:HyperLink NavigateUrl="" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>--%>
                        </ul>
                    </li>
                    <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>GSTR1A 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">

                            <%--  <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>--%>
                            <li>
                                <asp:HyperLink NavigateUrl="~/User/ureturn/GSTR1A.aspx" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>View Invoice</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="~/User/ureturn/GSTR1APreviewB2B.aspx" runat="server"><i class="fa fa-circle-o"></i>File GSTR-1A</asp:HyperLink></li>
                        </ul>

                    </li>
                    <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>GSTR3 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <%--<li>
                                <asp:HyperLink NavigateUrl="~/User/ureturn/GSTR3.aspx" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>View Invoice</asp:HyperLink></li>--%>
                            <li>
                                <asp:HyperLink NavigateUrl="~/User/ureturn/GSTR3PreviewB2B.aspx" runat="server"><i class="fa fa-circle-o"></i>View GSTR-3</asp:HyperLink></li>

                            <%-- <li>
                                <asp:HyperLink NavigateUrl="" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>--%>
                        </ul>
                    </li>
                    <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>GSTR3B 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <%--<li>
                                <asp:HyperLink NavigateUrl="~/User/ureturn/GSTR3.aspx" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>View Invoice</asp:HyperLink></li>--%>

                            <li>
                                <%--<asp:HyperLink NavigateUrl="~/User/ureturn/Gstr3B-Preview.aspx" runat="server"><i class="fa fa-circle-o"></i>View GSTR-3B</asp:HyperLink></li>--%>
                            <li>
                                <asp:HyperLink NavigateUrl="~/User/ureturn/GSTR3BPreview.aspx" runat="server"><i class="fa fa-circle-o"></i>View Gstr3B</asp:HyperLink></li>
                        </ul>
                    </li>
                    <li>
                        <asp:HyperLink NavigateUrl="~/User/ureturn/GSTR4.aspx" runat="server"><i class="fa fa-circle-o"></i>GSTR4 
               <%-- <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>--%>
                        </asp:HyperLink>
                        <%--  <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="~/User/Return/GSTR4.aspx" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>MODIFY</asp:HyperLink></li>
                        </ul>--%>
                    </li>
                    <%-- <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>GSTR4A 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="~/User/Return/GSTR4A.aspx" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>MODIFY</asp:HyperLink></li>
                        </ul>
                    </li>--%>
                    <%-- <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>GSTR5 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="~/User/Return/GSTR5.aspx" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>MODIFY</asp:HyperLink></li>
                        </ul>
                    </li>
                    <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>GSTR6 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="~/User/Return/GSTR6.aspx" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>MODIFY</asp:HyperLink></li>
                        </ul>
                    </li>--%>
                    <li>
                        <asp:HyperLink NavigateUrl="~/User/ureturn/GSTR6.aspx" runat="server"><i class="fa fa-circle-o"></i>GSTR6 
               <%-- <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>--%>
                        </asp:HyperLink>
                        <%--<ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="~/User/Return/GSTR6A.aspx" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>MODIFY</asp:HyperLink></li>
                        </ul>--%>
                    </li>
                    <li>
                        <asp:HyperLink NavigateUrl="~/User/ureturn/GSTR7.aspx" runat="server"><i class="fa fa-circle-o"></i>GSTR7 
               <%-- <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>--%>
                        </asp:HyperLink>
                        <%-- <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>MODIFY</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="~/User/Return/GSTR7.aspx" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>
                        </ul>--%>
                    </li>
                    <%--<li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>GSTR7A 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>MODIFY</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>
                        </ul>
                    </li>--%>
                    <li>
                        <asp:HyperLink NavigateUrl="~/User/ureturn/GSTR8.aspx" runat="server"><i class="fa fa-circle-o"></i>GSTR8 
                <%--<span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>--%>
                        </asp:HyperLink>
                        <%--<ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>MODIFY</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>
                        </ul>--%>
                    </li>
                    <%--<li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>GSTR9 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>MODIFY</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>
                        </ul>
                    </li>--%>
                    <%-- <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>GSTR9A 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>MODIFY</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>
                        </ul>
                    </li>
                    <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>GSTR9B 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>MODIFY</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>
                        </ul>
                    </li>
                    <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>GSTR10 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>MODIFY</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>
                        </ul>
                    </li>
                    <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>GSTR11 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>MODIFY</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>
                        </ul>
                    </li>
                    <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>GSTR ITC 1 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>MODIFY</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>
                        </ul>
                    </li>
                    <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>GSTR TRP 1 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>MODIFY</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>
                        </ul>
                    </li>
                    <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>GSTR TRP 2 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>MODIFY</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>
                        </ul>
                    </li>
                    <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>GSTR TRP 3
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>MODIFY</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>
                        </ul>
                    </li>
                   <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>GSTR TRP 4 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o" onmouseover="AMENDED INVOICE"></i>MODIFY</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>UPLOAD</asp:HyperLink></li>
                        </ul>
                    </li>--%>--%>
                </ul>
            </li>
            <%-- <li class="header">LEDGERS SECTION</li>
            <li class="active treeview"></li>--%>

            <li class="treeview">
                <asp:HyperLink NavigateUrl="#" runat="server">
                    <i class="fa fa-book"></i><span>LEDGERS</span>
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                </asp:HyperLink>
                <ul class="treeview-menu">
                    <%-- <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CASH LEDGERS 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                        </ul>
                    </li>--%>
                    <li>
                        <asp:HyperLink NavigateUrl="~/User/Trans/ITC.aspx" runat="server"><i class="fa fa-circle-o"></i>ITC LEDGERS 
                <span class="pull-right-container">
                   <%-- <i class="fa fa-angle-left pull-right"></i>--%>
                </span>
                        </asp:HyperLink>
                    </li>
                    <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREDIT / DEBIT NOTES 
                     <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                         </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="~/User/Trans/Note.aspx" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                            <li>
                                <asp:HyperLink NavigateUrl="~/User/Trans/ViewNote.aspx" runat="server"><i class="fa fa-circle-o"></i>VIEW</asp:HyperLink></li>
                        </ul>
                    </li>
                    <%-- <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>TAX LEDGERS 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                        </ul>
                    </li>--%>
                    <%-- <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>UTILIZE ITC/CASH 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                        </ul>
                    </li>--%>
                    <%-- <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>PRE INVOIVE TAX IDs 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                        </ul>
                    </li>--%>
                    <%--  <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>PARTIAL ITC INVOICES
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE</asp:HyperLink></li>
                        </ul>
                    </li>--%>
                </ul>
            </li>
            <%-- <li class="header">PAYMENTS SECTION</li>
            <li class="active treeview"></li>--%>

            <li class="treeview">
                <asp:HyperLink NavigateUrl="#" runat="server">
                    <i class="fa fa-credit-card"></i><span>PAYMENTS</span>
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                </asp:HyperLink>
                <ul class="treeview-menu">
                    <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CHALLAN 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CREATE CHALLAN</asp:HyperLink></li>
                        </ul>
                    </li>
                    <li>
                        <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>CHALLAN STATUS 
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </asp:HyperLink>
                        <ul class="treeview-menu">
                            <li>
                                <asp:HyperLink NavigateUrl="#" runat="server"><i class="fa fa-circle-o"></i>TRACK PAYMANT STATUS</asp:HyperLink></li>
                        </ul>
                    </li>
                </ul>
            </li>

            <%--amitsnew--%>
            <li class="treeview">
                <asp:HyperLink NavigateUrl="#" runat="server">
                    <i class="fa fa-credit-card"></i><span>File Returns</span>
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                </asp:HyperLink>
                <ul class="treeview-menu">
                    <li>
                        <asp:HyperLink NavigateUrl="~/User/ureturn/ReturnGstr1.aspx" runat="server"><i class="fa fa-circle-o"></i>File Returns Details 
                <%--<span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>--%>
                        </asp:HyperLink>
                      
            </li>
        </ul>
        </li>
            <%--<li><a NavigateUrl="../Account/profile.aspx">
                <i class="fa fa-user-plus"></i><span>User Profile</span>
                <span class="pull-right-container">
                    <small class="label pull-right bg-green">new</small>
                </span>
            </a>
            </li>--%>


        <%--  <li class="header">USER MANAGEMENT</li>
            <li class="active treeview"></li>

            <li class="treeview">
                <asp:HyperLink ID="hlUsers" NavigateUrl="~/Account/Manage" runat="server">
                    <i class="fa fa-user"></i><span>Users</span>
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                </asp:HyperLink>
            </li>--%>

        <%--USER SERVICES--%>
        <%--  <li class="header">USER SERVICES</li>
            <li class="active treeview"></li>

            <li class="treeview">
                <a NavigateUrl="#">
                    <i class="fa fa-share"></i><span>TAX PROFESSIONAL</span>
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                </a>
                <ul class="treeview-menu">
                    <li>
                        <a NavigateUrl="#"><i class="fa fa-circle-o"></i>ENGAGE/DIS-ENGAGE  
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </a>
                        <ul class="treeview-menu">
                            <li><a NavigateUrl="#"><i class="fa fa-circle-o"></i>CREATE ENGAGE/DIS-ENGAGE </a></li>
                        </ul>
                    </li>
                     <li>
                        <a NavigateUrl="#"><i class="fa fa-circle-o"></i>SEARCH TAX PAYER  
                <span class="pull-right-container">
                    <i class="fa fa-angle-left pull-right"></i>
                </span>
                        </a>
                        <ul class="treeview-menu">
                            <li><a NavigateUrl="#"><i class="fa fa-circle-o"></i>SEARCH</a></li>
                        </ul>
                    </li>
                </ul>
            </li>--%>



        <%--
            <li class="treeview">
                <a NavigateUrl="#">
                    <i class="fa fa-files-o"></i>
                    <span>Layout Options</span>
                    <span class="pull-right-container">
                        <span class="label label-primary pull-right">4</span>
                    </span>
                </a>
                <ul class="treeview-menu">
                    <li><a NavigateUrl="pages/layout/top-nav.html"><i class="fa fa-circle-o"></i>Top Navigation</a></li>
                    <li><a NavigateUrl="pages/layout/boxed.html"><i class="fa fa-circle-o"></i>Boxed</a></li>
                    <li><a NavigateUrl="pages/layout/fixed.html"><i class="fa fa-circle-o"></i>Fixed</a></li>
                    <li><a NavigateUrl="pages/layout/collapsed-sidebar.html"><i class="fa fa-circle-o"></i>Collapsed Sidebar</a></li>
                </ul>
            </li>
        --%>


        <%--
            <li class="treeview">
                <a NavigateUrl="#">
                    <i class="fa fa-pie-chart"></i>
                    <span>Charts</span>
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                </a>
                <ul class="treeview-menu">
                    <li><a NavigateUrl="pages/charts/chartjs.html"><i class="fa fa-circle-o"></i>ChartJS</a></li>
                    <li><a NavigateUrl="pages/charts/morris.html"><i class="fa fa-circle-o"></i>Morris</a></li>
                    <li><a NavigateUrl="pages/charts/flot.html"><i class="fa fa-circle-o"></i>Flot</a></li>
                    <li><a NavigateUrl="pages/charts/inline.html"><i class="fa fa-circle-o"></i>Inline charts</a></li>
                </ul>
            </li>
            <%--<li class="treeview">
                <a NavigateUrl="#">
                    <i class="fa fa-laptop"></i>
                    <span>UI Elements</span>
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                </a>
                <ul class="treeview-menu">
                    <li><a NavigateUrl="pages/UI/general.html"><i class="fa fa-circle-o"></i>General</a></li>
                    <li><a NavigateUrl="pages/UI/icons.html"><i class="fa fa-circle-o"></i>Icons</a></li>
                    <li><a NavigateUrl="pages/UI/buttons.html"><i class="fa fa-circle-o"></i>Buttons</a></li>
                    <li><a NavigateUrl="pages/UI/sliders.html"><i class="fa fa-circle-o"></i>Sliders</a></li>
                    <li><a NavigateUrl="pages/UI/timeline.html"><i class="fa fa-circle-o"></i>Timeline</a></li>
                    <li><a NavigateUrl="pages/UI/modals.html"><i class="fa fa-circle-o"></i>Modals</a></li>
                </ul>
            </li>
            <li class="treeview">
                <a NavigateUrl="#">
                    <i class="fa fa-edit"></i><span>Forms</span>
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                </a>
                <ul class="treeview-menu">
                    <li><a NavigateUrl="pages/forms/general.html"><i class="fa fa-circle-o"></i>General Elements</a></li>
                    <li><a NavigateUrl="pages/forms/advanced.html"><i class="fa fa-circle-o"></i>Advanced Elements</a></li>
                    <li><a NavigateUrl="pages/forms/editors.html"><i class="fa fa-circle-o"></i>Editors</a></li>
                </ul>
            </li>
            <li class="treeview">
                <a NavigateUrl="#">
                    <i class="fa fa-table"></i><span>Tables</span>
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                </a>
                <ul class="treeview-menu">
                    <li><a NavigateUrl="pages/tables/simple.html"><i class="fa fa-circle-o"></i>Simple tables</a></li>
                    <li><a NavigateUrl="pages/tables/data.html"><i class="fa fa-circle-o"></i>Data tables</a></li>
                </ul>
            </li>
            <li>
                <a NavigateUrl="pages/calendar.html">
                    <i class="fa fa-calendar"></i><span>Calendar</span>
                    <span class="pull-right-container">
                        <small class="label pull-right bg-red">3</small>
                        <small class="label pull-right bg-blue">17</small>
                    </span>
                </a>
            </li>
            <li>
                <a NavigateUrl="pages/mailbox/mailbox.html">
                    <i class="fa fa-envelope"></i><span>Mailbox</span>
                    <span class="pull-right-container">
                        <small class="label pull-right bg-yellow">12</small>
                        <small class="label pull-right bg-green">16</small>
                        <small class="label pull-right bg-red">5</small>
                    </span>
                </a>
            </li>
            <li class="treeview">
                <a NavigateUrl="#">
                    <i class="fa fa-folder"></i><span>Examples</span>
                    <span class="pull-right-container">
                        <i class="fa fa-angle-left pull-right"></i>
                    </span>
                </a>
                <ul class="treeview-menu">
                    <li><a NavigateUrl="pages/examples/invoice.html"><i class="fa fa-circle-o"></i>Invoice</a></li>
                    <li><a NavigateUrl="pages/examples/profile.html"><i class="fa fa-circle-o"></i>Profile</a></li>
                    <li><a NavigateUrl="pages/examples/login.html"><i class="fa fa-circle-o"></i>Login</a></li>
                    <li><a NavigateUrl="pages/examples/register.html"><i class="fa fa-circle-o"></i>Register</a></li>
                    <li><a NavigateUrl="pages/examples/lockscreen.html"><i class="fa fa-circle-o"></i>Lockscreen</a></li>
                    <li><a NavigateUrl="pages/examples/404.html"><i class="fa fa-circle-o"></i>404 Error</a></li>
                    <li><a NavigateUrl="pages/examples/500.html"><i class="fa fa-circle-o"></i>500 Error</a></li>
                    <li><a NavigateUrl="pages/examples/blank.html"><i class="fa fa-circle-o"></i>Blank Page</a></li>
                    <li><a NavigateUrl="pages/examples/pace.html"><i class="fa fa-circle-o"></i>Pace Page</a></li>
                </ul>
            </li>

            <li><a NavigateUrl="documentation/index.html"><i class="fa fa-book"></i><span>Documentation</span></a></li>
            <li class="header">LABELS</li>
            <li><a NavigateUrl="#"><i class="fa fa-circle-o text-red"></i><span>Important</span></a></li>
            <li><a NavigateUrl="#"><i class="fa fa-circle-o text-yellow"></i><span>Warning</span></a></li>
            <li><a NavigateUrl="#"><i class="fa fa-circle-o text-aqua"></i><span>Information</span></a></li>--%>
        </ul>
    </div>
    <!-- /.sidebar -->
</div>
