using System.Collections.Generic;
using System.Threading.Tasks;
using NPress.Core.Domains;

namespace NPress.Core.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> PagePostsAsync(string cursor, bool beforeCursor, int pageSize);
        Task CreatePostAsync(Post post);
    }
}
