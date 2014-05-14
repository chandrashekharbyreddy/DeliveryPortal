using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
    public class AccountModel
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public Nullable<int> LastUpdateBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> SectorID { get; set; }
        public Nullable<int> SectorID2 { get; set; }
        public Nullable<int> GeoID { get; set; }
        public string Geography { get; set; }
        public string SectorName { get; set; }
        public string SubSectorName { get; set; }
    }
}
