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
    public partial class DEUpdateList : System.Web.UI.Page
    {
        DEReviewDL _deReviewDL = new DEReviewDL();
        ProjectDL _projectDL = new ProjectDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                PopulateDEReviewList();
               
            }

        }
        protected void PopulateDEReviewList() 
        {
            grdDEUpdateList.DataSource = _deReviewDL.GetDEReviewList();
            grdDEUpdateList.DataBind();
        }

       

        protected void btnAddNewDEReview_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DEUpdateMaster.aspx");
        }

        protected void grdDEUpdateList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDEUpdateList.PageIndex = e.NewPageIndex;
            grdDEUpdateList.DataSource = _deReviewDL.SearchDEReviewList(txtProjectName.Text.Trim());
            
            grdDEUpdateList.DataBind();
        }

        protected void grdDEUpdateList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hypLnk = (HyperLink)e.Row.Cells[3].Controls[0];
               
                if (((DEReviewModel)e.Row.DataItem).ReviewDate.HasValue)
                {
                    hypLnk.Text = "Submit";
                }
                else 
                {
                    hypLnk.Text = "Edit";
                }
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            grdDEUpdateList.DataSource = _deReviewDL.SearchDEReviewList(txtProjectName.Text.Trim());

            grdDEUpdateList.DataBind();
        }

    }
}