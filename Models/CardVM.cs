using SkoorCard.Services;
using System;

namespace SkoorCard.Models
{
	public class CardVM {
		private readonly Card card;

		public CardVM(Card card)
		{
			this.card = card;
		}

		public string CourseName => card.CourseData.Name;
		public DateTime Created => card.Created;
	}
}