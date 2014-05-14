using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPortalEntities
{
    public class DEReviewReminderModel
    {
        public string ProjectOwner { get; set; }
        public string EM { get; set; }
        public List<string> Reviewer { get; set; }
        public DateTime ReviewDate { get; set; }
        public string ProjectName { get; set; }
    }
}
