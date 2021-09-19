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
        public int BusinessAreaID { get; set; }
        public string MilitaryStatus { get; set; }
        public string Gender { get; set; }
        public string MarriageStatus { get; set; }
        public bool ProgramState { get; set; }
        public string Hobby { get; set; }
    }
}
