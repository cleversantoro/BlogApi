using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BlogApi.Entities;

namespace BlogApi.Context;

public class BlogApiContext : IdentityDbContext
{
    public BlogApiContext(DbContextOptions<BlogApiContext> options) : base(options) { }

    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().HasData(
            new Post
            {
                Id = 1,
                Title = "teste 1",
                Comment = "Teste de Comentario 1"
            },
            new Post
            {
                Id = 2,
                Title = "teste 2",
                Comment = "Teste de Comentario 2"
            },
            new Post
            {
                Id = 3,
                Title = "teste 3",
                Comment = "Teste de Comentario 3"
            }
        );
        base.OnModelCreating(modelBuilder);
    }
}
