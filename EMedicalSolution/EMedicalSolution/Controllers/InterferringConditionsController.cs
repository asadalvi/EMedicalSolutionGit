using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMedicalSolution.Models;

namespace EMedicalSolution.Controllers
{
    public class InterferingConditionsController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetInterferingConditions()
        {
            PatientMgmtEntities db = new PatientMgmtEntities();
            {
                var InterferingConditions = db.InterferingConditions.OrderBy(a => a.Title).ToList();
                return Json(new
                {
                    data = InterferingConditions.Select(ic => new
                    {
                        ic.ID,
                        ic.Title,
                        ic.Created,

                    })
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            using (PatientMgmtEntities db = new PatientMgmtEntities())
            {
                var v = db.InterferingConditions.Where(a => a.ID == id).FirstOrDefault();
                return View(v);
            }

        }

        [HttpPost]
        public ActionResult Save(InterferingCondition condition)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                using (PatientMgmtEntities db = new PatientMgmtEntities())
                {
                    if (condition.ID > 0)
                    {
                        //edit 
                        var v = db.InterferingConditions.Where(a => a.ID == condition.ID).FirstOrDefault();
                        if (v != null)
                        {
                            v.Title = condition.Title;
                            v.Created = DateTime.Now;
                            v.CreatedBy = 1;
                        }

                    }
                    else
                    {
                        //save
                        condition.Created = DateTime.Now;
                        condition.CreatedBy = 1;
                        db.InterferingConditions.Add(condition);
                    }
                    db.SaveChanges();
                    status = true;
                }

            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (PatientMgmtEntities db = new PatientMgmtEntities())
            {
                var v = db.InterferingConditions.Where(a => a.ID == id).FirstOrDefault();
                if (v != null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteCondition(int id)
        {
            bool status = false;
            using (PatientMgmtEntities db = new PatientMgmtEntities())
            {
                var v = db.InterferingConditions.Where(a => a.ID == id).FirstOrDefault();
                if (v != null)
                {
                    db.InterferingConditions.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

    }
}
