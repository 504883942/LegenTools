using System.Web;
using System.Web.Mvc;
using Words.Attribute;

namespace Words
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AccountAttribute() { IsChecked=true});
        }
    }
}
