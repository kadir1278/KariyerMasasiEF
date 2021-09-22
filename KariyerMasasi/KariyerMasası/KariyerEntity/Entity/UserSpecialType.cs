using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class UserSpecialType:LowerBase
    {
        public UserSpecialType()
        {
            this.SpecialDirectories = new HashSet<SpecialDirectory>();
        }
        public string Name { get; set; }
        public virtual ICollection<SpecialDirectory> SpecialDirectories { get; set; }
    }
}
