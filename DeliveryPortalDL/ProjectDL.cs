using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryPortalEntities;
using System.Runtime.Remoting.Contexts;
using System.Globalization;
using System.Net.Mail;
using System.Net;
using System.Data;

namespace DeliveryPortalDL
{
    public class ProjectDL
    {
        DashboardEntities _context = new DashboardEntities();

        public List<AccountModel> GetAccounts()
        {
            List<AccountModel> accounts = null;
            accounts = _context.MST_Account.Select(a => new AccountModel { AccountId = a.AccountId, AccountName = a.AccountName }).ToList<AccountModel>();
            return accounts;
        }

        /// <summary>
        /// Search Device 
        /// </summary>
        /// <param name="RegionId"></param>
        /// <returns></returns>
        public List<SectorModel> GetSectors()
        {
            List<SectorModel> sectors = new List<SectorModel>();
            SectorModel sectorModel = new SectorModel();
            List<MST_Sector> sectorEntities = _context.MST_Sector.Where(s => s.ParentSectorId.HasValue).ToList();
            foreach (MST_Sector sector in sectorEntities)
            {
                MST_Sector sectorEntity = _context.MST_Sector.Where(s => s.SectorId == sector.ParentSectorId.Value).FirstOrDefault();
                sectorModel = new SectorModel();
                sectorModel.SectorId = sector.SectorId;
                sectorModel.SectorName = sectorEntity.SectorName + " - " + sector.SectorName;
                sectors.Add(sectorModel);
            }
            //sectors = _context.MST_Sector.Select(s => new SectorModel { SectorId = s.SectorId, SectorName = s.SectorName }).ToList<SectorModel>();
            return sectors;
        }

        public List<SectorModel> GetPrimarySectors()
        {
            List<SectorModel> sectors = new List<SectorModel>();
            SectorModel sectorModel = new SectorModel();
            List<MST_Sector> sectorEntities = _context.MST_Sector.Where(s => !s.ParentSectorId.HasValue).ToList();
            foreach (MST_Sector sector in sectorEntities)
            {
                sectorModel = new SectorModel();
                sectorModel.SectorId = sector.SectorId;
                sectorModel.SectorName = sector.SectorName;
                sectors.Add(sectorModel);
            }
            return sectors;
        }

        public List<SectorModel> GetSubSectors(int primarySectorId)
        {
            List<SectorModel> sectors = new List<SectorModel>();
            SectorModel sectorModel = new SectorModel();
            List<MST_Sector> sectorEntities = _context.MST_Sector.Where(s => s.ParentSectorId == primarySectorId).ToList();
            foreach (MST_Sector sector in sectorEntities)
            {
                sectorModel = new SectorModel();
                sectorModel.SectorId = sector.SectorId;
                sectorModel.SectorName = sector.SectorName;
                sectors.Add(sectorModel);
            }
            return sectors;
        }

        public List<IDPModel> GetIDPs()
        {
            List<IDPModel> idp = null;
            idp = _context.MST_IDP.Select(i => new IDPModel { IdpId = i.IDPId, IdpName = i.IDPName }).ToList<IDPModel>();
            return idp;
        }

               
        public List<EmployeeModel> GetEmployees()
        {
            List<EmployeeModel> employees = null;
            employees = _context.MST_Employee.OrderBy(o => o.EmployeeName).Select(e => new EmployeeModel { EmployeeId = e.EmployeeId, EmployeeName = e.EmployeeName }).ToList<EmployeeModel>();
            return employees;
        }

        public List<GeoModel> GetGeoLocations()
        {
            List<GeoModel> geo = null;
            geo = _context.MST_Geo.Select(g => new GeoModel { GeoId = g.GeoId, GeoName = g.GeoName }).ToList<GeoModel>();
            return geo;
        }

        public List<MethodologyModel> GetMethodologies()
        {
            List<MethodologyModel> methodology = null;
            methodology = _context.MST_Methodology.Select(m => new MethodologyModel { MethodologyId = m.MethodologyId, MethodologyName = m.MethodologyName }).ToList<MethodologyModel>();
            return methodology;
        }

        public List<NoWModel> GetNoWs()
        {
            List<NoWModel> nows = null;
            nows = _context.MST_NoW.Select(n => new NoWModel { NoWId = n.NoWId, NoWName = n.NoWName }).ToList<NoWModel>();
            return nows;
        }

        public List<ESTBasisModel> GetEstBasisModel()
        {
            List<ESTBasisModel> basis = null;
            basis = _context.MST_EstBasis.Select(b => new ESTBasisModel { EstBasisId = b.EstBasisId, EstBasisName = b.EstBasisName }).ToList<ESTBasisModel>();
            return basis;
        }

        public List<EmployeeModel> GetEmployeeModel()
        {
            List<MST_Employee> employee = null;
            List<EmployeeModel> employees = new List<EmployeeModel>();
            //employee = _context.MST_Employee.Select(b => new EmployeeModel { EmployeeId = b.EmployeeId, EmployeeName = b.EmployeeName, Designation = b.Designation, EmailId = b.EmailId, Location = b.Location, EmployeeCode=b.EmployeeCode }).ToList<EmployeeModel>();
            employee = _context.MST_Employee.Where(b => b.IsActive == true).ToList();

            foreach (MST_Employee emp in employee)
            { employees.Add(new EmployeeModel { EmployeeId = emp.EmployeeId, EmployeeCode = emp.EmployeeCode, EmailId = emp.EmailId, EmployeeName = emp.EmployeeName, Designation = emp.Designation, IsActive = emp.IsActive, Location = emp.Location }); }

            return employees;
        }

        public void DeleteEmployee(int id)
        {
            MST_Employee employee = _context.MST_Employee.Where(a => a.EmployeeId == id).FirstOrDefault();

            if (employee != null)
            {
                employee.IsActive = false;
                _context.SaveChanges();
            }
        }

        public List<ProjectModel> GetProjectModel()
        {
            List<ProjectModel> projectModel = null;
            projectModel = _context.MST_Project.Select(m => new ProjectModel { ProjectId = m.ProjectId, ProjectCode = m.ProjectCode, AccountId = m.AccountId, ProjectName = m.ProjectName, StartDate = m.StartDate, EndDate = m.EndDate, IsStrategic = m.IsStrategic }).ToList<ProjectModel>();
            return projectModel;
        }

        //public ProjectModel GetProjectDetails(int projectID)
        //{
        //    ProjectModel projectModel = null;
        //    projectModel = _context.MST_Project.Select(m => new ProjectModel { ProjectId = m.ProjectId, ProjectCode = m.ProjectCode, AccountId = m.AccountId, ProjectName = m.ProjectName, StartDate = m.StartDate, EndDate = m.EndDate, IsStrategic = m.IsStrategic }).Where(c => c.ProjectId == projectID).FirstOrDefault();
        //    return projectModel;
        //}

        //public List<DECalendarModel> 

        //public List<ProjectModel> GetDEModel()
        //{
        //    List<ProjectModel> projectModel = null;
        //    projectModel = _context.MST_Project.Select(m => new ProjectModel { ProjectId = m.ProjectId, ProjectCode = m.ProjectCode, AccountId = m.AccountId, ProjectName = m.ProjectName, StartDate = m.StartDate, EndDate = m.EndDate, IsStrategic = m.IsStrategic }).ToList<ProjectModel>();
        //    return projectModel;
        //}

        public List<DECalendarModel> GetDEModel()
        {
            List<DECalendarModel> calendarModel = null;
            calendarModel = _context.Tran_DE_Calendar.Select(m => new DECalendarModel
            {
                ProjectId = m.ProjectId,
                ReviewDate = m.ReviewDate,
                DEReviewCalendarId = m.DEReviewCalendarId,
                ReviewerName = m.MST_Employee.EmployeeName,
                ProjectCode = m.MST_Project.ProjectCode,
                ProjectName = m.MST_Project.ProjectName,
                ReviewStatusName = m.MST_ReviewStatus.ReviewStatusName
            }).ToList<DECalendarModel>();
            //    return projectModel;
            //   ReviewStatusName = m.MST_ReviewStatus.ReviewStatusName, ProjectName = m.MST_Project.ProjectName 
            return calendarModel;
        }

