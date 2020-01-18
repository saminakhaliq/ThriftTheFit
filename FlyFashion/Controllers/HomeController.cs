using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Firebase.Database;
using Firebase.Database.Query;
using FlyFashion.Models;
using System.Net.Http;

namespace FlyFashion.Controllers
{
    public class HomeController : Controller
    {

        // ---------- Home page ----------
        public IActionResult Index()
        {
            return View();
        }

        // ---------- Submit an item to the marketplace ----------

        [HttpGet]
        public IActionResult PostItem()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostItem(Item model)
        {
            if (ModelState.IsValid)
            {

                Item item = new Item(model.Type, model.Colour, model.Season, model.Description, model.Price ?? 0, model.Title, model.Image, model.Condition);
                var firebaseClient = new FirebaseClient("https://fly-fashion.firebaseio.com/");
                var result = await firebaseClient
                  .Child("Items")
                  .PostAsync(item);

                return RedirectToAction("Confirmation");

            }
            return View();
        }

        // ---------- Confirm post has been added to marketplace ----------

        [HttpGet]
        public IActionResult Confirmation()
        {
            return View();
        }

        public async Task<string> AllItems()
        {

            string URL = "https://fly-fashion.firebaseio.com/.json";

          
            using (HttpClient client = new HttpClient())
            {
                return await client.GetStringAsync(URL);
            }

            

            
        }

        // ---------- Display market place ----------


        public async Task<IActionResult> Marketplace()
        {

            
            var firebaseClient = new FirebaseClient("https://fly-fashion.firebaseio.com/");
          

            //Retrieve data from Firebase
            var dbItems = await firebaseClient
              .Child("Items")
              .OnceAsync<Item>();

            var itemsList = new List<Item>();

            foreach (var item in dbItems)
            {
               
                var newItem = new Item(item.Object.Type, item.Object.Colour, item.Object.Season,
                    item.Object.Description, item.Object.Price ?? 0, item.Object.Title, item.Object.Image, item.Object.Condition);
                itemsList.Add(newItem);
            }

            return View(itemsList);


        }

        // ---------- Raincheck Feature----------

        [HttpGet]
        public IActionResult Raincheck()
        {
            return View();
        }



        // ---------- TUTORIALS OVER HERE / TESTING ----------

        /*
    public async Task<ActionResult> About()
    {
        //Simulate test user data and login timestamp
        var userId = "12345";
        var currentLoginTime = DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss");

        //Save non identifying data to Firebase
        var currentUserLogin = new Item() { TimestampUtc = currentLoginTime };
        var firebaseClient = new FirebaseClient("https://fly-fashion.firebaseio.com/");
        var result = await firebaseClient
          .Child("Users/" + userId + "/Logins")
          .PostAsync(currentUserLogin);

        //Retrieve data from Firebase
        var dbItems = await firebaseClient
          .Child("Users")
          .Child(userId)
          .Child("Logins")
          .OnceAsync<Item>();

        var timestampList = new List<DateTime>();

        //Convert JSON data to original datatype
        foreach (var item in dbItems)
        {
            timestampList.Add(Convert.ToDateTime(item.Object.TimestampUtc).ToLocalTime());
        }

        //Pass data to the view
        ViewBag.CurrentUser = userId;
        ViewBag.Logins = timestampList.OrderByDescending(x => x);
        return View();
    }*/

    }
}