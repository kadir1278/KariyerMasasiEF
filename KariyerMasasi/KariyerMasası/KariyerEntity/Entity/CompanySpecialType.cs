using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class CompanySpecialType:LowerBase
    {
        public int CompanyID { get; set; }
        public Company Company { get; set; }
        public int UserSpecialTypeID { get; set; }
        public UserSpecialType UserSpecialType { get; set; }
    }
}
