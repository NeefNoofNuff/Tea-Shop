using CouchDB.Driver;
using CouchDB.Driver.Options;
using InternetShop.Models;

namespace InternetShop.Data
{
    public class InvoiceContext : CouchContext
    {
        public CouchDatabase<Invoice> Invoices { get; set; }
        protected override void OnConfiguring(CouchOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseEndpoint("http://localhost:5984/")
                .EnsureDatabaseExists()
                .UseBasicAuthentication("couchdb", "couchdb");
        }
    }
      
}
