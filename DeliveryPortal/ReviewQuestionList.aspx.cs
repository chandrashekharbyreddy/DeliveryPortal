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
    public partial class ReviewQuestionList : System.Web.UI.Page
    {
        ProjectDL _projectDL = new ProjectDL();
        List<ReviewQuestionModel> _reviewQuestions = new List<ReviewQuestionModel>();

        protected void Page_Load(object sender, EventArgs e)
        {
          _reviewQuestions = _projectDL.GetReviewQuestion();
         
            if (!IsPostBack)
            {
                PopulateReviewQuestion();
                hidPageSize.Value = GridViewReview.PageSize.ToString();
            }
        }
        private void PopulateReviewQuestion()
        {
           
            GridViewReview.DataSource = _reviewQuestions;

            GridViewReview.DataBind();
        }

        //protected void GridViewReview_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        CheckBox c = e.Row.FindControl("ChkIsActive") as CheckBox;
        //        Label lbl = e.Row.FindControl("lblIsActive") as Label;
        //        if (c != null && lbl != null)
        //        {
        //            c.Checked = (lbl.Text.ToUpper() == "TRUE");
        //        }
        //    }

        //}
        protected void GridViewReview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewReview.PageIndex = e.NewPageIndex;
            GridViewReview.DataSource = _projectDL.GetReviewQuestion();
            hidPageIndex.Value = GridViewReview.PageIndex.ToString();
            GridViewReview.DataBind();
        }

        protected void BtnAddNewQuestion_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReviewMaster.aspx");
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            PopulateProjectAttributesSearch();
        }
          private void PopulateProjectAttributesSearch()
        {

            ProjectDL projectDL = new ProjectDL();
            GridViewReview.DataSource = projectDL.SearchQuestion(txtQuestionName.Text);
            GridViewReview.DataBind();
        }

        protected void BtnDeleteAttribute_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow row in GridViewReview.Rows)
            {
                var check = row.FindControl("chkSelect") as CheckBox;
                if (check.Checked)
                {
                    int id = Convert.ToInt32(GridViewReview.DataKeys[row.RowIndex].Values["QuestionId"]);
                    _projectDL.DeleteQuestion(id);
                    GridViewReview.DataSource = _projectDL.GetReviewQuestion(); ;
                }
            }
            GridViewReview.DataBind();
            hidPageIndex.Value = GridViewReview.PageIndex.ToString();


        }
    }
}