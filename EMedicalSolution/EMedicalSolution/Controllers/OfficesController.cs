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
                var offices = db.Offices.OrderBy(a => a.Title).ToList();
                return Json(new
                {
                    data = offices.Select(o => new
                    {
                        o.ID,
                        o.Title,
                        o.Description,
                        o.Location,
                        o.Tel,
                        o.Email,
                        o.Created
                    }
                    )
                }, JsonRequestBehavior.AllowGet);
                //return Json(new { data = Offices }, JsonRequestBehavior.AllowGet);
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
                            v.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
                        }

                    }
                    else
                    {
                        //save
                        office.Created = DateTime.Now;
                        office.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
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
