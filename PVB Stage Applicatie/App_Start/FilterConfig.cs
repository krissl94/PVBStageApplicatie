using System.Web;
using System.Web.Mvc;

namespace PVB_Stage_Applicatie
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}