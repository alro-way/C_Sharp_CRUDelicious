using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using C_Sharp_CRUDelicious.Models;
using Microsoft.EntityFrameworkCore;

namespace C_Sharp_CRUDelicious.Controllers
{

    public class HomeController : Controller
    {
        private MyContext dbContext;
     
        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
     
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            // ViewBag.AllDishes = dbContext.Dishes
            //     .Where(l => l.Name.Contains("Cake"));
            // return View("Index");

            
            List<Dish> AllDishes = dbContext.Dishes
                .OrderByDescending(k => k.Created_at)
                .ToList();
            return View("Index", AllDishes);
        }
        
        [HttpGet]
        [Route("{DishesId}")]
        public IActionResult Details( int DishesId)
        {
            Dish thisDish = dbContext.Dishes.FirstOrDefault(k => k.DishesId == DishesId);
            return View("Details", thisDish);
        }

        [HttpGet]
        [Route("new")]
        public IActionResult New()
        {
            return View("New");
        }

        [HttpPost]
        [Route("new")]
        public IActionResult Create(Dish newDish)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(newDish);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("New");
            }
        }

        [HttpGet]
        [Route("delete/{DishesId}")]
        public IActionResult Delete(int DishesId)
        {
            Dish deleteDish = dbContext.Dishes.SingleOrDefault(k => k.DishesId == DishesId);
            dbContext.Dishes.Remove(deleteDish);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("edit_page/{DishesId}")]
        public IActionResult Edit_page(int DishesId)
        {
            Dish thisDish = dbContext.Dishes.FirstOrDefault(k => k.DishesId == DishesId);
            return View("Edit_page", thisDish);
        }


        // [HttpPost]
        // [Route("update/{DishesId}")]
        // public IActionResult Update(int DishesId, string name, string chef, int tastiness, int calories, string description)
        // {
        //     Dish updateDish = dbContext.Dishes.FirstOrDefault(k => k.DishesId == DishesId);
        //     updateDish.Name = name;
        //     updateDish.Chef = chef;
        //     updateDish.Tastiness = tastiness;
        //     updateDish.Calories = calories;
        //     updateDish.Description = description;
        //     updateDish.Updated_at = DateTime.Now;
        //     dbContext.SaveChanges();
        //     return RedirectToAction("Index");
        // }

        [HttpPost]
        [Route("update")]
        public IActionResult Update(Dish updateDish)
        {
            Dish oldDish = dbContext.Dishes.FirstOrDefault(k => k.DishesId == updateDish.DishesId);

            if(ModelState.IsValid)
            {
                // Dish oldDish = dbContext.Dishes.FirstOrDefault(k => k.DishesId == updateDish.DishesId);
                oldDish.Name = updateDish.Name;
                oldDish.Chef = updateDish.Chef;
                oldDish.Tastiness = updateDish.Tastiness;
                oldDish.Calories = updateDish.Calories;
                oldDish.Description = updateDish.Description;
                oldDish.Updated_at = DateTime.Now;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit_page", oldDish);
            }
        }







        // public IActionResult Index()
        // {
        //     return View();
        // }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
