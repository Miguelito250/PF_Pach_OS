using Microsoft.AspNetCore.Identity;
using static ServiceStack.LicenseUtils;
using System.ComponentModel.DataAnnotations;

namespace PF_Pach_OS.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Tipo de documento *")]
        public string DocumentType { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "N° documento *")]
        public string DocumentNumber { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Nombre *")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        [Display(Name = "Apellido *")]
        public string LastName { get; set; }
        public DateTime EntryDay { get; set; }
        public int State { get; set; }
        public string EmailConfirmationToken { get; set; }
        public string PasswordResetToken { get; set; }
        public string PhoneNumber { get; set; }
        public int Id_Rol { get; set; }


    }
}
