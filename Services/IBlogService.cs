using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalBlog.Models;

namespace PersonalBlog.Services
{
	public interface IBlogService
	{
		Task<IEnumerable<BlogPost>> GetBlogPostsAsync();
	}
}
