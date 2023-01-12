using CouchDB.Driver;
using CouchDB.Driver.Options;
using InternetShop.Data.Models;

namespace InternetShop.Data.Context
{
    public class InvoiceContext : CouchContext
    {
        public CouchDatabase<Invoice> Invoices { get; set; }
        protected override void OnConfiguring(CouchOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder
                .UseEndpoint("http://localhost:5984/")
                .EnsureDatabaseExists()
                .UseBasicAuthentication("couchdb", "couchdb");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
      
}
