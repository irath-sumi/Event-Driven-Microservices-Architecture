using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Entities;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // All the action methods are for creating, modiying and retrieving users
    public class UsersController : Controller
    {
        private readonly UserServiceContext _context;
        public UsersController(UserServiceContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.User.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<User>> CreateUsers(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUsers", new { id = user.ID }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyUser(int id, User user)
        {
            //In Entity Framework, the "Entry" method is used to get the instance of the "DbEntityEntry"
            //class, which represents a single entity within the context that can be used to perform
            //change tracking, data validation, and database management.
            //By setting the "State" property to "EntityState.Modified", it tells the Entity Framework to
            //track the changes made to this entity and consider it for updating in the database
            //when "SaveChanges" or "SaveChangesAsync" method is called.

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
