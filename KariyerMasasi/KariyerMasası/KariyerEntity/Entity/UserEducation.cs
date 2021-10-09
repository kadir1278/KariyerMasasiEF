using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class UserEducation : LowerBase
    {

        public string SchoolName { get; set; }
        public string GraduationStatus { get; set; }
        public DateTime GraduationYear { get; set; }
        public string GraduationGrade { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
