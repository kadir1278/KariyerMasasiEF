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
        }
        public string Name { get; set; }
        public virtual ICollection<JobAdvertisement> JobAdvertisements { get; set; }
        public virtual ICollection<Company> Companies { get; set; }

    }
}
