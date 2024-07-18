using OMS.Core.Entities;
using OMS.Core.IRepositories;
using OMS.Core.IServices;

namespace OMS.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderValidationService _orderValidationService;
        private readonly IDiscountService _discountService;
        private readonly IPaymentService _paymentService;
        private readonly IStockUpdateService _stockUpdateService;
        private readonly IInvoiceService _invoiceService;
        private readonly IEmailNotificationService _emailNotificationService;

        public OrderService(
            IOrderRepository orderRepository,
            IOrderValidationService orderValidationService,
            IDiscountService discountService,
            IPaymentService paymentService,
            IStockUpdateService stockUpdateService,
            IInvoiceService invoiceService,
            IEmailNotificationService emailNotificationService)
        {
            _orderRepository = orderRepository;
            _orderValidationService = orderValidationService;
            _discountService = discountService;
            _paymentService = paymentService;
            _stockUpdateService = stockUpdateService;
            _invoiceService = invoiceService;
            _emailNotificationService = emailNotificationService;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            // Validate the order
            if (!await _orderValidationService.ValidateOrderAsync(order))
            {
                throw new InvalidOperationException("Order validation failed.");
            }

            // Apply discounts
            order.TotalAmount = _discountService.ApplyDiscounts(order.TotalAmount);

            // Handle payment
            if (!await _paymentService.HandlePaymentAsync(order))
            {
                throw new InvalidOperationException("Payment failed.");
            }

            // Update stock
            await _stockUpdateService.UpdateStockAsync(order);

            // Save the order
            await _orderRepository.CreateOrderAsync(order);

            // Generate invoice
            var invoice = await _invoiceService.GenerateInvoiceAsync(order);

            // Send order status change email
            await _emailNotificationService.SendOrderStatusChangeEmailAsync(order);

            return order;
        }

        public async Task<Order> GetOrderAsync(int orderId)
        {
            return await _orderRepository.GetOrderAsync(orderId);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task<Order> UpdateOrderStatusAsync(int orderId, string status)
        {
            await _orderRepository.UpdateOrderStatusAsync(orderId, status);
            var order = await _orderRepository.GetOrderAsync(orderId);
            await _emailNotificationService.SendOrderStatusChangeEmailAsync(order);
            return order;
        }

    }
    }
