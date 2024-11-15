using BlogApi.Entities;

namespace BlogApi.Services;

public interface IPostService
{
    Task<IEnumerable<Post>> GetPosts();
    Task<Post> GetPost(int id);
    Task<Post> AddPost(Post Post);
    Task<Post> DeletePost(int id);
    Task<Post> UpdatePost(Post Post);   
}
