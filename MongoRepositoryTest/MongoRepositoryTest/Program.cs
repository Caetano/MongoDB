using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MongoRepositoryTest
{
    public class Program
    {
        private static MongoDb<Person> _db;

        public static void Main(string[] args)
        {
            _db = new MongoDb<Person>();
        }
    }
}
