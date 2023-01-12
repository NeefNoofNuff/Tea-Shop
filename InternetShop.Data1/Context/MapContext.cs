using Neo4j.Driver;
using Newtonsoft.Json;
using InternetShop.Data.Models;

namespace InternetShop.Data.Context
{
    public class MapContext : IDisposable
    {
        private bool _disposed = false;
        private readonly IDriver driver;

        public bool Created { get; set; } = false;
        ~MapContext() => Dispose(false);

        private readonly List<Shop> Shops = new()
        {
            new Shop(0, "Peremohy Ave, 1, Kyiv", "Mon-Fr 10:00 - 21:00, Sat-Sun 11:00-19:00"),
            new Shop(1, "Volodymyrska St, 60, Kyiv", "Mon-Fr 10:00 - 21:00, Sat-Sun 11:00-19:00"),
            new Shop(2, "Pushkinska St, 20, Kyiv", "Mon-Fr 10:00 - 21:00, Sat-Sun 11:00-19:00"),
            new Shop(3, "Budivelnykiv St, 40, Kyiv", "Mon-Fr 10:00 - 21:00, Sat-Sun 11:00-19:00"),
            new Shop(4, "Velyka Vasylkivska St, 5, Kyiv", "Mon-Fr 10:00 - 21:00, Sat-Sun 11:00-19:00"),
            new Shop(5, "Politekhnichna St, 31, Kyiv", "Mon-Fr 10:00 - 20:00, Sat-Sun 11:00-19:00"),
            new Shop(6, "Andriivska St, 8-9, Kyiv", "Mon-Fr 10:00 - 20:00, Sat-Sun 11:00-18:00")
        };

        public MapContext(string uri = "bolt://localhost:7687", string user = "neo4j", string password = "map4nnn")
        {
            driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));

            Created = true;

            if (CountEntities() == 0)
                CreateGraph();
        }


        public Shop Find(int? id)
        {
            var shops = new List<Shop>();

            try
            {
                using var session = driver.Session();
                var entities = session.WriteTransaction(x =>
                {
                    var result = x.Run("MATCH (s:Shop) " +
                                        $"WHERE s.id = '{id}' " +
                                        "RETURN s"
                                    );
                    foreach (var record in result)
                    {
                        var nodeProps = JsonConvert.SerializeObject(record[0].As<INode>().Properties);
                        if (nodeProps == null)
                        {
                            throw new JsonException("Json serialize error!");
                        }
                        shops.Add(JsonConvert.DeserializeObject<Shop>(nodeProps));
                    }
                    return shops;
                });
                if (!entities.Any()) 
                    throw new NullReferenceException("Mao location was not found!");
                else return entities.First();
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
        public int CountEntities()
        {
            if (!Created) return 0;
            else return GetAll().Count;
        }
        public void CreateGraph()
        {
            try
            {
                using var session = driver.Session();
                for (int i = 0; i < Shops.Count; i++)
                {
                    var message = session.WriteTransaction(x =>
                    {
                        var result = x.Run("CREATE (s:Shop) " +
                                            $"SET s.id = '{Shops[i].Id}'" +
                                            $"SET s.address = '{Shops[i].Address}'" +
                                            $"SET s.hours = '{Shops[i].Hours}'" +
                                            $"RETURN 'Created node {Shops[i].Id}'"
                            );
                        return result.Single()[0].As<string>();
                    });
                    Console.WriteLine(message);
                }
            }
            catch (ServiceUnavailableException)
            {
                throw new ServiceUnavailableException("Neo4j is not connected!");
            }
        }
        public void Add(Shop shop)
        {
            try
            {
                using var session = driver.Session();
                var message = session.WriteTransaction(x =>
                {
                    var result = x.Run("CREATE (s:Shop) " +
                                        $"SET s.id = '{shop.Id}'" +
                                        $"SET s.address = '{shop.Address}'" +
                                        $"SET s.hours = '{shop.Hours}'" +
                                        $"RETURN 'Created node {shop.Id}'"
                        );
                    return result.Single()[0].As<string>();
                });
                Console.WriteLine(message);
            }
            catch (ServiceUnavailableException)
            {
                throw new ServiceUnavailableException("Neo4j is not connected!");
            }
        }
        public void Remove(int? id)
        {
            try
            {
                using var session = driver.Session();
                var message = session.WriteTransaction(x =>
                {
                    var result = x.Run("MATCH (s:Shop) " +
                                        $"WHERE s.id = '{id}' " +
                                        "OPTIONAL MATCH (s)-[r]-() " +
                                        "DELETE r,s " +
                                        $"RETURN 'Deleted node {id}'"
                        );
                    return result.Single()[0].As<string>();
                });
                Console.WriteLine(message);
            }
            catch (ServiceUnavailableException)
            {
                throw new ServiceUnavailableException("Neo4j is not connected!");
            }
        }
        public void Update(Shop shop)
        {
            using var session = driver.Session();
            var message = session.WriteTransaction(x =>
            {
                var result = x.Run("MATCH (s:Shop) " +
                                    $"WHERE s.id = '{shop.Id}' " +
                                    $"SET s.address = '{shop.Address}' " +
                                    $"SET s.hours = '{shop.Hours}' " +
                                    $"RETURN 'Updated node {shop.Id}'"
                    );
                return result.Single()[0].As<string>();
            });
            Console.WriteLine(message);
        }

        public List<Shop> GetAll()
        {
            using var session = driver.Session();
            var shops = new List<Shop>();
            try
            {
                var creation = session.ReadTransaction(x =>
                {
                    var result = x.Run("MATCH (s:Shop) RETURN s");

                    foreach (var record in result)
                    {
                        var nodeProps = JsonConvert.SerializeObject(record[0].As<INode>().Properties);
                        if(nodeProps == null)
                        {
                            throw new JsonException("Json serialize error!");
                        }
                        shops.Add(JsonConvert.DeserializeObject<Shop>(nodeProps));
                    }

                    return shops;
                });
                return creation;
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

            if (disposing) driver?.Dispose();

            _disposed = true;
        }
    }
}
