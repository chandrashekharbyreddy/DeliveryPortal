using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
   public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }
        public string EmailId { get; set; }
        public bool IsActive { get; set; }
        public string EmployeeCode { get; set; }
        public string WindowsId { get; set; }
        public bool IsEmailConfigured { get; set; }
    }
}
