using Microsoft.AspNetCore.Http;
using System;


namespace Imemories.ViewModels
{
    public class PostViewModel
    {
        
        public string Title { get; set; }
        public string Text { get; set; }
        public IFormFile AudioPath { get; set; }
        
        public IFormFile Photo { get; set; }
        public DateTime Time { get; set; }
    }
}