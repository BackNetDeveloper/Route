﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public Customer Customer { get; set; }
    }
}
