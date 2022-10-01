using DnDPuzzles.Data.Entities;
using System.Collections.Generic;

namespace DnDPuzzles.Data
{
    public interface IPuzzleRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);

        IEnumerable<Order> GetAllOrders(bool includeItems);
        IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems);
        Order GetOrderbyId(string username, int id);

        bool SaveAll();
        void AddEntity(object model);
    }
}