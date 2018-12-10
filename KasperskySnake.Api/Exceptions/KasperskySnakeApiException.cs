using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace KasperskySnake.Api.Exceptions
{
	public class KasperskySnakeApiException : Exception
	{
		public KasperskySnakeApiException() : base() { }
		public KasperskySnakeApiException(string message, IRestResponse response) : base(message) { this.response = response; }
		public KasperskySnakeApiException(string message, Exception e) : base(message, e) { }

		public IRestResponse response;
	}
}
