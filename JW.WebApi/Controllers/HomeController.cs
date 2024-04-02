using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JW.WebApi.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        
        [Authorize(Roles = "Adm")]
        public ActionResult SecretPlace()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Shop()
        {
            ViewBag.Message = "Your shop page.";
            return View();
        }

        public ActionResult Blog()
        {
            ViewBag.Message = "Your blog page.";
            return View();        
        }
    }
}