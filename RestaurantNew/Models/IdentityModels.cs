using System.Data.Entity;

using System.Reflection.Emit;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RestaurantNew.Models
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

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

       
        public System.Data.Entity.DbSet<RestaurantNew.Models.City> Cities { get; set; }

        public System.Data.Entity.DbSet<RestaurantNew.Models.Menu> Menus { get; set; }

        public System.Data.Entity.DbSet<RestaurantNew.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<RestaurantNew.Models.CustUser> CustUsers { get; set; }

        //protected override void OnModelCreating(ModuleBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<MenuSale>()
        //        .HasKey(t => new { t.MenuId, t.SaleId });

        //    modelBuilder.Entity<MenuSale>()
        //        .HasOne(pt => pt.Menu)
        //        .WithMany(p => p.MenuSale)
        //        .HasForeignKey(pt => pt.MenuId);

        //    modelBuilder.Entity<MenuSale>()
        //        .HasOne(pt => pt.Sale)
        //        .WithMany(t => t.MenuSale)
        //        .HasForeignKey(pt => pt.SaleId);

        //}
    }
}