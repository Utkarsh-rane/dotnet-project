using DivorseManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;

using System.Text.Json.Serialization;

namespace DivorseManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Aboutus()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email,string password)
        {
           if(email == "admin@gmail.com" && password == "admin")
            {
                return RedirectToAction("Welcome");
            }
            return View();
        }
        public IActionResult Welcome()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Register(string email, string password, string firstname, string lastname, string contactnumber)
        {
            Console.WriteLine(" register HTTP POST Request Processing Logic is called..");

            Customer theCustomer = new Customer();
            theCustomer.Email = email;
            theCustomer.Password = password;
            theCustomer.ContactNumber = contactnumber;
            theCustomer.LastName = lastname;
            theCustomer.FirstName = firstname;
            Console.WriteLine(theCustomer.FirstName + " " + theCustomer.LastName);

         List<Customer> list = new List<Customer>();
           list.Add(theCustomer);
           
                var options = new JsonSerializerOptions { IncludeFields = true };
                var customerJson = JsonSerializer.Serialize<List<Customer>>(list, options);
                string fileName = @"D:\.net\day7\customer.json";
                System.IO.File.WriteAllText(fileName, customerJson);


                string jsonString = System.IO.File.ReadAllText(fileName);
                List<Customer> jsonCustomer = JsonSerializer.Deserialize<List<Customer>>(jsonString);
                Console.WriteLine("\n JSON :Deserializing data from json file\n \n");
                ViewBag.v=jsonCustomer;
                foreach (Customer customer in jsonCustomer)
                {
                    Console.WriteLine($"{customer.FirstName} : {customer.LastName}");
                }
          
      
            ViewBag.c = list;
            return View("list");
            //return RedirectToAction("list");
        }
        public IActionResult List()
        {

            return View();
        }
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