        public List<AttributeModel> GetProjectAttributesList()
        {
            List<MST_ProjectAttributes> projectAttributes = null;
            List<AttributeModel> attributeModel = new List<AttributeModel>();
            projectAttributes = _context.MST_ProjectAttributes.Where(a => a.IsActive == true).ToList();

            foreach (MST_ProjectAttributes projectAttribute in projectAttributes)
            {
                attributeModel.Add(new AttributeModel { AttributeId = projectAttribute.AttributeId, AttributeName = projectAttribute.AttributeName, ParentAttributeId = projectAttribute.ParentAttributeId, IsDE = projectAttribute.IsDE, IsActive = projectAttribute.IsActive, EffectiveStartDate = projectAttribute.EffectiveStartDate, EffectiveEndDate = projectAttribute.EffectiveEndDate });
            }
            return attributeModel;
        }
        public int InsertProjectDetails(ProjectModel projectModel)
        {
            int newProjectId = -1;
            MST_Project project = new MST_Project();
            project.ProjectCode = projectModel.ProjectCode;
            project.AccountId = projectModel.AccountId;
            project.ProjectName = projectModel.ProjectName;
            project.IsActive = true;
            if (projectModel.IdpId.HasValue)
            {
                project.IDPId = projectModel.IdpId;
            }
            if (projectModel.EMId.HasValue)
            {
                project.EMId = projectModel.EMId;
            }
            if (projectModel.PMID.HasValue)
            {
                project.PMId = projectModel.PMID;
            }
            if (projectModel.GeoId.HasValue)
            {
                project.GeoId = projectModel.GeoId;
            }
            if (projectModel.SectorId.HasValue)
            {
                project.SectorId = projectModel.SectorId;
            }
            if (projectModel.StartDate.HasValue)
            {
                project.StartDate = projectModel.StartDate;
            }
            if (projectModel.EndDate.HasValue)
            {
                project.EndDate = projectModel.EndDate;
            }
            if (projectModel.MethodologyId.HasValue)
            {
                project.MethodologyId = projectModel.MethodologyId;
            }
            if (projectModel.NoWId.HasValue)
            {
                project.NoWId = projectModel.NoWId;
            }
            if (projectModel.EstBasisId.HasValue)
            {
                project.EstBasisId = projectModel.EstBasisId;
            }
            if (projectModel.IsStrategic.HasValue)
            {
                project.IsStrategic = projectModel.IsStrategic;
            }
            //if (projectModel.IsRA.HasValue)
            //{
            //    project.IsRA = projectModel.IsRA;
            //}
            project.LastUpdateDate = projectModel.LastUpdateDate;
            project.LastUpdatedBy = projectModel.LastUpdatedBy;

            _context.MST_Project.Add(project);
            _context.SaveChanges();

            newProjectId = project.ProjectId;
            return newProjectId;
        }

        public ProjectModel GetProjectDetails(int projectId)
        {
            MST_Project projectRecord = _context.MST_Project.Where(p => p.ProjectId == projectId).FirstOrDefault();

            return ConvertProjectEntityToProjectModel(projectRecord);
        }

        public List<ProjectModel> SearchProjects(string projectCode, string projectName)
        {
            List<ProjectModel> projects = new List<ProjectModel>();
            List<MST_Project> projectsEntities = new List<MST_Project>();

            if (projectCode != string.Empty && projectName != string.Empty)
            {
                projectsEntities = _context.MST_Project.Where(p => p.ProjectCode.Contains(projectCode) && p.ProjectName.Contains(projectName)).ToList();
            }
            else if (projectCode != string.Empty)
            {
                projectsEntities = _context.MST_Project.Where(p => p.ProjectCode.Contains(projectCode)).ToList();
            }
            else if (projectName != string.Empty)
            {
                projectsEntities = _context.MST_Project.Where(p => p.ProjectName.Contains(projectName)).ToList();
            }
            else
            {
                //projectsEntities = _context.MST_Project.ToList();
                projectsEntities = _context.MST_Project.Where(p => p.IsActive == true).ToList();
            }

            foreach (MST_Project projectEntity in projectsEntities)
            {
                projects.Add(ConvertProjectEntityToProjectModel(projectEntity));
            }

            return projects;
        }


        public List<QuestionnaireModel> SearchQuestionnair(string questionnairName, string reviewtype)
        {
            List<QuestionnaireModel> questionnairList = new List<QuestionnaireModel>();
            List<MST_Questionnaire> questionnairEntities = new List<MST_Questionnaire>();
            
           

            if (questionnairName != string.Empty && reviewtype != string.Empty)
            {
                questionnairEntities = _context.MST_Questionnaire.Where(q => q.QuestionnaireName.Contains(questionnairName) && q.MST_ReviewType.ReviewTypeName.Contains(reviewtype)).ToList();
            }
            else if (questionnairName != string.Empty)
            {
                questionnairEntities = _context.MST_Questionnaire.Where(q => q.QuestionnaireName.Contains(questionnairName)).ToList();
            }
            else if (reviewtype != string.Empty)
            {
                questionnairEntities = _context.MST_Questionnaire.Where(q => q.MST_ReviewType.ReviewTypeName.Contains(reviewtype)).ToList();
            }
            else
            {
                //projectsEntities = _context.MST_Project.ToList();
                questionnairEntities = _context.MST_Questionnaire.Where(q => q.IsActive == true).ToList();
            }

            foreach (MST_Questionnaire questionnairEntity in questionnairEntities)
            {
                questionnairList.Add(new QuestionnaireModel() { QuestionnaireName = questionnairEntity.QuestionnaireName, ReviewTypeName = questionnairEntity.MST_ReviewType.ReviewTypeName, IDPName = questionnairEntity.MST_IDP.IDPName, IsActive = questionnairEntity.IsActive ,QuestionnaireId=questionnairEntity.QuestionnaireId,ReviewTypeId=questionnairEntity.ReviewTypeId,IDPId=questionnairEntity.IDPId });
            }

            return questionnairList;
        }

        public List<ProjectModel> ProjectsList()
        {
            List<ProjectModel> projects = new List<ProjectModel>();
            List<MST_Project> projectsEntities = new List<MST_Project>();
            projectsEntities = _context.MST_Project.ToList();

            foreach (MST_Project projectEntity in projectsEntities)
            {
                projects.Add(new ProjectModel { ProjectId = projectEntity.ProjectId, ProjectName = projectEntity.ProjectName });
            }


            return projects;
        }

        public List<EmployeeModel> EmployeeList()
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();
            List<MST_Employee> employeesEntities = new List<MST_Employee>();
            employeesEntities = _context.MST_Employee.ToList();

            foreach (MST_Employee employeeEntity in employeesEntities)
            {
                employees.Add(new EmployeeModel { EmployeeName = employeeEntity.EmployeeName, EmployeeId = employeeEntity.EmployeeId });
            }

            return employees;
        }

        private static ProjectModel ConvertProjectEntityToProjectModel(MST_Project projectRecord)
        {
            ProjectModel project = new ProjectModel();
            project.ProjectId = projectRecord.ProjectId;
            project.ProjectCode = projectRecord.ProjectCode;
            project.ProjectName = projectRecord.ProjectName;
            project.AccountId = projectRecord.AccountId;
            project.IdpId = projectRecord.IDPId;
            project.EMId = projectRecord.EMId;
            project.PMID = projectRecord.PMId;
            project.GeoId = projectRecord.GeoId;
            project.SectorId = projectRecord.SectorId;
            project.StartDate = projectRecord.StartDate;
            project.EndDate = projectRecord.EndDate;
            project.MethodologyId = projectRecord.MethodologyId;
            project.NoWId = projectRecord.NoWId;
            project.EstBasisId = projectRecord.EstBasisId;

            project.LastDEReviewsDate = projectRecord.LastDEReviewDate;
            project.LastDQADate = projectRecord.LastIDQADate;
            project.LastSMRDate = projectRecord.LastSMRDate;
            project.IsStrategic = projectRecord.IsStrategic;
            //project.IsRA = projectRecord.IsRA;

            return project;
        }

