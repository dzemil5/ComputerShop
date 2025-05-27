using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ComputerShop.Models;

namespace ComputerShop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Kategorija> Kategorija { get; set; }
        public DbSet<Proizvod> Proizvod { get; set; }
        public DbSet<Porudzbina> Porudzbina { get; set; }
        public DbSet<StavkaPorudzbine> StavkaPorudzbine { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Proizvod>()
                .HasOne(p => p.Kategorija)
                .WithMany()
                .HasForeignKey(p => p.KategorijaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(l => new { l.LoginProvider, l.ProviderKey });
                entity.Property(l => l.LoginProvider).HasMaxLength(128).IsRequired();
                entity.Property(l => l.ProviderKey).HasMaxLength(128).IsRequired();
                entity.Property(l => l.UserId).IsRequired();
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
                entity.Property(t => t.LoginProvider).HasMaxLength(128).IsRequired();
                entity.Property(t => t.Name).HasMaxLength(128).IsRequired();
            });

            builder.Entity<Porudzbina>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.KorisnikId).IsRequired();

                entity.HasOne(e => e.Korisnik)
                      .WithMany()
                      .HasForeignKey(e => e.KorisnikId)
                      .IsRequired();

                entity.HasMany(e => e.Stavke)
                      .WithOne(s => s.Porudzbina)
                      .HasForeignKey(s => s.PorudzbinaId);
            });

            builder.Entity<StavkaPorudzbine>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Porudzbina)
                      .WithMany(p => p.Stavke)
                      .HasForeignKey(e => e.PorudzbinaId)
                      .IsRequired();

                entity.HasOne(e => e.Proizvod)
                      .WithMany()
                      .HasForeignKey(e => e.ProizvodId)
                      .IsRequired();

                entity.Property(e => e.Kolicina).IsRequired();
            });
        }
    }
}
