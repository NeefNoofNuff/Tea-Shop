using InternetShop.Data;
using InternetShop.Models;

namespace InternetShop.Services
{
    public static class InvoiceFactory
    {
        public static async Task Create(Order order)
        {
            await using var context = new InvoiceContext();

            await context.Invoices.AddAsync(new Invoice(order));
        }
    }
}
