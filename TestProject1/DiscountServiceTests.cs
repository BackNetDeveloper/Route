using OMS.Service;

namespace RouteTechAhmedAtwanTask
{
    public class DiscountServiceTests
    {
        private readonly DiscountService _discountService;

        public DiscountServiceTests()
        {
            _discountService = new DiscountService();
        }

        [Theory]
        [InlineData(50, 50)]
        [InlineData(150, 142.5)]
        [InlineData(250, 225)]
        public void ApplyDiscount_AppliesCorrectDiscount(double totalAmount, double expectedAmount)
        {
            // Act
            var result = _discountService.ApplyDiscounts(totalAmount);

            // Assert
            Assert.Equal(expectedAmount, result);
        }
    }
}
