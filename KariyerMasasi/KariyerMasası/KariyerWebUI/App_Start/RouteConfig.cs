using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KariyerWebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Anasayfa",
                url: "",
                defaults: new { controller = "Anasayfa", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "User",
                url: "kullanici",
                defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "UserDelete",
               url: "kullanici-sil/{id}",
               defaults: new { controller = "User", action = "Delete", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "UserAdd",
              url: "kullanici-ekle/{id}",
              defaults: new { controller = "User", action = "Add", id = UrlParameter.Optional }
          );
            routes.MapRoute(
             name: "UserUpdate",
             url: "kullanici-guncelle/{id}",
             defaults: new { controller = "User", action = "Update", id = UrlParameter.Optional }
         );
            #region Partial

            routes.MapRoute(
              name: "GetUserData",
              url: "GetUserData/{searchText}",
              defaults: new { controller = "User", action = "GetUserData", searchText = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "PartialAddUser",
                url: "PartialAddUser",
                defaults: new { controller = "User", action = "PartialAddUser", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "PartialUpdateUser",
               url: "PartialUpdateUser/{id}",
               defaults: new { controller = "User", action = "PartialUpdateUser", id = UrlParameter.Optional }
           );
            #endregion
        }
    }
}
