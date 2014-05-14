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
    public partial class ProjectMaster : System.Web.UI.Page
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
                    PopulateAccounts();
                    PopulateIDPs();
                    PopulateEMandPMIds();
                    PopulateGeoLocations();
                    PopulateSectors();
                    PopulateMethodologies();
                    PopulateNoWs();
                    PoulateEsts();

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

            txtProjectCode.Text = project.ProjectCode;
            txtProjectName.Text = project.ProjectName;
            drpAccount.SelectedValue = project.AccountId.ToString();
            if (project.IdpId.HasValue)
            {
                drpIDP.SelectedValue = project.IdpId.Value.ToString();
            }
            if (project.EMId.HasValue)
            {
                drpEMs.SelectedValue = project.EMId.Value.ToString();
            }
            if (project.PMID.HasValue)
            {
                drpPMs.SelectedValue = project.PMID.Value.ToString();
            }
            if (project.GeoId.HasValue)
            {
                drpGeo.SelectedValue = project.GeoId.Value.ToString();
            }
            if (project.SectorId.HasValue)
            {
                drpSector.SelectedValue = project.SectorId.Value.ToString();
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
            if (project.MethodologyId.HasValue)
            {
                drpMethodology.SelectedValue = project.MethodologyId.Value.ToString();
            }
            if (project.NoWId.HasValue)
            {
                drpNow.SelectedValue = project.NoWId.Value.ToString();
            }
            if (project.EstBasisId.HasValue)
            {
                drpEST.SelectedValue = project.EstBasisId.Value.ToString();
            }
            if (project.LastDEReviewsDate.HasValue)
            {
                DateTime lastDEReviewsDate = project.LastDEReviewsDate.Value;
                lblLastDEReviewDate.Text = lastDEReviewsDate.ToString("MM/dd/yyyy");
            }
            else
            {
                lblLastDEReviewDate.Text = "NA";
            }
            if (project.LastDQADate.HasValue)
            {
                DateTime lastDQADate = project.LastDQADate.Value;
                lblLastIDQADate.Text = lastDQADate.ToString("MM/dd/yyyy");
            }
            else
            {
                lblLastIDQADate.Text = "NA";
            }
            if (project.LastSMRDate.HasValue)
            {
                DateTime lastSMRDate = project.LastSMRDate.Value;
                lblLastSMRDate.Text = lastSMRDate.ToString("MM/dd/yyyy");
            }
            else
            {
                lblLastSMRDate.Text = "NA";
            }
            if (project.IsStrategic.HasValue)
            {
                chkIsStrategic.Checked = project.IsStrategic.Value;
            }
            //if (project.IsRA.HasValue) 
            //{
            //    chkIsRA.Checked = project.IsRA.Value;
            //}

        }

        private void PopulateAccounts()
        {
            drpAccount.DataSource = _projectDL.GetAccounts();
            drpAccount.DataTextField = "AccountName";
            drpAccount.DataValueField = "AccountId";
            drpAccount.DataBind();
            drpAccount.Items.Insert(0, new ListItem("--Select--", ""));
        }

        private void PopulateIDPs()
        {
            drpIDP.DataSource = _projectDL.GetIDPs();
            drpIDP.DataTextField = "IDPName";
            drpIDP.DataValueField = "IDPId";
            drpIDP.DataBind();
            drpIDP.Items.Insert(0, new ListItem("--Select--", ""));
        }

        private void PopulateEMandPMIds()
        {
            List<EmployeeModel> employees = _projectDL.GetEmployees();

            drpEMs.DataSource = employees;
            drpEMs.DataTextField = "EmployeeName";
            drpEMs.DataValueField = "EmployeeId";
            drpEMs.DataBind();
            drpEMs.Items.Insert(0, new ListItem("--Select--", ""));

            drpPMs.DataSource = employees;
            drpPMs.DataTextField = "EmployeeName";
            drpPMs.DataValueField = "EmployeeId";
            drpPMs.DataBind();
            drpPMs.Items.Insert(0, new ListItem("--Select--", ""));
        }

        private void PopulateGeoLocations()
        {
            drpGeo.DataSource = _projectDL.GetGeoLocations();
            drpGeo.DataTextField = "GeoName";
            drpGeo.DataValueField = "GeoId";
            drpGeo.DataBind();
            drpGeo.Items.Insert(0, new ListItem("--Select--", ""));
        }

        private void PopulateSectors()
        {
            drpSector.DataSource = _projectDL.GetSectors();
            drpSector.DataTextField = "SectorName";
            drpSector.DataValueField = "SectorId";
            drpSector.DataBind();
            drpSector.Items.Insert(0, new ListItem("--Select--", ""));
        }

        private void PopulateMethodologies()
        {
            drpMethodology.DataSource = _projectDL.GetMethodologies();
            drpMethodology.DataTextField = "MethodologyName";
            drpMethodology.DataValueField = "MethodologyId";
            drpMethodology.DataBind();
            drpMethodology.Items.Insert(0, new ListItem("--Select--", ""));
        }

        private void PopulateNoWs()
        {
            drpNow.DataSource = _projectDL.GetNoWs();
            drpNow.DataTextField = "NoWName";
            drpNow.DataValueField = "NoWId";
            drpNow.DataBind();
            drpNow.Items.Insert(0, new ListItem("--Select--", ""));
        }

        private void PoulateEsts()
        {
            drpEST.DataSource = _projectDL.GetEstBasisModel();
            drpEST.DataTextField = "EstBasisName";
            drpEST.DataValueField = "EstBasisId";
            drpEST.DataBind();
            drpEST.Items.Insert(0, new ListItem("--Select--", ""));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {

                    ProjectModel project = new ProjectModel();
                    project.ProjectCode = txtProjectCode.Text.Trim();
                    project.AccountId = Convert.ToInt32(drpAccount.SelectedItem.Value);
                    project.ProjectName = txtProjectName.Text.Trim();
                    if (drpIDP.SelectedItem.Value != string.Empty)
                    {
                        project.IdpId = Convert.ToInt32(drpIDP.SelectedItem.Value);
                    }
                    if (drpEMs.SelectedItem.Value != string.Empty)
                    {
                        project.EMId = Convert.ToInt32(drpEMs.SelectedItem.Value);
                    }
                    if (drpPMs.SelectedItem.Value != string.Empty)
                    {
                        project.PMID = Convert.ToInt32(drpPMs.SelectedItem.Value);
                    }
                    if (drpGeo.SelectedItem.Value != string.Empty)
                    {
                        project.GeoId = Convert.ToInt32(drpGeo.SelectedItem.Value);
                    }
                    if (drpSector.SelectedItem.Value != string.Empty)
                    {
                        project.SectorId = Convert.ToInt32(drpSector.SelectedItem.Value);
                    }
                    if (datepickerStartDate.Text.Trim() != string.Empty)
                    {
                        project.StartDate = Convert.ToDateTime(datepickerStartDate.Text.Trim());
                    }
                    if (datepickerEndDate.Text.Trim() != string.Empty)
                    {
                        project.EndDate = Convert.ToDateTime(datepickerEndDate.Text.Trim());
                    }
                    if (drpMethodology.SelectedItem.Value != string.Empty)
                    {
                        project.MethodologyId = Convert.ToInt32(drpMethodology.SelectedItem.Value);
                    }
                    if (drpNow.SelectedItem.Value != string.Empty)
                    {
                        project.NoWId = Convert.ToInt32(drpNow.SelectedItem.Value);
                    }
                    if (drpEST.SelectedItem.Value != string.Empty)
                    {
                        project.EstBasisId = Convert.ToInt32(drpEST.SelectedItem.Value);
                    }
                    project.IsStrategic = chkIsStrategic.Checked;
                    //project.IsRA = chkIsRA.Checked;
                    project.LastUpdateDate = DateTime.Now;
                    project.LastUpdatedBy = Common.EmployeeId;

                    if (hidProjectId.Value != string.Empty)
                    {
                        project.ProjectId = int.Parse(hidProjectId.Value);
                        _projectDL.UpdateProjectDetails(project);
                    }
                    else
                    {
                        int newProjectId = _projectDL.InsertProjectDetails(project);
                        hidProjectId.Value = newProjectId.ToString();
                    }

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