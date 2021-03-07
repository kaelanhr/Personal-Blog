using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PersonalBlog.Helper;
using PersonalBlog.Models;

namespace PersonalBlog.Services
{
	public class BlogService : IBlogService
	{
		public async Task<IEnumerable<BlogPost>> GetBlogPostsAsync()
		{
			var files = Directory.GetFiles("Content/Blogs", "*.md");

			var blogList = new List<BlogPost>();
			// loop through all of the markdown files in the blogs folder and add to
			foreach (var file in files)
			{
				var markdownFile = await File.ReadAllTextAsync(file);
				var docBuilder = new MarkDownDocBuilder<BlogPost>(markdownFile)
					.WithYamlData()
					.Build();

				if (docBuilder.YamlData is not null)
				{
					blogList.Add(docBuilder.YamlData);
				}
			}

			return blogList;
		}
	}
}
