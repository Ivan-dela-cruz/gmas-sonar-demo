using System.ComponentModel;

namespace BUE.Inscriptions.Domain.Entity.DTO
{
    public class AuthDTO
    {
        [DefaultValue("admin@gmail.com")]
        public string Email { get; set; }
        [DefaultValue("root1234")]
        public string Password { get; set; }
        [DefaultValue(9)]
        public int? CurrentSchoolYear { get; set; }
    }
}
