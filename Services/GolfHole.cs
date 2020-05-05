namespace SkoorCard.Services
{
	internal class GolfHole
	{
		public GolfHole()
		{
			
		}
		public GolfHole(int number, int par, int index, int distanceY, int distanceR)
		{
			this.Number = number;
			this.Par = par;
			this.Index = index;
			this.DistanceY = distanceY;
			this.DistanceR = distanceR;
		}
		public int Number { get; internal set; }
		public int Par { get; internal set; }
		public int Index { get; internal set; }
		public int DistanceY { get; internal set; }
		public int DistanceR { get; internal set; }
	}
}