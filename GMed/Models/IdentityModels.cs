using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using GMed.Models;

namespace GMed.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Add(new DataTypePropertyAttributeConvention());
            modelBuilder.Entity<RendezVous>().HasRequired(r => r.ActeMedical);

            modelBuilder.Entity<Facture>().HasRequired(a => a.ActeMedical);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<GMed.Models.DMDPatient> DMDPatients { get; set; }

        public System.Data.Entity.DbSet<GMed.Models.RendezVous> RendezVous { get; set; }

        public System.Data.Entity.DbSet<GMed.Models.ActeMedical> ActeMedicals { get; set; }

        public System.Data.Entity.DbSet<GMed.Models.DocumentComplementaire> DocumentComplementaires { get; set; }

        public System.Data.Entity.DbSet<GMed.Models.Facture> Factures { get; set; }
        public System.Data.Entity.DbSet<GMed.Models.CalendarEvent> CalendarEvents { get; set; }
    }
}