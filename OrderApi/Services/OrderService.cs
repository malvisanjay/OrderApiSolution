using OrderApi.Models;
using OrderApi.Repositories;

namespace OrderApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Order>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Order> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<Order> CreateAsync(Order order) => _repository.CreateAsync(order);
        public Task UpdateAsync(Order order) => _repository.UpdateAsync(order);
        public Task DeleteAsync(int id) => _repository.DeleteAsync(id);

        public Task<IEnumerable<Order>> GetOrdersPagedAsync(
            int pageNumber, int pageSize,
            string sortColumn, string sortDirection,
            string labelNo, string carrier, string contactName)
        {
            return _repository.GetOrdersPagedAsync(pageNumber, pageSize, sortColumn, sortDirection, labelNo, carrier, contactName);
        }
    }
}