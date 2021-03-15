using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Models
{
    public record RecipeModel
    {        

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Directions { get; set; }

        public string Ingredients { get; set; }

        public DateTime Updated { get; set; }
    }

}
