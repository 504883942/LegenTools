using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Words.Attribute;

namespace Words.Controllers
{
    public class HomeController : BaseController
    {
        [AccountAttribute(IsChecked = false)]
        public ActionResult Index()
        {
            return View();
        }
        [AccountAttribute(IsChecked = false)]
        public ActionResult Detail(int id)
        {
            var thisword = getWords().Where(p => p.ID == id).First();
            ViewBag.childwords = getchildSimilar(thisword);
            ViewBag.parentwords = getparentSimilar(thisword);
            return View(thisword);
        }
        [AccountAttribute(IsChecked = false)]
        public ActionResult Search(string words) {
            var list = getWords().Where(p =>p.words.Contains(words)).ToList();
            return View(list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}