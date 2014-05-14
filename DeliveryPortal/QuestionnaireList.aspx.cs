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
    public partial class QuestionnaireList : System.Web.UI.Page
    {
        ProjectDL _projectDL = new ProjectDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                grdQuestionnaireList.DataSource = _projectDL.SearchQuestionnair(txtQuestionnaireName.Text.Trim(), txtQuestionnairetype.Text.Trim());
                grdQuestionnaireList.DataBind();
                hidPageSize.Value = grdQuestionnaireList.PageSize.ToString();
            }
        }

        protected void btnAddNewQuestionnaire_Click(object sender, EventArgs e)
        {
            Response.Redirect("QuestionnairMaster.aspx");
        }

        protected void btnQuestionnaireSearch_Click(object sender, EventArgs e)
        {
            grdQuestionnaireList.DataSource = _projectDL.SearchQuestionnair(txtQuestionnaireName.Text.Trim(), txtQuestionnairetype.Text.Trim());
            grdQuestionnaireList.DataBind();
        }

        protected void grdQuestionnaireList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdQuestionnaireList.PageIndex = e.NewPageIndex;
            grdQuestionnaireList.DataSource = _projectDL.SearchQuestionnair(txtQuestionnaireName.Text.Trim(), txtQuestionnairetype.Text.Trim());
            hidPageIndex.Value = grdQuestionnaireList.PageIndex.ToString();
            grdQuestionnaireList.DataBind();
        }

        protected void btnDeleteQuestionnaire_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdQuestionnaireList.Rows)
            {
                var check = row.FindControl("chkEmp") as CheckBox;
                if (check.Checked)
                {
                    int id = Convert.ToInt32(grdQuestionnaireList.DataKeys[row.RowIndex].Values["QuestionnaireId"]);
                    _projectDL.DeleteQuestionnair(id);
                    grdQuestionnaireList.DataSource = _projectDL.SearchQuestionnair(txtQuestionnaireName.Text.Trim(), txtQuestionnairetype.Text.Trim());

                    //grdProjectList_RowDeleting(object sender, GridViewDeleteEventArgs e);
                }
            }
            grdQuestionnaireList.DataBind();
            hidPageIndex.Value = grdQuestionnaireList.PageIndex.ToString();
        }

        //protected void grdQuestionnaireList_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        CheckBox c = e.Row.FindControl("chkEmp") as CheckBox;
        //        Label lbl = e.Row.FindControl("lblQuestionnaireId") as Label;
        //        if (c != null && lbl != null)
        //        {
        //            c.Checked = (lbl.Text.ToUpper() == "TRUE");
        //        }
        //    }

        //}
    }
}