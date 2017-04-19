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
        public IList<DiseaseTypeVM> objDiseaseTypeVM1 { get; set; }
        public IList<InterferingConditionVM> objInterferingConditionVM1 { get; set; }
        public IList<SymptomVM> objSymptomVM1 { get; set; }
    }    

    public class PatientProceduresVM
    {
        public int ID { get; set; }
        public string Title { get; set; }
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
    public class DiseaseTypeVM
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int DiseaseTypeID { get; set; }
        public int HistoryID { get; set; }
    }
}