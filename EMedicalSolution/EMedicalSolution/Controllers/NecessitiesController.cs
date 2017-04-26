using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMedicalSolution.Models;

namespace EMedicalSolution.Controllers
{
    public class NecessitiesController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetNecessities()
        {
            PatientMgmtEntities db = new PatientMgmtEntities();
            {
                var Necessities = db.MedicalNecessities.OrderBy(a => a.ICD10Code).ToList();
                return Json(new
                {
                    data = Necessities.Select(ic => new
                    {
                        ic.ID,
                        ic.ICD10Code,
                        ic.Description,
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
                var v = db.MedicalNecessities.Where(a => a.ID == id).FirstOrDefault();
                return View(v);
            }

        }

        [HttpPost]
        public ActionResult Save(MedicalNecessity necessity)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                using (PatientMgmtEntities db = new PatientMgmtEntities())
                {
                    if (necessity.ID > 0)
                    {
                        //edit 
                        var v = db.MedicalNecessities.Where(a => a.ID == necessity.ID).FirstOrDefault();
                        if (v != null)
                        {
                            v.ICD10Code = necessity.ICD10Code;
                            v.Description = necessity.Description;
                            v.Created = DateTime.Now;
                            v.CreatedBy = 1;
                        }
                    }
                    else
                    {
                        //save
                        necessity.Created = DateTime.Now;
                        necessity.CreatedBy = 1;
                        db.MedicalNecessities.Add(necessity);
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
                var v = db.MedicalNecessities.Where(a => a.ID == id).FirstOrDefault();
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
                var v = db.MedicalNecessities.Where(a => a.ID == id).FirstOrDefault();
                if (v != null)
                {
                    db.MedicalNecessities.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

    }
}
