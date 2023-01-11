
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShop.Data.Models
{
    public class OrderDetail
    {   
        public int Id { get; set; }
        public Order Order { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Product Product { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public int Units { get; set; }

        public OrderDetail() { }

        public OrderDetail(int id, Order order, int orderId, Product product, int productId, int units)
        {
            Id = id;
            Order = order;
            OrderId = orderId;
            Product = product;
            ProductId = productId;
            Units = units;
        }

        public OrderDetail(Order order, int orderId, Product product, int productId, int units)
        {
            Order = order;
            OrderId = orderId;
            Product = product;
            ProductId = productId;
            Units = units;
        }
    }
}
