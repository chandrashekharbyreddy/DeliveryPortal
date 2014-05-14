using DeliveryPortalEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace DeliveryPortalDL
{
    public class DEReviewDL
    {
        public List<DEAttributeModel> GetDEReviewDetails()
        {
            DashboardEntities context = new DashboardEntities();
            List<DEAttributeModel> attributes = new List<DEAttributeModel>();
            DEAttributeModel attribute = null;
            List<MST_ProjectAttributes> projectAttributes = context.MST_ProjectAttributes.Where(p => p.IsDE == true && (!p.EffectiveEndDate.HasValue ||
                (EntityFunctions.TruncateTime(p.EffectiveEndDate.Value) >= EntityFunctions.TruncateTime(DateTime.Now)))).ToList();
            foreach (MST_ProjectAttributes attributeEntity in projectAttributes)
            {
                attribute = new DEAttributeModel();
                attribute.AttributeId = attributeEntity.AttributeId;
                attribute.AttributeName = attributeEntity.AttributeName;
                attribute.SampleQuestions = attributeEntity.SampleQuestions;
                attributes.Add(attribute);
            }
            return attributes;
        }

        public int InsertDEReviewComments(DEReviewModel deReviewModel, List<DEAttributeModel> attributes)
        {
            DashboardEntities context = new DashboardEntities();
            Tran_Proj_DE_Attibute deAttribute = null;
            List<Tran_Proj_DE_Attibute> deAttributes = new List<Tran_Proj_DE_Attibute>();

            Tran_Proj_DE_Review projDEReview = new Tran_Proj_DE_Review();
            projDEReview.ProjectId = deReviewModel.ProjectId;
            projDEReview.DEReviewCalendarId = deReviewModel.DEReviewCalendarId;
            projDEReview.ReviewDate = deReviewModel.ReviewDate;
            projDEReview.LastUpdatedBy = deReviewModel.LastUpdatedBy;
            projDEReview.LastUpdatedDate = deReviewModel.LastUpdatedDate;

            foreach (DEAttributeModel attribute in attributes)
            {
                deAttribute = new Tran_Proj_DE_Attibute();
                deAttribute.AttributeId = attribute.AttributeId;
                deAttribute.FlagId = attribute.FlagId;
                deAttribute.Observations = attribute.Observations;
                deAttribute.Recommendations = attribute.Recommendations;
                deAttribute.LastUpdatedBy = attribute.LastUpdatedBy;
                deAttribute.LastUpdatedDate = attribute.LastUpdatedDate;
                projDEReview.Tran_Proj_DE_Attibute.Add(deAttribute);
            }

            context.Tran_Proj_DE_Review.Add(projDEReview);
            context.SaveChanges();

            return projDEReview.DEReviewId;
        }

        public void UpdateDEReviewComments(DEReviewModel deReviewModel, List<DEAttributeModel> attributes)
        {
            DashboardEntities context = new DashboardEntities();
            Tran_Proj_DE_Attibute deAttribute = null;
            List<Tran_Proj_DE_Attibute> deAttributes = new List<Tran_Proj_DE_Attibute>();

            Tran_Proj_DE_Review projDEReview = context.Tran_Proj_DE_Review.Where(p => p.DEReviewId == deReviewModel.DEReviewId).FirstOrDefault();
            if (projDEReview != null)
            {
                // Update project DE review properties
                projDEReview.DEReviewId = deReviewModel.DEReviewId;
                projDEReview.ProjectId = deReviewModel.ProjectId;
                projDEReview.ReviewDate = deReviewModel.ReviewDate;
                projDEReview.LastUpdatedBy = deReviewModel.LastUpdatedBy;
                projDEReview.LastUpdatedDate = deReviewModel.LastUpdatedDate;

                // Delete existing DE review attributes for that DEReview
                List<Tran_Proj_DE_Attibute> attributesToDelete = context.Tran_Proj_DE_Attibute.Where(p => p.DEReviewId == deReviewModel.DEReviewId).ToList();
                foreach (Tran_Proj_DE_Attibute attributeToDelete in attributesToDelete)
                {
                    projDEReview.Tran_Proj_DE_Attibute.Remove(attributeToDelete);
                    context.Entry(attributeToDelete).State = EntityState.Deleted;
                }

                // Insert new DE review attributes for that DEReview
                foreach (DEAttributeModel attribute in attributes)
                {
                    deAttribute = new Tran_Proj_DE_Attibute();
                    deAttribute.DEReviewId = deReviewModel.DEReviewId;
                    deAttribute.AttributeId = attribute.AttributeId;
                    deAttribute.FlagId = attribute.FlagId;
                    deAttribute.Observations = attribute.Observations;
                    deAttribute.Recommendations = attribute.Recommendations;
                    deAttribute.LastUpdatedBy = attribute.LastUpdatedBy;
                    deAttribute.LastUpdatedDate = attribute.LastUpdatedDate;
                    projDEReview.Tran_Proj_DE_Attibute.Add(deAttribute);
                }

                context.SaveChanges();
            }


        }

        public List<DEReviewModel> GetProjectDEReviewDetails(int projectId)
        {
            DashboardEntities context = new DashboardEntities();
            DEReviewModel deReview = null;
            List<DEReviewModel> deReviews = new List<DEReviewModel>();
            List<Tran_Proj_DE_Review> deProjectDEReviews = context.Tran_Proj_DE_Review.Where(p => p.ProjectId == projectId).OrderByDescending(o => o.ReviewDate).ToList();
            foreach (Tran_Proj_DE_Review deProjectDEReview in deProjectDEReviews)
            {
                if (deReviews.Where(d => d.ReviewDate == deProjectDEReview.ReviewDate).FirstOrDefault() == null)
                {
                    deReview = new DEReviewModel();
                    deReview.DEReviewId = deProjectDEReview.DEReviewId;
                    deReview.ProjectId = projectId;
                    deReview.ReviewDate = deProjectDEReview.ReviewDate;
                    deReviews.Add(deReview);
                }
            }
            return deReviews;
        }

        public List<DEAttributeModel> GetProjectDEReviewAttributeDetails(int deReviewId)
        {
            DashboardEntities context = new DashboardEntities();
            List<DEAttributeModel> deAttributes = new List<DEAttributeModel>();
            DEAttributeModel deAttribute = null;
            List<Tran_Proj_DE_Attibute> attributes = context.Tran_Proj_DE_Attibute.Where(a => a.DEReviewId == deReviewId).ToList();
            foreach (Tran_Proj_DE_Attibute attribute in attributes)
            {
                deAttribute = new DEAttributeModel();
                deAttribute.AttributeId = attribute.AttributeId;
                deAttribute.AttributeName = attribute.MST_ProjectAttributes.AttributeName;
                deAttribute.SampleQuestions = attribute.MST_ProjectAttributes.SampleQuestions;
                deAttribute.DEReviewId = attribute.DEReviewId.HasValue ? attribute.DEReviewId.Value : 0;
                deAttribute.FlagId = attribute.FlagId;
                deAttribute.Observations = attribute.Observations;
                deAttribute.Recommendations = attribute.Recommendations;
                deAttribute.CorrectiveActions = attribute.CorrectiveActions;
                deAttribute.ETA = attribute.ETA;
                deAttribute.ReviewStatusId = attribute.ReviewStatusId;
                deAttributes.Add(deAttribute);
            }
            return deAttributes;
        }

        public AttributeModel GetAttributeDetails(int attributeId)
        {
            DashboardEntities context = new DashboardEntities();
            AttributeModel attribute = context.MST_ProjectAttributes.Where(a => a.AttributeId == attributeId).Select(a => new AttributeModel { AttributeId = a.AttributeId, AttributeName = a.AttributeName, SampleQuestions = a.SampleQuestions }).FirstOrDefault();
            return attribute;
        }

        public void UpdateProjectDEReviewAttributeDetails(List<DEAttributeModel> attributes)
        {
            DashboardEntities context = new DashboardEntities();
            foreach (DEAttributeModel attribute in attributes)
            {
                Tran_Proj_DE_Attibute attr = context.Tran_Proj_DE_Attibute.Where(a => a.DEReviewId == attribute.DEReviewId && a.AttributeId == attribute.AttributeId).FirstOrDefault();                
                attr.CorrectiveActions = attribute.CorrectiveActions;
                attr.ETA = attribute.ETA;
                attr.LastUpdatedBy = attribute.LastUpdatedBy;
                attr.LastUpdatedDate = attribute.LastUpdatedDate;
            }

            context.SaveChanges();
        }

        public void UpdateProjectDEReviewStatus(List<DEAttributeModel> attributes)
        {
            DashboardEntities context = new DashboardEntities();
            int reviewIdStatusComplete = -1;
            int completedAttributes = -1;
            if (attributes != null && attributes.Count > 0 && context.MST_ReviewStatus.Where(r => r.ReviewStatusCode == "CMPL").FirstOrDefault() != null)
            {
                reviewIdStatusComplete = context.MST_ReviewStatus.Where(r => r.ReviewStatusCode == "CMPL").FirstOrDefault().ReviewStatusId;

                completedAttributes = attributes.Where(a => a.ReviewStatusId == reviewIdStatusComplete).Count();
            }
            foreach (DEAttributeModel attribute in attributes)
            {
                Tran_Proj_DE_Attibute attr = context.Tran_Proj_DE_Attibute.Where(a => a.DEReviewId == attribute.DEReviewId && a.AttributeId == attribute.AttributeId).FirstOrDefault();
                attr.ReviewStatusId = attribute.ReviewStatusId;
                attr.LastUpdatedBy = attribute.LastUpdatedBy;
                attr.LastUpdatedDate = attribute.LastUpdatedDate;
            }

            // If all the attributes status is complete, mark that DE Review as complete in Tran_DE_Calendar table
            if (attributes.Count > 0 && attributes.Count == completedAttributes)
            {
                int deReviewId = attributes[0].DEReviewId;
                Tran_Proj_DE_Review project = context.Tran_Proj_DE_Review.Where(p => p.DEReviewId == deReviewId).FirstOrDefault();
                if (project != null)
                {
                    project.Tran_DE_Calendar.ReviewStatusId = reviewIdStatusComplete;
                }
            }

            context.SaveChanges();
        }

        public DateTime? GetReviewDate(int deReviewId)
        {
            DashboardEntities context = new DashboardEntities();
            DateTime? reviewDate = DateTime.MinValue;
            Tran_Proj_DE_Review deReview = context.Tran_Proj_DE_Review.Where(p => p.DEReviewId == deReviewId).FirstOrDefault();
            return deReview.ReviewDate;
        }

        public List<AttributeModel> GetDEAttributes()
        {
            DashboardEntities context = new DashboardEntities();
            List<AttributeModel> attributes = context.MST_ProjectAttributes.Where(a => a.ParentAttributeId.HasValue).Select(a => new AttributeModel { AttributeId = a.AttributeId, AttributeName = a.AttributeName, SampleQuestions = a.SampleQuestions, ParentAttributeId = a.ParentAttributeId }).ToList();
            return attributes;
        }

        public List<DEAttributeModel> GetProjectDEAttributes(int? deReviewId)
        {
            DashboardEntities context = new DashboardEntities();
            List<DEAttributeModel> deAttributes = context.Tran_Proj_DE_Attibute.Where(de => de.DEReviewId == deReviewId).Select(r => new DEAttributeModel { AttributeId = r.AttributeId, FlagId = r.FlagId, Observations = r.Observations, Recommendations = r.Recommendations }).ToList();
            return deAttributes;
        }

        public List<FlagModel> GetAttributeFlags()
        {
            DashboardEntities context = new DashboardEntities();
            List<FlagModel> flags = context.MST_Flags.Select(f => new FlagModel { FlagId = f.FlagId, FlagName = f.FlagName }).ToList();
            return flags;
        }

        public List<AttributeModel> GetAttributeSummary()
        {
            DashboardEntities context = new DashboardEntities();
            List<AttributeModel> attributes = context.MST_ProjectAttributes.Select(a => new AttributeModel { AttributeName = a.AttributeName, AttributeId = a.AttributeId, ParentAttributeId = a.ParentAttributeId }).ToList();
            return attributes;
        }
        public List<DEReviewModel> GetDEReviewList()
        {
            DashboardEntities context = new DashboardEntities();
            List<DEReviewModel> deReviewModel = context.Tran_DE_Calendar.Select(a => new DEReviewModel { ProjectId = a.ProjectId, ProjectName = a.MST_Project.ProjectName, ScheduleDate = a.ReviewDate, DEReviewCalendarId = a.DEReviewCalendarId, DEReviewId = a.Tran_Proj_DE_Review.FirstOrDefault() != null ? a.Tran_Proj_DE_Review.FirstOrDefault().DEReviewId : 0, ReviewDate = a.Tran_Proj_DE_Review.FirstOrDefault() != null ? a.Tran_Proj_DE_Review.FirstOrDefault().ReviewDate : null }).ToList();
            return deReviewModel;
        }
        public List<DEReviewModel> SearchDEReviewList(string str)
        {
            DashboardEntities context = new DashboardEntities();
            List<DEReviewModel> deReviewModel = context.Tran_DE_Calendar.Where(a => a.MST_Project.ProjectName.Contains(str)).Select(a => new DEReviewModel { ProjectId = a.ProjectId, ProjectName = a.MST_Project.ProjectName, ScheduleDate = a.ReviewDate, ReviewDate = a.Tran_Proj_DE_Review.FirstOrDefault() != null ? a.Tran_Proj_DE_Review.FirstOrDefault().ReviewDate : null }).ToList();
            return deReviewModel;
        }

        public DEReviewModel GetDEReview(int DECalendarReviewId)
        {
            DashboardEntities context = new DashboardEntities();
            DEReviewModel deReviewModel = context.Tran_DE_Calendar.Where(a => a.DEReviewCalendarId == DECalendarReviewId).Select(a => new DEReviewModel { ProjectId = a.ProjectId, ProjectName = a.MST_Project.ProjectName, ScheduleDate = a.ReviewDate, ReviewDate = a.Tran_Proj_DE_Review.FirstOrDefault() != null ? a.Tran_Proj_DE_Review.FirstOrDefault().ReviewDate : null }).FirstOrDefault();
            return deReviewModel;
        }
    }
}
