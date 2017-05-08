using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMedicalSolution.ViewModels
{
    public class PatientHistoryVM
    {
        public int ID { get; set; }
        public string PatientName { get; set; }
        public DateTime DOB { get; set; }
        public string Title { get; set; }
        public string filename { get; set; }
        public string ClinicName { get; set; }
        public string PhysicianName { get; set; }
        public string SpecialistName { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
    }
}