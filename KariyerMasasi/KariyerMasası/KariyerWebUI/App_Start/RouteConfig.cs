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

            #region User

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

            #endregion User

            #region Admin

            routes.MapRoute(
                name: "Admin",
                url: "yonetici",
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
         );
            routes.MapRoute(
               name: "AdminDelete",
               url: "yonetici-sil/{id}",
               defaults: new { controller = "Admin", action = "Delete", id = UrlParameter.Optional }
         );
            routes.MapRoute(
              name: "AdminAdd",
              url: "yonetici-ekle/{id}",
              defaults: new { controller = "Admin", action = "Add", id = UrlParameter.Optional }
         );
            routes.MapRoute(
             name: "AdminUpdate",
             url: "yonetici-guncelle/{id}",
             defaults: new { controller = "Admin", action = "Update", id = UrlParameter.Optional }
         );
            routes.MapRoute(
              name: "GetAdminData",
              url: "GetAdminData/{searchText}",
              defaults: new { controller = "Admin", action = "GetAdminData", searchText = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "PartialAddAdmin",
                url: "PartialAddAdmin",
                defaults: new { controller = "Admin", action = "PartialAddAdmin", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                 name: "PartialUpdateAdmin",
                 url: "PartialUpdateAdmin/{id}",
                 defaults: new { controller = "Admin", action = "PartialUpdateAdmin", id = UrlParameter.Optional }
             );

            #endregion Admin

            #region Company

            routes.MapRoute(
                name: "Company",
                url: "sirket",
                defaults: new { controller = "Company", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "CompanyDelete",
               url: "sirket-sil/{id}",
               defaults: new { controller = "Company", action = "Delete", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "CompanyAdd",
              url: "sirket-ekle/{id}",
              defaults: new { controller = "Company", action = "Add", id = UrlParameter.Optional }
          );
            routes.MapRoute(
             name: "CompanyUpdate",
             url: "sirket-guncelle/{id}",
             defaults: new { controller = "Company", action = "Update", id = UrlParameter.Optional }
         );
            routes.MapRoute(
              name: "GetCompanyData",
              url: "GetCompanyData/{searchText}",
              defaults: new { controller = "Company", action = "GetCompanyData", searchText = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "PartialAddCompany",
                url: "PartialAddCompany",
                defaults: new { controller = "Company", action = "PartialAddCompany", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "PartialUpdateCompany",
               url: "PartialUpdateCompany/{id}",
               defaults: new { controller = "Company", action = "PartialUpdateCompany", id = UrlParameter.Optional }
           );

            #endregion Company

            #region CompanyUser

            routes.MapRoute(
                name: "CompanyUser",
                url: "sirket-personel",
                defaults: new { controller = "CompanyUser", action = "Index", id = UrlParameter.Optional }
         );
            routes.MapRoute(
               name: "CompanyUserDelete",
               url: "sirket-personel-sil/{id}",
               defaults: new { controller = "CompanyUser", action = "Delete", id = UrlParameter.Optional }
         );
            routes.MapRoute(
              name: "CompanyUserAdd",
              url: "sirket-personel-ekle/{id}",
              defaults: new { controller = "CompanyUser", action = "Add", id = UrlParameter.Optional }
         );
            routes.MapRoute(
             name: "CompanyUserUpdate",
             url: "sirket-personel-guncelle/{id}",
             defaults: new { controller = "CompanyUser", action = "Update", id = UrlParameter.Optional }
         );
            routes.MapRoute(
              name: "GetCompanyUserData",
              url: "GetCompanyUserData/{searchText}",
              defaults: new { controller = "CompanyUser", action = "GetCompanyUserData", searchText = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "PartialAddCompanyUser",
                url: "PartialAddCompanyUser",
                defaults: new { controller = "CompanyUser", action = "PartialAddCompanyUser", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "PartialUpdateCompanyUser",
               url: "PartialUpdateUser/{id}",
               defaults: new { controller = "CompanyUser", action = "PartialUpdateCompanyUser", id = UrlParameter.Optional }
           );

            #endregion CompanyUser
        }
    }
}