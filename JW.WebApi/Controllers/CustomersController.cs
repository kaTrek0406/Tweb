using System.Linq;
using System.Web.Mvc;
using JW.BusinessLogic.Services;
using JW.Domain;
using JW.Infrastructure;

namespace JW.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly JewelryStoreContext _context;

        public CustomersController()
        {
            _context = new JewelryStoreContext();
        }

        public ActionResult Index()
        {
            var customers = _context.Customers.ToList();
            return View(customers);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customer customer, string password)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(password))
                {
                    customer.PasswordHash = AuthService.CreatePasswordHash(password);
                }
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(Customer customer, string password)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(password))
                {
                    customer.PasswordHash = AuthService.CreatePasswordHash(password);
                }
                else
                {
                    _context.Entry(customer).Property(x => x.PasswordHash).IsModified = false;
                }
                _context.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        public ActionResult Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var customer = _context.Customers.Find(id);
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
