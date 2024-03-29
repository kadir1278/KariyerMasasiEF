﻿using KariyerEntity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Modal
{
    public class SystemContext:DbContext
    {
        public SystemContext() : base("name=system")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<BusinessArea> BusinessAreas { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<UserSpecialType> UserSpecialTypes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyUser> CompanyUsers { get; set; }
        public DbSet<UserEducation> UserEducations { get; set; }
        public DbSet<UserBusinessInformation> UserBusinessInformations { get; set; }
        public DbSet<UserLanguage> UserLanguages { get; set; }
        public DbSet<UserCertificate> UserCertificates { get; set; }
        public DbSet<UserComputerInformation> UserComputerInformations { get; set; }
        public DbSet<UserReference> UserReferences { get; set; }
        public DbSet<UserSeminar> UserSeminars { get; set; }
        public DbSet<SpecialDirectory> SpecialDirectories { get; set; }
        public DbSet<Role>Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<JobAdvertisement> JobAdvertisements { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<JobAdvertisementApplication> JobAdvertisementApplications { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<CompanyBusinessArea> CompanyBusinessAreas { get; set; }
        public DbSet<CompanyDepartment> CompanyDepartments { get; set; }
        public DbSet<CompanySpecialType> CompanySpecialTypes{ get; set; }
        public DbSet<UserBusinessArea> UserBusinessAreas { get; set; }
        public DbSet<UserDepartment> UserDepartments { get; set; }
        public DbSet<UserSpecialTypeCombine> UserSpecialTypeCombines { get; set; }
    }
}
