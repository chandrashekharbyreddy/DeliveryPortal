using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
    public class GeoModel
    {
        public int GeoId { get; set; }
        public string GeoName { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdateddate { get; set; }
    }
}
