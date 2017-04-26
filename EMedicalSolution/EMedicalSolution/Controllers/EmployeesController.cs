using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMedicalSolution.Models;

namespace EMedicalSolution.Controllers
{
    public class EmployeesController : Controller
    {
        PatientMgmtEntities db = new PatientMgmtEntities();
        //
        // GET: /Employees/

        public ActionResult EmployeesList()
        {
            var Employees = db.Staffs.OrderBy(a => a.ID).ToList();
            return View(Employees);
        }

        //For Ajax Call -- Not using right now
        public ActionResult GetEmployees()
        {
            PatientMgmtEntities db = new PatientMgmtEntities();
            {
                var Employees = db.Staffs.Include(o => o.Office).OrderBy(a => a.ID).ToList();
                return Json(new
                {
                    data = Employees.Select(e => new
                    {
                        e.ID,
                        Name = e.FirstName + " " + e.LastName,
                        e.Gender,
                        e.DOB,
                        e.Tel,
                        e.Email,
                        e.IDNumber,
                        OfficeName = e.Office.Title,
                        e.Created
                    }
                    )
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult SaveEmployee(int id)
        {
            PatientMgmtEntities db = new PatientMgmtEntities();
            var v = db.Staffs.Where(a => a.ID == id).FirstOrDefault();
            if (id > 0)
            {
                ViewBag.OfficeID = new SelectList(db.Offices, "ID", "Title", v.OfficeID);
            }
            else
            {
                ViewBag.OfficeID = new SelectList(db.Offices, "ID", "Title");
            }
            return View(v);
        }

        [HttpPost]
        public ActionResult SaveEmployee(Staff oStaff)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                using (PatientMgmtEntities db = new PatientMgmtEntities())
                {
                    if (oStaff.ID > 0)
                    {
                        //edit 
                        var v = db.Staffs.Where(a => a.ID == oStaff.ID).FirstOrDefault();
                        if (v != null)
                        {
                            v.FirstName = oStaff.FirstName;
                            v.LastName = oStaff.LastName;
                            v.DOB = oStaff.DOB;
                            v.Gender = oStaff.Gender;
                            v.IDNumber = oStaff.IDNumber;
                            v.Email = oStaff.Email;
                            v.Tel = oStaff.Tel;
                            v.OfficeID = oStaff.OfficeID;
                            v.Modified = DateTime.Now;
                            v.ModifiedBy = 1;
                        }
                    }
                    else
                    {
                        //save
                        oStaff.Created = DateTime.Now;
                        oStaff.CreatedBy = 1;
                        db.Staffs.Add(oStaff);
                    }
                    db.SaveChanges();
                    status = true;
                }

            }
            //return RedirectToAction("EmployeesList");
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult DeleteEmployee(int id)
        {
            var v = db.Staffs.Where(a => a.ID == id).FirstOrDefault();
            if (v != null)
            {
                return View(v);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ActionName("DeleteEmployee")]
        public ActionResult ConfirmDeleteEmployee(int id)
        {
            bool status = false;

            var v = db.Staffs.Where(a => a.ID == id).FirstOrDefault();
            if (v != null)
            {
                db.Staffs.Remove(v);
                db.SaveChanges();
                status = true;
            }

            return new JsonResult { Data = new { status = status } };
        }
    }
}
