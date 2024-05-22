using System;
using System.Web.Mvc;
using JW.Domain;
using JW.Infrastructure;

namespace JW.Web.Controllers
{
    public class ReturnController : Controller
    {
        private readonly JewelryStoreContext _context;

        public ReturnController()
        {
            _context = new JewelryStoreContext();
        }

        [HttpGet]
        public ActionResult Form()
        {
            return View(new ReturnRequest());
        }

        [HttpPost]
        public ActionResult SubmitReturn(ReturnRequest model)
        {
            if (ModelState.IsValid)
            {
                model.CustomerId = model.CustomerId;
                model.ReturnDate = DateTime.Now;
                _context.ReturnRequests.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Confirmation");
            }
            return View("Form", model);
        }

        [HttpGet]
        public ActionResult Confirmation()
        {
            return View();
        }
    }
}