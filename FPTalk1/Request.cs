using DiscriminatedUnion;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using static DiscriminatedUnion.Discriminator;
namespace FPTalk1
{
	using System.Collections;
	using System.IO;
	using System.Linq;

	// type Segment = Segment of String
	public class Segment : Tag<Segment, string> { }

	// type Host = Host of String
	public class Host : Tag<Host, string> { }

	// type URI = Uri | URI * Segment
	public class Request : Union<Uri, (Uri, RequestMethod.Method method)>
	{
		public static implicit operator Request((Uri, RequestMethod.Method) item) => new Request(item);
		public static implicit operator Request(Uri item) => new Request(item);

		public Request(Uri value) : base(value){}

		public Request((Uri, RequestMethod.Method) value) : base(value)
		{
		}
				
		public static implicit operator Request(string val)
		{
			if (Uri.TryCreate(val, UriKind.Relative, out var uri))
				return uri;

			return new Uri("/");
		}
	}


}
