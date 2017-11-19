namespace Parents.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicYears",
                c => new
                    {
                        AcademicYearId = c.Int(nullable: false, identity: true),
                        AcademicYearReference = c.String(),
                        AcademicYearClassDirector = c.String(),
                        AcademicYearGPA = c.String(),
                        AcademicYearAchievment = c.Boolean(nullable: false),
                        SchoolId = c.Int(),
                    })
                .PrimaryKey(t => t.AcademicYearId)
                .ForeignKey("dbo.Schools", t => t.SchoolId)
                .Index(t => t.SchoolId);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        SchoolId = c.Int(nullable: false, identity: true),
                        SchoolName = c.String(),
                        SchoolAddress = c.String(),
                        SchoolPhone = c.String(),
                    })
                .PrimaryKey(t => t.SchoolId);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        ExamId = c.Int(nullable: false, identity: true),
                        ExamDate = c.DateTime(nullable: false),
                        DisciplineId = c.Int(nullable: false),
                        ExamFamilyId = c.Int(nullable: false),
                        SchoolId = c.Int(nullable: false),
                        ExamFinalNote = c.String(),
                    })
                .PrimaryKey(t => t.ExamId)
                .ForeignKey("dbo.Disciplines", t => t.DisciplineId)
                .ForeignKey("dbo.ExamFamilies", t => t.ExamFamilyId)
                .ForeignKey("dbo.Schools", t => t.SchoolId)
                .Index(t => t.DisciplineId)
                .Index(t => t.ExamFamilyId)
                .Index(t => t.SchoolId);
            
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskFunctions", "ParentId", "dbo.Parents");
            DropForeignKey("dbo.TaskFunctions", "TaskFamilyId", "dbo.TaskFamilies");
            DropForeignKey("dbo.ItemToBuys", "ItemCategoryId", "dbo.ItemsCategories");
            DropForeignKey("dbo.PhysicalCharacteristics", "PhysicalCharacteristicTypeId", "dbo.PhysicalCharacteristicTypes");
            DropForeignKey("dbo.PhysicalCharacteristics", "HumanBodyAreaId", "dbo.HumanBodyAreas");
            DropForeignKey("dbo.Diseases", "MedicinePharmaceuticalFormId", "dbo.MedicinePharmaceuticalForms");
            DropForeignKey("dbo.Diseases", "MedicineDosageId", "dbo.MedicineDosages");
            DropForeignKey("dbo.Diseases", "MedicineId", "dbo.Medicines");
            DropForeignKey("dbo.Diseases", "DiseaseTypeId", "dbo.DiseaseTypes");
            DropForeignKey("dbo.Diseases", "DiseaseFamilyId", "dbo.DiseaseFamilies");
            DropForeignKey("dbo.DiseaseTypes", "DiseaseFamilyId", "dbo.DiseaseFamilies");
            DropForeignKey("dbo.Alergies", "AlergyTypeId", "dbo.AlergyTypes");
            DropForeignKey("dbo.Urgencies", "UrgencySeverityId", "dbo.UrgencySeverities");
            DropForeignKey("dbo.Urgencies", "UrgencyCategoryId", "dbo.UrgencyCategories");
            DropForeignKey("dbo.Urgencies", "ParentId", "dbo.Parents");
            DropForeignKey("dbo.Urgencies", "MedicalInstitutionId", "dbo.MedicalInstitutions");
            DropForeignKey("dbo.ParentsMeetings", "ParentId", "dbo.Parents");
            DropForeignKey("dbo.Children", "Parent_ParentId1", "dbo.Parents");
            DropForeignKey("dbo.Children", "MatrimonialStateId", "dbo.MatrimonialStates");
            DropForeignKey("dbo.ChildManagements", "MatrimonialStateId", "dbo.MatrimonialStates");
            DropForeignKey("dbo.Parents", "ParentalTypeId", "dbo.ParentalTypes");
            DropForeignKey("dbo.ChildManagements", "ParentalTypeId", "dbo.ParentalTypes");
            DropForeignKey("dbo.ChildManagements", "ParentalGuardTermId", "dbo.ParentalGuardTerms");
            DropForeignKey("dbo.ChildManagements", "ParentId", "dbo.Parents");
            DropForeignKey("dbo.ChildManagements", "ChildSupportVisitId", "dbo.ChildSupportVisits");
            DropForeignKey("dbo.ChildSupportVisits", "ChildSupportVisitTypeId", "dbo.ChildSupportVisitTypes");
            DropForeignKey("dbo.ChildManagements", "ChildSupportVisitTypeId", "dbo.ChildSupportVisitTypes");
            DropForeignKey("dbo.ChildManagements", "ChildrenId", "dbo.Children");
            DropForeignKey("dbo.Children", "Parent_ParentId", "dbo.Parents");
            DropForeignKey("dbo.Children", "Mother_ParentId", "dbo.Parents");
            DropForeignKey("dbo.Children", "Father_ParentId", "dbo.Parents");
            DropForeignKey("dbo.Children", "BoodInformationId", "dbo.BloodInformations");
            DropForeignKey("dbo.Activities", "ParentId", "dbo.Parents");
            DropForeignKey("dbo.Activities", "ActivityTypeId", "dbo.ActivityTypes");
            DropForeignKey("dbo.Activities", "ActivityPeriodicityId", "dbo.ActivityPeriodicities");
            DropForeignKey("dbo.Activities", "ActivityInstitutionTypeId", "dbo.ActivityInstitutionTypes");
            DropForeignKey("dbo.Activities", "ActivityFamilyId", "dbo.ActivityFamilies");
            DropForeignKey("dbo.Exams", "SchoolId", "dbo.Schools");
            DropForeignKey("dbo.Exams", "ExamFamilyId", "dbo.ExamFamilies");
            DropForeignKey("dbo.Exams", "DisciplineId", "dbo.Disciplines");
            DropForeignKey("dbo.AcademicYears", "SchoolId", "dbo.Schools");
            DropIndex("dbo.TaskFunctions", new[] { "TaskFamilyId" });
            DropIndex("dbo.TaskFunctions", new[] { "ParentId" });
            DropIndex("dbo.TaskFamilies", "TaskFamily_TaskFamilyDescription_Index");
            DropIndex("dbo.ItemToBuys", new[] { "ItemCategoryId" });
            DropIndex("dbo.ItemsCategories", "Children_ChildrenIdentityCard_Index");
            DropIndex("dbo.PhysicalCharacteristicTypes", "PhysicalCharacteristicType_PhysicalCharacteristicTypeDescription_Index");
            DropIndex("dbo.PhysicalCharacteristics", new[] { "HumanBodyAreaId" });
            DropIndex("dbo.PhysicalCharacteristics", new[] { "PhysicalCharacteristicTypeId" });
            DropIndex("dbo.HumanBodyAreas", "HumanBodyAreas_HumanBodyAreaDescription_Index");
            DropIndex("dbo.MedicinePharmaceuticalForms", "MedicinePharmaceuticalForm_MedicinePharmaceuticalFormDescription_Index");
            DropIndex("dbo.MedicineDosages", "MedicineDosage_MedicineDosageDescription_Index");
            DropIndex("dbo.Medicines", "Medicine_MedicineName_Index");
            DropIndex("dbo.Diseases", new[] { "MedicineId" });
            DropIndex("dbo.Diseases", new[] { "MedicineDosageId" });
            DropIndex("dbo.Diseases", new[] { "MedicinePharmaceuticalFormId" });
            DropIndex("dbo.Diseases", new[] { "DiseaseFamilyId" });
            DropIndex("dbo.Diseases", new[] { "DiseaseTypeId" });
            DropIndex("dbo.DiseaseTypes", "DiseaseType_DiseaseTypeDescription_Index");
            DropIndex("dbo.DiseaseTypes", new[] { "DiseaseFamilyId" });
            DropIndex("dbo.DiseaseFamilies", "DiseaseFamily_DiseaseFamilyDescription_Index");
            DropIndex("dbo.AlergyTypes", "AlergyTypes_AlergyTypeDescriptiom_Index");
            DropIndex("dbo.Alergies", new[] { "AlergyTypeId" });
            DropIndex("dbo.UrgencySeverities", "UrgencySeverity_UrgencySeverityDescription_Index");
            DropIndex("dbo.UrgencyCategories", "UrgencyCategory_UrgencyCategoryDescription_Index");
            DropIndex("dbo.MedicalInstitutions", "MedicalInstitutions_MedicalInstitutionName_Index");
            DropIndex("dbo.Urgencies", new[] { "MedicalInstitutionId" });
            DropIndex("dbo.Urgencies", new[] { "ParentId" });
            DropIndex("dbo.Urgencies", new[] { "UrgencyCategoryId" });
            DropIndex("dbo.Urgencies", new[] { "UrgencySeverityId" });
            DropIndex("dbo.ParentsMeetings", new[] { "ParentId" });
            DropIndex("dbo.ParentalTypes", "ParentalType_ParentalTypeDescription_Index");
            DropIndex("dbo.ParentalGuardTerms", "ParentalGuardTerm_ParentalGuardTermDescription_Index");
            DropIndex("dbo.ChildSupportVisitTypes", "ChildSupportVisitType_ChildSupportVisitTypeDescription_Index");
            DropIndex("dbo.ChildSupportVisits", new[] { "ChildSupportVisitTypeId" });
            DropIndex("dbo.ChildManagements", new[] { "ParentalGuardTermId" });
            DropIndex("dbo.ChildManagements", new[] { "MatrimonialStateId" });
            DropIndex("dbo.ChildManagements", new[] { "ChildSupportVisitId" });
            DropIndex("dbo.ChildManagements", new[] { "ChildSupportVisitTypeId" });
            DropIndex("dbo.ChildManagements", new[] { "ParentalTypeId" });
            DropIndex("dbo.ChildManagements", new[] { "ParentId" });
            DropIndex("dbo.ChildManagements", new[] { "ChildrenId" });
            DropIndex("dbo.MatrimonialStates", "MatrimonialState_MatrimonialStateDescription_Index");
            DropIndex("dbo.BloodInformations", "BloodType_BloodTypeDescription_Index");
            DropIndex("dbo.Children", new[] { "Parent_ParentId1" });
            DropIndex("dbo.Children", new[] { "Parent_ParentId" });
            DropIndex("dbo.Children", new[] { "Mother_ParentId" });
            DropIndex("dbo.Children", new[] { "Father_ParentId" });
            DropIndex("dbo.Children", "Children_ChildrenIdentityCard_Index");
            DropIndex("dbo.Children", new[] { "MatrimonialStateId" });
            DropIndex("dbo.Children", new[] { "BoodInformationId" });
            DropIndex("dbo.Parents", new[] { "ParentalTypeId" });
            DropIndex("dbo.Parents", "Children_ChildrenIdentityCard_Index");
            DropIndex("dbo.ActivityTypes", "ActivityType_ActivityTypeDescription_Index");
            DropIndex("dbo.ActivityPeriodicities", "ActivityPeriodicity_ActivityPeriodicityDescription_Index");
            DropIndex("dbo.ActivityInstitutionTypes", "ActivityInstitutionType_ActivityInstitutionTypeDescription_Index");
            DropIndex("dbo.ActivityFamilies", "ActivityFamily_ActivityFamilyDescription_Index");
            DropIndex("dbo.Activities", new[] { "ActivityInstitutionTypeId" });
            DropIndex("dbo.Activities", new[] { "ParentId" });
            DropIndex("dbo.Activities", new[] { "ActivityPeriodicityId" });
            DropIndex("dbo.Activities", new[] { "ActivityTypeId" });
            DropIndex("dbo.Activities", new[] { "ActivityFamilyId" });
            DropIndex("dbo.ExamFamilies", "ExamFamily_ExamFamilyDescription_Index");
            DropIndex("dbo.Disciplines", "Discipline_DisciplineDescription_Index");
            DropIndex("dbo.Exams", new[] { "SchoolId" });
            DropIndex("dbo.Exams", new[] { "ExamFamilyId" });
            DropIndex("dbo.Exams", new[] { "DisciplineId" });
            DropIndex("dbo.AcademicYears", new[] { "SchoolId" });
            DropTable("dbo.TaskFunctions");
            DropTable("dbo.TaskFamilies");
            DropTable("dbo.ItemToBuys");
            DropTable("dbo.ItemsCategories");
            DropTable("dbo.PhysicalCharacteristicTypes");
            DropTable("dbo.PhysicalCharacteristics");
            DropTable("dbo.HumanBodyAreas");
            DropTable("dbo.MedicinePharmaceuticalForms");
            DropTable("dbo.MedicineDosages");
            DropTable("dbo.Medicines");
            DropTable("dbo.Diseases");
            DropTable("dbo.DiseaseTypes");
            DropTable("dbo.DiseaseFamilies");
            DropTable("dbo.ChildSupportPayments");
            DropTable("dbo.AlergyTypes");
            DropTable("dbo.Alergies");
            DropTable("dbo.UrgencySeverities");
            DropTable("dbo.UrgencyCategories");
            DropTable("dbo.MedicalInstitutions");
            DropTable("dbo.Urgencies");
            DropTable("dbo.ParentsMeetings");
            DropTable("dbo.ParentalTypes");
            DropTable("dbo.ParentalGuardTerms");
            DropTable("dbo.ChildSupportVisitTypes");
            DropTable("dbo.ChildSupportVisits");
            DropTable("dbo.ChildManagements");
            DropTable("dbo.MatrimonialStates");
            DropTable("dbo.BloodInformations");
            DropTable("dbo.Children");
            DropTable("dbo.Parents");
            DropTable("dbo.ActivityTypes");
            DropTable("dbo.ActivityPeriodicities");
            DropTable("dbo.ActivityInstitutionTypes");
            DropTable("dbo.ActivityFamilies");
            DropTable("dbo.Activities");
            DropTable("dbo.ExamFamilies");
            DropTable("dbo.Disciplines");
            DropTable("dbo.Exams");
            DropTable("dbo.Schools");
            DropTable("dbo.AcademicYears");
        }
    }
}
