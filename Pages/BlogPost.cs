using System.Threading.Tasks;
using BlogWebsite.Helper;
using BlogWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogWebsite.Pages
{
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