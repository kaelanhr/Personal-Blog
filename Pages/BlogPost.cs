using System.Threading.Tasks;
using PersonalBlog.Helper;
using PersonalBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using PersonalBlog.Services;

namespace PersonalBlog.Pages
{
	[ResponseCache(Duration = 60 * 60 * 3)]
	public class BlogModel : PageModel
	{

		private IMemoryCache _cache;

		public BlogModel(IMemoryCacheService cache)
		{
			_cache = cache.Cache;
		}

		public BlogPost Blog { get; set; }
		public async Task<IActionResult> OnGetAsync(string? postTitle)
		{
			if (postTitle == null)
			{
				return NotFound();
			}

			if (!_cache.TryGetValue(postTitle, out BlogPost cachedBlog))
			{
				var filePath = $"Content/Blogs/{postTitle}.md";

				if (!System.IO.File.Exists(filePath))
				{
					return RedirectToPage("Error404");
				}

				var markdownFile = await System.IO.File.ReadAllTextAsync(filePath);
				var post = new MarkDownDocBuilder<BlogPost>(markdownFile)
					.WithYamlData()
					.WithContent()
					.Build();

				cachedBlog = post.YamlData;

				if (cachedBlog == null)
				{
					return RedirectToPage("Error404");
				}
				var cacheEntryOptions = new MemoryCacheEntryOptions()
						.SetSize(1);

				cachedBlog.Content = post.Document;
				_cache.Set(postTitle, cachedBlog, cacheEntryOptions);
			}
			Blog = cachedBlog;
			return Page();
		}
	}
}