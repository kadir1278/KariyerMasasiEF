using KariyerEntity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerEntity.Entity
{
    public class UserLanguage : LowerBase
    {
        public string SpeakingLevel { get; set; }
        public string WritingLevel { get; set; }
        public string ListeningLevel { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
        public int LanguageID { get; set; }
        public User User { get; set; }
        public Language Language { get; set; }
    }
}
