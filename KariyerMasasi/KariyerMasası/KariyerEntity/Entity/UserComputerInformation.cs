using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class UserComputerInformation : LowerBase
    {
        public string Name { get; set; } // neyin ismi?
        public string Description { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
