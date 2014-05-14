using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
    public class FunctionalityModel
    {
        public int FunctionalityId { get; set; }
        public string FunctionalityName { get; set; }
        public string FunctionalityCode { get; set; }
        public List<string> EmailIdList { get; set; }
        //public string EmailIds
        //{
        //    get {
        //        if (EmailIdList != null)
        //            return String.Join(",", EmailIdList.ToArray());
        //        else
        //            return string.Empty;
        //    }
            
        //}
        public string EmailIds { get; set; }

        public List<int> EmployeeIds { get; set; }
        
    }
}
