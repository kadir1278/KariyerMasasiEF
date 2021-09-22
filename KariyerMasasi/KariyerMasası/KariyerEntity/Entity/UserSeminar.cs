using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class UserSeminar : LowerBase
    {
        public string Name { get; set; }
        public DateTime SeminarDate { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