        public void UpdateProjectDetails(ProjectModel projectModel)
        {
            MST_Project project = _context.MST_Project.Where(p => p.ProjectId == projectModel.ProjectId).FirstOrDefault();

            if (project != null)
            {
                project.ProjectCode = projectModel.ProjectCode;
                project.AccountId = projectModel.AccountId;
                project.ProjectName = projectModel.ProjectName;
                if (projectModel.IdpId.HasValue)
                {
                    project.IDPId = projectModel.IdpId;
                }
                else
                {
                    project.IDPId = null;
                }
                if (projectModel.EMId.HasValue)
                {
                    project.EMId = projectModel.EMId;
                }
                else
                {
                    project.EMId = null;
                }
                if (projectModel.PMID.HasValue)
                {
                    project.PMId = projectModel.PMID;
                }
                else
                {
                    project.PMId = null;
                }
                if (projectModel.GeoId.HasValue)
                {
                    project.GeoId = projectModel.GeoId;
                }
                else
                {
                    project.GeoId = null;
                }
                if (projectModel.SectorId.HasValue)
                {
                    project.SectorId = projectModel.SectorId;
                }
                else
                {
                    project.SectorId = null;
                }
                if (projectModel.StartDate.HasValue)
                {
                    project.StartDate = projectModel.StartDate;
                }
                else
                {
                    project.StartDate = null;
                }
                if (projectModel.EndDate.HasValue)
                {
                    project.EndDate = projectModel.EndDate;
                }
                else
                {
                    project.EndDate = null;
                }
                if (projectModel.MethodologyId.HasValue)
                {
                    project.MethodologyId = projectModel.MethodologyId;
                }
                else
                {
                    project.MethodologyId = null;
                }
                if (projectModel.NoWId.HasValue)
                {
                    project.NoWId = projectModel.NoWId;
                }
                else
                {
                    project.NoWId = null;
                }
                if (projectModel.EstBasisId.HasValue)
                {
                    project.EstBasisId = projectModel.EstBasisId;
                }
                else
                {
                    project.EstBasisId = null;
                }
                if (projectModel.IsStrategic.HasValue)
                {
                    project.IsStrategic = projectModel.IsStrategic;
                }
                else
                {
                    project.IsStrategic = null;
                }
                //if (projectModel.IsRA.HasValue)
                //{
                //    project.IsRA = projectModel.IsRA;
                //}
                //else
                //{
                //    project.IsRA = null;
                //}
                project.LastUpdateDate = projectModel.LastUpdateDate;
                //project.LastUpdateDate = DateTime.Now;
                project.LastUpdatedBy = projectModel.LastUpdatedBy;

                _context.SaveChanges();
            }

        }
        public int InsertProjectAttribute(AttributeModel attributeModel)
        {
            int newAttributeId = -1;
            MST_ProjectAttributes projectAttributes = new MST_ProjectAttributes();
            projectAttributes.AttributeName = attributeModel.AttributeName;
            if (attributeModel.ParentAttributeId.HasValue)
            {
                projectAttributes.ParentAttributeId = attributeModel.ParentAttributeId.Value;
            }
            projectAttributes.IsDE = attributeModel.IsDE;
            if (attributeModel.EffectiveStartDate.HasValue)
            {
                projectAttributes.EffectiveStartDate = attributeModel.EffectiveStartDate;
            }
            if (attributeModel.EffectiveEndDate.HasValue)
            {
                projectAttributes.EffectiveEndDate = attributeModel.EffectiveEndDate;
            }
            projectAttributes.LastUpdatedDate = attributeModel.LastUpdatedDate;
            projectAttributes.IsActive = true;
            projectAttributes.LastUpdateBy = attributeModel.LastUpdateBy;
            _context.MST_ProjectAttributes.Add(projectAttributes);
            _context.SaveChanges();

            newAttributeId = projectAttributes.AttributeId;
            return newAttributeId;

        }
        public AttributeModel GetProjectAttribute(int attributeId)
        {
            AttributeModel attributeModel = new AttributeModel();

            MST_ProjectAttributes projectAttributes = _context.MST_ProjectAttributes.Where(a => a.AttributeId == attributeId).FirstOrDefault();
            attributeModel.AttributeName = projectAttributes.AttributeName;
            attributeModel.ParentAttributeId = projectAttributes.ParentAttributeId;
            attributeModel.IsDE = projectAttributes.IsDE;
            attributeModel.EffectiveStartDate = projectAttributes.EffectiveStartDate;
            attributeModel.EffectiveEndDate = projectAttributes.EffectiveEndDate;
            attributeModel.SampleQuestions = projectAttributes.SampleQuestions;
            return attributeModel;
        }
        public ReviewQuestionModel GetReviewQuestionForQuestionId(int questionId)
        {
            ReviewQuestionModel reviewQuestionModel=new ReviewQuestionModel();
            MST_ReviewQuestion reviewQuestion = _context.MST_ReviewQuestion.Where(m => m.QuestionId == questionId).FirstOrDefault();
            MST_Questionnaire QuestionnaireName = _context.MST_Questionnaire.Where(c => c.QuestionnaireId == reviewQuestion.QuestionnaireId).FirstOrDefault();
            reviewQuestionModel.QuestionnaireName = QuestionnaireName.QuestionnaireName;
            reviewQuestionModel.QuestionDescription = reviewQuestion.QuestionDescription;
            reviewQuestionModel.QuestionGuideLines = reviewQuestion.QuestionGuideLines;
            MST_ProjectAttributes attributeName = _context.MST_ProjectAttributes.Where(a => a.AttributeId == reviewQuestion.AttributeId).FirstOrDefault();
            reviewQuestionModel.AttributeName = attributeName.AttributeName;
            reviewQuestionModel.AttributeId = reviewQuestion.AttributeId;
            reviewQuestionModel.QuestionnaireId = reviewQuestion.QuestionnaireId;
            return reviewQuestionModel;
        }
        public void UpdateProjectAttribute(AttributeModel attributeModel)
        {
            MST_ProjectAttributes projectAttributes = _context.MST_ProjectAttributes.Where(a => a.AttributeId == attributeModel.AttributeId).FirstOrDefault();

            if (projectAttributes != null)
            {
                projectAttributes.AttributeName = attributeModel.AttributeName;
                projectAttributes.ParentAttributeId = attributeModel.ParentAttributeId;
                projectAttributes.IsDE = attributeModel.IsDE;
                projectAttributes.EffectiveStartDate = attributeModel.EffectiveStartDate;
                projectAttributes.EffectiveEndDate = attributeModel.EffectiveEndDate;
                projectAttributes.SampleQuestions = attributeModel.SampleQuestions;
                projectAttributes.LastUpdatedDate = attributeModel.LastUpdatedDate;
                projectAttributes.LastUpdateBy = attributeModel.LastUpdateBy;

                _context.SaveChanges();
            }
        }

        public void UpdateProjAttributes(TranProjIDPAttributesModel tranProjIDPAttr)
        {
            Tran_Proj_IDP_Attributes tranProjIDPAttribute = _context.Tran_Proj_IDP_Attributes.Where(a => a.AttributeId == tranProjIDPAttr.AttributeId && a.ProjectId == tranProjIDPAttr.ProjectId).FirstOrDefault();

            if (tranProjIDPAttribute != null)
                {
                    tranProjIDPAttribute.AttributeValueId = tranProjIDPAttr.AttributeValueId;
                    tranProjIDPAttribute.AttributeTextValue = tranProjIDPAttr.AttributeTextValue;
                    tranProjIDPAttribute.LastUpdatedBy = tranProjIDPAttr.LastUpdatedBy;
                    tranProjIDPAttribute.LastUpdateDate = tranProjIDPAttr.LastUpdateDate;
                    _context.SaveChanges();
                }
            
           
        }


        public List<AttributeModel> SearchProjectAttributes(string name)
        {
            List<MST_ProjectAttributes> projectAttributes = null;
            List<AttributeModel> attributeModel = new List<AttributeModel>();
            projectAttributes = _context.MST_ProjectAttributes.Where(a => a.AttributeName.Contains(name) && a.IsActive == true).ToList();

            foreach (MST_ProjectAttributes projectAttribute in projectAttributes)
            {
                attributeModel.Add(new AttributeModel { AttributeId = projectAttribute.AttributeId, AttributeName = projectAttribute.AttributeName, ParentAttributeId = projectAttribute.ParentAttributeId, IsDE = projectAttribute.IsDE, EffectiveStartDate = projectAttribute.EffectiveStartDate, EffectiveEndDate = projectAttribute.EffectiveEndDate });
            }
            return attributeModel;
        }

        public List<ReviewQuestionModel> SearchQuestion(string name)
        {
            List<MST_ReviewQuestion> reviewQuestion = null;
            List<ReviewQuestionModel> reviewQuestionModel = new List<ReviewQuestionModel>();
            reviewQuestion = _context.MST_ReviewQuestion.Where(a => a.QuestionDescription.Contains(name) && a.IsActive==true).ToList();

            foreach (MST_ReviewQuestion question in reviewQuestion)
            {
                reviewQuestionModel.Add(new ReviewQuestionModel { QuestionId=question.QuestionId,QuestionnaireId = question.QuestionnaireId, QuestionDescription = question.QuestionDescription, QuestionGuideLines = question.QuestionGuideLines, AttributeName = question.MST_ProjectAttributes.AttributeName, IsActive = question.IsActive, QuestionnaireName = question.MST_Questionnaire.QuestionnaireName });
            }
            return reviewQuestionModel;
        }


        public List<AttributeModel> getParentAttributeIdList(int attributeId)
        {
            List<AttributeModel> attributeModel = new List<AttributeModel>();
            List<MST_ProjectAttributes> projectAttributes = null;

            projectAttributes = _context.MST_ProjectAttributes.Where(a => (a.AttributeId != attributeId && a.IsActive == true)).ToList();



            foreach (MST_ProjectAttributes projectattribute in projectAttributes)
            {
                attributeModel.Add(new AttributeModel { AttributeId = projectattribute.AttributeId, AttributeName = projectattribute.AttributeName });
            }
            return attributeModel;
        }

        public int InsertEmployeeDetails(EmployeeModel employee)
        {
            int newEmployeeId = -1;
            MST_Employee emp = new MST_Employee();
            emp.EmployeeName = employee.EmployeeName;
            emp.EmailId = employee.EmailId;
            emp.Location = employee.Location;
            emp.Designation = employee.Designation;
            emp.WindowsId = employee.WindowsId;
            emp.EmployeeCode = employee.EmployeeCode;
            emp.IsActive = true;

            _context.MST_Employee.Add(emp);
            _context.SaveChanges();
            newEmployeeId = emp.EmployeeId;
            return newEmployeeId;

        }
        public int InsertAccountDetails(AccountModel account)
        {
            int newAccountId = -1;
            MST_Account acc = new MST_Account();
            acc.IsActive = true;
            acc.AccountName = account.AccountName;
            acc.LastUpdatedDate = DateTime.Now;
            acc.LastUpdateBy = account.LastUpdateBy;
            if (account.GeoID.HasValue)
            {
                acc.GeoID = account.GeoID;
            }
            if (account.SectorID.HasValue)
            {
                acc.SectorID = account.SectorID;
            }
            if (account.SectorID2.HasValue)
            {
                acc.SectorID2 = account.SectorID2;
            }
            _context.MST_Account.Add(acc);
            _context.SaveChanges();

            newAccountId = acc.AccountId;
            return newAccountId;


        }
        public void UpdateEmployeeDetails(EmployeeModel employee)
        {
            MST_Employee emp = _context.MST_Employee.Where(p => p.EmployeeId == employee.EmployeeId).FirstOrDefault();
            if (emp != null)
            {
                emp.EmployeeName = employee.EmployeeName;
                emp.EmailId = employee.EmailId;
                emp.Location = employee.Location;
                emp.Designation = employee.Designation;
                emp.WindowsId = employee.WindowsId;
                emp.EmployeeCode = employee.EmployeeCode;
                _context.SaveChanges();
            }
        }

