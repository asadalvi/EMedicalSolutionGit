using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMedicalSolution.Models;

namespace EMedicalSolution.Controllers
{
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
                return View(v);
            }

        }

        [HttpPost]
        public ActionResult Save(Disease oDisease)
        {
            bool status = false;

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
                            v.Created = DateTime.Now;
                            v.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
                        }

                    }
                    else
                    {
                        //save
                        oDisease.Created = DateTime.Now;
                        oDisease.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
                        db.Diseases.Add(oDisease);
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
