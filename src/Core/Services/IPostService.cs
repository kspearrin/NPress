using System.Collections.Generic;
using System.Threading.Tasks;
using NPress.Core.Domains;

namespace NPress.Core.Services
{
    public interface IPostService
    {
        Task<Post> GetPostByIdAsync(string id);
        Task<Post> GetPostBySlugAsync(string slug);
        Task<IEnumerable<Post>> PagePostsAsync(string cursor, int page, int pageSize, bool ascending);
        Task CreatePostAsync(Post post);
    }
}
