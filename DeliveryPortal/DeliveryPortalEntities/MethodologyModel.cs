using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
    public class MethodologyModel
    {
        public int MethodologyId { get; set; }
        public string MethodologyName { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdateddate { get; set; }
    }
}
