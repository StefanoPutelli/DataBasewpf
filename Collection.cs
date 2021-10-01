using System;
using System.Collections;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataBasewpf
{
    class Collection
    {
        public static MongoDB.Driver.IMongoCollection<User> collection;
        public Collection(){
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://rootBeer:superMegaSegreta0021@cluster0.cnok0.mongodb.net/dataReggia?retryWrites=true&w=majority");
            var client = new MongoClient(settings);
            var database = client.GetDatabase ("Cluster0");

            try{
                database.CreateCollection("User");
            }catch{
                Console.WriteLine ("database gia creato");
            }
            collection = database.GetCollection<User>("User");
        }
        public void addUser(User user){
            collection.InsertOne(user);
        }

        public MongoDB.Driver.IMongoCollection<User> getCollection()
        {
            return collection;
        }

        public void removeUserByUsername(string username){
            var result = collection.FindOneAndDeleteAsync(
                Builders<User>.Filter.Eq("username", username));
        }
        public void removeUserById(string id){
            var result = collection.FindOneAndDeleteAsync(
                Builders<User>.Filter.Eq("Id", new ObjectId(id)));
        }

        

    }
}