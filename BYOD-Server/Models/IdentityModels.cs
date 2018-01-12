using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Data.Entity;

namespace BYOD_Server.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string avatar { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public System.DateTime? date_joined { get; set; }
        public int strip_id { get; set; }

        public ICollection<Merchants> merchant { get; set; }
        public ICollection<ReviewRatings> review { get; set; }
        public ICollection<StripeCards> stripeCards { get; set; }
        public ICollection<PaymentHistory> paymentHistory { get; set; }
        public ICollection<Orders> orders { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<FoodType> food_type { get; set; }
        public DbSet<FoodOrdered> food_ordered { get; set; }
        public DbSet<MerchantCategory> merchant_category { get; set; }
        public DbSet<MerchantProduct> merchant_product { get; set; }
        public DbSet<Merchants> Merchants { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OutletProduct> outlet_product { get; set; }
        public DbSet<OutletReview> OutletReview { get; set; }
        public DbSet<Outlets> Outlets { get; set; }
        public DbSet<Promocodes> Promocodes { get; set; }
        public DbSet<ReviewRatings> Review_Ratings { get; set; }
        public DbSet<PaymentHistory> PaymentHistories { get; set; }
        public DbSet<StripeCards> StripeCards { get; set; }
        public DbSet<RestaurantTable> RestaurantTable { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<BYOD_Server.Models.CardType> CardTypes { get; set; }
    }
}