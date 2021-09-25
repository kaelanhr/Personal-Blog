using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBlog.Services
{
	public class MemoryCacheService : IMemoryCacheService
	{
		public MemoryCache Cache { get; private set; }
		public MemoryCacheService()
		{
			var a = new MemoryCacheEntryOptions();
			a.SetSlidingExpiration(TimeSpan.FromHours(6));
			Cache = new MemoryCache(new MemoryCacheOptions
			{
				SizeLimit = 1024,
			});
		}
	}
}
