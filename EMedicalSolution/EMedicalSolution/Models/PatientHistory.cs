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
    
    public partial class PatientHistory
    {
        public PatientHistory()
        {
            this.InsuranceCardPictures = new HashSet<InsuranceCardPicture>();
            this.PatientDiseases = new HashSet<PatientDiseas>();
            this.PatientInterferingConditions = new HashSet<PatientInterferingCondition>();
            this.PatientProcedures = new HashSet<PatientProcedure>();
            this.PatientSymtoms = new HashSet<PatientSymtom>();
        }
    
        public int ID { get; set; }
        public Nullable<int> PatientID { get; set; }
        public Nullable<int> InsuranceTypeID { get; set; }
        public Nullable<int> MedicalNecessitiesID { get; set; }
        public string SecondaryNecessities { get; set; }
        public Nullable<int> OfficeID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string PhysicianRemarks { get; set; }
        public Nullable<bool> isApprove { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public System.DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    
        public virtual ICollection<InsuranceCardPicture> InsuranceCardPictures { get; set; }
        public virtual InsuranceType InsuranceType { get; set; }
        public virtual MedicalNecessity MedicalNecessity { get; set; }
        public virtual Office Office { get; set; }
        public virtual ICollection<PatientDiseas> PatientDiseases { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<PatientInterferingCondition> PatientInterferingConditions { get; set; }
        public virtual ICollection<PatientProcedure> PatientProcedures { get; set; }
        public virtual ICollection<PatientSymtom> PatientSymtoms { get; set; }
    }
}
