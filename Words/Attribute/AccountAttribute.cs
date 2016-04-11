using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utils;
using Words.Account;

namespace Words.Attribute
{
    public class AccountAttribute:ActionFilterAttribute
    {
        public bool IsChecked { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            CookieManage.Cookie = System.Web.HttpContext.Current.Request.Cookies["MYWORD"];
            if (IsChecked) {
                if (CookieManage.HasUserinfo())
                {
                    return;
                }
                else {
                    filterContext.Result = new RedirectResult("/User/Login");
                }
               
            }
        }
    }
}