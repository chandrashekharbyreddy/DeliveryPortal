using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
    public class DEReviewModel
    {
        public int DEReviewId { get; set; }
        public int DEReviewCalendarId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public Nullable<System.DateTime> ReviewDate { get; set; }
        public Nullable<int> LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }

    }
}
