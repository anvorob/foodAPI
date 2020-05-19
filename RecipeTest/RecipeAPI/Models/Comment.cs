using System;
using System.Collections.Generic;

namespace RecipeAPI.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public string Comment1 { get; set; }
        public int? User { get; set; }
        public int? Recipe { get; set; }
        public int? Rating { get; set; }

        public virtual Recipe RecipeNavigation { get; set; }
        public virtual Users UserNavigation { get; set; }
    }
}
