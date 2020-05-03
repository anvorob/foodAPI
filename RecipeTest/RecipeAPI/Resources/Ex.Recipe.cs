using RecipeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeAPI.Resources
{
    public class ExRecipe
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string Ingredients { get; set; }  // JSON blob (array of text:"value")
        public string Image { get; set; }
        public int? NumberOfServings { get; set; }
        public int? CookTime { get; set; }
        public int? PrepTime { get; set; }
        public string Instructions { get; set; }  // JSON blob (array of {text:"value", image:""})
        public string Cuisine { get; set; }
        public List<Nutrition> Nutrition { get; set; }
        public List<MealType> MealTypeList { get; set; }
        public List<Category> Categories { get; set; }
        public List<Diet> DietList { get; set; }
        public List<Ingredients> IngredientList { get; set; }
    }

    public class ExCollection
    {
        public Dictionary<string,List<Recipe>> cuisineList { get; set; }
        
        public Dictionary<string, List<Recipe>> categoryList { get; set; }
        
        public Dictionary<string, List<Recipe>> dietList { get; set; }
        
    }

}
