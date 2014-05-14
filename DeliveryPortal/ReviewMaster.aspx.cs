using DeliveryPortalDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DeliveryPortalEntities;
using System.Web.UI.HtmlControls;

namespace DeliveryPortal
{
    public partial class ReviewMaster : System.Web.UI.Page
    {
          ProjectDL projectDL = new ProjectDL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    PopulateDropDownForQuestionnaireId();
                    PopulateDropDownForRollUp();


                    if (Request.QueryString["QuestionId"] != null)
                    {
                        hidQuestionId.Value = Convert.ToString(Request.QueryString["QuestionId"]);
                        GetReviewQuestion(int.Parse(Request.QueryString["QuestionId"]));
                    }
                }

            }

            catch (Exception ex)
            {
                lblMessage.Text = "Error fetching data  : " + ex.Message;
            }

        }
        private void GetReviewQuestion(int QuestionId)
        {
            ProjectDL projectDL = new ProjectDL();
           ReviewQuestionModel ReviewQuestionModel = new ReviewQuestionModel();
           ReviewQuestionModel = projectDL.GetReviewQuestionForQuestionId(QuestionId);
           txtQuestion.Text = ReviewQuestionModel.QuestionDescription;
           txtQuestionGuidelines.Text = ReviewQuestionModel.QuestionGuideLines;
           ddlQuestionnaireId.Items.Insert(0, new ListItem("--Select--", "0"));
           ddlQuestionnaireId.SelectedValue = ReviewQuestionModel.QuestionnaireId.ToString();
           drpRollUp.Items.Insert(0, new ListItem("--Select--", "0"));
           drpRollUp.SelectedValue = ReviewQuestionModel.AttributeId.ToString();
        }


        

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReviewQuestionList.aspx");
        }

        private void PopulateDropDownForQuestionnaireId()
        {
            ProjectDL projectDL = new ProjectDL();
            ddlQuestionnaireId.DataSource = projectDL.GetQuestionnaireIdList();
            ddlQuestionnaireId.DataTextField = "QuestionnaireName";
            ddlQuestionnaireId.DataValueField = "QuestionnaireId";
            ddlQuestionnaireId.DataBind();
            ddlQuestionnaireId.Items.Insert(0, new ListItem("--Select--", ""));
        }

        private void PopulateDropDownForRollUp()
        {
            ProjectDL projectDL = new ProjectDL();
            drpRollUp.DataSource = projectDL.GetProjectAttributesList();
            drpRollUp.DataTextField = "AttributeName";
            drpRollUp.DataValueField = "AttributeId";
            drpRollUp.DataBind();
            drpRollUp.Items.Insert(0, new ListItem("--Select--", ""));
        }

        protected void chkActive_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ReviewQuestionModel reviewQuestionModel = new ReviewQuestionModel();

                if (ddlQuestionnaireId.SelectedItem.Value != string.Empty)
                {
                    reviewQuestionModel.QuestionnaireId = Convert.ToInt32(ddlQuestionnaireId.SelectedItem.Value);
                }
                if (txtQuestion.Text != null)
                {
                    reviewQuestionModel.QuestionDescription = txtQuestion.Text.Trim();
                }
                if (txtQuestionGuidelines.Text != null)
                {
                    reviewQuestionModel.QuestionGuideLines = txtQuestionGuidelines.Text.Trim();
                }
                else
                {
                    reviewQuestionModel.QuestionGuideLines = null;
                }
                reviewQuestionModel.IsActive = true;

                if (drpRollUp.SelectedItem.Value != string.Empty)
                {
                    reviewQuestionModel.AttributeId = Convert.ToInt32(drpRollUp.SelectedItem.Value);
                }

                if (hidQuestionId.Value != string.Empty)
                {
                    reviewQuestionModel.QuestionId = int.Parse(hidQuestionId.Value);
                    projectDL.UpdateQuestion(reviewQuestionModel);
                }
                else
                {
                    int newQuestionId = projectDL.InsertReviewQuestionModel(reviewQuestionModel);
                    hidQuestionId.Value = newQuestionId.ToString();
                }

                lblMessage.Text = "Data Saved Successfully.";

            //    projectDL.InsertReviewQuestionModel(reviewQuestionModel);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Sorry!! We couldnot save the Data." + ex.Message;
            }
        

        }
    }
}