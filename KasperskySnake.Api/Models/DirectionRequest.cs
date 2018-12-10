using System;
using System.Collections.Generic;
using System.Text;

namespace KasperskySnake.Api.Models
{
	public class DirectionRequest
	{
		public DirectionRequest(string direction, string token)
		{
			this.direction = direction;
			this.token = token;
		}
		public string direction;
		public string token;

	}
}
