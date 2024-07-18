using OMS.Core.Entities;
using OMS.Core.IRepositories;
using OMS.Core.IServices;

namespace OMS.Service
{
    public class StockUpdateService : IStockUpdateService
    {
        private readonly IProductRepository _productRepository;

        public StockUpdateService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task UpdateStockAsync(Order order)
        {
            foreach (var item in order.OrderItems)
            {
                var product = await _productRepository.GetProductByIdAsync(item.ProductId);
                if (product is not null)
                {
                    product.Stock -= item.Quantity;
                    await _productRepository.UpdateProductAsync(0,product);
                }
            }
        }
    }
}
