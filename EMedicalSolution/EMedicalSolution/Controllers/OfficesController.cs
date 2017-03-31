using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMedicalSolution.Models;

namespace EMedicalSolution.Controllers
{
    public class OfficesController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetOffices()
        {
            PatientMgmtEntities db = new PatientMgmtEntities();
            {
                var Offices = db.Offices.OrderBy(a => a.Title).ToList();
                return Json(new { data = Offices }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            using (PatientMgmtEntities db = new PatientMgmtEntities())
            {
                var v = db.Offices.Where(a => a.ID == id).FirstOrDefault();
                return View(v);
            }

        }

        [HttpPost]
        public ActionResult Save(Office office)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                using (PatientMgmtEntities db = new PatientMgmtEntities())
                {
                    if (office.ID > 0)
                    {
                        //edit 
                        var v = db.Offices.Where(a => a.ID == office.ID).FirstOrDefault();
                        if (v != null)
                        {
                            v.Title = office.Title;
                            v.Description = office.Description;
                            v.Location = office.Location;
                            v.Tel = office.Tel;
                            v.Email = office.Email;
                            v.Created = DateTime.Now;
                            v.CreatedBy = 1;
                        }

                    }
                    else
                    {
                        //save
                        office.Created = DateTime.Now;
                        office.CreatedBy = 1;
                        db.Offices.Add(office);
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
                var v = db.Offices.Where(a => a.ID == id).FirstOrDefault();
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
                var v = db.Offices.Where(a => a.ID == id).FirstOrDefault();
                if (v != null)
                {
                    db.Offices.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

    }
}
