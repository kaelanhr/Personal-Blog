using Microsoft.Extensions.Caching.Memory;

namespace PersonalBlog.Services
{
	public interface IMemoryCacheService
	{
		MemoryCache Cache { get; }
	}
}