using System;
using System.ComponentModel.DataAnnotations;

namespace Imemories.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        
        public string Title { get; set; }
        public string Text { get; set; }
        public byte[] AudioPath { get; set; }
        
        public byte[] Photo { get; set; }
        
        public DateTime Time { get; set; }
        
        //Add Date Time
    }
}