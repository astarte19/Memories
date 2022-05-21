using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Imemories.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.Data.Sqlite;
using System.Security.Claims;
using Imemories.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace Imemories.Controllers
{
    public class PostController: Controller
	{
		

        private readonly PostContext context;
        private readonly IWebHostEnvironment webHostEnvironment;  
        public PostController(PostContext context, IWebHostEnvironment hostEnvironment)
        {
            this.context = context;
            webHostEnvironment = hostEnvironment;  
        }

        

       [HttpGet]
        public IActionResult CreatePost() => View();

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePost(PostViewModel pvm)
        {
            
                Post item = new Post()
                {
                    Title = pvm.Title, Text = pvm.Text, AudioPath = pvm.AudioPath, Time = pvm.Time
                };
                //
                if (pvm.Photo != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(pvm.Photo.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)pvm.Photo.Length);
                    }
                    // установка массива байтов
                    item.Photo = imageData;
                    
                }
                context.Add(item);
                await context.SaveChangesAsync();
                TempData["Success"] = "The item has been added!";
                return RedirectToAction("Index","Home");
            

            

        }
        

        [HttpGet]
        public async Task<ActionResult> EditPost(int id)
        {
            Post item = await context.PostList.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);

        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost(Post item)
        {
            if (ModelState.IsValid)
            {
                context.Update(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The item has been updated!";

                return RedirectToAction("Index","Home");
            }

            return View(item);
        }

        [HttpGet]
        public async Task<ActionResult> DeletePost(int id)
        {
            Post item = await context.PostList.FindAsync(id);
            if (item == null)
            {
                TempData["Error"] = "The item does not exist!";
            }
            else
            {
                context.PostList.Remove(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The item has been deleted!";
            }

            return RedirectToAction("Index","Home");
        }


    }
}