using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMedicalSolution.Models;
using EMedicalSolution.ViewModels;
using Newtonsoft.Json;
using System.IO;

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
            intakeView.objInterferingCondition = db.InterferingConditions.ToList();
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
            ViewBag.patientID = id;
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
                ViewBag.PatienName = db.Patients.Where(a => a.ID == id).Select(p => p.FirstName + " " + p.LastName).FirstOrDefault();
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
        public JsonResult patient(Patient patient)
        {
            bool status = false;
            int pHistoryId = 0;
            if (ModelState.IsValid)
            {
                using (PatientMgmtEntities db = new PatientMgmtEntities())
                {
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
                    }
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status, pHistoryId = pHistoryId } };

            //return Json(status,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult procedureTypes(string[] procedureType,int pHistoryId)
        {
             bool status = false;
            PatientProcedure pProcedure;
            if (ModelState.IsValid)
            {
                //string[] myType = procedureType.Split(',');
                 int[] myType = Array.ConvertAll(procedureType, s => int.Parse(s));
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
            //return new JsonResult { Data = new { status = status } };
            return PartialView();
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

        [HttpPost]
        public JsonResult intake(string[] symptom, string[] disease, int[] condition, string supportDevice, int pregnant, int pHistoryId)
        {
            bool status = false;
            int[] diseases;
            int[] symptoms;
            PatientDiseas patientDiseas;
            PatientSymtom patientSymtom;
            PatientInterferingCondition patientInterferingCondition;
            IntakeFormHead intakeFormHead;
            if (ModelState.IsValid)
            {
                using (PatientMgmtEntities db = new PatientMgmtEntities())
                {
                    intakeFormHead = new IntakeFormHead();
                    intakeFormHead.HistoryID = pHistoryId;
                    intakeFormHead.isPregnant = pregnant;
                    intakeFormHead.HaveSupportDevice = supportDevice;
                    intakeFormHead.Date = DateTime.Today;
                    intakeFormHead.Created = DateTime.Now;
                    intakeFormHead.CreatedBy = 1;
                    db.IntakeFormHeads.Add(intakeFormHead);
                    db.SaveChanges();
                    int intakeId = intakeFormHead.ID;
                    //intakeFormHead.HaveSupportDevice = 
                    if (symptom.Length > 0 && intakeId > 0)
                    {
                        patientSymtom = new PatientSymtom();
                        symptoms = Array.ConvertAll(symptom, s => int.Parse(s));
                        for (int i = 0; i < symptom.Length; i++)
                        {
                            patientSymtom.IntakeFormID = intakeId;
                            patientSymtom.HistoryID = pHistoryId;
                            patientSymtom.SymptomID = Convert.ToInt32(symptom[i]);
                            patientSymtom.Created = DateTime.Now;
                            patientSymtom.CreatedBy = 1;
                            db.PatientSymtoms.Add(patientSymtom);
                            db.SaveChanges();
                        }
                    }
                    if (disease.Length > 0 && intakeId > 0)
                    {
                        patientDiseas = new PatientDiseas();
                        diseases = Array.ConvertAll(disease, s => int.Parse(s));
                        for (int i = 0; i < diseases.Length; i++)
                        {
                            patientDiseas.IntakeFormID = intakeId;
                            patientDiseas.HistoryID = pHistoryId;
                            patientDiseas.DiseaseID = Convert.ToInt32(diseases[i]);
                            patientDiseas.Created = DateTime.Now;
                            patientDiseas.CreatedBy = 1;
                            db.PatientDiseases.Add(patientDiseas);
                            db.SaveChanges();
                        }
                    }
                    if (condition.Length > 0 && intakeId > 0)
                    {
                        for (int i = 0; i < condition.Length; i++)
                        {
                            patientInterferingCondition = new PatientInterferingCondition();
                            patientInterferingCondition.IntakeFormID = intakeId;
                            patientInterferingCondition.HistoryID = pHistoryId;
                            patientInterferingCondition.PatientConditionID = Convert.ToInt32(condition[i]);
                            patientInterferingCondition.Created = DateTime.Now;
                            patientInterferingCondition.CreatedBy = 1;
                            db.PatientInterferingConditions.Add(patientInterferingCondition);
                            db.SaveChanges();
                        }
                    }
                }

            
            status = true;
        }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

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
        [HttpGet]
        public ActionResult PatientInsuranceType(int id)
        {
            ViewBag.patientID = id;
            using (PatientMgmtEntities db = new PatientMgmtEntities())
            {
                IList<InsuranceType> iType = db.InsuranceTypes.ToList();
                return View(iType);
            }

        }
        [HttpPost]
        public ActionResult SavePatientInsuranceType()
        {
            bool status = false;
            int  pId = Convert.ToInt32(Request.Form["pId"]);
            int typeid = Convert.ToInt32(Request.Form["typeId"]);
            PatientHistory ph = new PatientHistory();
            ph.PatientID = pId; //Getting inserted ID

            ph.InsuranceTypeID = typeid;
            ph.Created = DateTime.Now;
            ph.CreatedBy = 1;
            using (PatientMgmtEntities db = new PatientMgmtEntities())
            {
                db.PatientHistories.Add(ph);
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpGet]
        public ActionResult GetReports(int pHistoryId)
        {
            PatientMgmtEntities db = new PatientMgmtEntities();
            {
                List<PatientReport> reports = db.PatientReports.Where(h => h.HistoryID == pHistoryId).OrderByDescending(a => a.Created).ToList();
                return Json(new
                {
                    data = reports.Select(p => new
                    {
                        p.ID,
                        p.Title,
                        p.FilePath,
                        p.Created
                    }
                )
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public FileResult DownloadTestReport(string fileName)
        {
            var filePath = Path.Combine(Server.MapPath("/Files/Reports/"), fileName);
            return File(filePath, MimeMapping.GetMimeMapping(filePath), fileName);
        }

        [HttpPost]
        public ActionResult UploadReports(HttpPostedFileBase[] filePath, string[] filetitle, int pHistoryId)
        {
            bool status = false;
            if (filePath != null)
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg", ".rar", ".zip", ".pdf", ".doc", ".docx", ".xls", ".xlsx" }; //Allowe Extensions
                int i = -1;
                foreach (var file in filePath)
                {
                    i++;
                    if (file != null && file.ContentLength > 0)
                    {
                        //oTests[i].ReportPath = files[i].ToString(); //getting complete url  
                        var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                        var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  

                        if (allowedExtensions.Contains(ext)) //check what type of extension  
                        {
                            PatientReport PatientReports = new PatientReport();
                            string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                            using (PatientMgmtEntities db = new PatientMgmtEntities())
                            {
                                int id = db.PatientReports.Max(p => (int?)p.ID) ?? 0 + 1;
                                string myfile = "Report_" + id + ext; //appending the name with id  
                                                                      // store the file inside ~/project folder(Img)  
                                var path = Path.Combine(Server.MapPath("~/Files/Reports"), myfile);
                                file.SaveAs(path);
                                PatientReports.FilePath = path;
                                PatientReports.HistoryID = pHistoryId;
                                PatientReports.Title = filetitle[i];
                                PatientReports.Created = DateTime.Now;
                                PatientReports.CreatedBy = 1;
                                db.PatientReports.Add(PatientReports);
                                db.SaveChanges();
                            }
                        }

                    } //checking file is not empty 
                } // for loop end
                status = true;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveMedicalNecessities(int[] necessities, int pHistoryId, string txtsecondary)
        {
            bool status = false;
            PatientNecessity pNecessity;
            PatientHistory pHistsory;
            if (ModelState.IsValid)
            {
                //string[] myType = procedureType.Split(',');
                //int[] pNecessities = Array.ConvertAll(necessities, s => int.Parse(s));
                using (PatientMgmtEntities db = new PatientMgmtEntities())
                {
                    pNecessity = new PatientNecessity();
                    pHistsory = new PatientHistory();
                    if (pHistoryId > 0)
                    {
                        pNecessity.HistoryID = pHistoryId;

                        for (int i = 0; i < necessities.Length; i++)
                        {
                            pNecessity.NecessityID = Convert.ToInt32(necessities[i]);
                            pNecessity.Created = DateTime.Now;
                            pNecessity.CreatedBy = 1;
                            db.PatientNecessities.Add(pNecessity);
                            db.SaveChanges();
                        }
                        pHistsory = db.PatientHistories.Find(pHistoryId);
                        pHistsory.SecondaryNecessities = txtsecondary;
                        pHistsory.MedicalNecessitiesID = necessities[0];
                        db.SaveChanges();
                        status = true;
                    }
                }
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }
}