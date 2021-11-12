using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class User : LowerBase
    {
        public User()
        {
            this.UserEducations = new HashSet<UserEducation>();
            this.UserBusinessInformations = new HashSet<UserBusinessInformation>();
            this.UserLanguages = new HashSet<UserLanguage>();
            this.UserCertificates = new HashSet<UserCertificate>();
            this.UserComputerInformations = new HashSet<UserComputerInformation>();
            this.UserReferences = new HashSet<UserReference>();
            this.UserSeminars = new HashSet<UserSeminar>();
            this.UserRoles = new HashSet<UserRole>();
            this.SpecialDirectories = new HashSet<SpecialDirectory>();
        }
        public string Photo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        public bool GeneralIsActive { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string MilitaryStatus { get; set; }
        public string Gender { get; set; }
        public string MarriageStatus { get; set; }
        public bool ProgramState { get; set; }
        public string Hobby { get; set; }
        public int BusinessAreaID { get; set; }
        public BusinessArea BusinessArea { get; set; }
        public virtual ICollection<UserEducation> UserEducations { get; set; }
        public virtual ICollection<UserBusinessInformation> UserBusinessInformations { get; set; }
        public virtual ICollection<UserLanguage> UserLanguages { get; set; }
        public virtual ICollection<UserCertificate> UserCertificates { get; set; }
        public virtual ICollection<UserComputerInformation> UserComputerInformations { get; set; }
        public virtual ICollection<UserReference> UserReferences { get; set; }
        public virtual ICollection<UserSeminar> UserSeminars { get; set; }
        public virtual ICollection<SpecialDirectory> SpecialDirectories { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }

    }
}
