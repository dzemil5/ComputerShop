using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ComputerShop.Models;

namespace ComputerShop.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            Input = new InputModel(); 
            Username = string.Empty;  
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string Username { get; set; }

        public class InputModel
        {
            [Display(Name = "Puno ime")]
            [Required(ErrorMessage = "Puno ime je obavezno.")]
            [StringLength(100)]
            public string FullName { get; set; } = string.Empty; 

            [Display(Name = "Adresa")]
            [StringLength(200)]
            public string Adresa { get; set; } = string.Empty; 

            [Display(Name = "Grad")]
            [StringLength(100)]
            public string Grad { get; set; } = string.Empty; 
            [Display(Name = "Poštanski broj")]
            [StringLength(20)]
            public string PostanskiBroj { get; set; } = string.Empty;

            [Display(Name = "Telefon")]
            [Phone(ErrorMessage = "Unesite ispravan broj telefona.")]
            public string Telefon { get; set; } = string.Empty;
        }

        private Task LoadAsync(ApplicationUser user)
        {
            Username = user.UserName ?? string.Empty;

            Input = new InputModel
            {
                FullName = user.FullName ?? string.Empty,
                Adresa = user.Adresa ?? string.Empty,
                Grad = user.Grad ?? string.Empty,
                PostanskiBroj = user.PostanskiBroj ?? string.Empty,
                Telefon = user.Telefon ?? string.Empty
            };

            return Task.CompletedTask;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Korisnik nije pronađen.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Korisnik nije pronađen.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            user.FullName = Input.FullName;
            user.Adresa = Input.Adresa;
            user.Grad = Input.Grad;
            user.PostanskiBroj = Input.PostanskiBroj;
            user.Telefon = Input.Telefon;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                await LoadAsync(user);
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            TempData["StatusMessage"] = "Vaš profil je uspešno ažuriran.";
            return RedirectToPage();
        }
    }
}
