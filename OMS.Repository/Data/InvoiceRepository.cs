using Microsoft.EntityFrameworkCore;
using OMS.Core.Entities;
using OMS.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Repository.Data
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly OrderManagementDbContext _context;

        public InvoiceRepository(OrderManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Invoice> GetInvoiceByIdAsync(int invoiceId)
        {
            return await _context.Invoices.FindAsync(invoiceId);
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            return await _context.Invoices.ToListAsync();
        }
    }
    }
