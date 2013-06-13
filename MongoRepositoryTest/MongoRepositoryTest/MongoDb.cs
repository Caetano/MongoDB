using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
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
        public string Telefone { get; set; }
        public string Sobrenome { get; set; }
    }

    public class MongoDb<T> where T : Entity
    {

        public object SingleOrDefault(Expression<Func<T, bool>> expression)
        {
            var result = OpenCollection().Find(Query<T>.Where(expression));
            return result.Count() == 1 ? result : null;
        }

        public IList<T> Where(Expression<Func<T, bool>> expression)
        {
            return OpenCollection().Find(Query<T>.Where(expression)).ToList();
        }

        public T GetById(string Id)
        {
            return OpenCollection().FindOne(Query<T>.EQ(e => e.Id, Id));
        }

        public IList<T> All()
        {
            return OpenCollection().FindAll().ToList();
        }

        public T Insert(T obj)
        {
            OpenCollection().Insert(obj);
            return obj;
        }

        private static MongoCollection<T> OpenCollection()
        {
            return GetDatabase("db").GetCollection<T>(typeof(T).Name);
        }

        private static MongoDatabase GetDatabase(string database)
        {
            return Client().GetServer().GetDatabase(database);
        }

        private static MongoServer GetServer()
        {
            return Client().GetServer();
        }

        private static MongoClient Client(string connectionString)
        {
            return new MongoClient(connectionString);
        }

        private static MongoClient Client()
        {
            return new MongoClient("mongodb://localhost");
        }

    }
}

