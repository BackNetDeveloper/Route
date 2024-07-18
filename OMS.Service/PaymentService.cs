using OMS.Core.Entities;
using OMS.Core.IServices;
using Stripe;
namespace OMS.Service
{
    public class PaymentService : IPaymentService
    {
        public PaymentService()
        {
        }
        public async Task<bool> HandlePaymentAsync(Order order)
        {
            // Configure Stripe API key
            StripeConfiguration.ApiKey = "sk_test_51N9r7gIokdC7ax9kwx8mJSswrSF1JiJSAlrofJX0ikTaf4YrXI2UHG3dZBoD46oa5H14BAjcewIF1CVc2gK1mE37009iym0XI9";

            try
            {
                // Create a PaymentIntent with Stripe
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(order.TotalAmount * 100), // Amount must be in cents
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" },
                    Metadata = new Dictionary<string, string>
                {
                    { "OrderId", order.OrderId.ToString() },
                    { "CustomerId", order.CustomerId.ToString() }
                }
                };

                var service = new PaymentIntentService();
                var paymentIntent = await service.CreateAsync(options);

                // PaymentIntent successfully created, you can now handle the payment

                // Example: Capture the PaymentIntent
                var captureOptions = new PaymentIntentCaptureOptions
                {
                    AmountToCapture = (long)(order.TotalAmount * 100)
                };

                var capturedIntent = await service.CaptureAsync(paymentIntent.Id, captureOptions);

                // Payment successfully captured
                return true;
            }
            catch (StripeException e)
            {
                // Handle Stripe errors
                Console.WriteLine($"Stripe Error: {e.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
