using DeliveryPortalDL;
using DeliveryPortalEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeliveryPortal
{
    public partial class DEUpdates : System.Web.UI.Page
    {
        DEReviewDL _deReviewDL = new DEReviewDL();
        ProjectDL _projectDL = new ProjectDL();
        WeeklyStatusDL _weeklyStatusDL = new WeeklyStatusDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["Id"] != null)
                    {

                        PopulateDEReviewDetails(int.Parse(Request.QueryString["Id"]));
                        if (Request.QueryString["Pid"] != null)
                        {
                            lblProjectName.Text = _projectDL.GetProjectDetails(int.Parse(Request.QueryString["Pid"])).ProjectName;
                        }
                        DateTime? reviewDate = _deReviewDL.GetReviewDate(int.Parse(Request.QueryString["Id"]));
                        if (reviewDate.HasValue)
                        {
                            lblReviewDate.Text = reviewDate.Value.ToString("dd-MMM-yyyy");                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error saving data  : " + ex.Message;
            }
        }

        private void PopulateDEReviewDetails(int deReviewId)
        {
            grdDEMaster.DataSource = _deReviewDL.GetProjectDEReviewAttributeDetails(deReviewId);
            grdDEMaster.DataBind();
        }

        protected void grdDEMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Visible = false;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int attributeId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "AttributeId"));
                AttributeModel attribute = _deReviewDL.GetAttributeDetails(attributeId);

                Label lblAttributeId = (Label)e.Row.FindControl("lblAttributeId");
                lblAttributeId.Text = attributeId.ToString();

                Label lblAttribute = (Label)e.Row.FindControl("lblAttribute");
                lblAttribute.Text = attribute.AttributeName;

                Label lblSampleQuestions = (Label)e.Row.FindControl("lblSampleQuestions");
                lblSampleQuestions.Text = attribute.SampleQuestions;

                DropDownList ddlRAGStatus = (DropDownList)e.Row.FindControl("ddlRAGStatus");
                if (ddlRAGStatus != null)
                {
                    ddlRAGStatus.DataSource = _weeklyStatusDL.GetFlags();
                    ddlRAGStatus.DataTextField = "FlagName";
                    ddlRAGStatus.DataValueField = "FlagId";
                    ddlRAGStatus.DataBind();
                    ddlRAGStatus.Items.Insert(0, new ListItem("--Select--", ""));

                    RequiredFieldValidator reqRAGStatus = (RequiredFieldValidator)e.Row.FindControl("reqRAGStatus");
                    reqRAGStatus.ErrorMessage = "Please select RAG Status for " + ((Label)e.Row.FindControl("lblAttribute")).Text;

                    int flagId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FlagId"));
                    ddlRAGStatus.SelectedValue = flagId.ToString();
                }

                TextBox datepickerETA = (TextBox)e.Row.FindControl("datepickerETA");
                if (datepickerETA != null && DataBinder.Eval(e.Row.DataItem, "ETA") != null)
                {
                    datepickerETA.Text = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "ETA")).ToString("MM/dd/yyyy");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    DEAttributeModel attribute = null;
                    List<DEAttributeModel> attributes = new List<DEAttributeModel>();
                    foreach (GridViewRow row in grdDEMaster.Rows)
                    {
                        attribute = new DEAttributeModel();

                        Label lblAttributeId = (Label)row.FindControl("lblAttributeId");
                        attribute.AttributeId = Convert.ToInt32(lblAttributeId.Text);

                        attribute.DEReviewId = int.Parse(Request.QueryString["Id"]);

                        attribute.FlagId = Convert.ToInt32(((DropDownList)row.FindControl("ddlRAGStatus")).SelectedValue);
                        if (attribute.FlagId == -1)
                        {
                            attribute.FlagId = null;
                        }

                        attribute.CorrectiveActions = ((TextBox)row.FindControl("txtCorrectiveActions")).Text;
                        TextBox eta = ((TextBox)row.FindControl("datepickerETA"));
                        if (eta != null)
                        {
                            DateTime etaDate = DateTime.MinValue;
                            if (DateTime.TryParse(eta.Text, out etaDate))
                            {
                                attribute.ETA = etaDate;
                            }
                        }

                        attribute.LastUpdatedBy = Common.EmployeeId;
                        attribute.LastUpdatedDate = DateTime.Now;

                        attributes.Add(attribute);
                    }

                    _deReviewDL.UpdateProjectDEReviewAttributeDetails(attributes);

                    lblMessage.Text = "Data saved successfully.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error saving data  : " + ex.Message;
            }
        }
        
    }
}