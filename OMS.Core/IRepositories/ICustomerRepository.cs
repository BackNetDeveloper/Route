using OMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.IRepositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerAsync(int customerId);
        Task<IEnumerable<Order>> GetCustomerOrdersAsync(int customerId);
        Task AddCustomerAsync(Customer customer);
    }
}
