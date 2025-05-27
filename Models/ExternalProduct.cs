using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Models
{
    public class ExternalProduct
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Naslov je obavezan.")]
        [StringLength(100, ErrorMessage = "Naslov ne sme biti duži od 100 karaktera.")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Opis je obavezan.")]
        [StringLength(1000, ErrorMessage = "Opis ne sme biti duži od 1000 karaktera.")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Cena je obavezna.")]
        [Range(0.01, 100000, ErrorMessage = "Cena mora biti između 0.01 i 100000.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Slike su obavezne.")]
        public required List<string> Images { get; set; }
    }
}
