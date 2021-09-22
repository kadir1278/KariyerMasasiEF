using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class SpecialDirectory : LowerBase
    {
        public int UserID { get; set; }
        public int UserSpecialTypeID { get; set; }
        public User User { get; set; }
        public UserSpecialType UserSpecialType { get; set; }
    }
}
