<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.WebForm1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

        </div>
      <%--  <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            <LocalReport ReportEmbeddedResource="BALAJI.GSP.APPLICATION.Report.Invoice.InvoiceGenerate.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="dsInvoiceReport" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="rpt_Invoice_DataSetTableAdapters.RPT_INVOICE_GENERATETableAdapter"></asp:ObjectDataSource>--%>

        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="22%"
            Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)"
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" PageCountMode="Actual" ShowBackButton="False" ShowFindControls="False" ShowPageNavigationControls="False" ShowZoomControl="False" SizeToReportContent="True">
            <LocalReport ReportPath="Report/Invoice/Invoice.rdlc" ReportEmbeddedResource="Report/Invoice/Invoice.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>

        <rsweb:ReportViewer ID="ReportViewer2" runat="server">
        </rsweb:ReportViewer>

        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
            SelectMethod="GetData" TypeName="BALAJI.GSP.APPLICATION.BALAJI_GST_DBDataSetTableAdapters.RPT_INVOICE_GENERATETableAdapter" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    </form>
</body>
</html>
