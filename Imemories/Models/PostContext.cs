using Microsoft.EntityFrameworkCore;
namespace Imemories.Models
{
    public class PostContext : DbContext
    {
        public PostContext(DbContextOptions<PostContext> options) : base(options)
        {

        }

        public DbSet<Post> PostList { get; set; }
    }
}