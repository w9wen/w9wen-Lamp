using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Syncfusion.EJ2.DropDowns;
using w9wen.Lamp.AspNetCore.Models;

namespace w9wen.Lamp.AspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var Order = OrdersDetails.GetAllRecords();
            ViewBag.DataSource = Order;


            // string[] data = new string[] { "Austria", "Brazil", "Canada", "Cameroon", "Denmark", "Finland", "Germany", "Switzerland" };

            // var data = new List<object>() { new { value = "test", text = "test" }, new { value = "test2", text = "test2" } };


            var data = new List<object>()
            {
                new {Id = 0, Country = "Australia"},
                new {Id = 1, Country = "Bermuda"},
                new {Id = 2, Country = "Canada"},
                new {Id = 3, Country = "Cameroon"},
                new {Id = 4, Country = "Denmark"},
                new {Id = 5, Country = "Finland"},
                new {Id = 6, Country = "Greenland"},
                new {Id = 7, Country = "Poland"},
            };

            // var dropDownList = new DropDownList()
            // {
            //     // DataSource = new Syncfusion.EJ2.DataManager()
            //     // {
            //     //     Url = "/api/Employee",
            //     //     Adaptor = "WebApiAdaptor"
            //     // },
            //     DataSource = data,
            //     Query = "new ej.data.Query()",
            //     Fields = new Syncfusion.EJ2.DropDowns.DropDownListFieldSettings()
            //     {
            //         Value = "Id",
            //         Text = "Country"
            //     },
            // };


            // ViewBag.DropDownData = new { placeholder = "Find a country", dataSource = data };
            ViewBag.DropdownData = data;
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
