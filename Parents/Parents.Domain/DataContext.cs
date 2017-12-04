namespace Parents.Domain
{
    using Parents.Domain.AppCore;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class DataContext : DbContext
    {

        public DataContext() : base("DefaultConnection")
        {

        }

        public DbSet<ChildManagement> ChildManagements { get; set; }

        public DbSet<Children> Children { get; set; }

        public DbSet<ParentalManagement.Helpers.ChildSupportVisit> ChildSupportVisits { get; set; }

        public DbSet<ParentalManagement.Helpers.ChildSupportVisitType> ChildSupportVisitTypes { get; set; }

        public DbSet<Parent> Parents { get; set; }

        public DbSet<ParentalManagement.Helpers.ParentalGuardTerm> ParentalGuardTerms { get; set; }

        public DbSet<ParentalManagement.Helpers.ParentalType> ParentalTypes { get; set; }

        public DbSet<ParentalManagement.Helpers.MatrimonialState> MatrimonialStates { get; set; }

        public DbSet<HealthManagement.Categories.BloodInformation> BloodInformations { get; set; }

        public DbSet<ActivitiesManagement.Activity> Activities { get; set; }

        public DbSet<ActivitiesManagement.Helpers.ActivityFamily> ActivityFamilies { get; set; }

        public DbSet<ActivitiesManagement.Helpers.ActivityInstitutionType> ActivityInstitutionTypes { get; set; }

        public DbSet<ActivitiesManagement.Helpers.ActivityPeriodicity> ActivityPeriodicities { get; set; }

        public DbSet<ActivitiesManagement.Helpers.ActivityType> ActivityTypes { get; set; }

        public DbSet<DomesticManagement.ItemsCategory> ItemsCategories { get; set; }

        public DbSet<DomesticManagement.ItemToBuy> ItemToBuys { get; set; }

        public DbSet<HealthManagement.Alergy> Alergies { get; set; }

        public DbSet<HealthManagement.Categories.AlergyType> AlergyTypes { get; set; }

        public DbSet<HealthManagement.Disease> Diseases { get; set; }

        public DbSet<HealthManagement.Categories.DiseaseFamily> DiseaseFamilies { get; set; }

        public DbSet<HealthManagement.Categories.DiseaseType> DiseaseTypes { get; set; }

        public DbSet<HealthManagement.Categories.Medicine> Medicines { get; set; }

        public DbSet<HealthManagement.Categories.MedicineDosage> MedicineDosages { get; set; }

        public DbSet<HealthManagement.Categories.MedicinePharmaceuticalForm> MedicinePharmaceuticalForms { get; set; }

        public DbSet<HealthManagement.PhysicalCharacteristic> PhysicalCharacteristics { get; set; }

        public DbSet<HealthManagement.Categories.HumanBodyAreas> HumanBodyAreas { get; set; }

        public DbSet<HealthManagement.Categories.PhysicalCharacteristicType> PhysicalCharacteristicTypes { get; set; }

        public DbSet<HealthManagement.Urgency> Urgencies { get; set; }

        public DbSet<HealthManagement.Categories.MedicalInstitutions> MedicalInstitutions { get; set; }

        public DbSet<HealthManagement.Categories.UrgencyCategory> UrgencyCategories { get; set; }

        public DbSet<HealthManagement.Categories.UrgencySeverity> UrgencySeverities { get; set; }

        public DbSet<ParentalManagement.Helpers.ChildSupportPayment> ChildSupportPayments { get; set; }

        public DbSet<SchoolManagement.Exam> Exams { get; set; }

        public DbSet<SchoolManagement.Helpers.Discipline> Disciplines { get; set; }

        public DbSet<SchoolManagement.Helpers.ExamFamily> ExamFamilies { get; set; }

        public DbSet<SchoolManagement.School> Schools { get; set; }

        public DbSet<SchoolManagement.ParentsMeeting> ParentsMeetings { get; set; }

        public DbSet<SchoolManagement.Helpers.AcademicYear> AcademicYears { get; set; }

        public DbSet<TasksManagement.TaskFunction> Tasks { get; set; }

        public DbSet<TasksManagement.Helpers.TaskFamily> TaskFamilies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<AppCore.Helpers.ManagementType> ManagementTypes { get; set; }

        public System.Data.Entity.DbSet<Parents.Domain.HealthManagement.Categories.Treatment> Treatments { get; set; }
    }
}
