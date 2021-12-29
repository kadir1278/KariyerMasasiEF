using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class UserBusinessArea : LowerBase
    {
        public int UserID { get; set; }
        public User User { get; set; }

        public int BusinessAreaID { get; set; }
        public BusinessArea BusinessArea { get; set; }
    }
}
