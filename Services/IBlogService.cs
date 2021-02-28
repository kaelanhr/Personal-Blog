using System.Collections.Generic;
using System.Threading.Tasks;
using BlogWebsite.Models;

namespace BlogWebsite.Services
{
	public interface IBlogService
	{
		Task<IEnumerable<BlogPost>> GetBlogPostsAsync();
	}
}
