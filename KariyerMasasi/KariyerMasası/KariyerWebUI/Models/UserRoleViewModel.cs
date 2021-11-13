using KariyerEntity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KariyerWebUI.Models
{
    public class UserRoleViewModel
    {
        public List<Role> Roles{ get; set; }
        public List<User> Users { get; set; }
    }
}