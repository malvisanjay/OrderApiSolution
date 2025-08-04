using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data;
using OrderApi.Models;
using System.Data;

namespace OrderApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync() =>
            await _context.Orders.ToListAsync();

        public async Task<Order> GetByIdAsync(int id) =>
            await _context.Orders.FindAsync(id);

        public async Task<Order> CreateAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersPagedAsync(
            int pageNumber, int pageSize,
            string sortColumn, string sortDirection,
            string labelNo, string carrier, string contactName)
        {
            var orders = new List<Order>();

            using (var conn = _context.Database.GetDbConnection())
            {
                await conn.OpenAsync();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "GetOrdersPagedSortedFiltered";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@PageNumber", pageNumber));
                    cmd.Parameters.Add(new SqlParameter("@PageSize", pageSize));
                    cmd.Parameters.Add(new SqlParameter("@SortColumn", sortColumn ?? "Id"));
                    cmd.Parameters.Add(new SqlParameter("@SortDirection", sortDirection ?? "ASC"));
                    cmd.Parameters.Add(new SqlParameter("@LabelNo", (object?)labelNo ?? DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@Carrier", (object?)carrier ?? DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@ContactName", (object?)contactName ?? DBNull.Value));

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            orders.Add(new Order
                            {
                                Id = reader.GetInt32(0),
                                LabelNo = reader.GetString(1),
                                Carrier = reader.GetString(2),
                                ContactName = reader.GetString(3),
                                CompanyName = reader.GetString(4),
                                SenderStreet = reader.GetString(5),
                                SenderCity = reader.GetString(6),
                                SenderPostCode = reader.GetString(7),
                                SenderPhone = reader.GetString(8),
                                ReceiverStreet = reader.GetString(9),
                                ReceiverCity = reader.GetString(10),
                                ReceiverPostCode = reader.GetString(11),
                                ReceiverPhone = reader.GetString(12),
                                DateCreated = reader.GetDateTime(13)
                            });
                        }
                    }
                }
            }

            return orders;
        }
    }
}