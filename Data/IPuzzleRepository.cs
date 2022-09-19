using DnDPuzzles.Data.Entities;
using System.Collections.Generic;

namespace DnDPuzzles.Data
{
    public interface IPuzzleRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        bool SaveAll();
    }
}