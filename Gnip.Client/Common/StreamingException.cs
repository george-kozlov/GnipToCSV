
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/23/2013</date>
// <summary>StreamingException class</summary>

using System;
using System.Net;

namespace Gnip.Client.Common
{
	public sealed class StreamingException : Exception
	{
		public StreamingException(HttpStatusCode code, string description)
		{
			StatusCode = code;
			Description = description;
		}

		public HttpStatusCode StatusCode
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}
	}
}