        public void UpdateAccountDetails(AccountModel account)
        {
            MST_Account acc = _context.MST_Account.Where(p => p.AccountId == account.AccountId).FirstOrDefault();
            if (acc != null)
            {
                acc.AccountName = account.AccountName;
                acc.LastUpdatedDate = DateTime.Now;
                acc.LastUpdateBy = account.LastUpdateBy;
                if (account.GeoID.HasValue)
                {
                    acc.GeoID = account.GeoID;
                }
                else
                {
                    acc.GeoID = null;
                }
                if (account.SectorID.HasValue)
                {
                    acc.SectorID = account.SectorID;
                }
                else
                {
                    acc.SectorID = null;
                }
                if (account.SectorID2.HasValue)
                {
                    acc.SectorID2 = account.SectorID2;
                }
                else
                {
                    acc.SectorID2 = null;
                }

                _context.SaveChanges();
            }
        }

        public EmployeeModel GetEmployeeDetails(int employeeId)
        {
            MST_Employee employeeRecord = _context.MST_Employee.Where(p => p.EmployeeId == employeeId).FirstOrDefault();

            return ConvertEmployeeEntityToEmployeeModel(employeeRecord);
        }

        public AccountModel GetAccountDetails(int accountId)
        {
            MST_Account accountRecord = _context.MST_Account.Where(p => p.AccountId == accountId).FirstOrDefault();

            return ConvertAccountEntityToAccountModel(accountRecord);
        }

        private AccountModel ConvertAccountEntityToAccountModel(MST_Account accountRecord)
        {
            AccountModel account = new AccountModel();
            account.AccountName = accountRecord.AccountName;
            account.AccountId = accountRecord.AccountId;
            account.GeoID = accountRecord.GeoID;
            account.SectorID = accountRecord.SectorID;
            account.SectorID2 = accountRecord.SectorID2;
            account.LastUpdateBy = accountRecord.LastUpdateBy;
            account.LastUpdatedDate = accountRecord.LastUpdatedDate;
            account.IsActive = accountRecord.IsActive;
            return account;
        }

        private EmployeeModel ConvertEmployeeEntityToEmployeeModel(MST_Employee employeeRecord)
        {
            EmployeeModel employee = new EmployeeModel();
            employee.EmployeeId = employeeRecord.EmployeeId;
            employee.EmployeeName = employeeRecord.EmployeeName;
            employee.Location = employeeRecord.Location;
            employee.EmailId = employeeRecord.EmailId;
            employee.Designation = employeeRecord.Designation;
            employee.EmployeeCode = employeeRecord.EmployeeCode;
            employee.IsActive = employeeRecord.IsActive;
            employee.WindowsId = employeeRecord.WindowsId;
            return employee;
        }

        public List<EmployeeModel> SearchEmployee(string empCode, string employeeName)
        {
            List<EmployeeModel> employee = new List<EmployeeModel>();
            List<MST_Employee> employeeEntities = new List<MST_Employee>();

            if (empCode != string.Empty && employeeName != string.Empty)
            {

                employeeEntities = _context.MST_Employee.Where(p => p.EmployeeCode.Contains(empCode) && p.EmployeeName.Contains(employeeName) && p.IsActive == true).ToList();
            }
            else if (empCode != string.Empty)
            {
                employeeEntities = _context.MST_Employee.Where(p => p.EmployeeCode.Contains(empCode) && p.IsActive == true).ToList();
            }
            else if (employeeName != string.Empty)
            {
                employeeEntities = _context.MST_Employee.Where(p => p.EmployeeName.Contains(employeeName) && p.IsActive == true).ToList();
            }
            else
            {
                employeeEntities = _context.MST_Employee.Where(p => p.IsActive == true).ToList();
            }

            foreach (MST_Employee employeeEntity in employeeEntities)
            {

                employee.Add(ConvertEmployeeEntityToEmployeeModel(employeeEntity));
            }

            return employee;
        }

        private DECalendarModel ConvertDECalendarEntityToDECalendarModel(Tran_DE_Calendar deCalendarRecord)
        {
            DECalendarModel deCalendar = new DECalendarModel();
            deCalendar.ReviewerName = deCalendarRecord.MST_Employee.EmployeeName;
            deCalendar.DEReviewCalendarId = deCalendarRecord.DEReviewCalendarId;
            deCalendar.ProjectCode = deCalendarRecord.MST_Project.ProjectCode;
            deCalendar.ProjectName = deCalendarRecord.MST_Project.ProjectName;
            deCalendar.ReviewDate = deCalendarRecord.ReviewDate;
            deCalendar.ReviewStatusName = deCalendarRecord.MST_ReviewStatus.ReviewStatusName;
            return deCalendar;
        }

        public List<DECalendarModel> SearchDECalendar(string projectCode, string projectName, DateTime? date)
        {
            List<DECalendarModel> deCalendar = new List<DECalendarModel>();
            List<Tran_DE_Calendar> deCalendarEntities = new List<Tran_DE_Calendar>();

            if (projectCode != string.Empty && projectName != string.Empty && date.HasValue && date != DateTime.MinValue)
            {
                //int projectCode = int.Parse(projectCode);
                deCalendarEntities = _context.Tran_DE_Calendar.Where(p => p.MST_Project.ProjectCode.Contains(projectCode) && p.MST_Project.ProjectName.Contains(projectName) && p.ReviewDate == date).ToList();
            }
            else if (projectCode != string.Empty && projectName != string.Empty && date == null)
            {
                // int projectCode = int.Parse(projectCode);
                deCalendarEntities = _context.Tran_DE_Calendar.Where(p => p.MST_Project.ProjectCode.Contains(projectCode) && p.MST_Project.ProjectName.Contains(projectName)).ToList();
                //employeeEntities = _context.MST_Employee.Where(p => p.EmployeeIdContains(employeeId)).ToList();
            }
            else if (projectCode == string.Empty && projectName != string.Empty && date.HasValue && date != DateTime.MinValue)
            {
                // int projectCode = int.Parse(projectCode);
                deCalendarEntities = _context.Tran_DE_Calendar.Where(p => p.MST_Project.ProjectName.Contains(projectName) && p.ReviewDate == date).ToList();
                //employeeEntities = _context.MST_Employee.Where(p => p.EmployeeIdContains(employeeId)).ToList();
            }
            else if (projectCode != string.Empty && projectName == string.Empty && date.HasValue && date != DateTime.MinValue)
            {
                // int projectCode = int.Parse(projectCode);
                deCalendarEntities = _context.Tran_DE_Calendar.Where(p => p.MST_Project.ProjectCode.Contains(projectCode) && p.ReviewDate == date).ToList();
                //employeeEntities = _context.MST_Employee.Where(p => p.EmployeeIdContains(employeeId)).ToList();
            }
            else if (projectName != string.Empty)
            {
                deCalendarEntities = _context.Tran_DE_Calendar.Where(p => p.MST_Project.ProjectName.Contains(projectName)).ToList();
            }
            else if (date.HasValue && date != DateTime.MinValue)
            {
                deCalendarEntities = _context.Tran_DE_Calendar.Where(p => p.ReviewDate == date).ToList();
            }
            else if (projectCode != string.Empty)
            {
                deCalendarEntities = _context.Tran_DE_Calendar.Where(p => p.MST_Project.ProjectCode.Contains(projectCode)).ToList();
            }
            else
            {
                deCalendarEntities = _context.Tran_DE_Calendar.ToList();
            }

            foreach (Tran_DE_Calendar deCalendarEntity in deCalendarEntities)
            {

                deCalendar.Add(ConvertDECalendarEntityToDECalendarModel(deCalendarEntity));
            }

            return deCalendar;
        }

        public void DeleteProject(int id)
        {
            MST_Project projectRecord = _context.MST_Project.Where(p => p.ProjectId == id).FirstOrDefault();
            // _context.MST_Project.Remove(projectRecord);

            if (projectRecord != null)
            {
                //project.ProjectCode = projectModel.ProjectCode;
                projectRecord.IsActive = false;
                // projectRecord.IsStrategic = false;


                _context.SaveChanges();
            }
        }

        public void DeleteDECalendar(int id)
        {
            Tran_DE_Calendar deCalendarRecord = _context.Tran_DE_Calendar.Where(p => p.DEReviewCalendarId == id).FirstOrDefault();
            _context.Tran_DE_Calendar.Remove(deCalendarRecord);
            _context.SaveChanges();
        }

        public void DeleteAttribute(int attributeId)
        {
            MST_ProjectAttributes projectAttributes = _context.MST_ProjectAttributes.Where(a => a.AttributeId == attributeId).FirstOrDefault();

            if (projectAttributes != null)
            {
                projectAttributes.IsActive = false;
                _context.SaveChanges();
            }
        }

        public List<ReviewStatusModel> GetStatus()
        {
            List<ReviewStatusModel> reviewStatusModel = new List<ReviewStatusModel>();
            List<MST_ReviewStatus> mstreviewStatus = null;
            mstreviewStatus = _context.MST_ReviewStatus.ToList();

            foreach (MST_ReviewStatus reviewStatus in mstreviewStatus)
            {
                reviewStatusModel.Add(new ReviewStatusModel { ReviewStatusId = reviewStatus.ReviewStatusId, ReviewStatusName = reviewStatus.ReviewStatusName });
            }

            return reviewStatusModel;
        }
        public void UpdateDECalendar(DECalendarModel deCalendar)
        {
            Tran_DE_Calendar tranDECalendar = _context.Tran_DE_Calendar.Where(m => m.DEReviewCalendarId == deCalendar.DEReviewCalendarId).FirstOrDefault();

            if (tranDECalendar != null)
            {
                tranDECalendar.DEReviewCalendarId = deCalendar.DEReviewCalendarId;
                tranDECalendar.ProjectId = deCalendar.ProjectId;
                tranDECalendar.ReviewDate = deCalendar.ReviewDate;
                tranDECalendar.EmployeeId = deCalendar.EmployeeId;
                tranDECalendar.ReviewStatusId = deCalendar.ReviewStatusId;
            }
            _context.SaveChanges();
        }

