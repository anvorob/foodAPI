using System;
using System.Collections.Generic;

namespace RecipeAPI.Models
{
    public partial class Users
    {
        public Users()
        {
            Comment = new HashSet<Comment>();
            Favourite = new HashSet<Favourite>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public int? IsLoggedIn { get; set; }

        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Favourite> Favourite { get; set; }
    }
}
