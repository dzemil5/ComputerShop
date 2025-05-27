using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Models
{
    public class Porudzbina
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Datum porudžbine je obavezan.")]
        [Display(Name = "Datum porudžbine")]
        public DateTime Datum { get; set; }

        [Required(ErrorMessage = "Korisnik je obavezan.")]
        [Display(Name = "Korisnik")]
        public string KorisnikId { get; set; } = string.Empty;

        public ApplicationUser? Korisnik { get; set; }

        public ICollection<StavkaPorudzbine>? Stavke { get; set; }
    }
}
