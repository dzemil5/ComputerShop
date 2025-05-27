using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ComputerShop.Models
{
    public class Korpa
    {
        [Required]
        public List<KorpaItem> Stavke { get; set; } = new List<KorpaItem>();

        public void DodajProizvod(Proizvod proizvod, int kolicina = 1)
        {
            var stavka = Stavke.FirstOrDefault(s => s.ProizvodId == proizvod.Id);
            if (stavka == null)
            {
                Stavke.Add(new KorpaItem { ProizvodId = proizvod.Id, Proizvod = proizvod, Kolicina = kolicina });
            }
            else
            {
                stavka.Kolicina += kolicina;
            }
        }

        public void UkloniProizvod(int proizvodId)
        {
            var stavka = Stavke.FirstOrDefault(s => s.ProizvodId == proizvodId);
            if (stavka != null)
            {
                Stavke.Remove(stavka);
            }
        }

        public decimal UkupnaCena()
        {
            return Stavke.Sum(s => s.Proizvod.Cena * s.Kolicina);
        }

        public void Ocisti()
        {
            Stavke.Clear();
        }
    }
}
