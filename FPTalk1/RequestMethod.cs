using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscriminatedUnion;


namespace FPTalk1
{
	using DiscriminatedUnion;
	using Microsoft.AspNetCore.Http;

	using static DiscriminatedUnion.MatchExtensions;



	public static class RequestMethod
	{
		public class Method : Union<Connect, Delete, Get, Head, Options, Patch, Post, Put, Trace>
		{
			private string item;

			public static implicit operator Method(string item) => new Method(item);
			public static implicit operator Method(Get item) => new Method(item);
			public static implicit operator Method(Post item) => new Method(item);

			public Method(ITypeContainer value) : base(value)
			{
			}

			public Method(Connect value) : base(value)
			{
			}

			public Method(Delete value) : base(value)
			{
			}

			public Method(Get value) : base(value)
			{
			}

			public Method(Head value) : base(value)
			{
			}

			public Method(Options value) : base(value)
			{
			}

			public Method(Patch value) : base(value)
			{
			}

			public Method(Post value) : base(value)
			{
			}

			public Method(Put value) : base(value)
			{
			}

			public Method(Trace value) : base(value)
			{
			}

			public Method(string item) 
				: base(item.Match<ITypeContainer>()
					.Case(x => HttpMethods.IsConnect(x), x => new TypedContainer<Connect>(Connect.Value))
					.Case(x => HttpMethods.IsGet(x), x => new TypedContainer<Get>(Get.Value))
					.Case(x => HttpMethods.IsHead(x), x => new TypedContainer<Head>(Head.Value))
					.Case(x => HttpMethods.IsOptions(x), x => new TypedContainer<Options>(Options.Value))
					.Case(x => HttpMethods.IsPatch(x), x => new TypedContainer<Patch>(Patch.Value))
					.Case(x => HttpMethods.IsPost(x), x => new TypedContainer<Post>(Post.Value))
					.Case(x => HttpMethods.IsPut(x), x => new TypedContainer<Put>(Put.Value))
					.Case(x => HttpMethods.IsTrace(x), x => new TypedContainer<Trace>(Trace.Value))
					.Default(() => new TypedContainer<Get>(Get.Value))){}
		}

		public class Trace
		{
			public static Trace Value { get; } = new Trace();

			private Trace() { }
		}

		public class Put
		{
			public static Put Value { get; } = new Put();
			private Put() { }
		}

		public class Post
		{
			public static Post Value { get; } = new Post();
			private Post() { }
		}

		public class Patch
		{
			public static Patch Value { get; } = new Patch();
			private Patch() { }
		}

		public class Options
		{
			public static Options Value { get; } = new Options();
			private Options() { }
		}

		public class Head
		{
			public static Head Value { get; } = new Head();
			private Head() { }
		}

		public class Get
		{
			public static Get Value { get; } = new Get();
			private Get() { }
		}

		public class Delete
		{
			public static Delete Value { get; } = new Delete();
			private Delete() { }
		}

		public class Connect
		{
			public static Connect Value { get; } = new Connect();
			private Connect() { }
		}
	}
}
