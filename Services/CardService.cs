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
		void AddScore(Card card, PlayerHoleScore score);

		Card GetCard();
		int CalculateExtraStrokes(int handicap, int holeIndex);
		Player GetPlayer(string playerId);
		int LastPlayedHole(string playerId);
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

		public void AddPlayer(Card card, Player player, TeeType teeType)
		{	
			if (string.IsNullOrEmpty(player.Id)) { player.Id = Guid.NewGuid().ToString(); }
			if (!card.Players.Any(x => x.Name.Equals(player.Name)))
			{
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

				card.Players.Add(player);
				card.PlayerScores.Add(new PlayerScore() {
					PlayerId = player.Id,
					RoundHandicap = roundHandicap
				});
			}
		}

		public void AddScore(Card card, PlayerHoleScore score) {
			if (score == null 
				|| score.HoleNumber <= 0
				|| score.HoleNumber > card.CourseData.Holes.Count()
				|| string.IsNullOrEmpty(score.PlayerId) 
				|| score.Score <= 0) 
			{
				return;
			}


			var player = card.Players.SingleOrDefault(x => x.Id.Equals(score.PlayerId));

			if (player != null) {
				var existingPlayerScore = card.PlayerScores.SingleOrDefault(x => x.PlayerId.Equals(player.Id));
				score.Points = CalculatePoints(card, score);

				if (existingPlayerScore != null) {
					var existingHoleScore = existingPlayerScore.HoleScores.SingleOrDefault(x => x.HoleNumber == score.HoleNumber);
					if (existingHoleScore != null) {
						existingHoleScore.Score = score.Score;
						existingHoleScore.Points = score.Points;
					} else {
						existingPlayerScore.HoleScores.Add(score);
					}
				} else {
					card.PlayerScores.Add(new PlayerScore() {
						HoleScores = new List<PlayerHoleScore>() { score },
						PlayerId = score.PlayerId
					});
				}
			}
		}

		private int? CalculatePoints(Card card, PlayerHoleScore score) {

			var hole = card.CourseData.Holes.SingleOrDefault(x => x.Number == score.HoleNumber);
			var playerScore = card.PlayerScores.SingleOrDefault(x => x.PlayerId == score.PlayerId);
			var extraStrokes = CalculateExtraStrokes(playerScore.RoundHandicap.Value, hole.Index);

			var points = 2 + (hole.Par + extraStrokes - score.Score);
			return points > 0 ? points : 0;
		}

		public int CalculateExtraStrokes(int handicap, int holeIndex) {
			var whole = (int) Math.Floor(handicap / (double)18);
			var remainder = handicap % 18;

			return whole + (remainder >= holeIndex ? 1 : 0);
		}

		public Player GetPlayer(string playerId)
		{
			if (Card == null) {
				throw new Exception("A score card must first be created");
			}

			if (Card.Players != null && Card.Players.Count() > 0) {
				return Card.Players.SingleOrDefault(x => x.Id.Equals(playerId));
			}
			return null;
		}

		public int LastPlayedHole(string playerId) {
			int lastHoleNumber = 0;
			if (Card.PlayerScores != null) {
				var xx = Card.PlayerScores?.SingleOrDefault(x => x.PlayerId.Equals(playerId));
				if (xx != null && xx.HoleScores != null && xx.HoleScores.Count > 0) {
					lastHoleNumber = xx.HoleScores.Max(x => x.HoleNumber);
				}
			} 
			return lastHoleNumber;
		}
	}
}