using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;
using System.Security.Claims;
using Imemories.Models;
using Microsoft.AspNetCore.Identity;


namespace Imemories.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private  readonly UserManager<User> _userManager;
        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
           
           
            
            /*SQLiteConnection check_connection = new SQLiteConnection("Data Source=UserData.db;");
            check_connection.Open();
            SQLiteCommand check_command = check_connection.CreateCommand();
         //   check_command.CommandText = $"SELECT count(rowid) FROM {a}";
            check_command.ExecuteNonQuery();
            int countRows = (int) (long) check_command.ExecuteScalar();
            check_connection.Close();
            if (countRows==0)
            {
                return Content("Нет записей в бд");
            }
            else
            {
                return Content("Запись");
            }*/
            return View();
        }
        
        

        
    }
}