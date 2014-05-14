using DeliveryPortalDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeliveryPortal
{
    public partial class Attribute : System.Web.UI.Page
    {
        ProjectDL _projectDL = new ProjectDL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { PopulateList(); hidPageSize.Value = grdAttribute.PageSize.ToString(); }
        }
        protected void PopulateList() 
        {
            grdAttribute.DataSource = _projectDL.GetIDPAttributeList();
            grdAttribute.DataBind();
        }
        protected void grdAttribute_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAttribute.PageIndex = e.NewPageIndex;
            grdAttribute.DataSource = _projectDL.SearchIDPAttributes(txtAttribute.Text.Trim());
            hidPageIndex.Value = grdAttribute.PageIndex.ToString();
            grdAttribute.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateSearchList();
        }
        protected void PopulateSearchList()
        {
            grdAttribute.DataSource = _projectDL.SearchIDPAttributes(txtAttribute.Text.Trim());
            grdAttribute.DataBind();
        }

        protected void btnAddAttribute_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/IDPAttributeMaster.aspx");
        }

        protected void BtnDeleteAttribute_Click(object sender, EventArgs e)
        {
            //ProjectDL projectDL = new ProjectDL();
            foreach (GridViewRow row in grdAttribute.Rows)
            {
                var check = row.FindControl("chkAttr") as CheckBox;

                if (check.Checked)
                {
                    int AttributeId = Convert.ToInt32(grdAttribute.DataKeys[row.RowIndex].Values["AttributeId"]);
                    
                    _projectDL.DeleteIDPAttribute(AttributeId);
                }

            }
            grdAttribute.DataSource = _projectDL.SearchIDPAttributes(txtAttribute.Text.Trim());

            grdAttribute.DataBind();
            hidPageIndex.Value = grdAttribute.PageIndex.ToString();
        }
    }
}