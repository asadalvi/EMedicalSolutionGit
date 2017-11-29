using EMedicalSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMedicalSolution.ViewModels
{
    public class patientIntakeViewModel
    {
        public Patient objPatient { get; set; }
        //public IList<Patient> objListPatients { get; set; }
        public IList<ProcedureType> objProcedureType { get; set; }
        public ProcedureType ChkprocedureType { get; set; }
        public IList<Symptom> objSymptom { get; set; }
        public IList<Disease> objDisease { get; set; }
        public IList<InterferingCondition> objInterferingCondition { get; set; }
        public IList<MedicalNecessity> objMedicalNecessity { get; set; }
        public IList<PatientNecessitiesVM> objMedicalNecessity1 { get; set; }
        public IList<PatientProceduresVM> PatientProceduresVM1 { get; set; }
        public IList<ProcedureTypeVM> objProcedureType1 { get; set; }
        public IList<PatientDiseaseVM> objPatientDiseaseVM { get; set; }
        public IList<InterferingConditionVM> objInterferingConditionVM1 { get; set; }
        public IList<SymptomVM> objSymptomVM1 { get; set; }
        public PatientHistory PatientHistoryVM11 { get; set; }
        public StaffName StaffNameVM { get; set; }
    }

    public class PatientProceduresVM
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string CPTcode { get; set; }
        public int HistoryID { get; set; }
        public int ProcedureTypeID { get; set; }
    }

    public class PatientNecessitiesVM
    {
        public int ID { get; set; }
        public string ICD10Code { get; set; }
        public string Description { get; set; }
        public int NecessityID { get; set; }
        public int HistoryID { get; set; }
    }
    public class ProcedureTypeVM
    {
        public int ID { get; set; }
        public string Title { get; set; }
    }

    public class InterferingConditionVM
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int HistoryID { get; set; }
        //public int IntakeFormID { get; set; }
        public int PatientConditionID { get; set; }
    }
    public class SymptomVM
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int HistoryID { get; set; }
        //public int IntakeFormID { get; set; }
        public int SymptomID { get; set; }
    }
    public class PatientDiseaseVM
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int DiseaseID { get; set; }
        public int HistoryID { get; set; }
    }
    public class StaffName
    {
        public string PhycisianName { get; set; }
        public string PhysicianRemarks { get; set; }
        public DateTime PhysicianApprovedDate { get; set; } 
        public string SpecialistName { get; set; }
        public string SpecialistRemarks { get; set; }
        public DateTime SpecialistApprovedDate { get; set; }
        public string SignatureFilePath { get; set; }
    }
    //    public class PatientHistoryVM1
    //    {
    //    public int ID { get; set; }
    //    public Nullable<int> PatientID { get; set; }
    //    public Nullable<int> InsuranceTypeID { get; set; }
    //    public Nullable<int> MedicalNecessitiesID { get; set; }
    //    public string SecondaryNecessities { get; set; }
    //    public Nullable<int> OfficeID { get; set; }
    //    public Nullable<int> StatusID { get; set; }
    //    public string PhysicianRemarks { get; set; }
    //    public Nullable<bool> isApprovedByPhysician { get; set; }
    //    public Nullable<int> PhysicianID { get; set; }
    //    public Nullable<System.DateTime> PhysicianApprovedDate { get; set; }
    //    public System.DateTime Created { get; set; }
    //    public int CreatedBy { get; set; }
    //    public Nullable<System.DateTime> Modified { get; set; }
    //    public Nullable<int> ModifiedBy { get; set; }
    //    public string SpecialistRemarks { get; set; }
    //    public Nullable<bool> isApprovedBySpecialist { get; set; }
    //    public Nullable<int> SpecialistID { get; set; }
    //    public Nullable<System.DateTime> SpecialistApprovedDate { get; set; }
    //}
}