﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PatientMgmtEntities : DbContext
    {
        public PatientMgmtEntities()
            : base("name=PatientMgmtEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Disease> Diseases { get; set; }
        public virtual DbSet<DiseasesProcedureMpiapng> DiseasesProcedureMpiapngs { get; set; }
        public virtual DbSet<DiseaseType> DiseaseTypes { get; set; }
        public virtual DbSet<InsuranceCardPicture> InsuranceCardPictures { get; set; }
        public virtual DbSet<InsuranceType> InsuranceTypes { get; set; }
        public virtual DbSet<IntakeFormHead> IntakeFormHeads { get; set; }
        public virtual DbSet<InterferingCondition> InterferingConditions { get; set; }
        public virtual DbSet<MedicalNecessity> MedicalNecessities { get; set; }
        public virtual DbSet<NecessitiesProcedureMapping> NecessitiesProcedureMappings { get; set; }
        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<PatientDiseas> PatientDiseases { get; set; }
        public virtual DbSet<PatientHistory> PatientHistories { get; set; }
        public virtual DbSet<PatientInterferingCondition> PatientInterferingConditions { get; set; }
        public virtual DbSet<PatientNecessity> PatientNecessities { get; set; }
        public virtual DbSet<PatientProcedure> PatientProcedures { get; set; }
        public virtual DbSet<PatientReport> PatientReports { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<PatientSymtom> PatientSymtoms { get; set; }
        public virtual DbSet<ProcedureType> ProcedureTypes { get; set; }
        public virtual DbSet<ProgressNote> ProgressNotes { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Symptom> Symptoms { get; set; }
        public virtual DbSet<SymptomsProcedureMapping> SymptomsProcedureMappings { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
