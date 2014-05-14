using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
    public class DECalendarModel
    {
        public int DEReviewCalendarId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }
        public int EmployeeId{get;set;}
        public DateTime ReviewDate { get; set; }
        public int ReviewStatusId { get; set; }
        public string ReviewStatusName { get; set; }
        public string ReviewerName { get; set; }
    }
}
