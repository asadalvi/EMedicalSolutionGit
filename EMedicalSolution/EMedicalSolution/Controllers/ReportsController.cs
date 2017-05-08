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
    public class ReportsController : Controller
    {
        Utility oUtility = new Utility();
        public ActionResult PatientReports()
        {
            return View();
        }

        public ActionResult HistoryList(int id)
        {
            ViewBag.patientID = id;
            return View();
        }
    }
}
