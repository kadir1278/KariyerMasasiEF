    using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class UserBusinessInformation : LowerBase
    {
        public string CompanyName { get; set; } 
        public DateTime StartingDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Duty { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
