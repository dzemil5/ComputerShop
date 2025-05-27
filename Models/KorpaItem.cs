using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Models
{
    public class KorpaItem
    {
        [Required]
        public int ProizvodId { get; set; }

        [Required]
        public required Proizvod Proizvod { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Količina mora biti bar 1.")]
        public int Kolicina { get; set; }
    }
}
