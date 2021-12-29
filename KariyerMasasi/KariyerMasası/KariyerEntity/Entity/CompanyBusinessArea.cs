using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class CompanyBusinessArea:LowerBase
    {
        public int CompanyID { get; set; }
        public Company Company { get; set; }

        public int BusinessAreaID { get; set; }
        public BusinessArea BusinessArea { get; set; }

    }
}
