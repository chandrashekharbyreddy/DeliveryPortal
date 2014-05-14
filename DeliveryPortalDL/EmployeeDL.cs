using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryPortalEntities;
namespace DeliveryPortalDL
{
    public class EmployeeDL
    {
        DashboardEntities _context = new DashboardEntities();
        public EmployeeModel GetEmployeeDetails(string windowsId)
        {
            EmployeeModel objEmployee = new EmployeeModel();

            objEmployee = _context.MST_Employee.Where(e => e.WindowsId == windowsId).Select(e => new EmployeeModel { EmployeeCode = e.EmployeeCode, EmployeeId = e.EmployeeId, EmployeeName = e.EmployeeName,
                EmailId = e.EmailId, Designation = e.Designation, Location = e.Location, WindowsId = e.WindowsId }).FirstOrDefault();

            
            return objEmployee;
        }
    }
}
