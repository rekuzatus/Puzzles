﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Puzzles.Data.Static;
using Puzzles.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzles.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();
                //Ingredient
                if (!context.Ingredients.Any())
                {
                    context.Ingredients.AddRange(new List<Ingredient>()
                    {
                        new Ingredient()
                        {
                            Name = "Spiced Rum",
                            Brand = "Bacardi",
                            ImageUrl = "https://i.ibb.co/NWkgr70/rumspiced.png"

                        },
                        new Ingredient()
                        {
                            Name = "Vodka",
                            Brand = "Absolut",
                            ImageUrl = "https://i.ibb.co/z77yvK8/vodka.png"
                        },
                        new Ingredient()
                        {
                            Name = "Gin",
                            Brand = "Beefeater",
                            ImageUrl = "https://i.ibb.co/4VnpXkB/gin.png"
                        },
                        new Ingredient()
                        {
                            Name = "Tequila Silver",
                            Brand = "Jose Cuervo",
                            ImageUrl = "https://i.ibb.co/1dHWsGG/tequilasilver.png"
                        },new Ingredient()
                        {
                            Name = "Lime Juice",
                            Brand = "Monin",
                            ImageUrl = "https://i.ibb.co/1nsqZpS/limejuice.png"
                        },
                        new Ingredient()
                        {
                            Name = "Coca-Cola",
                            Brand = "Cola-Coca",
                            ImageUrl = "https://i.ibb.co/k8tqY6f/cola.png"
                        }
                    });
                    context.SaveChanges();
                }
                //Glasses
                if (!context.Glasses.Any())
                {
                    context.Glasses.AddRange(new List<Glass>()
                    {
                        new Glass()
                        {
                            Name = "Collins",
                            ImageUrl ="https://i.ibb.co/b7BCxmq/collins.png"
                        }
                    });
                    context.SaveChanges();
                }
                //Cocktails
                if (!context.Cocktails.Any())
                {
                    context.Cocktails.AddRange(new List<Cocktail>()
                    {
                        new Cocktail()
                        {
                            Name = "Cuba Libre",
                            Description = "One of the most popular cocktails in the world. As the story goes, a captain in the U.S. Army station in Havana poured Coca-Cola and lime juice over his Bacardi and toasted to his Cuban comrades \"Por Cuba Libre\"",
                            Price = 15.00,
                            ImageUrl = "https://i.ibb.co/9pvGtpD/cubalibre.png",
                            DrinkCategory = DrinkCategory.Alcoholic,
                            GlassId = 1
                        }
                    });
                    context.SaveChanges();
                }
                //Ingredients & Cocktails
                if (!context.Ingredients_Cocktails.Any())
                {
                    context.Ingredients_Cocktails.AddRange(new List<Ingredient_Cocktail>()
                    {
                        new Ingredient_Cocktail()
                        {
                            IngredientId= 1,
                            CocktailId= 2
                        },
                        new Ingredient_Cocktail() 
                        { 
                            IngredientId= 5,
                            CocktailId= 2
                        },
                        new Ingredient_Cocktail()
                        {
                            IngredientId= 6,
                            CocktailId= 2
                        }

                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@puzzles.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Pass@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@puzzles.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Pass@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }

        
    }
}
