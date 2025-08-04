using OrderApi.Models;

namespace OrderApi.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(int id);
        Task<Order> CreateAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(int id);

        Task<IEnumerable<Order>> GetOrdersPagedAsync(
            int pageNumber, int pageSize,
            string sortColumn, string sortDirection,
            string labelNo, string carrier, string contactName);
    }
}