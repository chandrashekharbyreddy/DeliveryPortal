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
    public partial class ProjectDetails : System.Web.UI.Page
    {
        ProjectDL _projectDL = new ProjectDL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["ID"] != null)
                {
                    hidProjectId.Value = Request.QueryString["ID"];
                }

                if (!IsPostBack)
                {
                    PopulateIDPs();
                    PopulateAccounts();
                    PopulateProjectName();
                    PopulateEMandPMIds();


                    if (hidProjectId.Value != string.Empty)
                    {
                        GetProjectDetails(int.Parse(hidProjectId.Value));
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error fetching data  : " + ex.Message;
            }
        }
        private void GetProjectDetails(int projectId)
        {
            ProjectModel project = _projectDL.GetProjectDetails(projectId);

            if (project.IdpId.HasValue)
            {
                ddlIDP.SelectedValue = project.IdpId.Value.ToString();
            }
            ddlAccount.SelectedValue = project.AccountId.ToString();
            ddlProjectName.SelectedValue = project.ProjectId.ToString();

            if (project.EMId.HasValue)
            {
                ddlEM.SelectedValue = project.EMId.Value.ToString();
            }
            if (project.PMID.HasValue)
            {
                ddlPM.SelectedValue = project.PMID.Value.ToString();
            }
            if (project.StartDate.HasValue)
            {
                DateTime startDate = project.StartDate.Value;
                datepickerStartDate.Text = startDate.ToString("MM/dd/yyyy");
            }
            if (project.EndDate.HasValue)
            {
                DateTime endDate = project.EndDate.Value;
                datepickerEndDate.Text = endDate.ToString("MM/dd/yyyy");
            }
            if (project.IsStrategic.HasValue)
            {
                chkIsStrategic.Checked = project.IsStrategic.Value;
            }
        }
        private void PopulateAccounts()
        {
            ddlAccount.DataSource = _projectDL.GetAccounts();
            ddlAccount.DataTextField = "AccountName";
            ddlAccount.DataValueField = "AccountId";
            ddlAccount.DataBind();
            ddlAccount.Items.Insert(0, new ListItem("--Select--", ""));
        }
        private void PopulateProjectName()
        {
            ddlProjectName.DataSource = _projectDL.GetProjectModel();
            ddlProjectName.DataTextField = "ProjectName";
            ddlProjectName.DataValueField = "ProjectId";
            ddlProjectName.DataBind();
            ddlProjectName.Items.Insert(0, new ListItem("--Select--", ""));
        }
        //private void PopulateProjectCode() 
        //{

        //}
        private void PopulateIDPs()
        {
            ddlIDP.DataSource = _projectDL.GetIDPs();
            ddlIDP.DataTextField = "IDPName";
            ddlIDP.DataValueField = "IDPId";
            ddlIDP.DataBind();
            ddlIDP.Items.Insert(0, new ListItem("--Select--", ""));
        }

        private void PopulateEMandPMIds()
        {
            List<EmployeeModel> employees = _projectDL.GetEmployees();

            ddlEM.DataSource = employees;
            ddlEM.DataTextField = "EmployeeName";
            ddlEM.DataValueField = "EmployeeId";
            ddlEM.DataBind();
            ddlEM.Items.Insert(0, new ListItem("--Select--", ""));

            ddlPM.DataSource = employees;
            ddlPM.DataTextField = "EmployeeName";
            ddlPM.DataValueField = "EmployeeId";
            ddlPM.DataBind();
            ddlPM.Items.Insert(0, new ListItem("--Select--", ""));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {

                    ProjectModel project = new ProjectModel();

                    project.AccountId = Convert.ToInt32(ddlAccount.SelectedItem.Value);
                    project.ProjectName = Convert.ToString(ddlProjectName.SelectedItem.Value);
                    if (ddlIDP.SelectedItem.Value != string.Empty)
                    {
                        project.IdpId = Convert.ToInt32(ddlIDP.SelectedItem.Value);
                    }
                    if (ddlEM.SelectedItem.Value != string.Empty)
                    {
                        project.EMId = Convert.ToInt32(ddlEM.SelectedItem.Value);
                    }
                    if (ddlPM.SelectedItem.Value != string.Empty)
                    {
                        project.PMID = Convert.ToInt32(ddlPM.SelectedItem.Value);
                    }
                    if (datepickerStartDate.Text.Trim() != string.Empty)
                    {
                        project.StartDate = Convert.ToDateTime(datepickerStartDate.Text.Trim());
                    }
                    if (datepickerEndDate.Text.Trim() != string.Empty)
                    {
                        project.EndDate = Convert.ToDateTime(datepickerEndDate.Text.Trim());
                    }
                    project.IsStrategic = chkIsStrategic.Checked;

                    project.LastUpdateDate = DateTime.Now;
                    project.LastUpdatedBy = Common.EmployeeId;

                    project.ProjectId = int.Parse(hidProjectId.Value);
                    _projectDL.UpdateProjectDetails(project);


                    lblMessage.Text = "Data saved successfully.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error saving data  : " + ex.Message;
            }

        }

        protected void btnProjectList_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProjectsList.aspx");
        }
        

    }
}