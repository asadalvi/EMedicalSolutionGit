using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMedicalSolution.Models;
using System.IO;
using EMedicalSolution.App_Start;

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

        //For Ajax Call 
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
                            v.ModifiedBy = Convert.ToInt32(Session["userID"].ToString());
                        }
                    }
                    else
                    {
                        //save
                        oStaff.Created = DateTime.Now;
                        oStaff.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
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

        public ActionResult EmployeeProfile()
        {
            var Employees = db.Staffs.Find(1);
            return View(Employees);
        }

        public ActionResult SaveEmployeeProfile(Staff oStaff, HttpPostedFileBase SignatureFilePath)
        {
            Staff Employee = db.Staffs.Find(oStaff.ID);
            
            //Doctor tbl = new Doctor();
            var allowedExtensions = new[] {
            ".Jpg", ".png", ".jpg", "jpeg"
        };
            if (ModelState.IsValid)
            {
                Employee.FirstName = oStaff.FirstName;
                Employee.LastName = oStaff.LastName;
                Employee.Gender = oStaff.Gender;
                Employee.DOB = oStaff.DOB;
                Employee.Email = oStaff.Email;
                Employee.Tel = oStaff.Tel;
                Employee.IDNumber = oStaff.IDNumber;
                Employee.SignaturePassword = oStaff.SignaturePassword;

                if (SignatureFilePath != null)
                {
                    oStaff.SignatureFilePath = SignatureFilePath.ToString(); //getting complete url  
                    var fileName = Path.GetFileName(SignatureFilePath.FileName); //getting only file name(ex-ganesh.jpg)  
                    var ext = Path.GetExtension(SignatureFilePath.FileName); //getting the extension(ex-.jpg)  
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension         
                        string myfile = "Signature_" + oStaff.ID + ext; //appending the name with id  
                        string folderPath = Server.MapPath("~/Files/Signatures");
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        // store the file inside ~/project folder(Img)  
                        var path = Path.Combine(folderPath, myfile);
                        Employee.SignatureFilePath = path;
                        SignatureFilePath.SaveAs(path);
                    }
                    else
                    {
                        ViewBag.message = "Please choose only Image file";
                    }
                }
                db.Entry(Employee).CurrentValues.SetValues(Employee);
                db.SaveChanges();
                return RedirectToAction("EmployeeProfile");
            }
            return View("EmployeeProfile", oStaff);
        }
    }
}
