using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMedicalSolution.Models;
using EMedicalSolution.App_Start;

namespace EMedicalSolution.Controllers
{
    [SessionTimeout]
    public class UsersController : Controller
    {
        PatientMgmtEntities db = new PatientMgmtEntities();
        //
        // GET: /Users/

        public ActionResult UsersList()
        {
            var users = db.Users.Include(ur => ur.Staff).Include(u => u.UserRole).OrderBy(a => a.ID).ToList();
            return View(users);
        }

        //For Ajax Call -- Not using right now
        public ActionResult GetUsers()
        {
            PatientMgmtEntities db = new PatientMgmtEntities();
            {
                var users = db.Users.Include(ur => ur.Staff).Include(u => u.UserRole).OrderBy(a => a.ID).ToList();
                return Json(new
                {
                    data = users.Select(e => new
                    {
                        e.ID,
                        e.Username,
                        Name = e.Staff.FirstName + " " + e.Staff.LastName,
                        Role = e.UserRole.Title,
                        e.Created
                    }
                    )
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult SaveUser(int id)
        {
            PatientMgmtEntities db = new PatientMgmtEntities();
            var v = db.Users.Include(ur => ur.Staff).Include(u => u.UserRole).Where(a => a.ID == id).FirstOrDefault();
            if (id > 0)
            {
                ViewBag.RoleID = new SelectList(db.UserRoles, "ID", "Title", v.RoleID);
                ViewBag.StaffID = new SelectList(db.Staffs, "ID", "FirstName", v.StaffID);
            }
            else
            {
                ViewBag.RoleID = new SelectList(db.UserRoles, "ID", "Title");
                ViewBag.StaffID = new SelectList(db.Staffs, "ID", "FirstName");
            }
            return View(v);

        }

        [HttpPost]
        public ActionResult SaveUser(User user)
        {
            bool status = false;
            string errorMsg = "";

            if (ModelState.IsValid)
            {
                using (PatientMgmtEntities db = new PatientMgmtEntities())
                {
                    var emp = db.Users.Where(s => s.StaffID == user.StaffID).FirstOrDefault();
                    if (user.ID > 0)
                    {
                        if (emp.ID == user.ID)
                        {
                            //edit
                            var v = db.Users.Where(a => a.ID == user.ID).FirstOrDefault();
                            if (v != null)
                            {
                                v.Username = user.Username;
                                v.Password = user.Password;
                                v.RoleID = user.RoleID;
                                v.StaffID = user.StaffID;
                                v.Modified = DateTime.Now;
                                v.ModifiedBy = Convert.ToInt32(Session["userID"].ToString());
                                db.SaveChanges();
                                status = true;
                            }
                        }
                        else
                        {
                            errorMsg = "Selected employee already has a user";
                            status = false;
                        }
                    }
                    else
                    {
                        var u = db.Users.Where(a => a.Username == user.Username).FirstOrDefault();
                        if (u != null)
                        {
                            errorMsg = "User already exist";
                            status = false;
                        }
                        else
                        {
                            if (emp != null)
                            {
                                errorMsg = "Selected employee already has a user";
                                status = false;
                            }
                            else
                            {
                                user.Created = DateTime.Now;
                                user.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
                                db.Users.Add(user);
                                db.SaveChanges();
                                status = true;
                            }
                        }


                    }

                }
            }
            return new JsonResult { Data = new { status = status, errorMsg = errorMsg } };
        }

        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            var v = db.Users.Where(a => a.ID == id).FirstOrDefault();
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
        [ActionName("DeleteUser")]
        public ActionResult DeleteUserDeleteUser(int id)
        {
            bool status = false;

            var v = db.Users.Where(a => a.ID == id).FirstOrDefault();
            if (v != null)
            {
                db.Users.Remove(v);
                db.SaveChanges();
                status = true;
            }

            return new JsonResult { Data = new { status = status } };
        }
    }
}
