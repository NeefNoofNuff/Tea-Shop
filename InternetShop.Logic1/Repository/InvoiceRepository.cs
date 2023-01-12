using InternetShop.Logic.Repository.Interfaces;
using InternetShop.Data.Context;
using InternetShop.Data.Models;
using CouchDB.Driver.Exceptions;

namespace InternetShop.Logic.Repository
{
    public class InvoiceRepository : IInvoiceRepository, IDisposable
    {
        private readonly InvoiceContext _context;

        private bool disposed = false;

        ~InvoiceRepository() => Dispose(false);
        public InvoiceRepository(InvoiceContext invoiceContext)
        {
            _context = invoiceContext;
        }
        public async Task Create(Invoice invoice)
        {
            try
            {
                await _context.Invoices.AddAsync(invoice);
            }
            catch (Exception ex)
            {
                throw new Exception("Database is not active. " + ex.Message);
            }
        }

        public async Task<Invoice> Get(string id)
        {
            try
            {
                var result = await _context.Invoices.FindAsync(id);
                if (result == null)
                {
                    throw new NullReferenceException("Invoice was not found!");
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
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
