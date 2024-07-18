using OMS.Core.Entities;
using OMS.Core.IRepositories;
using OMS.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Service
{
    public class OrderValidationService : IOrderValidationService
    {
        private readonly IProductRepository _productRepository;

        public OrderValidationService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> ValidateOrderAsync(Order order)
        {
            foreach (var item in order.OrderItems)
            {
                var product = await _productRepository.GetProductByIdAsync(item.ProductId);
                if (product is null || product.Stock < item.Quantity)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
