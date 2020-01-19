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

                Item item = new Item(model.Type, model.Colour, model.Season, model.Description, model.Price ?? 0, model.Title, model.Image, model.Condition, model.Size);
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

        // ---------- Format strings, first letter Uppercase ----------

        string UppercaseFirst(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
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
               
                var newItem = new Item(UppercaseFirst(item.Object.Type), UppercaseFirst(item.Object.Colour), UppercaseFirst(item.Object.Season),
                    UppercaseFirst(item.Object.Description), item.Object.Price ?? 0, UppercaseFirst(item.Object.Title), UppercaseFirst(item.Object.Image), UppercaseFirst(item.Object.Condition), item.Object.Size.ToUpper());
                itemsList.Add(newItem);
            }

            return View(itemsList);


        }

        // ---------- Raincheck Feature----------

        [HttpGet]
        public async Task<IActionResult> Raincheck()
        {

            var firebaseClient = new FirebaseClient("https://fly-fashion.firebaseio.com/");

            //Retrieve data from Firebase
            var dbItems = await firebaseClient
              .Child("Items")
              .OnceAsync<Item>(); 

            var itemsList = new List<Item>();

            int month = DateTime.Now.Month;

            string season = "";

            if(month == 1 || month == 2 || month == 3 || month == 4)
            {
                season = "winter";
            }
            else if (month == 5 || month == 6 || month == 7 || month == 8)
            {
                season = "spring";
            }
            else if (month == 9 || month == 10 || month == 11 || month == 12)
            {
                season = "summer";
            }
            else
            {
                season = "fall";
            }



            foreach (var item in dbItems)
            {

                

                if(item.Object.Season.ToUpper() == season.ToUpper() || item.Object.Season == "all")
                {
                    var newItem = new Item(item.Object.Type, item.Object.Colour, item.Object.Season,
                    item.Object.Description, item.Object.Price ?? 0, item.Object.Title, item.Object.Image, item.Object.Condition, item.Object.Size);
                    itemsList.Add(newItem);
                }

                
            }

            return View(itemsList);
         
        }

        // ---------- Mix and Match Feature ----------

        [HttpGet]
        public async Task<IActionResult> MixAndMatch()
        {

            var firebaseClient = new FirebaseClient("https://fly-fashion.firebaseio.com/");

            //Retrieve data from Firebase
            var dbItems = await firebaseClient
              .Child("Items")
              .OnceAsync<Item>();

            var itemsList = new List<Item>();

            foreach (var item in dbItems)
            {

                var newItem = new Item(UppercaseFirst(item.Object.Type), UppercaseFirst(item.Object.Colour), UppercaseFirst(item.Object.Season),
                    UppercaseFirst(item.Object.Description), item.Object.Price ?? 0, UppercaseFirst(item.Object.Title), UppercaseFirst(item.Object.Image), UppercaseFirst(item.Object.Condition), item.Object.Size.ToUpper());
                itemsList.Add(newItem);
            }

            return View(itemsList);
        }

        [HttpGet]
        public IActionResult Test()
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