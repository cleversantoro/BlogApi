using Microsoft.EntityFrameworkCore;
using BlogApi.Context;
using BlogApi.Entities;

namespace BlogApi.Services;

public class PostService : IPostService
{
    private readonly BlogApiContext _context;

    public PostService(BlogApiContext context)
    {
        _context = context;
    }

    public async Task<Post> AddPost(Post Post)
    {
       if(Post is null)
            throw new ArgumentNullException(nameof(Post));

        _context.Add(Post);
        await _context.SaveChangesAsync();  

        return Post;
    }

    public async Task<Post> GetPost(int id)
    {
        var Post = await _context.Posts.FirstOrDefaultAsync(Post => Post.Id == id);

        return Post;
    }

    public async Task<IEnumerable<Post>> GetPosts()
    {
        var Posts = await _context.Posts.ToListAsync();
        return Posts;
    }


    public async Task<Post> DeletePost(int id)
    {
        var Post = await _context.Posts.FindAsync(id);

        if (Post is null)
            throw new ArgumentNullException(nameof(Post));

        _context.Remove(Post);
        await _context.SaveChangesAsync();

        return Post;
    }

    public async Task<Post> UpdatePost(Post Post)
    {
        if (Post is null)
            throw new ArgumentNullException(nameof(Post));

        _context.Update(Post);
        await _context.SaveChangesAsync();

        return Post;
    }
}
