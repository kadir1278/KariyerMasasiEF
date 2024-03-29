﻿using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class Company : LowerBase
    {
        public Company()
        {
            this.CompanyUsers = new HashSet<CompanyUser>();
            this.JobAdvertisements = new HashSet<JobAdvertisement>();
            this.Meetings = new HashSet<Meeting>();
            this.CompanyBusinessAreas = new HashSet<CompanyBusinessArea>();
            this.CompanyDepartments = new HashSet<CompanyDepartment>();

        }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string EMail { get; set; }
        public string Logo { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string Address { get; set; }
        public string TaxNumber { get; set; }
        public string TaxAddress { get; set; }
        public string TaxFile { get; set; }
        public bool ProgramState { get; set; }
        public bool GeneralIsActiveStatus { get; set; }
        public bool PaymentStatus { get; set; }

        public virtual ICollection<CompanyUser> CompanyUsers { get; set; }
        public virtual ICollection<JobAdvertisement> JobAdvertisements { get; set; }
        public virtual ICollection<Meeting> Meetings { get; set; }
        public virtual ICollection<CompanyBusinessArea> CompanyBusinessAreas { get; set; }
        public virtual ICollection<CompanyDepartment> CompanyDepartments { get; set; }
        public virtual ICollection<CompanySpecialType> CompanySpecialTypes { get; set; }


    }
}

