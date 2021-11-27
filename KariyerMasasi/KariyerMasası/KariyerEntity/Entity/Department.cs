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
        }
        public string Name { get; set; }
        public virtual ICollection<JobAdvertisement> JobAdvertisements { get; set; }

    }
}
