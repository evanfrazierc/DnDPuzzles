using DnDPuzzles.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDPuzzles.Data
{
    public class PuzzleRepository : IPuzzleRepository
    {
        private readonly PuzzleContext ctx;
        private readonly ILogger<PuzzleRepository> logger;

        public PuzzleRepository(PuzzleContext ctx, ILogger<PuzzleRepository> logger)
        {
            this.ctx = ctx;
            this.logger = logger;
        }

        public void AddEntity(object model)
        {
            ctx.Add(model);
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ToList();
            }
            else
            {
                return ctx.Orders
                    .ToList();
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                logger.LogInformation("GetAllProducts was called");

                return ctx.Products
                    .OrderBy(p => p.Title)
                    .ToList();
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public Order GetOrderbyId(int id)
        {
            return ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return ctx.Products
                .Where(p => p.Category == category)
                .ToList();
        }

        public bool SaveAll()
        {
            return ctx.SaveChanges() > 0;
        }
    }
}
