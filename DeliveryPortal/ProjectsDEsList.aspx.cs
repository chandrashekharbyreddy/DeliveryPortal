using DeliveryPortalDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DeliveryPortal
{
    public partial class ProjectsDEsList : System.Web.UI.Page
    {
        ProjectDL _projectDL = new ProjectDL();
        DEReviewDL _deReviewDL = new DEReviewDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateProjects();
            }
        }

        private void PopulateProjects()
        {
            ddlProjects.DataSource = _projectDL.ProjectsList();
            ddlProjects.DataTextField = "ProjectName";
            ddlProjects.DataValueField = "ProjectId";
            ddlProjects.DataBind();
            ddlProjects.Items.Insert(0, new ListItem("--Select--", ""));
        }

        protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProjects.SelectedIndex > 0)
            {
                grdProjectList.DataSource = _deReviewDL.GetProjectDEReviewDetails(int.Parse(ddlProjects.SelectedValue));
                grdProjectList.DataBind();
            }
            else
            {
                grdProjectList.DataSource = null;
                grdProjectList.DataBind();
            }
        }

        protected void grdProjectList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int proId = 0;
                //DropDownList ddlProId = (DropDownList)e.Row.FindControl("ddlProjects");
                if (ddlProjects.SelectedIndex > 0) 
                {
                    proId = Convert.ToInt32(ddlProjects.SelectedValue);
                }

                HtmlInputHidden hidReviewId = (HtmlInputHidden)e.Row.FindControl("hidReviewId");
                int reviewId = int.Parse(hidReviewId.Value);

                HyperLink hlReviewDate = (HyperLink)e.Row.FindControl("hlReviewDate");
                hlReviewDate.Text = Convert.ToDateTime(hlReviewDate.Text).ToString("dd-MMM-yyyy");                
                //hlReviewDate.Text = Convert.ToDateTime(Eval("ReviewDate")).ToString();
                //if (((DataRowView)e.Row.DataItem)["ReviewDate"] != DBNull.Value)
                //{
                //    hlReviewDate.Text = ((DateTime)((DataRowView)e.Row.DataItem)["ReviewDate"]).ToString();
                //}
                if (Request.QueryString["type"] == "DEUP")
                {
                    hlReviewDate.NavigateUrl = "~/DEUpdates.aspx?Id=" + reviewId + "&Pid=" + proId;
                }
                else 
                {
                    hlReviewDate.NavigateUrl = "~/DEVerification.aspx?Id=" + reviewId + "&Pid=" + proId;
                }
            }
        }
        
    }
}