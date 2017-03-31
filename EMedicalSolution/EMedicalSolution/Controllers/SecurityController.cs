using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMedicalSolution.Models;

namespace EMedicalSolution.Controllers
{
    public class SecurityController : Controller
    {
        //
        // GET: /Security/

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ValidateUser(FormCollection col)
        {
            using (PatientMgmtEntities db = new PatientMgmtEntities())
            {
                string username, password;
                int role = 0;
                username = col["userName"].ToString().Trim();
                password = col["password"].ToString().Trim();
                role = Convert.ToInt32(col["role"].ToString().Trim());
                var u = db.Users.Where(a => a.Username.Equals(username) && a.Password.Equals(password)).FirstOrDefault();

                if (u != null)
                {
                    var s = db.Staffs.Where(a => a.ID.Equals(u.StaffID)).FirstOrDefault();
                    Session["userID"] = u.ID.ToString();
                    Session["userName"] = u.Username.ToString();
                    Session["Name"] = s.FirstName.ToString() + " " + s.LastName.ToString();
                    Session["role"] = role.ToString();

                    return RedirectToAction("Dashboard", "Home");
                }

                ViewData["Error"] = "Incorrect User / Password";
                return View("Login",u);
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

    }
}
