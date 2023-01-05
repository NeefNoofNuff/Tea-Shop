using InternetShop.Models;

namespace InternetShop.Logic.Repository.Interfaces
{
    public interface IInvoiceRepository : IDisposable
    {
        public Task Create(Invoice invoice);
        public Task<Invoice> Get(string id);
    }
}
