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
using Microsoft.EntityFrameworkCore;


namespace Imemories.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private  readonly UserManager<User> _userManager;
        private readonly PostContext context;
        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, PostContext context)
        {
            _logger = logger;
            _userManager = userManager;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            IQueryable<Post> items = from i in context.PostList orderby i.Id select i;

            List<Post> posts = await items.ToListAsync();
           
            
            
            return View(posts);
        }
        
        

        
    }
}