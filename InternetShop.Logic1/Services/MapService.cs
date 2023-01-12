using InternetShop.Data.Models;
using InternetShop.Logic.Repository.Interfaces;
using InternetShop.Logic.Services.Interfaces;

namespace InternetShop.Logic.Services
{
    public class MapService : IMapService
    {   
        private readonly IMapRepository _mapRepository;

        public MapService(IMapRepository mapRepository)
        {
            _mapRepository = mapRepository;
        }

        public void Create(Shop item)
        {
            try
            {
                if (item == null)
                    throw new NullReferenceException("Shop is null!");
                _mapRepository.Create(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void Delete(int? id)
        {
            try
            {
                if (id == null)
                    throw new NullReferenceException("Null in delete shop!");
                _mapRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }          
        }

        public Shop Get(int? id)
        {
            try
            {
                if (id == null)
                    throw new NullReferenceException("Id is null in get shop!");
                return _mapRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public IEnumerable<Shop> GetAll()
        {
            try
            {
                return _mapRepository.GetAll().OrderBy(shop => shop.Id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void Update(Shop item)
        {
            try
            {
                if (item == null)
                    return;
                _mapRepository.Update(item);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
