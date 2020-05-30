using System;
using System.Collections.Generic;

namespace SkoorCard.Services
{
	public class Card
	{
		public Card()
		{
			Id = Guid.NewGuid();
			CardPlayers = new List<CardPlayer>();
		}

		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public CourseData CourseData { get; internal set; }
		public IList<CardPlayer> CardPlayers { get; internal set; }
	}
}