using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class CompanyUser:LowerBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int CompanyID { get; set; }
        public Company Company { get; set; }
    }
}
