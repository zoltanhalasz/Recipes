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
    public class RecipeEFController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeEFController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public ActionResult<List<RecipeModel>> Get() =>
            _recipeService.GetAll();

        [HttpGet("{id}", Name = "GetRecipeEF")]
        public ActionResult<RecipeModel> GetById(int id)
        {
            var rec = _recipeService.GetById(id);

            if (rec == null)
            {
                return NotFound();
            }

            return rec;
        }

        [HttpPost]
        public ActionResult<RecipeModel> CreateRecipe([FromBody] RecipeModel recipe)
        {
            try
            {   
                var returnRec = _recipeService.Create(recipe);
                return Ok(returnRec);
            }
            catch (Exception ex)
            {
                return Ok(new { Error = ex.Message });
            }

        }

        [HttpDelete]
        public ActionResult<RecipeModel> DeleteRecipe([FromQuery] int id)
        {

            var rec = _recipeService.GetById(id);

            if (rec == null)
            {
                return NotFound();
            }

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
        public ActionResult<RecipeModel> UpdateRecipe([FromQuery] int id, [FromBody] RecipeModel rec)
        {

            var myrec = _recipeService.GetById(id);

            if (rec == null)
            {
                return NotFound();
            }

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
