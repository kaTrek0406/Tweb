using System.Linq;
using System.Net;
using System.Web.Mvc;
using JW.Infrastructure;
using JW.Domain;
using System.Data.Entity;
using JW.WebApi.Security;

namespace JW.Web.Controllers
{
    [RoleAuthorize("Administrator", "Moderator")]
    public class ReviewsController : Controller
    {
        private JewelryStoreContext db = new JewelryStoreContext();

        // GET: Reviews
        public ActionResult Index()
        {
            var reviews = db.Reviews.Include(r => r.JewelryItem).Include(r => r.Customer).ToList();
            return View(reviews);
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            ViewBag.JewelryItemId = new SelectList(db.JewelryItems, "Id", "Name");
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            return View();
        }

        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content,Rating,JewelryItemId,CustomerId")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JewelryItemId = new SelectList(db.JewelryItems, "Id", "Name", review.JewelryItemId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", review.CustomerId);
            return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.JewelryItemId = new SelectList(db.JewelryItems, "Id", "Name", review.JewelryItemId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", review.CustomerId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content,Rating,JewelryItemId,CustomerId")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JewelryItemId = new SelectList(db.JewelryItems, "Id", "Name", review.JewelryItemId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", review.CustomerId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
