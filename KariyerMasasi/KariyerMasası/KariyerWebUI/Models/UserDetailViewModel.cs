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
        public string SpecialName { get; set; }
        public string SeminarName { get; set; }
        public DateTime SeminarDate { get; set; }
        public string SeminarDescription { get; set; }
        public string ReferenceNameSurname { get; set; }
        public string ReferencePhone { get; set; }
        public string ReferenceEMail { get; set; }
        public string ULSpeakingLevel { get; set; }
        public string ULWritingLevel { get; set; }
        public string ULListeningLevel { get; set; }
        public string ULDescription { get; set; }
        public int? ULLanguageID { get; set; }
        public string UESchoolName { get; set; }
        public bool UEGraduationStatus { get; set; }
        public DateTime UEGraduationYear { get; set; }
        public double UEGraduationGrade { get; set; }
        public string UEDepartment { get; set; }
        public string UEDescription { get; set; }
        public string UCIName { get; set; } 
        public string UCIDescription { get; set; }
        public string UCCertificateName { get; set; }
        public DateTime UCCertificateFinishDate { get; set; }
        public string UCInstitutionFromName { get; set; }
        public bool UCStatus { get; set; } 
        public string UCCertificateFile { get; set; }
        public string UBICompanyName { get; set; }
        public DateTime UBIStartingDate { get; set; }
        public DateTime UBIFinishDate { get; set; }
        public string UBIDuty { get; set; }
        public string UBIDescription { get; set; }

        public List<UserCertificate> Certificates { get; set; }
        public List<UserComputerInformation> ComputerInformations { get; set; }
        public List<UserBusinessInformation> BusinessInformations { get; set; }
        public List<UserReference> References { get; set; }
        public List<UserSeminar> Seminars { get; set; }
    }

}