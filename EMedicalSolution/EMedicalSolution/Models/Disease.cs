//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMedicalSolution.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Disease
    {
        public Disease()
        {
            this.PatientDiseases = new HashSet<PatientDiseas>();
        }
    
        public int ID { get; set; }
        public string Title { get; set; }
        public int DiseaseTypeID { get; set; }
        public System.DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    
        public virtual DiseaseType DiseaseType { get; set; }
        public virtual ICollection<PatientDiseas> PatientDiseases { get; set; }
    }
}
