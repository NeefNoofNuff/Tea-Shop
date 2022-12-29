using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using InternetShop.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Neo4j.Driver;
using Newtonsoft.Json;
using static System.Formats.Asn1.AsnWriter;

namespace InternetShop.Data
{
    public class MapContext : IDisposable
    {
        private bool _disposed = false;
        private readonly IDriver driver;

        public bool Created { get; set; } = false;
        ~MapContext() => Dispose(false);

        private List<Shop> Shops = new()
        {
            new Shop(0, "some address1", "working always"),
            new Shop(1, "some address2", "working always"),
            new Shop(2, "some address3", "working always"),
            new Shop(3, "some address4", "maworking alwaysn"),
            new Shop(4, "some address5", "working always"),
            new Shop(5, "some address6", "man"),
            new Shop(6, "some address7", "working always")
        };

        public MapContext(string uri = "bolt://localhost:7687", string user = "neo4j", string password = "map4nnn")
        {
            driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));

            Created = true;

            if (CountEntities() == 0)
                CreateGraph();
        }

        public bool Exists(Shop shop) => Find(shop.Id) == null ? false : true;
        public bool Exists(int? id) => Find(id) == null ? false : true;

        public Shop Find(int? id)
        {
            using (var session = driver.Session())
            {
                var entity = session.WriteTransaction(x =>
                {
                    var result = x.Run("MATCH (s:Shop) " +
                                        $"WHERE s.id = {id}" +
                                        "RETURN s"
                        );
                    return result.Single()[0];
                });

                if (entity.GetType() == typeof(Shop)) return (Shop)entity;

                else return null;
            }
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
        }
        public void Add(Shop shop)
        {
            if (Exists(shop)) return;

            using (var session = driver.Session())
            {
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
        }
        public void Remove(int? id)
        {
            if(!Exists(id))
            using (var session = driver.Session())
            {
                var message = session.WriteTransaction(x =>
                {
                    var result = x.Run("MATCH (s:Shop) " +
                                        $"WHERE s.id = {id}" +
                                        "OPTIONAL MATCH (s)-[r]-()" +
                                        "DELETE r,p" +
                                        $"RETURN 'Deleted node {id}'"
                        );
                    return result.Single()[0].As<string>();
                });
                Console.WriteLine(message);
            }
        }
        public void Update(Shop shop)
        {
            using (var session = driver.Session())
            {
                var message = session.WriteTransaction(x =>
                {
                    var result = x.Run("MATCH (s:Shop) " +
                                        $"WHERE s.id == {shop.Id}" +
                                        $"SET s.address = {shop.Address}'" +
                                        $"SET s.hours = {shop.Hours}'" +
                                        $"RETURN 'Updated node {shop.Id}'"
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
