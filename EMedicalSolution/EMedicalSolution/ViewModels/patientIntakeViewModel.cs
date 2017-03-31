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
        public IList<Symptom> objSymptom { get; set; }
        public IList<Disease> objDisease { get; set; }
        public IList<MedicalNecessity> objMedicalNecessity { get; set; }

    }
}