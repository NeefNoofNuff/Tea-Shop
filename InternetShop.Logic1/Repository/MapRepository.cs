using InternetShop.Data.Context;
using InternetShop.Data.Models;
using InternetShop.Logic.Repository.Interfaces;
using Neo4j.Driver;
using System.Text.Json;

namespace InternetShop.Logic.Repository
{
    public class MapRepository : IMapRepository, IDisposable
    {
        private readonly MapContext _mapContext;
        private bool _disposed = false;
        ~MapRepository() => Dispose(false);
        public MapRepository(MapContext mapContext)
        {
            _mapContext = mapContext;
        }
        public Shop Get(int? id)
        {
            try
            {
                return _mapContext.Find(id);
            }
            catch (ServiceUnavailableException)
            {
                throw new ServiceUnavailableException("Neo4j is not connected!");
            }
            catch (JsonException)
            {
                throw new JsonException("Json was not serialized!");
            }
        }

        public void Create(Shop shop)
        {
            try
            {
                var location = _mapContext.Find(shop.Id);
            }
            catch (ServiceUnavailableException)
            {
                throw new ServiceUnavailableException("Neo4j is not connected!");
            }
            catch (JsonException)
            {
                throw new JsonException("Json was not serialized!");
            }
            catch (NullReferenceException)
            {
                _mapContext.Add(shop);
            }
        }

        public void Delete(int? id)
        { 
            try
            {
                var location = _mapContext.Find(id);
            }
            catch (ServiceUnavailableException)
            {
                throw new ServiceUnavailableException("Neo4j is not connected!");
            }
            catch (JsonException)
            {
                throw new JsonException("Json was not serialized!");
            }
            catch (NullReferenceException)
            {
                _mapContext.Remove(id);
            }
        }

        public void Update(Shop shop)
        {
            try
            {
                var location = _mapContext.Find(shop.Id);
                _mapContext.Update(shop);
            }
            catch (ServiceUnavailableException)
            {
                throw new ServiceUnavailableException("Neo4j is not connected!");
            }
            catch (JsonException)
            {
                throw new JsonException("Json was not serialized!");
            }
        }

        public List<Shop> GetAll()
        {
            try
            {
                return _mapContext.GetAll();
            }
            catch (ServiceUnavailableException)
            {
                throw new ServiceUnavailableException("Neo4j is not connected!");
            }
            catch (JsonException)
            {
                throw new JsonException("Json was not serialized!");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing) _mapContext?.Dispose();

            _disposed = true;
        }
    }
}
