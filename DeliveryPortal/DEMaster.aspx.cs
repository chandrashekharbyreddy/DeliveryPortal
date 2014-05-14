using DeliveryPortalDL;
using DeliveryPortalEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeliveryPortal
{
    public partial class DEMaster : System.Web.UI.Page
    {
        DEReviewDL _deReviewDL = new DEReviewDL();
        WeeklyStatusDL _weeklyStatusDL = new WeeklyStatusDL();
        ProjectDL _projectDL = new ProjectDL();


        public int DEReviewId
        {
            get
            {
                if (ViewState["DEReviewId"] != null)
                {
                    return Convert.ToInt32(ViewState["DEReviewId"]);
                }
                else
                    return 0;
            }
            set { ViewState["DEReviewId"] = value; }
        }
        public int DEReviewCalendarId
        {
            get
            {
                if (ViewState["DEReviewCalendarId"] != null)
                {
                    return Convert.ToInt32(ViewState["DEReviewCalendarId"]);
                }
                else
                    return 0;
            }
            set { ViewState["DEReviewCalendarId"] = value; }
        }
        public int ProjectId
        {
            get
            {
                if (ViewState["ProjectId"] != null)
                {
                    return Convert.ToInt32(ViewState["ProjectId"]);
                }
                else
                    return 0;
            }
            set { ViewState["ProjectId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //PopulateProjects();
                BindDEEntry();
                if (Request.QueryString["DEReviewId"] != null)
                {
                    DEReviewId = Convert.ToInt32(Request.QueryString["DEReviewId"]);
                }
                if (Request.QueryString["DEReviewCalendarId"] != null)
                {
                    DEReviewCalendarId = Convert.ToInt32(Request.QueryString["DEReviewCalendarId"]);
                }

                if (DEReviewCalendarId != 0)
                {
                    DEReviewModel dReviewModel = _deReviewDL.GetDEReview(DEReviewCalendarId);
                    lblProjectName.Text = dReviewModel.ProjectName;
                    lblScheduleDate.Text = dReviewModel.ScheduleDate.HasValue ? dReviewModel.ScheduleDate.Value.ToString("dd-MMM-yyyy") : string.Empty;
                    ProjectId = dReviewModel.ProjectId;
                    datepickerReviewDate.Text = dReviewModel.ReviewDate.HasValue ? dReviewModel.ReviewDate.Value.ToShortDateString() : string.Empty;
                    if (DEReviewId != 0)
                    {
                        grdDEMaster.DataSource = _deReviewDL.GetProjectDEReviewAttributeDetails(DEReviewId);
                        grdDEMaster.DataBind();
                    }

                }
            }
        }

        //private void PopulateProjects()
        //{
        //    ddlProjects.DataSource = _projectDL.ProjectsList();
        //    ddlProjects.DataTextField = "ProjectName";
        //    ddlProjects.DataValueField = "ProjectId";
        //    ddlProjects.DataBind();
        //    ddlProjects.Items.Insert(0, new ListItem("--Select--", ""));
        //}

        private void BindDEEntry()
        {
            grdDEMaster.DataSource = _deReviewDL.GetDEReviewDetails();
            grdDEMaster.DataBind();
        }

        protected void grdDEMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlRAGStatus = (DropDownList)e.Row.FindControl("ddlRAGStatus");
                if (ddlRAGStatus != null)
                {
                    ddlRAGStatus.DataSource = _weeklyStatusDL.GetFlags();
                    ddlRAGStatus.DataTextField = "FlagName";
                    ddlRAGStatus.DataValueField = "FlagId";
                    ddlRAGStatus.DataBind();
                    ddlRAGStatus.Items.Insert(0, new ListItem("--Select--", ""));
                    Label lblFlagId = (Label)e.Row.FindControl("lblFlagId");
                    ListItem lstItem = ddlRAGStatus.Items.FindByValue(lblFlagId.Text);
                    if (lstItem != null)
                        lstItem.Selected = true;
                    RequiredFieldValidator reqRAGStatus = (RequiredFieldValidator)e.Row.FindControl("reqRAGStatus");
                    reqRAGStatus.ErrorMessage = "Please select RAG Status for " + ((Label)e.Row.FindControl("lblAttribute")).Text;
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    DEReviewModel deReviewModel = new DEReviewModel();
                    //deReviewModel.ProjectId = Convert.ToInt32(ddlProjects.SelectedValue);
                    deReviewModel.DEReviewCalendarId = DEReviewCalendarId;
                    deReviewModel.ProjectId = ProjectId;
                    deReviewModel.ReviewDate = Convert.ToDateTime(datepickerReviewDate.Text.Trim());
                    deReviewModel.LastUpdatedBy = Common.EmployeeId;
                    deReviewModel.LastUpdatedDate = DateTime.Now;

                    List<DEAttributeModel> deAttributes = new List<DEAttributeModel>();
                    foreach (GridViewRow row in grdDEMaster.Rows)
                    {
                        DEAttributeModel deAttribute = new DEAttributeModel();
                        deAttribute.AttributeId = Convert.ToInt32(grdDEMaster.DataKeys[row.RowIndex].Values["AttributeId"]);
                        deAttribute.FlagId = Convert.ToInt32(((DropDownList)row.FindControl("ddlRAGStatus")).SelectedValue);
                        if (deAttribute.FlagId == -1)
                        {
                            deAttribute.FlagId = null;
                        }
                        deAttribute.Observations = ((TextBox)row.FindControl("txtObservations")).Text;
                        deAttribute.Recommendations = ((TextBox)row.FindControl("txtRecommendations")).Text;
                        deAttribute.LastUpdatedBy = Common.EmployeeId;
                        deAttribute.LastUpdatedDate = DateTime.Now;

                        deAttributes.Add(deAttribute);
                    }

                    if (DEReviewId == 0)
                    {
                        // Insert
                        DEReviewId = _deReviewDL.InsertDEReviewComments(deReviewModel, deAttributes);
                    }
                    else
                    {
                        // Update
                        deReviewModel.DEReviewId = DEReviewId;
                        _deReviewDL.UpdateDEReviewComments(deReviewModel, deAttributes);
                    }
                    lblMessage.Text = "Data saved successfully.";

                    btnGenerateDEReport.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error saving data  : " + ex.Message;
            }

        }

        protected void btnGenerateDEReport_Click(object sender, EventArgs e)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;
            string scriptSuccess = "<script type=\"text/javascript\">window.open('DEReport.aspx?Id=" + DEReviewId.ToString() + "','_blank')</script>";
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alertSuccess"))
            {
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alertSuccess", scriptSuccess);
            }
        }
    }
}