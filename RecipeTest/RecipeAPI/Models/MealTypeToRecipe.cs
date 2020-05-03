using System;
using System.Collections.Generic;

namespace RecipeAPI.Models
{
    public partial class MealTypeToRecipe
    {
        public int RecipeId { get; set; }
        public int MealTypeId { get; set; }

        public virtual MealType MealType { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
