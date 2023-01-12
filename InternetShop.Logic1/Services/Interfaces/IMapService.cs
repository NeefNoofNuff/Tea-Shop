using InternetShop.Data.Models;

namespace InternetShop.Logic.Services.Interfaces
{
    public interface IMapService
    {
        public void Create(Shop item);
        public void Update(Shop item);
        public Shop Get(int? id);
        public IEnumerable<Shop> GetAll();
        public void Delete(int? id);
    }
}
