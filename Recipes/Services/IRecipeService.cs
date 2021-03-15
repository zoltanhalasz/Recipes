using Recipes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Services
{
    public interface IRecipeService
    {
        List<RecipeModel> GetAll();
        RecipeModel GetById(int id);
        RecipeModel Create(RecipeModel rec);
        void Remove(int id);
        public void Update(int id, RecipeModel recIn);

    }
}
