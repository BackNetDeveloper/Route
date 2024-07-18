using Moq;
using OMS.Core.Entities;
using OMS.Core.IRepositories;
using OMS.Service;

namespace TestProject1
{
    public class OrderValidationServiceTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly OrderValidationService _orderValidationService;

        public OrderValidationServiceTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _orderValidationService = new OrderValidationService(_mockProductRepository.Object);
        }

        [Fact]
        public async Task ValidateOrder_SufficientStock_ReturnsTrue()
        {
            // Arrange
            var order = new Order
            {
                OrderItems = new List<OrderItem>
            {
                new OrderItem { ProductId = 1, Quantity = 2 }
            }
            };

            var product = new Product { ProductId = 1, Stock = 10 };
            _mockProductRepository.Setup(repo => repo.GetProductByIdAsync(1)).ReturnsAsync(product);

            // Act
            var result = await _orderValidationService.ValidateOrderAsync(order);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ValidateOrder_InsufficientStock_ReturnsFalse()
        {
            // Arrange
            var order = new Order
            {
                OrderItems = new List<OrderItem>
            {
                new OrderItem { ProductId = 1, Quantity = 12 }
            }
            };

            var product = new Product { ProductId = 1, Stock = 10 };
            _mockProductRepository.Setup(repo => repo.GetProductByIdAsync(1)).ReturnsAsync(product);

            // Act
            var result = await _orderValidationService.ValidateOrderAsync(order);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ValidateOrder_ProductNotFound_ReturnsFalse()
        {
            // Arrange
            var order = new Order
            {
                OrderItems = new List<OrderItem>
            {
                new OrderItem { ProductId = 1, Quantity = 2 }
            }
            };

            _mockProductRepository.Setup(repo => repo.GetProductByIdAsync(1)).ReturnsAsync((Product)null);

            // Act
            var result = await _orderValidationService.ValidateOrderAsync(order);

            // Assert
            Assert.False(result);
        }
    }
}
