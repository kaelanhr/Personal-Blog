using System.Threading.Tasks;
using PersonalBlog.Helper;
using PersonalBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PersonalBlog.Pages
{
	[ResponseCache(Duration = 60 * 60 * 12)]
	public class BlogModel : PageModel
	{
		public BlogPost Blog { get; set; }
		public async Task<IActionResult> OnGetAsync(string? postTitle)
		{
			if (postTitle == null)
			{
				return NotFound();
			}

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

			Blog = post.YamlData;

			if (Blog == null)
			{
				return RedirectToPage("Error404");
			}

			Blog.Content = post.Document;

			return Page();
		}
	}
}