using System;
using System.Collections.Generic;

namespace SkoorCard.Services
{
	public class Card
	{
		public Card()
		{
			Players = new List<Player>();
			PlayerScores = new List<PlayerScore>();
		}

		public DateTime Created { get; set; }
		public CourseData CourseData { get; internal set; }
		public IList<Player> Players { get; internal set; }
		public IList<PlayerScore> PlayerScores { get; internal set; }
	}
}