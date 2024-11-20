namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class UserPassDto
    {
        public int UserId { get; set; }
        public string? Password { get; set; }
        public string? RememberToken { get; set; }
    }
}
