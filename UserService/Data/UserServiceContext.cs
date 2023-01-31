using Microsoft.EntityFrameworkCore;
namespace UserService.Data
{
    public class UserServiceContext:DbContext
    {
        public UserServiceContext(DbContextOptions<UserServiceContext> options):base(options)   
        {   }
        public DbSet<Entities.User> User { get; set; }
    }
}
