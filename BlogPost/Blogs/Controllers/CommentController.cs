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
        
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments() //
        {

            var comments = await _context.Comments.ToListAsync();
            try
            {
            foreach(var comment in comments)
            {
                var post = _context.Posts.Find(comment.postId);
                post.Comments = null;
                comment.Post = post;
            }
            return Ok(comments);
            }
            catch (Exception ex)
            {
             return NotFound();
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = _context.Comments.Find(id);
            var post = _context.Posts.Find(comment.postId);
            // error handling 
            try{
              post.Comments = null;   

            comment.Post = post;

            return Ok(comment);


            }catch(Exception ex)
            {
              return NotFound();
            }
            
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
            if(id != comment.id)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
         //async function for delete comment
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = _context.Comments.Find(id);

            try
            {
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return NoContent();
            }
            catch (Exception ex)
            {
                 return NotFound();
            }
            if(comment == null)
            {
                
            }


        }
    }
