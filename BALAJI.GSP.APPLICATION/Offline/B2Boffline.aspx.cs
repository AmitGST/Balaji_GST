using BusinessLogic.Repositories;
using GST.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BALAJI.GSP.APPLICATION.Offline
{
    public partial class B2Boffline : System.Web.UI.Page
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               BindExcelSection();
            }

            uc_B2B_Invoices.AddMoreClick += uc_B2B_Invoices_AddMoreClick;
            uc_B2CL_GSTR1.lkbBackEvent += B2CL_GSTR1_lkbBackEvent;
            uc_Tax_Liability.AddMoreClick += uc_Tax_Liability_AddMoreClick;
            uc_Adv_Adjustment_GSTR1.lkbBackEvent += AdvanceGSTR1_lkbBackEvent;
            uc_CreditCdnr_Gstr1.lkbBackEvent += Credit_Gstr1_lkbBackEvent;
            uc_B2CL.AddMoreClick += uc_B2CL_AddMoreClick;
            uc_Adjust_Advance.AddMoreClick += uc_Adjust_Advance_AddMoreClick;
            uc_ExportsInvoices.AddMoreClick += uc_ExportsInvoices_AddMoreClick;
            uc_crdr_registered.AddMoreClick += uc_crdr_registered_AddMoreClick;
            uc_crdr_unregister.AddMoreClick += uc_crdr_unregister_AddMoreClick;
            uc_EXPORT_GSTR1.lkbBackEvent += ucExportGstr1Add_lkbBackEvent;
        }

        private void ucExportGstr1Add_lkbBackEvent(object sender, EventArgs e)
        {
            try
            {
                ucExportGstr1Add.Visible = false;
                //amit
                uc_ExportsInvoices.BindItems(Convert.ToByte(rdbGSTR.SelectedValue));
                MainContent.Visible = true;
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void Credit_Gstr1_lkbBackEvent(object sender, EventArgs e)
        {
            try
            {
                Credit_Gstr1.Visible = false;
                //amit
                uc_crdr_registered.BindItems(Convert.ToByte(rdbGSTR.SelectedValue));
                uc_crdr_unregister.BindItems(Convert.ToByte(rdbGSTR.SelectedValue));
                MainContent.Visible = true;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void AdvanceGSTR1_lkbBackEvent(object sender, EventArgs e)
        {
            try
            {
                AdvanceGSTR1.Visible = false;
                //amit
                uc_Adjust_Advance.BindItems(Convert.ToByte(rdbGSTR.SelectedValue));
                uc_Tax_Liability.BindItems(Convert.ToByte(rdbGSTR.SelectedValue));
                MainContent.Visible = true;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void B2CL_GSTR1_lkbBackEvent(object sender, EventArgs e)
        {
            try
            {
                B2CLGSTR1.Visible = false;
                //amit
                uc_B2CL.BindItems(Convert.ToByte(rdbGSTR.SelectedValue));
                uc_B2B_Invoices.BindItems(Convert.ToByte(rdbGSTR.SelectedValue));
                MainContent.Visible = true;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        private void uc_Adjust_Advance_AddMoreClick(object sender, EventArgs e)
        {
            try
            {
                AdvanceGSTR1.Visible = true;
                uc_Adv_Adjustment_GSTR1.BindItems("ATADJ");
                MainContent.Visible = false;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void uc_crdr_unregister_AddMoreClick(object sender, EventArgs e)
        {
            try
            {
                Credit_Gstr1.Visible = true;
                uc_CreditCdnr_Gstr1.InvoiceNo = uc_crdr_unregister.InvoiceNumber;
                uc_CreditCdnr_Gstr1.BindItems("CRDR Unregister");
                MainContent.Visible = false;
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void uc_Tax_Liability_AddMoreClick(object sender, EventArgs e)
        {
            try
            {
                AdvanceGSTR1.Visible = true;
                //uc_Tax_Liability.
                uc_Adv_Adjustment_GSTR1.BindItems("AT");
                MainContent.Visible = false;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void uc_crdr_registered_AddMoreClick(object sender, EventArgs e)
        {
            try
            {
                Credit_Gstr1.Visible = true;
                uc_CreditCdnr_Gstr1.InvoiceNo = uc_crdr_registered.InvoiceNumber;
                uc_CreditCdnr_Gstr1.BindItems("CRDR Register");
                MainContent.Visible = false;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void uc_ExportsInvoices_AddMoreClick(object sender, EventArgs e)
        {
            try
            {
                ucExportGstr1Add.Visible = true;
                //uc_EXPORT_GSTR1.InvoiceNo = uc_ExportsInvoices.InvoiceNumber;
                uc_EXPORT_GSTR1.BindItems("Export");
                MainContent.Visible = false;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void uc_B2CL_AddMoreClick(object sender, EventArgs e)
        {
            try
            {
                B2CLGSTR1.Visible = true;
                uc_B2CL_GSTR1.InvoiceNo = uc_B2CL.InvoiceNumber;
                uc_B2CL_GSTR1.BindItems("B2CL");
                MainContent.Visible = false;
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        private void uc_B2B_Invoices_AddMoreClick(object sender, EventArgs e)
        {
            try
            {
                MainContent.Visible = false;
                uc_B2CL_GSTR1.InvoiceNo = uc_B2B_Invoices.InvoiceNumber;
                uc_B2CL_GSTR1.BindItems("B2B");
                B2CLGSTR1.Visible = true;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
            // DropDownList c = sender as DropDownList;
            //if(c!=null)
            //{
                //DropDownList ddlSection =ddlSectionion");
                 //uc_B2CL_GSTR1.SectionName=ddlSection.SelectedItem.Text;
                 //TextBox txtInvoiceNo = Page.FindControl("txtInvoiceNo") as TextBox;
                 //uc_B2CL_GSTR1.InvoiceNo = txtInvoiceNo.Text;
            //}
            //TextBox b = sender as TextBox;
            //if (b != null)
            //{
            //    TextBox txtInvoiceNo = (TextBox)b.Parent.FindControl("txtInvoiceNo");
                
               
            //}
        }

        public void BindExcelSection()
        {
            try
            {
                var userid = Common.LoggedInUserID();
                ddlSection.DataSource = unitOfWork.OfflinesectionRepository.Filter(f => f.ActiveStatus == true && f.Type == 1).OrderByDescending(o => o.CreatedDate).Select(s => new { Section_ID = s.Section_ID, Descriptions = s.Description }).ToList();
                ddlSection.DataValueField = "Section_ID";
                ddlSection.DataTextField = "Descriptions";
                ddlSection.DataBind();
                ddlSection.Items.Insert(0, new ListItem(" [ SELECT ] ", "0"));
            }
            catch(Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }

        }

        //public void OpenRateList()
        //{
        //    UserControl con = (UserControl)LoadControl(" ~/UserControl/ButtonClick.ascx");
        //    con.ID = "aaa";
        //    this.Controls.Add(con);
        //}

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            //for gstr1
            try
            {
                ucb2binvGSTR1.Visible = false;
                ucB2CLGSTR1.Visible = false;
                ucB2CSGSTR1.Visible = false;
                uccrdrregGSTR1.Visible = false;
                uccrdrGSTR1.Visible = false;
                ucexportGSTR1.Visible = false;
                uctaxliaGSTR1.Visible = false;
                ucadjustadvGSTR1.Visible = false;
                uchsnGSTR1.Visible = false;
                //for gstr2
                ucAdvADJGSTR2.Visible = false;
                ucAdvGSTR2.Visible = false;
                ucB2BGSTR2.Visible = false;
                ucB2BURGSTR2.Visible = false;
                ucCRDRGSTR2.Visible = false;
                ucCRDRunRegGSTR2.Visible = false;
                ucImportGoodsGSTR2.Visible = false;
                ucImportServicesGSTR2.Visible = false;
                ucHsnGstr2.Visible = false;
                // ucInwardGSTR2.Visible = false;
                //ucITCReversalGSTR2.Visible = false;
                ucNillRatedGSTR2.Visible = false;
                ucTaxLiaGSTR2.Visible = false;

                if (rdbGSTR.SelectedValue == "0")
                {
                    if (ddlSection.SelectedIndex == 1)
                    {

                       
                            uc_B2B_Invoices.BindItems(Convert.ToByte(rdbGSTR.SelectedValue));
                            ucb2binvGSTR1.Visible = true; //vivek
                            // taxLiability.Visible = false;
                    }
                    else if (ddlSection.SelectedIndex == 2)
                    {
                        uc_B2CL.BindItems(Convert.ToByte(rdbGSTR.SelectedValue));
                        ucB2CLGSTR1.Visible = true; //vivek
                    }
                    else if (ddlSection.SelectedIndex == 3)
                    {
                        uc_B2CS.BindItems(Convert.ToByte(rdbGSTR.SelectedValue));
                        ucB2CSGSTR1.Visible = true;  //vivek
                    }
                    else if (ddlSection.SelectedIndex == 4)
                    {
                        uc_crdr_registered.BindItems(Convert.ToByte(rdbGSTR.SelectedValue));
                        uccrdrregGSTR1.Visible = true;  //vivek
                    }
                    else if (ddlSection.SelectedIndex == 5)
                    {
                        uc_crdr_unregister.BindItems(Convert.ToByte(rdbGSTR.SelectedValue));
                        uccrdrGSTR1.Visible = true;  //vivek
                    }
                    else if (ddlSection.SelectedIndex == 6)
                    {
                        uc_ExportsInvoices.BindItems(Convert.ToByte(rdbGSTR.SelectedValue));
                        ucexportGSTR1.Visible = true;
                    }
                    else if (ddlSection.SelectedIndex == 7)
                    {
                        uc_Tax_Liability.BindItems(Convert.ToByte(rdbGSTR.SelectedValue));
                        uctaxliaGSTR1.Visible = true;
                    }
                    else if (ddlSection.SelectedIndex == 8)
                    {
                        uc_Adjust_Advance.BindItems(Convert.ToByte(rdbGSTR.SelectedValue));
                        ucadjustadvGSTR1.Visible = true;
                    }
                    else if (ddlSection.SelectedIndex == 9)
                    {
                        uc_hsn.BindItems(Convert.ToByte(rdbGSTR.SelectedValue));
                        uchsnGSTR1.Visible = true;
                    }
                }

                //gstr2

                if (rdbGSTR.SelectedValue == "2")
                {
                    if (ddlSection.SelectedIndex == 1)
                    {
                        uc_B2B_invoice_GSTR2.BindItems();
                        ucB2BGSTR2.Visible = true;
                        // taxLiability.Visible = false;
                    }
                    else if (ddlSection.SelectedIndex == 2)
                    {
                        uc_B2BUR_Invoice_GSTR2.BindItems();
                        ucB2BURGSTR2.Visible = true;
                    }
                    else if (ddlSection.SelectedIndex == 3)
                    {
                        uc_crdr_GSTR2.BindItems();
                        ucCRDRGSTR2.Visible = true;
                    }
                    else if (ddlSection.SelectedIndex == 4)
                    {
                        uc_crdr_unregister.BindItems(Convert.ToByte(rdbGSTR.SelectedValue));
                        ucCRDRunRegGSTR2.Visible = true;
                    }
                    else if (ddlSection.SelectedIndex == 5)
                    {
                        uc_ImportGoods_GSTR2.BindItems();
                        ucImportGoodsGSTR2.Visible = true;
                    }
                    else if (ddlSection.SelectedIndex == 6)
                    {
                        uc_ImportServices_GSTR2.BindItems();
                        ucImportServicesGSTR2.Visible = true;
                    }
                    else if (ddlSection.SelectedIndex == 7)
                    {

                        ucTaxLiaGSTR2.Visible = true;
                    }
                    else if (ddlSection.SelectedIndex == 8)
                    {
                        uc_hsn_GSTR2.BindItems();
                        ucHsnGstr2.Visible = true;
                    }
                    //else if (ddlSection.SelectedIndex == 8)
                    //{
                    //    ucInwardGSTR2.Visible = true;
                    //}
                    //else if(ddlSection.SelectedIndex==9)
                    //{
                    //    ucITCReversal.Visible = true;
                    //}
                    else if (ddlSection.SelectedIndex == 10)
                    {

                        ucNillRatedGSTR2.Visible = true;
                    }
                    else if (ddlSection.SelectedIndex == 11)
                    {
                        ucTaxLiaGSTR2.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }

        protected void rdbGSTR_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdbGSTR.SelectedValue == "GSTR-1")
                {
                    ddlSection.DataSource = unitOfWork.OfflinesectionRepository.Filter(f => f.ActiveStatus == true && f.Type == 1).OrderByDescending(o => o.CreatedDate).Select(s => new { Section_ID = s.Section_ID, Descriptions = s.Description }).ToList();
                    ddlSection.DataValueField = "Section_ID";
                    ddlSection.DataTextField = "Descriptions";
                    ddlSection.DataBind();
                    ddlSection.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
                }
                else if (rdbGSTR.SelectedValue == "GSTR-2")
                {
                    ddlSection.DataSource = unitOfWork.OfflinesectionRepository.Filter(f => f.ActiveStatus == true && f.Type == 2).OrderByDescending(o => o.CreatedDate).Select(s => new { Section_ID = s.Section_ID, Descriptions = s.Description }).ToList();
                    ddlSection.DataValueField = "Section_ID";
                    ddlSection.DataTextField = "Descriptions";
                    ddlSection.DataBind();
                    ddlSection.Items.Insert(0, new ListItem(" [ Select ] ", "0"));
                }
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
      
        protected void lkbViewSummary_Click(object sender, EventArgs e)
        {
            try
            {
                MainContent.Visible = false;
                SecondaryContent.Visible = true;
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
        //public EventHandler lkbMainBack;
        protected void lkbMainBack_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/User/Trans/Offline.aspx");
            }
            catch (Exception ex)
            {
                cls_ErrorLog ob = new cls_ErrorLog();
                cls_ErrorLog.LogError(ex, Common.LoggedInUserID());
            }
        }
       
    }
}