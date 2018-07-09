using DiscriminatedUnion;
using DiscriminatedUnion.Json;
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
		public static Action<Stream, Stream> ToJsonHandler<T, TReturn>(this Func<T, TReturn> testfun) => 
			testfun.IBindJson().IBindStream();

		public static Func<string, string> IBindJson<T, TReturn>(this Func<T, TReturn> fun)
			=> (string a) => ToJson(fun(FromJson<T>(a)));


		private static string ToJson<T>(T vaue) => JsonConvert.SerializeObject(vaue, new UnionJsonConverterDynamic<T>());

		private static T FromJson<T>(string json) => JsonConvert.DeserializeObject<T>(json, new UnionJsonConverterDynamic<T>());

		////public static Func<string, string> IBindPlainText<T, TReturn>(Func<T, TReturn> fun)
		////	=> (string a) => ToJson(fun(FromJson<T>(a)));

		////public static Func<string, string> IBindFormUrlEncoding<T, TReturn>(Func<T, TReturn> fun)
		////	=> (string a) => ToJson(fun(FromJson<T>(a)));

		////public static Func<string, string> IBindOctetStream<T, TReturn>(Func<T, TReturn> fun)
			////=> (string a) => ToJson(fun(FromJson<T>(a)));

		//public class Json : Tag<Json, string> { }

		//public class Url : Tag<Url, string> { }

		//public class Plain : Tag<Plain, string> { }

		//public class OctetStream : Tag<OctetStream, string> { }

		//public class ContentType : Union<Json, Url, Plain, OctetStream>
		//{
		//	public static implicit operator ContentType(string item) => new ContentType(item);
		//	public static implicit operator ContentType(Json item) => new ContentType(item);
		//	public static implicit operator ContentType(Url item) => new ContentType(item);
		//	public static implicit operator ContentType(Plain item) => new ContentType(item);
		//	public static implicit operator ContentType(OctetStream item) => new ContentType(item);

		//	public ContentType(string value) : base(
		//		value.Match<ITypeContainer>()
		//			.Case(c => c == "text/json", v => new TypedContainer<Json>(new Json()))
		//			.Case(c => c == "application/json", v => new TypedContainer<Json>(new Json()))
		//			.Case(c => c == "text/url", v => new TypedContainer<Url>(new Content.Url()))
		//			.Case(c => c == "text/plain", v => new TypedContainer<Plain>(new Plain()))
		//			.Case(c => c == "application/octet-stream", v => new TypedContainer<OctetStream>(new OctetStream()))
		//			.Default(() => new TypedContainer<Plain>(new Plain()))){ }

		//	public ContentType(Json value) : base(value)
		//	{
		//	}

		//	public ContentType(Url value) : base(value)
		//	{
		//	}

		//	public ContentType(Plain value) : base(value)
		//	{
		//	}

		//	public ContentType(OctetStream value) : base(value)
		//	{
		//	}
		//}
	}
}

