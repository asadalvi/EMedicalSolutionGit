using System.Linq;
using System.Web.Mvc;
using EMedicalSolution.ViewModels;
using System.Web.UI.WebControls;

namespace EMedicalSolution.Models
{
    public class Utility : Controller
    {
        public patientIntakeViewModel patientHistoryDetail(int id)
        {
            patientIntakeViewModel intakeView = new patientIntakeViewModel();
            using (PatientMgmtEntities db = new PatientMgmtEntities())
            {
                
                //PatientMgmtEntities db = new PatientMgmtEntities();
                var intakeHistory = db.IntakeFormHeads.Where(a => a.HistoryID == id).FirstOrDefault();
                if (intakeHistory != null)
                {
                    ViewBag.supportDevice = intakeHistory.HaveSupportDevice;
                    ViewBag.currentlyPregnant = intakeHistory.isPregnant;
                }
                ViewBag.HistoryID = id;
                intakeView.PatientHistoryVM11 = db.PatientHistories.Where(a => a.ID == id).FirstOrDefault();
                intakeView.PatientProceduresVM1 = (from p in db.ProcedureTypes
                                                   join pn in db.PatientProcedures on p.ID equals pn.ProcedureTypeID
                                                   where pn.HistoryID == id
                                                   select new PatientProceduresVM
                                                   {
                                                       ID = p.ID,
                                                       Title = p.Title,
                                                       CPTcode = p.CPTcode,
                                                       ProcedureTypeID = pn.ProcedureTypeID,
                                                       HistoryID = pn.HistoryID
                                                   }).ToList();
                intakeView.objInterferingConditionVM1 = (from p in db.InterferingConditions
                                                         join pn in db.PatientInterferingConditions on p.ID equals pn.PatientConditionID
                                                         where pn.HistoryID == id
                                                         select new InterferingConditionVM
                                                         {
                                                             ID = p.ID,
                                                             Title = p.Title,
                                                             PatientConditionID = pn.PatientConditionID,
                                                             HistoryID = pn.HistoryID
                                                         }).ToList();
                intakeView.objSymptomVM1 = (from mn in db.Symptoms
                                            join pn in db.PatientSymtoms on mn.ID equals pn.SymptomID
                                            where pn.HistoryID == id
                                            select new SymptomVM
                                            {
                                                ID = mn.ID,
                                                Title = mn.Title,
                                                SymptomID = pn.SymptomID,
                                                HistoryID = pn.HistoryID
                                            }).ToList();

                intakeView.objPatientDiseaseVM = (from dt in db.Diseases
                                                  join d in db.PatientDiseases on dt.ID equals d.DiseaseID
                                                  where d.HistoryID == id
                                                  select new PatientDiseaseVM
                                                  {
                                                      ID = dt.ID,
                                                      Title = dt.Title,
                                                      HistoryID = d.HistoryID
                                                  }).ToList();
                intakeView.objMedicalNecessity1 = (from mn in db.MedicalNecessities
                                                   join pn in db.PatientNecessities on mn.ID equals pn.NecessityID
                                                   where pn.HistoryID == id
                                                   select new PatientNecessitiesVM
                                                   {
                                                       ID = mn.ID,
                                                       ICD10Code = mn.ICD10Code,
                                                       Description = mn.Description,
                                                       NecessityID = pn.NecessityID,//,
                                                       HistoryID = pn.HistoryID
                                                   }).ToList();
                intakeView.objPatient = (from p in db.Patients
                                         join ph1 in db.PatientHistories on p.ID equals ph1.PatientID
                                         where ph1.ID == id
                                         select p).FirstOrDefault();

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
                                              //PhysicianApprovedDate = (p.PhysicianApprovedDate == null) ?? Convert.ToDateTime("1900-01-01"), p.PhysicianApprovedDate,
                                              SpecialistName = ljsSpec.FirstName + " " + ljsSpec.LastName,
                                              SpecialistRemarks = p.SpecialistRemarks,
                                              SignatureFilePath = s.SignatureFilePath
                                              //SpecialistApprovedDate = p.SpecialistApprovedDate,
                                          }).FirstOrDefault();
            }
            return intakeView;
        }
    }
}