using System;
using System.Collections.Generic;

namespace WebAppEnterprise
{
    public partial class Payroll
    {
        public int Id { get; set; }
        public short Employee { get; set; }
        public int Salary { get; set; }
        public int Bonus { get; set; }
        public int? FinalSalary { get; set; }

        public virtual Stuff EmployeeNavigation { get; set; }
    }
}
