using System;
using System.Collections.Generic;
using System.Text;
//using System.Windows.Media;
namespace KasperskySnake.Api.Models
{
	public class PlayerState
	{
		public override string ToString()
		{
			return name;
		}
		public string name;
		public string Name
		{
			get
			{
				return isSpawnProtected ? name+"(Респавн защита)" : name;
			}
		}

		

		public bool isSpawnProtected;
		public List<Point2D> snake;

	}
}
