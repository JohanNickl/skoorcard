using System.Collections.Generic;
using static SkoorCard.Services.Tee;

namespace SkoorCard.Services
{
	internal class Slope
	{
		public double CourseRating { get; internal set; }
		public int SlopeRating { get; internal set; }
		public TeeType Tee { get; internal set; }
		public List<SlopeItem> Handicaps { get; internal set; }
	}
}