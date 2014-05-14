using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
    public class IDPModel
    {

        public int IdpId { get; set; }
        public string IdpName { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdateddate { get; set; }
    }
}
