using MongoDB.Driver;
using Recipes.Models;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.Services
{
    public class RecipeService
    {
        private readonly IMongoCollection<Recipe> _recipes;
        public RecipeService(IRecipeDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _recipes = database.GetCollection<Recipe>(settings.RecipeCollectionName);

        }

        public List<Recipe> GetAll()
        {
            List<Recipe> employees;
            employees = _recipes.Find(rec => true).ToList();
            return employees;
        }

        public Recipe GetById(string id) =>
            _recipes.Find<Recipe>(rec => rec.Id == id).FirstOrDefault();

        public Recipe Create(Recipe rec)
        {
            _recipes.InsertOne(rec);
            return rec;
        }

        public void Remove(string id) =>
          _recipes.DeleteOne(rec => rec.Id == id);

        public void Update(string id, Recipe recIn) =>
           _recipes.ReplaceOne(rec => rec.Id == id, recIn);
    }
}
