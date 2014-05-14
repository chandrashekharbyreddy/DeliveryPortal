using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class CommonConstants
    {
        public enum Level
        {
            PQL = 1,
            PM = 1,
            EM = 2,
            DEReviewer = 2,
            SMRReviewer = 2,
            IDQAReviewer = 3,
            Leadership = 4
        }
        public enum Flag
        { 
            Grey = 0,
            Red = 1,
            Amber = 2,
            Green = 3
        
        }
    }
}
