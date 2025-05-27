using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Models
{
    public class Kategorija
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Naziv kategorije je obavezan.")]
        [StringLength(100, ErrorMessage = "Naziv kategorije može sadržati najviše 100 karaktera.")]
        [Display(Name = "Naziv kategorije")]
        public string Naziv { get; set; } = string.Empty; 
    }
}
