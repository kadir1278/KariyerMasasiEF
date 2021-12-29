using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class Department:LowerBase
    {
        public Department()
        {
            this.JobAdvertisements = new HashSet<JobAdvertisement>();
            this.Companies = new HashSet<Company>();
            this.CompanyDepartments = new HashSet<CompanyDepartment>();
            this.UserDepartments = new HashSet<UserDepartment>();
        }
        public string Name { get; set; }
        public virtual ICollection<JobAdvertisement> JobAdvertisements { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<CompanyDepartment> CompanyDepartments { get; set; }
        public virtual ICollection<UserDepartment> UserDepartments { get; set; }
    }
}
