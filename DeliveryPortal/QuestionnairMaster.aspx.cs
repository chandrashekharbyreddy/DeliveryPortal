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
    public partial class QuestionnairMaster : System.Web.UI.Page
    {
        ProjectDL _projectDL = new ProjectDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
               if (!IsPostBack)
                {
                    PopulateIDPs();
                    PopulateReviewTypes();

                    if (Request.QueryString["ID"] != null)
                    {
                        hidQuestionnaireId.Value = Request.QueryString["ID"];
                        GetQuestionnairDetail(int.Parse(hidQuestionnaireId.Value));
                    }
               }
            }
            
           catch (Exception ex)
            {
                lblMessage.Text = "Error fetching data  : " + ex.Message;
            }
            
        }

       

        private void PopulateIDPs()
        {
            drpIDP.DataSource = _projectDL.GetIDPs();
            drpIDP.DataTextField = "IDPName";
            drpIDP.DataValueField = "IDPId";
            drpIDP.DataBind();
            drpIDP.Items.Insert(0, new ListItem("--Select--", ""));
        }

        private void PopulateReviewTypes()
        {
            drpQuestionnairType.DataSource = _projectDL.GetReviewType();
            drpQuestionnairType.DataTextField = "ReviewTypeName";
            drpQuestionnairType.DataValueField = "ReviewTypeId";
            drpQuestionnairType.DataBind();
            drpQuestionnairType.Items.Insert(0, new ListItem("--Select--", ""));
        }

        private void GetQuestionnairDetail(int questionnairId)
        {
            QuestionnaireModel  questionnairModel = _projectDL.GetQuestionnairDetail(questionnairId);

            if (!string.IsNullOrEmpty(questionnairModel.QuestionnaireName))
            {
                txtQuestionName.Text = questionnairModel.QuestionnaireName.Trim();
            }

            drpIDP.SelectedValue = questionnairModel.IDPId.ToString();
            drpQuestionnairType.SelectedValue = questionnairModel.ReviewTypeId.ToString();

            if (questionnairModel.IsActive.HasValue)
            {
                chkIsActive.Checked = questionnairModel.IsActive.Value;
            }
            
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    QuestionnaireModel questionnairModel = new QuestionnaireModel();

                    if (!string.IsNullOrEmpty(Convert.ToString(txtQuestionName.Text)))
                    {
                        questionnairModel.QuestionnaireName = txtQuestionName.Text.Trim();
                    }

                    if (drpIDP.SelectedItem.Value != string.Empty)
                    {
                        questionnairModel.IDPId = Convert.ToInt32(drpIDP.SelectedItem.Value);
                    }

                    if (drpQuestionnairType.SelectedItem.Value != string.Empty)
                    {
                        questionnairModel.ReviewTypeId = Convert.ToInt32(drpQuestionnairType.SelectedItem.Value);
                    }

                    questionnairModel.IsActive = chkIsActive.Checked;




                    if (hidQuestionnaireId.Value != string.Empty)
                    {
                        questionnairModel.QuestionnaireId = int.Parse(hidQuestionnaireId.Value);
                        _projectDL.UpdateQuestionnairDetail(questionnairModel);
                    }
                    else
                    {
                        int newQuestionnairId = _projectDL.InsertQuestionnairDetails(questionnairModel);
                        hidQuestionnaireId.Value = newQuestionnairId.ToString();
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
            Response.Redirect("QuestionnaireList.aspx");
        }

        protected void txtQuestionName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}