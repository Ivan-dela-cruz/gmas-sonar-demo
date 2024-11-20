using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BUE.Inscriptions.Domain.Entity
{
    public class MedicalRecord
    {
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("requestId")]
        public int RequestId { get; set; }

        [JsonProperty("height")]
        public double Height { get; set; }

        [JsonProperty("weight")]
        public double Weight { get; set; }

        [JsonProperty("disease")]
        public bool Disease { get; set; }

        [JsonProperty("diseaseObservation")]
        public string? DiseaseObservation { get; set; }

        [JsonProperty("isAllergies")]
        public bool IsAllergies { get; set; }

        [JsonProperty("medicationAllergy")]
        public string? MedicationAllergy { get; set; }

        [JsonProperty("reactionMedication")]
        public string? ReactionMedication { get; set; }

        [JsonProperty("foodAllergy")]
        public string? FoodAllergy { get; set; }

        [JsonProperty("reactionFood")]
        public string? ReactionFood { get; set; }

        [JsonProperty("insectsAllergy")]
        public string? InsectsAllergy { get; set; }

        [JsonProperty("reactionInsects")]
        public string? ReactionInsects { get; set; }

        [JsonProperty("otherAllergy")]
        public string? OtherAllergy { get; set; }

        [JsonProperty("reactionOthers")]
        public string? ReactionOthers { get; set; }

        [JsonProperty("takeMedication")]
        public bool TakeMedication { get; set; }

        [JsonProperty("hasIncapacity")]
        public bool HasIncapacity { get; set; }  
        [JsonProperty("hasIncapacityUrlFile")]
        public string? HasIncapacityUrlFile { get; set; }

        [JsonProperty("takeMedicationObservation")]
        public string? TakeMedicationObservation { get; set; }

        [JsonProperty("hasIncapacityObservation")]
        public string? HasIncapacityObservation { get; set; }

        [JsonProperty("disqualifiedSport")]
        public bool DisqualifiedSport { get; set; }

        [JsonProperty("vaccineAutorization")]
        public bool VaccineAutorization { get; set; }

        [JsonProperty("vaccineUrlFile")]
        public string? VaccineUrlFile { get; set; }

        [JsonProperty("disqualifiedSportUrlFile")]
        public string? DisqualifiedSportUrlFile { get; set; }

        [JsonProperty("portalRequest")]
        public virtual PortalRequest? PortalRequest { get; set; }
    }
}
