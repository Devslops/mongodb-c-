using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDb
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoClient service = new MongoClient("mongodb://dbadmin:root@localhost:27017/db1");
            
            var database = service.GetDatabase("db1");
            var collection = database.GetCollection<BsonDocument>("resto");

            //FETCH ALL
            List<BsonDocument> documents = collection.Find(new BsonDocument()).ToList();
            foreach(BsonDocument doc in documents)
            {
                Console.WriteLine(doc);
            }

            //CREATE ONE
            var newDocument = new BsonDocument
            {
                { "borough", "c# on fire" },
            };
            collection.InsertOne(newDocument);

            //FETCH ONE 1
            var filter1 = Builders<BsonDocument>.Filter.Eq("borough", "c# on fire");
            var cDocument1 = collection.Find(filter1).First();
            Console.WriteLine("Best 1 : " + cDocument1);

            //UPDATE ONE
            var updateFilter = Builders<BsonDocument>.Filter.Eq("borough", "c# on fire");
            var updateData = Builders<BsonDocument>.Update.Set("borough", "javaaaa");
            collection.UpdateOne(updateFilter, updateData);

            //FETCH ONE 2
            var filter2 = Builders<BsonDocument>.Filter.Eq("borough", "javaaaa");
            var cDocument2 = collection.Find(filter2).First();
            Console.WriteLine("Best 2 " + cDocument2);

        }
    }
}
