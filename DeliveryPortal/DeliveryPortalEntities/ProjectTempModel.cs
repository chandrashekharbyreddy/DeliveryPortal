using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
    public class ProjectTempModel
    {
        public int ProjectId { get; set; }
        public int AccountId { get; set; }
        public string ProjectName { get; set; }
        public int IDPId { get; set; }
        public int EMId { get; set; }
        public int PMId { get; set; }
        public Nullable<bool> IsStrategic { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public int LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.DateTime> LastDEReviewDate { get; set; }
        public Nullable<System.DateTime> LastIDQADate { get; set; }
        public Nullable<System.DateTime> LastSMRDate { get; set; }
        public string ProjectCodes{get; set;}
    }
}
