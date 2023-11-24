using Microsoft.AspNetCore.Identity;

namespace PF_Pach_OS.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EntryDay { get; set; }
        public int State { get; set; }
        public string EmailConfirmationToken { get; set; }
        public string PasswordResetToken { get; set; }
        public string PhoneNumber { get; set; }
        public int Id_Rol { get; set; }


    }
}
