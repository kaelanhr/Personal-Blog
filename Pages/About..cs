using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalBlog.Helper;

namespace PersonalBlog.Pages
{
	public class AboutModel : PageModel
	{
		public string AboutContent;
		public async Task<IActionResult> OnGetAsync()
		{
			var filePath = $"Content/Personal/AboutMe.md";

			if (!System.IO.File.Exists(filePath))
			{
				return RedirectToPage("Error404");
			}

			var markdownFile = await System.IO.File.ReadAllTextAsync(filePath);
			var post = new MarkDownDocBuilder<string>(markdownFile)
				.WithContent()
				.Build();

			AboutContent = post.Document;

			if (AboutContent == null)
			{
				return RedirectToPage("Error404");
			}

			return Page();
		}
	}
}
