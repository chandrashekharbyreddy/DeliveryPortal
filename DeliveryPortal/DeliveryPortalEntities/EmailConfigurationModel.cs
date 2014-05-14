using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
    public class EmailConfigurationModel
    {
        public int EmailConfigId { get; set; }
        public int FunctionalityId { get; set; }
        public string EmailIds { get; set; }
        public int EmployeeId { get; set; }

    }
}
