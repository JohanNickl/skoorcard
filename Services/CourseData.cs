using System.Collections.Generic;
using System.Linq;

namespace SkoorCard.Services
{
	public class CourseData
	{
		public string Name { get; internal set; }
		internal IList<GolfHole> Holes { get; set; }
		internal IList<Slope> Slopes { get; set; }
		public int ParFirst9 => Holes == null ? 0 : Holes.Where(x => x.Number <= 9).Sum(x => x.Par);
		public int DistanceYellowFirst9 => Holes == null ? 0 : Holes.Where(x => x.Number <= 9).Sum(x => x.DistanceY);
		public int DistanceRedFirst9 => Holes == null ? 0 : Holes.Where(x => x.Number <= 9).Sum(x => x.DistanceR);
		public int ParLast9 => Holes == null ? 0 : Holes.Where(x => x.Number > 9).Sum(x => x.Par);
		public int DistanceYellowLast9 => Holes == null ? 0 : Holes.Where(x => x.Number > 9).Sum(x => x.DistanceY);
		public int DistanceRedLast9 => Holes == null ? 0 : Holes.Where(x => x.Number > 9).Sum(x => x.DistanceR);
		public int Par => ParFirst9 + ParLast9;
		public int DistanceYellow => DistanceYellowFirst9 + DistanceYellowLast9;
		public int DistanceRed => DistanceRedFirst9 + DistanceRedLast9;
		
	}
}