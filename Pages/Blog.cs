using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PersonalBlog.Models;
using PersonalBlog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PersonalBlog.Pages
{
	public class BlogListModel : PageModel
	{
		private readonly IBlogService _blogService;

		[StringLength(60, MinimumLength = 3)]
		[BindProperty(SupportsGet = true)]
		public string Search { get; set; }

		[BindProperty(SupportsGet = true)]
		public int PageNum { get; set; } = 1;

		public int PerPage { get; } = 8;
		public int TotalPages { get; set; }

		public BlogListModel(IBlogService blogService)
		{
			_blogService = blogService;
			Blogs = new List<BlogPost>();
		}

		public IEnumerable<BlogPost> Blogs { get; set; }
		public async Task<IActionResult> OnGetAsync()
		{
			if (PageNum < 1)
			{
				PageNum = 1;
			}

			var blogList = await _blogService.GetBlogPostsAsync();

			if (Search != null)
			{
				CultureInfo culture = new CultureInfo("en-AU");
				var stdSearch = HttpUtility.UrlDecode(Search);

				// whether the string is contained in either the title or in one of the tags.
				blogList = blogList
					.Where(x =>
					x.Title.IndexOf(stdSearch, System.StringComparison.OrdinalIgnoreCase) >= 0
					|| x.Tags.Where(t => t.IndexOf(stdSearch, System.StringComparison.OrdinalIgnoreCase) >= 0).Any()
					);
			}

			blogList = blogList.OrderByDescending(b => b.Published);
			TotalPages = (blogList.Count() + PerPage - 1) / PerPage;
			Blogs = blogList.OrderByDescending(b => b.Published).Skip(PerPage * (PageNum - 1)).Take(PerPage);
			return Page();
		}
	}
}