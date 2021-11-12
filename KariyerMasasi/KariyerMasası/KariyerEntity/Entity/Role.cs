using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class Role:LowerBase
    {
        public Role()
        {
            this.UserRoles = new HashSet<UserRole>();
        }
        public string Name { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }

    }
}
