using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
    public class ProjectCodesModel
    {
        public int ProjectCodeId { get; set; }
        public int AccountId { get; set; }
        public string ProjectCode { get; set; }
        public Nullable<int> LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public Nullable<int> OnshoreHC { get; set; }
        public Nullable<int> OffShoreHC { get; set; }
        public int ProjectId { get; set; }
    }
}
