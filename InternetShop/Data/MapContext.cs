using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using InternetShop.Models;
using Microsoft.CodeAnalysis;
using Neo4j.Driver;
using Newtonsoft.Json;

namespace InternetShop.Data
{
    public class MapContext : IDisposable
    {
        private bool _disposed = false;
        private readonly IDriver driver;

        public bool Created { get; set; } = false;
        ~MapContext() => Dispose(false);

        public List<Shop> Shops = new()
        {
            new Shop("some address1", "working always"),
            new Shop("some address2", "working always"),
            new Shop("some address3", "working always"),
            new Shop("some address4", "maworking alwaysn"),
            new Shop("some address5", "working always"),
            new Shop("some address6", "man"),
            new Shop("some address7", "working always")
        };

        public MapContext(string uri = "bolt://localhost:7687", string user = "neo4j", string password = "map4nnn")
        {
            driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));

            Created = true;

            if (CountEntities() == 0)
                CreateGraph();
        }

        public int CountEntities()
        {
            if (!Created) return 0;
            else return GetAll().Count;
        }
        public void CreateGraph()
        {
            using (var session = driver.Session())
            {
                for (int i = 0; i < Shops.Count; i++) 
                {
                    var message = session.WriteTransaction(x =>
                    {
                        var result = x.Run("CREATE (s:Shop) " +
                                            $"SET s.address = '{Shops[i].Address}'" +
                                            $"SET s.hours = '{Shops[i].Hours}'" +
                                            "RETURN 'Created node' + id(s)"
                            );
                        return result.Single()[0].As<string>();
                    });
                    Console.WriteLine(message);
                }
            }
        }
        public void AddShop(Shop shop)
        {
            using (var session = driver.Session())
            {
                var message = session.WriteTransaction(x =>
                {
                    var result = x.Run("CREATE (s:Shop) " +
                                        $"SET s.address = '{shop.Address}'" +
                                        $"SET s.hours = '{shop.Hours}'" +
                                        "RETURN 'Created node' + id(s)"
                        );
                    return result.Single()[0].As<string>();
                });
                Console.WriteLine(message);
            }
        }
        public void RemoveShop(string address)
        {
            using (var session = driver.Session())
            {
                var message = session.WriteTransaction(x =>
                {
                    var result = x.Run("MATCH (s:Shop) " +
                                        $"WHERE s.address = {address}" +
                                        "OPTIONAL MATCH (s)-[r]-()" +
                                        "DELETE r,p" +
                                        "RETURN 'Deleted node' + id(s)"
                        );
                    return result.Single()[0].As<string>();
                });
                Console.WriteLine(message);
            }
            //Shops.RemoveAll(x => x.Address == address);
        }
        public void EditShopsAddress(KeyValuePair<string, string> shopInfo)
        {
            using (var session = driver.Session())
            {
                var message = session.WriteTransaction(x =>
                {
                    var result = x.Run("MATCH (s:Shop) " +
                                        $"WHERE s.hours = '{shopInfo.Value}s.address = {shopInfo.Key}" +
                                        $"SET s.address = {shopInfo.Key}'" +
                                        "RETURN 'Updated node' + id(s)"
                        );
                    return result.Single()[0].As<string>();
                });
                Console.WriteLine(message);
            }
        }
        public void EditShopsWorkingHours(KeyValuePair<string, string> shopInfo)
        {
            using (var session = driver.Session())
            {
                var message = session.WriteTransaction(x =>
                {
                    var result = x.Run("MATCH (s:Shop) " +
                                        $"WHERE s.address = {shopInfo.Key}" +
                                        $"SET s.hours = '{shopInfo.Value}'" +
                                        "RETURN 'Updated node' + id(s)"
                        );
                    return result.Single()[0].As<string>();
                });
                Console.WriteLine(message);
            }
        }

        public List<Shop> GetAll()
        {
            using (var session = driver.Session())
            {
                var shops = new List<Shop>();

                var creation = session.ReadTransaction(x =>
                {
                    var result = x.Run("MATCH (s:Shop) RETURN s");
                    foreach (var record in result)
                    {
                        var nodeProps = JsonConvert.SerializeObject(record[0].As<INode>().Properties);
                        shops.Add(JsonConvert.DeserializeObject<Shop>(nodeProps));
                    }
                    return shops;
                });
                return creation;
            }
        }

        /*public void Create_Relation(string name1, string name2)
        {
            using (var session = driver.Session())
            {
                var creation = session.WriteTransaction(x =>
                {
                    var result = x.Run($"MATCH (p1: Man), (p2: Woman) " +
                                        $"WHERE p1.name = '{name1}' AND p2.name = '{name2}'" +
                                        "create (p1)-[r:friends]->(p2)" +
                                        "RETURN p1.name + ' & '+ p2.name + ' are friends.'"
                        );
                    return result.Single()[0].As<string>();
                });
                Console.WriteLine(creation);
            }
        }

        public Dictionary<string, int> Test_Length()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            using (var session = driver.Session())
            {
                for (int i = 7; i < 11; i++)
                {
                    for (int j = i + 1; j < 12; j++)
                    {
                        var length = session.WriteTransaction(x =>
                        {
                            var result = x.Run("MATCH (start:Woman {" +
                                                $"name:'{Shops[i].Key}'" +
                                                "}), (end: Woman {" +
                                                $"name:'{Shops[j].Key}'" +
                                                "}), p = shortestPath((start)-[:friends*]-(end))" +
                                                "RETURN length(p); ");
                            return result.Single()[0].As<string>();
                        }
                        );
                        string message = $"Woman {Shops[i].Key} and Woman {Shops[j].Key}";
                        result.TryAdd(message, int.Parse(length));
                    }
                }

                for (int i = 0; i < 6; i++)
                {
                    for (int j = i + 1; j <= 6; j++)
                    {
                        var length = session.WriteTransaction(tx =>
                        {
                            var result = tx.Run("MATCH (start:Man {" +
                                                $"name:'{Shops[i].Key}'" +
                                                "}),(end:Man {" +
                                                $"name: '{Shops[j].Key}'" +
                                                "}), p = shortestPath((start)-[:friends*]-(end))" +
                                                " RETURN length(p); ");
                            return result.Single()[0].As<string>();
                        }
                        );
                        string message = $"Man {Shops[i].Key} and Man {Shops[j].Key}";
                        result.TryAdd(message, int.Parse(length));
                    }
                }
            }
            return result;
        }*/
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
