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
    [Route("v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/User
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/User/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            RecipeapiContext con = new RecipeapiContext();
            ExUser usr = con.Users.Where(user => user.Id == id).Select(user=> new ExUser
            {
                Id = user.Id,
                Name = user.Name,
                Login = user.Login,
                Image = user.Image
            }).FirstOrDefault();
            if (usr == null)
                return NotFound("User not found");
            return Ok(usr);
        }

        // POST: api/User
        [HttpPost("create")]
        public ActionResult Post(ExUser userObj)
        {
            RecipeapiContext con = new RecipeapiContext();
            Users usr = con.Users.FirstOrDefault(user => user.Login == userObj.Login);
            if (usr == null)
            {
                usr = new Users();
                usr.Login = userObj.Login;
                usr.Name = userObj.Name;
                usr.Password = userObj.Password;
                usr.Image = userObj.Image;
                con.Users.Add(usr);
            }
            else
            {
                
                usr.Name = userObj.Name;
                usr.Password = userObj.Password;
                usr.Image = userObj.Image;
            }
            int result = con.SaveChanges();
            if (result > 0)
                return Ok(usr);
            return BadRequest("Could not save");
        }

        
        [HttpPost("login")]
        public ActionResult Login(ExUser userObj)
        {
            RecipeapiContext con = new RecipeapiContext();
            Users loginUser = con.Users.Where(user => user.Login == userObj.Login && user.Password == userObj.Password).FirstOrDefault();
            if(loginUser!=null)
            {
                if (loginUser.IsLoggedIn == 0)
                    loginUser.IsLoggedIn = 1;
                else
                {
                    return Ok(loginUser); ;
                }
                loginUser.Favourite=con.Favourite.Where(fav => fav.UserId == loginUser.Id).ToList();
                loginUser.Comment = con.Comment.Where(fav => fav.User == loginUser.Id).ToList();
                int result = con.SaveChanges();
                if (result > 0)
                    return Ok(loginUser);
            }

            return BadRequest("Failed to login");
        }

        [HttpGet("logout/{UserID}")]
        public ActionResult Logout(string UserID)
        {
            int id = 0;
            int.TryParse(UserID, out id);
            RecipeapiContext con = new RecipeapiContext();
            Users loginUser = con.Users.Where(user => user.Id == id).FirstOrDefault();
            
            if (loginUser != null)
            {
                if (loginUser.IsLoggedIn == 1)
                    loginUser.IsLoggedIn = 0;
                else
                {
                    return Ok(loginUser); ;
                }
                int result = con.SaveChanges();
                if (result > 0)
                    return Ok(loginUser);
            }

            return BadRequest("Failed to logout");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            RecipeapiContext con = new RecipeapiContext();
            Users user = con.Users.FirstOrDefault(user=>user.Id==id);
            con.Users.Remove(user);
            return Ok("Deleted");
        }
    }
}
