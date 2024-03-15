using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace TriviaApp3.DataAccessLayer
{
    internal class DB
    {
        private static MongoClient GetClient()
        {
            string connectionString = "mongodb+srv://grasmus95:Qbi1gl7DbiVH8K7J@cluster0.g1t6x5q.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";

            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            var mongoClient = new MongoClient(settings);

            return mongoClient;
        }

        public static IMongoCollection<Models.UserData> GetUserDataCollection(string userCollection)
        {
            var client = GetClient();

            var database = client.GetDatabase("UserDataDb");

            var userDataCollection = database.GetCollection<Models.UserData>(userCollection);

            return userDataCollection;
        }

        public static IMongoCollection<Models.User> GetUserCollection()
        {
            var client = GetClient();

            var database = client.GetDatabase("UserDb");

            var userCollection = database.GetCollection<Models.User>("Users");

            return userCollection;
        }
    }
}
