using Blog.Controllers;
using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Test;


public class UnitTest1
{

    //  Test for GetPosts method in PostController class


    [Fact]
    public void GetPostResultOk()
    {
      var options = new DbContextOptionsBuilder<ApiDbContext>()
      .UseInMemoryDatabase(databaseName: "Blog")
      .Options;

      var context = new ApiDbContext(options);

      var controller = new PostController(context);

      var result = controller.GetPosts();
      Assert.IsType<OkObjectResult>(result.Result);

    }


    
    //  Test for CreatePost method in PostController class  
    [Fact]
    public void CreatePostOk()
    {
      // Given
      var options = new DbContextOptionsBuilder<ApiDbContext>()
      .UseInMemoryDatabase(databaseName: "Blog")
      .Options;

      var context = new ApiDbContext(options);

      var controller = new PostController(context);
      var post_value = new Post {id = 1, title = "test", content = "test", Comments = {}};

      var result = controller.CreatePost(post_value);
      var createdAtResult = result.Result as CreatedAtActionResult;
      var post_res = createdAtResult?.Value as Post;
      // var result = controller.GetPosts();
      Assert.Equal("test", post_res?.title);
      Assert.Equal("test", post_res?.content);

    }

    //  Test for a bad request in CreatePost method in PostController class


    [Fact]
    public void CreateCommentOk()
    {
      // Given
      var options = new DbContextOptionsBuilder<ApiDbContext>()
      .UseInMemoryDatabase(databaseName: "Blog")
      .Options;

      var context = new ApiDbContext(options);

      var controller = new CommentController(context);
      var comment_value = new Comment {id = 1, text = "test", postId = 1, Post = null};

      var result = controller.CreateComment(comment_value);
      var createdAtResult = result.Result as CreatedAtActionResult;
      var comment_res = createdAtResult?.Value as Comment;
      // var result = controller.GetPosts();
      Assert.Equal("test", comment_res?.text);
      Assert.Equal(1, comment_res?.postId);

    }



    // create assync function for get all  comments


    [Fact]
    public void GetCommentResultOk()
    {
        var options = new DbContextOptionsBuilder<ApiDbContext>()
      .UseInMemoryDatabase(databaseName: "Blog")
      .Options;

      var context = new ApiDbContext(options);

      var controller = new CommentController(context);

      var result = controller.GetComments();
      Assert.IsType<OkObjectResult>(result.Result);

    }

    [Fact]
    public void UpdatePost()
    {
      var options = new DbContextOptionsBuilder<ApiDbContext>()
      .UseInMemoryDatabase(databaseName: "Blog")
      .Options;

      var context = new ApiDbContext(options);

      var controller = new PostController(context);

      var post_value = new Post {id = 2, title = "test", content = "test", Comments = {}};

      var result = controller.UpdatePost(2, post_value);

      var return_val = Assert.IsType<OkObjectResult>(result);
      Assert.Equal("Updated Successfully",return_val.Value);
    }






}