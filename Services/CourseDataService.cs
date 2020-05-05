using System.Collections.Generic;
using System.Threading.Tasks;
using static SkoorCard.Services.Tee;

namespace SkoorCard.Services
{
	public interface ICourseDataService
	{
		Task<CourseData> GetByCourseIdAsync(string id);
	}

	public class CourseDataService : ICourseDataService
	{
		public async Task<CourseData> GetByCourseIdAsync(string id)
		{
			return await Task.FromResult( new CourseData()
			{
				Name = "Kalmar GK - gamla banan",
				Holes = new List<GolfHole> {
					new GolfHole(1, 3, 10, 134, 118),
					new GolfHole(2, 4, 6, 361, 316),
					new GolfHole(3, 4, 8, 337, 266),
					new GolfHole(4, 4, 14, 331, 272),
					new GolfHole(5, 4, 2, 377, 286),
					new GolfHole(6, 3, 16, 152, 124),
					new GolfHole(7, 5, 18, 432, 367),
					new GolfHole(8, 5, 4, 512, 421),
					new GolfHole(9, 4, 12, 296, 253),
					new GolfHole(10, 4, 9, 339, 303),
					new GolfHole(11, 4, 17, 326, 305),
					new GolfHole(12, 5, 3, 464, 385),
					new GolfHole(13, 3, 1, 174, 140),
					new GolfHole(14, 5, 13, 498, 444),
					new GolfHole(15, 4, 7, 367, 316),
					new GolfHole(16, 3, 15, 153, 113),
					new GolfHole(17, 4, 5, 263, 233),
					new GolfHole(18, 4, 11, 316, 282)
				},
				Slopes = new List<Slope> {
					new Slope() {
						Tee = TeeType.Yellow,
						CourseRating = 71.7,
						SlopeRating = 142,
						Handicaps = new List<SlopeItem>() {
							new SlopeItem(-4, -3.4, -5),
							new SlopeItem(-3.3, -2.6, -4),
							new SlopeItem(11.0, 11.7, 14),
							new SlopeItem(11.8, 12.5, 15),
							new SlopeItem(12.6, 13.3, 16),
						}
					},
					new Slope() {
						Tee = TeeType.Red,
						CourseRating = 67.2,
						SlopeRating = 133,
						Handicaps = new List<SlopeItem>() {
							new SlopeItem(-4, -4, -10),
							new SlopeItem(-3.9, -3.2, -9),
							new SlopeItem(45.3, 46.1, 49),
							new SlopeItem(46.2, 46.9, 50),
							new SlopeItem(47.0, 47.8, 51),
							new SlopeItem(47.9, 48.6, 52),
							new SlopeItem(48.7, 49.5, 53)
						}
					}
				}
			});
		}
	}
}