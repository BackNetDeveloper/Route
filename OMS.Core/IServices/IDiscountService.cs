using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.IServices
{
    public interface IDiscountService
    {
        double ApplyDiscounts(double totalAmount);
    }
}
