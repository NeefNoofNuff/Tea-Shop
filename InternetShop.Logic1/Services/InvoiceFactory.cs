using InternetShop.Logic.Repository.Interfaces;
using InternetShop.Data.Models;

namespace InternetShop.Logic.Services
{
    public class InvoiceFactory
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public InvoiceFactory(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        public async Task Create(Order order)
        {
            try
            {
                using var context = _invoiceRepository;

                await context.Create(new Invoice(order));
            }
            catch (Exception)
            { 
                throw;
            }
        }
    }
}
