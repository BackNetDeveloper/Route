using Microsoft.AspNetCore.Mvc;
using OMS.Core.IRepositories;
using OMS.Core.IServices;
using OMS.Repository.Data;
using OMS.Service;
using RouteTechAhmedAtwanTask.Errors;
using RouteTechAhmedAtwanTask.Helpers;

namespace RouteTechAhmedAtwanTask.Extensions
{
    public static class AddServicesWithDependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped<ICustomerRepository, CustomerRepository>();
            Services.AddScoped<IOrderRepository, OrderRepository>();
            Services.AddScoped<IProductRepository, ProductRepository>();
            Services.AddScoped<IUserRepository, UserRepository>();
            Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            Services.AddScoped<IOrderValidationService, OrderValidationService>();
            Services.AddScoped<IDiscountService, DiscountService>();
            Services.AddScoped<IPaymentService, PaymentService>();
            Services.AddScoped<IStockUpdateService, StockUpdateService>();
            Services.AddScoped<IInvoiceService, InvoiceService>();
            Services.AddScoped<IEmailNotificationService, EmailNotificationService>();
            Services.AddScoped<IOrderService, OrderService>();
            //Services.AddAutoMapper(typeof(MappingProfiles));
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState.Where(M => M.Value.Errors.Count() > 0)
                                                         .SelectMany(M => M.Value.Errors)
                                                         .Select(E => E.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse()                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return Services;
        }
    }
}
