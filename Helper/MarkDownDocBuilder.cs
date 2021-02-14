using System.IO;
using Markdig;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace BlogWebsite.Helper
{
	public class MarkDownDocBuilder<T>
	{
		private MarkDownDoc<T> Doc = new MarkDownDoc<T>();
		private string Input;

		public MarkDownDocBuilder(string input)
		{
			Input = input;
		}

		public MarkDownDoc<T> Build() => Doc;


		public MarkDownDocBuilder<T> WithYamlData()
		{
			// create the yaml deserialiser.
			var yamlDeserializer = new DeserializerBuilder()
				.WithNamingConvention(CamelCaseNamingConvention.Instance)
				.Build();

			// read the input and extract the yaml front matter into an object.
			using (var reader = new StringReader(Input))
			{
				var parser = new Parser(reader);
				parser.Consume<StreamStart>();
				parser.Consume<DocumentStart>();
				var content = yamlDeserializer.Deserialize<T>(parser);
				parser.Consume<DocumentEnd>();
				Doc.YamlData = content;
			}
			return this;
		}

		public MarkDownDocBuilder<T> WithContent()
		{
			var builder = new MarkdownPipelineBuilder()
				.UseYamlFrontMatter()
				.Build();

			Doc.Document = Markdown.ToHtml(Input, builder);
			return this;
		}

	}
}