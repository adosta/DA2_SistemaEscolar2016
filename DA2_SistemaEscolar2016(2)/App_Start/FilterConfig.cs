using System.Web;
using System.Web.Mvc;

namespace DA2_SistemaEscolar2016_2_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
