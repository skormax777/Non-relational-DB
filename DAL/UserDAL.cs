using Entity;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class UserDAL
    {
        IMongoDatabase db;
        IMongoCollection<User> collection;

        public UserDAL()
        {
            db = ConfigurationManager.GetDefaultDatabase();
            collection = db.GetCollection<User>(GetTableName());
        }

        private string GetTableName()
        {
            return "user";
        }

        // Insert
        public void InsertUser(User user) =>
           collection.InsertOne(user);
        public void InsertUsers(IEnumerable<User> users) =>
            collection.InsertMany(users);

        // Update
        public void UpdateUser(ObjectId id, User user) =>
            collection.ReplaceOne(u => u.Id == id, user);
        public void UpdateByLogin(string login, User user) =>
            collection.ReplaceOne(u => u.Login == login, user);
        public void UpdateByField(string login, string fieldName, string value)
        {
            var filter = Builders<User>.Filter.Eq("Login", login);
            var update = Builders<User>.Update.Set(fieldName, value);
            collection.UpdateOne(filter, update);
        }
        public void UpdateByDateTime(string login)
        {
            var filter = Builders<User>.Filter.Eq("Login", login);
            var update = Builders<User>.Update.Set("LastLogin", DateTime.Now.ToString());
            collection.UpdateOne(filter, update);
        }

        // Select
        public List<User> SelectAllUsers() =>
            collection.Find(u => true).ToList();
        public User SelectById(ObjectId id) =>
         collection.Find(u => u.Id == id).FirstOrDefault();
        public User SelectByLogin(string login) =>
            collection.Find(u => u.Login == login).FirstOrDefault();

        // Delete
        public void DeleteUser(User user) =>
             collection.DeleteOne(u => u.Id == user.Id);
        public void DeleteById(ObjectId userId) =>
            collection.DeleteOne(u => u.Id == userId);
        public void DeleteByNickname(string login) =>
            collection.DeleteOne(u => u.Login == login);


        public void AddFollowers(string login, string newFollower)
        {
            var filter = Builders<User>.Filter.Eq("Login", login);
            var update = Builders<User>.Update.Push("Followers", newFollower);
            collection.UpdateOne(filter, update);

        }
        public void Unfollow(string login, string follower)
        {
            var filter = Builders<User>.Filter.Eq("Login", login);
            var update = Builders<User>.Update.Pull("Following", follower);
            collection.UpdateOne(filter, update);

            filter = Builders<User>.Filter.Eq("Login", follower);
            update = Builders<User>.Update.Pull("Followers", login);
            collection.UpdateOne(filter, update);
        }
        public void AddFollowing(string login, string newFollowing)
        {
            var filter = Builders<User>.Filter.Eq("Login", login);
            var update = Builders<User>.Update.Push("Following", newFollowing);
            collection.UpdateOne(filter, update);

        }
        public List<string> GetFollowers(string login)
        {
            var filter = Builders<User>.Filter.Eq("Login", login);
            var followers = collection.Find(filter).Project(x => x.Followers).First();
            return followers;
        }
        public List<string> GetFollowing(string login)
        {
            var filter = Builders<User>.Filter.Eq("Login", login);
            var following = collection.Find(filter).Project(x => x.Following).First();
            return following;
        }
        public ObjectId GetUserId(string login)
        {
            var user = collection.Find(u => u.Login == login).FirstOrDefault();
            return user.Id;
        }
    }
}
