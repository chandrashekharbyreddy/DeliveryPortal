using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DeliveryPortalEntities;
using DeliveryPortalDL;

namespace DeliveryPortal
{
    public partial class DECalendar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.QueryString["DEReviewCalendarId"]!=null)
                {
                    DEReviewCalendarId = Convert.ToInt32(Request.QueryString["DEReviewCalendarId"]);
                    hidDEReviewCalendarId.Value = Convert.ToString(DEReviewCalendarId);
                    GetDECalendar();
                }
                else{
                PopulateDropDowns();
                }
            }
            
        }
        public int DEReviewCalendarId
        {
            get { return Convert.ToInt32(Request.QueryString["DEReviewCalendarId"]); }
            set {ViewState["DEReviewCalendarId"]=value;}
        }
        protected void GetDECalendar() 
        {
            ProjectDL projectDL = new ProjectDL();
            DECalendarModel deCalendarModel = new DECalendarModel();
           
            
           deCalendarModel = projectDL.GetDECalendarModel(DEReviewCalendarId);
            
            ddlProject.DataSource = projectDL.ProjectsList();
            ddlProject.DataTextField = "ProjectName";
            ddlProject.DataValueField = "ProjectID";
            ddlProject.DataBind();
            ddlProject.SelectedValue = deCalendarModel.ProjectId.ToString();

            ddlReviewer.DataSource = projectDL.EmployeeList().ToList() ;
            ddlReviewer.DataTextField = "EmployeeName";
            ddlReviewer.DataValueField = "EmployeeId";
            ddlReviewer.DataBind();
            ddlReviewer.SelectedValue = deCalendarModel.EmployeeId.ToString();

            ddlStatus.DataSource = projectDL.GetStatus();
            ddlStatus.DataTextField = "ReviewStatusName";
            ddlStatus.DataValueField = "ReviewStatusId";
            ddlStatus.DataBind();
            ddlStatus.SelectedValue = deCalendarModel.ReviewStatusId.ToString();

            DateTime startDate = deCalendarModel.ReviewDate;
            datepickerDate.Text = startDate.ToString("MM/dd/yyyy");
         
           
        }
        protected void PopulateDropDowns() 
        {
            ProjectDL projectDL = new ProjectDL();
            ddlProject.DataSource = projectDL.ProjectsList();
            ddlProject.DataTextField = "ProjectName";
            ddlProject.DataValueField = "ProjectID";
            ddlProject.DataBind();
            ddlProject.Items.Insert(0, new ListItem("--Select--", ""));


            ddlReviewer.DataSource = projectDL.EmployeeList().ToList();
            ddlReviewer.DataTextField = "EmployeeName";
            ddlReviewer.DataValueField = "EmployeeId";
            ddlReviewer.DataBind();
            ddlReviewer.Items.Insert(0, new ListItem("--Select--", ""));

            ddlStatus.DataSource = projectDL.GetStatus();
            ddlStatus.DataTextField = "ReviewStatusName";
            ddlStatus.DataValueField = "ReviewStatusId";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("--Select--", ""));


        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    DECalendarModel deCalendarModel = new DECalendarModel();
                    ProjectDL projectDL = new ProjectDL();
                    if (ddlProject.SelectedItem.Value != string.Empty)
                    {
                        deCalendarModel.ProjectId = Convert.ToInt32(ddlProject.SelectedItem.Value);
                    }
                    if (ddlReviewer.SelectedItem.Value != string.Empty)
                    {
                        deCalendarModel.EmployeeId = Convert.ToInt32(ddlReviewer.SelectedItem.Value);
                    }
                    if (ddlStatus.SelectedItem.Value != string.Empty)
                    {
                        deCalendarModel.ReviewStatusId = Convert.ToInt32(ddlStatus.SelectedItem.Value);
                    }
                    if (datepickerDate.Text.Trim() != string.Empty)
                    {
                        deCalendarModel.ReviewDate = Convert.ToDateTime(datepickerDate.Text.Trim());
                    }
                    if (hidDEReviewCalendarId.Value != string.Empty)
                    {
                        deCalendarModel.DEReviewCalendarId = int.Parse(hidDEReviewCalendarId.Value);
                        projectDL.UpdateDECalendar(deCalendarModel);

                    }
                    else
                    {
                        int newDEReviewCalendarId = projectDL.InsertDECalendar(deCalendarModel);
                        hidDEReviewCalendarId.Value = newDEReviewCalendarId.ToString();
                    }

                    lblMessage.Text = "Data Saved Succesfully";
                }
            }
            catch(Exception ex)
            {
                lblMessage.Text = "Sorry!! We couldnot save the Data." + ex.Message;
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DECalendarList.aspx");
        }

    }
}