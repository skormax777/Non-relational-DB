using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using Entity;

namespace DAL
{
    public class PostDAL
    {
        IMongoDatabase db;
        IMongoCollection<Post> collection;

        public PostDAL()
        {
            db = ConfigurationManager.GetDefaultDatabase();
            collection = db.GetCollection<Post>(GetTableName());
        }

        private string GetTableName()
        {
            return "post";
        }



        //--------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------


        // Insert-------------------------------------------------------------------------
        public void InsertPost(Post post) =>
          collection.InsertOne(post);
        public void InsertPosts(IEnumerable<Post> posts) =>
            collection.InsertMany(posts);



        //--------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------


        // Update-------------------------------------------------------------------------
        public void UpdatePost(ObjectId postId, Post post) =>
            collection.ReplaceOne(p => p.Id == postId, post);
        public void UpdateByText(ObjectId postId, string newText)
        {
            var filter = Builders<Post>.Filter.Eq("_id", postId);
            var update = Builders<Post>.Update.Set("Text", newText);
            collection.UpdateOne(filter, update);
        }



        //--------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------


        // Select-------------------------------------------------------------------------
        public List<Post> SelectAllPosts(List<ObjectId> following)
        {
            var filter = Builders<Post>.Filter.In("OwnerId", following);
            var posts = collection.Find(filter).ToList();
            return posts;
        }                  
        public List<Post> SelectAllPosts(ObjectId OwnerId) =>
            collection.Find(p => p.OwnerId == OwnerId).ToList();
        public Post SelectById(ObjectId id) =>
          collection.Find(p => p.Id == id).FirstOrDefault();
        public List<Post> SelectNewPosts(string TimeOfLastUserLogin, List<ObjectId> following)
        {
            var filter = Builders<Post>.Filter.Gte("DateTime", TimeOfLastUserLogin);
            filter = filter & Builders<Post>.Filter.In("OwnerId", following);
            var posts = collection.Find(filter).ToList();
            return posts;
        }


        //--------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------


        // Delete-------------------------------------------------------------------------
        public void DeletePost(Post post) =>
             collection.DeleteOne(p => p.Id == post.Id);
        public void DeleteById(ObjectId postId) =>
           collection.DeleteOne(p => p.Id == postId);


        //--------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------


        public void AddLike(string likerLogin, ObjectId postId)
        {
            var filter = Builders<Post>.Filter.Eq("_id", postId);
            var update = Builders<Post>.Update.Inc("Likes", 1);
            collection.UpdateOne(filter, update);

            update = Builders<Post>.Update.Push("WhoLiked", likerLogin);
            collection.UpdateOne(filter, update);
        }
        public void DismissLike(string likerLogin, ObjectId postId)
        {
            var filter = Builders<Post>.Filter.Eq("_id", postId);
            var update = Builders<Post>.Update.Inc("Likes", -1);
            collection.UpdateOne(filter, update);

            update = Builders<Post>.Update.Pull("WhoLiked", likerLogin);
            collection.UpdateOne(filter, update);
        }


        public void AddComment(Comment comment, ObjectId postId)
        {
            var filter = Builders<Post>.Filter.Eq("_id", postId);
            var update = Builders<Post>.Update.Push("Comments", comment);
            collection.UpdateOne(filter, update);
        }
        public void DeleteComment(Comment comment, ObjectId postId)
        {
            var filter = Builders<Post>.Filter.Eq("_id", postId);
            var update = Builders<Post>.Update.Pull("Comments", comment);
            collection.UpdateOne(filter, update);
        }


        public List<string> GetWhoLiked(ObjectId postId)
        {
            var filter = Builders<Post>.Filter.Eq("_id", postId);
            var people = collection.Find(filter).Project(x => x.WhoLiked).First();
            return people;

        }
        public int GetNumOfLikes(ObjectId postId)
        {
            var filter = Builders<Post>.Filter.Eq("_id", postId);
            var likes = collection.Find(filter).Project(x => x.Likes).First();
            return likes;
        }


        public List<Comment> GetComments(ObjectId postId)
        {
            var filter = Builders<Post>.Filter.Eq("_id", postId);
            var comments = collection.Find(filter).Project(x => x.Comments).First();
            return comments;
        }
    }
}
