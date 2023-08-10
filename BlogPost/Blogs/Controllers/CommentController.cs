using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Blog.Data;
using Microsoft.EntityFrameworkCore;


namespace Blog.Controllers;
[ApiController]
[Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
         private readonly ApiDbContext _context;

        public CommentController(ApiDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        // async function for get all comments
        
        public  ActionResult<IEnumerable<Comment>> GetComments() //
        {

            var comments = _context.Comments.ToList();
            if(comments == null)
            {
                return NotFound();
            }

            foreach(var comment in comments)
            {

            var post = _context.Posts.Find(comment.postId);
            // var post = post1;
                 //Deferrence of a possibly null reference
                if(post?.Comments != null) {
                        post.Comments = null;
                    };

                comment.Post = post?? throw new ArgumentNullException(nameof(post));
            }

            // assync ok
            // return IAsyncResult(comments);
            return Ok(comments);

            
        }

        [HttpGet("{id}")]
        public ActionResult<Comment> GetComment(int id)
        {
            // error handling 
            var comment = _context.Comments.Find(id);
            var post = _context.Posts.Find(comment?.postId);
            if (comment == null)
            {
                return NotFound();
            }

        
            if(post.Comments != null) {
                        post.Comments = null;
                    }; 

            comment.Post = post;

            return Ok(comment);

            
        }

        [HttpPost]

        public ActionResult<Comment> CreateComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetComment), new {id = comment.id}, comment);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateComment(int id, Comment comment)
        {
            var existingComment = _context.Comments.Find(id);
            if(existingComment == null)
            {
                return NotFound();
            }
            existingComment.text = comment.text;
            _context.Comments.Update(existingComment);
            _context.SaveChanges();

            return Ok("updated successfully");
        }

        [HttpDelete("{id}")]
         //async function for delete comment
        public ActionResult DeleteComment(int id )
        { 

        try
            {
            var comment = _context.Comments.Find(id);
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return NoContent();
            }
            catch (Exception ex)
            {
                 return NotFound(ex.Message);
            }
        }
    }
