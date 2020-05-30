using System;
using System.Collections.Generic;
using System.Linq;
using SkoorCard.Models;

namespace SkoorCard.Services
{
	public class CardPlayer
	{


		public CardPlayer(Card card, Player player, Tee.TeeType teeType)
		{
			Id = Guid.NewGuid();
			this.Card = card;
			this.Player = player;
			this.TeeType = teeType;
			CardPlayerScores = new List<CardPlayerScore>();
		}

		public Guid Id { get; }
		public Card Card { get; }
		public IList<CardPlayerScore> CardPlayerScores { get; }
		public Player Player {get;}
		public Tee.TeeType TeeType {get; internal set;}
		public int ScoreOut => CardPlayerScores.Where(x => x.HoleNumber <= 9).Sum(x => x.Score);
		public int PointsOut => CardPlayerScores.Where(x => x.HoleNumber <= 9 && x.Points.HasValue).Sum(x => x.Points.Value);

		public int ScoreIn => CardPlayerScores.Where(x => x.HoleNumber > 9).Sum(x => x.Score);
		public int PointsIn => CardPlayerScores.Where(x => x.HoleNumber > 9 && x.Points.HasValue).Sum(x => x.Points.Value);

		public int Score => ScoreOut + ScoreIn;
		public int Points => PointsIn + PointsOut;

		public int? RoundHandicap { get; internal set; }
		public string RoundHandicapAsString => RoundHandicap.HasValue ? RoundHandicap.Value.ToString() : "";

		public void ChangeRoundHandicap(int roundHandicap) {
			this.RoundHandicap = roundHandicap;
		}
	}
}
