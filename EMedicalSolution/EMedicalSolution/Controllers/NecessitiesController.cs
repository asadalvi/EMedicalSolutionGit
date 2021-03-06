﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMedicalSolution.Models;
using EMedicalSolution.App_Start;

namespace EMedicalSolution.Controllers
{
    [SessionTimeout]
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
                var p = string.Join(",", db.NecessitiesProcedureMappings.Where(x => x.NecessityID == id).Select(x => x.ProcedureID).ToList());

                ViewBag.procedureTypeIDs = p;
                ViewBag.proTypes = db.ProcedureTypes.ToList();
                return View(v);
            }

        }

        [HttpPost]
        public ActionResult Save(MedicalNecessity necessity, string procedureTypeIDs)
        {
            bool status = false;
            var procedureTypeIDsArr = procedureTypeIDs.Split(',');


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
                            v.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
                        }
                    }
                    else
                    {
                        //save
                        necessity.Created = DateTime.Now;
                        necessity.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
                        db.MedicalNecessities.Add(necessity);
                    }
                    db.SaveChanges();

                    int last_id = necessity.ID;
                    db.NecessitiesProcedureMappings.RemoveRange(db.NecessitiesProcedureMappings.Where(x => x.NecessityID == last_id));
                    NecessitiesProcedureMapping mapps = new NecessitiesProcedureMapping();
                    //db.SaveChanges();
                    if (procedureTypeIDsArr.Length > 0)
                    {
                        for (var i = 0; i < procedureTypeIDsArr.Length; i++)
                        {
                            mapps.NecessityID = last_id;
                            mapps.ProcedureID = Convert.ToInt32(procedureTypeIDsArr[i]);
                            db.NecessitiesProcedureMappings.Add(mapps);
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
                db.NecessitiesProcedureMappings.RemoveRange(db.NecessitiesProcedureMappings.Where(x => x.NecessityID == id));
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
