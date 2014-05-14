using DeliveryPortalDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DeliveryPortalEntities;

namespace DeliveryPortal
{
    public partial class ProjectsList : System.Web.UI.Page
    {
        ProjectDL _projectDL = new ProjectDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //grdProjectList.DataSource = _projectDL.GetProjectModel();
                //grdProjectList.DataBind();
                grdProjectList.DataSource = _projectDL.SearchProjects(txtProjectCode.Text.Trim(), txtProjectName.Text.Trim());
                grdProjectList.DataBind();
                hidPageSize.Value = grdProjectList.PageSize.ToString();
            }
        }

        protected void btnProjectSearch_Click(object sender, EventArgs e)
        {
            grdProjectList.DataSource = _projectDL.SearchProjects(txtProjectCode.Text.Trim(), txtProjectName.Text.Trim());
            grdProjectList.DataBind();
        }

        protected void grdProjectList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdProjectList.PageIndex = e.NewPageIndex;
            grdProjectList.DataSource = _projectDL.SearchProjects(txtProjectCode.Text.Trim(), txtProjectName.Text.Trim());
            hidPageIndex.Value = grdProjectList.PageIndex.ToString();
            grdProjectList.DataBind();
        }

        protected void btnAddNewProject_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProjectMaster.aspx");
        }
        //protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    //CheckBox ChkBoxHeader = (CheckBox).HeaderRow.FindControl("chkboxSelectAll");
        //    CheckBox ChkBoxHeader = (CheckBox)grdProjectList.HeaderRow.FindControl("chkboxSelectAll");
        //    foreach (GridViewRow row in grdProjectList.Rows)
        //    {
        //        CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkEmp");
        //        if (ChkBoxHeader.Checked == true)
        //        {
        //            ChkBoxRows.Checked = true;
        //        }
        //        else
        //        {
        //            ChkBoxRows.Checked = false;
        //        }
        //    }
        //}

        protected void btnDeleteProject_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdProjectList.Rows)
            {
                var check = row.FindControl("chkEmp") as CheckBox;
                if (check.Checked)
                {
                    int id = Convert.ToInt32(grdProjectList.DataKeys[row.RowIndex].Values["ProjectId"]);
                    _projectDL.DeleteProject(id);
                    grdProjectList.DataSource = _projectDL.SearchProjects(txtProjectCode.Text.Trim(), txtProjectName.Text.Trim());
                   
                    //grdProjectList_RowDeleting(object sender, GridViewDeleteEventArgs e);
                }
            }
            grdProjectList.DataBind();
            hidPageIndex.Value = grdProjectList.PageIndex.ToString();
        }


        //protected void grdProjectList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    GridViewRow row = (GridViewRow)grdProjectList.Rows[e.RowIndex];
        //    Label lblProjectId = row.FindControl("lblProjectId") as Label;
        //    if (lblProjectId != null)
        //    {
        //        _projectDL.DeleteProject(int.Parse(lblProjectId.Text));
        //        grdProjectList.DataSource = _projectDL.SearchProjects(txtProjectCode.Text.Trim(), txtProjectName.Text.Trim());
        //        grdProjectList.DataBind();
        //    }
        //}

    }

}