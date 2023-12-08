using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Memorial.Core.Domain;
using Memorial.Persistence;

namespace Memorial.Areas.AccessRight.Controllers
{
    public class AccessRightController : Controller
    {
        private MemorialContext db = new MemorialContext();

        // GET: AccessRight/AccessRight
        public ActionResult Index()
        {
            return View(db.AccessControls.ToList());
        }

        // GET: AccessRight/AccessRight/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessControl accessControl = db.AccessControls.Find(id);
            if (accessControl == null)
            {
                return HttpNotFound();
            }
            return View(accessControl);
        }

        // GET: AccessRight/AccessRight/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccessRight/AccessRight/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AncestralTablet,Cemetery,Columbarium,Cremation,Miscellaneous,Urn,Space,ActiveStatus,CreatedById,CreatedDate,ModifiedById,ModifiedDate,DeletedById,DeletedDate")] AccessControl accessControl)
        {
            if (ModelState.IsValid)
            {
                db.AccessControls.Add(accessControl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accessControl);
        }

        // GET: AccessRight/AccessRight/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessControl accessControl = db.AccessControls.Find(id);
            if (accessControl == null)
            {
                return HttpNotFound();
            }
            return View(accessControl);
        }

        // POST: AccessRight/AccessRight/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AncestralTablet,Cemetery,Columbarium,Cremation,Miscellaneous,Urn,Space,ActiveStatus,CreatedById,CreatedDate,ModifiedById,ModifiedDate,DeletedById,DeletedDate")] AccessControl accessControl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accessControl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accessControl);
        }

        // GET: AccessRight/AccessRight/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessControl accessControl = db.AccessControls.Find(id);
            if (accessControl == null)
            {
                return HttpNotFound();
            }
            return View(accessControl);
        }

        // POST: AccessRight/AccessRight/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccessControl accessControl = db.AccessControls.Find(id);
            db.AccessControls.Remove(accessControl);
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
