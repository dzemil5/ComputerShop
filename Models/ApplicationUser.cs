using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Puno ime")]
        [Required(ErrorMessage = "Puno ime je obavezno.")]
        [StringLength(100, ErrorMessage = "Puno ime može imati najviše 100 karaktera.")]
        public string? FullName { get; set; }

        [Display(Name = "Adresa")]
        [StringLength(200, ErrorMessage = "Adresa može imati najviše 200 karaktera.")]
        public string? Adresa { get; set; }

        [Display(Name = "Grad")]
        [StringLength(100, ErrorMessage = "Grad može imati najviše 100 karaktera.")]
        public string? Grad { get; set; }

        [Display(Name = "Poštanski broj")]
        [StringLength(20, ErrorMessage = "Poštanski broj može imati najviše 20 karaktera.")]
        public string? PostanskiBroj { get; set; }

        [Display(Name = "Telefon")]
        [Phone(ErrorMessage = "Unesite ispravan broj telefona.")]
        public string? Telefon { get; set; }
    }
}
