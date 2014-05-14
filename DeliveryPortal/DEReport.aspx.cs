using Common;
using DeliveryPortalDL;
using DeliveryPortalEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeliveryPortal
{
    public partial class DEReport : System.Web.UI.Page
    {
        DEReviewDL _deReviewDL = new DEReviewDL();
        ReminderServiceDL _reminderServiceDL = new ReminderServiceDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Id"] != null)
            {
                GenerateReport();
            }
        }

        private void GenerateReport()
        {
            string deReviewID = Request.QueryString["Id"];
            if (!string.IsNullOrEmpty(deReviewID))
            {
                #region Individual Delivery KPI
                int serialNo = 0;

                int? reviewID = Convert.ToInt32(deReviewID);
                List<AttributeModel> attributes = _deReviewDL.GetDEAttributes();
                List<DEAttributeModel> projectDEAttributes = _deReviewDL.GetProjectDEAttributes(reviewID);
                List<FlagModel> flags = _deReviewDL.GetAttributeFlags();

                DataTable dtDEReport = new DataTable();
                dtDEReport.Columns.Add("Sr", typeof(string));
                dtDEReport.Columns.Add("Attribute Name", typeof(string));
                dtDEReport.Columns.Add("Sample Questions", typeof(string));
                dtDEReport.Columns.Add("RAG Status", typeof(string));
                dtDEReport.Columns.Add("Reviewer Observations", typeof(string));
                dtDEReport.Columns.Add("Action Points", typeof(string));

                foreach (DEAttributeModel deAttribute in projectDEAttributes)
                {
                    DataRow dr = dtDEReport.NewRow();
                    AttributeModel attributeModel = attributes.Where(a => a.AttributeId == deAttribute.AttributeId).FirstOrDefault();
                    if (attributeModel != null)
                    {
                        dr["Sr"] = ++serialNo;
                        dr["Attribute Name"] = attributeModel.AttributeName;
                        dr["Sample Questions"] = attributeModel.SampleQuestions;
                        dr["RAG Status"] = flags.Where(f => f.FlagId == deAttribute.FlagId).FirstOrDefault() != null ? flags.Where(f => f.FlagId == deAttribute.FlagId).FirstOrDefault().FlagName.Substring(0, 1) : null;
                        dr["Reviewer Observations"] = deAttribute.Observations;
                        dr["Action Points"] = deAttribute.Recommendations;
                        dtDEReport.Rows.Add(dr);
                    }
                }
                grdDEReport.DataSource = dtDEReport;
                grdDEReport.DataBind();

                #endregion

                #region Attribute Summary
                DataRow drSummary;
                DataTable dtAttributeSummaries = new DataTable();
                dtAttributeSummaries.Columns.Add("Attribute", typeof(string));
                dtAttributeSummaries.Columns.Add("RAG", typeof(string));
                dtAttributeSummaries.Columns.Add("Description", typeof(string));

                List<AttributeModel> attributeSummaries = _deReviewDL.GetAttributeSummary();
                foreach (AttributeModel attribute in attributeSummaries.Where(a => !a.ParentAttributeId.HasValue))
                {
                    string rag = GetRAGStatus(attribute.AttributeId, projectDEAttributes, attributeSummaries, flags);
                    drSummary = dtAttributeSummaries.NewRow();
                    drSummary["Attribute"] = attribute.AttributeName;
                    drSummary["RAG"] = rag ?? "Gray";
                    drSummary["Description"] = string.Empty;

                    dtAttributeSummaries.Rows.Add(drSummary);
                }

                grdAttributeSummary.DataSource = dtAttributeSummaries;
                grdAttributeSummary.DataBind();

                #endregion
            }

        }

        private string GetRAGStatus(int attributeId, List<DEAttributeModel> projectDEAttributes, List<AttributeModel> attributeSummaries, List<FlagModel> flags)
        {
            List<int> rag = new List<int>();
            attributeSummaries.Where(a => a.ParentAttributeId == attributeId).ToList().ForEach(a => rag.Add(a.AttributeId));
            DEAttributeModel parentAttribute = projectDEAttributes.Where(a => rag.Contains(a.AttributeId)).Count() > 0 ? projectDEAttributes.Where(a => rag.Contains(a.AttributeId)).OrderBy(a => a.FlagId).First() : null;

            string ragStatus = null;
            if (parentAttribute != null && parentAttribute.FlagId.HasValue)
                ragStatus = flags.Where(f => f.FlagId == parentAttribute.FlagId).First() != null ? flags.Where(f => f.FlagId == parentAttribute.FlagId).First().FlagName : null;

            return ragStatus;
        }

        protected void grdDEReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            foreach (TableCell tc in e.Row.Cells)
            {
                tc.Attributes["style"] = "border-color: #c3cecc";
            };
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell cell = e.Row.Cells[2];
                cell.HorizontalAlign = HorizontalAlign.Center;
                if (((DataRowView)e.Row.DataItem)["RAG Status"] != DBNull.Value)
                {
                    if ((string)((DataRowView)e.Row.DataItem)["RAG Status"] == "R")
                    {
                        cell.BackColor = System.Drawing.Color.Red;
                    }
                    else if ((string)((DataRowView)e.Row.DataItem)["RAG Status"] == "A")
                    {
                        cell.BackColor = System.Drawing.Color.Orange;
                    }
                    else if ((string)((DataRowView)e.Row.DataItem)["RAG Status"] == "G")
                    {
                        cell.BackColor = System.Drawing.Color.Green;
                    }
                }
                else
                {
                    cell.BackColor = System.Drawing.Color.LightGray;
                    cell.Text = "NA";
                }
            }
        }

        protected void grdAttributeSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strRed = "<img src='Images/circle_red.png'>";
                string strGreen = "<img src='Images/circle_green.png'>";
                string strAmber = "<img src='Images/circle_amber.png'>";
                string strGrey = "<img src='Images/circle_grey.png'>";

                TableCell cell = e.Row.Cells[1];
                cell.HorizontalAlign = HorizontalAlign.Center;
                if (((DataRowView)e.Row.DataItem)["RAG"] != DBNull.Value)
                {
                    if ((string)((DataRowView)e.Row.DataItem)["RAG"] == "Red")
                    {
                        cell.Text = strRed;
                    }
                    else if ((string)((DataRowView)e.Row.DataItem)["RAG"] == "Amber")
                    {
                        cell.Text = strAmber;
                    }
                    else if ((string)((DataRowView)e.Row.DataItem)["RAG"] == "Green")
                    {
                        cell.Text = strGreen;
                    }
                    else
                    {
                        cell.Text = strGrey;
                    }
                }
                else
                {
                    cell.Text = strGrey;
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected string GetgrdAttributeSummaryData() 
        {
            StringWriter str_wrt = new StringWriter();
            HtmlTextWriter html_wrt = new HtmlTextWriter(str_wrt);
            grdAttributeSummary.RenderControl(html_wrt);
            return str_wrt.ToString();
        }
        protected string GetgrdDEReportData()
        {
            StringWriter str_wrt = new StringWriter();
            HtmlTextWriter html_wrt = new HtmlTextWriter(str_wrt);
            grdDEReport.RenderControl(html_wrt);
            return str_wrt.ToString();
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            String HTML = GetgrdAttributeSummaryData();
            HTML += GetgrdDEReportData();
            string strFuncCode = "DER"; //Hardcoded value
            
            string emailIdFrom= Convert.ToString(ConfigurationManager.AppSettings["EmailIdFrom"]);
         
            string emailIdTo = _reminderServiceDL.GetEmailIdList(strFuncCode);
            string subject = "DE Report";

            HTML= HTML.Replace("Images/circle_red.png", "cid:redIcon");
            HTML = HTML.Replace("Images/circle_amber.png", "cid:amberIcon");
            HTML = HTML.Replace("Images/circle_grey.png", "cid:greyIcon");
            HTML = HTML.Replace("Images/circle_green.png", "cid:greenIcon");
            
            AlternateView avHtml = AlternateView.CreateAlternateViewFromString(HTML, null, MediaTypeNames.Text.Html);
            
            // Create a LinkedResource object for each embedded image
            LinkedResource redIcon = new LinkedResource(Server.MapPath("~/Images/circle_red.png"), MediaTypeNames.Image.Jpeg);
            redIcon.ContentId = "redIcon";
            avHtml.LinkedResources.Add(redIcon);

            LinkedResource amberIcon = new LinkedResource(Server.MapPath("~/Images/circle_amber.png"), MediaTypeNames.Image.Jpeg);
            amberIcon.ContentId = "amberIcon";
            avHtml.LinkedResources.Add(amberIcon);

            LinkedResource greenIcon = new LinkedResource(Server.MapPath("~/Images/circle_green.png"), MediaTypeNames.Image.Jpeg);
            greenIcon.ContentId = "greenIcon";
            avHtml.LinkedResources.Add(greenIcon);

            LinkedResource greyIcon = new LinkedResource(Server.MapPath("~/Images/circle_grey.png"), MediaTypeNames.Image.Jpeg);
            greyIcon.ContentId = "greyIcon";
            avHtml.LinkedResources.Add(greyIcon);
            //string strRed = "<img src='Images/circle_red.png'>";
            //string strGreen = "<img src='Images/circle_green.png'>";
            //string strAmber = "<img src='Images/circle_amber.png'>";
            //string strGrey = "<img src='Images/circle_grey.png'>";
            


            Utilities.SendEmails(emailIdFrom, emailIdTo, subject, HTML, avHtml);
        }

        
    }
}