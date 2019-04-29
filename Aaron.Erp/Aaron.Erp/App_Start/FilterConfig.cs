using Aaron.Erp.App_Start.Filter;
using Aaron.WebCommon;
using System.Web;
using System.Web.Mvc;

namespace Aaron.Erp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new WholeErrorAttribute());
            filters.Add(new LoginFilter());

        }
    }
}
