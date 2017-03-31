using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMedicalSolution.Models;

namespace EMedicalSolution.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        //public ActionResult GetPatients()
        //{
        //    PatientMgmtEntities db = new PatientMgmtEntities();
        //    {
        //        var patients = db.Patients.OrderBy(a => a.FirstName).ToList();
        //        return Json(new { data = patients }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpGet]
        //public ActionResult Save(int id)
        //{
        //    using (PatientMgmtEntities db = new PatientMgmtEntities())
        //    {
        //        var v = db.Patients.Where(a => a.ID == id).FirstOrDefault();
        //        return View(v);
        //    }

        //}

        //[HttpPost]
        //public ActionResult Save(Patient patient)
        //{
        //    bool status = false;

        //    if (ModelState.IsValid)
        //    {
        //        using (PatientMgmtEntities db = new PatientMgmtEntities())
        //        {
        //            if (patient.ID > 0)
        //            {
        //                //edit 
        //                var v = db.Patients.Where(a => a.ID == patient.ID).FirstOrDefault();
        //                if (v != null)
        //                {
        //                    v.FirstName = patient.FirstName;
        //                    v.LastName = patient.LastName;
        //                    v.Gender = patient.Gender;
        //                    v.DOB = patient.DOB;
        //                    v.Created = DateTime.Now;
        //                    v.CreatedBy = 1;
        //                }

        //            }
        //            else
        //            {
        //                //save
        //                patient.Created = DateTime.Now;
        //                patient.CreatedBy = 1;
        //                db.Patients.Add(patient);
        //            }
        //            db.SaveChanges();
        //            status = true;
        //        }

        //    }
        //    return new JsonResult { Data = new { status = status } };
        //}

        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    using (PatientMgmtEntities db = new PatientMgmtEntities())
        //    {
        //        var v = db.Patients.Where(a => a.ID == id).FirstOrDefault();
        //        if (v != null)
        //        {
        //            return View(v);
        //        }
        //        else
        //        {
        //            return HttpNotFound();
        //        }
        //    }
        //}

        //[HttpPost]
        //[ActionName("Delete")]
        //public ActionResult DeletePatient(int id)
        //{
        //    bool status = false;
        //    using (PatientMgmtEntities db = new PatientMgmtEntities())
        //    {
        //        var v = db.Patients.Where(a => a.ID == id).FirstOrDefault();
        //        if (v != null)
        //        {
        //            db.Patients.Remove(v);
        //            db.SaveChanges();
        //            status = true; 
        //        }
        //    }
        //    return new JsonResult { Data = new { status = status } };
        //}

    }
}
