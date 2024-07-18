using OMS.Core.Entities;
using OMS.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Service
{
    public class EmailNotificationService : IEmailNotificationService
    {
        public Task SendOrderStatusChangeEmailAsync(Order order)
        {
            // Implement email sending logic here, e.g., using an SMTP client or an email service provider API.
            return Task.CompletedTask;
        }
    }
}
