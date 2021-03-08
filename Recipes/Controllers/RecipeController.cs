using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Recipes.Models;
using Recipes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly RecipeService _recipeService;

        public RecipeController(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public ActionResult<List<Recipe>> Get() =>
            _recipeService.GetAll();

        [HttpGet("{id}", Name = "GetRecipe")]
        public ActionResult<Recipe> GetById(string id)
        {
            var rec = _recipeService.GetById(id);

            if (rec == null)
            {
                return NotFound();
            }

            return rec;
        }

        [HttpPost]
        public ActionResult<Recipe> CreateRecipe([FromBody] Recipe recipe)
        {
            try
            {                               
                recipe.Id = ObjectId.GenerateNewId().ToString();
                var returnRec = _recipeService.Create(recipe);
                return Ok(returnRec);
            }
            catch (Exception ex)
            {
                return Ok(new { Error = ex.Message });
            }

        }

        [HttpDelete]
        public ActionResult<Recipe>DeleteRecipe([FromQuery] string id)
        {
            try
            {
                _recipeService.Remove(id);
                return Ok(new { RemovedId = id });
            }
            catch (Exception ex)
            {
                return Ok(new { Error = ex.Message });
            }

        }

        [HttpPut]
        public ActionResult<Recipe> UpdateRecipe([FromQuery] string id, [FromBody] Recipe rec)
        {
            try
            {
                _recipeService.Update(id,rec);
                rec.Id = id;
                return Ok(rec);
            }
            catch (Exception ex)
            {
                return Ok(new { Error = ex.Message });
            }

        }
    }
}
