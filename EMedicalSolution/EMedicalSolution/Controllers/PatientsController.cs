﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMedicalSolution.Models;
using EMedicalSolution.ViewModels;
using EMedicalSolution.App_Start;
using Newtonsoft.Json;
using System.IO;
using HiQPdf;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net.Mime;

namespace EMedicalSolution.Controllers
{
    [SessionTimeout]
    public class PatientsController : Controller
    {
        //
        // GET: /Home/
        PatientMgmtEntities db = new PatientMgmtEntities();
        Utility oUtility = new Utility();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PatientsList()
        {
            return View();
        }
        public ActionResult Wizard(int? id = 0)
        {
            patientIntakeViewModel intakeView = new patientIntakeViewModel();

            var intakeHistory = db.IntakeFormHeads.Where(a => a.HistoryID == id).FirstOrDefault();
            if (intakeHistory != null)
            {
                ViewBag.supportDevice = intakeHistory.HaveSupportDevice;
                ViewBag.currentlyPregnant = intakeHistory.isPregnant;
            }
            ViewBag.HistoryID = id;
            intakeView.StaffNameVM = (from p in db.PatientHistories
                                      join pn in db.Users on p.PhysicianID equals pn.ID
                                      join s in db.Staffs on pn.StaffID equals s.ID
                                      join us in db.Users on p.SpecialistID equals us.ID into leftuSpec
                                      from ljuSpec in (from us in leftuSpec select us).DefaultIfEmpty()
                                      join ss in db.Staffs on ljuSpec.StaffID equals ss.ID into leftsSpec
                                      from ljsSpec in (from ss in leftsSpec select ss).DefaultIfEmpty()
                                      where p.ID == id
                                      select new StaffName
                                      {
                                          PhycisianName = s.FirstName + " " + s.LastName,
                                          PhysicianRemarks = p.PhysicianRemarks,
                                          SpecialistName = ljsSpec.FirstName + " " + ljsSpec.LastName,
                                          SpecialistRemarks = p.SpecialistRemarks
                                      }).FirstOrDefault();

            intakeView.PatientHistoryVM11 = db.PatientHistories.Where(a => a.ID == id).FirstOrDefault();
            intakeView.PatientProceduresVM1 = (from p in db.ProcedureTypes
                                               join pn in db.PatientProcedures on p.ID equals pn.ProcedureTypeID into leftJ
                                               from lj in (from pn in leftJ
                                                           where pn.HistoryID == id
                                                           select pn).DefaultIfEmpty()
                                               select new PatientProceduresVM
                                               {
                                                   ID = p.ID,
                                                   Title = p.Title,
                                                   ProcedureTypeID = (lj.ProcedureTypeID != null) ? lj.ProcedureTypeID : 0,//,
                                                   HistoryID = (lj.HistoryID != null) ? lj.HistoryID : 0
                                               }).ToList();
            intakeView.objInterferingConditionVM1 = (from p in db.InterferingConditions
                                                     join pn in db.PatientInterferingConditions on p.ID equals pn.PatientConditionID into leftJ
                                                     from lj in (from pn in leftJ
                                                                 where pn.HistoryID == id
                                                                 select pn).DefaultIfEmpty()
                                                     select new InterferingConditionVM
                                                     {
                                                         ID = p.ID,
                                                         Title = p.Title,
                                                         PatientConditionID = (lj.PatientConditionID != null) ? lj.PatientConditionID : 0,//,
                                                         HistoryID = (lj.HistoryID != null) ? lj.HistoryID : 0
                                                     }).ToList();
            intakeView.objSymptomVM1 = (from mn in db.Symptoms
                                        join pn in db.PatientSymtoms on mn.ID equals pn.SymptomID into leftJ
                                        from lj in (from pn in leftJ
                                                    where pn.HistoryID == id
                                                    select pn).DefaultIfEmpty()
                                        select new SymptomVM
                                        {
                                            ID = mn.ID,
                                            Title = mn.Title,
                                            SymptomID = (lj.SymptomID != null) ? lj.SymptomID : 0,//,
                                            HistoryID = (lj.HistoryID != null) ? lj.HistoryID : 0
                                        }).ToList();

            intakeView.objPatientDiseaseVM = (from dt in db.Diseases
                                              join d in db.PatientDiseases on dt.ID equals d.DiseaseID into leftJ
                                              from lj in (from d in leftJ
                                                          where d.HistoryID == id  // what should be here
                                                          select d).DefaultIfEmpty()
                                              select new PatientDiseaseVM
                                              {
                                                  ID = dt.ID,
                                                  Title = dt.Title,
                                                  HistoryID = (lj.HistoryID != null) ? lj.HistoryID : 0,//,
                                              }).ToList();
            intakeView.objMedicalNecessity1 = (from mn in db.MedicalNecessities
                                               join pn in db.PatientNecessities on mn.ID equals pn.NecessityID into leftJ
                                               from lj in (from pn in leftJ
                                                           where pn.HistoryID == id
                                                           select pn).DefaultIfEmpty()
                                               select new PatientNecessitiesVM
                                               {
                                                   ID = mn.ID,
                                                   ICD10Code = mn.ICD10Code,
                                                   Description = mn.Description,
                                                   NecessityID = (lj.NecessityID != null) ? lj.NecessityID : 0,//,
                                                   HistoryID = (lj.HistoryID != null) ? lj.HistoryID : 0
                                               }).ToList();
            intakeView.objPatient = (from p in db.Patients
                                     join ph1 in db.PatientHistories on p.ID equals ph1.PatientID
                                     where ph1.ID == id
                                     select p).FirstOrDefault();
            //}
            return View(intakeView);
        }

