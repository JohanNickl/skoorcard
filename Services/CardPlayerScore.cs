using System;

namespace SkoorCard.Services
{
	public class CardPlayerScore
	{
		public Guid Id { get; }
		public CardPlayer CardPlayer { get; }
		public int HoleNumber { get; set; }
		public int Score { get; set; }
		public string ScoreAsString => Score.ToString();

		public int? Points { get; internal set; }
		public string PointsAsString => Points.HasValue ? Points.Value.ToString() : "";

		public CardPlayerScore(CardPlayer cardPlayer)
		{
			Id = Guid.NewGuid();
			this.CardPlayer = cardPlayer;
		}
	}
}