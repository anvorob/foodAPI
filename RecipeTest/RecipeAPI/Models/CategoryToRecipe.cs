using System;
using System.Collections.Generic;

namespace RecipeAPI.Models
{
    public partial class CategoryToRecipe
    {
        public int RecipeId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
