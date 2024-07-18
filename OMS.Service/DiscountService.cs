using OMS.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Service
{
    public class DiscountService: IDiscountService
    {
        public double ApplyDiscounts(double totalAmount)
        {
            if (totalAmount > 200)
            {
                return totalAmount * 0.9; // 10% off
            }
            else if (totalAmount > 100)
            {
                return totalAmount * 0.95; // 5% off
            }
            return totalAmount;
        }
    }
}
