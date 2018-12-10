using System;
using System.Collections.Generic;
using System.Text;

namespace KasperskySnake.Api.Models
{
	public class GameStateResponse
	{
		public bool isStarted;
		public bool isPaused;
		public int roundNumber;
		public int turnNumber;
		public int turnTimeMilliseconds;
		public int timeUntilNextTurnMilliseconds;
		public Size2D gameBoardSize;
		public int maxFood;
		public List<PlayerState> players;
		public List<Point2D> food;
		public List<Rectangle2D> walls;
	}
}
