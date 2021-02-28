using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
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

		[StringLength(60, MinimumLength = 3)]
		[BindProperty(SupportsGet = true)]
		public string SearchString { get; set; }
		public BlogListModel(IBlogService blogService)
		{
			_blogService = blogService;
			Blogs = new List<BlogPost>();
		}

		public IEnumerable<BlogPost> Blogs { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{
			var blogList = await _blogService.GetBlogPostsAsync();

			if (SearchString != null)
			{
				CultureInfo culture = new CultureInfo("en-AU");
				var stdSearch = SearchString.Replace("+", " ");


				// whether the string is contained in either the title or in one of the tags.
				blogList = blogList
					.Where(x =>
					x.Title.IndexOf(stdSearch, System.StringComparison.OrdinalIgnoreCase) >= 0
					|| x.Tags.Where(t => t.IndexOf(stdSearch, System.StringComparison.OrdinalIgnoreCase) >= 0).Any()
					);
			}

			Blogs = blogList.OrderByDescending(b => b.Published);
			return Page();
		}
	}
}