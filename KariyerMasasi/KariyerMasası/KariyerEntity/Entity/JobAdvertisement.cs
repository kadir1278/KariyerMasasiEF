using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class JobAdvertisement:LowerBase
    {
        public JobAdvertisement()
        {
            this.Meetings = new HashSet<Meeting>();
            this.JobAdvertisementApplications = new HashSet<JobAdvertisementApplication>();
        }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ContractType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public bool SpecialCase { get; set; } // Özel Durumlumu Başvurabilsin mi ?
        public int CompanyID { get; set; }
        public Company Company { get; set; }
        public int BusinessAreaID { get; set; }
        public BusinessArea BusinessArea { get; set; }
        public virtual ICollection<Meeting> Meetings { get; set; }
        public virtual ICollection<JobAdvertisementApplication> JobAdvertisementApplications { get; set; }


    }
}
