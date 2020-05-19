using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeAPI.Models;
using RecipeAPI.Resources;

namespace RecipeAPI.Controllers
{
    [Route("v1/[controller]/")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        // GET: api/Comment
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Comment/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            RecipeapiContext con = new RecipeapiContext();
            ExComment comment = con.Comment.Where(com => com.Id == id).Select(com => new ExComment {
                    Id = com.Id,
                    Comment = com.Comment1,
                    User = com.UserNavigation.Name,
                    RecipeTitle = com.RecipeNavigation.Name,
                    Rating = com.Rating
            }).FirstOrDefault();
            if (comment != null)
                return Ok(comment);
            return NotFound();
        }

        // POST: api/Comment
        [HttpPost]
        public ActionResult Post(ExComment commentObj)
        {
            RecipeapiContext con = new RecipeapiContext();
            Comment newComment = new Comment();
            newComment.Rating = commentObj.Rating;
            newComment.Comment1 = commentObj.Comment;
            newComment.User = commentObj.UserID;
            newComment.Recipe = commentObj.RecipeId;
            con.Comment.Add(newComment);
            int result = con.SaveChanges();
            if (result > 0)
                return Ok(newComment);
            return BadRequest("Could not save");
        }

        // PUT: api/Comment/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
