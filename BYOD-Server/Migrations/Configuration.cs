namespace BYOD_Server.Migrations
{
    using BYOD_Server.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BYOD_Server.Models.ApplicationDbContext>
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BYOD_Server.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

            //Role seeder
            if (!context.Roles.Any(r => r.Name == "AppAdmin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "AppAdmin" };

                manager.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "MerchantAdmin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "MerchantAdmin" };

                manager.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "Customer"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Customer" };

                manager.Create(role);
            }
            //End Role seeder


            //User seeder
            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("Password123!");

            if (!context.Users.Any(u => u.UserName == "Steve@Steve.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "Steve@Steve.com",
                    Email = "Steve@Steve.com",
                    PasswordHash = password,
                    PhoneNumber = "08869879"
                };
                manager.Create(user);
                manager.AddToRole(user.Id, "AppAdmin");
            }
            if (!context.Users.Any(u => u.UserName == "Barrel@Barrel.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "Barrel@Barrel.com",
                    Email = "Barrel@Barrel.com",
                    PasswordHash = password,
                    PhoneNumber = "08869879"
                };
                manager.Create(user);
                manager.AddToRole(user.Id, "MerchantAdmin");
                //Merchant seeder
                context.Merchants.AddOrUpdate(x => x.merchant_id,
                    new Merchants()
                    {
                        biz_add = "Woodlands Ave 1",
                        biz_postal = "123456",
                        merchant_cat = 1,
                        biz_verified = true,
                        mobile_no = "61237894",
                        user_id = user.Id,
                        merchant_id = 1
                    });
                //End Merchant seeder
                //Outlet seeder
                context.Outlets.AddOrUpdate(x => x.outlet_id,
                    new Outlets()
                    {
                        name = "Macdonalds Extra",
                        merchant_id = 3,
                        postal_code = "123456",
                        opening_status = true,
                        total_comments = 0,
                        streetname = "Kallang",
                        opening_time = DateTime.Now,
                        unit_no = "02-123",
                        avg_ratings = 3,
                        closing_time = DateTime.Now.AddHours(8),
                        contact_no = "1233423",
                        lat = 1.4422,
                        lon = 103.7858,
                        last_review_time = DateTime.Now
                    });

                //End Outlet seeder
            }

            if (!context.Users.Any(u => u.UserName == "Andrew@Andrew.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "Andrew@Andrew.com",
                    Email = "Andrew@Andrew.com",
                    PasswordHash = password,
                    PhoneNumber = "08869879"
                };
                manager.Create(user);
                manager.AddToRole(user.Id, "Customer");
            }

            //End User seeder

            //Food type seeder
            if (!context.food_type.Any(u => u.name == "Starter"))
            {
                context.food_type.AddOrUpdate(x => x.type_id,
                new FoodType()
                {
                    name = "Starter"
                },
                new FoodType()
                {
                    name = "Mains"
                },
                new FoodType()
                {
                    name = "Beverage"
                },
                new FoodType()
                {
                    name = "Dessert"
                }
                );
            }
            //End Food type seeder

            //Merchant category seeder
            if (!context.merchant_category.Any(u => u.name == "Fast food"))
            {
                context.merchant_category.AddOrUpdate(x => x.merchantCat_id,
                new MerchantCategory()
                {
                    name = "Fast food"
                });
            }
            //End Merchant category seeder


        }
    }
}
