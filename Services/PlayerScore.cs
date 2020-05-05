using System.Collections.Generic;
using System.Linq;

namespace SkoorCard.Services
{
	public class PlayerScore
	{
		public PlayerScore()
		{
			HoleScores = new List<PlayerHoleScore>();
		}

		public string PlayerId { get; internal set; }
		public IList<PlayerHoleScore> HoleScores { get; set; }
		public int ScoreOut => HoleScores.Where(x => x.HoleNumber <= 9).Sum(x => x.Score);
		public int PointsOut => HoleScores.Where(x => x.HoleNumber <= 9 && x.Points.HasValue).Sum(x => x.Points.Value);

		public int ScoreIn => HoleScores.Where(x => x.HoleNumber > 9).Sum(x => x.Score);
		public int PointsIn => HoleScores.Where(x => x.HoleNumber > 9 && x.Points.HasValue).Sum(x => x.Points.Value);

		public int Score => ScoreOut + ScoreIn;
		public int Points => PointsIn + PointsOut;

		public int? RoundHandicap { get; internal set; }
		public string RoundHandicapAsString => RoundHandicap.HasValue ? RoundHandicap.Value.ToString() : "";
	}
}
