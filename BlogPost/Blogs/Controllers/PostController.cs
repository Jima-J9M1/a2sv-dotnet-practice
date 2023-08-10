using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Blog.Data;


namespace Blog.Controllers;
[ApiController]
[Route("api/[controller]")]
    public class PostController: ControllerBase
    {
        private readonly ApiDbContext _context;

        public PostController(ApiDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetPosts() //
        {
            var posts = _context.Posts.ToList();
            if(posts == null)
            {
                return NotFound();
            }

            foreach(var post in posts)
            {
                var comment = _context?.Comments.Where(c => c.postId == post.id).ToList();
                // remove post from the comment
                if(comment == null)
                {
                    return NotFound();
                }
                foreach(var c in comment)
                {
                 if(c.Post != null) {
                        c.Post = null;
                    };
                }
                post.Comments = comment;

            }
            return Ok(posts);
        }


        [HttpGet("{id}")]
        public ActionResult<Post> GetPost(int id)
        {
            var post = _context.Posts.Find(id);
            var comment = _context.Comments.Where(c => c.postId == id).ToList();
            // remove post from the comment
            foreach(var c in comment)
            {
                if(c.Post != null) {
                        c.Post = null;
                    };
            }
            post.Comments = comment.ToList();
            if(post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }


        [HttpPost]

        public ActionResult<Post> CreatePost(Post post)
        {
             
            _context.Posts.Add(post);
            _context.SaveChanges();
            // what is createdAtAction doing?
            return CreatedAtAction(nameof(GetPost), new {id = post.id}, post);
        }




        [HttpPut("{id}")]
        public IActionResult UpdatePost(int id, Post post)
        {
            var existingPost = _context.Posts.Find(id);
            if(existingPost == null)
            {
                return NotFound();
            }
            existingPost.title = post.title;
            existingPost.content = post.content;
            _context.Posts.Update(existingPost);
            _context.SaveChanges();
            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {
            var post = _context.Posts.Find(id);
            if(post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            _context.SaveChanges();
            return NoContent();
        }

        // Comments routes below this line //
    }
