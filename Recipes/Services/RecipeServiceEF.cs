using Recipes.Data;
using Recipes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Services
{
    public class RecipeServiceEF : IRecipeService
    {
        readonly  ApplicationContext _context;

        public RecipeServiceEF(ApplicationContext context)
        {
            _context = context;
        }

        public RecipeModel Create(RecipeModel rec)
        {
            _context.Add(rec);
            _context.SaveChanges();
            return rec;
        }

        public List<RecipeModel> GetAll()
        {
            return _context.Recipes.ToList();
        }

        public RecipeModel GetById(int id)
        {
            return _context.Recipes.FirstOrDefault(x=> x.Id == id);
        }

        public void Remove(int id)
        {
            var recipeToRemove = this.GetById(id);
            if (recipeToRemove!=null)
            {
                _context.Recipes.Remove(recipeToRemove);
                _context.SaveChanges();
            }
            
        }

        public void Update(int id, RecipeModel recIn)
        {
            var recipeToUpdate = this.GetById(id);
            if (recipeToUpdate!=null)
            {
                recipeToUpdate.Ingredients = recIn.Ingredients;
                recipeToUpdate.Description = recIn.Description;
                recipeToUpdate.Directions = recIn.Directions;
                recipeToUpdate.Title = recIn.Title;
                recipeToUpdate.Updated = DateTime.Now;
                _context.Update(recipeToUpdate);
                _context.SaveChanges();
            }
            
        }
    }
}
