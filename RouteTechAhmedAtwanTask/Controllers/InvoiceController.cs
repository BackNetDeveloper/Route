using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.Core.IRepositories;
using RouteTechAhmedAtwanTask.Consts;
using Swashbuckle.AspNetCore.Annotations;
namespace RouteTechAhmedAtwanTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet("{invoiceId}")]
        [Authorize(Policy = SystemPolicy.AdminPolicy)]
        [SwaggerOperation(Summary = "Get details of a specific invoice (admin only)")]
        [SwaggerResponse(200, "Returns the invoice details")]
        [SwaggerResponse(404, "Invoice not found")]
        public async Task<IActionResult> GetInvoiceById(int invoiceId)
        {
            var invoice = await _invoiceRepository.GetInvoiceByIdAsync(invoiceId);
            if (invoice == null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }

        [HttpGet]
        [Authorize(Policy = SystemPolicy.AdminPolicy)]
        [SwaggerOperation(Summary = "Get all invoices (admin only)")]
        [SwaggerResponse(200, "Returns the list of invoices")]
        public async Task<IActionResult> GetAllInvoices()
        {
            var invoices = await _invoiceRepository.GetAllInvoicesAsync();
            return Ok(invoices);
        }
    }
}
