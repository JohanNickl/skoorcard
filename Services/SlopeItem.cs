namespace SkoorCard.Services
{
	internal class SlopeItem
	{
		
		public SlopeItem(double low, double high, int hcp)
		{
			Low = low;
			High = high;
			Hcp = hcp;
		}

		public double Low { get; }
		public double High { get; }
		public int Hcp { get; }
	}
}