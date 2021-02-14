using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BlogWebsite.Helper;
using BlogWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogWebsite.Pages
{
	public class BlogListModel : PageModel
	{
		public BlogListModel()
		{
			Blogs = new List<BlogPost>();
		}

		public IList<BlogPost> Blogs { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{
			var files = Directory.GetFiles("Content/Blogs", "*.md");

			// loop through all of the markdown files in the blogs folder and add to
			foreach (var file in files)
			{
				var markdownFile = await System.IO.File.ReadAllTextAsync(file);
				var docBuilder = new MarkDownDocBuilder<BlogPost>(markdownFile)
					.WithYamlData()
					.Build();
				Blogs.Add(docBuilder.YamlData);
			}
			return Page();
		}
	}
}