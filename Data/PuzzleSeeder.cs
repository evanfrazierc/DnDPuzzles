using DnDPuzzles.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DnDPuzzles.Data
{
    public class PuzzleSeeder
    {
        private readonly PuzzleContext ctx;
        private readonly IWebHostEnvironment env;
        private readonly UserManager<StoreUser> userManager;

        public PuzzleSeeder(PuzzleContext ctx, IWebHostEnvironment env, UserManager<StoreUser> userManager)
        {
            this.ctx = ctx;
            this.env = env;
            this.userManager = userManager;
        }

        public async Task SeedAsync()
        {
            ctx.Database.EnsureCreated();

            StoreUser user = await userManager.FindByEmailAsync("efrazier@dndpuzzles.com");
            if (user == null)
            {
                user = new StoreUser
                {
                    FirstName = "Evan",
                    LastName = "Frazier",
                    Email = "efrazier@dndpuzzles.com",
                    UserName = "efrazier@dndpuzzles.com"
                };

                var result = await userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }
            }

            if (!ctx.Products.Any())
            {
                // Need to create the Sample Data
                var filePath = Path.Combine(env.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filePath);
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);

                ctx.Products.AddRange(products);

                var order = new Order()
                {
                    User = user,
                    OrderDate = DateTime.Today,
                    OrderNumber = "10000",
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                };

                ctx.Orders.Add(order);

                ctx.SaveChanges();
            }
        }
    }
}
