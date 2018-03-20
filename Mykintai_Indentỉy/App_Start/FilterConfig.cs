using Mykintai_Indentỉy.AuthenticationFilters;
using System.Web;
using System.Web.Mvc;

namespace Mykintai_Indentỉy
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new JwtAuthorizeAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
