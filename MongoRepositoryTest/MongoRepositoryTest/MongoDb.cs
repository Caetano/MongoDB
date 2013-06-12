using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoRepository;


namespace MongoRepositoryTest
{
    public class MyEntity : Entity
    {
        public string Name { get; set; }
    }
    public class Person : MyEntity
    {

    }

    public class MongoDb
    {
        public static void Main(string[] args)
        {
            Insert<Person>("Teste");
            var entity = GetByName<Person>("Teste");
        }
        
        public static IList<T> All<T>() where T : Usuario
       {
           var Collection = OpenCollection<T>().FindAll();
           return Collection.ToList();
       }

        public static T GetByName<T>(string name) where T : MyEntity
        {
            var query = Query<T>.EQ(e => e.Name, name);
            var collection = OpenCollection<T>();
            var entity = collection.FindOne(query);
            return entity;
        }

        public static void Insert<T>(string name) where T : MyEntity, new()
        {
            var collection = OpenCollection<T>();
            collection.Insert(new T { Name = name });
        }

        public static MongoCollection<T> OpenCollection<T>() where T : MyEntity
        {
            var client = new MongoClient("mongodb://localhost");
            var server = client.GetServer();
            var database = server.GetDatabase("db");
            return database.GetCollection<T>(typeof(T).Name);
        }

    }
}
