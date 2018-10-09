using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using DiscriminatedUnion;
using static FPTalk1.Content;
using static DiscriminatedUnion.MatchExtensions;

//using contentType = DiscriminatedUnion
//	.Union<FPTalk1.Content.Json,
//		   FPTalk1.Content.Url,
//		   FPTalk1.Content.Plain,
//		   FPTalk1.Content.OctetStream>;
using static FPTalk1.RequestMethod;

namespace FPTalk1
{
	public class Startup
	{

		readonly Func<HttpContext, Func<Task>, Task> UnitHandler = (context, next) => next();
		
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			var filter = MethodFilter(RequestMethod.POST).And(PathFilter("/"));
			
			app.Use(Middleware(filter, fun((string a) => $"Hello {a}").Jsonify()));
			app.Use(Middleware(h => true, fun((string a) => $"Goodbye").Jsonify()));
		}

	}
}
