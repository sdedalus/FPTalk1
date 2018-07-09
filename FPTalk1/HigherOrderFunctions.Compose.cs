using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FPTalk1
{
    public static partial class HigherOrderFunctions
    {
		public static Action<Stream, Stream> IBindStream(this Func<string, string> fun) =>
			(Stream a, Stream output) =>
			{
				using (var reader = new StreamReader(a))
				using (var writer = new StreamWriter(output))
					writer.Write(fun(reader.ReadToEnd()));
			};
	}
}
