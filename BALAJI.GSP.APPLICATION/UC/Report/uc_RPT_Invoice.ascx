<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_RPT_Invoice.ascx.cs" Inherits="BALAJI.GSP.APPLICATION.UC.Report.uc_RPT_Invoice" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
 <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="22%"
            Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)"
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" PageCountMode="Actual" ShowBackButton="False" ShowFindControls="False" ShowPageNavigationControls="False" ShowZoomControl="False" SizeToReportContent="True" AsyncRendering="False">
            <LocalReport ReportPath="Report/Invoice/Invoice.rdlc" ReportEmbeddedResource="Report/Invoice/Invoice.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>

        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
            SelectMethod="GetData" TypeName="BALAJI.GSP.APPLICATION.BALAJI_GST_DBDataSetTableAdapters.RPT_INVOICE_GENERATETableAdapter" OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
  