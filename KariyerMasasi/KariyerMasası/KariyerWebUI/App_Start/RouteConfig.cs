using System.Web.Mvc;
using System.Web.Routing;

namespace KariyerWebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


      //      #region UserDetail

      //      routes.MapRoute(
      //          name: "UserDetail",
      //          url: "kullanici-detay/{id}",
      //          defaults: new { controller = "UserDetail", action = "Index", id = UrlParameter.Optional }
      //      );

      //      routes.MapRoute(
      //        name: "UserWorkAdd",
      //        url: "kullanici-is-ekle/{id}",
      //        defaults: new { controller = "UserDetail", action = "AddWork", id = UrlParameter.Optional }
      //    );
      //      routes.MapRoute(
      //        name: "UserWorkEdit",
      //        url: "kullanici-is-duzenle/{id}",
      //        defaults: new { controller = "UserDetail", action = "EditWork", id = UrlParameter.Optional }
      //    );
      //      #region Delete
      //      routes.MapRoute(
      //        name: "UserWorkDelete",
      //        url: "kullanici-is-sil/{id}",
      //        defaults: new { controller = "UserDetail", action = "DeleteWork", id = UrlParameter.Optional }
      //    );
      //      routes.MapRoute(
      //      name: "UserEducationDelete",
      //      url: "kullanici-egitim-sil/{id}",
      //      defaults: new { controller = "UserDetail", action = "DeleteEducation", id = UrlParameter.Optional }
      //  );
      //      routes.MapRoute(
      //      name: "UserReferenceDelete",
      //      url: "kullanici-referans-sil/{id}",
      //      defaults: new { controller = "UserDetail", action = "DeleteReference", id = UrlParameter.Optional }
      //  );
      //      routes.MapRoute(
      //      name: "UserCertificateDelete",
      //      url: "kullanici-sertifika-sil/{id}",
      //      defaults: new { controller = "UserDetail", action = "DeleteCertificate", id = UrlParameter.Optional }
      //  );
      //      routes.MapRoute(
      //      name: "UserSeminarDelete",
      //      url: "kullanici-seminer-sil/{id}",
      //      defaults: new { controller = "UserDetail", action = "DeleteSeminar", id = UrlParameter.Optional }
      //  );
      //      #endregion
      //      routes.MapRoute(
      //       name: "UserDetailUpdate",
      //       url: "kullanici-detay-guncelle/{id}",
      //       defaults: new { controller = "UserDetail", action = "Update", id = UrlParameter.Optional }
      //   );
      //      routes.MapRoute(
      //      name: "PartialBusinessAdd",
      //      url: "kullanici-isbilgisi-ekle/{id}",
      //      defaults: new { controller = "UserDetail", action = "PartialBusinessAdd", id = UrlParameter.Optional }
      //  );
      //      routes.MapRoute(
      //     name: "PartialBusinessEdit",
      //     url: "kullanici-isbilgisi-duzenle/{id}",
      //     defaults: new { controller = "UserDetail", action = "PartialBusinessEdit", id = UrlParameter.Optional }
      // );
      //      routes.MapRoute(
      //    name: "PartialEducationAdd",
      //    url: "kullanici-egitim-ekle/{id}",
      //    defaults: new { controller = "UserDetail", action = "PartialEducationAdd", id = UrlParameter.Optional }
      //);
      //      routes.MapRoute(
      //     name: "PartialEducationEdit",
      //     url: "kullanici-egitim-duzenle/{id}",
      //     defaults: new { controller = "UserDetail", action = "PartialEducationEdit", id = UrlParameter.Optional }
      // );
      //      #endregion UserDetail

        }
    }
}