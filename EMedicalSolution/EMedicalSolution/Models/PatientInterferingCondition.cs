//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMedicalSolution.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PatientInterferingCondition
    {
        public int ID { get; set; }
        public int HistoryID { get; set; }
        public int IntakeFormID { get; set; }
        public int PatientConditionID { get; set; }
        public System.DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    
        public virtual IntakeFormHead IntakeFormHead { get; set; }
        public virtual InterferingCondition InterferingCondition { get; set; }
        public virtual PatientHistory PatientHistory { get; set; }
    }
}
