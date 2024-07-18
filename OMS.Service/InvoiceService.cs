using OMS.Core.Entities;
using OMS.Core.IServices;
using OMS.Repository.Data;

namespace OMS.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly OrderManagementDbContext _context;

        public InvoiceService(OrderManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Invoice> GenerateInvoiceAsync(Order order)
        {
            var invoice = new Invoice
            {
                OrderId = order.OrderId,
                InvoiceDate = DateTime.UtcNow,
                TotalAmount = order.TotalAmount
            };

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return invoice;
        }
    }
}
