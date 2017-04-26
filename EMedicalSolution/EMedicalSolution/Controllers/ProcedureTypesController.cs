using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMedicalSolution.Models;

namespace EMedicalSolution.Controllers
{
    public class ProcedureTypesController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetProcedureTypes()
        {
            PatientMgmtEntities db = new PatientMgmtEntities();
            {
                var oProcedureTypes = db.ProcedureTypes.OrderBy(a => a.Title).ToList();
                return Json(new
                {
                    data = oProcedureTypes.Select(s => new
                    {
                        s.ID,
                        s.Title,
                        s.Created,
                        s.CreatedBy
                    })
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            using (PatientMgmtEntities db = new PatientMgmtEntities())
            {
                var v = db.ProcedureTypes.Where(a => a.ID == id).FirstOrDefault();
                return View(v);
            }

        }

        [HttpPost]
        public ActionResult Save(ProcedureType oProcedureType)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                using (PatientMgmtEntities db = new PatientMgmtEntities())
                {
                    if (oProcedureType.ID > 0)
                    {
                        //edit 
                        var v = db.ProcedureTypes.Where(a => a.ID == oProcedureType.ID).FirstOrDefault();
                        if (v != null)
                        {
                            v.Title = oProcedureType.Title;
                            v.Created = DateTime.Now;
                            v.CreatedBy = 1;
                        }

                    }
                    else
                    {
                        //save
                        oProcedureType.Created = DateTime.Now;
                        oProcedureType.CreatedBy = 1;
                        db.ProcedureTypes.Add(oProcedureType);
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
                var v = db.ProcedureTypes.Where(a => a.ID == id).FirstOrDefault();
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
        public ActionResult DeleteSymptom(int id)
        {
            bool status = false;
            using (PatientMgmtEntities db = new PatientMgmtEntities())
            {
                var v = db.ProcedureTypes.Where(a => a.ID == id).FirstOrDefault();
                if (v != null)
                {
                    db.ProcedureTypes.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

    }
}
