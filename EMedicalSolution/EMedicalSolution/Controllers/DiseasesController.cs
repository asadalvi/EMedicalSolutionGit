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
    public class DiseasesController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDiseases()
        {
            PatientMgmtEntities db = new PatientMgmtEntities();
            {
                var oDiseases = db.Diseases.OrderBy(a => a.Title).ToList();
                return Json(new
                {
                    data = oDiseases.Select(s => new
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
                var v = db.Diseases.Where(a => a.ID == id).FirstOrDefault();
                var p = string.Join(",", db.DiseasesProcedureMpiapngs.Where(x => x.DiseaseID == id).Select(x => x.ProcedureID).ToList());

                ViewBag.procedureTypeIDs = p;
                ViewBag.proTypes = db.ProcedureTypes.ToList();
                return View(v);
            }

        }

        [HttpPost]
        public ActionResult Save(Disease oDisease, string procedureTypeIDs)
        {
            bool status = false;
            var procedureTypeIDsArr = procedureTypeIDs.Split(',');

            if (ModelState.IsValid)
            {
                using (PatientMgmtEntities db = new PatientMgmtEntities())
                {
                    if (oDisease.ID > 0)
                    {
                        //edit 
                        var v = db.Diseases.Where(a => a.ID == oDisease.ID).FirstOrDefault();
                        if (v != null)
                        {
                            v.Title = oDisease.Title;
                            v.DiseaseTypeID = 3;//static data

                            v.Created = DateTime.Now;
                            v.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
                        }
                    }
                    else
                    {
                        //save
                        oDisease.DiseaseTypeID = 3;//static data
                        oDisease.Created = DateTime.Now;
                        oDisease.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
                        db.Diseases.Add(oDisease);
                    }
                    db.SaveChanges();

                    int last_id = oDisease.ID;
                    DiseasesProcedureMpiapng mapps = new DiseasesProcedureMpiapng();
                    //db.SaveChanges();
                    if (procedureTypeIDsArr.Length > 0)
                    {
                        for (var i = 0; i < procedureTypeIDsArr.Length; i++)
                        {
                            mapps.DiseaseID = last_id;
                            mapps.ProcedureID = Convert.ToInt32(procedureTypeIDsArr[i]);
                            db.DiseasesProcedureMpiapngs.Add(mapps);
                            db.SaveChanges();
                        }

                    }
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
                var v = db.Diseases.Where(a => a.ID == id).FirstOrDefault();
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
        public ActionResult DeleteDisease(int id)
        {
            bool status = false;
            using (PatientMgmtEntities db = new PatientMgmtEntities())
            {
                var v = db.Diseases.Where(a => a.ID == id).FirstOrDefault();
                if (v != null)
                {
                    db.Diseases.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

    }
}
