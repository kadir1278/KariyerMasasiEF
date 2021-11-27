using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class JobAdvertisementApplication:LowerBase
    {
        public int JobAdvertisementID { get; set; }
        public JobAdvertisement JobAdvertisement { get; set; }
        public int DepartmentID { get; set; }
        public Department Department { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public bool Status { get; set; }
        public DateTime ApprovalDate { get; set; }
    }
}
