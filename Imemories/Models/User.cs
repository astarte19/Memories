using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
namespace Imemories.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Posts = new HashSet<Post>();
        }
        
        public ICollection<Post> Posts { get; set; }
    }
}