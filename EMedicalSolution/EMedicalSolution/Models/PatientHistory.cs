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
    
    public partial class PatientHistory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PatientHistory()
        {
            this.InsuranceCardPictures = new HashSet<InsuranceCardPicture>();
            this.PatientDiseases = new HashSet<PatientDiseas>();
            this.PatientInterferingConditions = new HashSet<PatientInterferingCondition>();
            this.PatientNecessities = new HashSet<PatientNecessity>();
            this.PatientProcedures = new HashSet<PatientProcedure>();
            this.PatientReports = new HashSet<PatientReport>();
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
        public Nullable<bool> isApprovedByPhysician { get; set; }
        public Nullable<int> PhysicianID { get; set; }
        public Nullable<System.DateTime> PhysicianApprovedDate { get; set; }
        public string SpecialistRemarks { get; set; }
        public Nullable<bool> isApprovedBySpecialist { get; set; }
        public Nullable<int> SpecialistID { get; set; }
        public Nullable<System.DateTime> SpecialistApprovedDate { get; set; }
        public System.DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InsuranceCardPicture> InsuranceCardPictures { get; set; }
        public virtual InsuranceType InsuranceType { get; set; }
        public virtual MedicalNecessity MedicalNecessity { get; set; }
        public virtual Office Office { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PatientDiseas> PatientDiseases { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Status Status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PatientInterferingCondition> PatientInterferingConditions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PatientNecessity> PatientNecessities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PatientProcedure> PatientProcedures { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PatientReport> PatientReports { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PatientSymtom> PatientSymtoms { get; set; }
    }
}
