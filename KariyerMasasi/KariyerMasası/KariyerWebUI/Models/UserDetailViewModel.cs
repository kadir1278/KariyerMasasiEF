using KariyerEntity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KariyerWebUI.Models
{

    public class UserDetailViewModel
    {
        public int UserID { get; set; }
        public List<UserCertificate> Certificates { get; set; }
        public List<UserComputerInformation> ComputerInformations { get; set; }
        public List<UserBusinessInformation> BusinessInformations { get; set; }
        public List<UserReference> References { get; set; }
        public List<UserSeminar> Seminars { get; set; }
    }

}