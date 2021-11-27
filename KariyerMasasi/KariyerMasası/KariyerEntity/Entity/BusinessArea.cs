﻿using KariyerEntity.BaseEntity;
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
            this.JobAdvertisements = new HashSet<JobAdvertisement>();

        }

        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<JobAdvertisement> JobAdvertisements { get; set; }

    }
}
