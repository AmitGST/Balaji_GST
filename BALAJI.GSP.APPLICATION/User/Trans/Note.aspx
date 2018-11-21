<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeBehind="Note.aspx.cs" Inherits="BALAJI.GSP.APPLICATION.User.Trans.Note" %>

<%@ Register Src="~/UC/uc_sucess.ascx" TagPrefix="uc1" TagName="uc_sucess" %>


<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-header">
        <h1>
          Credit / Debit Notes  
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">LEDGRES</a></li>
            <li class="#">CREDIT / DEBIT NOTES</li>
              <li class="active">CREATE</li>
        </ol>
        
    </div>
    <div class="content">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Create</h3>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Invoice Number</label>
                            <asp:DropDownList ID="ddl_InvoiceID" CssClass="form-control" OnSelectedIndexChanged="ddl_InvoiceID_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                     <div class="col-md-3">
                        <div class="form-group">
                            <label>Credit / Debit Note</label> 
                    <asp:DropDownList ID="ddlNoteType" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                         </div>
                    <div class="col-md-6"></div>
                    </div>
                <div class="row">
                     <div class="col-md-6">
                        <div class="form-group">
                            <label>Reason For Issuing Dr./Cr. Notes </label> 
                            <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6"></div>
                </div>
                <div class="table-responsive">
                <asp:GridView ID="gvItems" DataKeyNames="InvoiceDataID" AllowPaging="false" CssClass="table table-responsive no-padding table-striped table-bordered" runat="server" Width="100%" UseAccessibleHeader="true" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="#" Visible="false">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HSN/SAC" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="txtGoodService" Text='<%#DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.ITEMCODE") %>' MaxLength="8" CssClass="form-control input-sm" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:TextBox ID="txtGoodServiceDesciption" Text='<%#DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.Description") %>' CssClass="form-control input-sm" ReadOnly="true" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty." ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="txtQty" MaxLength="6" Text='<%#DataBinder.Eval(Container.DataItem,"Qty") %>' autocomplete="off" CssClass="form-control input-sm" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="txtUnit" autocomplete="off" Text='<%#DataBinder.Eval(Container.DataItem,"GST_MST_ITEM.Unit") %>' CssClass="form-control input-sm" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate(per item)" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="txtRate" CssClass="form-control input-sm" Text='<%#DataBinder.Eval(Container.DataItem,"Rate") %>' MaxLength="10" autocomplete="off" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total(<i class='fa fa-inr' aria-hidden='true'></i>)" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="txtTotal" autocomplete="off" Text='<%#DataBinder.Eval(Container.DataItem,"TotalAmount") %>' CssClass="form-control input-sm" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Discount(<i class='fa fa-percent' aria-hidden='true' style='font-size:10px;' ></i>)" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="txtDiscount" Text='<%#DataBinder.Eval(Container.DataItem,"Discount") %>' MaxLength="5" CssClass="form-control input-sm" autocomplete="off" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="Taxable Val.(<i class='fa fa-inr' aria-hidden='true'></i>)">
                            <ItemTemplate>
                                <asp:Label ID="txtTaxableValue" Text='<%#DataBinder.Eval(Container.DataItem,"TaxableAmount") %>' CssClass="form-control input-sm" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </div>
            </div>
            <div class="box-footer">
                <uc1:uc_sucess runat="server" ID="uc_sucess" />
                <asp:LinkButton ID="lkbSubmitItems" runat="server" OnClick="lkbSubmitItems_Click" CssClass="btn btn-success"><i class="fa fa-save"></i>&nbsp;Submit</asp:LinkButton>
            </div>
          

            
        </div>
    </div>

</asp:Content>
