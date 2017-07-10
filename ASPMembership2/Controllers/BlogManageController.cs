using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ASPMembership2.DataAccess;

namespace ASPMembership2.Controllers
{
    public class BlogManageController : Controller
    {
        private EntityModel db = new EntityModel();

        private const string className = "BlogManageController";

        [HttpGet]
        [AuthorizeRoles(className)]
        public ActionResult Index()
        {
            var blogItem = db.BlogItem.Include(b => b.AspNetUsers);
            return View(blogItem.ToList());
        }

        // GET: BlogManage/Details/5
        [AuthorizeRoles(className)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogItem blogItem = db.BlogItem.Find(id);
            if (blogItem == null)
            {
                return HttpNotFound();
            }
            return View(blogItem);
        }

        // GET: BlogManage/Create
        [AuthorizeRoles(className)]
        public ActionResult Create()
        {
            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: BlogManage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles(className)]
        public ActionResult Create([Bind(Include = "Id,Title,Body,CreatedOn,CreatedBy")] BlogItem blogItem)
        {
            if (ModelState.IsValid)
            {
                db.BlogItem.Add(blogItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email", blogItem.CreatedBy);
            return View(blogItem);
        }

        // GET: BlogManage/Edit/5
        [AuthorizeRoles(className)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogItem blogItem = db.BlogItem.Find(id);
            if (blogItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email", blogItem.CreatedBy);
            return View(blogItem);
        }

        // POST: BlogManage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRoles(className)]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,CreatedOn,CreatedBy")] BlogItem blogItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = new SelectList(db.AspNetUsers, "Id", "Email", blogItem.CreatedBy);
            return View(blogItem);
        }

        // GET: BlogManage/Delete/5
        [AuthorizeRoles(className)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogItem blogItem = db.BlogItem.Find(id);
            if (blogItem == null)
            {
                return HttpNotFound();
            }
            return View(blogItem);
        }

        // POST: BlogManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogItem blogItem = db.BlogItem.Find(id);
            db.BlogItem.Remove(blogItem);
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
