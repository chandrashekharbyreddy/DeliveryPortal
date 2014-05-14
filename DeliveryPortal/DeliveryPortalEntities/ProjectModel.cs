using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
    public class ProjectModel
    {
        public int ProjectId { get; set; }
        public int AccountId { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public int? IdpId { get; set; }
        public int? EMId { get; set; }
        public int? PMID { get; set; }
        public int? GeoId { get; set; }
        public int? SectorId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? MethodologyId { get; set; }
        public int? NoWId { get; set; }
        public int? EstBasisId { get; set; }
        public DateTime? LastDEReviewsDate { get; set; }
        public DateTime? LastDQADate { get; set; }
        public DateTime? LastSMRDate { get; set; }
        public Boolean? IsStrategic { get; set; }
        public int? LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        //public Nullable<bool> IsRA { get; set; }
    }
}
