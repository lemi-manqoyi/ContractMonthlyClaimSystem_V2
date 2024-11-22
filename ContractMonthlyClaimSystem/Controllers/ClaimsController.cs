using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContractMonthlyClaimSystem.Models;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class ClaimsController : Controller
    {
        private ContractMonthlyClaimSystemDBEntities1 db = new ContractMonthlyClaimSystemDBEntities1();

        // GET: Claims
        public ActionResult Index()
        {
            return View(db.Claims.ToList());
        }

        // GET: Claims/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim claim = db.Claims.Find(id);
            if (claim == null)
            {
                return HttpNotFound();
            }
            return View(claim);
        }


        public ActionResult Menu()
        {
            return View();
        }




        // GET: Claims/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Claims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClaimID,SubmittedDate,HoursWorked,HourlyRate,ClaimDocumentPath,Status,UserID")] Claim claim)
        {
            if (ModelState.IsValid)
            {
                claim.ClaimID = Guid.NewGuid();
                db.Claims.Add(claim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(claim);
        }

        // GET: Claims/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim claim = db.Claims.Find(id);
            if (claim == null)
            {
                return HttpNotFound();
            }
            
             var model = new Claim();
             model.Status = "Pending";
             return View(claim );
        }


        // POST: Claims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClaimID,SubmittedDate,HoursWorked,HourlyRate,ClaimDocumentPath,Status,UserID")] Claim claim)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //db.Entry(claim).Property(e => e.RowVersion).OriginalValue = Request.Form["RowVersion"];
                    db.Entry(claim).State = EntityState.Modified;

                    int rowsAffected = db.SaveChanges();

                    if (rowsAffected == 0)
                    {
                        ModelState.AddModelError("", "No changes were detected.");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    ModelState.AddModelError("", "The record you attempted to edit was modified by another user after you got the original value.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An unexpected error occurred: " + ex.Message);
                }
            }
            return View(claim);
        }


        // GET: Claims/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim claim = db.Claims.Find(id);
            if (claim == null)
            {
                return HttpNotFound();
            }
            return View(claim);
        }

        // POST: Claims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Claim claim = db.Claims.Find(id);
            db.Claims.Remove(claim);
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
