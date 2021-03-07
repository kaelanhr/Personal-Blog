using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PersonalBlog.Helper;
using PersonalBlog.Models;
using PersonalBlog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace PersonalBlog.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly IBlogService _blogService;

		public IndexModel(ILogger<IndexModel> logger, IBlogService blogService)
		{
			_logger = logger;
			_blogService = blogService;
			Blogs = new List<BlogPost>();
		}

		public IEnumerable<BlogPost> Blogs { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{
			Blogs = (await _blogService.GetBlogPostsAsync()).OrderByDescending(b => b.Published).Take(4);
			return Page();
		}
	}
}
