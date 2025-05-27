using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerShop.Models
{
    public class Proizvod
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Naziv je obavezan.")]
        [StringLength(100, ErrorMessage = "Naziv može sadržati najviše 100 karaktera.")]
        public required string Naziv { get; set; }

        [Required(ErrorMessage = "Opis je obavezan.")]
        [StringLength(1000, ErrorMessage = "Opis može sadržati najviše 1000 karaktera.")]
        public required string Opis { get; set; }

        [Required(ErrorMessage = "Cena je obavezna.")]
        [Range(0.01, 1000000, ErrorMessage = "Cena mora biti između 0.01 i 1.000.000.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cena { get; set; }

        [Required(ErrorMessage = "Količina je obavezna.")]
        [Range(0, int.MaxValue, ErrorMessage = "Količina mora biti pozitivan broj.")]
        public int Kolicina { get; set; }

        [Display(Name = "Putanja slike")]
        [StringLength(500, ErrorMessage = "Putanja slike može imati najviše 500 karaktera.")]
        public string? SlikaPath { get; set; }

        [Required(ErrorMessage = "Kategorija je obavezna.")]
        [Display(Name = "Kategorija")]
        public int KategorijaId { get; set; }

        [ForeignKey("KategorijaId")]
        public Kategorija? Kategorija { get; set; }
    }
}
