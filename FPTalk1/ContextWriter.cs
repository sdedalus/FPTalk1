using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FPTalk1
{
    public static class ContextWriter
    {
		public static Task WriteAsync((Request request, Action<Stream, Stream> act) routedHandler, HttpContext context, Func<Task> next)
		{
			var request = ToRequest(context);

			if (Compare(routedHandler.request, request))
			{
				routedHandler.act(context.Request.Body, context.Response.Body);

				return Task.CompletedTask;
			}
			else
				return next();
		}

		private static bool Compare(Request a, Request b)
		{
			(Request achild, Request bchild, bool match) tmp = (a, b, true);
			bool output = true;
			do
			{
				tmp = tmp.achild.Match<(Request achild, Request bchild, bool match)>()
						   .Case(x => tmp.bchild.Match<(Request, Request, bool)>()
							   .Case(y => (null, null, x == y))
							   .Case(y => (x, y.Item1, false))
							   .Default(() => (x, null, false)))
						   .Case(x => b.Match<(Request, Request, bool)>()
							   .Case(y => (x.Item1, y, false))
							   // this is problematic I should implement equality
							   .Case(y => (x.Item1, y.Item1, x.method.ValueContainer.ValueAsObject == y.method.ValueContainer.ValueAsObject))
							   .Default(() => (x.Item1, null, false)))
						   .Default(() => (null, null, false));
				output = output && tmp.match;
			} while (tmp.achild != null && tmp.bchild != null);

			return output;
		}

		private static Request ToRequest(HttpContext context)
		{
			Uri uri = new Uri(context.Request.Path.ToString());
			return (uri, new RequestMethod.Method(context.Request.Method));
		}
	}
}
