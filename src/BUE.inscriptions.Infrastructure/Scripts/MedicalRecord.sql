CREATE TABLE MedicalRecords (
    Id INT PRIMARY KEY IDENTITY(1, 1),
    RequestId int NOT NULL,
    Height FLOAT NOT NULL,
    Weight FLOAT NOT NULL,
    Disease BIT NOT NULL,
    DiseaseObservation VARCHAR(255),
    IsAllergies BIT NOT NULL,
    MedicationAllergy VARCHAR(255),
    ReactionMedication VARCHAR(255),
    FoodAllergy VARCHAR(255),
    ReactionFood VARCHAR(255),
    InsectsAllergy VARCHAR(255),
    ReactionInsects VARCHAR(255),
    OtherAllergy VARCHAR(255),
    ReactionOthers VARCHAR(255),
    TakeMedication BIT NOT NULL,
    HasIncapacity BIT NOT NULL,
    TakeMedicationObservation VARCHAR(255),
    HasIncapacityObservation VARCHAR(255),
    DisqualifiedSport BIT NOT NULL,
    VaccineAutorization BIT NOT NULL,
    VaccineUrlFile VARCHAR(255),
    DisqualifiedSportUrlFile VARCHAR(255)
);