        public int InsertDECalendar(DECalendarModel deCalendar)
        {
            int newDEReviewCalendarId = -1;
            Tran_DE_Calendar tranDECalendar = new Tran_DE_Calendar();

            tranDECalendar.ProjectId = deCalendar.ProjectId;
            tranDECalendar.ReviewDate = deCalendar.ReviewDate;
            tranDECalendar.EmployeeId = deCalendar.EmployeeId;
            tranDECalendar.ReviewStatusId = deCalendar.ReviewStatusId;
            _context.Tran_DE_Calendar.Add(tranDECalendar);
            _context.SaveChanges();

            newDEReviewCalendarId = deCalendar.DEReviewCalendarId;
            return newDEReviewCalendarId;


        }
        public DECalendarModel GetDECalendarModel(int DEReviewCalendarId)
        {

            DECalendarModel decalendar = new DECalendarModel();
            Tran_DE_Calendar tranDECalendar = new Tran_DE_Calendar();

            tranDECalendar = _context.Tran_DE_Calendar.Where(a => a.DEReviewCalendarId == DEReviewCalendarId).FirstOrDefault();
            decalendar.DEReviewCalendarId = DEReviewCalendarId;
            decalendar.ProjectId = tranDECalendar.ProjectId;
            decalendar.EmployeeId = tranDECalendar.EmployeeId;
            decalendar.ReviewDate = tranDECalendar.ReviewDate;
            decalendar.ReviewStatusId = tranDECalendar.ReviewStatusId;

            return decalendar;
        }
        public List<AccountModel> GetAccountData()
        {
            List<MST_Account> mstAccount = null;
            List<AccountModel> account = new List<AccountModel>();
            mstAccount = _context.MST_Account.Where(p => p.IsActive == true).ToList();
            //foreach (MST_Account mAccount in mstAccount)
            //{
            //    AccountModel acc = new AccountModel();
            //    acc.AccountId = mAccount.AccountId;
            //    acc.AccountName = mAccount.AccountName;
            //    if (mAccount.LastUpdateBy.HasValue)
            //    {
            //        acc.LastUpdateBy = mAccount.LastUpdateBy;

            //    }
            //    if (mAccount.LastUpdatedDate.HasValue)
            //    {
            //        acc.LastUpdatedDate = mAccount.LastUpdatedDate;
            //    }
            //    acc.GeoID = mAccount.GeoID;
            //    acc.SectorID = mAccount.SectorID;
            //    acc.SectorID2 = mAccount.SectorID2;
            //    //acc.Geography = acc.MST_Geo.GeoName;
            //    //ProjectName = a.MST_Project.ProjectName
            //    account.Add(acc);

            //}
            account = _context.MST_Account.Select(a => new AccountModel { AccountId = a.AccountId, AccountName = a.AccountName, GeoID = a.GeoID, SectorID = a.SectorID, SectorID2 = a.SectorID2, Geography = a.MST_Geo.GeoName, SectorName = a.MST_Sector.SectorName, SubSectorName=a.MST_Sector1.SectorName,LastUpdatedDate = a.LastUpdatedDate }).ToList();
            return account;
        }
        public List<AccountModel> SearchAccountData(string name)
        {
            List<MST_Account> mstAccount = null;
            List<AccountModel> account = new List<AccountModel>();
            //mstAccount = _context.MST_Account.Where(p => p.AccountName.Contains(name) && p.IsActive == true).ToList();

            //foreach (MST_Account mAccount in mstAccount)
            //{
            //    AccountModel acc = new AccountModel();
            //    acc.AccountId = mAccount.AccountId;
            //    acc.AccountName = mAccount.AccountName;
            //    if (mAccount.LastUpdateBy.HasValue)
            //    {
            //        acc.LastUpdateBy = mAccount.LastUpdateBy;

            //    }
            //    if (mAccount.LastUpdatedDate.HasValue)
            //    {
            //        acc.LastUpdatedDate = mAccount.LastUpdatedDate;
            //    }
            //    account.Add(acc);
            //}
            account = _context.MST_Account.Where(p => p.AccountName.Contains(name) && p.IsActive == true).Select(a => new AccountModel { AccountId = a.AccountId, AccountName = a.AccountName, GeoID = a.GeoID, SectorID = a.SectorID, SectorID2 = a.SectorID2, Geography = a.MST_Geo.GeoName, SectorName = a.MST_Sector.SectorName, SubSectorName = a.MST_Sector1.SectorName, LastUpdatedDate = a.LastUpdatedDate }).ToList();
            return account;
        }
        public void DeleteAccount(int accountId)
        {
            MST_Account mstAccount = _context.MST_Account.Where(p => p.AccountId == accountId).FirstOrDefault();
            if (mstAccount != null)
            {
                mstAccount.IsActive = false;
                _context.SaveChanges();
            }

        }

        public string GetDDReminderServiceEmailIdList()
        {
            int year = System.DateTime.Now.Year;
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(System.DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);



            var final = _context.MST_Project.Where(p => p.Tran_Proj_Wkly_Status.Where(trn => trn.WeekId == weekNum && trn.Year == year && trn.ProjectId == p.ProjectId).Count() <= 0)
                .Select(p => new { p.MST_Employee.EmailId }).ToList();

            /*(from a in _context.MST_Project
                             where !_context.Tran_Proj_Wkly_Status.Any(p => p.ProjectId == a.ProjectId && (p.Year != year && p.WeekId != weekNum))
                             select a.PMId).ToList<int?>();
                 
        var final = (from MST_Employee a in _context.MST_Employee
                     where result.Contains<int?>(a.EmployeeId)
                     select new
                     {
                         a.EmailId
                     }).ToList();
        */
            string emailIdList = string.Empty;
            foreach (var item in final)
            {

                emailIdList = emailIdList + item.EmailId.ToString() + ","; //String.Join(item.EmailId.ToString(), ";");           
            }


            emailIdList = emailIdList.TrimEnd(',');

            return emailIdList;


        }
        //public string GetParentAttributeName(int attributeId) 
        //{
        //    int? i;
        //    string str=null;

        //    MST_ProjectAttributes proAttribute = _context.MST_ProjectAttributes.Where(s => s.AttributeId == attributeId).FirstOrDefault();
        //    i = proAttribute.ParentAttributeId;

        //    if(proAttribute.ParentAttributeId.HasValue)
        //    {
        //        MST_ProjectAttributes pAttribute = _context.MST_ProjectAttributes.Where(sm => sm.AttributeId == i).FirstOrDefault();
        //        if (pAttribute.ParentAttributeId.HasValue) 
        //        {
        //            str = pAttribute.ParentAttributeId.Value.ToString();
        //        }
        //    }

        //    return str;
        //}




        public string GetFunctionalityId(int functionalityId)
        {
            MST_Functionality Record = _context.MST_Functionality.Where(p => p.FunctionalityId == functionalityId).FirstOrDefault();
            if (Record != null)
                return Record.FunctionalityName;
            else
                return string.Empty;
        }

        private FunctionalityModel ConvertFunctionalityEntityToFunctionalityModel(Tran_EmailConfiguration Record)
        {
            FunctionalityModel emailConfig = new FunctionalityModel();
            emailConfig.FunctionalityName = Record.MST_Functionality.FunctionalityName;

            return emailConfig;
        }

        public List<EmployeeModel> GetCheckBox(int functionalityId)
        {

            List<EmployeeModel> employees = new List<EmployeeModel>();
            employees = _context.MST_Employee.Select(e => new EmployeeModel
            {
                EmployeeCode = e.EmployeeCode,
                EmployeeId = e.EmployeeId,
                EmployeeName = e.EmployeeName,
                EmailId = e.EmailId,
                IsEmailConfigured = (_context.Tran_EmailConfiguration.Where(ec => ec.MST_Employee.EmployeeId == e.EmployeeId && ec.FunctionalityId == functionalityId).Count() > 0)
            }).ToList();
            //foreach (Tran_EmailConfiguration email in record)
            //{
            //    EmailConfiguration em = new EmailConfiguration();
            //    em.EmployeeId = email.EmployeeId;
            //    emailConfig.Add(em);
            //}
            //return emailConfig;
            //Tran_EmailConfiguration record = _context.Tran_EmailConfiguration.Where(p => p.FunctionalityId == functionalityId).FirstOrDefault();
            //return ConvertEmployeeReminderEntityToEmployeeReminderModel(record);
            return employees;
        }

        //private List<EmailConfiguration> ConvertEmployeeReminderEntityToEmployeeReminderModel(Tran_EmailConfiguration record)
        //{
        //    List<Tran_EmailConfiguration> email = new List<Tran_EmailConfiguration>();
        //    EmailConfiguration emailConfig = new EmailConfiguration();
        //    foreach (emailConfig in email)
        //    emailConfig.EmployeeId = record.EmployeeId;
        //    return 0;
        //}

        public void UpdateEmailConfiguration(EmailConfigurationModel emailConfiguration)
        {
            // Tran_EmailConfiguration emailConfig=_context.Tran_EmailConfiguration.Where
        }

