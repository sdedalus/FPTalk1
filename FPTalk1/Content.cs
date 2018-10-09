using DiscriminatedUnion;
using DiscriminatedUnion.Json;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace FPTalk1
{
	public static class Content
	{
		public static Func<HttpContext, Func<Task>, Task> Middleware(
			Func<HttpContext, bool> filter,
			Func<HttpContext, Task> act) =>
			(cont, continuewith) => filter(cont) ? act(cont) : continuewith();

		/// Object to Json 
		public static Stream Json<T>(Stream ms, T myObj)
		{
			using(var writer = new StreamWriter(ms))
			{ 
				writer.Write(JsonConvert.SerializeObject(myObj));
			}
			return ms;
		}

		///// Object from Json 
		public static T Unjson<T>(Stream jsonStream)
		{
			var reader = new StreamReader(jsonStream);
			return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
		}
		public static Func<A, B> fun<A, B>(Func<A, B> f) => f;

		// Function Adapter
		public static Func<HttpContext, Task> Jsonify<A, B>(this Func<A, B> f) =>
			(HttpContext a) => Task.Run(() => Json(a.Response.Body, f(Unjson<A>(a.Request.Body))));

		// Routing Filters
		public static Func<HttpContext, bool> PathFilter(PathString path) =>
			(HttpContext h) =>
				h.Request.Path == path;

		public static Func<HttpContext, bool> MethodFilter(RequestMethod method) =>
			(HttpContext h) => 
				 h.Request.Method == Enum.GetName(typeof(RequestMethod), method);

		public static Func<HttpContext, bool> And(this Func<HttpContext, bool> a, Func<HttpContext, bool> b) =>
			(HttpContext h) => a(h) && b(h);

	}
}

