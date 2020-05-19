using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecipeAPI.Models;
using RecipeAPI.Resources;

namespace RecipeAPI.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        // GET: api/Recipe
        [HttpGet]
        public ActionResult Get()
        {
            RecipeapiContext con = new RecipeapiContext();
            con.Recipe.ToList();
            List<ExRecipe> returnRecipeList = new List<ExRecipe>();
            foreach(Recipe recipe in con.Recipe.ToList())
            {
                ExRecipe returnRecipe = new ExRecipe();
                returnRecipe.id = recipe.Id;
                returnRecipe.name = recipe.Name;
                returnRecipe.description = recipe.Description;
                returnRecipe.Categories = con.Category.Where(mt => mt.CategoryToRecipe.Any(mt1 => mt1.Recipe.Id == recipe.Id)).ToList();
                returnRecipe.CookTime = recipe.CookTime;
                returnRecipe.PrepTime = recipe.PrepTime;
                returnRecipe.Cuisine = recipe.Cuisine;
                returnRecipe.DietList = con.Diet.Where(mt => mt.DietToRecipe.Any(mt1 => mt1.Recipe.Id == recipe.Id)).ToList();
                returnRecipe.Image = recipe.Image;
                returnRecipe.IngredientList = con.Ingredients.Where(nutr => nutr.Recipe.Value == recipe.Id).ToList();
                //returnRecipe.Ingredients = JsonConvert.SerializeObject(returnRecipe.IngredientList);
                returnRecipe.Instructions = (!string.IsNullOrEmpty(recipe.Instructions))?JsonConvert.DeserializeObject<List<InstructionObj>>(recipe.Instructions):new List<InstructionObj>();
                returnRecipe.MealTypeList = con.MealType.Where(mt => mt.MealTypeToRecipe.Any(mt1 => mt1.Recipe.Id == recipe.Id)).ToList();
                returnRecipe.NumberOfServings = recipe.NumberOfServings;
                returnRecipe.Nutrition = con.Nutrition.Where(nutr => nutr.Recipe.Value == recipe.Id).ToList();
                returnRecipeList.Add(returnRecipe);
            }
            return Ok(JsonConvert.SerializeObject(returnRecipeList));
        }

        // GET: api/Recipe/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult Get(int id)
        {
            if (id == 0)
                return BadRequest("Id cannot be 0");
            try
            {
                RecipeapiContext con = new RecipeapiContext();
                Recipe recipe = con.Recipe.FirstOrDefault(res => res.Id == id);


                ExRecipe returnRecipe = new ExRecipe();
                returnRecipe.id = recipe.Id;
                returnRecipe.name = recipe.Name;
                returnRecipe.description = recipe.Description;
                returnRecipe.Categories = con.Category.Where(mt => mt.CategoryToRecipe.Any(mt1 => mt1.Recipe.Id == recipe.Id)).ToList();
                returnRecipe.CookTime = recipe.CookTime;
                returnRecipe.PrepTime = recipe.PrepTime;
                returnRecipe.Cuisine = recipe.Cuisine;
                returnRecipe.DietList = con.Diet.Where(mt => mt.DietToRecipe.Any(mt1 => mt1.Recipe.Id == recipe.Id)).ToList();
                returnRecipe.Image = recipe.Image;
                returnRecipe.IngredientList = con.Ingredients.Where(nutr => nutr.Recipe.Value == recipe.Id).ToList();
                returnRecipe.Ingredients = JsonConvert.SerializeObject(returnRecipe.IngredientList);
                returnRecipe.Instructions = (!string.IsNullOrEmpty(recipe.Instructions)) ? JsonConvert.DeserializeObject<List<InstructionObj>>(recipe.Instructions) : new List<InstructionObj>(); ;
                returnRecipe.MealTypeList = con.MealType.Where(mt => mt.MealTypeToRecipe.Any(mt1 => mt1.Recipe.Id == recipe.Id)).ToList();
                returnRecipe.NumberOfServings = recipe.NumberOfServings;
                returnRecipe.Nutrition = con.Nutrition.Where(nutr => nutr.Recipe.Value == recipe.Id).ToList();


                if (recipe != null)
                    return Ok(JsonConvert.SerializeObject(returnRecipe));
                else
                    return NotFound();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Favourite/{isFavourite}")]
        public ActionResult Favourite(Favourite favourite,bool isFavourite)
        {
            
            if (favourite.RecipeId == 0 && favourite.UserId==0)
                return BadRequest("Id cannot be 0");
            try
            {
                RecipeapiContext con = new RecipeapiContext();
                Recipe recipe = con.Recipe.FirstOrDefault(res => res.Id == favourite.RecipeId);
                Users usr = con.Users.FirstOrDefault(usr => usr.Id == favourite.UserId);
                //Favourite favourite22 = new Favourite();
                //favourite22.RecipeId = favourite.RecipeId;
                //favourite22.UserId = favourite.UserId;
                //favourite22.Recipe = recipe;
                //favourite22.User = usr;
                if (recipe == null || usr == null)
                    return BadRequest("Could not find one of entities");

                if (con.Favourite.Any(fav => fav.RecipeId == favourite.RecipeId && fav.UserId == favourite.UserId) && !isFavourite) {
                    con.Favourite.Remove(favourite);
                    //usr.Favourite.Remove(favourite);
                }
                else {
                    con.Favourite.Add(favourite);
                    //usr.Favourite.Add(favourite22);
                }
                int result = con.SaveChanges();

                if (result > 0)
                    return Ok("Success");
                else
                    return BadRequest("Failed to update");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Recipe
        [HttpPost]
        public ActionResult Post(ExRecipe recipe)
        {
            
            RecipeapiContext con = new RecipeapiContext();
            
            //List<Ingredients> ingrList = new List<Ingredients>();
            //foreach(Ingredients ingredient in recipe.IngredientList)
            //{
                
            //    ingrList.Add(ingredient);
            //}
            Recipe recipeObj = new Recipe();
            if (recipe.id >0)
            {
                recipeObj = con.Recipe.FirstOrDefault(rec => rec.Id == recipe.id)?? new Recipe(); ;
            }
            

            recipeObj.Name = recipe.name;
            recipeObj.Description = recipe.description;
            recipeObj.Ingredients = null;// JsonConvert.SerializeObject(recipe.IngredientList);
            recipeObj.Image = recipe.Image;
            recipeObj.NumberOfServings = recipe.NumberOfServings;
            recipeObj.CookTime = recipe.CookTime;
            recipeObj.PrepTime = recipe.PrepTime;
            recipeObj.Instructions = JsonConvert.SerializeObject(recipe.Instructions);
            recipeObj.Cuisine = recipe.Cuisine;
            //recipeObj.IngredientsNavigation = ingrList;
                
            

            //List< DietToRecipe> dietToRList = new List<DietToRecipe>();
            //List<CategoryToRecipe> CategoryToRList = new List<CategoryToRecipe>();
            //List<MealTypeToRecipe> MealTypeToRList = new List<MealTypeToRecipe>();


            // Search for deleted Nutrition items
            List<int> existinglistIngre = con.Ingredients.Where(nutr => nutr.Recipe == recipe.id).Select(dbnutr => dbnutr.Id).ToList();
            List<int> deleteListIngr = new List<int>();
            if (recipe.IngredientList != null)
            {
                deleteListIngr = existinglistIngre.Except(recipe.IngredientList.Select(x => x.Id)).ToList();
                foreach (int id in deleteListIngr)
                {
                    Ingredients n = con.Ingredients.FirstOrDefault(nut => nut.Id == id);
                    if (n != null)
                        con.Ingredients.Remove(n);
                }

                // Add or update nutrition records
                foreach (Ingredients nutr in recipe.IngredientList)
                {
                    if (nutr.Id > 0)
                    {
                        Ingredients n = con.Ingredients.FirstOrDefault(nutrition => nutrition.Id == nutr.Id);
                        n.Product = nutr.Product;
                        n.Units = nutr.Units;
                        n.Quantity = nutr.Quantity;

                    }
                    else
                        recipeObj.IngredientsNavigation.Add(nutr);
                }
            }


            // Search for deleted Nutrition items
            List<int> existinglist =con.Nutrition.Where(nutr => nutr.Recipe == recipe.id).Select(dbnutr => dbnutr.Id).ToList();
            List<int> deleteList = new List<int>();
            if (recipe.Nutrition != null)
            {
                deleteList = existinglist.Except(recipe.Nutrition.Select(x => x.Id)).ToList();

                foreach (int id in deleteList)
                {
                    Nutrition n = con.Nutrition.FirstOrDefault(nut => nut.Id == id);
                    if (n != null)
                        con.Nutrition.Remove(n);
                }

                // Add or update nutrition records
                foreach (Nutrition nutr in recipe.Nutrition)
                {
                    if (nutr.Id > 0)
                    {
                        Nutrition n = con.Nutrition.FirstOrDefault(nutrition => nutrition.Id == nutr.Id);
                        n.Label = nutr.Label;
                        n.Unit = nutr.Unit;
                        n.Value = nutr.Value;

                    }
                    else
                        recipeObj.Nutrition.Add(nutr);
                }
            }

            // Search for deleted Nutrition items
            List<string> existinglistMeal = con.MealTypeToRecipe.Where(nutr => nutr.RecipeId == recipe.id).Select(dbnutr => dbnutr.MealType.Name).ToList();
            List<string> deleteListMeal = new List<string>();
            if (recipe.MealTypeList != null)
            {
                deleteListMeal = existinglistMeal.Except(recipe.MealTypeList.Select(x => x.Name)).ToList();
                foreach (string id in deleteListMeal)
                {
                    MealTypeToRecipe n = con.MealTypeToRecipe.FirstOrDefault(nut => nut.MealType.Name == id && nut.RecipeId == recipe.id);
                    if (n != null)
                        con.MealTypeToRecipe.Remove(n);
                }
                foreach (MealType meal in recipe.MealTypeList)
                {
                    MealTypeToRecipe dtor = null;
                    //Getting mealType from DB list
                    MealType mealType = con.MealType.FirstOrDefault(d => d.Name == meal.Name);
                    if (mealType != null)
                    {
                        // checking if it is linked to recipe
                        dtor = con.MealTypeToRecipe.FirstOrDefault(mtr => mtr.MealTypeId == mealType.Id && recipe.id == mtr.RecipeId);
                    }

                    // if connection between recipe and mealtype dont exist, create it
                    if (dtor == null)
                    {
                        dtor = new MealTypeToRecipe();
                        dtor.MealType = mealType ?? meal;
                        dtor.Recipe = recipeObj;
                        recipeObj.MealTypeToRecipe.Add(dtor);
                    }
                }
            }


            // Search for deleted Category items
            List<string> existinglistCate = con.CategoryToRecipe.Where(nutr => nutr.RecipeId == recipe.id).Select(dbnutr => dbnutr.Category.Name).ToList();
            List<string> deleteListCat = new List<string>();
            if (recipe.Categories!=null)
                deleteListCat = existinglistCate.Except(recipe.Categories.Select(x=>x.Name)).ToList();

            foreach (string id in deleteListCat)
            {
                CategoryToRecipe n = con.CategoryToRecipe.FirstOrDefault(nut => nut.Category.Name == id && nut.RecipeId == recipe.id);
                if (n != null)
                    con.CategoryToRecipe.Remove(n);
            }
            foreach (Category category in recipe.Categories)
            {
                CategoryToRecipe dtor = null;
                Category cat = con.Category.FirstOrDefault(d => d.Name == category.Name);
                if(cat!=null)
                {
                    // checking if it is linked to recipe
                    dtor = con.CategoryToRecipe.FirstOrDefault(ctr => ctr.CategoryId == cat.Id && recipe.id == ctr.RecipeId);
                }
                if (dtor==null)
                {
                    dtor = new CategoryToRecipe();
                    dtor.Category = cat ?? category;
                    dtor.Recipe = recipeObj;
                    recipeObj.CategoryToRecipe.Add(dtor);
                }
            }

            // Delete diets connection
            List<string> existinglistDietCon = con.DietToRecipe.Where(nutr => nutr.RecipeId == recipe.id).Select(dbnutr => dbnutr.Diet.Name).ToList();
            List<string> deleteListDiet = new List<string>();
            if (recipe.DietList != null)
            {
                deleteListDiet = existinglistDietCon.Except(recipe.DietList.Select(x => x.Name)).ToList();

                foreach (string id in deleteListDiet)
                {
                    DietToRecipe n = con.DietToRecipe.FirstOrDefault(nut => nut.Diet.Name == id && nut.RecipeId == recipe.id);
                    if (n != null)
                        con.DietToRecipe.Remove(n);
                }
                foreach (Diet d in recipe.DietList)
                {
                    Diet diet = con.Diet.FirstOrDefault(di => di.Name == d.Name);
                    DietToRecipe dtor = null;
                    if (diet != null)
                    {
                        dtor = con.DietToRecipe.FirstOrDefault(ctr => ctr.Diet.Name == d.Name && recipe.id == ctr.RecipeId);
                    }

                    if (dtor == null)
                    {
                        dtor = new DietToRecipe();
                        dtor.Diet = diet ?? d;
                        dtor.Recipe = recipeObj;
                        recipeObj.DietToRecipe.Add(dtor);
                    }
                }
            }
            //recipeObj.CategoryToRecipe = null;
            //recipeObj.DietToRecipe = null;
            //recipeObj.MealTypeToRecipe = null;

            //recipeObj.CategoryToRecipe = CategoryToRList;
            //recipeObj.DietToRecipe = dietToRList;
            //recipeObj.MealTypeToRecipe = MealTypeToRList;

            if (recipe.id == 0)
                con.Recipe.Add(recipeObj);
            con.SaveChanges();
            return Ok();
        }

        // PUT: api/Recipe/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            RecipeapiContext con = new RecipeapiContext();
            Recipe res= con.Recipe.FirstOrDefault(recipe => recipe.Id == id);
            if (res == null)
                return NotFound("This record not found");
            con.Recipe.Remove(res);
            con.SaveChanges();
            return Ok("Deleted");
        }

        [HttpGet("Categories")]
        public ActionResult<ExCollection> Categories(string groupby, string search)
        {
            RecipeapiContext con = new RecipeapiContext();

            Dictionary<string,List<Recipe>> categoryList = new Dictionary<string, List<Recipe>>();
            Dictionary<string, List<Recipe>> dietList = new Dictionary<string, List<Recipe>>();
            Dictionary<string, List<Recipe>> cuisineList = new Dictionary<string, List<Recipe>>();
            
            if(string.IsNullOrEmpty(groupby) || groupby=="category")
            foreach (Category category in con.Category.OrderBy(x=>x.Name).ToList())
            {
                    if (!string.IsNullOrEmpty(search) && search != category.Name)
                        continue;
                if (category != null && !categoryList.Keys.Any(x => x == category.Name))
                {
                    categoryList.TryAdd(category.Name, con.CategoryToRecipe.Where(x => x.Category.Name == category.Name).Select(x => x.Recipe).ToList());
                       
                }
            }

            if (string.IsNullOrEmpty(groupby) || groupby == "diet")
            foreach (Diet diet in con.Diet.OrderBy(cat => cat.Name))
            {
                 if (!string.IsNullOrEmpty(search) && search != diet.Name)
                        continue;
                if (diet != null && !dietList.Keys.Any(x => x == diet.Name))
                {
                    dietList.TryAdd(diet.Name, con.DietToRecipe.Where(x => x.Diet.Name == diet.Name).Select(x => x.Recipe).ToList());
                }
            }

            if (string.IsNullOrEmpty(groupby) || groupby == "cuisine")
            foreach (string cuisine in con.Recipe.Select(x=>x.Cuisine))
            {
                if (!string.IsNullOrEmpty(search) && search != cuisine.ToLower().Trim())
                     continue;
                if (cuisine != "" && !cuisineList.Keys.Contains(cuisine.ToLower().Trim()))
                {
                    cuisineList.TryAdd(cuisine.ToLower().Trim(), con.Recipe.Where(x => x.Cuisine.Trim().ToLower() == cuisine.ToLower().Trim()).ToList());

                }
            }
            
            ExCollection collection = new ExCollection();
            collection.categoryList = categoryList;
            collection.cuisineList = cuisineList;
            collection.dietList = dietList;

            return Ok(collection);
        }

        [HttpGet("Misc")]
        public ActionResult Misc()
        {
            
            RecipeapiContext con = new RecipeapiContext();
            ExMisc mics = new ExMisc();
            mics.categoryList = con.Category.ToList();
            mics.dietList = con.Diet.ToList();
            mics.mealTypeList = con.MealType.ToList();
            mics.units = new List<string>() { "g", "mg", "tblspoon", "tspoon", "ng", "µg", "cup", "pound","pcs", "1/4", "1/3","1/2", "2/3", "3/4" };
            return Ok(JsonConvert.SerializeObject(mics));
        }

        [HttpGet("Search")]
        public ActionResult<List<Recipe>> Search(string searchWord)
        {
            RecipeapiContext con = new RecipeapiContext();
            return con.Recipe.Where(rec => rec.Name.Contains(searchWord) || rec.Description.Contains(searchWord)).ToList();
        }
    }
}
