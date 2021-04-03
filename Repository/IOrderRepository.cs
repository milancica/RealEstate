using System.Linq;
using RealEstate.Models;

namespace RealEstate.Repository
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        void SaveOrder(Order order);
    }
}
