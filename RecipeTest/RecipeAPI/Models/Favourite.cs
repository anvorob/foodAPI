using System;
using System.Collections.Generic;

namespace RecipeAPI.Models
{
    public partial class Favourite
    {
        public int RecipeId { get; set; }
        public int UserId { get; set; }

        public virtual Recipe Recipe { get; set; }
        public virtual Users User { get; set; }
    }
}