        public void DeleteEmailConfiguration(EmailConfigurationModel emailConfiguration)
        {
            Tran_EmailConfiguration email = _context.Tran_EmailConfiguration.Where(p => p.EmployeeId == emailConfiguration.EmployeeId && p.FunctionalityId == emailConfiguration.FunctionalityId).FirstOrDefault();
            if (email != null)
            {
                _context.Tran_EmailConfiguration.Remove(email);
                _context.SaveChanges();
            }

        }

        public void AddEmailConfiguration(EmailConfigurationModel emailConfiguration)
        {
            //Tran_DE_Calendar tranDECalendar = new Tran_DE_Calendar();

            //tranDECalendar.ProjectId = deCalendar.ProjectId;
            //tranDECalendar.ReviewDate = deCalendar.ReviewDate;
            //tranDECalendar.EmployeeId = deCalendar.EmployeeId;
            //tranDECalendar.ReviewStatusId = deCalendar.ReviewStatusId;
            //_context.Tran_DE_Calendar.Add(tranDECalendar);
            //_context.SaveChanges();
            Tran_EmailConfiguration email = new Tran_EmailConfiguration();
            email.FunctionalityId = emailConfiguration.FunctionalityId;
            email.EmployeeId = emailConfiguration.EmployeeId;
            _context.Tran_EmailConfiguration.Add(email);
            _context.SaveChanges();
        }
        public void SaveEmailConfiguration(FunctionalityModel functional)
        {
            MST_Functionality functionalityMaster = _context.MST_Functionality.Where(f => f.FunctionalityId == functional.FunctionalityId).FirstOrDefault();

            //Tran_EmailConfiguration emailConfigurations = _context.Tran_EmailConfiguration.Where(ec => ec.em
            functionalityMaster.Tran_EmailConfiguration.Clear();
            //List<Tran_EmailConfiguration> ToBeRemoved = functionalityMaster.Tran_EmailConfiguration.Where(ec => !functional.EmployeeIds.Contains(ec.EmployeeId)).ToList();
            //List<int> ToBeAdded = functional.EmployeeIds.Where(e => functionalityMaster.Tran_EmailConfiguration.Where(ec=> ec.EmployeeId == e).Count() < 0).ToList();
            foreach (int EmpId in functional.EmployeeIds)
            {
                Tran_EmailConfiguration email = _context.Tran_EmailConfiguration.Create();
                email.FunctionalityId = functional.FunctionalityId;
                email.EmployeeId = EmpId;
                //email.MST_Employee = _context.MST_Employee.Where(e => e.EmployeeId == EmpId).FirstOrDefault();
                functionalityMaster.Tran_EmailConfiguration.Add(email);

            }

            _context.Entry(functionalityMaster).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public List<IDPAttributeModel> GetIDPAttributeList()
        {
            DateTime date = DateTime.Today;
            List<IDPAttributeModel> listIDPAttributes = new List<IDPAttributeModel>();
            listIDPAttributes = _context.MST_Attributes.Where(c => c.AttributeEndDate >= date && c.AttributeStartDate <= date).Select(a => new IDPAttributeModel { AttributeId = a.AttributeId, AttributeName = a.AttributeName, AttributeStartDate = a.AttributeStartDate, AttributeEndDate = a.AttributeEndDate, AttributeTypeId = a.AttributeId, AttributeTypeName = a.MST_AttributeTypes.AttributeTypeName }).ToList();

            return listIDPAttributes;
        }
        public List<IDPAttributeModel> SearchIDPAttributes(string name)
        {
            DateTime date = DateTime.Today;
            List<IDPAttributeModel> listIDPAttributes = new List<IDPAttributeModel>();
            listIDPAttributes = _context.MST_Attributes.Where(c => c.AttributeEndDate >= date && c.AttributeStartDate <= date && c.AttributeName.Contains(name)).Select(a => new IDPAttributeModel { AttributeId = a.AttributeId, AttributeName = a.AttributeName, AttributeStartDate = a.AttributeStartDate, AttributeEndDate = a.AttributeEndDate, AttributeTypeId = a.AttributeId, AttributeTypeName = a.MST_AttributeTypes.AttributeTypeName }).ToList();

            return listIDPAttributes;
        }
        public IDPAttributeModel GetIDPAttributeModel(int attributeId)
        {
            IDPAttributeModel idpAttributeModel = new IDPAttributeModel();
            List<MST_AttributeValues> mstAttributeValuesList = new List<MST_AttributeValues>();

            idpAttributeModel = _context.MST_Attributes.Where(c => c.AttributeId == attributeId).Select(a => new IDPAttributeModel { AttributeId = a.AttributeId, AttributeName = a.AttributeName, AttributeStartDate = a.AttributeStartDate, AttributeEndDate = a.AttributeEndDate, AttributeTypeId = a.AttributeTypeId }).FirstOrDefault();
            mstAttributeValuesList = _context.MST_AttributeValues.Where(c => c.AttributeId == attributeId).ToList();
            idpAttributeModel.AttributeValueStringList = new List<string>();
            foreach (MST_AttributeValues mstAttributeValues in mstAttributeValuesList)
            {
                if (mstAttributeValues.AttributeText != string.Empty)
                {
                    idpAttributeModel.AttributeValueStringList.Add(mstAttributeValues.AttributeText);
                }

            }

            return idpAttributeModel;
        }
        public void UpdateIDPAttributeModel(IDPAttributeModel idpAttributeModel)
        {
            MST_Attributes attributes = _context.MST_Attributes.Where(c => c.AttributeId == idpAttributeModel.AttributeId).FirstOrDefault();
            if (attributes != null)
            {
                attributes.AttributeName = idpAttributeModel.AttributeName;
                attributes.AttributeStartDate = idpAttributeModel.AttributeStartDate;
                attributes.AttributeEndDate = idpAttributeModel.AttributeEndDate;
                attributes.AttributeTypeId = idpAttributeModel.AttributeTypeId;

                //Deleting existing Attribute Values
                List<MST_AttributeValues> mstAttributeValues = _context.MST_AttributeValues.Where(c => c.AttributeId == idpAttributeModel.AttributeId).ToList();
                //mstAttributeValues.Clear();
                //_context.SaveChanges();

                foreach (MST_AttributeValues mstAttriValueToDelete in mstAttributeValues)
                {
                    _context.MST_AttributeValues.Remove(mstAttriValueToDelete);
                    //mstAttributeValues.Remove(mstAttriValueToDelete);
                    //_context.Entry(mstAttriValueToDelete).State = EntityState.Deleted;
                }
                _context.SaveChanges();

                if (idpAttributeModel.AttributeValueStringList != null)
                {
                    foreach (String s in idpAttributeModel.AttributeValueStringList)
                    {
                        MST_AttributeValues mstAttriValues = new MST_AttributeValues();
                        mstAttriValues.AttributeId = idpAttributeModel.AttributeId;
                        mstAttriValues.AttributeText = s;
                        mstAttriValues.AttributeValue = s;
                        _context.Entry(mstAttriValues).State = EntityState.Added;

                        _context.MST_AttributeValues.Add(mstAttriValues);
                    }
                }
            }

            _context.SaveChanges();
        }
        public int InsertIDPAttributeModel(IDPAttributeModel idpAttributeModel)
        {
            int newAttributeId = -1;
            MST_Attributes attribute = new MST_Attributes();

            attribute.AttributeName = idpAttributeModel.AttributeName;
            attribute.AttributeStartDate = idpAttributeModel.AttributeStartDate;
            attribute.AttributeEndDate = idpAttributeModel.AttributeEndDate;
            attribute.AttributeTypeId = idpAttributeModel.AttributeTypeId;

            _context.MST_Attributes.Add(attribute);


            _context.SaveChanges();
            newAttributeId = attribute.AttributeId;

            foreach (String s in idpAttributeModel.AttributeValueStringList)
            {
                MST_AttributeValues mstAttriValues = new MST_AttributeValues();
                mstAttriValues.AttributeId = newAttributeId;
                mstAttriValues.AttributeText = s;
                mstAttriValues.AttributeValue = s;
                _context.Entry(mstAttriValues).State = EntityState.Added;

                _context.MST_AttributeValues.Add(mstAttriValues);
            }
            _context.SaveChanges();

            return newAttributeId;
        }
        public List<AttributeTypesModel> GetIDPAttributesTypeList()
        {
            List<MST_AttributeTypes> Attributes = null;
            List<AttributeTypesModel> attributeTypeModel = new List<AttributeTypesModel>();
            Attributes = _context.MST_AttributeTypes.ToList();

            foreach (MST_AttributeTypes attribute in Attributes)
            {
                attributeTypeModel.Add(new AttributeTypesModel { AttributeTypeId = attribute.AttributeTypeId, AttributeTypeName = attribute.AttributeTypeName, AttributeTypeCode = attribute.AttributeTypeCode });
            }
            return attributeTypeModel;
        }

        public void AddUpdate(List<AttributeValuesModel> values, int id)
        {
            MST_AttributeValues attriVal = new MST_AttributeValues();
            //AttributeValuesModel attri=new AttributeValuesModel();
            List<MST_AttributeValues> mstAttributevalueslist = _context.MST_AttributeValues.Where(c => c.AttributeId == id).ToList();
            if (mstAttributevalueslist != null)
            {
                mstAttributevalueslist = null;

                foreach (AttributeValuesModel attri in values)
                {
                    attriVal.AttributeId = attri.AttributeId;
                    attriVal.AttributeText = attri.AttributeText;
                    //mstAttributevalueslist.Add(attriVal);
                    _context.MST_AttributeValues.Add(attriVal);
                }
                _context.SaveChanges();
            }
            else
            {
                foreach (AttributeValuesModel attri in values)
                {
                    attriVal.AttributeId = attri.AttributeId;
                    attriVal.AttributeText = attri.AttributeText;
                    //mstAttributevalueslist.Add(attriVal);
                    _context.MST_AttributeValues.Add(attriVal);
                }
                _context.SaveChanges();
            }
        }
        public void DeleteIDPAttribute(int AttributeId)
        {
            List<MST_AttributeValues> mstAttriValList = _context.MST_AttributeValues.Where(p => p.AttributeId == AttributeId).ToList();
            foreach (MST_AttributeValues mstAttriValueToDelete in mstAttriValList)
            {
                _context.MST_AttributeValues.Remove(mstAttriValueToDelete);
                //mstAttributeValues.Remove(mstAttriValueToDelete);
                //_context.Entry(mstAttriValueToDelete).State = EntityState.Deleted;
            }
            _context.SaveChanges();

            MST_Attributes mstAttributes = _context.MST_Attributes.Where(p => p.AttributeId == AttributeId).FirstOrDefault();
            if (mstAttributes != null)
            {
                _context.MST_Attributes.Remove(mstAttributes);
                _context.SaveChanges();
            }
           
        }

        public void DeleteQuestion(int QuestionId)
        {
            List<MST_ReviewQuestion> mstReviewQuestion = _context.MST_ReviewQuestion.Where(p => p.QuestionId == QuestionId).ToList();
       //     List<MST_AttributeValues> mstAttriValList = _context.MST_AttributeValues.Where(p => p.AttributeId == AttributeId).ToList();
            foreach (MST_ReviewQuestion mstReviewQuestionToDelete in mstReviewQuestion)
            {
                mstReviewQuestionToDelete.IsActive = false;
            }
            _context.SaveChanges();
        }

        public void DeleteReviewQuestion(int questionID)
        {
            List<MST_ReviewQuestion> mstReviewQuestionList = _context.MST_ReviewQuestion.Where(p => p.QuestionId == questionID).ToList();
            foreach (MST_ReviewQuestion mstReviewQuestionToDelete in mstReviewQuestionList)
            {
                _context.MST_ReviewQuestion.Remove(mstReviewQuestionToDelete);
            }
            _context.SaveChanges();

            MST_ReviewQuestion mstReview = _context.MST_ReviewQuestion.Where(p => p.QuestionId == questionID).FirstOrDefault();
            _context.MST_ReviewQuestion.Remove(mstReview);
            _context.SaveChanges();
        }

        public void DeleteQuestionnair(int questionnairId)
        {
            List<MST_Questionnaire> questionnairlist = _context.MST_Questionnaire.Where(q => q.QuestionnaireId == questionnairId).ToList();
            foreach (MST_Questionnaire q in questionnairlist)
            {
                q.IsActive = false;
            }

            _context.SaveChanges();

        }


        public int GetAttributeTypeId(string s)
        {
            MST_AttributeTypes attriType = _context.MST_AttributeTypes.Where(p => p.AttributeTypeCode.Contains(s)).FirstOrDefault();
            return attriType.AttributeTypeId;
        }
        public int InsertProjectCode(ProjectCodesModel projCode)
        {
            int newProjectCodeId = -1;
            MST_ProjectCodes mstProjCode = new MST_ProjectCodes();
            //mstProjCode.AccountId = projCode.AccountId;
            mstProjCode.ProjectCode = projCode.ProjectCode;
            mstProjCode.OnshoreHC = projCode.OnshoreHC;
            mstProjCode.OffShoreHC = projCode.OffShoreHC;
            mstProjCode.LastUpdatedBy = projCode.LastUpdatedBy;
            mstProjCode.LastUpdateDate = projCode.LastUpdateDate;
            _context.MST_ProjectCodes.Add(mstProjCode);
            _context.SaveChanges();
            newProjectCodeId = mstProjCode.ProjectCodeId;
            
            return newProjectCodeId;
        }
        public void UpdateProjectCode(ProjectCodesModel projCode) 
        {
            MST_ProjectCodes mstProjCode = _context.MST_ProjectCodes.Where(p => p.ProjectCodeId == projCode.ProjectCodeId).FirstOrDefault();
            if (mstProjCode!=null) 
            {
                //mstProjCode.AccountId = projCode.AccountId;
                mstProjCode.ProjectCode = projCode.ProjectCode;
                if (projCode.OnshoreHC.HasValue)
                {
                    mstProjCode.OnshoreHC = projCode.OnshoreHC;
                }
                else
                {
                    mstProjCode.OnshoreHC = null;
                }
                if (projCode.OnshoreHC.HasValue)
                {
                    mstProjCode.OffShoreHC = projCode.OffShoreHC;
                }
                else
                {
                    mstProjCode.OffShoreHC = null;
                }
                mstProjCode.OnshoreHC = projCode.OnshoreHC;
                mstProjCode.OffShoreHC = projCode.OffShoreHC;
                mstProjCode.LastUpdatedBy = projCode.LastUpdatedBy;
                mstProjCode.LastUpdateDate = projCode.LastUpdateDate;
               

                _context.SaveChanges();
            }
            
        }
        public List<IDPAttributeModel> GetIDPSpecificAttributes(int idp) 
        {
            DateTime date = DateTime.Today;
            List<IDPAttributeModel> idpAttributeModelList = new List<IDPAttributeModel>();
            
            var idList = new[] {idp, 9};
            List<Tran_IDP_Attributes> attributeIdList = _context.Tran_IDP_Attributes.Where( c => idList.Contains(c.IDPId)).ToList();
           
            foreach (Tran_IDP_Attributes tranIDPAttribute in attributeIdList) 
            {
                IDPAttributeModel idpmodel = _context.MST_Attributes.Where(c => c.AttributeId == tranIDPAttribute.AttributeId && c.AttributeEndDate >= date && c.AttributeStartDate <= date).Select(a => new IDPAttributeModel { AttributeId = a.AttributeId, AttributeName = a.AttributeName, AttributeTypeId = a.AttributeTypeId, AttributeTypeName = a.MST_AttributeTypes.AttributeTypeName }).FirstOrDefault();
                idpAttributeModelList.Add(idpmodel);
            }
            return idpAttributeModelList;
        }

        public List<AttributeValuesModel> GetIDPSpecificAttributeValues(int idp) 
        {
            DateTime date = DateTime.Today;
            List<AttributeValuesModel> attributeValueModelList = new List<AttributeValuesModel>();
            //List<Tran_IDP_Attributes> attributeIdList = _context.Tran_IDP_Attributes.Where(c => (c.IDPId == idp && c.IDPId == 9)).ToList();

            var idList = new[] { idp, 9 };
            List<Tran_IDP_Attributes> attributeIdList = _context.Tran_IDP_Attributes.Where(c => idList.Contains(c.IDPId)).ToList();
            foreach (Tran_IDP_Attributes tranIDPAttribute in attributeIdList) 
            {
                var obj = _context.MST_AttributeValues.Where(c => c.AttributeId == tranIDPAttribute.AttributeId).Select(a => new AttributeValuesModel { AttributeId = a.AttributeId, AttributeValueId = a.AttributeValueId, AttributeText = a.AttributeText, AttributeValue = a.AttributeValue });

                foreach (var item in obj)
            	{
		              attributeValueModelList.Add(item);
            	}
            }
            return attributeValueModelList;

        }

        public List<AttributeValuesModel> GetIDPDynamicAttributeValue(int idp)
        {
            DateTime date = DateTime.Today;
            List<AttributeValuesModel> attributeValueModelList = new List<AttributeValuesModel>();        

            var idList = new[] { idp, 9 };
            List<Tran_IDP_Attributes> attributeIdList = _context.Tran_IDP_Attributes.Where(c => idList.Contains(c.IDPId)).ToList();

            foreach (Tran_IDP_Attributes tranIDPAttribute in attributeIdList)
            {
                var obj = _context.Tran_Proj_IDP_Attributes.Where(c => c.AttributeId == tranIDPAttribute.AttributeId).Select(a => new AttributeValuesModel { AttributeId = a.AttributeId, AttributeValueId = a.AttributeValueId, AttributeText = a.AttributeTextValue });

                foreach (var item in obj)
                {
                    attributeValueModelList.Add(item);
                }
            }
            return attributeValueModelList;

        }

        public int InsertProjDetail(ProjectTempModel projectModel)
        {
            int newProjectId = -1;
            MST_Project_Temp project = new MST_Project_Temp();
            project.IDPId = projectModel.IDPId;
            project.AccountId = projectModel.AccountId;
            project.ProjectName = projectModel.ProjectName;
            project.IsActive = true;
            project.EMId = projectModel.EMId;
            project.PMId = projectModel.PMId;
            if (projectModel.IsStrategic.HasValue)
            {
                project.IsStrategic = projectModel.IsStrategic;
            }
            
            project.LastUpdateDate = projectModel.LastUpdateDate;
            project.LastUpdatedBy = projectModel.LastUpdatedBy;

            _context.MST_Project_Temp.Add(project);
            _context.SaveChanges();

            newProjectId = project.ProjectId;
            return newProjectId;
        }

        public void UpdateProjDetail(ProjectTempModel projectModel)
        {

            MST_Project_Temp project = new MST_Project_Temp();
            project.IDPId = projectModel.IDPId;
            project.AccountId = projectModel.AccountId;
            project.ProjectName = projectModel.ProjectName;
            project.IsActive = true;
            project.EMId = projectModel.EMId;
            project.PMId = projectModel.PMId;
            if (projectModel.IsStrategic.HasValue)
            {
                project.IsStrategic = projectModel.IsStrategic;
            }

            project.LastUpdateDate = projectModel.LastUpdateDate;
            project.LastUpdatedBy = projectModel.LastUpdatedBy;

            _context.SaveChanges();

        
        }

        public void InsertProjAttributes(List<TranProjIDPAttributesModel> prjIDPAttr, string projId) 
        {
            foreach(TranProjIDPAttributesModel prj in prjIDPAttr)
            {
                Tran_Proj_IDP_Attributes tranproj = new Tran_Proj_IDP_Attributes();

                tranproj.AttributeId = prj.AttributeId;
                //tranproj.ProjectId=prj.ProjectId;
                tranproj.ProjectId = Convert.ToInt32(projId);
                if (prj.AttributeTextValue != null) 
                {
                    tranproj.AttributeTextValue = prj.AttributeTextValue;
                }
                if (prj.AttributeValueId != 0)
                {
                    tranproj.AttributeValueId = prj.AttributeValueId;
                }
                tranproj.LastUpdatedBy = prj.LastUpdatedBy;
                tranproj.LastUpdateDate = prj.LastUpdateDate;
                _context.Tran_Proj_IDP_Attributes.Add(tranproj);
                _context.SaveChanges();
            }
            
        }
        public ProjectTempModel GetProjectTempDetails(int projectId)
        {
            MST_Project_Temp projectRecord = _context.MST_Project_Temp.Where(p => p.ProjectId == projectId).FirstOrDefault();
            ProjectTempModel project = new ProjectTempModel();
            project.ProjectId = projectRecord.ProjectId;
            project.ProjectName = projectRecord.ProjectName;
            project.AccountId = projectRecord.AccountId;
            project.IDPId = projectRecord.IDPId;
            project.EMId = projectRecord.EMId;
            project.PMId = projectRecord.PMId;
            project.IsStrategic = projectRecord.IsStrategic;
            
            return project;
        }

        public List<ReviewTypeModel> GetReviewType()
        {
            List<ReviewTypeModel> reviewTypeModel = null;
            reviewTypeModel = _context.MST_ReviewType.Select(r => new ReviewTypeModel { ReviewTypeId = r.ReviewTypeId, ReviewTypeName = r.ReviewTypeName }).ToList<ReviewTypeModel>();
            return reviewTypeModel;
        }
        public QuestionnaireModel GetQuestionnairDetail(int questionnairId)
        {
            MST_Questionnaire questionnairRecord = _context.MST_Questionnaire.Where(q => q.QuestionnaireId == questionnairId).FirstOrDefault();

            return ConvertQuestionnairEntityToQuestionnairModel(questionnairRecord);
        }

        private static QuestionnaireModel ConvertQuestionnairEntityToQuestionnairModel(MST_Questionnaire questionnairModel)
        {
            QuestionnaireModel _questionnaireModel = new QuestionnaireModel();
            if (questionnairModel != null)
            {
                _questionnaireModel.QuestionnaireId = questionnairModel.QuestionnaireId;
                _questionnaireModel.QuestionnaireName = questionnairModel.QuestionnaireName;
                _questionnaireModel.IDPId = questionnairModel.IDPId;
                _questionnaireModel.ReviewTypeId = questionnairModel.ReviewTypeId;
                _questionnaireModel.IsActive = questionnairModel.IsActive;
            }

            return _questionnaireModel;
        }


        public int InsertQuestionnairDetails(QuestionnaireModel questionnaireModel)
        {
            int newQuestionnairId= -1;
            MST_Questionnaire _questionnairModel = new MST_Questionnaire();

            if (!string.IsNullOrEmpty(questionnaireModel.QuestionnaireName))
            {
                _questionnairModel.QuestionnaireName = questionnaireModel.QuestionnaireName.Trim();
            }

                     
             _questionnairModel.IDPId = questionnaireModel.IDPId;
            
            _questionnairModel.IsActive=questionnaireModel.IsActive;
            
            _questionnairModel.ReviewTypeId=questionnaireModel.ReviewTypeId;

            _context.MST_Questionnaire.Add(_questionnairModel);
            _context.SaveChanges();

            newQuestionnairId = _questionnairModel.QuestionnaireId;
            return newQuestionnairId;
        }

        public void UpdateQuestionnairDetail(QuestionnaireModel questionnaireModel)
        {
            MST_Questionnaire _questionnaireModel = _context.MST_Questionnaire.Where(q => q.QuestionnaireId == questionnaireModel.QuestionnaireId).FirstOrDefault();

            if (_questionnaireModel != null)
            {
                _questionnaireModel.QuestionnaireName = questionnaireModel.QuestionnaireName;
                _questionnaireModel.IDPId = questionnaireModel.IDPId;
                _questionnaireModel.ReviewTypeId = questionnaireModel.ReviewTypeId;
                _questionnaireModel.IsActive = questionnaireModel.IsActive;
              
                _context.SaveChanges();
            }

        }
        public List<ReviewQuestionModel> GetReviewQuestion()
        {
            List<ReviewQuestionModel> reviewQuestionModel = new List<ReviewQuestionModel>();
            reviewQuestionModel = _context.MST_ReviewQuestion.Where(m => m.IsActive == true).Select(m => new ReviewQuestionModel {QuestionId=m.QuestionId, QuestionnaireId = m.QuestionnaireId, QuestionDescription = m.QuestionDescription, QuestionGuideLines = m.QuestionGuideLines, AttributeName = m.MST_ProjectAttributes.AttributeName, IsActive = m.IsActive, QuestionnaireName = m.MST_Questionnaire.QuestionnaireName }).ToList<ReviewQuestionModel>();
         //   reviewQuestionModel = _context.MST_ReviewQuestion.Select(m => new ReviewQuestionModel { QuestionnaireId = m.QuestionnaireId, QuestionDescription = m.QuestionDescription, QuestionGuideLines = m.QuestionGuideLines, AttributeName = m.MST_ProjectAttributes.AttributeName, IsActive = m.IsActive, QuestionnaireName = m.MST_Questionnaire.QuestionnaireName }).ToList<ReviewQuestionModel>();

            return reviewQuestionModel;
        }

        public int InsertReviewQuestionModel(ReviewQuestionModel reviewQuestionModel)
        {
            int newQuestionId = -1;
            MST_ReviewQuestion reviewQuestion = new MST_ReviewQuestion();
            reviewQuestion.QuestionnaireId = reviewQuestionModel.QuestionnaireId;
            reviewQuestion.QuestionDescription = reviewQuestionModel.QuestionDescription;
            reviewQuestion.QuestionGuideLines = reviewQuestionModel.QuestionGuideLines;
            reviewQuestion.IsActive = reviewQuestionModel.IsActive;
            reviewQuestion.AttributeId = reviewQuestionModel.AttributeId;

            _context.MST_ReviewQuestion.Add(reviewQuestion);
            _context.SaveChanges();

            newQuestionId = reviewQuestionModel.QuestionId;
            return newQuestionId;

        }

        public List<QuestionnaireModel> GetQuestionnaireIdList()
        {
            List<QuestionnaireModel> questionnaireId = null;
            questionnaireId = _context.MST_Questionnaire.Select(m => new QuestionnaireModel { QuestionnaireId = m.QuestionnaireId, QuestionnaireName = m.QuestionnaireName }).ToList<QuestionnaireModel>();
            return questionnaireId;
        }
        public void UpdateQuestion(ReviewQuestionModel reviewQuestionModel)
        {
            MST_ReviewQuestion _questionModel = _context.MST_ReviewQuestion.Where(q => q.QuestionId == reviewQuestionModel.QuestionId).FirstOrDefault();

            if (_questionModel != null)
            {
                _questionModel.QuestionnaireId = reviewQuestionModel.QuestionnaireId;
                _questionModel.QuestionDescription = reviewQuestionModel.QuestionDescription;
                _questionModel.QuestionGuideLines = reviewQuestionModel.QuestionGuideLines;
                _questionModel.AttributeId = reviewQuestionModel.AttributeId;
                _questionModel.IsActive = reviewQuestionModel.IsActive;

                _context.SaveChanges();
            }

        }
        public void MapProjectCodes(List<int> projectCodeList, string projId)
        {
            
            Tran_Proj_ProjCode_Details tranProjectCode;

            foreach (int projCode in projectCodeList)
            {
                tranProjectCode = new Tran_Proj_ProjCode_Details();
                tranProjectCode.ProjectCodeId = projCode;
                tranProjectCode.ProjectId = Convert.ToInt32(projId);
                _context.Tran_Proj_ProjCode_Details.Add(tranProjectCode);
                _context.SaveChanges();
            }

        }

        public List<ProjectCodesModel> GetProjectCodes() 
        {
            List<ProjectCodesModel> lstProCodeModel= new List<ProjectCodesModel>();
            
            lstProCodeModel = _context.MST_ProjectCodes.Select(a => new ProjectCodesModel { ProjectCodeId = a.ProjectCodeId, ProjectCode = a.ProjectCode }).ToList<ProjectCodesModel>();

            return lstProCodeModel;
        }
    }
}
