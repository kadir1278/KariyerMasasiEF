using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class CompanyDepartment : LowerBase
    {
        public int CompanyID { get; set; }
        public Company Company { get; set; }

        public int DepartmentID { get; set; }
        public Department Department { get; set; }

    }
}
