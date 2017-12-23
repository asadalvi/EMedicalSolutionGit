using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMedicalSolution.Models;
using EMedicalSolution.App_Start;

namespace EMedicalSolution.Controllers
{
    [SessionTimeout]
    public class SymptomsController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSymptoms()
        {
            PatientMgmtEntities db = new PatientMgmtEntities();
            {
                var oSymptoms = db.Symptoms.OrderBy(a => a.Title).ToList();
                return Json(new
                {
                    data = oSymptoms.Select(s => new
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
                var v = db.Symptoms.Where(a => a.ID == id).FirstOrDefault();
                var p = string.Join(",", db.SymptomsProcedureMappings.Where(x => x.SymptomID == id).Select(x => x.ProcedureID).ToList());

                ViewBag.procedureTypeIDs = p;
                ViewBag.proTypes = db.ProcedureTypes.ToList();
                return View(v);
            }

        }

        [HttpPost]
        public ActionResult Save(Symptom symptom, string procedureTypeIDs)
        {
            bool status = false;
            var procedureTypeIDsArr = procedureTypeIDs.Split(',');


            if (ModelState.IsValid)
            {
                using (PatientMgmtEntities db = new PatientMgmtEntities())
                {
                    if (symptom.ID > 0)
                    {
                        //edit 
                        var v = db.Symptoms.Where(a => a.ID == symptom.ID).FirstOrDefault();
                        if (v != null)
                        {
                            v.Title = symptom.Title;
                            v.Created = DateTime.Now;
                            v.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
                        }
                    }
                    else
                    {
                        //save
                        symptom.Created = DateTime.Now;
                        symptom.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
                        db.Symptoms.Add(symptom);
                    }
                    db.SaveChanges();

                    int last_id = symptom.ID;
                    db.SymptomsProcedureMappings.RemoveRange(db.SymptomsProcedureMappings.Where(x => x.SymptomID == last_id));
                    SymptomsProcedureMapping mapps = new SymptomsProcedureMapping();
                    //db.SaveChanges();
                    if (procedureTypeIDsArr.Length > 0)
                    {
                        for (var i = 0; i < procedureTypeIDsArr.Length; i++)
                        {
                            mapps.SymptomID = last_id;
                            mapps.ProcedureID = Convert.ToInt32(procedureTypeIDsArr[i]);
                            db.SymptomsProcedureMappings.Add(mapps);
                            db.SaveChanges();
                        }

                    }
                    status = true;
                }

            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteSymptom(int id)
        {
            bool status = false;
            using (PatientMgmtEntities db = new PatientMgmtEntities())
            {
                db.SymptomsProcedureMappings.RemoveRange(db.SymptomsProcedureMappings.Where(x => x.SymptomID == id));
                var v = db.Symptoms.Where(a => a.ID == id).FirstOrDefault();
                if (v != null)
                {
                    db.Symptoms.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

    }
}
