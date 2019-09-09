using System.Data.Entity;

using System.Reflection.Emit;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace RestaurantNew.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string UserStatus { get; set; }
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

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

       
        public System.Data.Entity.DbSet<RestaurantNew.Models.City> Cities { get; set; }

        public System.Data.Entity.DbSet<RestaurantNew.Models.Menu> Menus { get; set; }

        public System.Data.Entity.DbSet<RestaurantNew.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<RestaurantNew.Models.CustUser> CustUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        
            

            modelBuilder.Entity<Menu>()
            .HasMany<Sale>(s => s.Sales)
            .WithMany(c => c.Menus)
            .Map(cs =>
            {
                cs.MapLeftKey("IdMenu");
                cs.MapRightKey("Id");
                cs.ToTable("MenuSale");
            });

        }

        public System.Data.Entity.DbSet<RestaurantNew.Models.Sale> Sales { get; set; }

        public System.Data.Entity.DbSet<RestaurantNew.Models.MenuSale> MenuSales { get; set; }
    }
}