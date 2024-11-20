namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class PermissionDTO
    {
        public int code { get; set; }
        public string name { get; set; }
        public string module { get; set; }
        public bool? status { get; set; }
        public string? filter { get; set; }
        public string? ObjectType { get; set; }
        public string? Object { get; set; }
        public string? Config { get; set; }
    }
}
