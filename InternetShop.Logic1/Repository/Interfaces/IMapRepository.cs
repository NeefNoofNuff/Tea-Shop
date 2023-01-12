using InternetShop.Data.Models;

namespace InternetShop.Logic.Repository.Interfaces
{
    public interface IMapRepository
    {
        public Shop Get(int? id);
        public void Create(Shop shop);
        public void Delete(int? id);
        public void Update(Shop shop);
        public List<Shop> GetAll();
    }
}
