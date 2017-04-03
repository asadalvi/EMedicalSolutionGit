using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMedicalSolution.Models;
using EMedicalSolution.ViewModels;
using Newtonsoft.Json;

namespace EMedicalSolution.Controllers
{
    public class PatientsController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PatientsList()
        {
            return View();
        }
        public ActionResult Wizard()
        {
            patientIntakeViewModel intakeView = new patientIntakeViewModel();
            PatientMgmtEntities db = new PatientMgmtEntities();

            intakeView.objSymptom = db.Symptoms.ToList();
            intakeView.objDisease = db.Diseases.ToList();
            intakeView.objProcedureType = db.ProcedureTypes.ToList();
            intakeView.objMedicalNecessity = db.MedicalNecessities.ToList();
            return View(intakeView);
        }

        public ActionResult GetPatients()
        {
            PatientMgmtEntities db = new PatientMgmtEntities();
            {
                List<Patient> patients = db.Patients.OrderBy(a => a.FirstName).ToList();
                return Json(new
                {
                    data = patients.Select(p => new
                    {
                        p.ID,
                        p.FirstName,
                        p.LastName,
                        p.Gender,
                        p.DOB,
                        p.IDNumber,
                        p.Tel,
                        p.Email,
                        p.Created
                    }
                )
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult HistoryList(int id)
        {          
            PatientMgmtEntities db = new PatientMgmtEntities();
            {
                var pHistory = (from p in db.Patients
                                join ph in db.PatientHistories on p.ID equals ph.PatientID
                                join i in db.InsuranceTypes on ph.InsuranceTypeID equals i.ID
                                where p.ID == id
                               // select new { ph.ID, p.FirstName, p.LastName, i.Title }).ToList();
                select new PatientHistoryVM
                {
                    ID = ph.ID,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Title = i.Title,
                    Created = i.Created
                });
               // ViewBag.pHistory = pHistory.ToList();

                //pHistoryVM = phistory.ToList();
                //db.PatientHistories
                //                    .Where(h => h.PatientID.Equals(patientid)).OrderByDescending(a => a.Created).ToList();
                //return Json(new
                //{
                //    data = pHistory.Select(p => new
                //    {
                //        p.ID,
                //        p.FirstName,
                //        p.LastName,
                //        p.Title,
                //        p.Created
                //    }
                //)
                //}, JsonRequestBehavior.AllowGet);
                return View(pHistory);
            }
        }

        [HttpGet]
        public ActionResult Patient(int id)
        {
            using (PatientMgmtEntities db = new PatientMgmtEntities())
            {
                var v = db.Patients.Where(a => a.ID == id).FirstOrDefault();
                return View(v);
            }

        }

        [HttpPost]
        public JsonResult patient(Patient patient, string insurance)
        {
            bool status = false;
            int pHistoryId = 0;
            PatientHistory ph;
            if (ModelState.IsValid)
            {
                using (PatientMgmtEntities db = new PatientMgmtEntities())
                {
                    ph = new PatientHistory();
                    if (patient.ID > 0)
                    {
                        //edit 
                        var v = db.Patients.Where(a => a.ID == patient.ID).FirstOrDefault();
                        if (v != null)
                        {
                            v.FirstName = patient.FirstName;
                            v.LastName = patient.LastName;
                            v.Gender = patient.Gender;
                            v.DOB = patient.DOB;
                            v.IDNumber = patient.IDNumber;
                            v.Tel = patient.Tel;
                            v.Email = patient.Email;
                            v.Modified = DateTime.Now;
                            v.ModifiedBy = 1;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        //save
                        patient.Created = DateTime.Now;
                        patient.CreatedBy = 1;
                        db.Patients.Add(patient);  //Saving new object
                        db.SaveChanges();

                        ph.PatientID = patient.ID; //Getting inserted ID

                        ph.InsuranceTypeID = Convert.ToInt32(insurance);
                        ph.Created = DateTime.Now;
                        ph.CreatedBy = 1;
                        db.PatientHistories.Add(ph);
                        db.SaveChanges();

                        ViewBag.pHistoryId = ph.ID;
                        pHistoryId = ph.ID; //getting last inserted History ID
                    }
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status, pHistoryId = pHistoryId } };

            //return Json(status,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult procedureTypes(string type, int pHistoryId)
        {
            bool status = false;
            PatientProcedure pProcedure;
            if (ModelState.IsValid)
            {
                string[] myType = type.Split(',');
                //int[] myType = Array.ConvertAll(type, s => int.Parse(s));
                using (PatientMgmtEntities db = new PatientMgmtEntities())
                {
                    pProcedure = new PatientProcedure();
                    if (pHistoryId > 0)
                    {
                        pProcedure.HistoryID = pHistoryId;
                        for (int i = 0; i < myType.Length; i++)
                        {
                            pProcedure.ProcedureTypeID = Convert.ToInt32(myType[i]);
                            pProcedure.Created = DateTime.Now;
                            pProcedure.CreatedBy = 1;
                            db.PatientProcedures.Add(pProcedure);
                            db.SaveChanges();
                        }
                        status = true;
                    }
                }
            }
            return new JsonResult { Data = new { status = status } };

        }
        [HttpGet]
        public ActionResult intake()
        {
            //SelectListItem listItem 
            patientIntakeViewModel intakeView = new patientIntakeViewModel();
            PatientMgmtEntities db = new PatientMgmtEntities();

            intakeView.objSymptom = db.Symptoms.ToList();
            intakeView.objDisease = db.Diseases.ToList();//.Include(a => a.dis).ToList();//Select(diseise => new Disease { Title = diseise.Title, ID = diseise.ID }).ToList();

            //  return Json(intakeView, JsonRequestBehavior.AllowGet);
            var result = JsonConvert.SerializeObject(intakeView, Formatting.Indented,
                             new JsonSerializerSettings
                             {
                                 ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                             });
            // return new JsonResult { Data = new { intakeView = intakeView } };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult intake(string value)
        //{
        //    bool status = false;

        //    status = true;

        //    return Json(status, JsonRequestBehavior.AllowGet);
        //}
        //[HttpPost]
        //public JsonResult intake(string value)
        //{
        //    bool status = false;

        //    status = true;

        //    return Json(status, JsonRequestBehavior.AllowGet);
        //}
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (PatientMgmtEntities db = new PatientMgmtEntities())
            {
                var v = db.Patients.Where(a => a.ID == id).FirstOrDefault();
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
        public ActionResult DeletePatient(int id)
        {
            bool status = false;
            using (PatientMgmtEntities db = new PatientMgmtEntities())
            {
                var v = db.Patients.Where(a => a.ID == id).FirstOrDefault();
                if (v != null)
                {
                    db.Patients.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

    }
}