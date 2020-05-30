using System;

namespace SkoorCard.Services
{
	public class Player
	{
		public Guid Id { get; }
		public string Name { get; set; }
		public double Handicap { get; set; }

		public Player()
		{
			Id = Guid.NewGuid();
		}

	}
}