using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Models
{
    public class StavkaPorudzbine
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Porudžbina je obavezna.")]
        [Display(Name = "Porudžbina")]
        public int PorudzbinaId { get; set; }

        public Porudzbina? Porudzbina { get; set; }

        [Required(ErrorMessage = "Proizvod je obavezan.")]
        [Display(Name = "Proizvod")]
        public int ProizvodId { get; set; }

        public Proizvod? Proizvod { get; set; }

        [Required(ErrorMessage = "Količina je obavezna.")]
        [Range(1, int.MaxValue, ErrorMessage = "Količina mora biti veća od 0.")]
        [Display(Name = "Količina")]
        public int Kolicina { get; set; }
    }
}
