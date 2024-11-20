namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class MedicalRecordDTO
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public bool Disease { get; set; }
        public string? DiseaseObservation { get; set; }
        public bool IsAllergies { get; set; }
        public string? MedicationAllergy { get; set; }
        public string? ReactionMedication { get; set; }
        public string? FoodAllergy { get; set; }
        public string? ReactionFood { get; set; }
        public string? InsectsAllergy { get; set; }
        public string? ReactionInsects { get; set; }
        public string? OtherAllergy { get; set; }
        public string? ReactionOthers { get; set; }
        public bool TakeMedication { get; set; }
        public bool HasIncapacity { get; set; }
        public string? TakeMedicationObservation { get; set; }
        public string? HasIncapacityObservation { get; set; }
        public bool DisqualifiedSport { get; set; }
        public bool VaccineAutorization { get; set; }
        public byte[]? VaccineFile { get; set; }
        public string? VaccineUrlFile { get; set; }
        public byte[]? DisqualifiedSportFile { get; set; }
        public byte[]? HasIncapacityFile { get; set; }
        public string? HasIncapacityUrlFile { get; set; }
        public string? DisqualifiedSportUrlFile { get; set; }
        public virtual PortalRequest? PortalRequest { get; set; }
    }
}