        //[HttpPost]
        //public ActionResult Wizard(int id) //id = Patient History ID
        //{
        //    patientIntakeViewModel intakeView = new patientIntakeViewModel();
        //    //PatientMgmtEntities db = new PatientMgmtEntities();
        //    intakeView.objProcedureType = db.ProcedureTypes.ToList();
        //    intakeView.objInterferingCondition = db.InterferingConditions.ToList();
        //    intakeView.objSymptom = (from s in db.Symptoms
        //                             join ps in db.PatientSymtoms on s.ID equals ps.SymptomID into leftJ
        //                             from lj in leftJ.DefaultIfEmpty()
        //                             where lj.HistoryID == id
        //                             select s).ToList();

        //    intakeView.objDisease = db.Diseases.ToList();
        //    ViewBag.PatientNecessities = (from mn in db.MedicalNecessities
        //                                  join pn in db.PatientNecessities on mn.ID equals pn.NecessityID into leftJ
        //                                  from lj in leftJ.DefaultIfEmpty()
        //                                  where lj.HistoryID == id
        //                                  select new { mn, lj.HistoryID }).ToList();
        //    return View(intakeView);
        //}
        public ActionResult GetPatients()
        {
            List<Patient> patients;
                if (Session["role"].ToString() == "1")
                {
                    patients = db.Patients.OrderBy(a => a.FirstName).ToList();
                }
                else
                {
                    int officeID = Convert.ToInt32(Session["officeID"].ToString());
                    patients = db.Patients.Where(p => p.OfficeID == officeID).OrderBy(a => a.FirstName).ToList();
                }

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

        [HttpPost]
        public ActionResult getHistoryList(int id)
        {
            ViewBag.patientID = id;
                var pHistory = (from p in db.Patients
                                join ph in db.PatientHistories on p.ID equals ph.PatientID
                                join i in db.InsuranceTypes on ph.InsuranceTypeID equals i.ID
                                join pc in db.InsuranceCardPictures on ph.ID equals pc.HistoryID
                                join off in db.Offices on p.OfficeID equals off.ID
                                join status in db.Statuses on ph.StatusID equals status.ID into leftStatus
                                from lfStatus in (from st in leftStatus select st).DefaultIfEmpty()
                                join up in db.Users on ph.PhysicianID equals up.ID into leftuPhy
                                from ljuPhy in (from us in leftuPhy select us).DefaultIfEmpty()
                                join s in db.Staffs on ljuPhy.StaffID equals s.ID into leftsPhy
                                from ljsPhy in (from ss in leftsPhy select ss).DefaultIfEmpty()
                                join us in db.Users on ph.SpecialistID equals us.ID into leftuSpec
                                from ljuSpec in (from us in leftuSpec select us).DefaultIfEmpty()
                                join ss in db.Staffs on ljuSpec.StaffID equals ss.ID into leftsSpec
                                from ljsSpec in (from ss in leftsSpec select ss).DefaultIfEmpty()
                                where (id>0)? ph.PatientID == id : 1 == 1
                                select new PatientHistoryVM
                                {
                                    ID = ph.ID,
                                    PatientName = p.FirstName + " " + p.LastName,
                                    DOB = p.DOB,
                                    Title = i.Title,
                                    MemberID = pc.MemberID,
                                    Status = lfStatus.Title,
                                    filename = pc.FilePath,
                                    ClinicName = off.Title,
                                    PhysicianName = (ljsPhy.FirstName == string.Empty || ljsPhy.FirstName == null) ? "Approval Required" : "Dr. "+ ljsPhy.FirstName + " " + ljsPhy.LastName,
                                    SpecialistName = (ljsSpec.FirstName == string.Empty || ljsSpec.FirstName == null) ? "Interpretation Required" : "Dr. " + ljsSpec.FirstName + " " + ljsSpec.LastName,
                                    Created = ph.Created,
                                });


                return Json(new
                {
                    data = pHistory.Select(p => new
                    {
                        p.ID,
                        p.PatientName,
                        p.DOB,
                        p.Title,
                        p.MemberID,
                        p.ClinicName, 
                        p.Status,
                        p.PhysicianName,
                        p.SpecialistName,
                        p.Created,
                        p.filename
                    }
                )
                }, JsonRequestBehavior.AllowGet);

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
                            v.ModifiedBy = Convert.ToInt32(Session["userID"].ToString());
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        //save
                        patient.OfficeID = Convert.ToInt32(Session["officeID"].ToString());
                        patient.Created = DateTime.Now;
                        patient.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
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
        public ActionResult procedureTypes(string[] procedureType, int pHistoryId)
        {
            bool status = false;
            PatientProcedure pProcedure;
            if (ModelState.IsValid)
            {
                //string[] myType = procedureType.Split(',');
                int[] myType = Array.ConvertAll(procedureType, s => int.Parse(s));
                using (PatientMgmtEntities db = new PatientMgmtEntities())
                {
                    db.PatientProcedures.RemoveRange(db.PatientProcedures.Where(p => p.HistoryID == pHistoryId));
                    db.SaveChanges();

                    pProcedure = new PatientProcedure();
                    if (pHistoryId > 0)
                    {
                        pProcedure.HistoryID = pHistoryId;
                        for (int i = 0; i < myType.Length; i++)
                        {
                            pProcedure.ProcedureTypeID = Convert.ToInt32(myType[i]);
                            pProcedure.Created = DateTime.Now;
                            pProcedure.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
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
            //PatientMgmtEntities db = new PatientMgmtEntities();

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
                    db.PatientSymtoms.RemoveRange(db.PatientSymtoms.Where(s => s.HistoryID == pHistoryId));
                    db.PatientDiseases.RemoveRange(db.PatientDiseases.Where(s => s.HistoryID == pHistoryId));
                    db.PatientInterferingConditions.RemoveRange(db.PatientInterferingConditions.Where(s => s.HistoryID == pHistoryId));
                    db.SaveChanges();
                    intakeFormHead = new IntakeFormHead();
                    intakeFormHead = db.IntakeFormHeads.Where(r => r.HistoryID == pHistoryId).FirstOrDefault();
                    if (intakeFormHead != null)
                    {
                        intakeFormHead.HaveSupportDevice = supportDevice;
                        intakeFormHead.isPregnant = pregnant;
                        intakeFormHead.Modified = DateTime.Now;
                        intakeFormHead.ModifiedBy = Convert.ToInt32(Session["userID"].ToString());
                    }
                    else
                    {
                        intakeFormHead = new IntakeFormHead();
                        intakeFormHead.HistoryID = pHistoryId;
                        intakeFormHead.isPregnant = pregnant;
                        intakeFormHead.HaveSupportDevice = supportDevice;
                        intakeFormHead.Date = DateTime.Today;
                        intakeFormHead.Created = DateTime.Now;
                        intakeFormHead.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
                        db.IntakeFormHeads.Add(intakeFormHead);
                    }
                    db.SaveChanges();
                    int intakeId = intakeFormHead.ID;
                    //intakeFormHead.HaveSupportDevice = 
                    if (symptom != null && symptom.Length > 0 && intakeId > 0)
                    {
                        patientSymtom = new PatientSymtom();
                        symptoms = Array.ConvertAll(symptom, s => int.Parse(s));
                        for (int i = 0; i < symptom.Length; i++)
                        {
                            patientSymtom.IntakeFormID = intakeId;
                            patientSymtom.HistoryID = pHistoryId;
                            patientSymtom.SymptomID = Convert.ToInt32(symptom[i]);
                            patientSymtom.Created = DateTime.Now;
                            patientSymtom.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
                            db.PatientSymtoms.Add(patientSymtom);
                            db.SaveChanges();
                        }
                    }
                    if (disease != null && disease.Length > 0 && intakeId > 0)
                    {
                        patientDiseas = new PatientDiseas();
                        diseases = Array.ConvertAll(disease, s => int.Parse(s));
                        for (int i = 0; i < diseases.Length; i++)
                        {
                            patientDiseas.IntakeFormID = intakeId;
                            patientDiseas.HistoryID = pHistoryId;
                            patientDiseas.DiseaseID = Convert.ToInt32(diseases[i]);
                            patientDiseas.Created = DateTime.Now;
                            patientDiseas.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
                            db.PatientDiseases.Add(patientDiseas);
                            db.SaveChanges();
                        }
                    }
                    if (condition != null && condition.Length > 0 && intakeId > 0)
                    {
                        for (int i = 0; i < condition.Length; i++)
                        {
                            patientInterferingCondition = new PatientInterferingCondition();
                            patientInterferingCondition.IntakeFormID = intakeId;
                            patientInterferingCondition.HistoryID = pHistoryId;
                            patientInterferingCondition.PatientConditionID = Convert.ToInt32(condition[i]);
                            patientInterferingCondition.Created = DateTime.Now;
                            patientInterferingCondition.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
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
            string errorMsg = "";
            try {
                using (PatientMgmtEntities db = new PatientMgmtEntities())
                {
                    var v = db.Patients.Where(a => a.ID == id).FirstOrDefault();
                    if (v != null)
                    {
                        var chk = db.PatientHistories.Where(ph => ph.PatientID == v.ID);
                        if(chk != null)
                        {
                            errorMsg = "Patient cannot be deleted as there are some other entries against this patient";
                        }
                        else
                        {
                            db.Patients.Remove(v);
                            db.SaveChanges();
                            status = true;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                { 
                    status = false; // The {...} statement conflicted with the {...} constraint {...}
                    errorMsg = "Patient cannot be deleted as there are some other entries against this patient";
                    }
            }
            return new JsonResult { Data = new { status = status, errorMsg = errorMsg } };
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
        public ActionResult SavePatientInsuranceType(HttpPostedFileBase filePath, string insuranceTitle,string insuranceMemberId, int pId, int typeid)
        {
            bool status = false;
            PatientHistory ph = new PatientHistory();
            InsuranceCardPicture insureCard = new InsuranceCardPicture();
            ph.PatientID = pId; //Getting inserted ID

            ph.InsuranceTypeID = typeid;
            ph.StatusID = 1; //Pending
            ph.Created = DateTime.Now;
            ph.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
            string firstName = "";
            string lastName = "";
            insureCard.PatientID = pId;
            insureCard.Title = insuranceTitle;
            insureCard.MemberID = insuranceMemberId;
            insureCard.Created = DateTime.Now;
            insureCard.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
            using (PatientMgmtEntities db = new PatientMgmtEntities())
            {
                db.PatientHistories.Add(ph);
                db.SaveChanges();
                int hId = ph.ID;
                var v = (from p in db.Patients
                         join ph1 in db.PatientHistories on p.ID equals ph1.PatientID
                         where ph1.PatientID == pId
                         select new { firstName = p.FirstName, lastName = p.LastName }).FirstOrDefault();
                firstName = v.firstName;
                lastName = v.lastName;
                if (filePath != null)
                {
                    var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg", ".rar", ".zip", ".pdf", ".doc", ".docx", ".xls", ".xlsx" }; //Allowe Extensions
                    if (filePath != null && filePath.ContentLength > 0)
                    {
                        //oTests[i].ReportPath = files[i].ToString(); //getting complete url  
                        var fileName = Path.GetFileName(filePath.FileName); //getting only file name(ex-ganesh.jpg)  
                        var ext = Path.GetExtension(filePath.FileName); //getting the extension(ex-.jpg)  

                        if (allowedExtensions.Contains(ext)) //check what type of extension  
                        {
                            string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                            int id = db.PatientReports.Max(p => (int?)p.ID) ?? 0 + 1;
                            string myfile = "Card_" + id + ext; //appending the name with id  
                                                                // store the file inside ~/project folder(Img)  
                            var path = Path.Combine(Server.MapPath("~/Files/Cards"), myfile);
                            filePath.SaveAs(path);
                            insureCard.FilePath = path;
                            insureCard.HistoryID = hId;
                            db.InsuranceCardPictures.Add(insureCard);
                            db.SaveChanges();
                        }
                    }
                }
                status = true;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
            //return new JsonResult { Data = new { firstName = firstName, lastName= firstName,title= insuranceTitle,pId=pId,createdDate = DateTime.Now } };
        }

        [HttpGet]
        public ActionResult GetReports(int pHistoryId)
        {
            //PatientMgmtEntities db = new PatientMgmtEntities();
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
        //download insurance card

        public FileResult DownloadInsuranceCard(string fileName)
        {
            var filePath = Path.Combine(Server.MapPath("/Files/Cards/"), fileName);
            return File(filePath, MimeMapping.GetMimeMapping(filePath), fileName);
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
                                PatientReports.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
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
                    db.PatientNecessities.RemoveRange(db.PatientNecessities.Where(n => n.HistoryID == pHistoryId));
                    db.SaveChanges();

                    pNecessity = new PatientNecessity();
                    pHistsory = new PatientHistory();
                    if (pHistoryId > 0)
                    {
                        pNecessity.HistoryID = pHistoryId;

                        for (int i = 0; i < necessities.Length; i++)
                        {
                            pNecessity.NecessityID = Convert.ToInt32(necessities[i]);
                            pNecessity.Created = DateTime.Now;
                            pNecessity.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
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
        //view history of a patient

        //[HttpPost]
        public ActionResult ViewHistory(int id)
        {
            ViewBag.pHistoryId = id;
            bool status = false;
            if (ModelState.IsValid)
            {
                status = true;
            }
            return View();
        }

        

        //order for report
        public ActionResult orderReport(int id)
        {
            patientIntakeViewModel intakeView = oUtility.patientHistoryDetail(id);
            return View(intakeView);
        }
        //order bill for report
        public ActionResult orderBill(int id)
        {
            patientIntakeViewModel intakeView = oUtility.patientHistoryDetail(id);
            return View(intakeView);
        }
        //report rendering
        public string RenderViewAsString(string viewName, int id)
        {
            //place this code in one place
            patientIntakeViewModel intakeView = oUtility.patientHistoryDetail(id);
            // create a string writer to receive the HTML code
            StringWriter stringWriter = new StringWriter();

            // get the view to render
            ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext, viewName, null);
            // create a context to render a view based on a model
            ViewContext viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    new ViewDataDictionary(intakeView),
                    new TempDataDictionary(),
                    stringWriter
                    );

            // render the view to a HTML code
            viewResult.View.Render(viewContext, stringWriter);

            // return the HTML code
            return stringWriter.ToString();
        }

        [HttpPost]
        public ActionResult ConvertPatientToPdf()
        {
            int id = Convert.ToInt32(Request["orderFormHistoryId"].ToString());
            string type = Request["orderFormType"].ToString();
            // get the About view HTML code
            string htmlToConvert;
            if (type == "orderReport")
            {
                htmlToConvert = RenderViewAsString("orderReport", id);
            }
            else if (type == "orderBill")
            { htmlToConvert = RenderViewAsString("orderBill", id); }
            else
            {
                return View();
            }
            // the base URL to resolve relative images and css
            String thisPageUrl = this.ControllerContext.HttpContext.Request.Url.AbsoluteUri;
            String baseUrl = thisPageUrl.Substring(0, thisPageUrl.Length - "Patients/ConvertPatientToPdf".Length);

            // instantiate the HiQPdf HTML to PDF converter
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

            // render the HTML code as PDF in memory
            byte[] pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlToConvert, baseUrl);

            // send the PDF file to browser
            FileResult fileResult = new FileContentResult(pdfBuffer, "application/pdf");
            fileResult.FileDownloadName = "orderForm.pdf";

            return fileResult;
        }
        //physician password,physician signature
        public ActionResult physicianSignature()
        {
            //bool status = false;
            if (ModelState.IsValid)
            {
                //status = true;
            }
            //status = false;
            return View();
        }
        //Approved physician password,Approved physician signature
        [HttpPost]
        public ActionResult physicianSignature(string physiciasnSignature, int pHistoryId)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (PatientMgmtEntities db = new PatientMgmtEntities())
                {
                    var v = db.Staffs.Where(a => a.SignaturePassword == physiciasnSignature).FirstOrDefault();
                    if (v != null)
                    {
                        status = true;
                    }
                }
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        //save physician remarks
        [HttpPost]
        public ActionResult PhysicianApproval(string physicianRemarks, int pHistoryId)
        {
            bool status = false;
            string namePhyscian = "";
            PatientHistory pHistory;
            if (ModelState.IsValid)
            {
                //string[] myType = procedureType.Split(',');
                using (PatientMgmtEntities db = new PatientMgmtEntities())
                {
                    // pHistory = new PatientHistory();
                    if (pHistoryId > 0)
                    {
                        pHistory = (from d in db.PatientHistories where d.ID == pHistoryId select d).FirstOrDefault();

                        if (pHistory != null)
                        {
                            pHistory.PhysicianRemarks = physicianRemarks;
                            pHistory.SpecialistApprovedDate = DateTime.Now;
                            pHistory.isApprovedByPhysician = true;
                            pHistory.PhysicianID = Convert.ToInt32(Session["userID"].ToString());
                            pHistory.StatusID = 5; //Approved by Physician
                            db.SaveChanges();
                        }
                        string na = "";
                        string nb = "";
                        var physicianName = (from p in db.PatientHistories
                                             join pn in db.Users on p.PhysicianID equals pn.ID into leftJ
                                             from lj in (from pn in leftJ select pn).DefaultIfEmpty()
                                             join s in db.Staffs on lj.StaffID equals s.ID
                                             where p.ID == pHistoryId
                                             select new { na = s.FirstName, nb = s.LastName }).FirstOrDefault();
                        namePhyscian = physicianName.na + " " + physicianName.nb;
                        status = true;
                    }
                }
            }
            return new JsonResult { Data = new { status = status, namePhyscian = namePhyscian, physicianRemarks = physicianRemarks } };
            // return Json(status, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult resultInterpretation(string specialistRemarks, int pHistoryId)
        {
            bool status = false;
            PatientHistory pHistory;
            if (ModelState.IsValid)
            {
                //string[] myType = procedureType.Split(',');
                using (PatientMgmtEntities db = new PatientMgmtEntities())
                {
                    // pHistory = new PatientHistory();
                    if (pHistoryId > 0)
                    {
                        pHistory = (from d in db.PatientHistories where d.ID == pHistoryId select d).FirstOrDefault();

                        if (pHistory != null)
                        {
                            pHistory.SpecialistRemarks = specialistRemarks;
                            pHistory.SpecialistApprovedDate = DateTime.Now;
                            pHistory.isApprovedBySpecialist = true;
                            pHistory.SpecialistID = Convert.ToInt32(Session["userID"].ToString());
                            pHistory.StatusID = 6; //Approved by Specialist
                            db.SaveChanges();
                        }
                        status = true;
                    }
                }
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Finish(int pHistoryId)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                //string[] myType = procedureType.Split(',');
                using (PatientMgmtEntities db = new PatientMgmtEntities())
                {                   
                    if (pHistoryId > 0)
                    {

                        string htmlToConvert = RenderViewAsString("orderBill", pHistoryId);

                        // the base URL to resolve relative images and css
                        String thisPageUrl = this.ControllerContext.HttpContext.Request.Url.AbsoluteUri;
                        String baseUrl = thisPageUrl.Substring(0, thisPageUrl.Length - "Patients/ConvertPatientToPdf".Length);

                        // instantiate the HiQPdf HTML to PDF converter
                        HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

                        htmlToPdfConverter.Document.PageSize = PdfPageSize.A4;
                        htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;

                        // set PDF page margins 
                        htmlToPdfConverter.Document.Margins = new PdfMargins(0, 0, 0, 0);

                        // set a wait time before starting the conversion 
                        htmlToPdfConverter.WaitBeforeConvert = int.Parse("2"); //seconds

                        // render the HTML code as PDF in memory
                        byte[] pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlToConvert, baseUrl);

                        Stream stream = new MemoryStream(pdfBuffer);
                        ContentType ct = new ContentType(MediaTypeNames.Application.Pdf);
                        var attachmnt = new Attachment(stream, ct);
                        attachmnt.ContentType.MediaType = MediaTypeNames.Application.Pdf;
                        attachmnt.Name = "superbill.pdf";
                        MailMessage mail = new MailMessage();
                        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                        mail.From = new MailAddress("testmailnaveed@gmail.com");
                        mail.To.Add("asad.awan69@gmail.com");
                        mail.Subject = "A Supper Bill generated";
                        mail.Body = "Supper bill of a paitient has been attached herewith:";

                        mail.Attachments.Add(attachmnt);
                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new System.Net.NetworkCredential("testmailnaveed@gmail.com", "naveed1234");
                        SmtpServer.EnableSsl = true;
                        SmtpServer.Send(mail);

                    }
                    status = true;
                    }
                }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        //history views
        [HttpGet]
        public ActionResult HistoryList(int id)
        {
            ViewBag.patientID = id;
            return View();
        }

        //patient full information
        public ActionResult patientInfo(int id)
        {
            patientIntakeViewModel intakeView = oUtility.patientHistoryDetail(id);
            return View(intakeView);
        }
        //excel
        public ActionResult ExportToExcel(int id)
        {
            var products = new System.Data.DataTable("test");
            products.Columns.Add("col1", typeof(int));
            products.Columns.Add("col2", typeof(string));

            products.Rows.Add(1, "product 1");
            products.Rows.Add(2, "product 2");
            products.Rows.Add(3, "product 3");
            products.Rows.Add(4, "product 4");
            products.Rows.Add(5, "product 5");
            products.Rows.Add(6, "product 6");
            products.Rows.Add(7, "product 7");


            var grid = new GridView();
            grid.DataSource = products;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View();
        }

        //progress notes
        public FileResult DownloadProgrssNoteReport(string fileName)
        {
            var filePath = Path.Combine(Server.MapPath("/Files/Notes/"), fileName);
            return File(filePath, MimeMapping.GetMimeMapping(filePath), fileName);
        }

        [HttpPost]
        public ActionResult uploadProgressNote(HttpPostedFileBase[] notePath, string[] notetitle, int pHistoryId)
        {
            bool status = false;
            if (notePath != null)
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg", ".rar", ".zip", ".pdf", ".doc", ".docx", ".xls", ".xlsx" }; //Allowe Extensions
                int i = -1;
                foreach (var file in notePath)
                {
                    i++;
                    if (file != null && file.ContentLength > 0)
                    {
                        //oTests[i].ReportPath = files[i].ToString(); //getting complete url  
                        var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                        var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  

                        if (allowedExtensions.Contains(ext)) //check what type of extension  
                        {
                            ProgressNote pNote = new ProgressNote();
                            string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                            using (PatientMgmtEntities db = new PatientMgmtEntities())
                            {
                                int id = db.PatientReports.Max(p => (int?)p.ID) ?? 0 + 1;
                                string myfile = "Notes_" + id + ext; //appending the name with id  
                                                                      // store the file inside ~/project folder(Img)  
                                var path = Path.Combine(Server.MapPath("~/Files/Notes"), myfile);
                                file.SaveAs(path);
                                pNote.FilePath = path;
                                pNote.HistoryID = pHistoryId;
                                pNote.Title = notetitle[i];
                                pNote.Created = DateTime.Now;
                                pNote.CreatedBy = Convert.ToInt32(Session["userID"].ToString());
                                db.ProgressNotes.Add(pNote);
                                db.SaveChanges();
                            }
                        }

                    } //checking file is not empty 
                } // for loop end
                status = true;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetProgressNote(int pHistoryId)
        {
            //PatientMgmtEntities db = new PatientMgmtEntities();
            {
                List<ProgressNote> reports = db.ProgressNotes.Where(h => h.HistoryID == pHistoryId).OrderByDescending(a => a.Created).ToList();
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
        //get necessities against procedures id
        [HttpPost]
        public ActionResult GetNecessityCode(int procId)
        {
            patientIntakeViewModel intakeView = new patientIntakeViewModel();          
            var pAll = from ns in db.MedicalNecessities
                                              where ns.NecessitiesProcedureMappings.Any(c => c.ProcedureID == procId)
                                              select ns.ID;

         return Json(new
                {
                    data = pAll
         }, JsonRequestBehavior.AllowGet);
        }

        //get necessities against procedures id
        [HttpPost]
        public ActionResult GetSymptomsDiseasesProType(int pHistoryId)
        {
            var pids = from p in db.ProcedureTypes
                       join pn in db.PatientProcedures on p.ID equals pn.ProcedureTypeID
                       where pn.HistoryID == pHistoryId
                       select pn.ProcedureTypeID;
            //int[] prodids;
            //foreach (var app in pids)
            //{
            //    prodids[app] = pids;
            //}

            // int domain = "1,2";
            //int[] prodids = pids.Split(',');

            //var records = context.Records.Where(r => domains.Contains(r.Domain)).ToList();


            //var Allsymptom = db.SymptomsProcedureMappings.Where(r => pids.Contains(r.SymptomID)).ToList();


            var Allsymptom = (from ns in db.SymptomsProcedureMappings.Where(c => pids.Contains(c.ProcedureID))
                             select ns.SymptomID).Distinct();
            var Alldiseases = (from ns in db.DiseasesProcedureMpiapngs
                              .Where(c => pids.Contains(c.ProcedureID))
                               select ns.DiseaseID).Distinct();
            return new JsonResult { Data = new { Allsymptom = Allsymptom, Alldiseases = Alldiseases } };
            //return Json(new
            //{
            //    data = pids
            //}, JsonRequestBehavior.AllowGet);
        }
    }
}