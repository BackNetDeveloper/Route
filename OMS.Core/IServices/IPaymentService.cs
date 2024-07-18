using OMS.Core.Entities;

namespace OMS.Core.IServices
{
    public interface IPaymentService
    {
        Task<bool> HandlePaymentAsync(Order order);
    }
}
