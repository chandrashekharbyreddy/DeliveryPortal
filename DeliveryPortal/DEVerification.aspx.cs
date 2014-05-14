using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DeliveryPortalDL;
using DeliveryPortalEntities;

namespace DeliveryPortal
{
    public partial class DEVerification : System.Web.UI.Page
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

                string strRed = "<img src='Images/circle_red.png' >";
                string strGreen = "<img src='Images/circle_green.png'>";
                string strAmber = "<img src='Images/circle_amber.png'>";
                string strGrey = "<img src='Images/circle_grey.png'>";

                int flagId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FlagId"));
                Label lblRAGStatus = (Label)e.Row.FindControl("lblRAGStatus");

                if (flagId == 3)
                {
                    lblRAGStatus.Text = strGreen;
                }
                else if (flagId == 2)
                {
                    lblRAGStatus.Text = strAmber;
                }
                else if (flagId == 1)
                {
                    lblRAGStatus.Text = strRed;
                }
                else
                {
                    lblRAGStatus.Text = strGrey;
                }

                Label lblETA = (Label)e.Row.FindControl("lblETA");
                if (DataBinder.Eval(e.Row.DataItem, "ETA") != null)
                {
                    lblETA.Text = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "ETA")).ToString("MM/dd/yyyy");
                }

                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                if (ddlStatus != null)
                {
                    ddlStatus.DataSource = _weeklyStatusDL.GetReviewStatus();
                    ddlStatus.DataTextField = "ReviewStatusName";
                    ddlStatus.DataValueField = "ReviewStatusId";
                    ddlStatus.DataBind();
                    ddlStatus.Items.Insert(0, new ListItem("--Select--", ""));


                    RequiredFieldValidator reqStatus = (RequiredFieldValidator)e.Row.FindControl("reqStatus");
                    reqStatus.ErrorMessage = "Please select Status for " + ((Label)e.Row.FindControl("lblAttribute")).Text;

                    int flagid = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ReviewStatusId"));
                    ddlStatus.SelectedValue = flagid.ToString();
                }


            }
        }

        protected void btnDone_Click(object sender, EventArgs e)
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

                        //Label lblRAGStatus = (Label)row.FindControl("lblRAGStatus");
                        //attribute.FlagId = Convert.ToInt32(lblRAGStatus.Text);
                        //if (attribute.FlagId == -1)
                        //{
                        //    attribute.FlagId = null;
                        //}

                        //attribute.CorrectiveActions = ((Label)row.FindControl("lblCorrectiveActions")).Text;
                        //TextBox eta = ((TextBox)row.FindControl("lblETA"));

                        //Label lblETA = (Label)row.FindControl("lblETA");
                        //attribute.ETA = Convert.ToDateTime(lblETA.Text);

                        attribute.ReviewStatusId = Convert.ToInt32(((DropDownList)row.FindControl("ddlStatus")).SelectedValue);


                        attribute.LastUpdatedBy = Common.EmployeeId;
                        attribute.LastUpdatedDate = DateTime.Now;

                        attributes.Add(attribute);
                    }

                    _deReviewDL.UpdateProjectDEReviewStatus(attributes);

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



