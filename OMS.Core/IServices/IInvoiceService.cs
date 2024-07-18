﻿using OMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Core.IServices
{
    public interface IInvoiceService
    {
        Task<Invoice> GenerateInvoiceAsync(Order order);
    }
}