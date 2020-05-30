using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SkoorCard.Services.Tee;

namespace SkoorCard.Services
{
	public interface ICardService
	{
		void AddPlayer(Card card, Player player, TeeType teeType);
		Task<Card> CreateAsync(CourseData cd);
		void AddScore(Card card, CardPlayerScore score);

		Card GetCard();
		int CalculateExtraStrokes(int? handicap, int holeIndex);
		void AddPlayer(Card card, CardPlayer cardPlayer);
	}

	public class CardService : ICardService
	{
		private Card Card { get; set; }

		public Card GetCard() { return Card; }

		public async Task<Card> CreateAsync(CourseData cd)
		{
			var card = new Card()
			{
				Created = DateTime.Now,
				CourseData = cd
			};

			this.Card = card;

			return await Task.FromResult(card);
		}

		private int? GetRoundHandicap(Player player, TeeType teeType) {
				int? roundHandicap = null;
				// find slope handicap for player
				if (Card != null) {
					var slopeTee = Card.CourseData.Slopes.SingleOrDefault(x => x.Tee.Equals(teeType));
					if (slopeTee != null) {
						var slope = slopeTee.Handicaps.SingleOrDefault(x => x.Low <= player.Handicap && x.High >= player.Handicap);
						if (slope != null) {
							roundHandicap = slope.Hcp;
						}
					}
				}
				return roundHandicap;
		}

		public void AddPlayer(Card card, Player player, TeeType teeType)
		{	
			int? roundHandicap = GetRoundHandicap(player, teeType);
			AddPlayer(card, player, teeType, roundHandicap);
		}

		public void AddPlayer(Card card, Player player, TeeType teeType, int? roundHandicap) {
			if (!card.CardPlayers.Any(x => x.Player.Id.Equals(player.Id))) {
				AddPlayer(card, new CardPlayer(card, player, teeType) {
					RoundHandicap = roundHandicap
				});
			}
		}

		public void AddPlayer(Card card, CardPlayer cardPlayer) {
			var existing = card.CardPlayers.SingleOrDefault(x => x.Id.Equals(cardPlayer.Id));
			if (existing != null) {
				// have anything of importance changed?
				if (existing.TeeType != cardPlayer.TeeType && existing.RoundHandicap == cardPlayer.RoundHandicap) {
					// re-calculate
					existing.RoundHandicap = GetRoundHandicap(existing.Player, cardPlayer.TeeType);
					existing.TeeType = cardPlayer.TeeType;
				} else if (existing.RoundHandicap != cardPlayer.RoundHandicap) {
					existing.RoundHandicap = cardPlayer.RoundHandicap;
					existing.TeeType = cardPlayer.TeeType;
				}
			} else {
				// new
				if (!cardPlayer.RoundHandicap.HasValue) {
					cardPlayer.RoundHandicap = GetRoundHandicap(cardPlayer.Player, cardPlayer.TeeType);
				}
				card.CardPlayers.Add(cardPlayer);
			}
			
		}

		public void AddScore(Card card, CardPlayerScore score) {
			
			if (score == null 
				|| score.HoleNumber <= 0
				|| score.HoleNumber > card.CourseData.Holes.Count()
				|| score.CardPlayer.Id.Equals(Guid.Empty)
				|| score.Score <= 0) 
			{
				return;
			}

			CalculatePoints(card, score);
			
			var existing = card.CardPlayers.FirstOrDefault(x => x.Id.Equals(score.CardPlayer.Id)).CardPlayerScores.SingleOrDefault(x => x.HoleNumber == score.HoleNumber);
			if (existing != null) {
				existing.Score = score.Score;
				existing.Points = score.Points;
			} else {
				card.CardPlayers.FirstOrDefault(x => x.Id.Equals(score.CardPlayer.Id)).CardPlayerScores.Add(score);
			}

		}

		private void CalculatePoints(Card card, CardPlayerScore score) {

			var hole = card.CourseData.Holes.SingleOrDefault(x => x.Number == score.HoleNumber);
			var playerScore = card.CardPlayers.SingleOrDefault(x => x.Id.Equals(score.CardPlayer.Id));
			var extraStrokes = CalculateExtraStrokes(playerScore.RoundHandicap, hole.Index);

			var points = 2 + (hole.Par + extraStrokes - score.Score);
			score.Points = points > 0 ? points : 0;
		}

		public int CalculateExtraStrokes(int? handicap, int holeIndex) {
			if (!handicap.HasValue) return 0;

			var whole = (int) Math.Floor(handicap.Value / (double)18);
			var remainder = handicap % 18;

			return whole + (remainder >= holeIndex ? 1 : 0);
		}
	}
}