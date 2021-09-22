using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class UserReference : LowerBase
    {
        public string NameSurname { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
