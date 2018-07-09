using DiscriminatedUnion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using static DiscriminatedUnion.MatchExtensions;

namespace FPTalk1
{
	public static class MiddlewareExtensions
    {
		//public static ICase<HttpContext, TReturn> Match<TReturn>(this HttpContext value) =>
		//	Match<HttpContext, TReturn>(value);
		
		public static IApplicationBuilder Use<T1>(this IApplicationBuilder app, Func<T1, HttpContext, Func<Task>, Task> handler, T1 a)
		{
			return app.Use(Adapt(handler)(a));
		}

		public static IApplicationBuilder UseContextWriter(this IApplicationBuilder app, (Request request, Action<Stream, Stream> act) routedHandler)
		{
			return app.Use(Adapt<(Request request, Action<Stream, Stream> act)>(ContextWriter.WriteAsync)(routedHandler));
		}

		public static Func<T1, Func<HttpContext, Func<Task>, Task>> Adapt<T1>(Func<T1, HttpContext, Func<Task>, Task> implementation)
			=> a => (context, next) => implementation(a, context, next);
	}
}
