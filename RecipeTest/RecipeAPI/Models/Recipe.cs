using System;
using System.Collections.Generic;

namespace RecipeAPI.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            CategoryToRecipe = new HashSet<CategoryToRecipe>();
            DietToRecipe = new HashSet<DietToRecipe>();
            IngredientsNavigation = new HashSet<Ingredients>();
            MealTypeToRecipe = new HashSet<MealTypeToRecipe>();
            Nutrition = new HashSet<Nutrition>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Image { get; set; }
        public int? NumberOfServings { get; set; }
        public int? CookTime { get; set; }
        public int? PrepTime { get; set; }
        public string Instructions { get; set; }
        public string Cuisine { get; set; }

        public virtual ICollection<CategoryToRecipe> CategoryToRecipe { get; set; }
        public virtual ICollection<DietToRecipe> DietToRecipe { get; set; }
        public virtual ICollection<Ingredients> IngredientsNavigation { get; set; }
        public virtual ICollection<MealTypeToRecipe> MealTypeToRecipe { get; set; }
        public virtual ICollection<Nutrition> Nutrition { get; set; }
    }
}
