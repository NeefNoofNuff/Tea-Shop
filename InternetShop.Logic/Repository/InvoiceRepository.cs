using InternetShop.Logic.Repository.Interfaces;
using InternetShop.Models;
using System.ComponentModel;
using System.Reflection.Metadata;

namespace InternetShop.Data.Data.Repository
{
    public class InvoiceRepository : IInvoiceRepository, IDisposable
    {
        private readonly InvoiceContext _context;

        private bool disposed = false;
        public InvoiceRepository(InvoiceContext invoiceContext)
        {
            _context = invoiceContext;
        }
        public async Task Create(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
        }
        
        public async Task<Invoice> Get(string id)
        {
            var result = await _context.Invoices.FindAsync(id);
            if (result == null)
            {
                throw new NullReferenceException("Invoice was not found!");
            }
            return result;
        }
        public void Dispose()
        {   
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.DisposeAsync();
                }
                disposed = true;
            }
        }
    }
}
