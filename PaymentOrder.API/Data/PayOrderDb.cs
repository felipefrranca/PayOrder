using Microsoft.EntityFrameworkCore;

namespace PaymentOrder.API.Data
{
    public class PayOrderDb : DbContext
    {
        public PayOrderDb(DbContextOptions<PayOrderDb> options)
        : base(options) { }

        public DbSet<PayOrder> PayOrders => Set<PayOrder>();
    }
}
