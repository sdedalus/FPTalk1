using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DiscriminatedUnion;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static FPTalk1.Content;

namespace FPTalk1
{
    public class Program
    {
        public static void Main(string[] args)
        {

			////var x = (""
			////	.Match<ITypeContainer>()
			////	.Case(c => c == "text/json", v => new TypedContainer<Json>(new Json()) )
			////	.Case(c => c == "application/json", v => new TypedContainer<Json>(new Json()) as ITypeContainer)
			////	.Case(c => c == "text/url", v => new TypedContainer<Url>(new Content.Url()) as ITypeContainer)
			////	.Case(c => c == "text/plain", v => new TypedContainer<Plain>(new Plain()) as ITypeContainer)
			////	.Case(c => c == "application/octet-stream", v => new TypedContainer<OctetStream>(new OctetStream()) as ITypeContainer)
			////	.Default(() => new TypedContainer<Plain>(new Plain()) as ITypeContainer));

			BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
