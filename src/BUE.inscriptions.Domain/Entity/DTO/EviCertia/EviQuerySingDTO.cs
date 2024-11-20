namespace BUE.Inscriptions.Domain.Entity.DTO.EviCertia
{
    public class EviQuerySingDTO
    {
        public string evidenceId { get; set; }

        public string lookupKey { get; set; }

        public string? document { get; set; }

        public string subject { get; set; }

        public string state { get; set; }

        public string outcome { get; set; }

        public DateTime creationDate { get; set; }

        public DateTime lastStateChangeDate { get; set; }

        public DateTime submittedOn { get; set; }

        public DateTime processedOn { get; set; }

        public DateTime sentOn { get; set; }

        public DateTime closedOn { get; set; }

        public DateTime signedOn { get; set; }

        public List<SigningParty> signingParties { get; set; }

        public List<object> interestedParties { get; set; }

        public List<object> affidavits { get; set; }

        public List<object> attachments { get; set; }

        public string issuer { get; set; }

        public int timeToLive { get; set; }

        public int onlineRetentionPeriod { get; set; }

        public int notaryRetentionPeriod { get; set; }

        public string sourceChannel { get; set; }

        public List<string> affidavitKinds { get; set; }
    }

    public class EvicertiaCancelDTO
    {
        public string uniqueid { get; set; }
        public string comments { get; set; }
    }
}
