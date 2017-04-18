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
}