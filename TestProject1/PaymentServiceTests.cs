using OMS.Core.Entities;
using OMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public class PaymentServiceTests
    {
        private readonly PaymentService _paymentService;

        public PaymentServiceTests()
        {
            _paymentService = new PaymentService();
        }

        [Fact]
        public void ProcessPayment_ValidPaymentMethod_ReturnsTrue()
        {
            // Arrange
            var order = new Order { TotalAmount = 100, PaymentMethod = "Credit Card" };

            // Act
            var result = _paymentService.HandlePaymentAsync(order);

            // Assert
            Assert.True(result.Result);
        }

        [Fact]
        public void ProcessPayment_InvalidPaymentMethod_ReturnsFalse()
        {
            // Arrange
            var order = new Order { TotalAmount = 100, PaymentMethod = "Unknown" };

            // Act
            var result = _paymentService.HandlePaymentAsync(order);

            // Assert
            Assert.False(result.Result);
        }
    }
}
