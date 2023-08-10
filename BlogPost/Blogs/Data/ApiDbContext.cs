using System.Reflection.Emit;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Blog.Models;

namespace Blog.Data;

public class ApiDbContext : DbContext
{
    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }


    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
         
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

     modelBuilder.Entity<Post>(entity =>
        {
            entity
            .HasMany(p => p.Comments)
            .WithOne(c => c.Post)
            .OnDelete(DeleteBehavior.Cascade);
        });

    }
}

