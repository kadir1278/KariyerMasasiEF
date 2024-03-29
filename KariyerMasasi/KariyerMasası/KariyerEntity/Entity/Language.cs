﻿using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class Language:LowerBase
    {
        public Language()
        {
            this.UserCertificates = new HashSet<UserCertificate>();
        }
        public string Name { get; set; }
        public virtual ICollection<UserCertificate> UserCertificates { get; set; }
    }
}
