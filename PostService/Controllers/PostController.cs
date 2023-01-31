using Microsoft.AspNetCore.Mvc;
using PostService.Data;
using PostService.Entities;
using Microsoft.EntityFrameworkCore;

namespace PostService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly PostServiceContext _postServiceContext;
        public PostController(PostServiceContext postServiceContext)
        {
                _postServiceContext = postServiceContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
           return await _postServiceContext.Posts.Include(x => x.User).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost(Post post)
        {
            _postServiceContext.Posts.Add(post);
            await _postServiceContext.SaveChangesAsync();
            return CreatedAtAction("GetPosts",new {id=post.PostId},post);   
        }
    }
}
