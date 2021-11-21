using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class UserCertificate : LowerBase
    {
        public string Name { get; set; }
        public DateTime FinishDate { get; set; }
        public string InstitutionFromName { get; set; }
        public bool Status { get; set; }
        public string File { get; set; }
        public int UserID { get; set; }
        public int LanguageID { get; set; }
        public User User { get; set; }
        public Language Language { get; set; }
    }
}
