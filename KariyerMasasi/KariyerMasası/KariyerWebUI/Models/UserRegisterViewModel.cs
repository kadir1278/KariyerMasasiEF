using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KariyerEntity.Entity;
namespace KariyerWebUI.Models
{
    public class UserRegisterViewModel
    {
        public User User { get; set; }
        public IEnumerable<SelectListItem> BusinessAreas { get; set; }
        
    }
}