using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscriminatedUnion;


namespace FPTalk1
{
	public enum RequestMethod
	{
		TRACE,
		PUT,
		POST,
		PATCH,
		OPTIONS,
		HEAD,
		GET,
		DELETE,
		CONNECT
	}
}
