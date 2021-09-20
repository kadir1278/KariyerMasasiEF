using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class BusinessArea : LowerBase
    {
        public BusinessArea()
        {
            this.Users = new HashSet<User>();
            this.Companies = new HashSet<Company>();
        }

        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Company> Companies { get; set; }

    }
}
