using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class Meeting:LowerBase
    {
        public string RoomName { get; set; }
        public bool Status { get; set; }
        public bool IsMomentary { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int CompanyID { get; set; }
        public Company Company { get; set; }
        public int JobAdvertisementID { get; set; }
        public JobAdvertisement JobAdvertisement { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
