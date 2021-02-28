using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebsite.Models;
using BlogWebsite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogWebsite.Pages
{
	public class BlogListModel : PageModel
	{
		private readonly IBlogService _blogService;
		public BlogListModel(IBlogService blogService)
		{
			_blogService = blogService;
			Blogs = new List<BlogPost>();
		}

		public IEnumerable<BlogPost> Blogs { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{
			Blogs = (await _blogService.GetBlogPostsAsync()).OrderByDescending(b => b.Published);
			return Page();
		}
	}
}