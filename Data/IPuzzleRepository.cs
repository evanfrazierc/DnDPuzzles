using DnDPuzzles.Data.Entities;
using System.Collections.Generic;

namespace DnDPuzzles.Data
{
    public interface IPuzzleRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);

        IEnumerable<Order> GetAllOrders(bool includeItems);
        Order GetOrderbyId(int id);

        bool SaveAll();
        void AddEntity(object model);
    }
}