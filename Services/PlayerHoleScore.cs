namespace SkoorCard.Services
{
	public class PlayerHoleScore
	{
		public string PlayerId { get; internal set; }
		public int HoleNumber { get; set; }
		public int Score { get; set; }
		public string ScoreAsString => Score.ToString();

		public int? Points { get; internal set; }
		public string PointsAsString => Points.HasValue ? Points.Value.ToString() : "";
	}
}