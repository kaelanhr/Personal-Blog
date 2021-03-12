using System;

namespace PersonalBlog.Models
{
	/// <summary>
	/// A single blog post contains both the content of the article
	/// and the meta data.
	/// </summary>
	public class BlogPost
	{
		// if applicable, the last date it was edited.
		public DateTime LastEdited { get; set; }

		// the publish date of the post
		public DateTime Published { get; set; }

		// the title of the blog post (should be unique)
		public string Title { get; set; }

		// the url path of blog post and assets.
		public string Path { get; set; }

		// the html content of the blog post.
		public string Content { get; set; }

		// a brief summary of the article.
		public string Blurb { get; set; }

		// searchable tags relevant to the subject matter of the article.
		public string[] Tags { get; set; }

		// estimated minutes to read the article.
		public int Length { get; set; }

	}
}